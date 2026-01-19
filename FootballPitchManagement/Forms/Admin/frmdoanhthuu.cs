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
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmdoanhthuu : Form
    {
        string strConnect = @"Data Source=MSI;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        public frmdoanhthuu()
        {
            InitializeComponent();
            // --- THÊM 2 DÒNG NÀY ĐỂ BẮT BUỘC CODE PHẢI CHẠY ---
            this.Load += new EventHandler(frmDoanhThu_Load);
            btnThongKe.Click += new EventHandler(btnThongKe_Click);
        }


        // 2. SỰ KIỆN KHI FORM VỪA MỞ LÊN (LOAD)
        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxChiNhanh(); // Tải danh sách chi nhánh vào ComboBox

                // Mặc định chọn ngày: Từ đầu tháng đến hiện tại
                DateTime now = DateTime.Now;
                dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
                dtpDenNgay.Value = now;

                // Tải dữ liệu báo cáo lần đầu
                LoadDashboardCards();
                LoadMainReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải form: " + ex.Message);
            }
        }



        // 3. SỰ KIỆN KHI BẤM NÚT "LỌC DỮ LIỆU"
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadDashboardCards(); // Tính lại 4 thẻ trên cùng
            LoadMainReport();     // Vẽ lại biểu đồ và bảng
        }




        // =======================================================================
        // CÁC HÀM XỬ LÝ LOGIC (SQL) - KHU VỰC NÀY LÀ "TRÁI TIM" CỦA FORM
        // =======================================================================

        // Hàm 1: Đổ dữ liệu vào ComboBox Chi Nhánh
        private void LoadComboBoxChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                conn.Open();
                string sql = "SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Thêm dòng "Tất cả chi nhánh" lên đầu
                DataRow dr = dt.NewRow();
                dr["MaChiNhanh"] = -1;
                dr["TenChiNhanh"] = "Tất cả chi nhánh";
                dt.Rows.InsertAt(dr, 0);

                cboChiNhanh.DataSource = dt;
                cboChiNhanh.DisplayMember = "TenChiNhanh";
                cboChiNhanh.ValueMember = "MaChiNhanh";
            }
        }
        // Hàm phụ trợ: Tính tổng tiền trong khoảng thời gian và chi nhánh cụ thể
        private decimal GetRevenue(DateTime from, DateTime to, int? maCN)
        {
            decimal total = 0;
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                try
                {
                    conn.Open();
                    // SQL tính tổng: (Tổng tiền Hóa Đơn Sân) + (Tổng tiền Hóa Đơn Đồ Ăn)
                    string sql = @"
                SELECT (
                    ISNULL((SELECT SUM(ThanhTien) FROM HoaDon 
                            WHERE NgayLap BETWEEN @F AND @T 
                            AND TrangThaiThanhToan='DA_THANH_TOAN' 
                            AND (@M IS NULL OR MaChiNhanh=@M)),0) 
                    + 
                    ISNULL((SELECT SUM(TongTien) FROM HoaDonDoAn 
                            WHERE NgayLap BETWEEN @F AND @T 
                            AND TrangThai='DA_THANH_TOAN' 
                            AND (@M IS NULL OR MaChiNhanh=@M)),0)
                )";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Thêm tham số an toàn
                    cmd.Parameters.AddWithValue("@F", from.Date);
                    // Lấy đến giây cuối cùng của ngày kết thúc (23:59:59)
                    cmd.Parameters.AddWithValue("@T", to.Date.AddDays(1).AddSeconds(-1));

                    // Xử lý tham số Chi nhánh (Nếu null thì truyền DBNull)
                    if (maCN.HasValue)
                        cmd.Parameters.AddWithValue("@M", maCN.Value);
                    else
                        cmd.Parameters.AddWithValue("@M", DBNull.Value);

                    // Thực thi và lấy kết quả
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        total = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    // Nếu lỗi thì trả về 0 (để không crash chương trình)
                    total = 0;
                }
            }
            return total;
        }
        // Hàm 2: Tính toán tiền cho 4 thẻ Card (Hôm nay, Tuần này, Tháng này, Tổng)
        private void LoadDashboardCards()
        {
            // 1. Lấy thông tin Chi Nhánh đang chọn
            int? maCN = null;
            if (cboChiNhanh.SelectedValue != null && (int)cboChiNhanh.SelectedValue != -1)
            {
                maCN = (int)cboChiNhanh.SelectedValue;
            }

            DateTime now = DateTime.Now;

            // =============================================================
            // 1. TÍNH DOANH THU HÔM NAY (00:00:00 -> 23:59:59)
            // =============================================================
            DateTime startToday = now.Date;
            DateTime endToday = now.Date.AddDays(1).AddSeconds(-1);
            lblSoTienHomNay.Text = GetRevenue(startToday, endToday, maCN).ToString("N0") + " đ";

            // =============================================================
            // 2. TÍNH DOANH THU TUẦN NÀY (THỨ 2 -> CHỦ NHẬT)
            // =============================================================
            // Xác định ngày Thứ 2 đầu tuần
            // (Nếu hôm nay là Chủ Nhật (0) thì coi là thứ 7 để lùi về 6 ngày)
            int dayOfWeek = (int)now.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7;

            DateTime startOfWeek = now.AddDays(-(dayOfWeek - 1)).Date; // Về 00:00 sáng Thứ 2
            DateTime endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1); // Đến 23:59 tối Chủ Nhật

            lblSoTienTuanNay.Text = GetRevenue(startOfWeek, endOfWeek, maCN).ToString("N0") + " đ";

            // Mẹo: Hiển thị Tooltip để biết đang tính từ ngày nào
            // toolTip1.SetToolTip(lblSoTienTuanNay, $"Tuần: {startOfWeek:dd/MM} - {endOfWeek:dd/MM}");

            // =============================================================
            // 3. TÍNH DOANH THU THÁNG NÀY (NGÀY 1 -> NGÀY CUỐI THÁNG)
            // =============================================================
            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddSeconds(-1); // Ngày cuối cùng của tháng

            lblSoTienThangNay.Text = GetRevenue(startOfMonth, endOfMonth, maCN).ToString("N0") + " đ";

            // =============================================================
            // 4. TỔNG DOANH THU (THEO BỘ LỌC NGƯỜI DÙNG CHỌN)
            // =============================================================
            // Phần này giữ nguyên theo lịch chọn
            DateTime fromFilter = dtpTuNgay.Value.Date;
            DateTime toFilter = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            lblTongDoanhThu.Text = GetRevenue(fromFilter, toFilter, maCN).ToString("N0") + " đ";
        }

        // Hàm 3: Hàm phụ trợ để chạy SQL tính tổng tiền trong khoảng thời gian
        private decimal GetRevenueByDateRange(DateTime fromDate, DateTime toDate, int? maChiNhanh)
        {
            decimal totalRevenue = 0;
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                conn.Open();
                // SQL: Cộng tổng tiền Sân (HoaDon) + Tiền Đồ Ăn (HoaDonDoAn)
                string sql = @"
                    SELECT 
                        (
                            ISNULL((SELECT SUM(ThanhTien) FROM HoaDon 
                                    WHERE CAST(NgayLap AS DATE) BETWEEN @From AND @To 
                                    AND (@MaCN IS NULL OR MaChiNhanh = @MaCN)
                                    AND TrangThaiThanhToan = 'DA_THANH_TOAN'), 0) 
                            +
                            ISNULL((SELECT SUM(TongTien) FROM HoaDonDoAn 
                                    WHERE CAST(NgayLap AS DATE) BETWEEN @From AND @To 
                                    AND (@MaCN IS NULL OR MaChiNhanh = @MaCN)
                                    AND TrangThai = 'DA_THANH_TOAN'), 0)
                        )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@From", fromDate.Date);
                    cmd.Parameters.AddWithValue("@To", toDate.Date);
                    if (maChiNhanh.HasValue)
                        cmd.Parameters.AddWithValue("@MaCN", maChiNhanh.Value);
                    else
                        cmd.Parameters.AddWithValue("@MaCN", DBNull.Value);

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value) totalRevenue = Convert.ToDecimal(result);
                }
            }
            return totalRevenue;

        }

        // Hàm 4: Lấy dữ liệu chi tiết để vẽ Biểu đồ và Bảng
        private void LoadMainReport()
        {
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                conn.Open();

                // LẤY NGÀY TỪ BỘ LỌC (QUAN TRỌNG)
                DateTime fromDate = dtpTuNgay.Value.Date;
                DateTime toDate = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1); // Lấy hết ngày cuối
                object maCN = (cboChiNhanh.SelectedValue != null && (int)cboChiNhanh.SelectedValue != -1) ? cboChiNhanh.SelectedValue : DBNull.Value;

                // =========================================================
                // PHẦN A: BẢNG DỮ LIỆU (GRID) - ĐÃ HOẠT ĐỘNG TỐT
                // =========================================================
                string sqlGrid = @"SELECT CN.TenChiNhanh, 
                            SUM(ISNULL(T.S,0)) AS TongTienSan, 
                            SUM(ISNULL(T.D,0)) AS TongTienDoAn,
                            SUM(ISNULL(T.S,0)+ISNULL(T.D,0)) as TongCong 
                       FROM ChiNhanh CN LEFT JOIN (
                           SELECT MaChiNhanh, ThanhTien S, 0 D FROM HoaDon 
                           WHERE NgayLap BETWEEN @F AND @T AND TrangThaiThanhToan='DA_THANH_TOAN'
                           UNION ALL 
                           SELECT MaChiNhanh, 0, TongTien FROM HoaDonDoAn 
                           WHERE NgayLap BETWEEN @F AND @T AND TrangThai='DA_THANH_TOAN'
                       ) T ON CN.MaChiNhanh = T.MaChiNhanh 
                       WHERE (@M IS NULL OR CN.MaChiNhanh=@M) 
                       GROUP BY CN.TenChiNhanh";

                SqlCommand cmdGrid = new SqlCommand(sqlGrid, conn);
                cmdGrid.Parameters.AddWithValue("@F", fromDate);
                cmdGrid.Parameters.AddWithValue("@T", toDate);
                cmdGrid.Parameters.AddWithValue("@M", maCN);

                DataTable dtGrid = new DataTable();
                new SqlDataAdapter(cmdGrid).Fill(dtGrid);
                dgvDoanhThu.DataSource = dtGrid;
                SetupDataGridView();
                dgvDoanhThu.ClearSelection();

                // =========================================================
                // PHẦN B: BIỂU ĐỒ CỘT (SỬA LẠI ĐỂ DÙNG THAM SỐ @F, @T)
                // =========================================================
                string sqlChart = @"
            SELECT 
                MONTH(NgayLap) as Thang, 
                YEAR(NgayLap) as Nam,
                SUM(ThanhTien) as TongTien
            FROM (
                SELECT NgayLap, ThanhTien FROM HoaDon 
                WHERE NgayLap BETWEEN @F AND @T AND TrangThaiThanhToan='DA_THANH_TOAN'
                AND (@M IS NULL OR MaChiNhanh=@M)
                UNION ALL
                SELECT NgayLap, TongTien FROM HoaDonDoAn 
                WHERE NgayLap BETWEEN @F AND @T AND TrangThai='DA_THANH_TOAN'
                AND (@M IS NULL OR MaChiNhanh=@M)
            ) AS Temp
            GROUP BY MONTH(NgayLap), YEAR(NgayLap)
            ORDER BY Nam, Thang";

                SqlCommand cmdChart = new SqlCommand(sqlChart, conn);
                cmdChart.Parameters.AddWithValue("@F", fromDate); // Ép dùng ngày bắt đầu
                cmdChart.Parameters.AddWithValue("@T", toDate);   // Ép dùng ngày kết thúc
                cmdChart.Parameters.AddWithValue("@M", maCN);

                DataTable dtChart = new DataTable();
                new SqlDataAdapter(cmdChart).Fill(dtChart);

                // Vẽ lại Chart Cột
                chartDoanhThuNam.Series.Clear();
                chartDoanhThuNam.Titles.Clear();
                chartDoanhThuNam.Titles.Add($"Biểu Đồ ({fromDate:dd/MM} - {toDate:dd/MM})"); // Đổi tiêu đề cho dễ thấy

                Series sCol = new Series("Doanh Thu");
                sCol.ChartType = SeriesChartType.Column;
                sCol.IsValueShownAsLabel = true;

                foreach (DataRow r in dtChart.Rows)
                {
                    // Label: T1/2026
                    string label = "T" + r["Thang"].ToString() + "/" + r["Nam"].ToString();
                    sCol.Points.AddXY(label, r["TongTien"]);
                }
                chartDoanhThuNam.Series.Add(sCol);

                // =========================================================
                // PHẦN C: BIỂU ĐỒ TRÒN (LẤY DỮ LIỆU TỪ GRID ĐÃ LỌC)
                // =========================================================
                // Cách này đảm bảo 100% biểu đồ tròn khớp với bảng số liệu
                decimal totalSan = 0;
                decimal totalDoAn = 0;

                foreach (DataRow r in dtGrid.Rows)
                {
                    totalSan += Convert.ToDecimal(r["TongTienSan"]);
                    totalDoAn += Convert.ToDecimal(r["TongTienDoAn"]);
                }

                chartTyTrong.Series.Clear();
                Series sPie = new Series("DichVu");
                sPie.ChartType = SeriesChartType.Doughnut;

                if (totalSan + totalDoAn > 0)
                {
                    sPie.Points.AddXY("Tiền Sân", totalSan);
                    sPie.Points.AddXY("Đồ Ăn", totalDoAn);
                    sPie.Label = "#PERCENT";
                    sPie.LegendText = "#VALX";
                }
                else
                {
                    sPie.Points.AddXY("Không có dữ liệu", 1);
                    sPie.IsVisibleInLegend = false;
                }
                chartTyTrong.Series.Add(sPie);
            }
        }

        // Hàm 5: Vẽ 2 biểu đồ
        private void DrawCharts(DataTable dt)
        {
            // --- Biểu đồ Cột (Doanh thu theo chi nhánh) ---
            chartDoanhThuNam.Series.Clear();
            Series seriesCot = new Series("Doanh Thu");
            seriesCot.ChartType = SeriesChartType.Column; // Dạng cột
            seriesCot.IsValueShownAsLabel = true;

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToDecimal(row["TongCong"]) > 0)
                    seriesCot.Points.AddXY(row["TenChiNhanh"].ToString(), row["TongCong"]);
            }
            chartDoanhThuNam.Series.Add(seriesCot);

            // --- Biểu đồ Tròn (Tỷ trọng Tiền Sân vs Đồ Ăn) ---
            decimal totalSan = 0;
            decimal totalDoAn = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalSan += Convert.ToDecimal(row["TongTienSan"]);
                totalDoAn += Convert.ToDecimal(row["TongTienDoAn"]);
            }

            chartTyTrong.Series.Clear();
            Series seriesTron = new Series("TyTrong");
            seriesTron.ChartType = SeriesChartType.Doughnut; // Dạng bánh Donut

            if (totalSan + totalDoAn > 0)
            {
                seriesTron.Points.AddXY("Tiền Sân", totalSan);
                seriesTron.Points.AddXY("Đồ Ăn", totalDoAn);
                seriesTron.Label = "#PERCENT";
                seriesTron.LegendText = "#VALX";
            }
            chartTyTrong.Series.Add(seriesTron);
        }

        // Hàm 6: Format bảng hiển thị (Đặt tên tiếng Việt, định dạng tiền)
        private void SetupDataGridView()
        {
            // 1. CẤU HÌNH CHUNG CHO BẢNG
            dgvDoanhThu.AllowUserToAddRows = false;       // Bỏ dòng trống cuối
            dgvDoanhThu.RowHeadersVisible = false;        // Bỏ cột xám bên trái
            dgvDoanhThu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoanhThu.ReadOnly = true;

            // 2. TRANG TRÍ DÒNG VÀ FONT CHỮ (QUAN TRỌNG)
            dgvDoanhThu.BackgroundColor = Color.White; // Nền trắng
            dgvDoanhThu.BorderStyle = BorderStyle.None; // Bỏ viền cho hiện đại

            // Chỉnh Font chữ toàn bảng (Dùng Segoe UI nhìn sẽ sang hơn Sans Serif mặc định)
            dgvDoanhThu.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDoanhThu.RowTemplate.Height = 35; // Tăng chiều cao dòng cho thoáng (Mặc định là 22 hơi chật)

            // 3. TRANG TRÍ TIÊU ĐỀ CỘT (HEADER)
            dgvDoanhThu.EnableHeadersVisualStyles = false; // Bắt buộc dòng này mới chỉnh màu được
            dgvDoanhThu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Màu Xanh Dương
            dgvDoanhThu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Chữ Trắng
            dgvDoanhThu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDoanhThu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDoanhThu.ColumnHeadersHeight = 40; // Tiêu đề cao hơn chút

            // 4. ĐỊNH DẠNG TỪNG CỘT (CĂN LỀ & SỐ TIỀN)

            // Cột Chi Nhánh: Căn giữa hoặc trái
            if (dgvDoanhThu.Columns.Contains("TenChiNhanh"))
            {
                dgvDoanhThu.Columns["TenChiNhanh"].HeaderText = "Chi Nhánh";
                dgvDoanhThu.Columns["TenChiNhanh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // Cột Tiền Sân: Căn Phải + Dấu phẩy ngăn cách
            if (dgvDoanhThu.Columns.Contains("TongTienSan"))
            {
                dgvDoanhThu.Columns["TongTienSan"].HeaderText = "Tiền Sân";
                dgvDoanhThu.Columns["TongTienSan"].DefaultCellStyle.Format = "N0"; // Ra dạng 1,000,000
                dgvDoanhThu.Columns["TongTienSan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Cột Tiền Đồ Ăn: Căn Phải
            if (dgvDoanhThu.Columns.Contains("TongTienDoAn"))
            {
                dgvDoanhThu.Columns["TongTienDoAn"].HeaderText = "Tiền Đồ Ăn";
                dgvDoanhThu.Columns["TongTienDoAn"].DefaultCellStyle.Format = "N0";
                dgvDoanhThu.Columns["TongTienDoAn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Cột Tổng Cộng: Căn Phải + In Đậm + Màu chữ khác
            if (dgvDoanhThu.Columns.Contains("TongCong"))
            {
                dgvDoanhThu.Columns["TongCong"].HeaderText = "Tổng Cộng";
                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.Format = "N0";
                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.ForeColor = Color.Red; // Tô màu đỏ cho tổng tiền
            }
        }


        // Sự kiện khi bấm nút Xuất Excel
        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (dgvDoanhThu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = "C:\\";
            sfd.Title = "Lưu file báo cáo doanh thu";
            sfd.FileName = "BaoCaoDoanhThu_" + DateTime.Now.ToString("ddMMyyyy_HHmm");
            sfd.Filter = "Excel Files|*.xlsx;*.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // --- KIỂM TRA XEM CÓ ĐANG CHỌN "TẤT CẢ" KHÔNG ---
                    bool isTongHop = false;
                    if (cboChiNhanh.SelectedValue != null && (int)cboChiNhanh.SelectedValue == -1)
                    {
                        isTongHop = true;
                    }

                    // Truyền biến isTongHop vào hàm xuất
                    ExportToExcel(dgvDoanhThu, sfd.FileName, "Báo Cáo Doanh Thu", isTongHop);

                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // HÀM XỬ LÝ CHÍNH: GHI DỮ LIỆU TỪ GRID RA EXCEL
        // Thêm tham số bool tinhTongVaoCuoi (Mặc định là false)
        private void ExportToExcel(DataGridView g, string duongDan, string tenTapTin, bool tinhTongVaoCuoi = false)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet sheet = null;

            try
            {
                sheet = wb.ActiveSheet;
                sheet.Name = "DoanhThu";

                // Biến để cộng dồn
                decimal sumSan = 0;
                decimal sumDoAn = 0;
                decimal sumTong = 0;

                // 1. VIẾT TIÊU ĐỀ
                int colIndex = 1;
                for (int i = 0; i < g.Columns.Count; i++)
                {
                    if (g.Columns[i].Visible)
                    {
                        sheet.Cells[1, colIndex] = g.Columns[i].HeaderText;
                        Excel.Range range = sheet.Cells[1, colIndex];
                        range.Font.Bold = true;
                        range.Interior.Color = System.Drawing.Color.LightGray;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        colIndex++;
                    }
                }

                // 2. VIẾT DỮ LIỆU & CỘNG DỒN
                for (int i = 0; i < g.Rows.Count; i++)
                {
                    colIndex = 1;
                    for (int j = 0; j < g.Columns.Count; j++)
                    {
                        if (g.Columns[j].Visible)
                        {
                            var value = g.Rows[i].Cells[j].Value;
                            if (value != null)
                            {
                                sheet.Cells[i + 2, colIndex] = value.ToString();

                                // --- Logic cộng dồn (Chỉ chạy khi cần tính tổng) ---
                                if (tinhTongVaoCuoi)
                                {
                                    string colName = g.Columns[j].Name;
                                    decimal valDec = 0;
                                    decimal.TryParse(value.ToString(), out valDec);

                                    if (colName == "TongTienSan") sumSan += valDec;
                                    if (colName == "TongTienDoAn") sumDoAn += valDec;
                                    if (colName == "TongCong") sumTong += valDec;
                                }
                            }

                            // Kẻ khung
                            Excel.Range cell = sheet.Cells[i + 2, colIndex];
                            cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                            colIndex++;
                        }
                    }
                }

                // 3. VẼ DÒNG TỔNG CỘNG (MỚI)
                if (tinhTongVaoCuoi)
                {
                    int lastRow = g.Rows.Count + 2; // Dòng tiếp theo sau dữ liệu

                    // Ghi chữ "TỔNG CỘNG HỆ THỐNG" vào cột đầu tiên (Chi Nhánh)
                    sheet.Cells[lastRow, 1] = "TỔNG CỘNG HỆ THỐNG";

                    // Ghi các số tổng vào đúng cột tương ứng
                    // Lưu ý: Phải đếm lại colIndex vì trong vòng lặp trên colIndex chạy lung tung
                    int currentExcelCol = 1;
                    for (int j = 0; j < g.Columns.Count; j++)
                    {
                        if (g.Columns[j].Visible)
                        {
                            string colName = g.Columns[j].Name;
                            if (colName == "TongTienSan") sheet.Cells[lastRow, currentExcelCol] = sumSan;
                            if (colName == "TongTienDoAn") sheet.Cells[lastRow, currentExcelCol] = sumDoAn;
                            if (colName == "TongCong") sheet.Cells[lastRow, currentExcelCol] = sumTong;

                            currentExcelCol++;
                        }
                    }

                    // Trang trí dòng tổng: Chữ Đỏ, In Đậm, Nền Vàng nhạt
                    Excel.Range totalRowRange = sheet.Range[sheet.Cells[lastRow, 1], sheet.Cells[lastRow, colIndex - 1]];
                    totalRowRange.Font.Bold = true;
                    totalRowRange.Font.Color = System.Drawing.Color.Red;
                    totalRowRange.Interior.Color = System.Drawing.Color.LightYellow;
                    totalRowRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                // 4. CĂN CHỈNH
                sheet.Columns.AutoFit();
                wb.SaveAs(duongDan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                app.Quit();
                releaseObject(wb);
                releaseObject(sheet);
                releaseObject(app);
            }
        }

        // Hàm phụ trợ để giải phóng bộ nhớ COM (Tránh lỗi Excel chạy ngầm)
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }







        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void DoanhThu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDoanhThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
