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
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox6;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox2;
        private Panel pnlHomNay;
        private Label lblSoTienHomNay;
        private GroupBox groupBox3;
        private Panel pnlTuanNay;
        private Label lblSoTienTuanNay;
        private GroupBox groupBox4;
        private Panel pnlThangNay;
        private Label lblSoTienThangNay;
        private GroupBox groupBox5;
        private Panel pnlTongDoanhThu;
        private Label lblTongDoanhThu;
        private TableLayoutPanel DoanhThu;
        private GroupBox groupBox7;
        private Chart chartDoanhThuNam;
        private GroupBox groupBox8;
        private Chart chartTyTrong;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel panel3;
        private GroupBox groupBox12;
        private Panel panel4;
        private Button btnXuatBaoCao;
        private GroupBox groupBox9;
        private Panel panel2;
        private Label label4;
        private Label label3;
        private Button btnThongKe;
        private ComboBox cboChiNhanh;
        private GroupBox groupBox10;
        private Panel panel1;
        private DateTimePicker dtpTuNgay;
        private Label label2;
        private DateTimePicker dtpDenNgay;
        private Label label1;
        private GroupBox groupBox11;
        private DataGridView dgvDoanhThu;
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
                    System.Diagnostics.Debug.WriteLine("Lỗi tính doanh thu: " + ex.Message);
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










        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlHomNay = new System.Windows.Forms.Panel();
            this.lblSoTienHomNay = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlTuanNay = new System.Windows.Forms.Panel();
            this.lblSoTienTuanNay = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pnlThangNay = new System.Windows.Forms.Panel();
            this.lblSoTienThangNay = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pnlTongDoanhThu = new System.Windows.Forms.Panel();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.DoanhThu = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chartDoanhThuNam = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chartTyTrong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlHomNay.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnlTuanNay.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.pnlThangNay.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.pnlTongDoanhThu.SuspendLayout();
            this.DoanhThu.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuNam)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTyTrong)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.DoanhThu, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox11, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.24859F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.75141F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1254, 663);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox6.Controls.Add(this.tableLayoutPanel1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1248, 89);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "DOANH THU";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1242, 52);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.pnlHomNay);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 46);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DoanhThu Hôm Nay";
            // 
            // pnlHomNay
            // 
            this.pnlHomNay.BackColor = System.Drawing.SystemColors.Control;
            this.pnlHomNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHomNay.Controls.Add(this.lblSoTienHomNay);
            this.pnlHomNay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHomNay.Location = new System.Drawing.Point(3, 26);
            this.pnlHomNay.Name = "pnlHomNay";
            this.pnlHomNay.Size = new System.Drawing.Size(298, 17);
            this.pnlHomNay.TabIndex = 0;
            // 
            // lblSoTienHomNay
            // 
            this.lblSoTienHomNay.AutoSize = true;
            this.lblSoTienHomNay.Location = new System.Drawing.Point(3, 0);
            this.lblSoTienHomNay.Name = "lblSoTienHomNay";
            this.lblSoTienHomNay.Size = new System.Drawing.Size(35, 23);
            this.lblSoTienHomNay.TabIndex = 1;
            this.lblSoTienHomNay.Text = "0 đ";
            this.lblSoTienHomNay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox3.Controls.Add(this.pnlTuanNay);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(313, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 46);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Doanh Thu Tuần Này";
            // 
            // pnlTuanNay
            // 
            this.pnlTuanNay.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTuanNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTuanNay.Controls.Add(this.lblSoTienTuanNay);
            this.pnlTuanNay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTuanNay.Location = new System.Drawing.Point(3, 26);
            this.pnlTuanNay.Name = "pnlTuanNay";
            this.pnlTuanNay.Size = new System.Drawing.Size(298, 17);
            this.pnlTuanNay.TabIndex = 2;
            // 
            // lblSoTienTuanNay
            // 
            this.lblSoTienTuanNay.AutoSize = true;
            this.lblSoTienTuanNay.Location = new System.Drawing.Point(3, 1);
            this.lblSoTienTuanNay.Name = "lblSoTienTuanNay";
            this.lblSoTienTuanNay.Size = new System.Drawing.Size(35, 23);
            this.lblSoTienTuanNay.TabIndex = 2;
            this.lblSoTienTuanNay.Text = "0 đ";
            this.lblSoTienTuanNay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox4.Controls.Add(this.pnlThangNay);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(623, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 46);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Doanh Thu Tháng Này";
            // 
            // pnlThangNay
            // 
            this.pnlThangNay.BackColor = System.Drawing.SystemColors.Control;
            this.pnlThangNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlThangNay.Controls.Add(this.lblSoTienThangNay);
            this.pnlThangNay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThangNay.Location = new System.Drawing.Point(3, 26);
            this.pnlThangNay.Name = "pnlThangNay";
            this.pnlThangNay.Size = new System.Drawing.Size(298, 17);
            this.pnlThangNay.TabIndex = 0;
            // 
            // lblSoTienThangNay
            // 
            this.lblSoTienThangNay.AutoSize = true;
            this.lblSoTienThangNay.Location = new System.Drawing.Point(3, 1);
            this.lblSoTienThangNay.Name = "lblSoTienThangNay";
            this.lblSoTienThangNay.Size = new System.Drawing.Size(35, 23);
            this.lblSoTienThangNay.TabIndex = 3;
            this.lblSoTienThangNay.Text = "0 đ";
            this.lblSoTienThangNay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox5.Controls.Add(this.pnlTongDoanhThu);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(933, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(306, 46);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tổng Doanh Thu";
            // 
            // pnlTongDoanhThu
            // 
            this.pnlTongDoanhThu.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTongDoanhThu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTongDoanhThu.Controls.Add(this.lblTongDoanhThu);
            this.pnlTongDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTongDoanhThu.Location = new System.Drawing.Point(3, 26);
            this.pnlTongDoanhThu.Name = "pnlTongDoanhThu";
            this.pnlTongDoanhThu.Size = new System.Drawing.Size(300, 17);
            this.pnlTongDoanhThu.TabIndex = 0;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(3, 1);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(35, 23);
            this.lblTongDoanhThu.TabIndex = 3;
            this.lblTongDoanhThu.Text = "0 đ";
            this.lblTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DoanhThu
            // 
            this.DoanhThu.ColumnCount = 2;
            this.DoanhThu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.DoanhThu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.DoanhThu.Controls.Add(this.groupBox7, 0, 0);
            this.DoanhThu.Controls.Add(this.groupBox8, 1, 0);
            this.DoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DoanhThu.Location = new System.Drawing.Point(3, 98);
            this.DoanhThu.Name = "DoanhThu";
            this.DoanhThu.RowCount = 1;
            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.DoanhThu.Size = new System.Drawing.Size(1248, 237);
            this.DoanhThu.TabIndex = 6;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox7.Controls.Add(this.chartDoanhThuNam);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(867, 231);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "BIỂU ĐỒ DOANH THU NĂM";
            // 
            // chartDoanhThuNam
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThuNam.ChartAreas.Add(chartArea1);
            this.chartDoanhThuNam.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDoanhThuNam.Legends.Add(legend1);
            this.chartDoanhThuNam.Location = new System.Drawing.Point(3, 26);
            this.chartDoanhThuNam.Name = "chartDoanhThuNam";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDoanhThuNam.Series.Add(series1);
            this.chartDoanhThuNam.Size = new System.Drawing.Size(861, 202);
            this.chartDoanhThuNam.TabIndex = 2;
            this.chartDoanhThuNam.Text = "chart1";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox8.Controls.Add(this.chartTyTrong);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(876, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(369, 231);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "DOANH THU THEO DỊCH VỤ";
            // 
            // chartTyTrong
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTyTrong.ChartAreas.Add(chartArea2);
            this.chartTyTrong.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Doanh Thu Đồ Ăn";
            this.chartTyTrong.Legends.Add(legend2);
            this.chartTyTrong.Location = new System.Drawing.Point(3, 26);
            this.chartTyTrong.Name = "chartTyTrong";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Doanh Thu Đồ Ăn";
            series2.Name = "Series1";
            this.chartTyTrong.Series.Add(series2);
            this.chartTyTrong.Size = new System.Drawing.Size(363, 202);
            this.chartTyTrong.TabIndex = 1;
            this.chartTyTrong.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 341);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1248, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BỘ LỌC DỮ LIỆU";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.16203F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.53192F));
            this.tableLayoutPanel3.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox9, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox10, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 26);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1242, 106);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Cornsilk;
            this.panel3.Controls.Add(this.groupBox12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(927, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(312, 100);
            this.panel3.TabIndex = 2;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox12.Controls.Add(this.panel4);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox12.Location = new System.Drawing.Point(0, 0);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(312, 100);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Xuất Doanh Thu";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.btnXuatBaoCao);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 26);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(306, 71);
            this.panel4.TabIndex = 0;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnXuatBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(3, 4);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(298, 51);
            this.btnXuatBaoCao.TabIndex = 7;
            this.btnXuatBaoCao.Text = "Xuất Excel";
            this.btnXuatBaoCao.UseVisualStyleBackColor = false;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox9.Controls.Add(this.panel2);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(416, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(505, 100);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Chi Nhánh";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnThongKe);
            this.panel2.Controls.Add(this.cboChiNhanh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(499, 71);
            this.panel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Click:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Chọn Chi Nhánh:";
            // 
            // btnThongKe
            // 
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Location = new System.Drawing.Point(178, 39);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(309, 30);
            this.btnThongKe.TabIndex = 6;
            this.btnThongKe.Text = "Lọc Dữ Liệu";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Location = new System.Drawing.Point(178, 4);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(309, 31);
            this.cboChiNhanh.TabIndex = 5;
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox10.Controls.Add(this.panel1);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox10.Location = new System.Drawing.Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(407, 100);
            this.groupBox10.TabIndex = 4;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Ngày";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.dtpTuNgay);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtpDenNgay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 71);
            this.panel1.TabIndex = 0;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(110, 2);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(282, 30);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến Ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(110, 37);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(282, 30);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ Ngày:";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.dgvDoanhThu);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.Location = new System.Drawing.Point(3, 482);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(1248, 178);
            this.groupBox11.TabIndex = 9;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "DOANH THU THEO CHI NHÁNH";
            // 
            // dgvDoanhThu
            // 
            this.dgvDoanhThu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoanhThu.Location = new System.Drawing.Point(3, 26);
            this.dgvDoanhThu.Name = "dgvDoanhThu";
            this.dgvDoanhThu.ReadOnly = true;
            this.dgvDoanhThu.RowHeadersVisible = false;
            this.dgvDoanhThu.RowHeadersWidth = 51;
            this.dgvDoanhThu.RowTemplate.Height = 24;
            this.dgvDoanhThu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoanhThu.Size = new System.Drawing.Size(1242, 149);
            this.dgvDoanhThu.TabIndex = 7;
            // 
            // frmdoanhthuu
            // 
            this.ClientSize = new System.Drawing.Size(1254, 663);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "frmdoanhthuu";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pnlHomNay.ResumeLayout(false);
            this.pnlHomNay.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.pnlTuanNay.ResumeLayout(false);
            this.pnlTuanNay.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.pnlThangNay.ResumeLayout(false);
            this.pnlThangNay.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.pnlTongDoanhThu.ResumeLayout(false);
            this.pnlTongDoanhThu.PerformLayout();
            this.DoanhThu.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuNam)).EndInit();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTyTrong)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
