

namespace FootballPitchManagement
{
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

    namespace FootballPitchManagement
    {
        public partial class LichDatSan : Form
        {
            // Kiểm tra lại tên máy (Data Source) cho đúng máy bạn
            private string connStr = @"Data Source=MSI;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True;TrustServerCertificate=True;";

            private int _maSanDangChon = 0;       // Để lưu ID sân đang bấm
            private decimal _giaSanHienTai = 0;   // Để lưu giá tiền sân đó

            public LichDatSan()
            {
                InitializeComponent();
            }

            private void LichDatSan_Load(object sender, EventArgs e)
            {
                // 1. Load dữ liệu vào ComboBox
                LoadChiNhanh();
                LoadLoaiSan();

                // 2. Gán sự kiện cho DateTimePicker
                dtpNgayXem.ValueChanged += Filter_Changed;

                // Lúc này chưa load sân, form sẽ trắng trơn cho đến khi chọn đủ
            }

            // --- HÀM XỬ LÝ SỰ KIỆN CHUNG (BỘ LỌC) ---
            // Hàm này sẽ được gọi mỗi khi người dùng thay đổi bất kỳ ô nào
            private void Filter_Changed(object sender, EventArgs e)
            {
                // 1. Kiểm tra Chi nhánh
                if (cboChiNhanh.SelectedValue == null) return;
                int maCN;
                if (!int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maCN)) return;

                // 2. Kiểm tra Loại sân
                if (cboLoaiSan.SelectedValue == null) return;
                int maLoai;
                if (!int.TryParse(cboLoaiSan.SelectedValue.ToString(), out maLoai)) return;

                // 3. Lấy Ngày xem từ DateTimePicker
                DateTime ngayXem = dtpNgayXem.Value;

                // GỌI HÀM VỚI 3 THAM SỐ
                LoadSanBong(maCN, maLoai, ngayXem);
            }
            // --- HÀM LOAD DỮ LIỆU NỀN ---

            // HÀM 1: Load danh sách Chi Nhánh
            private void LoadChiNhanh()
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        string sql = "SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh WHERE TrangThai = 1";
                        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboChiNhanh.DataSource = dt;
                        cboChiNhanh.DisplayMember = "TenChiNhanh";
                        cboChiNhanh.ValueMember = "MaChiNhanh";

