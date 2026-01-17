using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FootballPitchManagement.Common;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmQuanlysan : Form
    {
        private DataTable dtSanBong = new DataTable();
        private int maSanDangChon = 0;

        public frmQuanlysan()
        {
            InitializeComponent();
            this.Load += FrmQuanlysan_Load;
        }

        private void FrmQuanlysan_Load(object sender, EventArgs e)
        {
            try
            {
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    this.Close();
                    return;
                }

                CauHinhGiaoDien();
                LoadComboBoxChiNhanh();
                LoadComboBoxLoaiSan();
                LoadComboBoxTinhTrang();
                LoadDanhSachSan();
                LoadThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region === CẤU HÌNH GIAO DIỆN ===

        private void CauHinhGiaoDien()
        {
            if (dgvDanhSachSan != null)
            {
                dgvDanhSachSan.AutoGenerateColumns = false;
                dgvDanhSachSan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDanhSachSan.MultiSelect = false;
                dgvDanhSachSan.AllowUserToAddRows = false;
                dgvDanhSachSan.ReadOnly = false;

                // GÁN DataPropertyName
                if (dgvDanhSachSan.Columns["colMaSan"] != null)
                    dgvDanhSachSan.Columns["colMaSan"].DataPropertyName = "MaSan";
                
                if (dgvDanhSachSan.Columns["colTenSan"] != null)
                    dgvDanhSachSan.Columns["colTenSan"].DataPropertyName = "TenSan";
                
                if (dgvDanhSachSan.Columns["colTenChiNhanh"] != null)
                    dgvDanhSachSan.Columns["colTenChiNhanh"].DataPropertyName = "TenChiNhanh";
                
                if (dgvDanhSachSan.Columns["colTenLoaiSan"] != null)
                    dgvDanhSachSan.Columns["colTenLoaiSan"].DataPropertyName = "TenLoaiSan";
                
                if (dgvDanhSachSan.Columns["colSoNguoi"] != null)
                    dgvDanhSachSan.Columns["colSoNguoi"].DataPropertyName = "SoNguoiToiDa";
                
                if (dgvDanhSachSan.Columns["colGia"] != null)
                {
                    dgvDanhSachSan.Columns["colGia"].DataPropertyName = "GiaMacDinh";
                    dgvDanhSachSan.Columns["colGia"].DefaultCellStyle.Format = "N0";
                    dgvDanhSachSan.Columns["colGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                
                if (dgvDanhSachSan.Columns["colTrangThai"] != null)
                    dgvDanhSachSan.Columns["colTrangThai"].DataPropertyName = "TenTinhTrang";

                // ✅ CẤU HÌNH CỘT NÚT SỬA
                if (dgvDanhSachSan.Columns["colEdit"] != null)
                {
                    DataGridViewButtonColumn btnEdit = dgvDanhSachSan.Columns["colEdit"] as DataGridViewButtonColumn;
                    if (btnEdit != null)
                    {
                        btnEdit.Text = "✏️ Sửa";
                        btnEdit.UseColumnTextForButtonValue = true;
                        btnEdit.Width = 80;
                        btnEdit.DefaultCellStyle.BackColor = Color.DodgerBlue;
                        btnEdit.DefaultCellStyle.ForeColor = Color.White;
                    }
                }

                // ✅ CẤU HÌNH CỘT NÚT XÓA
                if (dgvDanhSachSan.Columns["colDelete"] != null)
                {
                    DataGridViewButtonColumn btnDelete = dgvDanhSachSan.Columns["colDelete"] as DataGridViewButtonColumn;
                    if (btnDelete != null)
                    {
                        btnDelete.Text = "🗑️ Xóa";
                        btnDelete.UseColumnTextForButtonValue = true;
                        btnDelete.Width = 80;
                        btnDelete.DefaultCellStyle.BackColor = Color.Crimson;
                        btnDelete.DefaultCellStyle.ForeColor = Color.White;
                    }
                }

                // Gán sự kiện
                dgvDanhSachSan.SelectionChanged -= DgvDanhSachSan_SelectionChanged;
                dgvDanhSachSan.SelectionChanged += DgvDanhSachSan_SelectionChanged;
                
                dgvDanhSachSan.CellFormatting -= DgvDanhSachSan_CellFormatting;
                dgvDanhSachSan.CellFormatting += DgvDanhSachSan_CellFormatting;
                
                dgvDanhSachSan.CellContentClick -= DgvDanhSachSan_CellContentClick;
                dgvDanhSachSan.CellContentClick += DgvDanhSachSan_CellContentClick;
            }

            // Gán sự kiện cho các nút
            if (picThemSan != null) 
            {
                picThemSan.Click -= BtnThemSan_Click;
                picThemSan.Click += BtnThemSan_Click;
            }
            
            //if (btnLamMoi != null) 
            //{
            //    btnLamMoi.Click -= BtnLamMoi_Click;
            //    btnLamMoi.Click += BtnLamMoi_Click;
            //}
        }

        #endregion

        #region === LOAD DỮ LIỆU ===

        private void LoadComboBoxChiNhanh()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh WHERE TrangThai = 1 ORDER BY TenChiNhanh";
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DataRow row = dt.NewRow();
                        row["MaChiNhanh"] = 0;
                        row["TenChiNhanh"] = "-- Tất cả --";
                        dt.Rows.InsertAt(row, 0);

                        cboChiNhanh.DataSource = dt;
                        cboChiNhanh.DisplayMember = "TenChiNhanh";
                        cboChiNhanh.ValueMember = "MaChiNhanh";
                        cboChiNhanh.SelectedIndex = 0;

                        cboChiNhanh.SelectedIndexChanged -= CboFilter_Changed;
                        cboChiNhanh.SelectedIndexChanged += CboFilter_Changed;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load chi nhánh: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadComboBoxLoaiSan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaLoaiSan, TenLoaiSan FROM LoaiSan ORDER BY TenLoaiSan";
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DataRow row = dt.NewRow();
                        row["MaLoaiSan"] = 0;
                        row["TenLoaiSan"] = "-- Tất cả loại --";
                        dt.Rows.InsertAt(row, 0);

                        cboLoaiSan.DataSource = dt;
                        cboLoaiSan.DisplayMember = "TenLoaiSan";
                        cboLoaiSan.ValueMember = "MaLoaiSan";
                        cboLoaiSan.SelectedIndex = 0;

                        cboLoaiSan.SelectedIndexChanged -= CboFilter_Changed;
                        cboLoaiSan.SelectedIndexChanged += CboFilter_Changed;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load loại sân: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadComboBoxTinhTrang()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaTinhTrang, TenTinhTrang FROM TinhTrangSan ORDER BY TenTinhTrang";
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DataRow row = dt.NewRow();
                        row["MaTinhTrang"] = 0;
                        row["TenTinhTrang"] = "-- Tất cả --";        
                        dt.Rows.InsertAt(row, 0);

                        cboTinhTrang.DataSource = dt;
                        cboTinhTrang.DisplayMember = "TenTinhTrang";
                        cboTinhTrang.ValueMember = "MaTinhTrang";
                        cboTinhTrang.SelectedIndex = 0;

                        cboTinhTrang.SelectedIndexChanged -= CboFilter_Changed;
                        cboTinhTrang.SelectedIndexChanged += CboFilter_Changed;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load tình trạng: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDanhSachSan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    int maChiNhanh = cboChiNhanh.SelectedValue != null ? 
                        Convert.ToInt32(cboChiNhanh.SelectedValue) : 0;
                    int maLoaiSan = cboLoaiSan.SelectedValue != null ? 
                        Convert.ToInt32(cboLoaiSan.SelectedValue) : 0;
                    int maTinhTrang = cboTinhTrang.SelectedValue != null ? 
                        Convert.ToInt32(cboTinhTrang.SelectedValue) : 0;

                    string sql = @"
                        SELECT 
                            s.MaSan,
                            s.TenSan,
                            cn.TenChiNhanh,
                            ls.TenLoaiSan,
                            ls.SoNguoiToiDa,
                            s.GiaMacDinh,
                            tt.TenTinhTrang
                        FROM San s
                        JOIN ChiNhanh cn ON s.MaChiNhanh = cn.MaChiNhanh
                        JOIN LoaiSan ls ON s.MaLoaiSan = ls.MaLoaiSan
                        JOIN TinhTrangSan tt ON s.MaTinhTrang = tt.MaTinhTrang";
                        // ✅ BỎ ĐIỀU KIỆN WHERE s.TrangThai = 1

                    if (maChiNhanh > 0)
                        sql += " WHERE s.MaChiNhanh = @MaChiNhanh";
                    if (maLoaiSan > 0)
                    {
                        sql += (maChiNhanh > 0 ? " AND" : " WHERE") + " s.MaLoaiSan = @MaLoaiSan";
                    }
                    if (maTinhTrang > 0)
                    {
                        sql += ((maChiNhanh > 0 || maLoaiSan > 0) ? " AND" : " WHERE") + " s.MaTinhTrang = @MaTinhTrang";
                    }

                    sql += " ORDER BY s.TenSan";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (maChiNhanh > 0) cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                        if (maLoaiSan > 0) cmd.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);
                        if (maTinhTrang > 0) cmd.Parameters.AddWithValue("@MaTinhTrang", maTinhTrang);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtSanBong = new DataTable();
                            da.Fill(dtSanBong);
                        }
                    }

                    dgvDanhSachSan.DataSource = dtSanBong;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load danh sách sân: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadThongKe()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    string sqlTong = "SELECT COUNT(*) FROM San WHERE TrangThai = 1";
                    using (SqlCommand cmd = new SqlCommand(sqlTong, conn))
                    {
                        lblTongSoSan.Text = cmd.ExecuteScalar().ToString();
                    }

                    string sqlHoatDong = @"
                        SELECT COUNT(*) FROM San 
                        WHERE TrangThai = 1 
                        AND MaTinhTrang = (SELECT MaTinhTrang FROM TinhTrangSan WHERE TenTinhTrang = N'Trống')";
                    using (SqlCommand cmd = new SqlCommand(sqlHoatDong, conn))
                    {
                        lblDangHoatDong.Text = cmd.ExecuteScalar().ToString();
                    }

                    string sqlDangDung = @"
                        SELECT COUNT(*) FROM San 
                        WHERE TrangThai = 1 
                        AND MaTinhTrang = (SELECT MaTinhTrang FROM TinhTrangSan WHERE TenTinhTrang = N'Đang sử dụng')";
                    using (SqlCommand cmd = new SqlCommand(sqlDangDung, conn))
                    {
                        lblDangSuDung.Text = cmd.ExecuteScalar().ToString();
                    }

                    string sqlBaoTri = @"
                        SELECT COUNT(*) FROM San 
                        WHERE MaTinhTrang = (SELECT MaTinhTrang FROM TinhTrangSan WHERE TenTinhTrang = N'Bảo trì')";
                    using (SqlCommand cmd = new SqlCommand(sqlBaoTri, conn))
                    {
                        lblBaoTri.Text = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load thống kê: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region === SỰ KIỆN ===

        private void CboFilter_Changed(object sender, EventArgs e)
        {
            LoadDanhSachSan();
            LoadThongKe();
        }

        private void DgvDanhSachSan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSachSan.CurrentRow != null && dgvDanhSachSan.CurrentRow.Index >= 0)
            {
                var cellValue = dgvDanhSachSan.CurrentRow.Cells["colMaSan"].Value;
                if (cellValue != null && cellValue != DBNull.Value)
                {
                    maSanDangChon = Convert.ToInt32(cellValue);
                }
            }
        }

        private void DgvDanhSachSan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDanhSachSan.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                string tinhTrang = e.Value.ToString();
                if (tinhTrang == "Trống")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (tinhTrang == "Đang sử dụng")
                {
                    e.CellStyle.ForeColor = Color.Blue;
                }
                else if (tinhTrang == "Bảo trì")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                e.FormattingApplied = true;
            }
        }

        // ✅ XỬ LÝ CLICK NÚT SỬA VÀ XÓA
        private void DgvDanhSachSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dgvDanhSachSan.Columns[e.ColumnIndex].Name;
                int maSan = Convert.ToInt32(dgvDanhSachSan.Rows[e.RowIndex].Cells["colMaSan"].Value);

                // ✅ NÚT SỬA
                if (columnName == "colEdit")
                {
                    try
                    {
                        frmThemSuaSan frm = new frmThemSuaSan(maSan);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadDanhSachSan();
                            LoadThongKe();
                            MessageBox.Show("Cập nhật sân thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi mở form sửa: {ex.Message}", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // ✅ NÚT XÓA
                if (columnName == "colDelete")
                {
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc muốn xóa sân '{dgvDanhSachSan.Rows[e.RowIndex].Cells["colTenSan"].Value}'?\n\n" +
                        "Lưu ý: Chỉ xóa mềm (đổi trạng thái), không xóa khỏi database.",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        XoaSan(maSan);
                    }
                }
            }
        }

        #endregion

        #region === THÊM/SỬA/XÓA ===

        private void BtnThemSan_Click(object sender, EventArgs e)
        {
            frmThemSuaSan frm = new frmThemSuaSan();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachSan();
                LoadThongKe();
                MessageBox.Show("Thêm sân thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void XoaSan(int maSan)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE San SET TrangThai = 0 WHERE MaSan = @MaSan";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", maSan);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            maSanDangChon = 0;
                            LoadDanhSachSan();
                            LoadThongKe();
                            MessageBox.Show("Đã xóa sân thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sân cần xóa!", "Cảnh báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa sân: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            cboChiNhanh.SelectedIndex = 0;
            cboLoaiSan.SelectedIndex = 0;
            cboTinhTrang.SelectedIndex = 0;
            LoadDanhSachSan();
            LoadThongKe();
        }


        #endregion

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvDanhSachSan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
