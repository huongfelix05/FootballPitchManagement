//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;
//using Excel = Microsoft.Office.Interop.Excel;

//namespace FootballPitchManagement.Forms.Admin
//{
//    public partial class frmdoanhthuu : Form
//    {
//        string strConnect = @"Data Source=MSI;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

//        public frmdoanhthuu()
//        {
//            InitializeComponent();
//            // --- THÊM 2 DÒNG NÀY ĐỂ BẮT BUỘC CODE PHẢI CHẠY ---
//            this.Load += new EventHandler(frmDoanhThu_Load);
//            btnThongKe.Click += new EventHandler(btnThongKe_Click);
//        }


        // 2. SỰ KIỆN KHI FORM VỪA MỞ LÊN (LOAD)
        // 2. SỰ KIỆN KHI FORM VỪA MỞ LÊN (LOAD)
        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
            try
            {
                // Bước 1: Cài đặt ngày mặc định (Từ ngày 1 đến cuối tháng hiện tại)
                DateTime now = DateTime.Now;
                dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
                dtpDenNgay.Value = new DateTime(now.Year, now.Month, 1).AddMonths(1).AddDays(-1);

                // Bước 2: Nạp danh sách Chi nhánh vào ComboBox (QUAN TRỌNG)
                // Nếu không chạy dòng này, ComboBox sẽ rỗng và bộ lọc sẽ bị lỗi
                LoadComboBoxChiNhanh();

                // Chọn mặc định là "Tất cả chi nhánh"
                if (cboChiNhanh.Items.Count > 0)
                
                    cboChiNhanh.SelectedIndex = 0;
                

                // Bước 3: Đồng bộ dữ liệu Doanh Thu mới nhất từ Hóa Đơn
                //RefreshRevenueData();

                // Bước 4: Hiển thị dữ liệu lên màn hình
                LoadDashboardCards();
                LoadMainReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        // Hàm hỗ trợ chạy lệnh cập nhật doanh thu (Đã sửa logic lấy Ngày Đặt)
        private void RefreshRevenueData()
        {
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                conn.Open();
                string sqlUpdate = @"
            -- 1. Xóa sạch dữ liệu cũ để tính lại
            TRUNCATE TABLE DoanhThu;

            -- 2. CẬP NHẬT TIỀN SÂN: LẤY THEO NGÀY ĐẶT (LichDatSan.NgayDat)
            -- Logic: Join bảng HoaDon với LichDatSan để lấy đúng ngày khách chơi bóng
            INSERT INTO DoanhThu (MaChiNhanh, Ngay, LoaiDoanhThu, SoTien, GhiChu)
            SELECT 
                HD.MaChiNhanh, 
                CAST(LDS.NgayDat AS DATE), -- <--- QUAN TRỌNG: Lấy cột NgayDat của bảng Lịch
                'SAN', 
                SUM(HD.ThanhTien),
                N'Thu tiền sân đơn #' + CAST(MIN(HD.MaDatSan) AS NVARCHAR)
            FROM HoaDon HD
            JOIN LichDatSan LDS ON HD.MaDatSan = LDS.MaDatSan -- Kết nối 2 bảng
            WHERE HD.TrangThaiThanhToan = 'DA_THANH_TOAN' 
            GROUP BY HD.MaChiNhanh, CAST(LDS.NgayDat AS DATE);

            -- 3. CẬP NHẬT TIỀN ĐỒ ĂN (Giữ nguyên theo ngày bán)
            INSERT INTO DoanhThu (MaChiNhanh, Ngay, LoaiDoanhThu, SoTien, GhiChu)
            SELECT 
                MaChiNhanh, 
                CAST(NgayLap AS DATE), 
                'DO_AN', 
                SUM(TongTien),
                N'Bán đồ ăn'
            FROM HoaDonDoAn 
            WHERE TrangThai = 'DA_THANH_TOAN' 
            GROUP BY MaChiNhanh, CAST(NgayLap AS DATE);";

                SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
                cmd.ExecuteNonQuery();
            }
        }

        // Gọi hàm này khi bấm nút Lọc
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            RefreshRevenueData(); // <--- Thêm dòng này để cập nhật dữ liệu mới nhất
            LoadDashboardCards();
            LoadMainReport();
        }



        // =======================================================================
        // CÁC HÀM XỬ LÝ LOGIC (SQL) - KHU VỰC NÀY LÀ "TRÁI TIM" CỦA FORM
        // =======================================================================

