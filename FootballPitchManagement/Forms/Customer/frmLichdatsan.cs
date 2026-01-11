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
            // Gán sự kiện Load
            this.Load += LichDatSan_Load;
        }

        private void LichDatSan_Load(object sender, EventArgs e)
        {
            try
            {
                // Test kết nối database trước
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    this.Close();
                    return;
                }

                // 1. Load dữ liệu vào ComboBox
                LoadChiNhanh();
                LoadLoaiSan();

                // 2. Gán sự kiện cho DateTimePicker
                dtpNgayXem.ValueChanged += Filter_Changed;
                dtpGioBatDau.ValueChanged += dtpGio_ValueChanged;
                dtpGioKetThuc.ValueChanged += dtpGio_ValueChanged;

                // 3. Gán sự kiện cho các nút
                btnTimKH.Click += btnTimKH_Click;
                btnDatSan.Click += btnDatSan_Click;
                btnHuy.Click += btnHuy_Click;

                // 4. Thiết lập giá trị mặc định
                dtpNgayXem.Value = DateTime.Now;
                dtpGioBatDau.Value = DateTime.Now;
                dtpGioKetThuc.Value = DateTime.Now.AddHours(1);
                
                // 5. Reset form về trạng thái ban đầu
                ResetFormDatSan();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region === HÀM XỬ LÝ SỰ KIỆN BỘ LỌC ===

        private void Filter_Changed(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra Chi nhánh
                if (cboChiNhanh.SelectedValue == null) return;
                if (!int.TryParse(cboChiNhanh.SelectedValue.ToString(), out int maCN)) return;

                // Kiểm tra Loại sân
                if (cboLoaiSan.SelectedValue == null) return;
                if (!int.TryParse(cboLoaiSan.SelectedValue.ToString(), out int maLoai)) return;

                // Lấy Ngày xem
                DateTime ngayXem = dtpNgayXem.Value.Date;

                // Tải danh sách sân
                LoadSanBong(maCN, maLoai, ngayXem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lọc dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region === HÀM LOAD DỮ LIỆU ===

        private void LoadChiNhanh()
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

                        cboChiNhanh.DataSource = dt;
                        cboChiNhanh.DisplayMember = "TenChiNhanh";
                        cboChiNhanh.ValueMember = "MaChiNhanh";
                        cboChiNhanh.SelectedIndex = -1;

                        // Gán sự kiện (tránh gán trùng)
                        cboChiNhanh.SelectedIndexChanged -= Filter_Changed;
                        cboChiNhanh.SelectedIndexChanged += Filter_Changed;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load Chi nhánh: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboLoaiSan.DataSource = dt;
                        cboLoaiSan.DisplayMember = "TenLoaiSan";
                        cboLoaiSan.ValueMember = "MaLoaiSan";
                        cboLoaiSan.SelectedIndex = -1;

                        // Gán sự kiện (tránh gán trùng)
                        cboLoaiSan.SelectedIndexChanged -= Filter_Changed;
                        cboLoaiSan.SelectedIndexChanged += Filter_Changed;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load Loại sân: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadSanBong(int maChiNhanh, int maLoaiSan, DateTime ngayXem)
        {
            // Xóa hết sân cũ
            flpDanhSachSan.Controls.Clear();

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // ✅ ĐÃ SỬA: tt.MaTinhTrang (không có khoảng trắng)
                        string sql = @"
                SELECT 
                    s.MaSan, 
                    s.TenSan, 
                    s.GiaMacDinh, 
                    tt.TenTinhTrang AS TrangThaiCoDinh,
                    (SELECT COUNT(*) 
                     FROM LichDatSan l 
                     WHERE l.MaSan = s.MaSan 
                       AND l.NgayDat = @NgayXem 
                       AND l.TrangThai != N'DA_HUY') AS SoLuotDat
                FROM San s
                INNER JOIN TinhTrangSan tt ON s.MaTinhTrang = tt.MaTinhTrang
                WHERE s.MaChiNhanh = @MaCN
                  AND s.MaLoaiSan = @MaLoai
                  AND s.TrangThai = 1
                ORDER BY s.TenSan";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCN", maChiNhanh);
                        cmd.Parameters.AddWithValue("@MaLoai", maLoaiSan);
                        cmd.Parameters.AddWithValue("@NgayXem", ngayXem);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int count = 0;

                            while (reader.Read())
                            {
                                count++;

                                // Lấy dữ liệu
                                int id = Convert.ToInt32(reader["MaSan"]);
                                string ten = reader["TenSan"].ToString();
                                decimal gia = Convert.ToDecimal(reader["GiaMacDinh"]);
                                string trangThaiCoDinh = reader["TrangThaiCoDinh"].ToString();
                                int soLuotDat = Convert.ToInt32(reader["SoLuotDat"]);

                                // Xử lý trạng thái hiển thị
                                string trangThaiHienThi;
                                if (trangThaiCoDinh.Equals("Bảo trì", StringComparison.OrdinalIgnoreCase))
                                {
                                    trangThaiHienThi = "Bảo trì";
                                }
                                else if (soLuotDat > 0)
                                {
                                    trangThaiHienThi = "Đã đặt";
                                }
                                else
                                {
                                    trangThaiHienThi = "Trống";
                                }

                                // Tạo và thêm control
                                ucSanBong item = new ucSanBong();
                                item.ThietLapThongTin(id, ten, trangThaiHienThi, gia);
                                item.OnSelect += Item_OnSelect;
                                flpDanhSachSan.Controls.Add(item);
                            }

                            // Thông báo nếu không có sân
                            if (count == 0)
                            {
                                Label lblEmpty = new Label
                                {
                                    Text = "📢 Không có sân nào phù hợp với điều kiện lọc",
                                    AutoSize = false,
                                    Width = flpDanhSachSan.Width - 20,
                                    Height = 50,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    ForeColor = Color.Gray,
                                    Font = new Font("Segoe UI", 10, FontStyle.Italic)
                                };
                                flpDanhSachSan.Controls.Add(lblEmpty);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi tải sân: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region === XỬ LÝ CHỌN SÂN ===

        private void Item_OnSelect(object sender, EventArgs e)
        {
            ucSanBong sanDuocChon = sender as ucSanBong;
            if (sanDuocChon == null) return;

            // Kiểm tra trạng thái sân
            if (sanDuocChon.TrangThai.Equals("Bảo trì", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "⚠️ Sân này đang bảo trì/sửa chữa.\nVui lòng chọn sân khác!",
                    "Cảnh báo", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            if (sanDuocChon.TrangThai.Equals("Đã đặt", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "ℹ️ Sân này đã có lịch đặt trong ngày này.\nVui lòng chọn giờ khác hoặc sân khác!",
                    "Thông báo", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            // Cập nhật thông tin sân đã chọn
            lblTenSanChon.Text = sanDuocChon.TenSan;
            lblGiaGoc.Text = sanDuocChon.GiaMacDinh.ToString("N0") + " đ";
            lblMaSan.Text = sanDuocChon.MaSan.ToString();
            lblTenSanChon.ForeColor = Color.Green;

            _maSanDangChon = sanDuocChon.MaSan;
            _giaSanHienTai = sanDuocChon.GiaMacDinh;

            // Reset giờ về mặc định
            DateTime now = DateTime.Now;
            dtpGioBatDau.Value = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            dtpGioKetThuc.Value = dtpGioBatDau.Value.AddHours(1);

            // Tính tiền
            TinhTien();
        }

        #endregion

        #region === TÍNH TIỀN ===

        private void TinhTien()
        {
            try
            {
                // Kiểm tra có chọn sân chưa
                if (_giaSanHienTai == 0)
                {
                    txtTongTien.Text = "0";
                    return;
                }

                TimeSpan thoiLuong = dtpGioKetThuc.Value - dtpGioBatDau.Value;
                double soGio = thoiLuong.TotalHours;

                if (soGio <= 0)
                {
                    txtTongTien.Text = "0";
                    txtTongTien.ForeColor = Color.Red;
                    return;
                }

                decimal tongTien = (decimal)soGio * _giaSanHienTai;
                txtTongTien.Text = tongTien.ToString("N0");
                txtTongTien.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tính tiền: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpGio_ValueChanged(object sender, EventArgs e)
        {
            TinhTien();
        }

        #endregion

        #region === TÌM KHÁCH HÀNG ===

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("⚠️ Vui lòng nhập số điện thoại!", 
                    "Thiếu thông tin", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            // Kiểm tra định dạng số điện thoại
            if (sdt.Length < 10 || !sdt.All(char.IsDigit))
            {
                MessageBox.Show("⚠️ Số điện thoại không hợp lệ!\nVui lòng nhập từ 10 chữ số.", 
                    "Lỗi định dạng", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                txtSDT.Focus();
                txtSDT.SelectAll();
                return;
            }

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT MaKH, HoTen FROM KhachHang WHERE DienThoai = @SDT";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SDT", sdt);

                        using (SqlDataReader r = cmd.ExecuteReader())
                        {
                            if (r.Read())
                            {
                                // ✅ Tìm thấy khách hàng
                                txtTenKhach.Text = r["HoTen"].ToString();
                                lblMaKH.Text = r["MaKH"].ToString();
                                txtTenKhach.ForeColor = Color.Green;
                                
                                MessageBox.Show("✅ Đã tìm thấy thông tin khách hàng!", 
                                    "Thành công", 
                                    MessageBoxButtons.OK, 
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                // ❌ Không tìm thấy - Hỏi có thêm mới không
                                r.Close();
                                
                                DialogResult kq = MessageBox.Show(
                                    "❓ Khách hàng này chưa có trong hệ thống.\n\n" +
                                    "Bạn có muốn THÊM MỚI nhanh không?",
                                    "Không tìm thấy", 
                                    MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Question);

                                if (kq == DialogResult.Yes)
                                {
                                    ThemKhachHangMoi(sdt, conn);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi tìm khách hàng: {ex.Message}", 
                        "Lỗi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }

        private void ThemKhachHangMoi(string sdt, SqlConnection conn)
        {
            try
            {
                string tenKhachMoi = $"Khách vãng lai ({sdt})";
                string sqlInsert = @"
                    INSERT INTO KhachHang(HoTen, DienThoai) 
                    VALUES (@HoTen, @SDT); 
                    SELECT SCOPE_IDENTITY();";
                
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@HoTen", tenKhachMoi);
                    cmdInsert.Parameters.AddWithValue("@SDT", sdt);

                    object newId = cmdInsert.ExecuteScalar();

                    if (newId != null)
                    {
                        txtTenKhach.Text = tenKhachMoi;
                        lblMaKH.Text = newId.ToString();
                        txtTenKhach.ForeColor = Color.Green;
                        
                        MessageBox.Show(
                            "✅ Đã thêm khách hàng mới thành công!\n\n" +
                            $"Mã KH: {newId}\n" +
                            $"Tên: {tenKhachMoi}", 
                            "Thành công", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi thêm khách hàng: {ex.Message}", 
                    "Lỗi", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region === ĐẶT SÂN ===

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            // ===== VALIDATION =====

            // 1. Kiểm tra đã chọn sân chưa
            if (_maSanDangChon == 0)
            {
                MessageBox.Show(
                    "⚠️ Vui lòng chọn một sân bóng trước khi đặt!", 
                    "Thiếu thông tin", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra đã chọn khách hàng chưa
            if (string.IsNullOrEmpty(lblMaKH.Text) || lblMaKH.Text == "0")
            {
                MessageBox.Show(
                    "⚠️ Vui lòng tìm và chọn khách hàng trước!", 
                    "Thiếu thông tin", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            // 3. Lấy dữ liệu ngày giờ
            DateTime ngayDat = dtpNgayXem.Value.Date;
            TimeSpan gioBatDau = dtpGioBatDau.Value.TimeOfDay;
            TimeSpan gioKetThuc = dtpGioKetThuc.Value.TimeOfDay;

            // 4. Kiểm tra logic giờ
            if (gioKetThuc <= gioBatDau)
            {
                MessageBox.Show(
                    "❌ Giờ kết thúc phải lớn hơn giờ bắt đầu!", 
                    "Lỗi thời gian", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                dtpGioKetThuc.Focus();
                return;
            }

            // 5. Kiểm tra thời lượng tối thiểu
            double thoiLuongPhut = (gioKetThuc - gioBatDau).TotalMinutes;
            if (thoiLuongPhut < 30)
            {
                MessageBox.Show(
                    "⚠️ Thời gian đá tối thiểu phải là 30 phút!", 
                    "Lỗi thời gian", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            // 6. Kiểm tra đặt sân trong quá khứ
            DateTime gioHienTai = DateTime.Now;
            DateTime gioSoBatDau = ngayDat.Add(gioBatDau);
            
            if (gioSoBatDau < gioHienTai)
            {
                MessageBox.Show(
                    "⚠️ Không thể đặt sân trong quá khứ!\n\n" +
                    "Vui lòng chọn thời gian trong tương lai.", 
                    "Lỗi thời gian", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
                return;
            }

            // ===== XỬ LÝ ĐẶT SÂN =====
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Kiểm tra trùng lịch
                    if (KiemTraTrungLich(conn, ngayDat, gioBatDau, gioKetThuc))
                    {
                        MessageBox.Show(
                            "❌ Rất tiếc! Sân này ĐÃ CÓ NGƯỜI ĐẶT trong khung giờ bạn chọn.\n\n" +
                            "Vui lòng:\n" +
                            "• Kiểm tra lại lịch sân (các ô màu đỏ)\n" +
                            "• Hoặc chọn khung giờ khác",
                            "Trùng lịch đặt sân", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Stop);
                        return;
                    }

                    // Thực hiện đặt sân
                    if (ThucHienDatSan(conn, ngayDat, gioBatDau, gioKetThuc, thoiLuongPhut))
                    {
                        MessageBox.Show(
                            "✅ ĐẶT SÂN THÀNH CÔNG!\n\n" +
                            $"📅 Ngày: {ngayDat:dd/MM/yyyy}\n" +
                            $"⏰ Giờ: {gioBatDau:hh\\:mm} - {gioKetThuc:hh\\:mm}\n" +
                            $"💰 Tổng tiền: {txtTongTien.Text} đ",
                            "Thông báo", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);

                        // Refresh và reset
                        Filter_Changed(null, null);
                        ResetFormDatSan();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"❌ Lỗi hệ thống: {ex.Message}\n\n" +
                        "Vui lòng thử lại hoặc liên hệ quản trị viên.", 
                        "Lỗi", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }

        private bool KiemTraTrungLich(SqlConnection conn, DateTime ngayDat, TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            string sqlCheck = @"
                SELECT COUNT(*) 
                FROM LichDatSan 
                WHERE MaSan = @MaSan 
                  AND NgayDat = @NgayDat 
                  AND TrangThai != N'DA_HUY' 
                  AND (@GioBatDau < GioKetThuc) 
                  AND (@GioKetThuc > GioBatDau)";

            using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn))
            {
                cmdCheck.Parameters.AddWithValue("@MaSan", _maSanDangChon);
                cmdCheck.Parameters.AddWithValue("@NgayDat", ngayDat);
                cmdCheck.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                cmdCheck.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);

                int soLuongTrung = (int)cmdCheck.ExecuteScalar();
                return soLuongTrung > 0;
            }
        }

        private bool ThucHienDatSan(SqlConnection conn, DateTime ngayDat, TimeSpan gioBatDau, TimeSpan gioKetThuc, double thoiLuongPhut)
        {
            string sqlInsert = @"
                INSERT INTO LichDatSan (
                    MaKH, MaSan, NgayDat, GioBatDau, GioKetThuc, 
                    SoGio, TongTienSan, TrangThai, GhiChu
                )
                VALUES (
                    @MaKH, @MaSan, @NgayDat, @GioBatDau, @GioKetThuc, 
                    @SoGio, @TongTien, N'DA_XAC_NHAN', N'Đặt tại quầy'
                )";

            using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
            {
                cmdInsert.Parameters.AddWithValue("@MaKH", int.Parse(lblMaKH.Text));
                cmdInsert.Parameters.AddWithValue("@MaSan", _maSanDangChon);
                cmdInsert.Parameters.AddWithValue("@NgayDat", ngayDat);
                cmdInsert.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                cmdInsert.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);

                double soGio = thoiLuongPhut / 60.0;
                cmdInsert.Parameters.AddWithValue("@SoGio", soGio);

                string tienSach = txtTongTien.Text.Replace(".", "").Replace(",", "").Trim();
                decimal tongTien = decimal.TryParse(tienSach, out decimal t) ? t : 0;
                cmdInsert.Parameters.AddWithValue("@TongTien", tongTien);

                int ketQua = cmdInsert.ExecuteNonQuery();
                return ketQua > 0;
            }
        }

        #endregion

        #region === RESET FORM ===

        private void ResetFormDatSan()
        {
            // Reset thông tin khách hàng
            txtSDT.Clear();
            txtTenKhach.Clear();
            lblMaKH.Text = "0";
            txtTenKhach.ForeColor = SystemColors.ControlText;

            // Reset thông tin sân
            lblTenSanChon.Text = "Chưa chọn";
            lblTenSanChon.ForeColor = Color.Gray;
            lblGiaGoc.Text = "0 đ";
            lblMaSan.Text = "0";

            // Reset tiền
            txtTongTien.Clear();
            txtTongTien.ForeColor = SystemColors.ControlText;

            // Reset biến
            _maSanDangChon = 0;
            _giaSanHienTai = 0;

            // Focus vào ô số điện thoại
            txtSDT.Focus();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn hủy và xóa toàn bộ thông tin đã nhập?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ResetFormDatSan();
            }
        }

        #endregion
    }
}
