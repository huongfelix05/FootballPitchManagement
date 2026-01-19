using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FootballPitchManagement.Forms.Auth
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        private DataManager dataManager;
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
            // Khởi tạo bộ quản lý dữ liệu
            dataManager = new DataManager();
        }



        // =================================================================
        // PHẦN 1: XỬ LÝ GIAO DIỆN (FORM)
        // =================================================================

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            setupDataGridView(); // Cấu hình giao diện bảng
            TaiDuLieu();         // Tải dữ liệu
                                 // --- THÊM DÒNG NÀY ---
                                 // Ẩn khung chi tiết ngay khi mở form
            grpChiTiet.Visible = false;

        }

        // Cấu hình hiển thị bảng cho đẹp
        private void setupDataGridView()
        {
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.RowTemplate.Height = 50; // Dòng cao thoáng

            // Màu sắc dòng chẵn lẻ cho dễ nhìn
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvKhachHang.BackgroundColor = Color.White;

            // Cho phép cột liên hệ xuống dòng
            if (dgvKhachHang.Columns["ColLienHe"] != null)
                dgvKhachHang.Columns["ColLienHe"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.RowTemplate.Height = 50; // Giữ nguyên dòng cao 50 để chứa vừa chữ 12
            dgvKhachHang.RowHeadersVisible = false; // Ẩn cột mũi tên đầu tiên
            dgvKhachHang.AllowUserToAddRows = false; // Ẩn dòng trống cuối cùng

            // --- PHẦN THÊM MỚI: CHỈNH FONT CHỮ 12 ---

            // a. Chỉnh font cho nội dung các dòng (Cỡ 12)
            dgvKhachHang.DefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Regular);

            // b. Chỉnh font cho tiêu đề cột (Cỡ 12, In đậm)
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // ------------------------------------------

            // Màu sắc (Giữ nguyên cho đẹp)
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvKhachHang.BackgroundColor = Color.White;

            // Xuống dòng cột liên hệ
            if (dgvKhachHang.Columns["ColLienHe"] != null)
                dgvKhachHang.Columns["ColLienHe"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            // Cột ID: Nhỏ gọn
            if (dgvKhachHang.Columns["ColID"] != null)
                dgvKhachHang.Columns["ColID"].Width = 50;

            // Cột KHÁCH HÀNG: Đặt cố định khoảng 200 (đủ hiển thị tên)
            if (dgvKhachHang.Columns["ColTen"] != null)
            {
                dgvKhachHang.Columns["ColTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Tắt tự động giãn
                dgvKhachHang.Columns["ColTen"].Width = 200; // Đặt chiều rộng cố định
            }

            // Cột LIÊN HỆ: Cho tự động giãn hết phần còn lại (Vì Email thường dài)
            if (dgvKhachHang.Columns["ColLienHe"] != null)
            {
                dgvKhachHang.Columns["ColLienHe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvKhachHang.Columns["ColLienHe"].DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép xuống dòng
            }

            // Cột ĐƠN ĐẶT: Nhỏ gọn
            if (dgvKhachHang.Columns["ColDonDat"] != null)
            {
                dgvKhachHang.Columns["ColDonDat"].Width = 100;
                dgvKhachHang.Columns["ColDonDat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa cho đẹp
            }

            // Cột CHI TIẾT: Vừa đủ nút bấm
            if (dgvKhachHang.Columns["ColChiTiet"] != null)
                dgvKhachHang.Columns["ColChiTiet"].Width = 80;

            // ------------------------------------------

            // Màu sắc
            dgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvKhachHang.BackgroundColor = Color.White;
        }

        // Hàm gọi dữ liệu từ DataManager lên giao diện
        private void TaiDuLieu()
        {
            try
            {
                // 1. Lấy dữ liệu thống kê (Giữ nguyên)
                var thongKe = dataManager.LayThongKe();
                lblTongKH.Text = thongKe.Tong.ToString();
                lblHoatDong.Text = thongKe.HoatDong.ToString();
                lblKhachMoi.Text = thongKe.Moi.ToString();

                // 2. XỬ LÝ TỪ KHÓA TÌM KIẾM (SỬA ĐOẠN NÀY)
                string tuKhoa = txtTimKiem.Text.Trim();

                // Nếu ô tìm kiếm đang hiện chữ gợi ý -> Coi như rỗng (Lấy tất cả)
                if (tuKhoa == "Tên, SĐT, Email...")
                {
                    tuKhoa = "";
                }

                // 3. Gọi SQL lấy danh sách
                DataTable dt = dataManager.LayDanhSachKhachHang(tuKhoa);

                // Xử lý hiển thị cột Liên Hệ (Giữ nguyên)
                dt.Columns.Add("LienHeDisplay", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    row["LienHeDisplay"] = $"📞 {row["DienThoai"]}\r\n✉ {row["Email"]}";
                }

                dgvKhachHang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // Nút Tìm Kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            // --- THÊM DÒNG KIỂM TRA NÀY ---
            if (tuKhoa == "Tên, SĐT, Email...")
                tuKhoa = ""; // Coi như người dùng chưa nhập gì
                             // ------------------------------
            TaiDuLieu();
        }

        // Sự kiện Click vào bảng (Nút Xem)
        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra đúng cột nút "Xem"
            if (e.RowIndex >= 0 && dgvKhachHang.Columns[e.ColumnIndex].Name == "ColChiTiet")
            {
                string id = dgvKhachHang.Rows[e.RowIndex].Cells["ColID"].Value.ToString();
                DataRow r = dataManager.LayChiTietKhachHang(id);

                if (r != null)
                {

                    // --- THÊM DÒNG NÀY ---
                    // Khi lấy được dữ liệu thì mới hiện khung lên
                    grpChiTiet.Visible = true;


                    // --- CẬP NHẬT HIỂN THỊ MỚI ---

                    // 1. Thông tin cơ bản
                    lblDetailTen.Text = r["HoTen"].ToString();
                    lblDetailSDT.Text = "Điện thoại: " + r["DienThoai"].ToString();
                    lblDetailEmail.Text = "Email: " + r["Email"].ToString();

                    // 2. Xử lý ngày Đăng Ký
                    if (r["NgayDangKy"] != DBNull.Value)
                        lblDetailNgay.Text = "Ngày ĐK: " + Convert.ToDateTime(r["NgayDangKy"]).ToString("dd/MM/yyyy");
                    else
                        lblDetailNgay.Text = "Ngày ĐK: ---";

                    // 3. Xử lý ngày LẦN CUỐI (Mới)
                    if (r["LanCuoi"] != DBNull.Value)
                    {
                        // Nếu có đặt sân thì hiện ngày
                        lblDetailLanCuoi.Text = Convert.ToDateTime(r["LanCuoi"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        // Khách chưa đặt bao giờ
                        lblDetailLanCuoi.Text = "Chưa có hoạt động";
                    }

                    // 4. Xử lý TỔNG CHI TIÊU (Thay vì tổng đơn)
                    decimal tongTien = Convert.ToDecimal(r["TongTien"]);
                    // Định dạng tiền Việt Nam (Ví dụ: 200,000 đ)
                    lblDetailTien.Text = string.Format("{0:N0} đ", tongTien);

                    // Mẹo: Đổi màu chữ tiền cho nổi bật
                    lblDetailTien.ForeColor = Color.Green;
                    lblDetailTien.Font = new Font(lblDetailTien.Font, FontStyle.Bold);
                }
            }
        }


        // =================================================================
        // PHẦN 2: LỚP QUẢN LÝ SQL (DataManager) - BẠN CHỈ CẦN SỬA SQL Ở ĐÂY
        // =================================================================
        public class DataManager
        {
            // 1. CHUỖI KẾT NỐI (Sửa tên Server của bạn ở đây)
            private string connectionString = @"Data Source=.;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

            // Class chứa dữ liệu thống kê trả về
            public class ThongKeData
            {
                public int Tong { get; set; }
                public int HoatDong { get; set; }
                public int Moi { get; set; }
            }

            // Hàm 1: Lấy dữ liệu thống kê (3 ô trên cùng)
            public ThongKeData LayThongKe()
            {
                ThongKeData data = new ThongKeData();

                // --- SQL THỐNG KÊ ---
                string sql = @"
                SELECT 
                    (SELECT COUNT(*) FROM KhachHang WHERE TrangThai = 1) AS Tong,
                    (SELECT COUNT(DISTINCT MaKH) FROM LichDatSan 
                     WHERE MONTH(NgayDat) = MONTH(GETDATE()) 
                     AND YEAR(NgayDat) = YEAR(GETDATE())) AS HoatDong,
                    (SELECT COUNT(*) FROM KhachHang 
                     WHERE MONTH(NgayDangKy) = MONTH(GETDATE()) 
                     AND YEAR(NgayDangKy) = YEAR(GETDATE())) AS Moi";
                // --------------------

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.Tong = reader["Tong"] != DBNull.Value ? Convert.ToInt32(reader["Tong"]) : 0;
                            data.HoatDong = reader["HoatDong"] != DBNull.Value ? Convert.ToInt32(reader["HoatDong"]) : 0;
                            data.Moi = reader["Moi"] != DBNull.Value ? Convert.ToInt32(reader["Moi"]) : 0;
                        }
                    }
                }
                return data;
            }

            // Hàm 2: Lấy danh sách khách hàng tìm kiếm
            public DataTable LayDanhSachKhachHang(string tuKhoa)
            {
                DataTable dt = new DataTable();

                // --- SQL DANH SÁCH ---
                // Đã bỏ cột Hạng, Điểm. Chỉ lấy thông tin cơ bản + số đơn đặt
                string sql = @"
                SELECT 
                    kh.MaKH, 
                    kh.HoTen, 
                    kh.DienThoai, 
                    kh.Email,
                    (SELECT COUNT(*) FROM LichDatSan lds WHERE lds.MaKH = kh.MaKH) AS SoDonDat
                FROM KhachHang kh
                WHERE kh.TrangThai = 1
                AND (
                    kh.HoTen LIKE @key OR 
                    kh.DienThoai LIKE @key OR 
                    kh.Email LIKE @key
                )
                ORDER BY kh.MaKH DESC";
                // ---------------------

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    // Truyền tham số an toàn (tránh lỗi SQL Injection)
                    cmd.Parameters.AddWithValue("@key", "%" + tuKhoa + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
            // Dán hàm này vào trong class DataManager
            public DataRow LayChiTietKhachHang(string maKH)
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // --- CẬP NHẬT SQL MỚI ---
                    // 1. Lấy MAX(NgayDat) để biết lần cuối đặt sân
                    // 2. Lấy SUM(TongTienSan) để biết tổng chi tiêu
                    string sql = @"
            SELECT 
                k.*, 
                (SELECT COUNT(*) FROM LichDatSan l WHERE l.MaKH = k.MaKH) as TongDon,
                (SELECT MAX(NgayDat) FROM LichDatSan l WHERE l.MaKH = k.MaKH) as LanCuoi,
                (SELECT ISNULL(SUM(TongTienSan), 0) FROM LichDatSan l WHERE l.MaKH = k.MaKH) as TongTien
            FROM KhachHang k
            WHERE k.MaKH = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", maKH);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                if (dt.Rows.Count > 0) return dt.Rows[0];
                return null;
            }

        }



        // Sự kiện 1: Khi người dùng bấm chuột vào ô tìm kiếm (Enter)
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            // Nếu đang hiện chữ gợi ý thì xóa đi, chuyển màu về đen để nhập
            if (txtTimKiem.Text == "Tên, SĐT, Email...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

      
     
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            grpChiTiet.Visible = false; // Bấm đóng thì ẩn đi
        }


  // Sự kiện 2: Khi người dùng bấm ra chỗ khác (Leave)
        private void txtTimKiem_Leave_1(object sender, EventArgs e)
        {

            // Nếu người dùng không nhập gì cả (trống trơn) thì hiện lại gợi ý
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Tên, SĐT, Email...";
                txtTimKiem.ForeColor = Color.Gray; // Chuyển màu xám lại
            }
        }

   
        private void txtTimKiem_Enter_1(object sender, EventArgs e)
        {

            // Nếu đang hiện chữ gợi ý thì xóa đi, chuyển màu về đen để nhập
            if (txtTimKiem.Text == "Tên, SĐT, Email...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }
    }
}
