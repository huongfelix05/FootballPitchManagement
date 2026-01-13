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
using FootballPitchManagement.Common;

namespace FootballPitchManagement
{
    public partial class frmLichdatsan : Form
    {
        // Biến toàn cục
        private int _maSanDangChon = 0;
        private decimal _giaSanHienTai = 0;

        public frmLichdatsan()
        {
            InitializeComponent();
            this.Load += LichDatSan_Load;
        }

        private void LichDatSan_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. GỌI HÀM DỌN DẸP NGAY ĐẦU TIÊN
                TuDongXoaLichQuaKhu();
                // --------------------------------
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    this.Close();
                    return;
                }

                // 1. Load dữ liệu
                LoadChiNhanh();
                LoadLoaiSan();

                // 2. Gán sự kiện
                dtpNgayXem.ValueChanged += Filter_Changed;
                dtpGioBatDau.ValueChanged += dtpGio_ValueChanged;
                dtpGioKetThuc.ValueChanged += dtpGio_ValueChanged;

                btnTimKH.Click += btnTimKH_Click;
                btnDatSan.Click += btnDatSan_Click;
                btnHuy.Click += btnHuy_Click;

                // 3. Mặc định
                dtpNgayXem.Value = DateTime.Now;
                dtpGioBatDau.Value = DateTime.Now;
                dtpGioKetThuc.Value = DateTime.Now.AddHours(1);

                ResetFormDatSan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo hệ thống:\n{ex.Message}", "Lỗi Khởi Tạo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region === 1. BỘ LỌC & LOAD DỮ LIỆU ===

