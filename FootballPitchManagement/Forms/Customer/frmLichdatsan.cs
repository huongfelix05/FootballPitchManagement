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

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmLichdatsan : Form
    {
        // Biến toàn cục
        private int _maSanDangChon = 0;
        private decimal _giaSanHienTai = 0;

        // --- CẤU HÌNH NGÂN HÀNG (VIETQR & CASSO) ---
        private const string BANK_ID = "BIDV";       // Mã ngân hàng
        private const string ACCOUNT_NO = "V3CASS7500868888"; // Số tài khoản CỦA BẠN
        private const string ACCOUNT_NAME = "NGUYEN VAN HUONG";

        // API KEY CASSO (Bạn đã điền)
        private const string CASSO_API_KEY = "AK_CS.edc4ea30f36f11f0819c2d6675ce85db.52vDQ5Scri98fUECSifsHaXxiw6f5w5FpN00LtDUJGhrKfjmpoEReVfZZDkytjHQdbVujT9w";

        private int maChiNhanhDuocChon = 0;
        private string tenChiNhanhDuocChon = "";

        // Constructor mặc định
        public frmLichdatsan()
        {
            InitializeComponent();
            this.Load += FrmLichdatsan_Load;
        }

        // ✅ Constructor nhận tham số chi nhánh
        public frmLichdatsan(int maChiNhanh, string tenChiNhanh)
        {
            InitializeComponent();
            this.maChiNhanhDuocChon = maChiNhanh;
            this.tenChiNhanhDuocChon = tenChiNhanh;
            this.Load += FrmLichdatsan_Load;
        }

        private void FrmLichdatsan_Load(object sender, EventArgs e)
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

                LoadComboBoxChiNhanh();
                LoadComboBoxLoaiSan();

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

                // ✅ NẾU CÓ CHI NHÁNH ĐƯỢC CHỌN SẴN
                if (maChiNhanhDuocChon > 0)
                {
                    // Chọn chi nhánh trong ComboBox
                    cboChiNhanh.SelectedValue = maChiNhanhDuocChon;
                    
                    // Load thông tin chi nhánh và hiển thị
                    HienThiThongTinChiNhanh(maChiNhanhDuocChon);
                }

                LoadDanhSachSan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo hệ thống:\n{ex.Message}", "Lỗi Khởi Tạo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HÀM HIỂN THỊ THÔNG TIN CHI NHÁNH (ĐỊA CHỈ + SĐT)
        private void HienThiThongTinChiNhanh(int maChiNhanh)
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT TenChiNhanh, DiaChi, DienThoai 
                        FROM ChiNhanh 
                        WHERE MaChiNhanh = @MaChiNhanh AND TrangThai = 1";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tenChiNhanh = reader["TenChiNhanh"].ToString();
                                string diaChi = reader["DiaChi"].ToString();
                                string dienThoai = reader["DienThoai"]?.ToString() ?? "";

                                // ✅ COMMENT OUT OR REMOVE THIS MessageBox.Show() LINE
                                /*
                                string thongBao = $"✅ Đã chọn chi nhánh: {tenChiNhanh}\n" +
                                                 $"📍 Địa chỉ: {diaChi}\n" +
                                                 $"📞 Điện thoại: {dienThoai}";

                                MessageBox.Show(thongBao, "Thông tin chi nhánh", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                */
                               

                                // ✅ CẬP NHẬT LABEL HIỂN THỊ THÔNG TIN CHI NHÁNH (NẾU CÓ)
                                UpdateChiNhanhDisplay(tenChiNhanh, diaChi, dienThoai);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load thông tin chi nhánh: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ HÀM CẬP NHẬT HIỂN THỊ THÔNG TIN CHI NHÁNH TRÊN FORM (NẾU CÓ LABEL)
        private void UpdateChiNhanhDisplay(string tenChiNhanh, string diaChi, string dienThoai)
        {
            try
            {
                // Tìm các label để hiển thị thông tin (nếu có trong form)
                Control lblChiNhanh = this.Controls.Find("lblThongTinChiNhanh", true).FirstOrDefault();
                if (lblChiNhanh is Label)
                {
                    ((Label)lblChiNhanh).Text = $"{tenChiNhanh}\n📍 {diaChi}\n📞 {dienThoai}";
                }

                // Hoặc cập nhật title của form
                this.Text = $"Đặt lịch sân - {tenChiNhanh}";
            }
            catch (Exception ex)
            {
                // Không hiển thị lỗi này vì chỉ là cập nhật UI
                System.Diagnostics.Debug.WriteLine($"Không thể cập nhật UI: {ex.Message}");
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
                MessageBox.Show($"Đã xảy ra lỗi khi lọc dữ liệu:\n{ex.Message}", "Lỗi Bộ Lọc", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

                        // Thêm option "Chọn chi nhánh"
                        DataRow row = dt.NewRow();
                        row["MaChiNhanh"] = 0;
                        row["TenChiNhanh"] = "-- Chọn chi nhánh --";
                        dt.Rows.InsertAt(row, 0);

                        cboChiNhanh.DataSource = dt;
                        cboChiNhanh.DisplayMember = "TenChiNhanh";
                        cboChiNhanh.ValueMember = "MaChiNhanh";
                        cboChiNhanh.SelectedIndex = 0;

                        cboChiNhanh.SelectedIndexChanged += CboChiNhanh_SelectedIndexChanged;
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
                        row["TenLoaiSan"] = "-- Tất cả loại sân --";
                        dt.Rows.InsertAt(row, 0);

                        cboLoaiSan.DataSource = dt;
                        cboLoaiSan.DisplayMember = "TenLoaiSan";
                        cboLoaiSan.ValueMember = "MaLoaiSan";
                        cboLoaiSan.SelectedIndex = 0;

                        cboLoaiSan.SelectedIndexChanged += CboLoaiSan_SelectedIndexChanged;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load loại sân: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ SỰ KIỆN THAY ĐỔI CHI NHÁNH - HIỂN THỊ THÔNG TIN
        private void CboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChiNhanh.SelectedValue != null)
            {
                int maChiNhanh = Convert.ToInt32(cboChiNhanh.SelectedValue);
                if (maChiNhanh > 0)
                {
                    // Hiển thị thông tin chi nhánh khi người dùng thay đổi lựa chọn
                    HienThiThongTinChiNhanh(maChiNhanh);
                }
            }
            LoadDanhSachSan();
        }

        private void CboLoaiSan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDanhSachSan();
        }

        private void LoadDanhSachSan()
        {
            // Implementation giữ nguyên như cũ...
            LoadSanBong(
                cboChiNhanh.SelectedValue != null ? Convert.ToInt32(cboChiNhanh.SelectedValue) : 0,
                cboLoaiSan.SelectedValue != null ? Convert.ToInt32(cboLoaiSan.SelectedValue) : 0,
                dtpNgayXem.Value.Date
            );
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
                        Label lbl = new Label() { 
                            Text = "Không có sân phù hợp với bộ lọc", 
                            AutoSize = true, 
                            ForeColor = Color.Red, 
                            Font = new Font("Segoe UI", 10, FontStyle.Italic) 
                        };
                        flpDanhSachSan.Controls.Add(lbl);
                    }
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show($"Không thể tải danh sách sân:\n{ex.Message}", "Lỗi Tải Dữ Liệu", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
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
                MessageBox.Show("Sân đang trong quá trình bảo trì.\nVui lòng chọn sân khác!", 
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (_giaSanHienTai == 0) { txtTongTien.Text = "0"; return; }

            DateTime timeBD = new DateTime(dtpGioBatDau.Value.Year, dtpGioBatDau.Value.Month, dtpGioBatDau.Value.Day,
                                           dtpGioBatDau.Value.Hour, dtpGioBatDau.Value.Minute, 0);

            DateTime timeKT = new DateTime(dtpGioKetThuc.Value.Year, dtpGioKetThuc.Value.Month, dtpGioKetThuc.Value.Day,
                                           dtpGioKetThuc.Value.Hour, dtpGioKetThuc.Value.Minute, 0);

            double soGio = (timeKT - timeBD).TotalHours;
            if (soGio <= 0) { txtTongTien.Text = "0"; return; }

            decimal tongTien = (decimal)soGio * _giaSanHienTai;
            tongTien = Math.Round(tongTien, 0);
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
                lblThongBao.ForeColor = Color.Green;
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
                        txtTenKhach.Text = r["HoTen"].ToString();
                        lblMaKH.Text = r["MaKH"].ToString();
                        txtTenKhach.ForeColor = Color.Green;
                        LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text));
                        MessageBox.Show("✅ Đã tìm thấy khách hàng: " + txtTenKhach.Text, 
                            "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblMaKH.Text = "0";
                        txtTenKhach.Text = "";
                        if (flpDanhSachDonHang != null) flpDanhSachDonHang.Controls.Clear();

                        MessageBox.Show("❌ Không tìm thấy khách hàng!\nVui lòng kiểm tra lại SĐT.", 
                            "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSDT.Focus();
                        txtSDT.SelectAll();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi hệ thống khi tìm khách:\n{ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            if (_maSanDangChon == 0) 
            { 
                MessageBox.Show("Vui lòng chọn một sân bóng!", "Chưa Chọn Sân", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }
            
            if (lblMaKH.Text == "0" || string.IsNullOrEmpty(lblMaKH.Text)) 
            { 
                MessageBox.Show("Vui lòng nhập sđt và tìm khách hàng!", "Chưa Chọn Khách", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            DateTime ngayDat = dtpNgayXem.Value.Date;
            TimeSpan bd = dtpGioBatDau.Value.TimeOfDay;
            TimeSpan kt = dtpGioKetThuc.Value.TimeOfDay;

            if (kt <= bd) 
            { 
                MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return; 
            }
            
            if ((kt - bd).TotalMinutes < 60)
            {
                MessageBox.Show("Thời gian đặt sân tối thiểu phải là 1 tiếng (60 phút)!", 
                    "Quy Định", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (KiemTraTrungLich(_maSanDangChon, ngayDat, bd, kt))
            {
                MessageBox.Show("❌ Rất tiếc! Khung giờ này vừa có người đặt.", 
                    "Trùng Lịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                    MessageBox.Show("✅ Đặt sân thành công!", "Thành Công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDanhSachDonHangCuaKhach(int.Parse(lblMaKH.Text));
                    LoadLichDatCuaSan(_maSanDangChon);

                    _maSanDangChon = 0;
                    _giaSanHienTai = 0;
                    lblTenSanChon.Text = "Chưa chọn";
                    txtTongTien.Text = "0";
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show($"Lỗi khi lưu đặt sân:\n{ex.Message}", "Lỗi Hệ Thống", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn xóa hết thông tin đang nhập?", "Xác Nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetFormDatSan();
            }
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

                    string sqlDoanhThu = @"INSERT INTO DoanhThu (MaChiNhanh, Ngay, LoaiDoanhThu, SoTien, GhiChu)
                                   VALUES (@MaChiNhanh, CAST(GETDATE() AS DATE), N'SAN', @SoTien, N'Thu tiền sân đơn #' + CAST(@MaDatSan AS NVARCHAR))";
                    SqlCommand cmdDT = new SqlCommand(sqlDoanhThu, conn, transaction);
                    cmdDT.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                    cmdDT.Parameters.AddWithValue("@SoTien", tongTien);
                    cmdDT.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    cmdDT.ExecuteNonQuery();

                    string sqlUpdate = "UPDATE LichDatSan SET TrangThai = 'HOAN_THANH' WHERE MaDatSan = @MaDatSan";
                    SqlCommand cmdUpd = new SqlCommand(sqlUpdate, conn, transaction);
                    cmdUpd.Parameters.AddWithValue("@MaDatSan", maDatSan);
                    cmdUpd.ExecuteNonQuery();

                    transaction.Commit();
                    string msg = (phuongThuc == "CHUYEN_KHOAN") ? "✅ Đã xác nhận chuyển khoản thành công!" : "✅ Đã thu tiền mặt thành công!";
                    MessageBox.Show(msg, "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
private void flpDanhSachSan_Paint(object sender, PaintEventArgs e)
        {

        }
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
        // --- HÀM KIỂM TRA TIỀN (ĐÃ NÂNG CẤP CHỐNG CACHE & TĂNG TỐC) ---
        private async Task<bool> KiemTraTienVeQuaCasso(string noiDungCanTim, decimal soTienCanTim)
        {
            try
            {
                // 1. Cấu hình bảo mật (Bắt buộc)
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Apikey " + CASSO_API_KEY);

                    // 2. MẸO QUAN TRỌNG: Thêm tham số ngẫu nhiên &t=... để ép máy tính không được dùng bộ nhớ tạm (Cache)
                    // Tăng pageSize lên 20 để quét sâu hơn
                    string url = $"https://oauth.casso.vn/v2/transactions?pageSize=20&t={DateTime.Now.Ticks}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(jsonResponse);

                        // Check lỗi API
                        if (data["error"] != null && data["error"].ToString() != "0")
                        {
                            // Nếu lỗi thì âm thầm bỏ qua để Timer chạy tiếp lần sau
                            return false;
                        }

                        var transactions = data["data"]["records"];

                        // 3. DUYỆT TÌM GIAO DỊCH
                        foreach (var trans in transactions)
                        {
                            string description = trans["description"].ToString().ToUpper(); // Chuyển chữ hoa hết
                            decimal amount = decimal.Parse(trans["amount"].ToString());
                            string noiDungCanTimHoa = noiDungCanTim.ToUpper();

                            // LOGIC SO SÁNH:
                            // - Nội dung chứa mã đơn (VD: DS11)
                            // - Số tiền phải LỚN HƠN HOẶC BẰNG số tiền yêu cầu (để tránh lỗi làm tròn)
                            if (description.Contains(noiDungCanTimHoa) && amount >= soTienCanTim)
                            {
                                return true; // TÌM THẤY! -> Báo về để tắt form
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lỗi vào Output để debug nếu cần, không hiện MessageBox làm phiền người dùng lúc đang chờ
                System.Diagnostics.Debug.WriteLine("Lỗi Check Casso: " + ex.Message);
            }
            return false; // Chưa thấy tiền
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

        #endregion
    }
}