//        // Hàm 1: Đổ dữ liệu vào ComboBox Chi Nhánh
//        private void LoadComboBoxChiNhanh()
//        {
//            using (SqlConnection conn = new SqlConnection(strConnect))
//            {
//                conn.Open();
//                string sql = "SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh";
//                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
//                DataTable dt = new DataTable();
//                da.Fill(dt);

//                // Thêm dòng "Tất cả chi nhánh" lên đầu
//                DataRow dr = dt.NewRow();
//                dr["MaChiNhanh"] = -1;
//                dr["TenChiNhanh"] = "Tất cả chi nhánh";
//                dt.Rows.InsertAt(dr, 0);

                cboChiNhanh.DataSource = dt;
                cboChiNhanh.DisplayMember = "TenChiNhanh";
                cboChiNhanh.ValueMember = "MaChiNhanh";
            }
        }
        // Hàm phụ trợ: Tính tổng tiền trong khoảng thời gian và chi nhánh cụ thể
        // Hàm mới: Chỉ lấy dữ liệu từ bảng DoanhThu
        private decimal GetRevenueFromTable(DateTime from, DateTime to, int? maCN)
        {
            decimal total = 0;
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                try
                {
                    conn.Open();
                    // Câu lệnh SQL cực gọn: Chỉ cần SUM cột SoTien
                    string sql = @"
                SELECT SUM(SoTien) 
                FROM DoanhThu 
                WHERE Ngay >= @F AND Ngay <= @T 
                AND (@M IS NULL OR MaChiNhanh = @M)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Tham số ngày (Vì cột Ngay trong SQL là kiểu DATE nên chỉ cần truyền Date)
                    cmd.Parameters.AddWithValue("@F", from.Date);
                    cmd.Parameters.AddWithValue("@T", to.Date);

                    if (maCN.HasValue)
                        cmd.Parameters.AddWithValue("@M", maCN.Value);
                    else
                        cmd.Parameters.AddWithValue("@M", DBNull.Value);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        total = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    total = 0; // Nếu lỗi trả về 0
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

//            DateTime now = DateTime.Now;

            // --- Ô 1: HÔM NAY ---
            lblSoTienHomNay.Text = GetRevenueFromTable(now, now, maCN).ToString("N0") + " đ";

            // --- Ô 2: TUẦN NÀY ---
            int dayOfWeek = (int)now.DayOfWeek;
            if (dayOfWeek == 0) dayOfWeek = 7;
            DateTime startWeek = now.AddDays(-(dayOfWeek - 1));
            DateTime endWeek = startWeek.AddDays(6);
            lblSoTienTuanNay.Text = GetRevenueFromTable(startWeek, endWeek, maCN).ToString("N0") + " đ";

            // --- Ô 3: THÁNG NÀY ---
            DateTime startMonth = new DateTime(now.Year, now.Month, 1);
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);
            lblSoTienThangNay.Text = GetRevenueFromTable(startMonth, endMonth, maCN).ToString("N0") + " đ";

            // ========================================================================
            // --- Ô 4: TỔNG DOANH THU (SỬA LẠI: TRỌN ĐỜI / TOÀN THỜI GIAN) ---
            // ========================================================================

            // Thay vì lấy từ dtpTuNgay, ta lấy mốc thời gian cực rộng (từ năm 2000 đến năm 3000)
            // Để đảm bảo nó cộng hết tất cả tiền từ trước đến nay của Chi Nhánh đó
            DateTime allTimeStart = new DateTime(2000, 1, 1);
            DateTime allTimeEnd = new DateTime(3000, 1, 1);

            lblTongDoanhThu.Text = GetRevenueFromTable(allTimeStart, allTimeEnd, maCN).ToString("N0") + " đ";

            // Đổi tên tiêu đề GroupBox để người dùng hiểu đây là tổng trọn đời
            // (groupBox5 là cái khung chứa ô Tổng doanh thu trong code của bạn)
            groupBox5.Text = "Tổng Doanh Thu ";
        }

        // Hàm 4: Lấy dữ liệu chi tiết để vẽ Biểu đồ và Bảng
        private void LoadMainReport()
        {
            using (SqlConnection conn = new SqlConnection(strConnect))
            {
                conn.Open();

                // Lấy tham số từ bộ lọc
                DateTime from = dtpTuNgay.Value.Date;
                DateTime to = dtpDenNgay.Value.Date;
                object maCN = (cboChiNhanh.SelectedValue != null && (int)cboChiNhanh.SelectedValue != -1)
                              ? cboChiNhanh.SelectedValue : DBNull.Value;

                // ---------------------------------------------------------
                // 1. LẤY DỮ LIỆU CHO BẢNG GRID (Từ bảng DoanhThu)
                // ---------------------------------------------------------
                string sqlGrid = @"
            SELECT 
                CN.TenChiNhanh,
                SUM(CASE WHEN DT.LoaiDoanhThu = 'SAN' THEN DT.SoTien ELSE 0 END) AS TongTienSan,
                SUM(CASE WHEN DT.LoaiDoanhThu = 'DO_AN' THEN DT.SoTien ELSE 0 END) AS TongTienDoAn,
                SUM(ISNULL(DT.SoTien, 0)) AS TongCong
            FROM ChiNhanh CN
            LEFT JOIN DoanhThu DT ON CN.MaChiNhanh = DT.MaChiNhanh 
                 AND DT.Ngay BETWEEN @F AND @T
            WHERE (@M IS NULL OR CN.MaChiNhanh = @M)
            GROUP BY CN.TenChiNhanh";

                SqlCommand cmd = new SqlCommand(sqlGrid, conn);
                cmd.Parameters.AddWithValue("@F", from);
                cmd.Parameters.AddWithValue("@T", to);
                cmd.Parameters.AddWithValue("@M", maCN);

                DataTable dtGrid = new DataTable();
                new SqlDataAdapter(cmd).Fill(dtGrid);

                dgvDoanhThu.DataSource = dtGrid;
                SetupDataGridView();
                dgvDoanhThu.ClearSelection();

                // ---------------------------------------------------------
                // 2. VẼ BIỂU ĐỒ CỘT (DOANH THU NĂM)
                // ---------------------------------------------------------
                string sqlChart = @"
            SELECT 
                MONTH(Ngay) AS Thang, 
                YEAR(Ngay) AS Nam, 
                SUM(SoTien) AS TongTien
            FROM DoanhThu
            WHERE Ngay BETWEEN @F AND @T
              AND (@M IS NULL OR MaChiNhanh = @M)
            GROUP BY MONTH(Ngay), YEAR(Ngay)
            ORDER BY Nam, Thang";

                SqlCommand cmdChart = new SqlCommand(sqlChart, conn);
                cmdChart.Parameters.AddWithValue("@F", from);
                cmdChart.Parameters.AddWithValue("@T", to);
                cmdChart.Parameters.AddWithValue("@M", maCN);

//                DataTable dtChart = new DataTable();
//                new SqlDataAdapter(cmdChart).Fill(dtChart);

                // -- Vẽ Chart --
                chartDoanhThuNam.Series.Clear();
                chartDoanhThuNam.Titles.Clear();
                chartDoanhThuNam.Titles.Add($"Biểu Đồ ({from:dd/MM} - {to:dd/MM})");

//                Series sCol = new Series("Doanh Thu");
//                sCol.ChartType = SeriesChartType.Column;
//                sCol.IsValueShownAsLabel = true;

                foreach (DataRow r in dtChart.Rows)
                {
                    sCol.Points.AddXY("T" + r["Thang"], r["TongTien"]);
                }
                chartDoanhThuNam.Series.Add(sCol);

                // ---------------------------------------------------------
                // 3. VẼ BIỂU ĐỒ TRÒN (TỪ dtGrid)
                // ---------------------------------------------------------
                decimal totalSan = 0, totalDoAn = 0;
                foreach (DataRow r in dtGrid.Rows)
                {
                    totalSan += r["TongTienSan"] != DBNull.Value ? Convert.ToDecimal(r["TongTienSan"]) : 0;
                    totalDoAn += r["TongTienDoAn"] != DBNull.Value ? Convert.ToDecimal(r["TongTienDoAn"]) : 0;
                }

                chartTyTrong.Series.Clear();
                Series sPie = new Series("TyTrong");
                sPie.ChartType = SeriesChartType.Doughnut;

//                if (totalSan + totalDoAn > 0)
//                {
//                    sPie.Points.AddXY("Tiền Sân", totalSan);
//                    sPie.Points.AddXY("Đồ Ăn", totalDoAn);
//                    sPie.Label = "#PERCENT";
//                    sPie.LegendText = "#VALX";
//                }
//                else
//                {
//                    sPie.Points.AddXY("Không có dữ liệu", 1);
//                    sPie.IsVisibleInLegend = false;
//                }
//                chartTyTrong.Series.Add(sPie);
//            }
//        }

//        // Hàm 5: Vẽ 2 biểu đồ
//        private void DrawCharts(DataTable dt)
//        {
//            // --- Biểu đồ Cột (Doanh thu theo chi nhánh) ---
//            chartDoanhThuNam.Series.Clear();
//            Series seriesCot = new Series("Doanh Thu");
//            seriesCot.ChartType = SeriesChartType.Column; // Dạng cột
//            seriesCot.IsValueShownAsLabel = true;

//            foreach (DataRow row in dt.Rows)
//            {
//                if (Convert.ToDecimal(row["TongCong"]) > 0)
//                    seriesCot.Points.AddXY(row["TenChiNhanh"].ToString(), row["TongCong"]);
//            }
//            chartDoanhThuNam.Series.Add(seriesCot);

//            // --- Biểu đồ Tròn (Tỷ trọng Tiền Sân vs Đồ Ăn) ---
//            decimal totalSan = 0;
//            decimal totalDoAn = 0;
//            foreach (DataRow row in dt.Rows)
//            {
//                totalSan += Convert.ToDecimal(row["TongTienSan"]);
//                totalDoAn += Convert.ToDecimal(row["TongTienDoAn"]);
//            }

//            chartTyTrong.Series.Clear();
//            Series seriesTron = new Series("TyTrong");
//            seriesTron.ChartType = SeriesChartType.Doughnut; // Dạng bánh Donut

//            if (totalSan + totalDoAn > 0)
//            {
//                seriesTron.Points.AddXY("Tiền Sân", totalSan);
//                seriesTron.Points.AddXY("Đồ Ăn", totalDoAn);
//                seriesTron.Label = "#PERCENT";
//                seriesTron.LegendText = "#VALX";
//            }
//            chartTyTrong.Series.Add(seriesTron);
//        }

//        // Hàm 6: Format bảng hiển thị (Đặt tên tiếng Việt, định dạng tiền)
//        private void SetupDataGridView()
//        {
//            // 1. CẤU HÌNH CHUNG CHO BẢNG
//            dgvDoanhThu.AllowUserToAddRows = false;       // Bỏ dòng trống cuối
//            dgvDoanhThu.RowHeadersVisible = false;        // Bỏ cột xám bên trái
//            dgvDoanhThu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
//            dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
//            dgvDoanhThu.ReadOnly = true;

//            // 2. TRANG TRÍ DÒNG VÀ FONT CHỮ (QUAN TRỌNG)
//            dgvDoanhThu.BackgroundColor = Color.White; // Nền trắng
//            dgvDoanhThu.BorderStyle = BorderStyle.None; // Bỏ viền cho hiện đại

//            // Chỉnh Font chữ toàn bảng (Dùng Segoe UI nhìn sẽ sang hơn Sans Serif mặc định)
//            dgvDoanhThu.DefaultCellStyle.Font = new Font("Segoe UI", 10);
//            dgvDoanhThu.RowTemplate.Height = 35; // Tăng chiều cao dòng cho thoáng (Mặc định là 22 hơi chật)

//            // 3. TRANG TRÍ TIÊU ĐỀ CỘT (HEADER)
//            dgvDoanhThu.EnableHeadersVisualStyles = false; // Bắt buộc dòng này mới chỉnh màu được
//            dgvDoanhThu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Màu Xanh Dương
//            dgvDoanhThu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Chữ Trắng
//            dgvDoanhThu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
//            dgvDoanhThu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
//            dgvDoanhThu.ColumnHeadersHeight = 40; // Tiêu đề cao hơn chút

//            // 4. ĐỊNH DẠNG TỪNG CỘT (CĂN LỀ & SỐ TIỀN)

//            // Cột Chi Nhánh: Căn giữa hoặc trái
//            if (dgvDoanhThu.Columns.Contains("TenChiNhanh"))
//            {
//                dgvDoanhThu.Columns["TenChiNhanh"].HeaderText = "Chi Nhánh";
//                dgvDoanhThu.Columns["TenChiNhanh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
//            }

//            // Cột Tiền Sân: Căn Phải + Dấu phẩy ngăn cách
//            if (dgvDoanhThu.Columns.Contains("TongTienSan"))
//            {
//                dgvDoanhThu.Columns["TongTienSan"].HeaderText = "Tiền Sân";
//                dgvDoanhThu.Columns["TongTienSan"].DefaultCellStyle.Format = "N0"; // Ra dạng 1,000,000
//                dgvDoanhThu.Columns["TongTienSan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//            }

//            // Cột Tiền Đồ Ăn: Căn Phải
//            if (dgvDoanhThu.Columns.Contains("TongTienDoAn"))
//            {
//                dgvDoanhThu.Columns["TongTienDoAn"].HeaderText = "Tiền Đồ Ăn";
//                dgvDoanhThu.Columns["TongTienDoAn"].DefaultCellStyle.Format = "N0";
//                dgvDoanhThu.Columns["TongTienDoAn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//            }

//            // Cột Tổng Cộng: Căn Phải + In Đậm + Màu chữ khác
//            if (dgvDoanhThu.Columns.Contains("TongCong"))
//            {
//                dgvDoanhThu.Columns["TongCong"].HeaderText = "Tổng Cộng";
//                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.Format = "N0";
//                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
//                dgvDoanhThu.Columns["TongCong"].DefaultCellStyle.ForeColor = Color.Red; // Tô màu đỏ cho tổng tiền
//            }
//        }


//        // Sự kiện khi bấm nút Xuất Excel
//        private void btnXuatBaoCao_Click(object sender, EventArgs e)
//        {
//            if (dgvDoanhThu.Rows.Count == 0)
//            {
//                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            SaveFileDialog sfd = new SaveFileDialog();
//            sfd.InitialDirectory = "C:\\";
//            sfd.Title = "Lưu file báo cáo doanh thu";
//            sfd.FileName = "BaoCaoDoanhThu_" + DateTime.Now.ToString("ddMMyyyy_HHmm");
//            sfd.Filter = "Excel Files|*.xlsx;*.xls";

//            if (sfd.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    // --- KIỂM TRA XEM CÓ ĐANG CHỌN "TẤT CẢ" KHÔNG ---
//                    bool isTongHop = false;
//                    if (cboChiNhanh.SelectedValue != null && (int)cboChiNhanh.SelectedValue == -1)
//                    {
//                        isTongHop = true;
//                    }

//                    // Truyền biến isTongHop vào hàm xuất
//                    ExportToExcel(dgvDoanhThu, sfd.FileName, "Báo Cáo Doanh Thu", isTongHop);

//                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        // HÀM XỬ LÝ CHÍNH: GHI DỮ LIỆU TỪ GRID RA EXCEL
//        // Thêm tham số bool tinhTongVaoCuoi (Mặc định là false)
//        private void ExportToExcel(DataGridView g, string duongDan, string tenTapTin, bool tinhTongVaoCuoi = false)
//        {
//            Excel.Application app = new Excel.Application();
//            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
//            Excel.Worksheet sheet = null;

//            try
//            {
//                sheet = wb.ActiveSheet;
//                sheet.Name = "DoanhThu";

//                // Biến để cộng dồn
//                decimal sumSan = 0;
//                decimal sumDoAn = 0;
//                decimal sumTong = 0;

//                // 1. VIẾT TIÊU ĐỀ
//                int colIndex = 1;
//                for (int i = 0; i < g.Columns.Count; i++)
//                {
//                    if (g.Columns[i].Visible)
//                    {
//                        sheet.Cells[1, colIndex] = g.Columns[i].HeaderText;
//                        Excel.Range range = sheet.Cells[1, colIndex];
//                        range.Font.Bold = true;
//                        range.Interior.Color = System.Drawing.Color.LightGray;
//                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
//                        range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
//                        colIndex++;
//                    }
//                }

//                // 2. VIẾT DỮ LIỆU & CỘNG DỒN
//                for (int i = 0; i < g.Rows.Count; i++)
//                {
//                    colIndex = 1;
//                    for (int j = 0; j < g.Columns.Count; j++)
//                    {
//                        if (g.Columns[j].Visible)
//                        {
//                            var value = g.Rows[i].Cells[j].Value;
//                            if (value != null)
//                            {
//                                sheet.Cells[i + 2, colIndex] = value.ToString();

//                                // --- Logic cộng dồn (Chỉ chạy khi cần tính tổng) ---
//                                if (tinhTongVaoCuoi)
//                                {
//                                    string colName = g.Columns[j].Name;
//                                    decimal valDec = 0;
//                                    decimal.TryParse(value.ToString(), out valDec);

//                                    if (colName == "TongTienSan") sumSan += valDec;
//                                    if (colName == "TongTienDoAn") sumDoAn += valDec;
//                                    if (colName == "TongCong") sumTong += valDec;
//                                }
//                            }

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

//                    // Ghi chữ "TỔNG CỘNG HỆ THỐNG" vào cột đầu tiên (Chi Nhánh)
//                    sheet.Cells[lastRow, 1] = "TỔNG CỘNG HỆ THỐNG";

//                    // Ghi các số tổng vào đúng cột tương ứng
//                    // Lưu ý: Phải đếm lại colIndex vì trong vòng lặp trên colIndex chạy lung tung
//                    int currentExcelCol = 1;
//                    for (int j = 0; j < g.Columns.Count; j++)
//                    {
//                        if (g.Columns[j].Visible)
//                        {
//                            string colName = g.Columns[j].Name;
//                            if (colName == "TongTienSan") sheet.Cells[lastRow, currentExcelCol] = sumSan;
//                            if (colName == "TongTienDoAn") sheet.Cells[lastRow, currentExcelCol] = sumDoAn;
//                            if (colName == "TongCong") sheet.Cells[lastRow, currentExcelCol] = sumTong;

//                            currentExcelCol++;
//                        }
//                    }

//                    // Trang trí dòng tổng: Chữ Đỏ, In Đậm, Nền Vàng nhạt
//                    Excel.Range totalRowRange = sheet.Range[sheet.Cells[lastRow, 1], sheet.Cells[lastRow, colIndex - 1]];
//                    totalRowRange.Font.Bold = true;
//                    totalRowRange.Font.Color = System.Drawing.Color.Red;
//                    totalRowRange.Interior.Color = System.Drawing.Color.LightYellow;
//                    totalRowRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
//                }

//                // 4. CĂN CHỈNH
//                sheet.Columns.AutoFit();
//                wb.SaveAs(duongDan);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                app.Quit();
//                releaseObject(wb);
//                releaseObject(sheet);
//                releaseObject(app);
//            }
//        }

//        // Hàm phụ trợ để giải phóng bộ nhớ COM (Tránh lỗi Excel chạy ngầm)
//        private void releaseObject(object obj)
//        {
//            try
//            {
//                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
//                obj = null;
//            }
//            catch (Exception ex)
//            {
//                obj = null;
//                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
//            }
//            finally
//            {
//                GC.Collect();
//            }
//        }







//        private void groupBox1_Enter(object sender, EventArgs e)
//        {

//        }

//        private void DoanhThu_Paint(object sender, PaintEventArgs e)
//        {

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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1234, 654);
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
            this.groupBox6.Size = new System.Drawing.Size(1228, 87);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1222, 50);
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
            this.groupBox2.Size = new System.Drawing.Size(299, 44);
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
            this.pnlHomNay.Size = new System.Drawing.Size(293, 15);
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
            this.groupBox3.Location = new System.Drawing.Point(308, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 44);
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
            this.pnlTuanNay.Size = new System.Drawing.Size(293, 15);
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
            this.groupBox4.Location = new System.Drawing.Point(613, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(299, 44);
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
            this.pnlThangNay.Size = new System.Drawing.Size(293, 15);
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
            this.groupBox5.Location = new System.Drawing.Point(918, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(301, 44);
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
            this.pnlTongDoanhThu.Size = new System.Drawing.Size(295, 15);
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
            this.DoanhThu.Location = new System.Drawing.Point(3, 96);
            this.DoanhThu.Name = "DoanhThu";
            this.DoanhThu.RowCount = 1;
            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248F));
            this.DoanhThu.Size = new System.Drawing.Size(1228, 230);
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
            this.groupBox7.Size = new System.Drawing.Size(853, 224);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "BIỂU ĐỒ DOANH THU NĂM";
            // 
            // chartDoanhThuNam
            // 
            this.chartDoanhThuNam.BackColor = System.Drawing.SystemColors.Control;
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
            this.chartDoanhThuNam.Size = new System.Drawing.Size(847, 195);
            this.chartDoanhThuNam.TabIndex = 2;
            this.chartDoanhThuNam.Text = "chart1";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox8.Controls.Add(this.chartTyTrong);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(862, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(363, 224);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "DOANH THU THEO DỊCH VỤ";
            // 
            // chartTyTrong
            // 
            this.chartTyTrong.BackColor = System.Drawing.SystemColors.Control;
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
            this.chartTyTrong.Size = new System.Drawing.Size(357, 195);
            this.chartTyTrong.TabIndex = 1;
            this.chartTyTrong.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 332);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1228, 135);
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1222, 106);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Cornsilk;
            this.panel3.Controls.Add(this.groupBox12);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(912, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(307, 100);
            this.panel3.TabIndex = 2;
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox12.Controls.Add(this.panel4);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox12.Location = new System.Drawing.Point(0, 0);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(307, 100);
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
            this.panel4.Size = new System.Drawing.Size(301, 71);
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
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox9.Controls.Add(this.panel2);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(410, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(496, 100);
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
            this.panel2.Size = new System.Drawing.Size(490, 71);
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
            this.groupBox10.Size = new System.Drawing.Size(401, 100);
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
            this.panel1.Size = new System.Drawing.Size(395, 71);
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
            this.groupBox11.Location = new System.Drawing.Point(3, 473);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(1228, 178);
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
            this.dgvDoanhThu.Size = new System.Drawing.Size(1222, 149);
            this.dgvDoanhThu.TabIndex = 7;
            this.dgvDoanhThu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoanhThu_CellContentClick);
            this.dgvDoanhThu.Click += new System.EventHandler(this.frmDoanhThu_Load);
            // 
            // frmdoanhthuu
            // 
            this.ClientSize = new System.Drawing.Size(1234, 654);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "frmdoanhthuu";
            this.Load += new System.EventHandler(this.frmDoanhThu_Load);
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

        private void dgvDoanhThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
