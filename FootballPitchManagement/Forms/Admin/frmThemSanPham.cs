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
    public partial class frmThemSanPham : Form
    {
        // 1. CẤU HÌNH CHUỖI KẾT NỐI (Sửa lại Server name của bạn cho đúng)
        string strKetNoi = @"Data Source=DESKTOP-DHPAOGN;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // Biến lưu đường dẫn ảnh gốc khi người dùng chọn
        int maHangHienTai = -1;
        string duongDanAnhGoc = "";
        string tenFileAnh = ""; // Chỉ lưu tên file (ví dụ: nuocngot.jpg)
        public int maChiNhanhLamViec = 1;

        public frmThemSanPham()
        {
            InitializeComponent();
        }

        private void frmThemSanPham_Load(object sender, EventArgs e)
        {
            LoadComboBoxDanhMuc();
            LoadComboBoxTrangThai(); // Tách hàm này ra cho gọn
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nhập liệu
            if (string.IsNullOrEmpty(txtTenSanPham.Text)) { MessageBox.Show("Nhập tên món!"); return; }

            // 2. Lấy dữ liệu số
            int slTon = 0; int.TryParse(txtSoLuong.Text, out slTon);
            int slToiThieu = 10;
            if (!string.IsNullOrEmpty(txtSoLuongToiThieu.Text)) int.TryParse(txtSoLuongToiThieu.Text, out slToiThieu);

            // 3. Copy ảnh (Giữ nguyên)
            if (!string.IsNullOrEmpty(duongDanAnhGoc))
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                string destPath = Path.Combine(folderPath, tenFileAnh);
                try { File.Copy(duongDanAnhGoc, destPath, true); } catch { }
            }

            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = transaction;

                    int idSanPham = maHangHienTai;

                    // --- A. LƯU THÔNG TIN SẢN PHẨM VÀO DANH MỤC ---
                    if (maHangHienTai == -1) // THÊM MỚI
                    {
                        // SỬA Ở ĐÂY: Thêm cột MaChiNhanh vào câu lệnh INSERT
                        cmd.CommandText = @"INSERT INTO DanhMucHang (MaNhomHang, TenHang, DonViTinh, GiaNhap, GiaBan, HinhAnh, MoTa, TrangThai, MaChiNhanh) 
                                    VALUES (@MaNhom, @Ten, @DonVi, @GiaNhap, @GiaBan, @Anh, @MoTa, @TrangThai, @MaCN);
                                    SELECT SCOPE_IDENTITY();";
                    }
                    else // CẬP NHẬT
                    {
                        // SỬA Ở ĐÂY: Cập nhật luôn MaChiNhanh (đề phòng chuyển chi nhánh)
                        cmd.CommandText = @"UPDATE DanhMucHang 
                                    SET TenHang = @Ten, MaNhomHang = @MaNhom, DonViTinh = @DonVi, 
                                        GiaNhap = @GiaNhap, GiaBan = @GiaBan, HinhAnh = @Anh, 
                                        MoTa = @MoTa, TrangThai = @TrangThai, MaChiNhanh = @MaCN
                                    WHERE MaHang = @MaHang";
                        cmd.Parameters.AddWithValue("@MaHang", maHangHienTai);
                    }

                    // Truyền tham số
                    cmd.Parameters.AddWithValue("@Ten", txtTenSanPham.Text);
                    cmd.Parameters.AddWithValue("@MaNhom", cboDanhMuc.SelectedValue);
                    cmd.Parameters.AddWithValue("@DonVi", txtDonViTinh.Text);

                    decimal gban = 0; decimal.TryParse(txtGiaBan.Text, out gban);
                    cmd.Parameters.AddWithValue("@GiaBan", gban);

                    decimal gnhap = 0; decimal.TryParse(txtGiaNhap.Text, out gnhap);
                    cmd.Parameters.AddWithValue("@GiaNhap", gnhap);

                    cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                    cmd.Parameters.AddWithValue("@Anh", tenFileAnh);
                    cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedIndex == 0 ? 1 : 0);

                    // QUAN TRỌNG: Truyền Mã Chi Nhánh đang làm việc vào đây
                    cmd.Parameters.AddWithValue("@MaCN", maChiNhanhLamViec);

                    if (maHangHienTai == -1) idSanPham = Convert.ToInt32(cmd.ExecuteScalar());
                    else cmd.ExecuteNonQuery();

                    // --- B. LƯU KHO HÀNG (Giữ nguyên logic cũ nhưng vẫn cần thiết) ---
                    cmd.Parameters.Clear();

                    // Kiểm tra xem đã có trong bảng KhoHang chưa
                    cmd.CommandText = "SELECT COUNT(*) FROM KhoHang WHERE MaHang = @MaHang AND MaChiNhanh = @MaCN";
                    cmd.Parameters.AddWithValue("@MaHang", idSanPham);
                    cmd.Parameters.AddWithValue("@MaCN", maChiNhanhLamViec);

                    int tonTai = (int)cmd.ExecuteScalar();

                    if (tonTai > 0)
                    {
                        cmd.CommandText = @"UPDATE KhoHang 
                                    SET SoLuongTon = @SL, SoLuongToiThieu = @Min 
                                    WHERE MaHang = @MaHang AND MaChiNhanh = @MaCN";
                    }
                    else
                    {
                        cmd.CommandText = @"INSERT INTO KhoHang (MaHang, MaChiNhanh, SoLuongTon, SoLuongToiThieu) 
                                    VALUES (@MaHang, @MaCN, @SL, @Min)";
                    }

                    cmd.Parameters.AddWithValue("@SL", slTon);
                    cmd.Parameters.AddWithValue("@Min", slToiThieu);

                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Lưu thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void LoadComboBoxTrangThai()
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Đang bán"); // Index = 0
            cboTrangThai.Items.Add("Ngừng bán"); // Index = 1
            cboTrangThai.SelectedIndex = 0; 
        }
        private void LoadComboBoxDanhMuc()
        {
            using (SqlConnection conn = new SqlConnection(strKetNoi))
            {
                try
                {
                    conn.Open();

                    // CÂU LỆNH SQL CŨ: "SELECT MaNhom, TenNhom FROM NhomHang"
                    // -> Nó lấy hết tất cả.

                    // CÂU LỆNH SQL MỚI (SỬA Ở ĐÂY):
                    // Thêm: WHERE MaNhom IN (1, 2)
                    // Nghĩa là: Chỉ lấy nhóm có ID là 1 (Nước) và 2 (Ăn), bỏ qua ID 3 (Phụ kiện)
                    string sql = "SELECT MaNhom, TenNhom FROM NhomHang WHERE MaNhom IN (1, 2)";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboDanhMuc.DataSource = dt;
                    cboDanhMuc.DisplayMember = "TenNhom";
                    cboDanhMuc.ValueMember = "MaNhom";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load danh mục: " + ex.Message);
                }
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                picAnhSanPham.Image = Image.FromFile(open.FileName);
                picAnhSanPham.SizeMode = PictureBoxSizeMode.Zoom;

                duongDanAnhGoc = open.FileName;
                tenFileAnh = Path.GetFileName(duongDanAnhGoc); // Lấy tên file
            }
        }
        // Hàm này dùng để nạp dữ liệu từ dòng được chọn vào Form
        public void NapDuLieuSua(int maHang, string ten, int maNhom, decimal giaBan, decimal giaNhap, int soLuong, int soLuongToiThieu, string trangThai, string tenAnh, string donVi, string moTa)
        {
            maHangHienTai = maHang;

            // 1. QUAN TRỌNG: Load danh sách danh mục và trạng thái NGAY LẬP TỨC
            LoadComboBoxDanhMuc();
            LoadComboBoxTrangThai();

            // 2. Điền dữ liệu
            txtTenSanPham.Text = ten;

            // Bây giờ ComboBox đã có dữ liệu, lệnh này sẽ hoạt động chính xác
            cboDanhMuc.SelectedValue = maNhom;

            txtGiaBan.Text = giaBan.ToString("0.##");
            txtGiaNhap.Text = giaNhap.ToString("0.##");
            txtDonViTinh.Text = donVi;
            txtSoLuong.Text = soLuong.ToString();
            txtSoLuongToiThieu.Text = soLuongToiThieu.ToString();
            txtMoTa.Text = moTa; // Điền mô tả

            // 3. Xử lý trạng thái chính xác
            // So sánh chuỗi chính xác để chọn đúng Index
            if (trangThai == "Đang bán")
            {
                cboTrangThai.SelectedIndex = 0; // Đang bán
            }
            else
            {
                cboTrangThai.SelectedIndex = 1; // Ngừng bán
            }

            // 4. Xử lý ảnh
            tenFileAnh = tenAnh;
            string path = Path.Combine(Application.StartupPath, "Images", tenAnh);
            if (!string.IsNullOrEmpty(tenAnh) && File.Exists(path))
            {
                picAnhSanPham.Image = Image.FromFile(path);
                picAnhSanPham.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                picAnhSanPham.Image = null;
            }

            btnLuu.Text = "Cập nhật";
        }
        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
