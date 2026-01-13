using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq; // Cần thêm thư viện này để dùng LINQ
using System.Windows.Forms;

namespace QuanLySanBong
{
    public partial class frmQuanLyDatSan : Form
    {
        // 1. Chuỗi kết nối
        string strConn = @"Data Source=MSI;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // 2. BIẾN TOÀN CỤC: Lưu trữ dữ liệu gốc để không phải gọi SQL nhiều lần
        DataTable dtDuLieuGoc = new DataTable();

        public frmQuanLyDatSan()
        {
            InitializeComponent();
        }

        private void frmQuanLyDatSan_Load(object sender, EventArgs e)
        {
            CaiDatGiaoDien();
            LayDuLieuTuDatabase(); // Chỉ gọi DB 1 lần khi mở form (hoặc khi cần Refresh)
        }

        // --- PHẦN 1: CẤU HÌNH GIAO DIỆN ---
        void CaiDatGiaoDien()
        {
            // --- 1. Cấu hình ComboBox Trạng thái (SỬA LẠI ĐOẠN NÀY) ---
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Ma", typeof(string));
            dtTrangThai.Columns.Add("Ten", typeof(string));

            dtTrangThai.Rows.Add("ALL", "Tất cả");
            //dtTrangThai.Rows.Add("CHO_XAC_NHAN", "Chờ xác nhận");
            dtTrangThai.Rows.Add("DA_XAC_NHAN", "Đã xác nhận");
            dtTrangThai.Rows.Add("HOAN_THANH", "Hoàn thành"); // <--- MỚI THÊM: Để lọc được đơn đã xong
            dtTrangThai.Rows.Add("DA_HUY", "Đã hủy");

            cboTrangThai.DataSource = dtTrangThai;
            cboTrangThai.DisplayMember = "Ten";
            cboTrangThai.ValueMember = "Ma";
            cboTrangThai.SelectedIndex = 0;

            // --- 2. Cấu hình Ngày tháng ---
            DateTime now = DateTime.Now;
            // Mặc định lấy từ đầu tháng đến hiện tại cho dễ nhìn
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            // --- 3. Gán sự kiện (giữ nguyên) ---
            txtTimKiem.TextChanged += SuKienLocDuLieu;
            cboTrangThai.SelectedIndexChanged += SuKienLocDuLieu;
            dtpTuNgay.ValueChanged += SuKienLocDuLieu;
            dtpDenNgay.ValueChanged += SuKienLocDuLieu;
        }

        // --- PHẦN 2: LẤY DỮ LIỆU THÔ TỪ SQL (Thay thế Stored Procedure phức tạp) ---
        void LayDuLieuTuDatabase()
        {
            try
            {
                // Câu lệnh SQL thuần, JOIN các bảng để lấy đủ thông tin cần thiết
                string sql = @"
                    SELECT 
                        lds.MaDatSan,
                        kh.HoTen,
                        kh.DienThoai,
                        s.TenSan,
                        lds.NgayDat,
                        lds.GioBatDau,
                        lds.GioKetThuc,
                        lds.TongTienSan,
                        lds.TrangThai,
                        ISNULL(hd.TrangThaiThanhToan, 'CHUA_THANH_TOAN') AS TrangThaiThanhToan
                    FROM LichDatSan lds
                    JOIN KhachHang kh ON lds.MaKH = kh.MaKH
                    JOIN San s ON lds.MaSan = s.MaSan
                    LEFT JOIN HoaDon hd ON lds.MaDatSan = hd.MaDatSan
                    ORDER BY lds.NgayDat DESC";

                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    dtDuLieuGoc = new DataTable(); // Reset lại bảng gốc
                    da.Fill(dtDuLieuGoc);
                }

                // Sau khi lấy dữ liệu về, gọi hàm lọc để hiển thị lên lưới
                XuLyLocVaHienThi();
                TinhToanThongKe(); // Tính toán 4 ô số liệu phía trên
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
        }

