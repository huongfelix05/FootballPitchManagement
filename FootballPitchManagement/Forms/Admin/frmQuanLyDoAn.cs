using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmQuanLyDoAn : Form
    {
        string strKetNoi = @"Data Source=LAPTOP-BV9HL7MV;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";
        public frmQuanLyDoAn()
        {
            InitializeComponent();
        }

        private void frmQuanLyDoAn_Load(object sender, EventArgs e)
        {
            LoadComboBoxChiNhanh();
            LoadDuLieuLenLuoi(0); // Mặc định load tất cả
            LoadTopBanChay();
        }
        private void LoadDuLieuLenLuoi(int maNhomFilter = 0)
        {
            // Lấy ID chi nhánh đang chọn. Nếu chưa chọn gì (lúc mới mở) thì mặc định là 1.
            int maChiNhanh = 1;
            if (cboChiNhanh.SelectedValue != null)
            {
                int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maChiNhanh);
            }

            guna2DataGridView1.Rows.Clear();
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();

                    // SQL LOGIC MỚI:
                    // 1. Chỉ lấy sản phẩm thuộc về chi nhánh này (d.MaChiNhanh = @MaCN) 
                    //    HOẶC sản phẩm dùng chung (d.MaChiNhanh IS NULL) nhưng ĐÃ CÓ trong kho chi nhánh này (k.MaChiNhanh = @MaCN)
                    string sql = @"
                SELECT 
                    d.MaHang, d.HinhAnh, d.TenHang, n.TenNhom, d.GiaBan, 
                    ISNULL(k.SoLuongTon, 0) as SoLuongTon, 
                    ISNULL(k.SoLuongToiThieu, 10) as SoLuongToiThieu, -- Lấy số tối thiểu
                    ISNULL(s.DaBan, 0) as DaBan,
                    
                    -- Logic Trạng Thái: Hết hàng (<=0) thì ép Ngừng bán, còn lại theo cài đặt
                    CASE WHEN ISNULL(k.SoLuongTon, 0) <= 0 THEN 0 ELSE d.TrangThai END AS TrangThai, 

                    d.MaNhomHang, d.GiaNhap, d.DonViTinh, d.MoTa
                FROM DanhMucHang d
                LEFT JOIN NhomHang n ON d.MaNhomHang = n.MaNhom
                LEFT JOIN KhoHang k ON d.MaHang = k.MaHang AND k.MaChiNhanh = @MaCN
                LEFT JOIN (
                    SELECT MaHang, SUM(SoLuong) as DaBan
                    FROM ChiTietHoaDonDoAn c
                    JOIN HoaDonDoAn h ON c.MaHoaDonDoAn = h.MaHoaDonDoAn
                    WHERE h.MaChiNhanh = @MaCN
                    GROUP BY MaHang
                ) s ON d.MaHang = s.MaHang
                
                WHERE (@MaNhom = 0 OR d.MaNhomHang = @MaNhom)
                AND d.MaNhomHang IN (1, 2) -- Chỉ lấy Đồ ăn/uống
                -- ĐIỀU KIỆN LỌC RIÊNG BIỆT CHO CHI NHÁNH:
                AND (d.MaChiNhanh = @MaCN OR (d.MaChiNhanh IS NULL AND k.MaChiNhanh = @MaCN))";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaNhom", maNhomFilter);
                    cmd.Parameters.AddWithValue("@MaCN", maChiNhanh); // Truyền mã chi nhánh vào

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        // ... (Đoạn xử lý ảnh và ID SP001 giữ nguyên) ...
                        Image img = null;
                        string tenAnh = dr["HinhAnh"].ToString();
                        string path = Path.Combine(Application.StartupPath, "Images", tenAnh);
                        if (!string.IsNullOrEmpty(tenAnh) && File.Exists(path)) img = Image.FromFile(path);

                        int maSo = int.Parse(dr["MaHang"].ToString());
                        string maHienThi = "SP" + maSo.ToString("D3");
                        bool trangThai = Convert.ToBoolean(dr["TrangThai"]);

                        guna2DataGridView1.Rows.Add(
                            maHienThi, img, dr["TenHang"], dr["TenNhom"],
                            Convert.ToDecimal(dr["GiaBan"]).ToString("N0"),
                            dr["SoLuongTon"],
                            dr["DaBan"],
                            trangThai ? "Đang bán" : "Ngừng bán",
                            Properties.Resources.btn_edit,
                            Properties.Resources.btn_delete,
                            // CỘT ẨN:
                            dr["MaNhomHang"], tenAnh, dr["GiaNhap"], dr["DonViTinh"], dr["MoTa"],
                            dr["SoLuongToiThieu"] // <--- THÊM CỘT ẨN THỨ 15: SỐ LƯỢNG TỐI THIỂU
                        );
                    }
                    dr.Close();
                }
                catch (Exception) { }
            }
            CapNhatSoLuongTrenNut();
            CapNhatThongKe();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadComboBoxChiNhanh()
        {
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT MaChiNhanh, TenChiNhanh FROM ChiNhanh", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboChiNhanh.DataSource = dt;
                    cboChiNhanh.DisplayMember = "TenChiNhanh";
                    cboChiNhanh.ValueMember = "MaChiNhanh";

                    // Mặc định chọn Chi nhánh đầu tiên (thường là Q1)
                    if (dt.Rows.Count > 0) cboChiNhanh.SelectedIndex = 0;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 1. Gán icon cho nút Sửa/Xóa
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnSua") e.Value = Properties.Resources.btn_edit;
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnXoa") e.Value = Properties.Resources.btn_delete;

            // 2. Xử lý cột TRẠNG THÁI (Index 7)
            if (e.ColumnIndex == 7 && e.Value != null)
            {
                string trangThai = e.Value.ToString();
                if (trangThai == "Đang bán")
                {
                    e.CellStyle.BackColor = Color.FromArgb(220, 255, 220); // Xanh nhạt
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (trangThai == "Ngừng bán")
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 220, 220); // Đỏ nhạt
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
                }
            }

            // 3. Xử lý cột TỒN KHO (Index 5) - MỚI CẬP NHẬT
            if (e.ColumnIndex == 5 && e.Value != null)
            {
                int tonKho = 0;
                int.TryParse(e.Value.ToString(), out tonKho);

                // Lấy Số Lượng Tối Thiểu từ cột ẩn (Cột 15)
                // Lưu ý: e.RowIndex phải hợp lệ
                int slToiThieu = 10; // Mặc định
                if (e.RowIndex >= 0 && guna2DataGridView1.Rows[e.RowIndex].Cells.Count > 15)
                {
                    var cellMin = guna2DataGridView1.Rows[e.RowIndex].Cells[15].Value;
                    if (cellMin != null) int.TryParse(cellMin.ToString(), out slToiThieu);
                }

                // 1. HẾT HÀNG (ĐỎ)
                if (tonKho <= 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                // 2. SẮP HẾT (VÀNG/CAM) -> Khi Tồn <= Tối thiểu và Tồn > 0
                else if (tonKho <= slToiThieu)
                {
                    e.CellStyle.ForeColor = Color.Orange; // Màu vàng cam cho dễ nhìn trên nền trắng
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                // 3. BÌNH THƯỜNG
                else
                {
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            frmThemSanPham f = new frmThemSanPham();
            // Cũng phải truyền ID Chi Nhánh để biết thêm hàng vào kho nào
            f.maChiNhanhLamViec = Convert.ToInt32(cboChiNhanh.SelectedValue);

            if (f.ShowDialog() == DialogResult.OK) LoadDuLieuLenLuoi();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            // NÚT SỬA
            if (e.RowIndex < 0) return;
            // --- NÚT SỬA ---
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnSua")
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                // Lấy dữ liệu cơ bản
                string idString = row.Cells[0].Value.ToString();
                int id = int.Parse(idString.Replace("SP", ""));
                string ten = row.Cells[2].Value.ToString();
                decimal giaBan = Convert.ToDecimal(row.Cells[4].Value);
                int sl = Convert.ToInt32(row.Cells[5].Value);

                // Lấy trạng thái (Chữ "Đang bán" hoặc "Ngừng bán")
                string trangThai = row.Cells[7].Value.ToString();

                // LẤY DỮ LIỆU TỪ CỘT ẨN (Kiểm tra kỹ thứ tự cột trong Design của bạn)
                // Cột 10: MaNhom, 11: TenAnh, 12: GiaNhap, 13: DonVi, 14: MoTa

                int maNhom = Convert.ToInt32(row.Cells[10].Value); // Đây là cái quan trọng để ComboBox chọn đúng loại

                string tenAnh = row.Cells[11].Value != null ? row.Cells[11].Value.ToString() : "";
                decimal giaNhap = row.Cells[12].Value != DBNull.Value ? Convert.ToDecimal(row.Cells[12].Value) : 0;
                string donVi = row.Cells[13].Value != null ? row.Cells[13].Value.ToString() : "";

                // Lấy Mô tả (Cột 14)
                string moTa = "";
                if (row.Cells.Count > 14 && row.Cells[14].Value != null)
                {
                    moTa = row.Cells[14].Value.ToString();
                }

                int slToiThieu = 10; // mặc định
                if (row.Cells.Count > 15 && row.Cells[15].Value != null)
                    slToiThieu = Convert.ToInt32(row.Cells[15].Value);

                frmThemSanPham f = new frmThemSanPham();

                f.maChiNhanhLamViec = Convert.ToInt32(cboChiNhanh.SelectedValue);
                // Truyền dữ liệu sang form con
                f.NapDuLieuSua(id, ten, maNhom, giaBan, giaNhap, sl, slToiThieu, trangThai, tenAnh, donVi, moTa);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadDuLieuLenLuoi(0);
                }
            }

            // NÚT XOÁ
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnXoa")
            {
                if (MessageBox.Show("Xoá vĩnh viễn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string idString = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    int id = int.Parse(idString.Replace("SP", "")); // Cắt chữ SP

                    using (SqlConnection conn = new SqlConnection(strKetNoi))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM KhoHang WHERE MaHang=@id; DELETE FROM DanhMucHang WHERE MaHang=@id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    LoadDuLieuLenLuoi(0);
                }
            }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadDuLieuLenLuoi(0); // 0 = Tất cả
        }

        private void btnDoAn_Click(object sender, EventArgs e)
        {
            LoadDuLieuLenLuoi(2); // Giả sử ID 2 là Đồ Ăn
        }

        private void btnDoUong_Click(object sender, EventArgs e)
        {
            LoadDuLieuLenLuoi(1); // Giả sử ID 1 là Đồ Uống
        }
        private void CapNhatSoLuongTrenNut()
        {
            // 1. Lấy Chi Nhánh đang chọn
            int maChiNhanh = 1;
            if (cboChiNhanh.SelectedValue != null)
                int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maChiNhanh);

            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();

                    // QUAN TRỌNG: Câu điều kiện lọc (Copy y hệt từ hàm LoadDuLieuLenLuoi sang)
                    // Nghĩa là: Chỉ đếm những món thuộc chi nhánh này HOẶC món dùng chung đã có trong kho này
                    string dieuKienLoc = @"
                FROM DanhMucHang d
                LEFT JOIN KhoHang k ON d.MaHang = k.MaHang AND k.MaChiNhanh = @MaCN
                WHERE d.MaNhomHang IN (1, 2) -- Chỉ lấy Đồ ăn/uống
                AND (d.MaChiNhanh = @MaCN OR (d.MaChiNhanh IS NULL AND k.MaChiNhanh = @MaCN))";

                    // --- 1. ĐẾM TỔNG SỐ (Tất cả) ---
                    string sqlAll = "SELECT COUNT(d.MaHang) " + dieuKienLoc;
                    SqlCommand cmdAll = new SqlCommand(sqlAll, conn);
                    cmdAll.Parameters.AddWithValue("@MaCN", maChiNhanh);

                    int tong = (int)cmdAll.ExecuteScalar();
                    btnTatCa.Text = $"Tất Cả ({tong})";

                    // --- 2. ĐẾM TỪNG NHÓM (Đồ ăn / Đồ uống) ---
                    // Reset về 0 trước
                    btnDoUong.Text = "Đồ Uống (0)";
                    btnDoAn.Text = "Đồ Ăn (0)";

                    string sqlGroup = "SELECT d.MaNhomHang, COUNT(d.MaHang) " + dieuKienLoc + " GROUP BY d.MaNhomHang";
                    SqlCommand cmdGroup = new SqlCommand(sqlGroup, conn);
                    cmdGroup.Parameters.AddWithValue("@MaCN", maChiNhanh);

                    SqlDataReader dr = cmdGroup.ExecuteReader();
                    while (dr.Read())
                    {
                        int maNhom = dr.GetInt32(0);
                        int soLuong = dr.GetInt32(1);

                        if (maNhom == 1) btnDoUong.Text = $"Đồ Uống ({soLuong})";
                        else if (maNhom == 2) btnDoAn.Text = $"Đồ Ăn ({soLuong})";
                    }
                    dr.Close();
                }
                catch (Exception)
                {
                    // Không làm gì hoặc log lỗi
                }
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadTopBanChay()
        {
            // Xóa danh sách cũ đi để load mới
            flowBanChay.Controls.Clear();
            int maChiNhanh = 1;
            if (cboChiNhanh.SelectedValue != null)
            int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maChiNhanh);

            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    // Câu lệnh SQL: Lấy Top 5 món có tổng số lượng bán cao nhất
                    // Kết nối bảng ChiTietHoaDonDoAn và DanhMucHang
                    string sql = @"SELECT TOP 5 d.TenHang, d.HinhAnh, SUM(c.SoLuong) as TongBan
                           FROM ChiTietHoaDonDoAn c
                           JOIN HoaDonDoAn h ON c.MaHoaDonDoAn = h.MaHoaDonDoAn -- Liên kết hóa đơn để lấy chi nhánh
                           JOIN DanhMucHang d ON c.MaHang = d.MaHang
                           WHERE h.MaChiNhanh = @MaCN -- Chỉ lấy hóa đơn của chi nhánh này
                           GROUP BY d.TenHang, d.HinhAnh
                           ORDER BY TongBan DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaCN", maChiNhanh);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        // 1. Tạo một cái khung (Panel) cho mỗi món
                        Panel pnlItem = new Panel();
                        pnlItem.Width = flowBanChay.Width - 25; // Trừ hao thanh cuộn
                        pnlItem.Height = 60;
                        pnlItem.Margin = new Padding(3);
                        pnlItem.BackColor = Color.WhiteSmoke; // Màu nền nhẹ

                        // 2. Tạo hình ảnh nhỏ
                        PictureBox pic = new PictureBox();
                        pic.Size = new Size(50, 50);
                        pic.Location = new Point(5, 5);
                        pic.SizeMode = PictureBoxSizeMode.Zoom;

                        // Load ảnh
                        string tenAnh = dr["HinhAnh"].ToString();
                        string path = Path.Combine(Application.StartupPath, "Images", tenAnh);
                        if (!string.IsNullOrEmpty(tenAnh) && File.Exists(path))
                            pic.Image = Image.FromFile(path);
                        else
                            pic.Image = null; // Hoặc ảnh mặc định

                        // 3. Tạo tên món
                        Label lblTen = new Label();
                        lblTen.Text = dr["TenHang"].ToString();
                        lblTen.Location = new Point(60, 8);
                        lblTen.AutoSize = true;
                        lblTen.Font = new Font("Segoe UI", 9, FontStyle.Bold);

                        // 4. Tạo số lượng bán
                        Label lblSoLuong = new Label();
                        lblSoLuong.Text = "Đã bán: " + dr["TongBan"].ToString();
                        lblSoLuong.Location = new Point(60, 30);
                        lblSoLuong.AutoSize = true;
                        lblSoLuong.ForeColor = Color.Red;
                        lblSoLuong.Font = new Font("Segoe UI", 8, FontStyle.Regular);

                        // 5. Gắn các thành phần vào Panel, rồi gắn Panel vào FlowLayout
                        pnlItem.Controls.Add(pic);
                        pnlItem.Controls.Add(lblTen);
                        pnlItem.Controls.Add(lblSoLuong);

                        flowBanChay.Controls.Add(pnlItem);
                    }
                    dr.Close();
                }
                catch (Exception)
                {
                    // Nếu chưa có dữ liệu bán hàng thì thôi, không báo lỗi làm phiền
                    // MessageBox.Show("Lỗi load bán chạy: " + ex.Message); 
                }
            }
        }
        private void CapNhatThongKe()
        {
            int maChiNhanh = 1;
            if (cboChiNhanh.SelectedValue != null)
                int.TryParse(cboChiNhanh.SelectedValue.ToString(), out maChiNhanh);

            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();
                    // SQL Thống kê có tính đến SoLuongToiThieu riêng của từng món
                    string sql = @"
                SELECT 
                    COUNT(d.MaHang) AS TongSP,
                    
                    -- Còn hàng: Tồn > Tối thiểu
                    SUM(CASE WHEN ISNULL(k.SoLuongTon, 0) > ISNULL(k.SoLuongToiThieu, 10) THEN 1 ELSE 0 END) AS ConHang,
                    
                    -- Sắp hết: 0 < Tồn <= Tối thiểu
                    SUM(CASE WHEN ISNULL(k.SoLuongTon, 0) > 0 AND ISNULL(k.SoLuongTon, 0) <= ISNULL(k.SoLuongToiThieu, 10) THEN 1 ELSE 0 END) AS SapHet,
                    
                    -- Hết hàng: Tồn <= 0
                    SUM(CASE WHEN ISNULL(k.SoLuongTon, 0) <= 0 THEN 1 ELSE 0 END) AS HetHang

                FROM DanhMucHang d
                LEFT JOIN KhoHang k ON d.MaHang = k.MaHang AND k.MaChiNhanh = @MaCN
                WHERE d.MaNhomHang IN (1, 2)
                AND (d.MaChiNhanh = @MaCN OR (d.MaChiNhanh IS NULL AND k.MaChiNhanh = @MaCN))";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaCN", maChiNhanh);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        // Gán dữ liệu vào các Label
                        // Bạn nhớ thay tên lbl... cho đúng với Design của bạn nha
                        lblTongSanPham.Text = dr["TongSP"].ToString();
                        lblConHang.Text = dr["ConHang"].ToString();

                        lblSapHet.Text = dr["SapHet"].ToString();
                        lblSapHet.ForeColor = Color.Orange; // Tô màu cam cho cảnh báo

                        lblHetHang.Text = dr["HetHang"].ToString();
                        lblHetHang.ForeColor = Color.Red; // Tô màu đỏ cho nguy hiểm
                    }
                    dr.Close();
                }
                catch (Exception)
                {
                    // Bỏ qua lỗi hiển thị
                }
            }
        }

        private void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gọi một loạt hàm để làm mới TẤT CẢ mọi thứ
            LoadDuLieuLenLuoi(0); // Làm mới bảng chính
            LoadTopBanChay();     // Làm mới khung Bán chạy
            CapNhatThongKe();     // Làm mới 4 ô thống kê
        }
    }
}