                        // Mẹo: Đặt về -1 để chưa chọn gì cả
                        cboChiNhanh.SelectedIndex = -1;
                        // QUAN TRỌNG: Gán sự kiện đổi chi nhánh vào hàm Filter_Changed
                        cboChiNhanh.SelectedIndexChanged += Filter_Changed;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi load Chi nhánh: " + ex.Message);
                    }
                }
            }

            // HÀM 2: Load danh sách Loại Sân
            private void LoadLoaiSan()
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        string sql = "SELECT MaLoaiSan, TenLoaiSan FROM LoaiSan";
                        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cboLoaiSan.DataSource = dt;
                        cboLoaiSan.DisplayMember = "TenLoaiSan";
                        cboLoaiSan.ValueMember = "MaLoaiSan";

                        // Đặt về -1 để chưa chọn gì cả
                        cboLoaiSan.SelectedIndex = -1;

                        // QUAN TRỌNG: Gán sự kiện chọn loại sân vào hàm Filter_Changed
                        cboLoaiSan.SelectedIndexChanged += Filter_Changed;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi load Loại sân: " + ex.Message);
                    }
                }
            }

            // --- HÀM HIỂN THỊ SÂN ---

            // HÀM 3: Vẽ danh sách sân (Có kiểm tra lịch đặt)
            // HÀM 3: Vẽ danh sách sân (Xử lý logic Trống/Đầy/Bảo trì theo ngày)
            // HÀM 3: Vẽ danh sách sân (Ràng buộc 3 điều kiện + Check trạng thái động)
            private void LoadSanBong(int maChiNhanh, int maLoaiSan, DateTime ngayXem)
            {
                // 1. Xóa hết sân cũ
                flpDanhSachSan.Controls.Clear();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();

                        // --- CÂU SQL LOGIC ---
                        // 1. Lấy sân theo Chi Nhánh và Loại Sân (WHERE).
                        // 2. Dùng Sub-query đếm xem ngày đó (NgayXem) sân này có trong bảng LichDatSan không.
                        string sql = @"
                SELECT 
                    s.MaSan, 
                    s.TenSan, 
                    s.GiaMacDinh, 
                    tt.TenTinhTrang AS TrangThaiCoDinh, -- Trạng thái gốc (vd: Bảo trì)
                    (SELECT COUNT(*) 
                     FROM LichDatSan l 
                     WHERE l.MaSan = s.MaSan 
                     AND l.NgayDat = @NgayXem 
                     AND l.TrangThai != 'DA_HUY') AS SoLuotDat -- Đếm số đơn đặt trong ngày
                FROM San s
JOIN TinhTrangSan tt ON s.MaTinhTrang = tt.MaTinhTrang
                WHERE s.MaChiNhanh = @MaCN   -- RÀNG BUỘC 1
                  AND s.MaLoaiSan = @MaLoai  -- RÀNG BUỘC 2
                  AND s.TrangThai = 1        -- Chỉ lấy sân đang kinh doanh";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        // Truyền đủ 3 tham số ràng buộc
                        cmd.Parameters.AddWithValue("@MaCN", maChiNhanh);
                        cmd.Parameters.AddWithValue("@MaLoai", maLoaiSan);
                        cmd.Parameters.AddWithValue("@NgayXem", ngayXem.ToString("yyyy-MM-dd"));

                        SqlDataReader reader = cmd.ExecuteReader();
                        int count = 0;

                        while (reader.Read())
                        {
                            count++;
                            ucSanBong item = new ucSanBong();

                            // Lấy dữ liệu
                            int id = int.Parse(reader["MaSan"].ToString());
                            string ten = reader["TenSan"].ToString();
                            decimal gia = decimal.Parse(reader["GiaMacDinh"].ToString());

                            string trangThaiCoDinh = reader["TrangThaiCoDinh"].ToString();
                            int soLuotDat = int.Parse(reader["SoLuotDat"].ToString());

                            // --- XỬ LÝ MÀU SẮC (THEO YÊU CẦU CỦA BẠN) ---
                            string trangThaiHienThi = "";

                            // Ưu tiên 1: Nếu CSDL ghi là Bảo Trì (Sân hỏng) -> Luôn hiện Bảo Trì
                            if (trangThaiCoDinh == "Bảo trì")
                            {
                                trangThaiHienThi = "Bảo trì";
                            }
                            // Ưu tiên 2: Nếu tìm thấy trong bảng Lịch Đặt Sân (SoLuotDat > 0) -> Hiện Đã đặt
                            else if (soLuotDat > 0)
                            {
                                trangThaiHienThi = "Đã đặt";
                            }
                            // Ưu tiên 3: Không hỏng, Không ai đặt -> Hiện TRỐNG (Cho phép đặt)
                            else
                            {
                                trangThaiHienThi = "Trống";
                            }

                            // Đổ dữ liệu lên giao diện
                            item.ThietLapThongTin(id, ten, trangThaiHienThi, gia);
                            item.OnSelect += Item_OnSelect;
                            flpDanhSachSan.Controls.Add(item);
                        }

                        // Nếu không có sân nào thuộc Chi Nhánh + Loại Sân này
                        if (count == 0)
                        {
                            MessageBox.Show("Không có sân nào thỏa mãn yêu cầu (Sai chi nhánh hoặc loại sân)!", "Thông báo");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tải sân: " + ex.Message);
                    }
                }
            }

            // Sự kiện khi bấm vào 1 cái sân
            // Sự kiện: Khi bấm vào 1 cái sân bất kỳ ở giữa màn hình
            // Sự kiện: Khi bấm vào 1 cái sân bất kỳ
            private void Item_OnSelect(object sender, EventArgs e)
            {
                // 1. Lấy đối tượng sân vừa được click
                ucSanBong sanDuocChon = sender as ucSanBong;

                if (sanDuocChon != null)
                {
                    // ==========================================================
                    // KHU VỰC KIỂM TRA (VALIDATION) - CHẶN CLICK
                    // ==========================================================

                    // TRƯỜNG HỢP 1: Sân đang bảo trì (Màu xám)
                    if (sanDuocChon.TrangThai == "Bảo trì")
                    {
                        MessageBox.Show("Sân này đang bảo trì/sửa chữa.\nVui lòng chọn sân khác!",
                                        "Cảnh báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        // Lệnh return quan trọng: Dừng hàm ngay lập tức tại đây.
                        // Code bên dưới sẽ không chạy -> Thông tin sân không hiện sang phải -> Không đặt được.
                        return;
                    }

                    // TRƯỜNG HỢP 2: Sân đã có người đặt (Màu đỏ)
                    if (sanDuocChon.TrangThai == "Đã đặt")
                    {
                        MessageBox.Show("Sân này đã có lịch đặt trong ngày này rồi.\nBạn hãy chọn giờ khác hoặc sân khác nhé!",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        // Dừng ngay lập tức
                        return;
                    }

                    // ==========================================================
                    // KHU VỰC XỬ LÝ KHI SÂN HỢP LỆ (MÀU XANH)
                    // ==========================================================

                    // Nếu code chạy xuống được đến đây, nghĩa là sân "Trống".
                    // Ta bắt đầu đổ dữ liệu sang Panel bên phải.

                    // 1. Hiện tên và giá lên Panel phải
                    lblTenSanChon.Text = sanDuocChon.TenSan;
                    lblGiaGoc.Text = sanDuocChon.GiaMacDinh.ToString("N0") + " đ";
                    lblMaSan.Text = sanDuocChon.MaSan.ToString(); // Lưu ID ngầm

                    // 2. Cập nhật biến toàn cục (để nút Đặt Sân dùng)
                    _maSanDangChon = sanDuocChon.MaSan;
                    _giaSanHienTai = sanDuocChon.GiaMacDinh;

                    // 3. Reset lại giờ giấc (Giúp người dùng đỡ phải chỉnh nhiều)
                    dtpGioBatDau.Value = DateTime.Now;
                    dtpGioKetThuc.Value = DateTime.Now.AddHours(1); // Mặc định đá 1 tiếng

                    // 4. Tính tiền ngay lập tức để hiện ra
                    TinhTien();

                    // 5. Hiệu ứng nhỏ: Đổi màu chữ tên sân bên phải thành màu Xanh cho đẹp
                    lblTenSanChon.ForeColor = Color.Blue;
                }
            }

            // Hàm tính tiền tự động
            private void TinhTien()
            {
                // Lấy giờ
                DateTime timeBatDau = dtpGioBatDau.Value;
                DateTime timeKetThuc = dtpGioKetThuc.Value;

                // Tính thời lượng (Phút)
                TimeSpan thoiLuong = timeKetThuc - timeBatDau;
                double soGioDa = thoiLuong.TotalHours;

                // Logic: Nếu giờ kết thúc nhỏ hơn giờ bắt đầu -> Tiền = 0
                if (soGioDa <= 0)
                {
                    txtTongTien.Text = "0";
                    return;
                }

                // Tính tiền = Số giờ * Giá sân (Biến toàn cục lấy từ bước trước)
                decimal tongTien = (decimal)soGioDa * _giaSanHienTai;

                // Hiển thị format tiền tệ (300.000)
                txtTongTien.Text = tongTien.ToString("N0");
            }

            // SỰ KIỆN: Khi thay đổi giờ bắt đầu hoặc kết thúc -> Gọi tính tiền
            private void dtpGio_ValueChanged(object sender, EventArgs e)
            {
                TinhTien();
            }

            private void btnTimKH_Click(object sender, EventArgs e)
            {
                string sdt = txtSDT.Text.Trim();
                if (string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        // 1. Tìm xem khách có trong CSDL chưa
                        string sql = "SELECT MaKH, HoTen FROM KhachHang WHERE DienThoai = @SDT";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@SDT", sdt);

                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            // -- TRƯỜNG HỢP 1: TÌM THẤY --
                            txtTenKhach.Text = r["HoTen"].ToString();
                            lblMaKH.Text = r["MaKH"].ToString(); // Lưu ID lại để lát đặt sân
                            r.Close();
                        }
                        else
                        {
                            // -- TRƯỜNG HỢP 2: KHÔNG TÌM THẤY --
                            r.Close();
                            DialogResult kq = MessageBox.Show("Khách hàng này chưa có. Bạn có muốn THÊM MỚI nhanh không?",
                                                              "Không tìm thấy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (kq == DialogResult.Yes)
                            {
                                // Nhập tên nhanh bằng InputBox (hoặc mặc định là Khách Vãng Lai)
                                string tenKhachMoi = "Khách vãng lai (" + sdt + ")";

                                // Insert nhanh vào CSDL
                                string sqlInsert = "INSERT INTO KhachHang(HoTen, DienThoai) VALUES (@HoTen, @SDT); SELECT SCOPE_IDENTITY();";
                                SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                                cmdInsert.Parameters.AddWithValue("@HoTen", tenKhachMoi);
                                cmdInsert.Parameters.AddWithValue("@SDT", sdt);

                                // Lấy ID vừa tạo
                                object newId = cmdInsert.ExecuteScalar();

                                // Hiển thị lên form
                                txtTenKhach.Text = tenKhachMoi;
                                lblMaKH.Text = newId.ToString();
                                MessageBox.Show("Đã thêm khách hàng mới thành công!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tìm khách: " + ex.Message);
                    }
                }
            }



            private void btnDatSan_Click(object sender, EventArgs e)
            {
                // =================================================================================
                // BƯỚC 1: KIỂM TRA DỮ LIỆU ĐẦU VÀO (VALIDATION)
                // =================================================================================

                // 1. Kiểm tra đã chọn sân chưa
                if (_maSanDangChon == 0)
                {
                    MessageBox.Show("Vui lòng chọn một sân bóng trước khi đặt!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Kiểm tra đã chọn khách hàng chưa
                if (string.IsNullOrEmpty(lblMaKH.Text) || lblMaKH.Text == "0")
                {
                    MessageBox.Show("Vui lòng tìm và chọn khách hàng trước!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Lấy dữ liệu Ngày và Giờ từ giao diện
                DateTime ngayDat = dtpNgayXem.Value.Date; // Lấy ngày (bỏ phần giờ)
                TimeSpan gioBatDau = dtpGioBatDau.Value.TimeOfDay; // Lấy giờ bắt đầu (HH:mm)
                TimeSpan gioKetThuc = dtpGioKetThuc.Value.TimeOfDay; // Lấy giờ kết thúc (HH:mm)

                // 4. Kiểm tra logic giờ giấc
                if (gioKetThuc <= gioBatDau)
                {
                    MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // (Tùy chọn) Kiểm tra thời lượng tối thiểu 30 phút
                if ((gioKetThuc - gioBatDau).TotalMinutes < 30)
                {
                    MessageBox.Show("Thời gian đá tối thiểu phải là 30 phút!", "Lỗi thời gian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // =================================================================================
                // BƯỚC 2: KẾT NỐI CSDL ĐỂ KIỂM TRA TRÙNG VÀ LƯU
                // =================================================================================
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();

                        // --- A. KIỂM TRA TRÙNG LỊCH (QUAN TRỌNG NHẤT) ---
                        // Logic: Tìm xem có đơn đặt nào THỎA MÃN TẤT CẢ điều kiện sau không:
                        // 1. Cùng Mã Sân (MaSan đã đại diện cho Chi Nhánh rồi)
                        // 2. Cùng Ngày Đặt
                        // 3. Trạng thái không phải là "Đã Hủy"
                        // 4. Khung giờ bị CHỒNG LẤN nhau (Giao nhau)

                        string sqlCheck = @"
                SELECT COUNT(*) 
                FROM LichDatSan 
                WHERE MaSan = @MaSan 
                  AND NgayDat = @NgayDat 
                  AND TrangThai != 'DA_HUY' 
                  AND (
                      -- Công thức kiểm tra 2 khoảng thời gian giao nhau:
                      -- (Giờ Bắt Đầu Mới < Giờ Kết Thúc Cũ) VÀ (Giờ Kết Thúc Mới > Giờ Bắt Đầu Cũ)
                      (@GioBatDau < GioKetThuc) AND (@GioKetThuc > GioBatDau)
                  )";

                        SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn);
                        cmdCheck.Parameters.AddWithValue("@MaSan", _maSanDangChon);
                        cmdCheck.Parameters.AddWithValue("@NgayDat", ngayDat);
                        cmdCheck.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                        cmdCheck.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);

                        // Thực thi kiểm tra
                        int soLuongTrung = (int)cmdCheck.ExecuteScalar();

                        // NẾU TÌM THẤY TRÙNG -> BÁO LỖI VÀ DỪNG NGAY
                        if (soLuongTrung > 0)
                        {
                            MessageBox.Show("Rất tiếc! Sân này ĐÃ CÓ NGƯỜI ĐẶT trong khung giờ bạn chọn.\n\n" +
                                            "Vui lòng:\n" +
                                            "- Kiểm tra lại lịch sân (các ô màu đỏ).\n" +
                                            "- Hoặc chọn khung giờ khác.",
                                            "Trùng lịch đặt sân", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return; // Thoát khỏi hàm, không lưu gì cả
                        }
                        // --- B. NẾU KHÔNG TRÙNG -> TIẾN HÀNH LƯU (INSERT) ---
                        string sqlInsert = @"
                INSERT INTO LichDatSan (MaKH, MaSan, NgayDat, GioBatDau, GioKetThuc, SoGio, TongTienSan, TrangThai, GhiChu)
                VALUES (@MaKH, @MaSan, @NgayDat, @GioBatDau, @GioKetThuc, @SoGio, @TongTien, N'DA_XAC_NHAN', N'Đặt tại quầy')";

                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);

                        // Truyền tham số
                        cmdInsert.Parameters.AddWithValue("@MaKH", int.Parse(lblMaKH.Text));
                        cmdInsert.Parameters.AddWithValue("@MaSan", _maSanDangChon);
                        cmdInsert.Parameters.AddWithValue("@NgayDat", ngayDat);
                        cmdInsert.Parameters.AddWithValue("@GioBatDau", gioBatDau);
                        cmdInsert.Parameters.AddWithValue("@GioKetThuc", gioKetThuc);

                        // Tính số giờ
                        double soGio = (gioKetThuc - gioBatDau).TotalHours;
                        cmdInsert.Parameters.AddWithValue("@SoGio", soGio);

                        // Xử lý tiền (Xóa các ký tự thừa để lấy số)
                        decimal tongTien = 0;
                        string tienSach = txtTongTien.Text.Replace(".", "").Replace(",", "").Replace(" đ", "").Trim();
                        decimal.TryParse(tienSach, out tongTien);
                        cmdInsert.Parameters.AddWithValue("@TongTien", tongTien);

                        // Thực thi lệnh Insert
                        int ketQua = cmdInsert.ExecuteNonQuery();

                        if (ketQua > 0)
                        {
                            MessageBox.Show("Đặt sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // --- C. CẬP NHẬT GIAO DIỆN ---
                            // Gọi lại hàm Filter để nó vẽ lại sân (Sân vừa đặt sẽ chuyển màu Đỏ)
                            Filter_Changed(null, null);

                            // Xóa trắng form nhập liệu để chuẩn bị cho đơn sau
                            ResetFormDatSan();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Hàm phụ để xóa trắng form sau khi đặt xong (cho gọn code)
            private void ResetFormDatSan()
            {
                txtSDT.Clear();
                txtTenKhach.Clear();
                lblMaKH.Text = "0";
                txtTongTien.Text = "0";
                // Có thể reset giờ về mặc định nếu muốn
            }
        }
    }