        private void Filter_Changed(object sender, EventArgs e)
        {
            try
            {
                if (cboChiNhanh.SelectedValue == null || cboLoaiSan.SelectedValue == null) return;

                int maCN;
                int maLoai;

                if (!int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maCN)) return;
                if (!int.TryParse(cboLoaiSan.SelectedValue.ToString(), out maLoai)) return;

                DateTime ngayXem = dtpNgayXem.Value.Date;

                // Load sân bóng
                LoadSanBong(maCN, maLoai, ngayXem);

                // Load lại danh sách đơn hàng CỦA KHÁCH ĐANG CHỌN (nếu có)
                if (int.TryParse(lblMaKH.Text, out int maKH) && maKH > 0)
                {
                    LoadDanhSachDonHangCuaKhach(maKH);
                }
                else
                {
                    if (flpDanhSachDonHang != null) flpDanhSachDonHang.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lọc dữ liệu:\n{ex.Message}", "Lỗi Bộ Lọc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiNhanh()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh WHERE TrangThai = 1 ORDER BY TenChiNhanh";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboChiNhanh.DataSource = dt;
                    cboChiNhanh.DisplayMember = "TenChiNhanh";
                    cboChiNhanh.ValueMember = "MaChiNhanh";
                    cboChiNhanh.SelectedIndex = -1;

                    cboChiNhanh.SelectedIndexChanged -= Filter_Changed;
                    cboChiNhanh.SelectedIndexChanged += Filter_Changed;
                }
                catch { }
            }
        }

        private void LoadLoaiSan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaLoaiSan, TenLoaiSan FROM LoaiSan ORDER BY TenLoaiSan";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboLoaiSan.DataSource = dt;
                    cboLoaiSan.DisplayMember = "TenLoaiSan";
                    cboLoaiSan.ValueMember = "MaLoaiSan";
                    cboLoaiSan.SelectedIndex = -1;

                    cboLoaiSan.SelectedIndexChanged -= Filter_Changed;
                    cboLoaiSan.SelectedIndexChanged += Filter_Changed;
                }
                catch { }
            }
        }

        private void LoadSanBong(int maChiNhanh, int maLoaiSan, DateTime ngayXem)
        {
            flpDanhSachSan.Controls.Clear();
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT s.MaSan, s.TenSan, s.GiaMacDinh, tt.TenTinhTrang AS TrangThaiCoDinh
                        FROM San s
                        INNER JOIN TinhTrangSan tt ON s.MaTinhTrang = tt.MaTinhTrang
                        WHERE s.MaChiNhanh = @MaCN AND s.MaLoaiSan = @MaLoai AND s.TrangThai = 1
                        ORDER BY s.TenSan";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaCN", maChiNhanh);
                    cmd.Parameters.AddWithValue("@MaLoai", maLoaiSan);

                    SqlDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                        ucSanBong item = new ucSanBong();
                        int id = Convert.ToInt32(reader["MaSan"]);
                        string ten = reader["TenSan"].ToString();
                        decimal gia = Convert.ToDecimal(reader["GiaMacDinh"]);
                        string ttCoDinh = reader["TrangThaiCoDinh"].ToString();

                        string trangThaiHienThi = "Trống";
                        if (ttCoDinh.Equals("Bảo trì", StringComparison.OrdinalIgnoreCase))
                        {
                            trangThaiHienThi = "Bảo trì";
                        }

                        item.ThietLapThongTin(id, ten, trangThaiHienThi, gia);
                        item.OnSelect += Item_OnSelect;
                        flpDanhSachSan.Controls.Add(item);
                    }
                    if (count == 0)
                    {
                        Label lbl = new Label() { Text = "Không có sân phù hợp với bộ lọc", AutoSize = true, ForeColor = Color.Red, Font = new Font("Segoe UI", 10, FontStyle.Italic) };
                        flpDanhSachSan.Controls.Add(lbl);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Không thể tải danh sách sân:\n{ex.Message}", "Lỗi Tải Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        #endregion

        #region === 2. XỬ LÝ CHỌN SÂN & TÍNH TIỀN ===

        private void Item_OnSelect(object sender, EventArgs e)
        {
            ucSanBong san = sender as ucSanBong;
            if (san == null) return;

            // 1. Nếu bảo trì -> Chặn
            if (san.TrangThai == "Bảo trì")
            {
                MessageBox.Show("Sân đang trong quá trình bảo trì.\nVui lòng chọn sân khác!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Cập nhật biến toàn cục để chuẩn bị đặt
            _maSanDangChon = san.MaSan;
            _giaSanHienTai = san.GiaMacDinh;

            // 3. Đưa thông tin sang Panel Phải
            lblTenSanChon.Text = san.TenSan;
            lblMaSan.Text = san.MaSan.ToString();
            lblGiaGoc.Text = san.GiaMacDinh.ToString("N0");
            lblTenSanChon.ForeColor = Color.Blue;

            // 4. Kiểm tra giờ hiện tại có trùng không (để báo đỏ ô tiền nếu cần)
            dtpGio_ValueChanged(null, null);

            // 5. QUAN TRỌNG: LOAD DANH SÁCH CÁC KHUNG GIỜ ĐÃ ĐẶT CỦA SÂN NÀY
            LoadLichDatCuaSan(san.MaSan);
        }

        private void LoadKhachHangCuaSanDaDat(int maSan, DateTime ngayDat)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT TOP 1 k.MaKH, k.HoTen, k.DienThoai
                                   FROM LichDatSan l JOIN KhachHang k ON l.MaKH = k.MaKH
                                   WHERE l.MaSan=@San AND l.NgayDat=@Ngay AND l.TrangThai IN ('DA_XAC_NHAN','CHO_XAC_NHAN')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@San", maSan);
                    cmd.Parameters.AddWithValue("@Ngay", ngayDat);

                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        lblMaKH.Text = r["MaKH"].ToString();
                        txtTenKhach.Text = r["HoTen"].ToString();
                        txtSDT.Text = r["DienThoai"].ToString();
                        txtTenKhach.ForeColor = Color.Red;

                        LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text));
                    }
                }
                catch { }
            }
        }

        private void TinhTien()
        {
            if (_giaSanHienTai == 0) { txtTongTien.Text = "0"; return; }
            double soGio = (dtpGioKetThuc.Value - dtpGioBatDau.Value).TotalHours;
            if (soGio <= 0) { txtTongTien.Text = "0"; return; }

            decimal tongTien = (decimal)soGio * _giaSanHienTai;
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void dtpGio_ValueChanged(object sender, EventArgs e)
        {
            // 1. Reset thông báo
            lblThongBao.Text = "";
            txtTongTien.Text = "0"; // Mặc định về 0 khi đang check

            TimeSpan gioBD = dtpGioBatDau.Value.TimeOfDay;
            TimeSpan gioKT = dtpGioKetThuc.Value.TimeOfDay;
            double soPhutDa = (gioKT - gioBD).TotalMinutes;

            // 2. CHECK LỖI 1: Giờ kết thúc nhỏ hơn giờ bắt đầu
            if (gioKT <= gioBD)
            {
                lblThongBao.Text = "❌ Giờ kết thúc phải sau giờ bắt đầu!";
                lblThongBao.ForeColor = Color.Red;
                btnDatSan.Enabled = false;
                return;
            }

            // 3. CHECK LỖI 2: Thời lượng dưới 60 phút (MỚI THÊM)
            if (soPhutDa < 60)
            {
                lblThongBao.Text = $"⚠️ Tối thiểu phải đặt 1 tiếng!\n(Hiện tại: {soPhutDa} phút)";
                lblThongBao.ForeColor = Color.OrangeRed; // Màu cam đỏ cảnh báo
                btnDatSan.Enabled = false;
                return;
            }

            // 4. CHECK LỖI 3: Trùng lịch (SQL)
            bool biTrung = KiemTraTrungLich(
                _maSanDangChon,
                dtpNgayXem.Value.Date,
                gioBD,
                gioKT
            );

            if (biTrung)
            {
                lblThongBao.Text = "⛔ KHUNG GIỜ NÀY ĐÃ KÍN!";
                lblThongBao.ForeColor = Color.Red;
                btnDatSan.Enabled = false;
            }
            else
            {
                // HỢP LỆ
                lblThongBao.Text = "✅ Có thể đặt";
                lblThongBao.ForeColor = Color.Red;
                btnDatSan.Enabled = true;

                TinhTien(); // Tính tiền chuẩn
            }
        }

        #endregion

        #region === 3. KHÁCH HÀNG & ĐẶT SÂN ===

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();

            // 1. Kiểm tra nhập liệu
            if (string.IsNullOrEmpty(sdt) || sdt.Length < 10)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ (ít nhất 10 số)!",
                    "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT MaKH, HoTen FROM KhachHang WHERE DienThoai = @SDT", conn);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    SqlDataReader r = cmd.ExecuteReader();

                    if (r.Read())
                    {
                        // === TRƯỜNG HỢP 1: TÌM THẤY KHÁCH ===
                        txtTenKhach.Text = r["HoTen"].ToString();
                        lblMaKH.Text = r["MaKH"].ToString();
                        txtTenKhach.ForeColor = Color.Green;

                        // Load lịch sử đơn hàng của khách này
                        LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text));

                        MessageBox.Show("✅ Đã tìm thấy khách hàng: " + txtTenKhach.Text,
                            "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // === TRƯỜNG HỢP 2: KHÔNG TÌM THẤY ===
                        r.Close();

                        // 1. Reset thông tin để ngăn chặn việc đặt sân
                        lblMaKH.Text = "0";
                        txtTenKhach.Text = "";

                        // Xóa danh sách đơn hàng cũ (nếu có)
                        if (flpDanhSachDonHang != null) flpDanhSachDonHang.Controls.Clear();

                        // 2. Thông báo lỗi KHÔNG CHO PHÉP đặt
                        MessageBox.Show(
                            "❌ Không tìm thấy thông tin khách hàng với số điện thoại này!\n\n" +
                            "Vui lòng kiểm tra lại SĐT hoặc yêu cầu khách hàng đăng ký thành viên trước khi đặt sân.",
                            "Không Tìm Thấy",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error); // Dùng icon Error màu đỏ

                        txtSDT.Focus();
                        txtSDT.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi hệ thống khi tìm khách:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ThemKhachHangMoi(string sdt, SqlConnection conn)
        {
            string ten = $"Khách vãng lai ({sdt})";
            SqlCommand cmd = new SqlCommand("INSERT INTO KhachHang(HoTen, DienThoai) VALUES (@Ten, @SDT); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@Ten", ten);
            cmd.Parameters.AddWithValue("@SDT", sdt);
            object newId = cmd.ExecuteScalar();

            txtTenKhach.Text = ten;
            lblMaKH.Text = newId.ToString();
            txtTenKhach.ForeColor = Color.Red;

            LoadDanhSachDonHangCuaKhach(Convert.ToInt32(newId));
            MessageBox.Show("Đã thêm khách hàng mới thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            // Validation
            if (_maSanDangChon == 0) { MessageBox.Show("Vui lòng chọn một sân bóng!", "Chưa Chọn Sân", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (lblMaKH.Text == "0" || string.IsNullOrEmpty(lblMaKH.Text)) { MessageBox.Show("Vui lòng nhập sđt và tìm tên khách hàng!", "Chưa Chọn Khách", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            DateTime ngayDat = dtpNgayXem.Value.Date;
            TimeSpan bd = dtpGioBatDau.Value.TimeOfDay;
            TimeSpan kt = dtpGioKetThuc.Value.TimeOfDay;

            if (kt <= bd) { MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Lỗi Thời Gian", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            // --- [MỚI] KIỂM TRA DƯỚI 1 TIẾNG ---
            if ((kt - bd).TotalMinutes < 60)
            {
                MessageBox.Show(
                    "Thời gian đặt sân tối thiểu phải là 1 tiếng (60 phút)!",
                    "Quy Định Đặt Sân",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return; // Dừng lại, không cho lưu
            }

            // Kiểm tra trùng lần cuối
            if (KiemTraTrungLich(_maSanDangChon, ngayDat, bd, kt))
            {
                MessageBox.Show("❌ Rất tiếc! Khung giờ này vừa có người đặt.\nVui lòng chọn giờ khác.", "Trùng Lịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Filter_Changed(null, null);
                return;
            }

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sqlIns = @"INSERT INTO LichDatSan(MaKH, MaSan, NgayDat, GioBatDau, GioKetThuc, SoGio, TongTienSan, TrangThai) 
                                      VALUES(@MaKH, @MaSan, @NgayDat, @BD, @KT, @SoGio, @Tien, 'DA_XAC_NHAN')";
                    SqlCommand cmd = new SqlCommand(sqlIns, conn);
                    cmd.Parameters.AddWithValue("@MaKH", int.Parse(lblMaKH.Text));
                    cmd.Parameters.AddWithValue("@MaSan", _maSanDangChon);
                    cmd.Parameters.AddWithValue("@NgayDat", ngayDat);
                    cmd.Parameters.AddWithValue("@BD", bd);
                    cmd.Parameters.AddWithValue("@KT", kt);
                    cmd.Parameters.AddWithValue("@SoGio", (kt - bd).TotalHours);

                    decimal tien = 0;
                    decimal.TryParse(txtTongTien.Text.Replace(".", "").Replace(",", ""), out tien);
                    cmd.Parameters.AddWithValue("@Tien", tien);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Đặt sân thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text)); // Load đơn mới
                    LoadLichDatCuaSan(_maSanDangChon);

                    // Reset sân, giữ khách
                    _maSanDangChon = 0;
                    _giaSanHienTai = 0;
                    lblTenSanChon.Text = "Chưa chọn";
                    txtTongTien.Text = "0";
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi khi lưu đặt sân:\n{ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        #endregion

        #region === 4. QUẢN LÝ ĐƠN & LIST VIEW ===

        private void LoadDanhSachDonHangCuaKhach(int maKH)
        {
            if (flpDanhSachDonHang == null) return;
            flpDanhSachDonHang.Controls.Clear();
            if (maKH == 0) return;

            if (grpThongTinDon != null) grpThongTinDon.Enabled = true;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    // Lấy thêm cột NgayDat
                    string sql = @"
                SELECT l.MaDatSan, l.NgayDat, k.HoTen, s.TenSan, l.GioBatDau, l.GioKetThuc, l.TongTienSan, l.TrangThai
                FROM LichDatSan l
                JOIN KhachHang k ON l.MaKH = k.MaKH
                JOIN San s ON l.MaSan = s.MaSan
                WHERE l.NgayDat = @NgayDat AND l.MaKH = @MaKH AND l.TrangThai != 'DA_HUY'
                ORDER BY l.MaDatSan DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NgayDat", dtpNgayXem.Value.Date);
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    SqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        ucDonDatItem item = new ucDonDatItem();

                        int maDon = Convert.ToInt32(r["MaDatSan"]);
                        string tenKhach = r["HoTen"].ToString();
                        string tenSan = r["TenSan"].ToString();
                        DateTime ngay = Convert.ToDateTime(r["NgayDat"]);

                        string gioBD = TimeSpan.Parse(r["GioBatDau"].ToString()).ToString(@"hh\:mm");
                        string gioKT = TimeSpan.Parse(r["GioKetThuc"].ToString()).ToString(@"hh\:mm");
                        string thoiGian = $"{gioBD} - {gioKT}";

                        decimal tien = Convert.ToDecimal(r["TongTienSan"]);
                        string tt = r["TrangThai"].ToString();

                        // GỌI HÀM HIỂN THỊ MỚI
                        item.HienThiThongTin(maDon, tenKhach, tenSan, ngay, thoiGian, tien, tt);

                        item.OnThanhToanClick += Item_OnThanhToanClick;
                        item.OnHuyClick += Item_OnHuyClick;

                        flpDanhSachDonHang.Controls.Add(item);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi tải danh sách đơn:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void Item_OnThanhToanClick(object sender, EventArgs e)
        {
            ucDonDatItem item = sender as ucDonDatItem;
            if (item == null) return;

            if (MessageBox.Show($"Xác nhận THANH TOÁN cho đơn #{item.MaDatSan}?\nThao tác này sẽ lưu hóa đơn và không thể hoàn tác.", "Xác Nhận Thanh Toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ThucHienThanhToan(item.MaDatSan);
                if (int.TryParse(lblMaKH.Text, out int maKH)) LoadDanhSachDonHangCuaKhach(maKH);
                Filter_Changed(null, null);
            }
        }

        private void Item_OnHuyClick(object sender, EventArgs e)
        {
            ucDonDatItem item = sender as ucDonDatItem;
            if (item == null) return;

            if (MessageBox.Show($"Bạn có chắc chắn muốn HỦY đơn #{item.MaDatSan} không?\nĐơn hàng sẽ bị xóa khỏi danh sách đặt.", "Xác Nhận Hủy Đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ThucHienHuyDon(item.MaDatSan);
                if (int.TryParse(lblMaKH.Text, out int maKH)) LoadDanhSachDonHangCuaKhach(maKH);
                Filter_Changed(null, null);
            }
        }

        private void ThucHienThanhToan(int maDatSan)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO HoaDon (MaDatSan, MaKH, MaChiNhanh, TongTienSan, ThanhTien, NgayLap, NguoiLap, TrangThaiThanhToan)
                        SELECT MaDatSan, MaKH, (SELECT MaChiNhanh FROM San WHERE San.MaSan = LichDatSan.MaSan), TongTienSan, TongTienSan, GETDATE(), 1, 'DA_THANH_TOAN'
                        FROM LichDatSan WHERE MaDatSan = @Ma;
                        
                        UPDATE LichDatSan SET TrangThai = 'HOAN_THANH' WHERE MaDatSan = @Ma;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", maDatSan);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("✅ Thanh toán thành công!", "Đã Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi khi thanh toán:\n{ex.Message}", "Lỗi Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void ThucHienHuyDon(int maDatSan)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE LichDatSan SET TrangThai = 'DA_HUY' WHERE MaDatSan = @Ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", maDatSan);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã hủy đơn thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi khi hủy đơn:\n{ex.Message}", "Lỗi Hủy Đơn", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // HÀM KIỂM TRA TRÙNG LỊCH (ĐÃ SỬA LỖI TÊN BIẾN)
        private bool KiemTraTrungLich(int maSan, DateTime ngayDat, TimeSpan gioBD, TimeSpan gioKT)
        {
            bool ketQua = false;
            if (maSan == 0) return false;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT COUNT(*) FROM LichDatSan 
                        WHERE MaSan = @MaSan 
                          AND NgayDat = @NgayDat 
                          AND TrangThai != 'DA_HUY'
                          AND (@GioBD < GioKetThuc AND @GioKT > GioBatDau)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSan", maSan);
                    cmd.Parameters.AddWithValue("@NgayDat", ngayDat);
                    cmd.Parameters.AddWithValue("@GioBD", gioBD);
                    cmd.Parameters.AddWithValue("@GioKT", gioKT);

                    // Sửa lỗi: Dùng đúng biến cmd thay vì cmdCheck
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) ketQua = true;
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi kiểm tra lịch:\n{ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            return ketQua;
        }

        #endregion

        #region === 5. RESET ===

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa hết thông tin đang nhập?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetFormDatSan();
            }
        }

        private void ResetFormDatSan()
        {
            txtSDT.Clear();
            txtTenKhach.Clear();
            lblMaKH.Text = "0";
            txtTongTien.Clear();
            lblTenSanChon.Text = "Chưa chọn";
            _maSanDangChon = 0;
            _giaSanHienTai = 0;
            if (flpDanhSachDonHang != null) flpDanhSachDonHang.Controls.Clear();
        }

        private void LoadLichDatCuaSan(int maSan)
        {
            // Kiểm tra và xóa danh sách cũ
            if (flpDanhSachDonHang == null) return;
            flpDanhSachDonHang.Controls.Clear();

            // Mở khóa danh sách
            if (grpThongTinDon != null) grpThongTinDon.Enabled = true;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    // SQL: Lấy tất cả đơn của Sân này + Ngày này (Sắp xếp theo giờ đá tăng dần)
                    string sql = @"
                SELECT l.MaDatSan, l.NgayDat, k.HoTen, s.TenSan, l.GioBatDau, l.GioKetThuc, l.TongTienSan, l.TrangThai
                FROM LichDatSan l
                JOIN KhachHang k ON l.MaKH = k.MaKH
                JOIN San s ON l.MaSan = s.MaSan
                WHERE l.MaSan = @MaSan 
                  AND l.NgayDat = @NgayDat 
                  AND l.TrangThai != 'DA_HUY'
                ORDER BY l.GioBatDau ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSan", maSan);
                    cmd.Parameters.AddWithValue("@NgayDat", dtpNgayXem.Value.Date);

                    SqlDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        ucDonDatItem item = new ucDonDatItem();

                        // Lấy dữ liệu
                        int maDon = Convert.ToInt32(r["MaDatSan"]);
                        string tenKhach = r["HoTen"].ToString();
                        string tenSan = r["TenSan"].ToString();
                        DateTime ngay = Convert.ToDateTime(r["NgayDat"]);

                        string gioBD = TimeSpan.Parse(r["GioBatDau"].ToString()).ToString(@"hh\:mm");
                        string gioKT = TimeSpan.Parse(r["GioKetThuc"].ToString()).ToString(@"hh\:mm");
                        string thoiGian = $"{gioBD} - {gioKT}";

                        decimal tien = Convert.ToDecimal(r["TongTienSan"]);
                        string tt = r["TrangThai"].ToString();

                        // Hiển thị lên thẻ (Dùng hàm mới nhất bạn đã sửa có Tên, Ngày, Giờ)
                        item.HienThiThongTin(maDon, tenKhach, tenSan, ngay, thoiGian, tien, tt);

                        // Gán sự kiện click cho thẻ
                        item.OnThanhToanClick += Item_OnThanhToanClick;
                        item.OnHuyClick += Item_OnHuyClick;

                        flpDanhSachDonHang.Controls.Add(item);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi tải lịch sử sân:\n{ex.Message}", "Lỗi Tải Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // Hàm tự động xóa (hoặc hủy) các đơn chưa hoàn thành của những ngày trước
        private void TuDongXoaLichQuaKhu()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // --- LỰA CHỌN CỦA BẠN (Chọn 1 trong 2 dòng sql bên dưới) ---

                    // CÁCH 1: XÓA VĨNH VIỄN (Xóa bay khỏi CSDL luôn - Cho nhẹ máy)
                    string sql = @"
                DELETE FROM LichDatSan 
                WHERE NgayDat < CAST(GETDATE() AS DATE) 
                  AND TrangThai IN ('DA_XAC_NHAN', 'CHO_XAC_NHAN')";

                    /* // CÁCH 2: HỦY MỀM (Chỉ đổi trạng thái thành DA_HUY để lưu lịch sử, không xóa mất)
                    string sql = @"
                        UPDATE LichDatSan SET TrangThai = 'DA_HUY' 
                        WHERE NgayDat < CAST(GETDATE() AS DATE) 
                          AND TrangThai IN ('DA_XAC_NHAN', 'CHO_XAC_NHAN')";
                    */

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int soLuongXoa = cmd.ExecuteNonQuery();

                    // (Tùy chọn) Nếu muốn thông báo thì mở dòng dưới ra, nhưng thường thì nên làm âm thầm
                    // if (soLuongXoa > 0) MessageBox.Show($"Hệ thống đã tự động dọn dẹp {soLuongXoa} đơn quá hạn.");
                }
                catch (Exception ex)
                {
                    // Chỉ ghi ra cửa sổ Output để debug, không hiện MessageBox làm phiền khách lúc mở app
                    System.Diagnostics.Debug.WriteLine("Lỗi dọn dẹp lịch cũ: " + ex.Message);
                }
            }
        }

        // Các hàm rỗng để tránh lỗi Designer nếu còn sót sự kiện cũ
        private void ResetGroupDon() { }
        private void btnThanhToan_Click(object sender, EventArgs e) { }
        private void btnXoaDon_Click(object sender, EventArgs e) { }
        private void groupBox3_Enter(object sender, EventArgs e) { }
        private void lblThongBao_Click(object sender, EventArgs e) { }
        private void btnHuy_Click_1(object sender, EventArgs e) { }

        #endregion
    }
}