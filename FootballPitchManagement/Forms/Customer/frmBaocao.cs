using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Dùng để chỉnh màu
using System.Windows.Forms;
// Nhớ Add Reference: Microsoft.Office.Interop.Excel trước khi dùng
using Excel = Microsoft.Office.Interop.Excel;

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmBaocao : Form
    {
        // --- KHAI BÁO BIẾN TOÀN CỤC Ở ĐÂY (SỬA LỖI) ---
        string connStr = @"Data Source=MSI;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // Đưa 2 dòng này ra ngoài hàm frmBaocao()
        DataTable dtSanTotNhat = new DataTable();
        DataTable dtKhungGio = new DataTable();

        public frmBaocao()
        {
            InitializeComponent();
            // Không khai báo lại ở đây nữa
        }

        private void FrmBaoCao_Load(object sender, EventArgs e)
        {
            // Thiết lập ngày mặc định (Từ đầu tháng này đến hiện tại)
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Tải danh sách chi nhánh
            LoadComboBoxChiNhanh();

            // Làm đẹp giao diện GridView trước
            LamDepGiaoDien();

            // Tải dữ liệu báo cáo lần đầu
            HienThiKetQua();
        }

        // --- HÀM 1: LOAD COMBOBOX CHI NHÁNH ---
        private void LoadComboBoxChiNhanh()
        {
            string sql = @"
                SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh 
                UNION ALL 
                SELECT -1, N'-- Tất cả chi nhánh --'
                ORDER BY MaChiNhanh";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboChiNhanh.DataSource = dt;
                    cboChiNhanh.DisplayMember = "TenChiNhanh";
                    cboChiNhanh.ValueMember = "MaChiNhanh";
                    cboChiNhanh.SelectedValue = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải chi nhánh: " + ex.Message);
                }
            }
        }

        // --- SỰ KIỆN NÚT TÌM ---
        private void btntim_Click(object sender, EventArgs e)
        {
            HienThiKetQua();
        }

        // --- SỰ KIỆN NÚT TẠO BÁO CÁO ---
        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            HienThiKetQua(); // Tải lại dữ liệu mới nhất
            XuatRaExcel();   // Xuất file
        }

        // --- HÀM DÙNG CHUNG: CHỨA TOÀN BỘ LOGIC LẤY DỮ LIỆU ---
        private void HienThiKetQua()
        {
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;
            int maChiNhanh = -1;

            if (cboChiNhanh.SelectedValue != null)
            {
                int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maChiNhanh);
            }

            string sqlQuery = @"
                -- [QUERY 1] Bảng Sân Hoạt Động Tốt Nhất
                DECLARE @TongDoanhThu DECIMAL(18,2);
                
                SELECT @TongDoanhThu = SUM(TongTienSan) 
                FROM LichDatSan lds JOIN San s ON lds.MaSan = s.MaSan
                WHERE lds.NgayDat BETWEEN @TuNgay AND @DenNgay
                AND (@MaChiNhanh = -1 OR s.MaChiNhanh = @MaChiNhanh);

                SELECT TOP 10
                    s.TenSan,
                    COUNT(lds.MaDatSan) AS [SoDon],
                    CAST(SUM(lds.TongTienSan) AS DECIMAL(18,0)) AS [DoanhThu],
                    CASE 
                        WHEN @TongDoanhThu > 0 THEN CAST((SUM(lds.TongTienSan) * 100.0 / @TongDoanhThu) AS DECIMAL(5,1)) 
                        ELSE 0 
                    END AS [TyLe]
                FROM LichDatSan lds JOIN San s ON lds.MaSan = s.MaSan
                WHERE lds.NgayDat BETWEEN @TuNgay AND @DenNgay
                  AND (@MaChiNhanh = -1 OR s.MaChiNhanh = @MaChiNhanh)
                GROUP BY s.TenSan ORDER BY [DoanhThu] DESC;

                -- [QUERY 2] Biểu Đồ Khung Giờ (Thống kê số lượng)
                SELECT 
                    CAST(DATEPART(HOUR, GioBatDau) AS VARCHAR) + ':00' AS Gio,
                    COUNT(*) AS SoLuong
                FROM LichDatSan lds JOIN San s ON lds.MaSan = s.MaSan
                WHERE lds.NgayDat BETWEEN @TuNgay AND @DenNgay
                  AND (@MaChiNhanh = -1 OR s.MaChiNhanh = @MaChiNhanh)
                GROUP BY DATEPART(HOUR, GioBatDau) ORDER BY DATEPART(HOUR, GioBatDau) ASC;
            ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);
                    cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    // LƯU DỮ LIỆU VÀO BIẾN TOÀN CỤC (Giờ code đã hiểu biến này)
                    if (ds.Tables.Count > 0) dtSanTotNhat = ds.Tables[0];
                    if (ds.Tables.Count > 1) dtKhungGio = ds.Tables[1];

                    // Cập nhật GridView 
                    if (ds.Tables.Count > 0)
                    {
                        dgvSanTotNhat.DataSource = ds.Tables[0];
                        FormatDataGridView();
                    }

                    // Cập nhật Biểu đồ (FlowLayoutPanel)
                    if (ds.Tables.Count > 1)
                    {
                        VeBieuDo(ds.Tables[1]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // --- HÀM XUẤT EXCEL ---
        // --- HÀM XUẤT EXCEL (ĐÃ CẬP NHẬT THÊM CHI NHÁNH) ---
        private void XuatRaExcel()
        {
            // Kiểm tra dữ liệu
            if (dtSanTotNhat.Rows.Count == 0 && dtKhungGio.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
                excelApp.Visible = true;

                // --- 1. TIÊU ĐỀ CHUNG ---
                Excel.Range head = worksheet.get_Range("A1", "E1");
                head.Merge();
                head.Value2 = "BÁO CÁO HOẠT ĐỘNG SÂN BÓNG";
                head.Font.Bold = true;
                head.Font.Size = 18;
                head.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                Excel.Range dateInfo = worksheet.get_Range("A2", "E2");
                dateInfo.Merge();
                dateInfo.Value2 = $"Từ ngày: {dtpTuNgay.Value.ToString("dd/MM/yyyy")} - Đến ngày: {dtpDenNgay.Value.ToString("dd/MM/yyyy")}";
                dateInfo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                Excel.Range branchInfo = worksheet.get_Range("A3", "E3");
                branchInfo.Merge();
                branchInfo.Value2 = $"Phạm vi báo cáo: {cboChiNhanh.Text}";
                branchInfo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                branchInfo.Font.Italic = true;

                int rowStart = 5;

                // --- 2. XUẤT BẢNG 1: SÂN TỐT NHẤT ---
                worksheet.Cells[rowStart, 1] = "I. DANH SÁCH SÂN HOẠT ĐỘNG TỐT NHẤT";
                worksheet.get_Range($"A{rowStart}").Font.Bold = true;
                rowStart++;

                // Header bảng 1
                worksheet.Cells[rowStart, 1] = "Tên Sân";
                worksheet.Cells[rowStart, 2] = "Số Lượt";
                worksheet.Cells[rowStart, 3] = "Doanh Thu";
                worksheet.Cells[rowStart, 4] = "Tỷ Lệ (%)";

                Excel.Range headerRange1 = worksheet.get_Range($"A{rowStart}", $"D{rowStart}");
                headerRange1.Font.Bold = true;
                headerRange1.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                headerRange1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                rowStart++;

                // Dữ liệu bảng 1
                int table1StartRow = rowStart;
                foreach (DataRow row in dtSanTotNhat.Rows)
                {
                    worksheet.Cells[rowStart, 1] = row["TenSan"];
                    worksheet.Cells[rowStart, 2] = row["SoDon"];
                    worksheet.Cells[rowStart, 3] = row["DoanhThu"];
                    worksheet.Cells[rowStart, 4] = row["TyLe"];
                    rowStart++;
                }
                // Kẻ khung bảng 1
                if (rowStart > table1StartRow)
                {
                    Excel.Range tbl1 = worksheet.get_Range($"A{table1StartRow}", $"D{rowStart - 1}");
                    tbl1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                rowStart += 2; // Cách dòng

                // --- 3. XUẤT BẢNG 2: KHUNG GIỜ CAO ĐIỂM (CẬP NHẬT THÊM TỶ LỆ) ---
                worksheet.Cells[rowStart, 1] = "II. THỐNG KÊ KHUNG GIỜ CAO ĐIỂM";
                worksheet.get_Range($"A{rowStart}").Font.Bold = true;
                rowStart++;

                // Header bảng 2 (Thêm cột C)
                worksheet.Cells[rowStart, 1] = "Khung Giờ";
                worksheet.Cells[rowStart, 2] = "Số Lượng Đặt";
                worksheet.Cells[rowStart, 3] = "Tỷ Lệ (%)"; // <--- Cột mới

                Excel.Range headerRange2 = worksheet.get_Range($"A{rowStart}", $"C{rowStart}"); // Mở rộng ra cột C
                headerRange2.Font.Bold = true;
                headerRange2.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);
                headerRange2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                rowStart++;

                // Tính tổng số lượng để chia phần trăm
                int tongSoLuong = 0;
                foreach (DataRow row in dtKhungGio.Rows)
                {
                    if (row["SoLuong"] != DBNull.Value)
                        tongSoLuong += Convert.ToInt32(row["SoLuong"]);
                }

                int table2Start = rowStart;
                foreach (DataRow row in dtKhungGio.Rows)
                {
                    // Xử lý giờ
                    string gio = row["Gio"].ToString();
                    string gioHienThi = gio;
                    try
                    {
                        int g = int.Parse(gio.Split(':')[0]);
                        gioHienThi = g.ToString("00") + ":00 - " + (g + 1).ToString("00") + ":00";
                    }
                    catch { }

                    // Lấy số lượng
                    int soLuong = Convert.ToInt32(row["SoLuong"]);

                    // Tính tỷ lệ % trên tổng số
                    double tyLe = 0;
                    if (tongSoLuong > 0)
                        tyLe = (double)soLuong / tongSoLuong * 100;

                    // Ghi vào Excel
                    worksheet.Cells[rowStart, 1] = gioHienThi;
                    worksheet.Cells[rowStart, 2] = soLuong;
                    worksheet.Cells[rowStart, 3] = Math.Round(tyLe, 1); // <--- Ghi tỷ lệ làm tròn 1 số lẻ

                    rowStart++;
                }

                // Kẻ khung bảng 2 (Mở rộng ra cột C)
                if (rowStart > table2Start)
                {
                    Excel.Range tbl2 = worksheet.get_Range($"A{table2Start}", $"C{rowStart - 1}");
                    tbl2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                worksheet.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        // --- HÀM PHỤ: LÀM ĐẸP DATAGRIDVIEW ---
        private void FormatDataGridView()
        {
            if (dgvSanTotNhat.Columns["TenSan"] != null) dgvSanTotNhat.Columns["TenSan"].HeaderText = "Tên Sân";
            if (dgvSanTotNhat.Columns["SoDon"] != null) dgvSanTotNhat.Columns["SoDon"].HeaderText = "Số Lượt";

            if (dgvSanTotNhat.Columns["DoanhThu"] != null)
            {
                dgvSanTotNhat.Columns["DoanhThu"].HeaderText = "Doanh Thu";
                dgvSanTotNhat.Columns["DoanhThu"].DefaultCellStyle.Format = "#,##0 đ";
                dgvSanTotNhat.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvSanTotNhat.Columns["TyLe"] != null)
            {
                dgvSanTotNhat.Columns["TyLe"].HeaderText = "Tỷ Lệ (%)";
                dgvSanTotNhat.Columns["TyLe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvSanTotNhat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- HÀM PHỤ: VẼ DANH SÁCH GIỜ (THAY THẾ CHART) ---
        private void VeBieuDo(DataTable dtChart)
        {
            if (flpKhungGio == null) return;
            flpKhungGio.Controls.Clear();
            flpKhungGio.FlowDirection = FlowDirection.TopDown;
            flpKhungGio.WrapContents = false;
            flpKhungGio.AutoScroll = true;

            if (dtChart.Rows.Count == 0) return;

            int maxVal = 0;
            foreach (DataRow row in dtChart.Rows)
            {
                int val = Convert.ToInt32(row["SoLuong"]);
                if (val > maxVal) maxVal = val;
            }

            foreach (DataRow row in dtChart.Rows)
            {
                string gio = row["Gio"].ToString();
                int soLuong = Convert.ToInt32(row["SoLuong"]);

                string gioHienThi = gio;
                try
                {
                    int g = int.Parse(gio.Split(':')[0]);
                    gioHienThi = g.ToString("00") + ":00 - " + (g + 1).ToString("00") + ":00";
                }
                catch { }

                Panel pnlItem = new Panel();
                pnlItem.Width = flpKhungGio.ClientSize.Width - 30;
                pnlItem.Height = 90;
                pnlItem.Margin = new Padding(0, 0, 0, 15);
                pnlItem.BorderStyle = BorderStyle.FixedSingle;
                pnlItem.BackColor = Color.White;

                Label lblGio = new Label();
                lblGio.Text = gioHienThi;
                lblGio.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                lblGio.Location = new Point(15, 12);
                lblGio.AutoSize = true;

                Label lblSoDon = new Label();
                lblSoDon.Text = soLuong + " đơn";
                lblSoDon.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                lblSoDon.ForeColor = Color.Gray;
                lblSoDon.AutoSize = true;
                lblSoDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                lblSoDon.Location = new Point(pnlItem.Width - 70, 15);

                Panel pnlBarBack = new Panel();
                pnlBarBack.Size = new Size(pnlItem.Width - 40, 30);
                pnlBarBack.Location = new Point(20, 45);
                pnlBarBack.BackColor = Color.FromArgb(236, 240, 241);
                pnlBarBack.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                Panel pnlBarFront = new Panel();
                float percent = (maxVal > 0) ? (float)soLuong / maxVal : 0;
                int widthFront = (int)(pnlBarBack.Width * percent);
                if (widthFront < 5 && percent > 0) widthFront = 5;

                pnlBarFront.Size = new Size(widthFront, 30);
                pnlBarFront.Location = new Point(20, 45);
                pnlBarFront.BackColor = Color.FromArgb(52, 152, 219);

                Label lblPercent = new Label();
                lblPercent.Text = (percent * 100).ToString("0") + "%";
                lblPercent.ForeColor = Color.White;
                lblPercent.BackColor = Color.Transparent;
                lblPercent.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblPercent.AutoSize = true;
                if (widthFront > 30)
                {
                    lblPercent.Location = new Point((pnlBarFront.Width - 25) / 2, 6);
                    pnlBarFront.Controls.Add(lblPercent);
                }

                pnlItem.Controls.Add(lblGio);
                pnlItem.Controls.Add(lblSoDon);
                pnlItem.Controls.Add(pnlBarFront);
                pnlItem.Controls.Add(pnlBarBack);
                flpKhungGio.Controls.Add(pnlItem);
            }
        }

        // --- SỰ KIỆN VẼ CUSTOM CHO GRIDVIEW (THANH %) ---
        private void dgvSanTotNhat_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                float percentage = 0;
                if (e.Value != null)
                    float.TryParse(e.Value.ToString(), out percentage);

                int width = (int)((e.CellBounds.Width - 4) * (percentage / 100));

                Rectangle rectBack = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 5, e.CellBounds.Width - 4, e.CellBounds.Height - 10);
                using (SolidBrush backBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
                {
                    e.Graphics.FillRectangle(backBrush, rectBack);
                }

                if (width > 0)
                {
                    Rectangle rectProgress = new Rectangle(e.CellBounds.X + 2, e.CellBounds.Y + 5, width, e.CellBounds.Height - 10);
                    using (SolidBrush progBrush = new SolidBrush(Color.FromArgb(46, 204, 113)))
                    {
                        e.Graphics.FillRectangle(progBrush, rectProgress);
                    }
                }

                string text = percentage.ToString("0.0") + "%";
                TextRenderer.DrawText(e.Graphics, text, e.CellStyle.Font, e.CellBounds, Color.Black, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);

                e.Handled = true;
            }
        }

        private void LamDepGiaoDien()
        {
            dgvSanTotNhat.BackgroundColor = Color.White;
            dgvSanTotNhat.BorderStyle = BorderStyle.None;
            dgvSanTotNhat.AllowUserToAddRows = false;
            dgvSanTotNhat.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSanTotNhat.EnableHeadersVisualStyles = false;

            dgvSanTotNhat.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
            dgvSanTotNhat.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvSanTotNhat.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvSanTotNhat.ColumnHeadersHeight = 40;

            dgvSanTotNhat.DefaultCellStyle.SelectionBackColor = Color.AliceBlue;
            dgvSanTotNhat.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSanTotNhat.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvSanTotNhat.RowTemplate.Height = 40;
        }
    }
}