        // --- PHẦN 3: XỬ LÝ LỌC TRONG CODE (CORE) ---
        // Đây là hàm thay thế cho việc tìm kiếm bằng SQL
        void XuLyLocVaHienThi()
        {
            if (dtDuLieuGoc.Rows.Count == 0) return;

            // 1. Lấy giá trị từ các control
            string tuKhoa = txtTimKiem.Text.ToLower().Trim();
            string trangThaiChon = cboTrangThai.SelectedValue.ToString();
            DateTime tuNgay = dtpTuNgay.Value.Date; // Chỉ lấy ngày, bỏ giờ
            DateTime denNgay = dtpDenNgay.Value.Date;

            // 2. Sử dụng LINQ để lọc dữ liệu từ DataTable gốc
            // Logic: Lọc những dòng thỏa mãn TẤT CẢ điều kiện
            var ketQuaLoc = dtDuLieuGoc.AsEnumerable().Where(row =>
            {
                // A. Lọc theo ngày (So sánh ngày đặt)
                DateTime ngayDat = row.Field<DateTime>("NgayDat").Date;
                bool checkNgay = ngayDat >= tuNgay && ngayDat <= denNgay;

                // B. Lọc theo trạng thái
                string trangThaiDB = row.Field<string>("TrangThai");
                bool checkTrangThai = (trangThaiChon == "ALL") || (trangThaiDB == trangThaiChon);

                // C. Lọc theo từ khóa (Tên khách hoặc SĐT)
                string tenKhach = row.Field<string>("HoTen").ToLower();
                string sdt = row.Field<string>("DienThoai");
                bool checkTuKhoa = string.IsNullOrEmpty(tuKhoa) ||
                                   tenKhach.Contains(tuKhoa) ||
                                   sdt.Contains(tuKhoa);

                return checkNgay && checkTrangThai && checkTuKhoa;
            });

            // 3. Hiển thị kết quả lọc lên DataGridView
            if (ketQuaLoc.Any())
            {
                dgvDanhSach.DataSource = ketQuaLoc.CopyToDataTable();
            }
            else
            {
                // Nếu không tìm thấy gì thì xóa trắng Grid nhưng giữ Header
                DataTable dtTrong = dtDuLieuGoc.Clone();
                dgvDanhSach.DataSource = dtTrong;
            }

            CauHinhCotGrid();
        }

        void TinhToanThongKe()
        {
            // Kiểm tra dữ liệu rỗng để tránh lỗi
            if (dtDuLieuGoc == null) return;

            // 1. Tổng đơn
            lblTongDon.Text = dtDuLieuGoc.Rows.Count.ToString();

            // 2. Đã hủy (Đếm theo cột TrangThai)
            lblDaHuy.Text = dtDuLieuGoc.Select("TrangThai = 'DA_HUY'").Length.ToString();

            // 3. Đã xác nhận/Hoàn thành (Đếm theo cột TrangThai)
            // Lưu ý: Tùy vào dữ liệu của bạn muốn đếm cái nào. 
            // Ở đây tôi cộng cả HOAN_THANH và DA_XAC_NHAN (nếu có)
            int countHoanThanh = dtDuLieuGoc.Select("TrangThai = 'HOAN_THANH'").Length;
            int countDaXacNhan = dtDuLieuGoc.Select("TrangThai = 'DA_XAC_NHAN'").Length;
            lblDaXacNhan.Text = (countHoanThanh + countDaXacNhan).ToString();

            // --- 4. ĐÃ THANH TOÁN (CODE MỚI THÊM) ---
            // Logic: Đếm trong cột 'TrangThaiThanhToan' những dòng có giá trị 'DA_THANH_TOAN'
            try
            {
                // Kiểm tra xem trong bảng dữ liệu có cột này không trước khi đếm
                if (dtDuLieuGoc.Columns.Contains("TrangThaiThanhToan"))
                {
                    int countDaTT = dtDuLieuGoc.Select("TrangThaiThanhToan = 'DA_THANH_TOAN'").Length;
                    lblDaThanhToan.Text = countDaTT.ToString();
                }
            }
            catch
            {
                // Nếu lỗi thì hiện 0
                lblDaThanhToan.Text = "0";
            }

            
        }

        // Sự kiện dùng chung cho các nút lọc (được gán ở Form_Load)
        private void SuKienLocDuLieu(object sender, EventArgs e)
        {
            XuLyLocVaHienThi();
        }

        // Nút Tìm kiếm (nếu người dùng muốn nhấn thủ công)
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            XuLyLocVaHienThi(); // Thực ra đã lọc realtime ở trên rồi
        }

