using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmDanhGia : Form
    {
        // ⚠️ QUAN TRỌNG: Sửa lại "Data Source" cho đúng tên máy bạn
        // Nếu máy bạn là DANGKHOA thì để nguyên, nếu khác thì phải sửa lại
        private string strKetNoi = @"Data Source=DANGKHOA;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        private int _maKH; // Biến lưu mã khách hàng đang đăng nhập

        // Constructor nhận MaKH từ Form Đăng Nhập truyền sang
        public frmDanhGia(int maKH)
        {
            InitializeComponent();
            _maKH = maKH;

            // Đăng ký sự kiện
            this.Load += FrmDanhGia_Load;
            btnGui.Click += BtnGui_Click;
        }

        private void FrmDanhGia_Load(object sender, EventArgs e)
        {
            LoadDanhSachSan();
        }

        // Hàm tải danh sách sân từ bảng [San] lên ComboBox
        private void LoadDanhSachSan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strKetNoi))
                {
                    conn.Open();
                    // Lấy MaSan và TenSan để khách chọn
                    string sql = "SELECT MaSan, TenSan FROM San";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm dòng mặc định "-- Chọn sân --"
                    DataRow dr = dt.NewRow();
                    dr["MaSan"] = -1;
                    dr["TenSan"] = "-- Chọn sân bạn muốn đánh giá --";
                    dt.Rows.InsertAt(dr, 0);

                    cboSanBong.DataSource = dt;
                    cboSanBong.DisplayMember = "TenSan"; // Cái hiển thị lên
                    cboSanBong.ValueMember = "MaSan";    // Cái giá trị ngầm bên dưới
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm gửi đánh giá xuống bảng [DanhGia]
        private void BtnGui_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn sân chưa
            if (cboSanBong.SelectedIndex == 0 || Convert.ToInt32(cboSanBong.SelectedValue) == -1)
            {
                MessageBox.Show("Vui lòng chọn sân bóng bạn muốn đánh giá!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra nội dung
            if (string.IsNullOrWhiteSpace(txtNhanXet.Text))
            {
                MessageBox.Show("Hãy nhập vài lời nhận xét nhé!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(strKetNoi))
                {
                    conn.Open();

                    // Câu lệnh SQL chuẩn với bảng vừa tạo
                    string sql = @"INSERT INTO DanhGia (MaKH, MaSan, Diem, NoiDung, NgayDanhGia, TrangThai) 
                                   VALUES (@MaKH, @MaSan, @Diem, @NoiDung, GETDATE(), 1)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Truyền tham số an toàn (Tránh lỗi SQL Injection)
                        cmd.Parameters.AddWithValue("@MaKH", _maKH);
                        cmd.Parameters.AddWithValue("@MaSan", cboSanBong.SelectedValue); // Lấy MaSan từ ComboBox
                        cmd.Parameters.AddWithValue("@Diem", (int)rtSao.Value);          // Lấy điểm sao
                        cmd.Parameters.AddWithValue("@NoiDung", txtNhanXet.Text);        // Lấy nội dung

                        int ketQua = cmd.ExecuteNonQuery();

                        if (ketQua > 0)
                        {
                            MessageBox.Show("Cảm ơn bạn! Đánh giá đã được gửi thành công.", "Tuyệt vời", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset lại form cho sạch đẹp
                            txtNhanXet.Clear();
                            cboSanBong.SelectedIndex = 0;
                            rtSao.Value = 5;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lưu đánh giá: " + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}