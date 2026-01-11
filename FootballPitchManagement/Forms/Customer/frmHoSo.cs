using System;
using System.Data;
using System.Data.SqlClient; // Thư viện kết nối SQL
using System.Windows.Forms;

// Namespace đúng như trong ảnh của bạn
namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmHoSo : Form
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & KẾT NỐI
        // ==================================================================================

        // LƯU Ý: Sửa lại 'Server Name' (chỗ DESKTOP-...) cho đúng máy bạn
        string strKetNoi = @"Data Source=DESKTOP-XXX\SQLEXPRESS;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // Biến lưu mã khách hàng (ID)
        private int _maKH;

        // 2. SỬA LẠI CONSTRUCTOR (Quan trọng)
        // Trong ảnh của bạn là frmHoSo(), mình sửa thành frmHoSo(int maKH) để nhận ID
        public frmHoSo(int maKH)
        {
            InitializeComponent();
            _maKH = maKH; // Lưu lại ID người dùng đăng nhập để dùng sau này
        }

        // ==================================================================================
        // 2. CÁC SỰ KIỆN (EVENTS)
        // ==================================================================================

        // Sự kiện Form Load (Kích hoạt khi mở form)
        private void frmHoSo_Load(object sender, EventArgs e)
        {
            LoadDataTuSQL(); // Gọi hàm lấy dữ liệu
        }

        // Sự kiện nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false) return; // Nếu dữ liệu sai thì dừng

            if (MessageBox.Show("Bạn có chắc muốn cập nhật thông tin?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LuuDataVaoSQL();
            }
        }

        // Sự kiện nút Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ==================================================================================
        // 3. LOGIC XỬ LÝ SQL (BACKEND)
        // ==================================================================================

        private void LoadDataTuSQL()
        {
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    // Lấy thông tin dựa trên _maKH
                    string query = "SELECT * FROM KhachHang WHERE MaKH = @MaKH";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaKH", _maKH);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Đổ dữ liệu vào TextBox (Đảm bảo bên Design bạn đặt tên đúng như này)
                        txtMaKH.Text = reader["MaKH"].ToString();
                        txtHoTen.Text = reader["HoTen"].ToString();
                        txtSDT.Text = reader["DienThoai"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtCCCD.Text = reader["CMND_CCCD"].ToString();
                        txtDiaChi.Text = reader["DiaChi"].ToString();

                        // Xử lý Ngày sinh
                        if (reader["NgaySinh"] != DBNull.Value)
                            dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]);

                        // Xử lý Giới tính
                        string gioiTinh = reader["GioiTinh"].ToString();
                        if (gioiTinh == "Nam") rdoNam.Checked = true;
                        else if (gioiTinh == "Nữ") rdoNu.Checked = true;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void LuuDataVaoSQL()
        {
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = @"UPDATE KhachHang 
                                     SET HoTen = @HoTen, 
                                         DienThoai = @DienThoai, 
                                         Email = @Email, 
                                         GioiTinh = @GioiTinh, 
                                         NgaySinh = @NgaySinh, 
                                         DiaChi = @DiaChi, 
                                         CMND_CCCD = @CCCD
                                     WHERE MaKH = @MaKH";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Truyền tham số
                    cmd.Parameters.AddWithValue("@MaKH", _maKH);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@DienThoai", txtSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);

                    string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                        MessageBox.Show("Cập nhật thành công!");
                    else
                        MessageBox.Show("Không có thay đổi nào.");
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627)
                        MessageBox.Show("Số điện thoại hoặc Email đã tồn tại!");
                    else
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                }
            }
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Chưa nhập họ tên"); return false; }
            if (string.IsNullOrWhiteSpace(txtSDT.Text)) { MessageBox.Show("Chưa nhập SĐT"); return false; }
            return true;
        }
    }
}