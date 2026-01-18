using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmHoaDon1 : Form
    {
        // 1. CẤU HÌNH KẾT NỐI
        string connectionString = @"Data Source=LAPTOP-BV9HL7MV;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";
        bool isLoaded = false;

        public frmHoaDon1()
        {
            InitializeComponent();
        }

        private void frmHoaDon1_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu Combobox
            LoadComboBoxData();
            LoadSortOptions();
            LoadCboTrangThai();

            // Gắn sự kiện tự động lọc
            cboChiNhanh.SelectedIndexChanged += (s, ev) => LoadDataToGrid();
            cboLoaiSan.SelectedIndexChanged += (s, ev) => LoadDataToGrid();
            cboSapXep.SelectedIndexChanged += (s, ev) => LoadDataToGrid();
            cboTrangThai.SelectedIndexChanged += cboTrangThai_SelectedIndexChanged;

            // Xử lý giao diện đẹp & Dãn khung
            dgvHoaDon.CellFormatting += DgvHoaDon_CellFormatting;
            LamDepGiaoDien(); // <--- HÀM QUAN TRỌNG ĐỂ DÃN KHUNG

            isLoaded = true;
            LoadDataToGrid();
        }

        // =================================================================================
        // HÀM LÀM ĐẸP & TỰ ĐỘNG DÃN KHUNG (AUTO RESIZE)
        // =================================================================================
        private void LamDepGiaoDien()
        {
            // 1. Font chữ chuẩn
            System.Drawing.Font fontTieuDe = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            System.Drawing.Font fontNoiDung = new System.Drawing.Font("Segoe UI", 11, FontStyle.Regular);
            Color mauChuLabel = Color.FromArgb(64, 64, 64); // Xám đậm

            // 2. Chỉnh các ComboBox to rõ
            Control[] danhSachCombo = { cboLoaiSan, cboChiNhanh, cboTrangThai, cboSapXep };
            foreach (Control cbo in danhSachCombo)
            {
                if (cbo != null)
                {
                    cbo.Font = fontNoiDung;
                    cbo.BackColor = Color.White;
                    cbo.Height = 32;
                }
            }

            // 3. Chỉnh Label (trừ 2 cái hiển thị tiền to đùng)
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    if (c.Name.Contains("Tong") || c.Text.Contains("VNĐ") || c.Text.Contains("THANH TOÁN"))
                        continue;

                    c.Font = fontTieuDe;
                    c.ForeColor = mauChuLabel;
                }
            }

            // 4. XỬ LÝ LƯỚI DỮ LIỆU (QUAN TRỌNG NHẤT)

            // a. Màu sắc
            dgvHoaDon.BackgroundColor = Color.White;
            dgvHoaDon.BorderStyle = BorderStyle.None;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 102, 204); // Xanh dương đậm
            dgvHoaDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.ColumnHeadersHeight = 45; // Tiêu đề cao thoáng

            dgvHoaDon.RowTemplate.Height = 35; // Dòng dữ liệu cao dễ đọc
            dgvHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvHoaDon.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 250, 255); // Màu so le nhẹ

            // b. TỰ ĐỘNG DÃN RA THEO KHUNG (Anchor)
            // Lệnh này giúp lưới dính chặt vào 4 góc màn hình khi phóng to
            dgvHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // c. TỰ ĐỘNG DÃN CỘT (AutoSize)
            // Lệnh này giúp các cột bên trong tự chia đều, lấp đầy khoảng trống
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =================================================================================
        // CÁC HÀM XỬ LÝ DỮ LIỆU (GIỮ NGUYÊN)
        // =================================================================================
        private void LoadDataToGrid()
        {
            if (!isLoaded) return;
            try
            {
                int maChiNhanh = (cboChiNhanh.SelectedValue is int) ? (int)cboChiNhanh.SelectedValue : 0;
                int maLoaiSan = (cboLoaiSan.SelectedValue is int) ? (int)cboLoaiSan.SelectedValue : 0;
                string sortOption = (cboSapXep.SelectedValue != null) ? cboSapXep.SelectedValue.ToString() : "DATE_DESC";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            HD.MaHoaDon AS [Mã HĐ],
                            HD.NgayLap AS [Ngày Lập],
                            KH.HoTen AS [Khách Hàng],
                            S.TenSan AS [Tên Sân],
                            LS.TenLoaiSan AS [Loại Sân],
                            HD.TongTienSan AS [Tiền Sân],
                            ISNULL(HD.TongTienDoAn, 0) AS [Tiền Đồ Ăn],
                            HD.ThanhTien AS [Tổng Cộng], 
                            HD.TrangThaiThanhToan AS [Trạng Thái] 
                        FROM HoaDon HD
                        LEFT JOIN LichDatSan LDS ON HD.MaDatSan = LDS.MaDatSan
                        LEFT JOIN San S ON LDS.MaSan = S.MaSan
                        LEFT JOIN LoaiSan LS ON S.MaLoaiSan = LS.MaLoaiSan
                        LEFT JOIN KhachHang KH ON HD.MaKH = KH.MaKH
                        WHERE 1=1 ";

                    if (maChiNhanh > 0) sql += " AND HD.MaChiNhanh = @MaChiNhanh";
                    if (maLoaiSan > 0) sql += " AND S.MaLoaiSan = @MaLoaiSan";

                    switch (sortOption)
                    {
                        case "DATE_ASC": sql += " ORDER BY HD.NgayLap ASC"; break;
                        case "PRICE_DESC": sql += " ORDER BY HD.ThanhTien DESC"; break;
                        case "PRICE_ASC": sql += " ORDER BY HD.ThanhTien ASC"; break;
                        default: sql += " ORDER BY HD.NgayLap DESC"; break;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    if (maChiNhanh > 0) da.SelectCommand.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                    if (maLoaiSan > 0) da.SelectCommand.Parameters.AddWithValue("@MaLoaiSan", maLoaiSan);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHoaDon.DataSource = dt;

                    // Định dạng tiền tệ sau khi load xong
                    string[] cotTien = { "Tiền Sân", "Tiền Đồ Ăn", "Tổng Cộng" };
                    foreach (string col in cotTien)
                    {
                        if (dgvHoaDon.Columns.Contains(col))
                        {
                            dgvHoaDon.Columns[col].DefaultCellStyle.Format = "N0";
                            dgvHoaDon.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                    if (dgvHoaDon.Columns.Contains("Tổng Cộng"))
                    {
                        dgvHoaDon.Columns["Tổng Cộng"].DefaultCellStyle.ForeColor = Color.Red;
                        dgvHoaDon.Columns["Tổng Cộng"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }

                    CalculateTotals(dt);
                    ApplyStatusFilter();
                }
            }
            catch { }
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void ApplyStatusFilter()
        {
            if (cboTrangThai.SelectedValue == null) return;
            DataTable dt = dgvHoaDon.DataSource as DataTable;

            if (dt != null)
            {
                try
                {
                    string giaTriChon = cboTrangThai.SelectedValue.ToString();
                    if (giaTriChon == "1")
                        dt.DefaultView.RowFilter = "[Trạng Thái] = 'DA_THANH_TOAN' OR [Trạng Thái] = 'DA'";
                    else if (giaTriChon == "0")
                        dt.DefaultView.RowFilter = "[Trạng Thái] <> 'DA_THANH_TOAN' AND [Trạng Thái] <> 'DA' OR [Trạng Thái] IS NULL";
                    else
                        dt.DefaultView.RowFilter = "";
                }
                catch { dt.DefaultView.RowFilter = ""; }
            }
        }

        private void CalculateTotals(DataTable dt)
        {
            decimal daThanhToan = 0;
            decimal choThanhToan = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Tổng Cộng"] != DBNull.Value)
                {
                    decimal tien = Convert.ToDecimal(row["Tổng Cộng"]);
                    string trangThai = row["Trạng Thái"].ToString();

                    if (trangThai == "DA_THANH_TOAN" || trangThai == "DA")
                        daThanhToan += tien;
                    else
                        choThanhToan += tien;
                }
            }
            if (lblTongDaThanhToan != null) lblTongDaThanhToan.Text = daThanhToan.ToString("N0") + " VNĐ";
            if (lblTongChoThanhToan != null) lblTongChoThanhToan.Text = choThanhToan.ToString("N0") + " VNĐ";
        }

        private void DgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "Trạng Thái" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "DA_THANH_TOAN" || status == "DA")
                {
                    e.Value = "Đã thanh toán";
                    e.CellStyle.ForeColor = Color.ForestGreen;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else
                {
                    e.Value = "Chờ thanh toán";
                    e.CellStyle.ForeColor = Color.OrangeRed;
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold | FontStyle.Italic);
                }
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter daCN = new SqlDataAdapter("SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh", conn);
                    DataTable dtCN = new DataTable(); daCN.Fill(dtCN);
                    DataRow rowCN = dtCN.NewRow(); rowCN["MaChiNhanh"] = 0; rowCN["TenChiNhanh"] = "--- Tất cả chi nhánh ---";
                    dtCN.Rows.InsertAt(rowCN, 0);
                    cboChiNhanh.DataSource = dtCN; cboChiNhanh.DisplayMember = "TenChiNhanh"; cboChiNhanh.ValueMember = "MaChiNhanh";

                    SqlDataAdapter daLS = new SqlDataAdapter("SELECT MaLoaiSan, TenLoaiSan FROM LoaiSan", conn);
                    DataTable dtLS = new DataTable(); daLS.Fill(dtLS);
                    DataRow rowLS = dtLS.NewRow(); rowLS["MaLoaiSan"] = 0; rowLS["TenLoaiSan"] = "--- Tất cả loại sân ---";
                    dtLS.Rows.InsertAt(rowLS, 0);
                    cboLoaiSan.DataSource = dtLS; cboLoaiSan.DisplayMember = "TenLoaiSan"; cboLoaiSan.ValueMember = "MaLoaiSan";
                }
            }
            catch { }
        }

        private void LoadSortOptions()
        {
            DataTable dtSort = new DataTable();
            dtSort.Columns.Add("Value"); dtSort.Columns.Add("Display");
            dtSort.Rows.Add("DATE_DESC", "Mới nhất (Mặc định)");
            dtSort.Rows.Add("DATE_ASC", "Cũ nhất");
            dtSort.Rows.Add("PRICE_DESC", "Tổng tiền: Cao -> Thấp");
            dtSort.Rows.Add("PRICE_ASC", "Tổng tiền: Thấp -> Cao");
            cboSapXep.DataSource = dtSort; cboSapXep.DisplayMember = "Display"; cboSapXep.ValueMember = "Value";
        }

        private void LoadCboTrangThai()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("HienThi", typeof(string));
            dt.Columns.Add("GiaTri", typeof(int));
            dt.Rows.Add("Chờ thanh toán", 0);
            dt.Rows.Add("Đã thanh toán", 1);
            cboTrangThai.DataSource = dt;
            cboTrangThai.DisplayMember = "HienThi";
            cboTrangThai.ValueMember = "GiaTri";
            cboTrangThai.SelectedIndex = 0;
        }

        // CÁC SỰ KIỆN RỖNG (GIỮ ĐỂ TRÁNH LỖI)
        private void btnLoc_Click(object sender, EventArgs e) { }
        private void btnLoc_Click_1(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void lblTongDaThanhToan_Click(object sender, EventArgs e) { }
        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cboLoaiSan_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}