        // --- PHẦN 4: FORMAT GIAO DIỆN (Màu sắc và Mã Đơn B001) ---
        void CauHinhCotGrid()
        {
            // A. Ẩn các cột dữ liệu gốc (để giao diện gọn)
            if (dgvDanhSach.Columns["MaDatSan"] != null) dgvDanhSach.Columns["MaDatSan"].Visible = false;
            if (dgvDanhSach.Columns["GioBatDau"] != null) dgvDanhSach.Columns["GioBatDau"].Visible = false;
            if (dgvDanhSach.Columns["GioKetThuc"] != null) dgvDanhSach.Columns["GioKetThuc"].Visible = false;

            // B. Thêm cột "Mã Đơn" (nếu chưa có)
            if (dgvDanhSach.Columns["MaHienThi"] == null)
            {
                DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn();
                colMa.Name = "MaHienThi"; // Tên này quan trọng để code nhận diện
                colMa.HeaderText = "Mã Đơn";
                colMa.Width = 80;
                dgvDanhSach.Columns.Insert(0, colMa); // Chèn vào vị trí đầu tiên
            }

            // C. Thêm cột "Khung Giờ" (nếu chưa có)
            if (dgvDanhSach.Columns["KhungGioHienThi"] == null)
            {
                DataGridViewTextBoxColumn colGio = new DataGridViewTextBoxColumn();
                colGio.Name = "KhungGioHienThi"; // Tên này quan trọng
                colGio.HeaderText = "Khung Giờ";
                dgvDanhSach.Columns.Insert(4, colGio); // Chèn vào giữa
            }

            // D. Đặt lại tiêu đề tiếng Việt cho các cột khác
            if (dgvDanhSach.Columns["HoTen"] != null) dgvDanhSach.Columns["HoTen"].HeaderText = "Khách Hàng";
            if (dgvDanhSach.Columns["DienThoai"] != null) dgvDanhSach.Columns["DienThoai"].HeaderText = "SĐT";
            if (dgvDanhSach.Columns["TenSan"] != null) dgvDanhSach.Columns["TenSan"].HeaderText = "Sân";
            if (dgvDanhSach.Columns["NgayDat"] != null) dgvDanhSach.Columns["NgayDat"].HeaderText = "Ngày Đặt";

            // Format tiền
            if (dgvDanhSach.Columns["TongTienSan"] != null)
            {
                dgvDanhSach.Columns["TongTienSan"].HeaderText = "Tổng Tiền";
                dgvDanhSach.Columns["TongTienSan"].DefaultCellStyle.Format = "N0";
            }
            // --- THÊM ĐOẠN NÀY ĐỂ TẠO NÚT XÓA ---
            // Kiểm tra nếu chưa có cột btnXoa thì mới thêm
            if (dgvDanhSach.Columns["btnXoa"] == null)
            {
                DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn();
                btnXoa.Name = "btnXoa";
                btnXoa.HeaderText = "Thao tác";
                btnXoa.Text = "Xóa";
                btnXoa.UseColumnTextForButtonValue = true; // Hiển thị chữ "Xóa" lên nút
                btnXoa.Width = 60;

                // Thêm vào cuối bảng
                dgvDanhSach.Columns.Add(btnXoa);
            }
        }

        // Sự kiện Format từng dòng (Tạo màu, tạo mã B001, tạo giờ 18:00-19:00)
        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra hàng hợp lệ
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Lấy tên cột đang xử lý
            string colName = dgvDanhSach.Columns[e.ColumnIndex].Name;

            // 1. HIỂN THỊ MÃ ĐƠN (B001)
            if (colName == "MaHienThi")
            {
                // Lấy giá trị cột MaDatSan ẩn
                var giaTriGoc = dgvDanhSach.Rows[e.RowIndex].Cells["MaDatSan"].Value;

                if (giaTriGoc != null)
                {
                    // Chỉ cần chuyển sang chuỗi là xong, không cần "B" hay "D3" nữa
                    e.Value = giaTriGoc.ToString();

                    // Căn giữa cho đẹp (nếu thích)
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    e.FormattingApplied = true;
                }
            }

