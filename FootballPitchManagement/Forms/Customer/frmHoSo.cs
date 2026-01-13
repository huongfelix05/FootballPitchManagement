using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FootballPitchManagement.Forms.Customer
{
    public partial class frmHoSo : Form
    {
        // ==================================================================================
        // 1. KHAI BÁO BIẾN & KẾT NỐI
        // ==================================================================================

        // LƯU Ý: Sửa lại 'Server Name' (chỗ DESKTOP-...) cho đúng máy bạn
        private string strKetNoi = @"Data Source=DANGKHOA;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";

        // Biến lưu mã khách hàng (ID). Nếu = 0 là thêm mới, > 0 là sửa.
        private int _maKH;

        // Biến kiểm tra thay đổi dữ liệu & báo lỗi
        private bool _isDirty;
        private ErrorProvider _errorProvider;

        // ==================================================================================
        // 2. CONSTRUCTORS (HÀM KHỞI TẠO)
        // ==================================================================================

        // Constructor mặc định (Bắt buộc phải có để Designer hoạt động)
        public frmHoSo()
        {
            InitializeComponent();
            _maKH = 0;
            InitializeCommon();
        }

        // Constructor nhận ID (Dùng khi mở form sửa user)
        public frmHoSo(int maKH)
        {
            InitializeComponent();
            _maKH = maKH;
            InitializeCommon();
        }

        // Hàm khởi tạo chung cho cả 2 trường hợp
        private void InitializeCommon()
        {
            _errorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

            // Đăng ký sự kiện Load an toàn
            this.Load -= frmHoSo_Load;
            this.Load += frmHoSo_Load;

            // Đăng ký sự kiện Click (Kiểm tra null để tránh lỗi Designer)
            if (btnLuu != null) btnLuu.Click += btnLuu_Click;
            if (btnThoat != null) btnThoat.Click += btnThoat_Click;

            // Đăng ký sự kiện thay đổi dữ liệu (để bật nút Lưu)
            if (txtHoTen != null) txtHoTen.TextChanged += AnyControlChanged;
            if (txtSDT != null) txtSDT.TextChanged += AnyControlChanged;
            if (txtEmail != null) txtEmail.TextChanged += AnyControlChanged;
          //  if (txtCCCD != null) txtCCCD.TextChanged += AnyControlChanged;
            if (txtDiaChi != null) txtDiaChi.TextChanged += AnyControlChanged;
            if (dtpNgaySinh != null) dtpNgaySinh.ValueChanged += AnyControlChanged;
          //  if (rdoNam != null) rdoNam.CheckedChanged += AnyControlChanged;
         //   if (rdoNu != null) rdoNu.CheckedChanged += AnyControlChanged;

            if (btnLuu != null) btnLuu.Enabled = false;
            if (dtpNgaySinh != null) dtpNgaySinh.MaxDate = DateTime.Today;

            _isDirty = false;
        }

        // ==================================================================================
        // 3. CÁC SỰ KIỆN (EVENTS)
        // ==================================================================================

        private void frmHoSo_Load(object sender, EventArgs e)
        {
            // Nếu có mã KH > 0 thì load dữ liệu cũ lên
            if (_maKH > 0)
            {
                LoadDataTuSQL();
                _isDirty = false;
                if (btnLuu != null) btnLuu.Enabled = false;
            }
            else
            {
                // Nếu là thêm mới thì xóa trắng
                if (txtMaKH != null) txtMaKH.Text = string.Empty;
                if (txtHoTen != null) txtHoTen.Text = string.Empty;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_errorProvider != null) _errorProvider.Clear();

            // Kiểm tra dữ liệu đầu vào
            if (!KiemTraDuLieu()) return;

            try
            {
                if (_maKH > 0)
                    UpdateCustomer(); // Cập nhật
                else
                    InsertCustomer(); // Thêm mới
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (_isDirty)
            {
                var dr = MessageBox.Show("Dữ liệu chưa lưu. Bạn có muốn thoát mà không lưu?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No) return;
            }
            this.Close();
        }

        // Hàm này chạy khi người dùng gõ phím hoặc thay đổi bất cứ gì
        private void AnyControlChanged(object sender, EventArgs e)
        {
            _isDirty = true;
            if (btnLuu != null) btnLuu.Enabled = true;
        }

        // ==================================================================================
        // 4. XỬ LÝ DATABASE (LOAD / INSERT / UPDATE)
        // ==================================================================================

        private void LoadDataTuSQL()
        {
            using (var conn = new SqlConnection(strKetNoi))
            using (var cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "SELECT MaKH, HoTen, DienThoai, Email, GioiTinh, NgaySinh, DiaChi, CMND_CCCD FROM KhachHang WHERE MaKH = @MaKH";
                    cmd.Parameters.AddWithValue("@MaKH", _maKH);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (txtMaKH != null) txtMaKH.Text = reader["MaKH"].ToString();
                            if (txtHoTen != null) txtHoTen.Text = reader["HoTen"].ToString();
                            if (txtSDT != null) txtSDT.Text = reader["DienThoai"].ToString();
                            if (txtEmail != null) txtEmail.Text = reader["Email"].ToString();
                       //     if (txtCCCD != null) txtCCCD.Text = reader["CMND_CCCD"].ToString();
                            if (txtDiaChi != null) txtDiaChi.Text = reader["DiaChi"].ToString();

                            if (dtpNgaySinh != null)
                            {
                                if (reader["NgaySinh"] != DBNull.Value)
                                    dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]);
                                else
                                    dtpNgaySinh.Value = DateTime.Today;
                            }

                            var gioiTinh = (reader["GioiTinh"] ?? string.Empty).ToString();
                        //    if (rdoNam != null) rdoNam.Checked = (gioiTinh == "Nam");
                     //       if (rdoNu != null) rdoNu.Checked = (gioiTinh == "Nữ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertCustomer()
        {
            using (var conn = new SqlConnection(strKetNoi))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = @"
                    INSERT INTO KhachHang (HoTen, DienThoai, Email, GioiTinh, NgaySinh, DiaChi, CMND_CCCD)
                    VALUES (@HoTen, @DienThoai, @Email, @GioiTinh, @NgaySinh, @DiaChi, @CCCD);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                AddCommonParameters(cmd);

                var scalar = cmd.ExecuteScalar();
                if (scalar != null && int.TryParse(scalar.ToString(), out int newId))
                {
                    _maKH = newId;
                    if (txtMaKH != null) txtMaKH.Text = newId.ToString();
                    MessageBox.Show("Thêm khách hàng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isDirty = false;
                    if (btnLuu != null) btnLuu.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không thể lấy ID vừa tạo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void UpdateCustomer()
        {
            using (var conn = new SqlConnection(strKetNoi))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = @"
                    UPDATE KhachHang
                    SET HoTen = @HoTen,
                        DienThoai = @DienThoai,
                        Email = @Email,
                        GioiTinh = @GioiTinh,
                        NgaySinh = @NgaySinh,
                        DiaChi = @DiaChi,
                        CMND_CCCD = @CCCD
                    WHERE MaKH = @MaKH";

                cmd.Parameters.AddWithValue("@MaKH", _maKH);
                AddCommonParameters(cmd);

                int affected = cmd.ExecuteNonQuery();
                if (affected > 0)
                {
                    MessageBox.Show("Cập nhật thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isDirty = false;
                    if (btnLuu != null) btnLuu.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không có thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void AddCommonParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@HoTen", (txtHoTen?.Text ?? string.Empty).Trim());
            cmd.Parameters.AddWithValue("@DienThoai", (txtSDT?.Text ?? string.Empty).Trim());
            cmd.Parameters.AddWithValue("@Email", (txtEmail?.Text ?? string.Empty).Trim());

          //  var gioiTinh = (rdoNam != null && rdoNam.Checked) ? "Nam" : ((rdoNu != null && rdoNu.Checked) ? "Nữ" : string.Empty);
          //  cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);

            if (dtpNgaySinh != null)
                cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
            else
                cmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);

            cmd.Parameters.AddWithValue("@DiaChi", (txtDiaChi?.Text ?? string.Empty).Trim());
        //    cmd.Parameters.AddWithValue("@CCCD", (txtCCCD?.Text ?? string.Empty).Trim());
        }

        // ==================================================================================
        // 5. KIỂM TRA DỮ LIỆU (VALIDATION)
        // ==================================================================================

        private bool KiemTraDuLieu()
        {
            bool ok = true;
            if (_errorProvider != null) _errorProvider.Clear();

            // Kiểm tra Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen?.Text))
            {
                if (_errorProvider != null) _errorProvider.SetError(txtHoTen, "Chưa nhập họ tên");
                ok = false;
            }

            // Kiểm tra SĐT
            if (string.IsNullOrWhiteSpace(txtSDT?.Text))
            {
                if (_errorProvider != null) _errorProvider.SetError(txtSDT, "Chưa nhập SĐT");
                ok = false;
            }
            else if (!IsValidPhone(txtSDT.Text))
            {
                if (_errorProvider != null) _errorProvider.SetError(txtSDT, "SĐT không hợp lệ (7-15 số).");
                ok = false;
            }

            // Kiểm tra Email (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtEmail?.Text) && !IsValidEmail(txtEmail.Text))
            {
                if (_errorProvider != null) _errorProvider.SetError(txtEmail, "Email không hợp lệ.");
                ok = false;
            }

            // Kiểm tra CCCD (cơ bản)
       //     if (!string.IsNullOrWhiteSpace(txtCCCD?.Text) && txtCCCD.Text.Length < 6)
            {
              //  if (_errorProvider != null) _errorProvider.SetError(txtCCCD, "Số CMND/CCCD quá ngắn.");
                ok = false;
            }

            // Kiểm tra Giới tính (Nếu chưa chọn cái nào)
         //   if ((rdoNam == null || !rdoNam.Checked) && (rdoNu == null || !rdoNu.Checked))
            {
                // Nếu bạn có flowLayoutPanel1 thì báo lỗi vào đó, nếu không thì thôi
                // if (flowLayoutPanel1 != null) _errorProvider.SetError(flowLayoutPanel1, "Chưa chọn giới tính.");
                // ok = false; 
            }

            return ok;
        }

        // Hàm kiểm tra định dạng Email
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch { return false; }
        }

        // Hàm kiểm tra SĐT
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            try
            {
                return Regex.IsMatch(phone, @"^\+?\d{7,15}$");
            }
            catch { return false; }
        }

        private void frmHoSo_Load_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}