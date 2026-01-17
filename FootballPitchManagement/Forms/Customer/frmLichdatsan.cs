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
using Newtonsoft.Json.Linq; // Thư viện đọc JSON
using System.Net.Http;      // Thư viện gọi mạng

namespace FootballPitchManagement
{
    public partial class frmLichdatsan : Form
    {
        // Biến toàn cục
        private int _maSanDangChon = 0;
        private decimal _giaSanHienTai = 0;

        // --- CẤU HÌNH NGÂN HÀNG (VIETQR & CASSO) ---
        private const string BANK_ID = "MB";       // Mã ngân hàng
        private const string ACCOUNT_NO = "431903007"; // Số tài khoản CỦA BẠN
        private const string ACCOUNT_NAME = "NGUYEN QUOC DAT";

        // API KEY CASSO (Bạn đã điền)
        private const string CASSO_API_KEY = "AK_CS.9be3baf0f35811f0819c2d6675ce85db.yCFlnHVyEjYp55PoFwLY9bQ75ykJZ2gq2SHA2jE5FgzNXyjdyXAzA9IwcjRHVmIjYHJE5UAq";

        public frmLichdatsan()
        {
            InitializeComponent();
            this.Load += LichDatSan_Load;
        }

        private void LichDatSan_Load(object sender, EventArgs e)
        {
            try
            {
                TuDongXoaLichQuaKhu();
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    this.Close();
                    return;
                }

                LoadChiNhanh();
                LoadLoaiSan();

                dtpNgayXem.ValueChanged += Filter_Changed;
                dtpGioBatDau.ValueChanged += dtpGio_ValueChanged;
                dtpGioKetThuc.ValueChanged += dtpGio_ValueChanged;

                btnTimKH.Click += btnTimKH_Click;
                btnDatSan.Click += btnDatSan_Click;
                btnHuy.Click += btnHuy_Click;

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

                LoadSanBong(maCN, maLoai, ngayXem);

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

            if (san.TrangThai == "Bảo trì")
            {
                MessageBox.Show("Sân đang trong quá trình bảo trì.\nVui lòng chọn sân khác!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _maSanDangChon = san.MaSan;
            _giaSanHienTai = san.GiaMacDinh;

            lblTenSanChon.Text = san.TenSan;
            lblMaSan.Text = san.MaSan.ToString();
            lblGiaGoc.Text = san.GiaMacDinh.ToString("N0");
            lblTenSanChon.ForeColor = Color.Blue;

            dtpGio_ValueChanged(null, null);
            LoadLichDatCuaSan(san.MaSan);
        }

        private void TinhTien()
        {
            // Nếu chưa chọn sân thì thôi
            if (_giaSanHienTai == 0) { txtTongTien.Text = "0"; return; }

            // 1. LẤY GIỜ SẠCH (Cắt bỏ giây và mili-giây)
            // Ví dụ: 07:00:45 -> Về thành 07:00:00 phẳng lì
            DateTime timeBD = new DateTime(dtpGioBatDau.Value.Year, dtpGioBatDau.Value.Month, dtpGioBatDau.Value.Day,
                                           dtpGioBatDau.Value.Hour, dtpGioBatDau.Value.Minute, 0);

            DateTime timeKT = new DateTime(dtpGioKetThuc.Value.Year, dtpGioKetThuc.Value.Month, dtpGioKetThuc.Value.Day,
                                           dtpGioKetThuc.Value.Hour, dtpGioKetThuc.Value.Minute, 0);

            // 2. Tính số giờ chênh lệch
            double soGio = (timeKT - timeBD).TotalHours;

            // Nếu giờ kết thúc nhỏ hơn giờ bắt đầu -> không tính
            if (soGio <= 0) { txtTongTien.Text = "0"; return; }

            // 3. TÍNH TIỀN (Làm tròn số tiền để không bị lẻ 1-2 đồng)
            decimal tongTien = (decimal)soGio * _giaSanHienTai;

            // Làm tròn đến hàng nghìn (hoặc hàng đơn vị tùy bạn, ở đây tôi làm tròn đơn vị cho chuẩn)
            tongTien = Math.Round(tongTien, 0);

            // 4. Hiển thị
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void dtpGio_ValueChanged(object sender, EventArgs e)
        {
            lblThongBao.Text = "";
            txtTongTien.Text = "0";

            TimeSpan gioBD = dtpGioBatDau.Value.TimeOfDay;
            TimeSpan gioKT = dtpGioKetThuc.Value.TimeOfDay;
            double soPhutDa = (gioKT - gioBD).TotalMinutes;

            if (gioKT <= gioBD)
            {
                lblThongBao.Text = "❌ Giờ kết thúc phải sau giờ bắt đầu!";
                lblThongBao.ForeColor = Color.Red;
                btnDatSan.Enabled = false;
                return;
            }

            if (soPhutDa < 60)
            {
                lblThongBao.Text = $"⚠️ Tối thiểu phải đặt 1 tiếng!\n(Hiện tại: {soPhutDa} phút)";
                lblThongBao.ForeColor = Color.OrangeRed;
                btnDatSan.Enabled = false;
                return;
            }

            bool biTrung = KiemTraTrungLich(_maSanDangChon, dtpNgayXem.Value.Date, gioBD, gioKT);

            if (biTrung)
            {
                lblThongBao.Text = "⛔ KHUNG GIỜ NÀY ĐÃ KÍN!";
                lblThongBao.ForeColor = Color.Red;
                btnDatSan.Enabled = false;
            }
            else
            {
                lblThongBao.Text = "✅ Có thể đặt";
                lblThongBao.ForeColor = Color.Red;
                btnDatSan.Enabled = true;
                TinhTien();
            }
        }

        #endregion

        #region === 3. KHÁCH HÀNG & ĐẶT SÂN ===

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();
            if (string.IsNullOrEmpty(sdt) || sdt.Length < 10)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ (ít nhất 10 số)!", "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        txtTenKhach.Text = r["HoTen"].ToString();
                        lblMaKH.Text = r["MaKH"].ToString();
                        txtTenKhach.ForeColor = Color.Green;
                        LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text));
                        MessageBox.Show("✅ Đã tìm thấy khách hàng: " + txtTenKhach.Text, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        r.Close();
                        lblMaKH.Text = "0";
                        txtTenKhach.Text = "";
                        if (flpDanhSachDonHang != null) flpDanhSachDonHang.Controls.Clear();

                        MessageBox.Show("❌ Không tìm thấy khách hàng!\nVui lòng kiểm tra lại SĐT.", "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            if (_maSanDangChon == 0) { MessageBox.Show("Vui lòng chọn một sân bóng!", "Chưa Chọn Sân", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (lblMaKH.Text == "0" || string.IsNullOrEmpty(lblMaKH.Text)) { MessageBox.Show("Vui lòng nhập sđt và tìm khách hàng!", "Chưa Chọn Khách", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            DateTime ngayDat = dtpNgayXem.Value.Date;
            TimeSpan bd = dtpGioBatDau.Value.TimeOfDay;
            TimeSpan kt = dtpGioKetThuc.Value.TimeOfDay;

            if (kt <= bd) { MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if ((kt - bd).TotalMinutes < 60)
            {
                MessageBox.Show("Thời gian đặt sân tối thiểu phải là 1 tiếng (60 phút)!", "Quy Định", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KiemTraTrungLich(_maSanDangChon, ngayDat, bd, kt))
            {
                MessageBox.Show("❌ Rất tiếc! Khung giờ này vừa có người đặt.", "Trùng Lịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

                    LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text));
                    LoadLichDatCuaSan(_maSanDangChon);

                    _maSanDangChon = 0;
                    _giaSanHienTai = 0;
                    lblTenSanChon.Text = "Chưa chọn";
                    txtTongTien.Text = "0";
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi khi lưu đặt sân:\n{ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        #endregion

        #region === 4. QUẢN LÝ ĐƠN & LIST VIEW & THANH TOÁN ===

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

                        item.HienThiThongTin(maDon, tenKhach, tenSan, ngay, thoiGian, tien, tt);
                        item.OnThanhToanClick += Item_OnThanhToanClick;
                        item.OnHuyClick += Item_OnHuyClick;

                        flpDanhSachDonHang.Controls.Add(item);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Lỗi tải danh sách đơn:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // SỰ KIỆN NÚT THANH TOÁN (ĐÃ SỬA LỖI)
        private void Item_OnThanhToanClick(object sender, EventArgs e)
        {
            try
            {
                ucDonDatItem item = sender as ucDonDatItem;
                if (item == null) return;

                DialogResult result = MessageBox.Show(
                    $"Thanh toán đơn hàng #{item.MaDatSan} - {item.lblGia.Text}?\n\n" +
                    "Chọn [YES] để quét mã QR (Chuyển khoản).\n" +
                    "Chọn [NO] để thanh toán Tiền mặt.",
                    "Chọn phương thức thanh toán",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel) return;

                if (result == DialogResult.No) // TIỀN MẶT
                {
                    ThucHienThanhToan(item.MaDatSan, "TIEN_MAT");
                }
                else if (result == DialogResult.Yes) // QR CODE
                {
                    // Lọc lấy số tiền sạch (chỉ lấy số)
                    string strTien = item.lblGia.Text;
                    string strTienSach = new string(strTien.Where(c => char.IsDigit(c)).ToArray());

                    decimal soTien = 0;
                    if (!decimal.TryParse(strTienSach, out soTien) || soTien == 0)
                    {
                        MessageBox.Show("Lỗi: Không đọc được số tiền hợp lệ.", "Lỗi Xử Lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Gọi Form QR
                    bool daQuetXong = HienThiFormQR(item.MaDatSan, soTien, item.lblTenKhach.Text);

                    if (daQuetXong)
                    {
                        ThucHienThanhToan(item.MaDatSan, "CHUYEN_KHOAN");
                    }
                }

                if (int.TryParse(lblMaKH.Text, out int maKH)) LoadDanhSachDonHangCuaKhach(maKH);
                Filter_Changed(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ĐÃ CÓ LỖI XẢY RA:\n{ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // SỰ KIỆN NÚT HỦY ĐƠN (Đã thêm lại hàm bị thiếu)
        private void Item_OnHuyClick(object sender, EventArgs e)
        {
            ucDonDatItem item = sender as ucDonDatItem;
            if (item == null) return;

            if (MessageBox.Show($"Bạn có chắc chắn muốn HỦY đơn #{item.MaDatSan} không?", "Xác Nhận Hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ThucHienHuyDon(item.MaDatSan);
                if (int.TryParse(lblMaKH.Text, out int maKH)) LoadDanhSachDonHangCuaKhach(maKH);
                Filter_Changed(null, null);
            }
        }

        private void ThucHienThanhToan(int maDatSan, string phuongThuc)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string sqlGetInfo = @"SELECT l.MaKH, s.MaChiNhanh, l.TongTienSan, l.SoGio, l.MaSan, l.NgayDat
                                          FROM LichDatSan l JOIN San s ON l.MaSan = s.MaSan WHERE l.MaDatSan = @MaDatSan";
                    SqlCommand cmdGet = new SqlCommand(sqlGetInfo, conn, transaction);
                    cmdGet.Parameters.AddWithValue("@MaDatSan", maDatSan);

                    int maKH = 0, maChiNhanh = 0;
                    decimal tongTien = 0;
                    double soGio = 0;
                    DateTime ngayDat = DateTime.Now;

                    using (SqlDataReader r = cmdGet.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            maKH = Convert.ToInt32(r["MaKH"]);
                            maChiNhanh = Convert.ToInt32(r["MaChiNhanh"]);
                            tongTien = Convert.ToDecimal(r["TongTienSan"]);
                            soGio = Convert.ToDouble(r["SoGio"]);
                            ngayDat = Convert.ToDateTime(r["NgayDat"]);
                        }
                        else throw new Exception("Không tìm thấy đơn!");
                    }

                    string sqlHoaDon = @"INSERT INTO HoaDon (MaDatSan, MaKH, MaChiNhanh, NgayLap, TongTienSan, ThanhTien, TrangThaiThanhToan, PhuongThucTT, NguoiLap)
                                         VALUES (@MaDatSan, @MaKH, @MaChiNhanh, @NgayLap, @TongTien, @TongTien, N'DA_THANH_TOAN', @PT, 1);
                                         SELECT SCOPE_IDENTITY();";
                    SqlCommand cmdHoaDon = new SqlCommand(sqlHoaDon, conn, transaction);
                    cmdHoaDon.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    cmdHoaDon.Parameters.AddWithValue("@MaKH", maKH);
                    cmdHoaDon.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                    cmdHoaDon.Parameters.AddWithValue("@NgayLap", ngayDat);
                    cmdHoaDon.Parameters.AddWithValue("@TongTien", tongTien);
                    cmdHoaDon.Parameters.AddWithValue("@PT", phuongThuc);
                    int maHoaDon = Convert.ToInt32(cmdHoaDon.ExecuteScalar());

                    string sqlChiTiet = @"INSERT INTO ChiTietHoaDonSan (MaHoaDon, MaDatSan, DonGia, SoGio, ThanhTien)
                                          VALUES (@MaHoaDon, @MaDatSan, @DonGia, @SoGio, @ThanhTien)";
                    SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, transaction);
                    cmdChiTiet.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdChiTiet.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    decimal donGia = (soGio > 0) ? (tongTien / (decimal)soGio) : tongTien;
                    cmdChiTiet.Parameters.AddWithValue("@DonGia", donGia);
                    cmdChiTiet.Parameters.AddWithValue("@SoGio", soGio);
                    cmdChiTiet.Parameters.AddWithValue("@ThanhTien", tongTien);
                    cmdChiTiet.ExecuteNonQuery();

                    string sqlThanhToan = @"INSERT INTO ThanhToan (MaHoaDon, MaKH, SoTien, PhuongThuc, TrangThai, NgayThanhToan, NguoiThucHien)
                                            VALUES (@MaHoaDon, @MaKH, @SoTien, @PT, N'THANH_CONG', GETDATE(), 1)";
                    SqlCommand cmdTT = new SqlCommand(sqlThanhToan, conn, transaction);
                    cmdTT.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmdTT.Parameters.AddWithValue("@MaKH", maKH);
                    cmdTT.Parameters.AddWithValue("@SoTien", tongTien);
                    cmdTT.Parameters.AddWithValue("@PT", phuongThuc);
                    cmdTT.ExecuteNonQuery();

                    string sqlUpdate = "UPDATE LichDatSan SET TrangThai = 'HOAN_THANH' WHERE MaDatSan = @MaDatSan";
                    SqlCommand cmdUpd = new SqlCommand(sqlUpdate, conn, transaction);
                    cmdUpd.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    cmdUpd.ExecuteNonQuery();

                    transaction.Commit();
                    string msg = (phuongThuc == "CHUYEN_KHOAN") ? "✅ Đã xác nhận chuyển khoản thành công!" : "✅ Đã thu tiền mặt thành công!";
                    MessageBox.Show(msg, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Lỗi thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                catch (Exception ex) { MessageBox.Show($"Lỗi khi hủy đơn:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private bool KiemTraTrungLich(int maSan, DateTime ngayDat, TimeSpan gioBD, TimeSpan gioKT)
        {
            bool ketQua = false;
            if (maSan == 0) return false;
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT COUNT(*) FROM LichDatSan 
                                   WHERE MaSan = @MaSan AND NgayDat = @NgayDat AND TrangThai != 'DA_HUY'
                                   AND (@GioBD < GioKetThuc AND @GioKT > GioBatDau)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSan", maSan);
                    cmd.Parameters.AddWithValue("@NgayDat", ngayDat);
                    cmd.Parameters.AddWithValue("@GioBD", gioBD);
                    cmd.Parameters.AddWithValue("@GioKT", gioKT);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) ketQua = true;
                }
                catch { }
            }
            return ketQua;
        }

        // --- HÀM 1: HIỂN THỊ MÃ QR VÀ TỰ ĐỘNG CHECK TIỀN (REAL-TIME) ---
        private bool HienThiFormQR(int maDon, decimal soTien, string tenKhach)
        {
            string noiDungCK = $"DS{maDon}";
            string linkQR = $"https://img.vietqr.io/image/{BANK_ID}-{ACCOUNT_NO}-compact2.jpg?amount={soTien}&addInfo={noiDungCK}&accountName={ACCOUNT_NAME}";

            Form frmQR = new Form();
            frmQR.Size = new Size(500, 680);
            frmQR.StartPosition = FormStartPosition.CenterScreen;
            frmQR.Text = "THANH TOÁN TỰ ĐỘNG (AUTO BANKING)";
            frmQR.BackColor = Color.White;
            frmQR.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmQR.ControlBox = false;

            Label lblTitle = new Label() { Text = "QUÉT MÃ ĐỂ THANH TOÁN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Navy, Dock = DockStyle.Top, Height = 50, TextAlign = ContentAlignment.MiddleCenter };
            frmQR.Controls.Add(lblTitle);

            PictureBox picQR = new PictureBox() { ImageLocation = linkQR, SizeMode = PictureBoxSizeMode.Zoom, Dock = DockStyle.Top, Height = 350 };
            frmQR.Controls.Add(picQR);

            Label lblMoney = new Label() { Text = $"{soTien:N0} VNĐ", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.Red, Dock = DockStyle.Top, Height = 60, TextAlign = ContentAlignment.MiddleCenter };
            frmQR.Controls.Add(lblMoney);

            Label lblGuide = new Label() { Text = $"⚠️ Vui lòng giữ nguyên nội dung: {noiDungCK}", Font = new Font("Segoe UI", 10, FontStyle.Italic), ForeColor = Color.Red, Dock = DockStyle.Top, Height = 30, TextAlign = ContentAlignment.MiddleCenter };
            frmQR.Controls.Add(lblGuide);

            Label lblStatus = new Label() { Text = "Đang chờ tiền về tài khoản...", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Blue, Dock = DockStyle.Top, Height = 50, TextAlign = ContentAlignment.MiddleCenter };
            frmQR.Controls.Add(lblStatus);

            Button btnHuy = new Button() { Text = "Hủy bỏ / Quay lại", Dock = DockStyle.Bottom, Height = 45, BackColor = Color.LightGray, DialogResult = DialogResult.Cancel };
            frmQR.Controls.Add(btnHuy);

            // --- TIMER CHECK TIỀN TỰ ĐỘNG ---
            System.Windows.Forms.Timer timerCheck = new System.Windows.Forms.Timer();
            timerCheck.Interval = 3000; // 3 giây
            int countCheck = 0;

            timerCheck.Tick += async (s, e) =>
            {
                countCheck++;
                lblStatus.Text = $"Đang kiểm tra giao dịch... ({countCheck})";

                bool daNhanTien = await KiemTraTienVeQuaCasso(noiDungCK, soTien);

                if (daNhanTien)
                {
                    timerCheck.Stop();
                    lblStatus.Text = "✅ GIAO DỊCH THÀNH CÔNG!";
                    lblStatus.ForeColor = Color.Green;
                    MessageBox.Show("Hệ thống đã nhận được tiền!\nĐang xuất hóa đơn...", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmQR.DialogResult = DialogResult.OK;
                    frmQR.Close();
                }

                if (countCheck > 100)
                {
                    timerCheck.Stop();
                    lblStatus.Text = "Hết thời gian chờ.";
                    lblStatus.ForeColor = Color.Red;
                    MessageBox.Show("Hết thời gian chờ thanh toán!", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            timerCheck.Start();
            return frmQR.ShowDialog() == DialogResult.OK;
        }

        // --- HÀM 2: GỌI API CASSO ---
        // --- HÀM KIỂM TRA TIỀN (PHIÊN BẢN THÔNG MINH - HIỆN LÝ DO NẾU KHÔNG KHỚP) ---
        private async Task<bool> KiemTraTienVeQuaCasso(string noiDungCanTim, decimal soTienCanTim)
        {
            try
            {
                // 1. Cài đặt bảo mật (Bắt buộc)
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Apikey " + CASSO_API_KEY);
                    // Lấy 10 giao dịch gần nhất cho chắc ăn
                    string url = "https://oauth.casso.vn/v2/transactions?pageSize=10";

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(jsonResponse);

                        // 2. Bỏ qua nếu không có lỗi (error = 0 hoặc null đều OK)
                        if (data["error"] != null && data["error"].ToString() != "0")
                        {
                            return false;
                        }

                        var transactions = data["data"]["records"];

                        // 3. DUYỆT QUA CÁC GIAO DỊCH ĐỂ TÌM
                        foreach (var trans in transactions)
                        {
                            string description = trans["description"].ToString().ToUpper(); // Đổi hết sang chữ HOA
                            decimal amount = decimal.Parse(trans["amount"].ToString());
                            string noiDungCanTimHoa = noiDungCanTim.ToUpper();

                            // --- LOGIC KIỂM TRA ---

                            // TRƯỜNG HỢP 1: ĐÚNG TIỀN + ĐÚNG NỘI DUNG -> THÀNH CÔNG 100%
                            if (description.Contains(noiDungCanTimHoa) && amount >= soTienCanTim)
                            {
                                return true;
                            }

                            // TRƯỜNG HỢP 2 (HỖ TRỢ DEBUG): ĐÚNG TIỀN NHƯNG SAI NỘI DUNG
                            // Chỉ hiện thông báo này 1 lần để bạn biết mình sai ở đâu
                            if (amount >= soTienCanTim && !description.Contains(noiDungCanTimHoa))
                            {
                                // Kiểm tra xem đây có phải là giao dịch mới không (tránh báo giao dịch cũ rích)
                                // Chỉ báo nếu giao dịch này chưa từng được báo
                                System.Diagnostics.Debug.WriteLine($"⚠️ TÌM THẤY TIỀN NHƯNG SAI NỘI DUNG!");
                                System.Diagnostics.Debug.WriteLine($"App cần tìm: '{noiDungCanTimHoa}'");
                                System.Diagnostics.Debug.WriteLine($"Ngân hàng báo: '{description}'");

                                // Mẹo: Nếu bạn muốn "du di" cho qua luôn kể cả khi sai nội dung (chỉ cần đúng tiền)
                                // Thì bỏ comment dòng dưới đây (Nguy hiểm nếu nhiều người ck cùng lúc):
                                // return true; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi: " + ex.Message);
            }
            return false;
        }


        private void LoadLichDatCuaSan(int maSan)
        {
            if (flpDanhSachDonHang == null) return;
            flpDanhSachDonHang.Controls.Clear();
            if (grpThongTinDon != null) grpThongTinDon.Enabled = true;

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
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
                        int maDon = Convert.ToInt32(r["MaDatSan"]);
                        string tenKhach = r["HoTen"].ToString();
                        string tenSan = r["TenSan"].ToString();
                        DateTime ngay = Convert.ToDateTime(r["NgayDat"]);
                        string gioBD = TimeSpan.Parse(r["GioBatDau"].ToString()).ToString(@"hh\:mm");
                        string gioKT = TimeSpan.Parse(r["GioKetThuc"].ToString()).ToString(@"hh\:mm");
                        string thoiGian = $"{gioBD} - {gioKT}";
                        decimal tien = Convert.ToDecimal(r["TongTienSan"]);
                        string tt = r["TrangThai"].ToString();

                        item.HienThiThongTin(maDon, tenKhach, tenSan, ngay, thoiGian, tien, tt);
                        item.OnThanhToanClick += Item_OnThanhToanClick;
                        item.OnHuyClick += Item_OnHuyClick;

                        flpDanhSachDonHang.Controls.Add(item);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi load lịch sân: " + ex.Message); }
            }
        }

        private void TuDongXoaLichQuaKhu()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"DELETE FROM LichDatSan WHERE NgayDat < CAST(GETDATE() AS DATE) AND TrangThai IN ('DA_XAC_NHAN', 'CHO_XAC_NHAN')";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch { }
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

        private void btnHuy_Click_1(object sender, EventArgs e) { }
        private void btnThanhToan_Click(object sender, EventArgs e) { }
        private void btnXoaDon_Click(object sender, EventArgs e) { }
        private void groupBox3_Enter(object sender, EventArgs e) { }
        private void lblThongBao_Click(object sender, EventArgs e) { }
        private void ResetGroupDon() { }
        // --- BỔ SUNG HÀM BỊ THIẾU ---
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa hết thông tin đang nhập?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetFormDatSan();
            }
        }

        #endregion
    }
}