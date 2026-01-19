using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmNhanVien : Form
    {

        bool dangThemMoi = false;
        string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public frmNhanVien()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        // ================= LOAD FORM =================
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dgvNhanVien.AutoGenerateColumns = true;
            LoadNhanVien();

            // --- GỌI CÁC HÀM TRANG TRÍ ---
            DecorateDataGridView(dgvNhanVien);
            StyleButtons(); // Làm đẹp nút (Màu sáng)
            this.BackColor = Color.White;
            // -----------------------------

            SetTrangThaiBanDau();
            this.Text = "Quản lý Nhân viên";
        }

        // ================= LOAD DATA GRID =================
        void LoadNhanVien()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM NhanVien", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvNhanVien.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            dangThemMoi = true;
            XoaTrangForm();
            MoKhoaForm(true);
            txtMaNV.Focus();
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa trên bảng danh sách.");
                return;
            }
            dangThemMoi = false;
            MoKhoaForm(true);
            txtMaNV.Enabled = false;
        }

        // ================= LƯU =================
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" || txtHo.Text == "" || txtTen.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Mã, Họ, Tên)");
                return;
            }

            string gt = rdoNam.Checked ? "Nam" : "Nữ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd;

                    if (dangThemMoi)
                    {
                        cmd = new SqlCommand(
                            @"INSERT INTO NhanVien (MaNV, Ho, Ten, NgaySinh, GioiTinh, DiaChi,Email,MaChiNhanh)
                              VALUES (@MaNV, @Ho, @Ten, @NgaySinh, @GioiTinh, @DiaChi,@Email,MaChiNhanh)", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand(
                            @"UPDATE NhanVien SET
                              Ho=@Ho, Ten=@Ten, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, DiaChi=@DiaChi
                              WHERE MaNV=@MaNV", conn);
                    }

                    cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                    cmd.Parameters.AddWithValue("@Ho", txtHo.Text);
                    cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", gt);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);

                    cmd.ExecuteNonQuery();
                }

                dangThemMoi = false;
                LoadNhanVien();
                SetTrangThaiBanDau();
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lưu: " + ex.Message);
            }
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM NhanVien WHERE MaNV=@MaNV", conn);
                        cmd.Parameters.AddWithValue("@MaNV", txtMaNV.Text);
                        cmd.ExecuteNonQuery();
                    }
                    LoadNhanVien();
                    SetTrangThaiBanDau();
                    MessageBox.Show("Đã xóa xong.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        // ================= CLICK GRID =================
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow r = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = r.Cells[0].Value?.ToString();
                txtHo.Text = r.Cells[1].Value?.ToString();
                txtTen.Text = r.Cells[2].Value?.ToString();

                if (r.Cells[3].Value != DBNull.Value)
                    dtpNgaySinh.Value = Convert.ToDateTime(r.Cells[3].Value);

                string gioitinh = r.Cells[4].Value?.ToString();
                if (gioitinh == "Nam") rdoNam.Checked = true;
                else rdoNu.Checked = true;

                txtDiaChi.Text = r.Cells[5].Value?.ToString();
            }
            catch { }
        }

        // ================= HÀM TIỆN ÍCH =================
        void XoaTrangForm()
        {
            txtMaNV.Clear(); txtHo.Clear(); txtTen.Clear(); txtDiaChi.Clear();
            rdoNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now;
        }

        void MoKhoaForm(bool mo)
        {
            txtMaNV.Enabled = mo;
            txtHo.Enabled = mo;
            txtTen.Enabled = mo;
            txtDiaChi.Enabled = mo;
            dtpNgaySinh.Enabled = mo;
            rdoNam.Enabled = mo;
            rdoNu.Enabled = mo;
            butLuu.Enabled = mo;
        }

        void SetTrangThaiBanDau()
        {
            MoKhoaForm(false);
        }

        // ==========================================================
        // 1. HÀM TRANG TRÍ BẢNG
        // ==========================================================
        private void DecorateDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.RowTemplate.Height = 35;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;
        }

        // ==========================================================
        // 2. HÀM TRANG TRÍ NÚT (ĐÃ ĐỔI SANG MÀU SÁNG HƠN)
        // ==========================================================
        private void StyleButtons()
        {
            // === ĐỔI MÀU Ở ĐÂY NÈ ===
            // Color.DodgerBlue: Xanh dương sáng (Rất đẹp)
            // Nếu vẫn thấy tối, bạn thử đổi thành: Color.DeepSkyBlue hoặc Color.MediumTurquoise
            Color commonColor = Color.DodgerBlue;

            void ApplyButtonStyle(Button btn, Color bgColor)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = bgColor;
                btn.ForeColor = Color.White; // Chữ trắng nổi bật trên nền xanh sáng
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
            }

            ApplyButtonStyle(btnThem, commonColor);

            // Xử lý nút Lưu (btnLuu hoặc butLuu)
            if (Controls.Find("butLuu", true).Length > 0)
                ApplyButtonStyle((Button)Controls.Find("butLuu", true)[0], commonColor);
            else if (Controls.Find("btnLuu", true).Length > 0)
                ApplyButtonStyle((Button)Controls.Find("btnLuu", true)[0], commonColor);

            ApplyButtonStyle(btnSua, commonColor);
            ApplyButtonStyle(btnXoa, commonColor);
        }




    }
}
