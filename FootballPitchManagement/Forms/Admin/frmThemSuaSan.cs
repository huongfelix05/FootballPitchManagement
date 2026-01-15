using FootballPitchManagement.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmThemSuaSan : Form
    {
        private int maSan = 0;
        private bool isEditMode = false;

        // Constructor cho chế độ Thêm
        public frmThemSuaSan()
        {
            InitializeComponent();
            isEditMode = false;
            this.Text = "Thêm Sân Mới";
            this.Load += FrmThemSuaSan_Load;
        }

        // Constructor cho chế độ Sửa
        public frmThemSuaSan(int maSan)
        {
            InitializeComponent();
            this.maSan = maSan;
            isEditMode = true;
            this.Text = "Chỉnh Sửa Thông Tin Sân";
            this.Load += FrmThemSuaSan_Load;
        }

        private void FrmThemSuaSan_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxes();

                if (isEditMode)
                {
                    LoadThongTinSan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxes()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Load Chi Nhánh
                    string sqlCN = "SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh WHERE TrangThai = 1 ORDER BY TenChiNhanh";
                    using (SqlCommand cmd = new SqlCommand(sqlCN, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dtCN = new DataTable();
                            dtCN.Load(reader);

                            cboChiNhanh.DataSource = dtCN;
                            cboChiNhanh.DisplayMember = "TenChiNhanh";
                            cboChiNhanh.ValueMember = "MaChiNhanh";
                            cboChiNhanh.SelectedIndex = -1;
                        }
                    }

                    // Load Loại Sân
                    string sqlLS = "SELECT MaLoaiSan, TenLoaiSan FROM LoaiSan ORDER BY TenLoaiSan";
                    using (SqlCommand cmd = new SqlCommand(sqlLS, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dtLS = new DataTable();
                            dtLS.Load(reader);

                            cboLoaiSan.DataSource = dtLS;
                            cboLoaiSan.DisplayMember = "TenLoaiSan";
                            cboLoaiSan.ValueMember = "MaLoaiSan";
                            cboLoaiSan.SelectedIndex = -1;
                        }
                    }

                    // Load Tình Trạng
                    string sqlTT = "SELECT MaTinhTrang, TenTinhTrang FROM TinhTrangSan ORDER BY TenTinhTrang";
                    using (SqlCommand cmd = new SqlCommand(sqlTT, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DataTable dtTT = new DataTable();
                            dtTT.Load(reader);

                            cboTinhTrang.DataSource = dtTT;
                            cboTinhTrang.DisplayMember = "TenTinhTrang";
                            cboTinhTrang.ValueMember = "MaTinhTrang";
                            cboTinhTrang.SelectedIndex = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load ComboBox: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadThongTinSan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT TenSan, MaChiNhanh, MaLoaiSan, GiaMacDinh, MaTinhTrang, GhiChu
                        FROM San WHERE MaSan = @MaSan";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", maSan);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTenSan.Text = reader["TenSan"].ToString();
                                // ✅ SỬA: Dùng txtGiaMacDinh thay vì txtTenSan
                                txtGiaMacDinh.Text = reader["GiaMacDinh"].ToString();
                                txtGhiChu.Text = reader["GhiChu"]?.ToString() ?? "";

                                cboChiNhanh.SelectedValue = reader["MaChiNhanh"];
                                cboLoaiSan.SelectedValue = reader["MaLoaiSan"];
                                cboTinhTrang.SelectedValue = reader["MaTinhTrang"];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load thông tin: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                if (isEditMode)
                    UpdateSan();
                else
                    ThemSanMoi();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenSan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sân!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSan.Focus();
                return false;
            }

            if (cboChiNhanh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboLoaiSan.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại sân!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ✅ SỬA: Kiểm tra txtGiaMacDinh thay vì txtTenSan
            if (string.IsNullOrWhiteSpace(txtGiaMacDinh.Text) || !decimal.TryParse(txtGiaMacDinh.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá mặc định phải là số dương!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaMacDinh.Focus();
                return false;
            }

            if (cboTinhTrang.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn tình trạng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ThemSanMoi()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO San (TenSan, MaLoaiSan, MaChiNhanh, MaTinhTrang, GiaMacDinh, GhiChu, TrangThai)
                        VALUES (@TenSan, @MaLoaiSan, @MaChiNhanh, @MaTinhTrang, @GiaMacDinh, @GhiChu, 1)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenSan", txtTenSan.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaLoaiSan", cboLoaiSan.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaChiNhanh", cboChiNhanh.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaTinhTrang", cboTinhTrang.SelectedValue);
                        // ✅ SỬA: Parse txtGiaMacDinh thay vì txtTenSan
                        cmd.Parameters.AddWithValue("@GiaMacDinh", decimal.Parse(txtGiaMacDinh.Text));
                        cmd.Parameters.AddWithValue("@GhiChu",
                            string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());

                        cmd.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thêm sân: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateSan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        UPDATE San SET 
                            TenSan = @TenSan, 
                            MaLoaiSan = @MaLoaiSan, 
                            MaChiNhanh = @MaChiNhanh, 
                            MaTinhTrang = @MaTinhTrang, 
                            GiaMacDinh = @GiaMacDinh, 
                            GhiChu = @GhiChu
                        WHERE MaSan = @MaSan";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenSan", txtTenSan.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaLoaiSan", cboLoaiSan.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaChiNhanh", cboChiNhanh.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaTinhTrang", cboTinhTrang.SelectedValue);
                        // ✅ SỬA: Parse txtGiaMacDinh thay vì txtTenSan
                        cmd.Parameters.AddWithValue("@GiaMacDinh", decimal.Parse(txtGiaMacDinh.Text));
                        cmd.Parameters.AddWithValue("@GhiChu",
                            string.IsNullOrWhiteSpace(txtGhiChu.Text) ? (object)DBNull.Value : txtGhiChu.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaSan", maSan);

                        cmd.ExecuteNonQuery();
                        this.DialogResult = DialogResult.OK;
                        this.Close();       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
