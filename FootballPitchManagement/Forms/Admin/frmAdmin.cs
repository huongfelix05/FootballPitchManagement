using FootballPitchManagement.Common;
using FootballPitchManagement.Forms.Admin;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FootballPitchManagement
{
    public partial class frmAdmin : Form
    {
        private Form currentFormChild;

        public frmAdmin()
        {
            InitializeComponent();
            this.Load += frmAdmin_Load;
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    return;
                }

                CauHinhChart();
                LoadThongKeTongQuan();
                LoadBieuDoDoanhThuThang();
                ShowHomePage();
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
                chartDoanhThu.Series.Clear();
                chartDoanhThu.ChartAreas[0].BackColor = Color.White;
                chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
                chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
                chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (Triệu đồng)";
                chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;

                Series series = new Series("Doanh thu");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(138, 43, 226);
                series.IsValueShownAsLabel = true;
                series.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                series.LabelFormat = "N1";
                chartDoanhThu.Series.Add(series);

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

                    string sqlDoanhThu = @"SELECT ISNULL(SUM(SoTien), 0) AS TongDoanhThu FROM DoanhThu";
                    using (SqlCommand cmd = new SqlCommand(sqlDoanhThu, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        decimal tongDoanhThu = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        lblTongDoanhThu.Text = $"{(tongDoanhThu / 1000000):N1} M";
                    }

                    string sqlLuotDat = @"SELECT COUNT(*) FROM LichDatSan WHERE TrangThai != N'DA_HUY'";
                    using (SqlCommand cmd = new SqlCommand(sqlLuotDat, conn))
                    {
                        lblSoluotdat.Text = cmd.ExecuteScalar().ToString();
                    }

                    string sqlKH = "SELECT COUNT(*) FROM KhachHang";
                    using (SqlCommand cmd = new SqlCommand(sqlKH, conn))
                    {
                        lblSokhach.Text = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load thống kê: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    lblTongDoanhThu.Text = "0 M";
                    lblSoluotdat.Text = "0";
                    lblSokhach.Text = "0";
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

                            if (chartDoanhThu.Series.Count > 0)
                            {
                                chartDoanhThu.Series[0].Points.Clear();
                            }

                            if (dt.Rows.Count == 0)
                            {
                                chartDoanhThu.Titles.Clear();
                                chartDoanhThu.Titles.Add(new Title(
                                    "CHƯA CÓ DỮ LIỆU DOANH THU NĂM NAY",
                                    Docking.Top,
                                    new Font("Segoe UI", 12, FontStyle.Bold),
                                    Color.Red
                                ));
                                return;
                            }

                            foreach (DataRow row in dt.Rows)
                            {
                                int thang = Convert.ToInt32(row["Thang"]);
                                decimal doanhThu = Convert.ToDecimal(row["TongDoanhThu"]);
                                decimal doanhThuTrieu = doanhThu / 1000000;
                                
                                int pointIndex = chartDoanhThu.Series[0].Points.AddXY(thang, doanhThuTrieu);
                                chartDoanhThu.Series[0].Points[pointIndex].ToolTip = 
                                    $"Tháng {thang}\nDoanh thu: {doanhThu:N0} đ";
                            }

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
                    MessageBox.Show($"Lỗi load biểu đồ: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ HÀM HIỂN THỊ TRANG CHỦ
        private void ShowHomePage()
        {
            try
            {
                // Đóng form con nếu có
                if (currentFormChild != null)
                {
                    tlpMainAdmin.Controls.Remove(currentFormChild);
                    currentFormChild.Close();
                    currentFormChild.Dispose();
                    currentFormChild = null;
                }

                // HIỆN LẠI TẤT CẢ CONTROL DASHBOARD (theo tên từ Designer)
                pnlTopBa.Visible = true;
                pnlTongdoanhthu.Visible = true;
                pnlLuotdatsan.Visible = true;
                pnlKhachhang.Visible = true;
                chartDoanhThu.Visible = true;

                // Đưa lên trên cùng
                pnlTopBa.BringToFront();
                chartDoanhThu.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị trang chủ: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ HÀM MỞ FORM CON (FIXED)
        private void OpenChildForm(Form childForm)
        {
            try
            {
                // 1. Đóng form con cũ
                if (currentFormChild != null)
                {
                    tlpMainAdmin.Controls.Remove(currentFormChild);
                    currentFormChild.Close();
                    currentFormChild.Dispose();
                }

                // 2. ẨN TẤT CẢ CONTROL DASHBOARD (theo tên từ Designer)
                pnlTopBa.Visible = false;
                pnlTongdoanhthu.Visible = false;
                pnlLuotdatsan.Visible = false;
                pnlKhachhang.Visible = false;
                chartDoanhThu.Visible = false;

                // 3. THIẾT LẬP FORM CON MỚI
                currentFormChild = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // 4. THÊM VÀO tlpMainAdmin
                tlpMainAdmin.Controls.Add(childForm);
                tlpMainAdmin.SetColumnSpan(childForm, 3); // Chiếm cả 3 cột
                tlpMainAdmin.SetRowSpan(childForm, 3);    // Chiếm cả 3 row
                childForm.BringToFront();
                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NÚT QUẢN LÝ SÂN
        private void btnQuanLySan_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFormChild is frmQuanlysan)
                {
                    return;
                }

                OpenChildForm(new frmQuanlysan());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở Quản lý sân: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ NÚT TRANG CHỦ
        private void btnHome_Click(object sender, EventArgs e)
        {
            ShowHomePage();
        }

        // ✅ NÚT TỔNG QUAN
        private void btnTongQuan_Click(object sender, EventArgs e)
        {
            ShowHomePage();
            LoadThongKeTongQuan();
            LoadBieuDoDoanhThuThang();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
        }

        private void btnQuanLyLichDat_Click(object sender, EventArgs e)
        {
        }

        private void lblTongQuan_Click(object sender, EventArgs e)
        {
            ShowHomePage();
        }

        private void btnQuanLyDatSan_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFormChild is frmQuanlysan)
                {
                    return;
                }

                OpenChildForm(new frmQuanlysan());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở Quản lý sân: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

