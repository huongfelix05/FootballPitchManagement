using System;
using System.Data;
using System.Data.SqlClient; // Thư viện kết nối SQL
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmDanhGia : Form
    {
        // =======================================================================
        // 1. KHAI BÁO BIẾN KẾT NỐI
        // (Lưu ý: Bạn kiểm tra lại tên Server DANGKHOA xem đúng máy bạn chưa nhé)
        // =======================================================================
        private string strKetNoi = @"Data Source=DangKhoa;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // Biến lưu mã khách hàng được truyền từ bên ngoài vào
        private int _maKH;

        // =======================================================================
        // 2. CONSTRUCTOR (Hàm khởi tạo)
        // =======================================================================
        // Đã sửa thêm tham số 'int maKH' để fix lỗi bên Program.cs
        public frmDanhGia(int maKH)
        {
            InitializeComponent();

            // Lưu mã khách hàng lại để dùng khi lưu xuống DB
            _maKH = maKH;

            // Gán các sự kiện (Event) bằng code cho chắc chắn
            this.Load += FrmDanhGia_Load;

            // Kiểm tra nút btnGui có tồn tại không trước khi gán sự kiện click
            if (btnGui != null)
            {
                btnGui.Click += BtnGui_Click;
            }
        }

        // =======================================================================
        // 3. CÁC HÀM XỬ LÝ LOGIC
        // =======================================================================

        // Sự kiện khi Form vừa hiện lên -> Load danh sách sân
        private void FrmDanhGia_Load(object sender, EventArgs e)
        {
            LoadDanhSachSan();
        }

        // Hàm kết nối SQL lấy tên sân đổ vào ComboBox
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

                    // Tạo một dòng "giả" để làm placeholder: "-- Chọn sân --"
                    DataRow dr = dt.NewRow();
                    dr["MaSan"] = -1;
                    dr["TenSan"] = "-- Chọn sân bạn muốn đánh giá --";
                    dt.Rows.InsertAt(dr, 0);

                    if (cboSanBong != null)
                    {
                        cboSanBong.DataSource = dt;
                        cboSanBong.DisplayMember = "TenSan"; // Hiển thị tên
                        cboSanBong.ValueMember = "MaSan";    // Lưu mã bên dưới
                        cboSanBong.SelectedIndex = 0;        // Chọn dòng đầu tiên
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách sân: " + ex.Message);
                }
            }
        }

        // Sự kiện khi bấm nút "GỬI ĐÁNH GIÁ"
        private void BtnGui_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu
            if (cboSanBong.SelectedIndex == 0 || Convert.ToInt32(cboSanBong.SelectedValue) == -1)
            {
                MessageBox.Show("Vui lòng chọn sân bóng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNhanXet.Text))
            {
                MessageBox.Show("Hãy viết vài lời nhận xét bạn nhé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lưu vào CSDL
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO DanhGia (MaKH, MaSan, Diem, NoiDung, NgayDanhGia, TrangThai) 
                                     VALUES (@MaKH, @MaSan, @Diem, @NoiDung, GETDATE(), 1)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", _maKH);
                        cmd.Parameters.AddWithValue("@MaSan", Convert.ToInt32(cboSanBong.SelectedValue));
                        cmd.Parameters.AddWithValue("@Diem", (int)rtSao.Value); // Lấy số sao từ control
                        cmd.Parameters.AddWithValue("@NoiDung", txtNhanXet.Text.Trim());

                        int ketQua = cmd.ExecuteNonQuery();

                        if (ketQua > 0)
                        {
                            MessageBox.Show("Cảm ơn bạn đã gửi đánh giá!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset lại form sau khi gửi xong
                            txtNhanXet.Clear();
                            cboSanBong.SelectedIndex = 0;
                            rtSao.Value = 5;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu đánh giá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Hàm này code cũ của bạn tự sinh ra, cứ để trống cũng được
        private void cboSanBong_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}