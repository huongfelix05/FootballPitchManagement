using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmDanhGia : Form
    {
        // ==========================================================
        // KHAI BÁO KẾT NỐI (Sửa tên máy tính DANGKHOA nếu cần)
        // ==========================================================
        private string strKetNoi = @"Data Source=DANGKHOA;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // Biến lưu mã khách hàng đang đăng nhập
        private int _maKH;

        // Constructor nhận ID khách hàng từ Form Main truyền sang
        public frmDanhGia(int maKH)
        {
            InitializeComponent();
            _maKH = maKH;

            // Đăng ký sự kiện
            this.Load += FrmDanhGia_Load;
            if (btnGui != null) btnGui.Click += BtnGui_Click;
        }

        // Sự kiện khi Form vừa mở lên -> Load danh sách sân
        private void FrmDanhGia_Load(object sender, EventArgs e)
        {
            LoadDanhSachSan();
        }

        // Hàm lấy dữ liệu sân từ SQL đổ vào ComboBox
        private void LoadDanhSachSan()
        {
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaSan, TenSan FROM San";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Tạo dòng "Chọn sân..." giả ở đầu danh sách
                    DataRow dr = dt.NewRow();
                    dr["MaSan"] = -1; // Giá trị ảo
                    dr["TenSan"] = "-- Chọn sân bạn muốn đánh giá --";
                    dt.Rows.InsertAt(dr, 0);

                    if (cboSanBong != null)
                    {
                        cboSanBong.DataSource = dt;
                        cboSanBong.DisplayMember = "TenSan"; // Cái hiện lên cho người xem
                        cboSanBong.ValueMember = "MaSan";    // Cái giá trị lưu ngầm bên dưới
                        cboSanBong.SelectedIndex = 0;        // Mặc định chọn dòng đầu tiên
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách sân: " + ex.Message);
                }
            }
        }

        // Sự kiện khi bấm nút Gửi
        private void BtnGui_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn sân chưa (Mã phải khác -1)
            if (cboSanBong.SelectedIndex == 0 || Convert.ToInt32(cboSanBong.SelectedValue) == -1)
            {
                MessageBox.Show("Vui lòng chọn một sân bóng để đánh giá!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra xem đã viết nội dung chưa
            if (string.IsNullOrWhiteSpace(txtNhanXet.Text))
            {
                MessageBox.Show("Bạn hãy viết vài lời nhận xét nhé!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Tiến hành lưu vào SQL
            LuuDanhGia();
        }

        private void LuuDanhGia()
        {
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO DanhGia (MaKH, MaSan, Diem, NoiDung, NgayDanhGia, TrangThai) 
                                     VALUES (@MaKH, @MaSan, @Diem, @NoiDung, GETDATE(), 1)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Truyền các tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@MaKH", _maKH);
                        cmd.Parameters.AddWithValue("@MaSan", Convert.ToInt32(cboSanBong.SelectedValue));
                        cmd.Parameters.AddWithValue("@Diem", (int)rtSao.Value); // Lấy số sao
                        cmd.Parameters.AddWithValue("@NoiDung", txtNhanXet.Text.Trim());

                        int ketQua = cmd.ExecuteNonQuery();

                        if (ketQua > 0)
                        {
                            MessageBox.Show("Cảm ơn bạn đã gửi đánh giá!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Reset lại form cho sạch
                            txtNhanXet.Clear();
                            cboSanBong.SelectedIndex = 0;
                            rtSao.Value = 5;
                        }
                        else
                        {
                            MessageBox.Show("Gửi thất bại. Vui lòng thử lại.", "Lỗi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }
    }
}