using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FootballPitchManagement.Common;

namespace FootballPitchManagement
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                // Test kết nối
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    return;
                }

                // Cấu hình chart
                CauHinhChart();
                
                // Load dữ liệu thống kê
                LoadThongKeTongQuan();
                
                // Load biểu đồ doanh thu
                LoadBieuDoDoanhThuThang();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CauHinhChart()
        {
            try
            {
                // Xóa series mặc định nếu có
                chartDoanhThu.Series.Clear();

                // Cấu hình Chart Area
                chartDoanhThu.ChartAreas[0].BackColor = Color.White;
                chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
                chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
                chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (Triệu đồng)";
                chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;

                // Tạo Series mới
                Series series = new Series("Doanh thu");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(138, 43, 226); // Màu xanh lá
                series.IsValueShownAsLabel = true;
                series.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                series.LabelFormat = "N1";
                chartDoanhThu.Series.Add(series);

                // Cấu hình Legend
                chartDoanhThu.Legends[0].Docking = Docking.Top;
                chartDoanhThu.Legends[0].Font = new Font("Segoe UI", 9, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cấu hình chart: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeTongQuan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    DateTime dauThang = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime cuoiThang = dauThang.AddMonths(1).AddDays(-1);

                        // ✅ 1. TỔNG DOANH THU TOÀN BỘ (TẤT CẢ THỜI GIAN)
                    string sqlDoanhThu = @"
                        SELECT ISNULL(SUM(SoTien), 0) AS TongDoanhThu
                        FROM DoanhThu";

                    using (SqlCommand cmd = new SqlCommand(sqlDoanhThu, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal tongDoanhThu = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        
                        // Hiển thị dưới dạng "75.6 M"
                        lblTongDoanhThu.Text = $"{(tongDoanhThu / 1000000):N1} M";
                    }

                    // ✅ 2. TỔNG LƯỢT ĐẶT SÂN
                    string sqlLuotDat = @"
                        SELECT COUNT(*) 
                        FROM LichDatSan
                        WHERE TrangThai != N'DA_HUY'";

                    using (SqlCommand cmd = new SqlCommand(sqlLuotDat, conn))
                    {
                        lblSoluotdat.Text = cmd.ExecuteScalar().ToString();
                    }

                    // ✅ 3. TỔNG KHÁCH HÀNG
                    string sqlKH = "SELECT COUNT(*) FROM KhachHang";
                    using (SqlCommand cmd = new SqlCommand(sqlKH, conn))
                    {
                        lblSokhach.Text = cmd.ExecuteScalar().ToString();
                    }

                    // ✅ 4. TỶ LỆ LẤP ĐẦY (THÁNG HIỆN TẠI)
                    string sqlTyLe = @"
                        SELECT 
                            COUNT(DISTINCT l.MaSan) AS SoSanDaDat,
                            (SELECT COUNT(*) FROM San WHERE TrangThai = 1) AS TongSan
                        FROM LichDatSan l
                        WHERE l.TrangThai != N'DA_HUY'
                          AND l.NgayDat BETWEEN @DauThang AND @CuoiThang";

                    using (SqlCommand cmd = new SqlCommand(sqlTyLe, conn))
                    {
                        cmd.Parameters.AddWithValue("@DauThang", dauThang);
                        cmd.Parameters.AddWithValue("@CuoiThang", cuoiThang);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int sanDaDat = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                int tongSan = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                int tyLe = tongSan > 0 ? (sanDaDat * 100 / tongSan) : 0;
                                lblPhamtram.Text = $"{tyLe}%";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load thống kê: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    // Hiển thị giá trị mặc định
                    lblTongDoanhThu.Text = "0 M";
                    lblSoluotdat.Text = "0";
                    lblSokhach.Text = "0";
                    lblPhamtram.Text = "0%";
                }
            }
        }

        private void LoadBieuDoDoanhThuThang()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // ✅ QUERY ĐÚNG: Lấy doanh thu 12 tháng gần nhất (theo năm 2023)
                    string sql = @"
                        SELECT 
                            MONTH(Ngay) AS Thang,
                            SUM(SoTien) AS TongDoanhThu
                        FROM DoanhThu
                        WHERE YEAR(Ngay) = YEAR(GETDATE())
                        GROUP BY MONTH(Ngay)
                        ORDER BY MONTH(Ngay)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // ✅ HIỂN THỊ DỮ LIỆU THẬT (không dùng mẫu nữa)
                            if (chartDoanhThu.Series.Count > 0)
                            {
                                chartDoanhThu.Series[0].Points.Clear();
                            }

                            if (dt.Rows.Count == 0)
                            {
                                // Nếu không có dữ liệu năm nay, hiển thị thông báo
                                chartDoanhThu.Titles.Clear();
                                chartDoanhThu.Titles.Add(new Title(
                                    "CHƯA CÓ DỮ LIỆU DOANH THU NĂM NAY",
                                    Docking.Top,
                                    new Font("Segoe UI", 12, FontStyle.Bold),
                                    Color.Red
                                ));
                                return;
                            }

                            // Thêm dữ liệu THẬT từ database
                            foreach (DataRow row in dt.Rows)
                            {
                                int thang = Convert.ToInt32(row["Thang"]);
                                decimal doanhThu = Convert.ToDecimal(row["TongDoanhThu"]);
                                decimal doanhThuTrieu = doanhThu / 1000000;
                                
                                int pointIndex = chartDoanhThu.Series[0].Points.AddXY(thang, doanhThuTrieu);
                                
                                // Tooltip
                                chartDoanhThu.Series[0].Points[pointIndex].ToolTip = 
                                    $"Tháng {thang}\nDoanh thu: {doanhThu:N0} đ";
                            }

                            // Đặt tiêu đề
                            chartDoanhThu.Titles.Clear();
                            chartDoanhThu.Titles.Add(new Title(
                                $"BIỂU ĐỒ DOANH THU NĂM {DateTime.Now.Year}",
                                Docking.Top,
                                new Font("Segoe UI", 12, FontStyle.Bold),
                                Color.FromArgb(52, 73, 94)
                            ));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load biểu đồ: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            // Refresh dữ liệu
            LoadThongKeTongQuan();
            LoadBieuDoDoanhThuThang();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // Không cần xử lý
        }
    }
}

