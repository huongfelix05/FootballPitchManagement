using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FootballPitchManagement.Common;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmQuanLyDatSan : Form
    {
        DataTable dtDuLieuGoc = new DataTable();

        public frmQuanLyDatSan()
        {
            InitializeComponent();
        }

        private void frmQuanLyDatSan_Load(object sender, EventArgs e)
        {
            try
            {
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    this.Close();
                    return;
                }

                CaiDatGiaoDien();
                LayDuLieuTuDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- PHẦN 1: CẤU HÌNH GIAO DIỆN ---
        void CaiDatGiaoDien()
        {
            // ✅ CHỈ GÁN 1 LẦN (XÓA 13 DÒNG TRÙNG LẶP)
            dgvDanhSach.CellFormatting += dgvDanhSach_CellFormatting;
            dgvDanhSach.CellContentClick += dgvDanhSach_CellContentClick;

            // --- 1. Cấu hình ComboBox Trạng thái ---
            DataTable dtTrangThai = new DataTable();
            dtTrangThai.Columns.Add("Ma", typeof(string));
            dtTrangThai.Columns.Add("Ten", typeof(string));

            dtTrangThai.Rows.Add("ALL", "Tất cả");
            dtTrangThai.Rows.Add("DA_XAC_NHAN", "Đã xác nhận");
            dtTrangThai.Rows.Add("HOAN_THANH", "Hoàn thành");
            dtTrangThai.Rows.Add("DA_HUY", "Đã hủy");

            cboTrangThai.DataSource = dtTrangThai;
            cboTrangThai.DisplayMember = "Ten";
            cboTrangThai.ValueMember = "Ma";
            cboTrangThai.SelectedIndex = 0;

            // --- 2. Cấu hình Ngày tháng ---
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;

            // --- 3. Gán sự kiện lọc ---
            txtTimKiem.TextChanged += SuKienLocDuLieu;
            cboTrangThai.SelectedIndexChanged += SuKienLocDuLieu;
            dtpTuNgay.ValueChanged += SuKienLocDuLieu;
            dtpDenNgay.ValueChanged += SuKienLocDuLieu;
        }

        // --- PHẦN 2: LẤY DỮ LIỆU THÔ TỪ SQL ---
        void LayDuLieuTuDatabase()
        {
            try
            {
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

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    {
                        dtDuLieuGoc = new DataTable();
                        da.Fill(dtDuLieuGoc);
                    }
                }

                XuLyLocVaHienThi();
                TinhToanThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy dữ liệu: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- PHẦN 3: XỬ LÝ LỌC TRONG CODE ---
        void XuLyLocVaHienThi()
        {
            if (dtDuLieuGoc == null || dtDuLieuGoc.Rows.Count == 0)
            {
                dgvDanhSach.DataSource = null;
                return;
            }

            try
            {
                string tuKhoa = txtTimKiem.Text.ToLower().Trim();
                string trangThaiChon = cboTrangThai.SelectedValue?.ToString() ?? "ALL";
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                var ketQuaLoc = dtDuLieuGoc.AsEnumerable().Where(row =>
                {
                    DateTime ngayDat = row.Field<DateTime>("NgayDat").Date;
                    bool checkNgay = ngayDat >= tuNgay && ngayDat <= denNgay;

                    string trangThaiDB = row.Field<string>("TrangThai");
                    bool checkTrangThai = (trangThaiChon == "ALL") || (trangThaiDB == trangThaiChon);

                    string tenKhach = row.Field<string>("HoTen").ToLower();
                    string sdt = row.Field<string>("DienThoai");
                    bool checkTuKhoa = string.IsNullOrEmpty(tuKhoa) ||
                                       tenKhach.Contains(tuKhoa) ||
                                       sdt.Contains(tuKhoa);

                    return checkNgay && checkTrangThai && checkTuKhoa;
                });

                if (ketQuaLoc.Any())
                {
                    dgvDanhSach.DataSource = ketQuaLoc.CopyToDataTable();
                }
                else
                {
                    DataTable dtTrong = dtDuLieuGoc.Clone();
                    dgvDanhSach.DataSource = dtTrong;
                }

                CauHinhCotGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lọc dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void TinhToanThongKe()
        {
            if (dtDuLieuGoc == null || dtDuLieuGoc.Rows.Count == 0)
            {
                lblTongDon.Text = "0";
                lblDaHuy.Text = "0";
                lblDaXacNhan.Text = "0";
                lblDaThanhToan.Text = "0";
                return;
            }

            lblTongDon.Text = dtDuLieuGoc.Rows.Count.ToString();
            lblDaHuy.Text = dtDuLieuGoc.Select("TrangThai = 'DA_HUY'").Length.ToString();

            int countHoanThanh = dtDuLieuGoc.Select("TrangThai = 'HOAN_THANH'").Length;
            int countDaXacNhan = dtDuLieuGoc.Select("TrangThai = 'DA_XAC_NHAN'").Length;
            lblDaXacNhan.Text = (countHoanThanh + countDaXacNhan).ToString();

            try
            {
                if (dtDuLieuGoc.Columns.Contains("TrangThaiThanhToan"))
                {
                    int countDaTT = dtDuLieuGoc.Select("TrangThaiThanhToan = 'DA_THANH_TOAN'").Length;
                    lblDaThanhToan.Text = countDaTT.ToString();
                }
                else
                {
                    lblDaThanhToan.Text = "0";
                }
            }
            catch
            {
                lblDaThanhToan.Text = "0";
            }
        }

        private void SuKienLocDuLieu(object sender, EventArgs e)
        {
            XuLyLocVaHienThi();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            XuLyLocVaHienThi();
        }

        // --- PHẦN 4: FORMAT GIAO DIỆN ---
        void CauHinhCotGrid()
        {
            if (dgvDanhSach.Columns.Count == 0) return;

            try
            {
                // Ẩn các cột gốc
                if (dgvDanhSach.Columns["MaDatSan"] != null) 
                    dgvDanhSach.Columns["MaDatSan"].Visible = false;
                if (dgvDanhSach.Columns["GioBatDau"] != null) 
                    dgvDanhSach.Columns["GioBatDau"].Visible = false;
                if (dgvDanhSach.Columns["GioKetThuc"] != null) 
                    dgvDanhSach.Columns["GioKetThuc"].Visible = false;

                // Thêm cột Mã Đơn
                if (dgvDanhSach.Columns["MaHienThi"] == null)
                {
                    DataGridViewTextBoxColumn colMa = new DataGridViewTextBoxColumn
                    {
                        Name = "MaHienThi",
                        HeaderText = "Mã Đơn",
                        Width = 80
                    };
                    dgvDanhSach.Columns.Insert(0, colMa);
                }

                // Thêm cột Khung Giờ
                if (dgvDanhSach.Columns["KhungGioHienThi"] == null)
                {
                    DataGridViewTextBoxColumn colGio = new DataGridViewTextBoxColumn
                    {
                        Name = "KhungGioHienThi",
                        HeaderText = "Khung Giờ",
                        Width = 120
                    };
                    
                    // Tìm vị trí cột TenSan để insert sau đó
                    int viTriInsert = 4;
                    for (int i = 0; i < dgvDanhSach.Columns.Count; i++)
                    {
                        if (dgvDanhSach.Columns[i].Name == "TenSan")
                        {
                            viTriInsert = i + 1;
                            break;
                        }
                    }
                    dgvDanhSach.Columns.Insert(viTriInsert, colGio);
                }

                // Đổi tên cột
                if (dgvDanhSach.Columns["HoTen"] != null) 
                    dgvDanhSach.Columns["HoTen"].HeaderText = "Khách Hàng";
                if (dgvDanhSach.Columns["DienThoai"] != null) 
                    dgvDanhSach.Columns["DienThoai"].HeaderText = "SĐT";
                if (dgvDanhSach.Columns["TenSan"] != null) 
                    dgvDanhSach.Columns["TenSan"].HeaderText = "Sân";
                if (dgvDanhSach.Columns["NgayDat"] != null) 
                {
                    dgvDanhSach.Columns["NgayDat"].HeaderText = "Ngày Đặt";
                    dgvDanhSach.Columns["NgayDat"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvDanhSach.Columns["TrangThai"] != null) 
                    dgvDanhSach.Columns["TrangThai"].HeaderText = "Trạng Thái";
                if (dgvDanhSach.Columns["TrangThaiThanhToan"] != null) 
                    dgvDanhSach.Columns["TrangThaiThanhToan"].HeaderText = "Thanh Toán";

                // Format tiền
                if (dgvDanhSach.Columns["TongTienSan"] != null)
                {
                    dgvDanhSach.Columns["TongTienSan"].HeaderText = "Tổng Tiền";
                    dgvDanhSach.Columns["TongTienSan"].DefaultCellStyle.Format = "N0";
                    dgvDanhSach.Columns["TongTienSan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Thêm nút Xóa
                if (dgvDanhSach.Columns["btnXoa"] == null)
                {
                    DataGridViewButtonColumn btnXoa = new DataGridViewButtonColumn
                    {
                        Name = "btnXoa",
                        HeaderText = "Thao tác",
                        Text = "Xóa",
                        UseColumnTextForButtonValue = true,
                        Width = 80
                    };
                    dgvDanhSach.Columns.Add(btnXoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cấu hình cột: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDanhSach_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                string colName = dgvDanhSach.Columns[e.ColumnIndex].Name;

                // 1. Format Mã Đơn
                if (colName == "MaHienThi")
                {
                    var giaTriGoc = dgvDanhSach.Rows[e.RowIndex].Cells["MaDatSan"].Value;
                    if (giaTriGoc != null && giaTriGoc != DBNull.Value)
                    {
                        e.Value = $"D{giaTriGoc}";
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        e.FormattingApplied = true;
                    }
                }

                // 2. Format Khung Giờ
                if (colName == "KhungGioHienThi")
                {
                    object valBD = dgvDanhSach.Rows[e.RowIndex].Cells["GioBatDau"].Value;
                    object valKT = dgvDanhSach.Rows[e.RowIndex].Cells["GioKetThuc"].Value;

                    if (valBD != null && valKT != null && valBD != DBNull.Value && valKT != DBNull.Value)
                    {
                        string strBD = valBD.ToString();
                        string strKT = valKT.ToString();

                        if (strBD.Length >= 5) strBD = strBD.Substring(0, 5);
                        if (strKT.Length >= 5) strKT = strKT.Substring(0, 5);

                        e.Value = $"{strBD} - {strKT}";
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        e.FormattingApplied = true;
                    }
                }

                // 3. Format Trạng Thái
                if (colName == "TrangThai")
                {
                    string status = e.Value?.ToString();
                    switch (status)
                    {
                        case "HOAN_THANH":
                            e.Value = "Hoàn thành";
                            e.CellStyle.ForeColor = Color.Blue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
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
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Strikeout);
                            break;
                    }
                    e.FormattingApplied = true;
                }

                // 4. Format Trạng Thái Thanh Toán
                if (colName == "TrangThaiThanhToan")
                {
                    string tt = e.Value?.ToString();
                    if (tt != null && tt.Contains("DA_THANH"))
                    {
                        e.Value = "✓ Đã thanh toán";
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                    else
                    {
                        e.Value = "✗ Chưa thanh toán";
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi format cell: {ex.Message}");
            }
        }

        bool XoaDonDatSanTrongSQL(int maDatSan)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "DELETE FROM LichDatSan WHERE MaDatSan = @MaDatSan";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDatSan", maDatSan);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "Không thể xóa đơn này vì đã có Hóa Đơn hoặc dữ liệu liên quan.\n\nPhải xóa hóa đơn trước!", 
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && 
                dgvDanhSach.Columns[e.ColumnIndex].Name == "btnXoa")
            {
                try
                {
                    DialogResult hoi = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa đơn đặt sân này không?\n\nHành động này không thể hoàn tác!", 
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (hoi == DialogResult.Yes)
                    {
                        int maDatSan = Convert.ToInt32(dgvDanhSach.Rows[e.RowIndex].Cells["MaDatSan"].Value);

                        if (XoaDonDatSanTrongSQL(maDatSan))
                        {
                            DataRow[] dongCanXoa = dtDuLieuGoc.Select($"MaDatSan = {maDatSan}");
                            if (dongCanXoa.Length > 0)
                            {
                                dtDuLieuGoc.Rows.Remove(dongCanXoa[0]);
                            }

                            XuLyLocVaHienThi();
                            TinhToanThongKe();

                            MessageBox.Show("Đã xóa đơn thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Event handlers rỗng
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e) { }
        private void groupBox4_Enter(object sender, EventArgs e) { }
    }
}