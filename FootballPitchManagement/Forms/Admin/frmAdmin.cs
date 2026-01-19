using FootballPitchManagement.Common;
using FootballPitchManagement.Forms.Admin;
using FootballPitchManagement.Forms.Customer;

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
                LoadComboBoxChiNhanh(); // ✅ THÊM LOAD COMBO BOX CHI NHÁNH
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

        // ✅ THÊM HÀM LOAD COMBO BOX CHI NHÁNH
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

                        // ✅ THÊM OPTION "TẤT CẢ CHI NHÁNH"
                        DataRow row = dt.NewRow();
                        row["MaChiNhanh"] = 0;
                        row["TenChiNhanh"] = "-- Tất cả--";
                        dt.Rows.InsertAt(row, 0);

                        cboChiNhanh.DataSource = dt;
                        cboChiNhanh.DisplayMember = "TenChiNhanh";
                        cboChiNhanh.ValueMember = "MaChiNhanh";
                        cboChiNhanh.SelectedIndex = 0;

                        // ✅ GÁN SỰ KIỆN THAY ĐỔI
                        cboChiNhanh.SelectedIndexChanged -= CboChiNhanh_SelectedIndexChanged;
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

        // ✅ SỰ KIỆN THAY ĐỔI CHI NHÁNH
        private void CboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadThongKeTongQuan();
            LoadBieuDoDoanhThuThang();
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

        // ✅ CẬP NHẬT HÀM LoadThongKeTongQuan THEO CHI NHÁNH
        private void LoadThongKeTongQuan()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // ✅ LẤY CHI NHÁNH ĐƯỢC CHỌN
                    int maChiNhanh = cboChiNhanh.SelectedValue != null ? 
                        Convert.ToInt32(cboChiNhanh.SelectedValue) : 0;

                    // ✅ SQL TÍNH DOANH THU THEO CHI NHÁNH
                    string whereChiNhanh = maChiNhanh > 0 ? " AND MaChiNhanh = @MaChiNhanh" : "";
                    
                    string sqlDoanhThu = $@"
                        SELECT (
                            ISNULL((SELECT SUM(ThanhTien) FROM HoaDon 
                                   WHERE TrangThaiThanhToan = 'DA_THANH_TOAN' {whereChiNhanh}), 0) +
                            ISNULL((SELECT SUM(TongTien) FROM HoaDonDoAn 
                                   WHERE TrangThai = 'DA_THANH_TOAN' {whereChiNhanh}), 0)
                        ) AS TongDoanhThu";

                    using (SqlCommand cmd = new SqlCommand(sqlDoanhThu, conn))
                    {
                        if (maChiNhanh > 0)
                        {
                            cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                        }

                        object result = cmd.ExecuteScalar();
                        decimal tongDoanhThu = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        lblTongDoanhThu.Text = $"{(tongDoanhThu / 1000000):N1} M";
                    }

                    // ✅ TỔNG SỐ KHÁCH HÀNG (KHÔNG LỌC THEO CHI NHÁNH VÌ KHÁCH HÀNG KHÔNG THUỘC CHI NHÁNH CỤ THỂ)
                    string sqlKH = "SELECT COUNT(*) FROM KhachHang";
                    using (SqlCommand cmd = new SqlCommand(sqlKH, conn))
                    {
                        lblSokhach.Text = cmd.ExecuteScalar().ToString();
                    }

                    // ✅ CẬP NHẬT LABEL HIỂN THỊ CHI NHÁNH ĐANG CHỌN
                    string tenChiNhanh = cboChiNhanh.Text;
                    if (maChiNhanh == 0)
                    {
                        lblTongQuan.Text = "Tổng Quan - Tất cả chi nhánh";
                    }
                    else
                    {
                        lblTongQuan.Text = $"Tổng Quan - {tenChiNhanh}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ CẬP NHẬT HÀM LoadBieuDoDoanhThuThang THEO CHI NHÁNH
        private void LoadBieuDoDoanhThuThang()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    // ✅ LẤY CHI NHÁNH ĐƯỢC CHỌN
                    int maChiNhanh = cboChiNhanh.SelectedValue != null ? 
                        Convert.ToInt32(cboChiNhanh.SelectedValue) : 0;

                    // ✅ ĐIỀU KIỆN WHERE THEO CHI NHÁNH
                    string whereChiNhanh = maChiNhanh > 0 ? " AND MaChiNhanh = @MaChiNhanh" : "";

                    string sql = $@"
                        SELECT Nam, Thang, SUM(Tien) AS TongDoanhThu
                        FROM (
                            SELECT YEAR(NgayLap) AS Nam, MONTH(NgayLap) AS Thang, ThanhTien AS Tien 
                            FROM HoaDon 
                            WHERE TrangThaiThanhToan = 'DA_THANH_TOAN' {whereChiNhanh}
                            UNION ALL
                            SELECT YEAR(NgayLap) AS Nam, MONTH(NgayLap) AS Thang, TongTien AS Tien 
                            FROM HoaDonDoAn 
                            WHERE TrangThai = 'DA_THANH_TOAN' {whereChiNhanh}
                        ) AS KetQua
                        GROUP BY Nam, Thang
                        ORDER BY Nam ASC, Thang ASC";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (maChiNhanh > 0)
                        {
                            cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        chartDoanhThu.Series[0].Points.Clear();
                        chartDoanhThu.Titles.Clear();

                        if (dt.Rows.Count == 0)
                        {
                            string titleText = maChiNhanh == 0 ? 
                                "CHƯA CÓ DỮ LIỆU DOANH THU" : 
                                $"CHƯA CÓ DỮ LIỆU DOANH THU - {cboChiNhanh.Text}";
                            
                            chartDoanhThu.Titles.Add(new Title(titleText, Docking.Top, 
                                new Font("Segoe UI", 12, FontStyle.Bold), Color.Red));
                        }
                        else
                        {
                            string titleText = maChiNhanh == 0 ? 
                                "BIỂU ĐỒ DOANH THU TẤT CẢ CHI NHÁNH" : 
                                $"BIỂU ĐỒ DOANH THU - {cboChiNhanh.Text}";
                            
                            chartDoanhThu.Titles.Add(new Title(titleText, Docking.Top, 
                                new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(52, 73, 94)));

                            foreach (DataRow row in dt.Rows)
                            {
                                int nam = Convert.ToInt32(row["Nam"]);
                                int thang = Convert.ToInt32(row["Thang"]);
                                decimal doanhThu = Convert.ToDecimal(row["TongDoanhThu"]);
                                decimal doanhThuTrieu = doanhThu / 1000000;

                                string label = $"T{thang}/{nam}";
                                int pointIndex = chartDoanhThu.Series[0].Points.AddXY(label, doanhThuTrieu);

                                chartDoanhThu.Series[0].Points[pointIndex].ToolTip = 
                                    $"Thời gian: {label}\nDoanh thu: {doanhThu:N0} VNĐ";
                            }

                            chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi load biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ✅ HÀM HIỂN THỊ TRANG CHỦ - SỬA LẠI   
        private void ShowHomePage()
        {
            try
            {
                // 1. Đóng form con
                if (currentFormChild != null)
                {
                    this.Controls.Remove(currentFormChild);
                    currentFormChild.Close();
                    currentFormChild.Dispose();
                    currentFormChild = null;
                }

                // 2. Hiện lại TableLayoutPanel chính
                tlpMainAdmin.Visible = true;
                tlpMainAdmin.BringToFront();
                
                // 3. Refresh dữ liệu
                LoadThongKeTongQuan();
                LoadBieuDoDoanhThuThang();
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
                    currentFormChild.Close();
                    currentFormChild.Dispose();
                    currentFormChild = null;
                }

                // 2. Ẩn toàn bộ TableLayoutPanel chính
                tlpMainAdmin.Visible = false;

                // 3. Thiết lập form con
                currentFormChild = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // 4. Thêm trực tiếp vào form chính (không qua TableLayoutPanel)
                this.Controls.Add(childForm);
                childForm.BringToFront();
                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form: {ex.Message}", "Lỗi", 
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
                if (currentFormChild is frmQuanLyDatSan)
                {
                    return;
                }

                OpenChildForm(new frmQuanLyDatSan());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở Quản lý đặt sân: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new frmdoanhthuu()); 
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBaocao()); 
        }

        private void tlpMainAdmin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCaidat());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyDoAn());
        }
    }
}