            // 2. HIỂN THỊ KHUNG GIỜ (18:00 - 19:00)
            if (colName == "KhungGioHienThi")
            {
                // Lấy giá trị cột ẩn (dùng Object để tránh lỗi null)
                object valBD = dgvDanhSach.Rows[e.RowIndex].Cells["GioBatDau"].Value;
                object valKT = dgvDanhSach.Rows[e.RowIndex].Cells["GioKetThuc"].Value;

                if (valBD != null && valKT != null)
                {
                    // Xử lý an toàn: Chuyển sang chuỗi rồi cắt lấy 5 ký tự đầu (00:00)
                    // Cách này chạy đúng dù DB trả về TimeSpan hay DateTime
                    string strBD = valBD.ToString();
                    string strKT = valKT.ToString();

                    // Lấy 5 ký tự đầu (VD: 18:00:00 -> 18:00)
                    if (strBD.Length >= 5) strBD = strBD.Substring(0, 5);
                    if (strKT.Length >= 5) strKT = strKT.Substring(0, 5);

                    e.Value = $"{strBD} - {strKT}";
                    e.FormattingApplied = true;
                }
            }

            // 3. MÀU SẮC TRẠNG THÁI (Code cũ của bạn, giữ nguyên hoặc dùng cái này cho chuẩn)
            if (colName == "TrangThai")
            {
                string status = e.Value?.ToString();
                switch (status)
                {
                    case "HOAN_THANH":
                        e.Value = "Hoàn thành";
                        e.CellStyle.ForeColor = Color.Blue;
                        break;
                    case "DA_XAC_NHAN":
                        e.Value = "Đã xác nhận";
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                    case "CHO_XAC_NHAN":
                        e.Value = "Chờ xác nhận";
                        e.CellStyle.ForeColor = Color.OrangeRed;
                        break;
                    case "DA_HUY":
                        e.Value = "Đã hủy";
                        e.CellStyle.ForeColor = Color.Gray;
                        break;
                }
            }

            // 4. TRẠNG THÁI THANH TOÁN
            if (colName == "TrangThaiThanhToan")
            {
                string tt = e.Value?.ToString();
                if (tt != null && (tt.Contains("DA_THANH") || tt == "Đã TT"))
                {
                    e.Value = "Đã thanh toán";
                    e.CellStyle.ForeColor = Color.Green;
                }
                else
                {
                    e.Value = "Chưa thanh toán";
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }


        bool XoaDonDatSanTrongSQL(int maDatSan)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    // Lưu ý: Nếu bảng LichDatSan có liên kết khóa ngoại với HoaDon, 
                    // bạn cần xóa HoaDon trước hoặc xử lý Constraint trong SQL.
                    // Ở đây tôi viết lệnh xóa cơ bản:
                    string sql = "DELETE FROM LichDatSan WHERE MaDatSan = @MaDatSan";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaDatSan", maDatSan);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu có dòng bị xóa
                }
            }
            catch (SqlException ex)
            {
                // Lỗi thường gặp: FK Constraint (Có hóa đơn rồi không cho xóa lịch)
                if (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa đơn này vì đã có Hóa Đơn hoặc dữ liệu liên quan.\nPhải xóa hóa đơn trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message);
                }
                return false;
            }
        }




        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra xem người dùng có bấm vào cột "btnXoa" không
            if (e.RowIndex >= 0 && dgvDanhSach.Columns[e.ColumnIndex].Name == "btnXoa")
            {
                // 2. Hỏi xác nhận cho chắc ăn
                DialogResult hoi = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn đặt sân này không?\nHành động này không thể hoàn tác!", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (hoi == DialogResult.Yes)
                {
                    // 3. Lấy MaDatSan của dòng đó (Cột ẩn)
                    int maDatSan = Convert.ToInt32(dgvDanhSach.Rows[e.RowIndex].Cells["MaDatSan"].Value);

                    // 4. Gọi hàm xóa SQL (Bước 2)
                    if (XoaDonDatSanTrongSQL(maDatSan))
                    {
                        // 5. NẾU XÓA SQL THÀNH CÔNG -> XÓA TRÊN GIAO DIỆN
                        // Ta xóa dòng tương ứng trong DataTable gốc (dtDuLieuGoc)
                        // để không phải Load lại toàn bộ Database => App chạy nhanh hơn

                        DataRow[] dongCanXoa = dtDuLieuGoc.Select("MaDatSan = " + maDatSan);
                        if (dongCanXoa.Length > 0)
                        {
                            dtDuLieuGoc.Rows.Remove(dongCanXoa[0]); // Xóa khỏi bộ nhớ
                        }

                        // 6. Cập nhật lại Grid và Số liệu thống kê
                        XuLyLocVaHienThi(); // Load lại lưới từ DataTable đã cập nhật
                        TinhToanThongKe();  // Tính lại tổng đơn, số tiền...

                        MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}