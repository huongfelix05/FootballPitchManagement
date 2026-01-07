using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace FootballPitchManagement
{
    public partial class frmRegister : Form
    {
        private string conn = @"Data Source=LAPTOP-BV9HL7MV;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True;TrustServerCertificate=True;";
        private enum ErrorDisplayStyle { Tooltip, MessageBox, InlineLabel, RedBackground }
        private ErrorDisplayStyle pwdErrorStyle = ErrorDisplayStyle.InlineLabel;
        private Label lblPasswordError;
        private Label lblEmailError; // inline label for email errors
        private Label lblPhoneError; // inline label for phone errors
        private Label lblUsernameError; // inline label for username errors

        public frmRegister()
        {
            InitializeComponent();
           // button1.Enabled = false; // KHÓA nút Đăng ký ban đầu không cho đăng kí khi chưa nhập thông tin

            if (this.errorProvider1 == null)
            {
                this.errorProvider1 = new ErrorProvider();
                this.errorProvider1.ContainerControl = this;
            }

            // Ensure icon alignment/padding for relevant controls (username, phone, email, password)
            if (this.txtPassword1 != null)
            {
                this.errorProvider1.SetIconAlignment(this.txtPassword1, ErrorIconAlignment.BottomRight);
                this.errorProvider1.SetIconPadding(this.txtPassword1, 2);
            }
            if (this.txtPassword != null)
            {
                this.errorProvider1.SetIconAlignment(this.txtPassword, ErrorIconAlignment.BottomRight);
                this.errorProvider1.SetIconPadding(this.txtPassword, 2);
            }
            if (this.txtTenDangNhap != null)
            {
                this.errorProvider1.SetIconAlignment(this.txtTenDangNhap, ErrorIconAlignment.BottomRight);
                this.errorProvider1.SetIconPadding(this.txtTenDangNhap, 2);
            }
            if (this.textBox4 != null)
            {
                this.errorProvider1.SetIconAlignment(this.textBox4, ErrorIconAlignment.BottomRight);
                this.errorProvider1.SetIconPadding(this.textBox4, 2);
            }
            if (this.textBox5 != null)
            {
                this.errorProvider1.SetIconAlignment(this.textBox5, ErrorIconAlignment.BottomRight);
                this.errorProvider1.SetIconPadding(this.textBox5, 2);
            }

            lblPasswordError = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Visible = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular)
            };
            PositionInlineLabel();
            if (this.panel1 != null)
                this.panel1.Controls.Add(lblPasswordError);
            else
                this.Controls.Add(lblPasswordError);

            // tạo label lỗi cho username (hiển thị phía dưới ô tên đăng nhập)
            lblUsernameError = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Visible = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular)
            };
            PositionUsernameInlineLabel();
            if (this.panel1 != null)
                this.panel1.Controls.Add(lblUsernameError);
            else
                this.Controls.Add(lblUsernameError);

            // tạo label lỗi cho email (hiển thị phía dưới ô email)
            lblEmailError = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Visible = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular)
            };
            PositionEmailInlineLabel();
            if (this.panel1 != null)
                this.panel1.Controls.Add(lblEmailError);
            else
                this.Controls.Add(lblEmailError);

            // tạo label lỗi cho số điện thoại (hiển thị phía dưới ô SĐT)
            lblPhoneError = new Label
            {
                ForeColor = Color.Red,
                AutoSize = true,
                Visible = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular)
            };
            PositionPhoneInlineLabel();
            if (this.panel1 != null)
                this.panel1.Controls.Add(lblPhoneError);
            else
                this.Controls.Add(lblPhoneError);

            // đảm bảo Leave đã đăng ký (Designer đã thêm Leave)
            // Không đăng ký TextChanged để bỏ kiểm tra realtime
        }

        private void PositionInlineLabel()
        {
            if (txtPassword1 != null && lblPasswordError != null)
            {
                lblPasswordError.Location = new Point(txtPassword1.Left, txtPassword1.Bottom + 4);
            }
        }

        private void PositionUsernameInlineLabel()
        {
            if (txtTenDangNhap != null && lblUsernameError != null)
            {
                lblUsernameError.Location = new Point(txtTenDangNhap.Left, txtTenDangNhap.Bottom + 4);
            }
        }

        private void PositionEmailInlineLabel()
        {
            if (textBox5 != null && lblEmailError != null)
            {
                lblEmailError.Location = new Point(textBox5.Left, textBox5.Bottom + 4);
            }
        }

        private void PositionPhoneInlineLabel()
        {
            if (textBox4 != null && lblPhoneError != null)
            {
                lblPhoneError.Location = new Point(textBox4.Left, textBox4.Bottom + 4);
            }
        }

        private bool IsUsernameValid(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            // allow only ASCII letters, digits and ._- characters (no Vietnamese diacritics)
            return Regex.IsMatch(username, "^[A-Za-z0-9_.-]+$");
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string username = txtTenDangNhap?.Text?.Trim() ?? string.Empty;
            PositionUsernameInlineLabel();

            if (string.IsNullOrEmpty(username))
            {
                if (lblUsernameError != null)
                {
                    lblUsernameError.Text = "Vui lòng nhập tên đăng nhập.";
                    lblUsernameError.Visible = true;
                }
                if (txtTenDangNhap != null) txtTenDangNhap.BackColor = Color.MistyRose;

                if (errorProvider1 != null) errorProvider1.SetError(txtTenDangNhap, "Vui lòng nhập tên đăng nhập.");
                return;
            }

            if (!IsUsernameValid(username))
            {
                if (lblUsernameError != null)
                {
                    lblUsernameError.Text = "không được chứa dấu tiếng Việt!.";
                    lblUsernameError.Visible = true;
                }
                if (txtTenDangNhap != null) txtTenDangNhap.BackColor = Color.MistyRose;
                if (errorProvider1 != null) errorProvider1.SetError(txtTenDangNhap, "Tên đăng nhập không hợp lệ.");
            }
            else
            {
                if (lblUsernameError != null)
                {
                    lblUsernameError.Text = string.Empty;
                    lblUsernameError.Visible = false;
                }
                if (txtTenDangNhap != null) txtTenDangNhap.BackColor = SystemColors.Window;
                if (errorProvider1 != null) errorProvider1.SetError(txtTenDangNhap, string.Empty);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtPassword.UseSystemPasswordChar = !chkShowPass.Checked;
                txtPassword1.UseSystemPasswordChar = !chkShowPass.Checked;
            }
            catch { }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email && email.Split('@').Length == 2 && email.Split('@')[1].Contains('.');
            }
            catch { return false; }
        }

        

private void button1_Click(object sender, EventArgs e)
    {
            if (!ValidateRegisterForm())
                return;


            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                textBox3.Focus();
                return;
            }

            if (IsRegisterInfoExists(
                txtTenDangNhap.Text.Trim(),
                textBox5.Text.Trim(),
                textBox4.Text.Trim()
                ))
            {
                MessageBox.Show("Tên đăng nhập, Email hoặc SĐT đã tồn tại!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword1.Text != txtPassword.Text)
                {
                 MessageBox.Show("Mật khẩu xác nhận không khớp");
                  return;
                }

             if (cmdgioitinh.SelectedIndex == -1)
                {
                  MessageBox.Show("Vui lòng chọn giới tính");
                 return;
                 }

               if (txtHoChieu.Text.Length != 12)
                 {
                MessageBox.Show("Hộ chiếu phải đúng 12 chữ số");
                return;
                 }

       


        using (SqlConnection connn = new SqlConnection(conn))
        {
            connn.Open();
            SqlTransaction tran = connn.BeginTransaction();

            try
            {
                // 1. Insert KHÁCH HÀNG
                string sqlKH = @"
            INSERT INTO KhachHang
            (HoTen, DienThoai, Email, GioiTinh, NgaySinh, DiaChi, CMND_CCCD)
            VALUES
            (@HoTen, @SDT, @Email, @GioiTinh, @NgaySinh, @DiaChi, @CMND);
            SELECT SCOPE_IDENTITY();";

                SqlCommand cmdKH = new SqlCommand(sqlKH, connn, tran);
                cmdKH.Parameters.AddWithValue("@HoTen", txtTenDangNhap.Text);
                cmdKH.Parameters.AddWithValue("@SDT", textBox4.Text);
                cmdKH.Parameters.AddWithValue("@Email", textBox5.Text);
                cmdKH.Parameters.AddWithValue("@GioiTinh", cmdgioitinh.Text);
                cmdKH.Parameters.AddWithValue("@NgaySinh", dateTimePicker1.Value.Date);
                cmdKH.Parameters.AddWithValue("@DiaChi", textBox3.Text);
                cmdKH.Parameters.AddWithValue("@CMND", txtHoChieu.Text);

                int maKH = Convert.ToInt32(cmdKH.ExecuteScalar());

                // 2. Insert TÀI KHOẢN
                string sqlTK = @"
            INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaKH, MaLoaiTK)
            VALUES (@User, @Pass, @MaKH, 4)";

                SqlCommand cmdTK = new SqlCommand(sqlTK, connn, tran);
                cmdTK.Parameters.AddWithValue("@User", txtTenDangNhap.Text);
                cmdTK.Parameters.AddWithValue("@Pass", txtPassword1.Text); // nên hash
                cmdTK.Parameters.AddWithValue("@MaKH", maKH);
                cmdTK.ExecuteNonQuery();

                // 3. Log đăng ký
                string sqlLog = @"INSERT INTO LogDangKy (MaKH, TrangThai)
                              VALUES (@MaKH, N'THANH_CONG')";

                SqlCommand cmdLog = new SqlCommand(sqlLog, connn, tran);
                cmdLog.Parameters.AddWithValue("@MaKH", maKH);
                cmdLog.ExecuteNonQuery();

                tran.Commit();

                MessageBox.Show("Đăng ký thành công!");

                // Chuyển về form Login
                frmLogin f = new frmLogin();
                    f.Show();
                    this.Hide() ;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
            }
        }
    }



    // Không gọi ValidatePasswords trên TextChanged nữa
    private void txtPassword_TextChanged(object sender, EventArgs e) { /* no-op */ }
        private void txtPassword1_TextChanged(object sender, EventArgs e) { /* no-op */ }

        // Chỉ kiểm tra khi rời ô (người dùng nhập xong) -> force = true
        private void txtPassword_Leave(object sender, EventArgs e) => ValidatePasswords(force: true);
        private void txtPassword1_Leave(object sender, EventArgs e) => ValidatePasswords(force: true);

        private void textBox5_Leave(object sender, EventArgs e)
        {
            // đảm bảo ErrorProvider tồn tại
            if (this.errorProvider1 == null)
            {
                this.errorProvider1 = new ErrorProvider();
                this.errorProvider1.ContainerControl = this;
            }

            string email = textBox5?.Text?.Trim() ?? string.Empty;

            // ensure label positioned (in case layout changed)
            PositionEmailInlineLabel();

            if (string.IsNullOrEmpty(email))
            {
                // nếu để trống thì thông báo yêu cầu nhập - hiển thị dưới ô email
                if (lblEmailError != null)
                {
                    lblEmailError.Text = "Vui lòng nhập email.";
                    lblEmailError.Visible = true;
                }
                if (textBox5 != null) textBox5.BackColor = Color.MistyRose;
                // set ErrorProvider icon
                if (errorProvider1 != null) errorProvider1.SetError(textBox5, "Vui lòng nhập email.");
                return;
            }

            if (!IsValidEmail(email))
            {
                // nếu sai định dạng thì báo lỗi ngay (inline dưới ô email)
                if (lblEmailError != null)
                {
                    lblEmailError.Text = "Email không đúng định dạng!.";
                    lblEmailError.Visible = true;
                }
                if (textBox5 != null) textBox5.BackColor = Color.MistyRose;
                if (errorProvider1 != null) errorProvider1.SetError(textBox5, "Email không đúng định dạng!.");
            }
            else
            {
                // hợp lệ -> ẩn label lỗi và xóa nền đỏ
                if (lblEmailError != null)
                {
                    lblEmailError.Text = string.Empty;
                    lblEmailError.Visible = false;
                }
                if (textBox5 != null) textBox5.BackColor = SystemColors.Window;
                if (errorProvider1 != null) errorProvider1.SetError(textBox5, string.Empty);

                
            }
      }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            // validate phone: must be 10 or 11 digits
            string phone = textBox4?.Text?.Trim() ?? string.Empty;

            // ensure label positioned
            PositionPhoneInlineLabel();

            // Count digits only
            int digitCount = phone.Count(char.IsDigit);

            if (string.IsNullOrEmpty(phone))
            {
                if (lblPhoneError != null)
                {
                    lblPhoneError.Text = "Vui lòng nhập số điện thoại!.";
                    lblPhoneError.Visible = true;
                }
                if (textBox4 != null) textBox4.BackColor = Color.MistyRose;
                if (errorProvider1 != null) errorProvider1.SetError(textBox4, "Vui lòng nhập số điện thoại!.");
                return;
            }

            if (digitCount != 10 && digitCount != 11)
            {
                if (lblPhoneError != null)
                {
                    lblPhoneError.Text = "Vui lòng kiểm tra lại sđt của bạn!.";
                    lblPhoneError.Visible = true;
                }
                if (textBox4 != null) textBox4.BackColor = Color.MistyRose;
                if (errorProvider1 != null) errorProvider1.SetError(textBox4, "Số điện thoại phải có 10 hoặc 11 chữ số.");
            }
            else
            {
                if (lblPhoneError != null)
                {
                    lblPhoneError.Text = string.Empty;
                    lblPhoneError.Visible = false;
                }
                if (textBox4 != null) textBox4.BackColor = SystemColors.Window;
                if (errorProvider1 != null) errorProvider1.SetError(textBox4, string.Empty);
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    // nút mũi tên trên cùng bên phải để thoát khỏi form đăng ký
        //    var result = MessageBox.Show("Bạn có muốn thoát mà không đăng ký?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        this.Close();
        //    }
        //}

        //private void btnnutthoat_Click(object sender, EventArgs e)
        //{

        //    // nút mũi tên trên cùng bên phải để thoát khỏi form đăng ký
        //    var result = MessageBox.Show("Bạn có muốn thoát mà không đăng ký?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        this.Close();
        //    }

        //}

        private void ValidatePasswords(bool force = false)
        {
            if (this.errorProvider1 == null)
            {
                this.errorProvider1 = new ErrorProvider();
                this.errorProvider1.ContainerControl = this;
                if (this.txtPassword1 != null)
                {
                    this.errorProvider1.SetIconAlignment(this.txtPassword1, ErrorIconAlignment.BottomRight);
                    this.errorProvider1.SetIconPadding(this.txtPassword1, 2);
                }
            }

            string p1 = txtPassword?.Text ?? string.Empty;
            string p2 = txtPassword1?.Text ?? string.Empty;
            string message = string.Empty;

            // Khi force = true (rời ô) thực hiện kiểm tra bắt buộc
            if (force)
            {
                if (string.IsNullOrEmpty(p2))
                {
                    message = "Vui lòng kiểm tra lại passwword!.";
                }
                else if (string.IsNullOrEmpty(p1))
                {
                    message = "Vui lòng nhập mật khẩu mạnh.";
                }
                else if (p1 != p2)
                {
                    message = "Mật khẩu không khớp. Vui lòng kiểm tra lại!.";
                }
                else
                {
                    message = string.Empty;
                }
            }
            else
            {
                // Không thực hiện kiểm tra realtime nữa; chỉ clear UI nếu cả hai rỗng
                if (string.IsNullOrEmpty(p1) && string.IsNullOrEmpty(p2))
                {
                    ClearAllPasswordErrorUI();
                    return;
                }
                // Không đặt lỗi trong trường hợp realtime (bỏ)
                return;
            }

            // Hiển thị lỗi chỉ trên ô xác nhận
            errorProvider1.SetError(txtPassword1, message);
            errorProvider1.SetError(txtPassword, string.Empty);

            // Cập nhật theo style
            switch (pwdErrorStyle)
            {
                case ErrorDisplayStyle.Tooltip:
                    lblPasswordError.Visible = false;
                    txtPassword1.BackColor = string.IsNullOrEmpty(message) ? SystemColors.Window : Color.MistyRose;
                    break;
                case ErrorDisplayStyle.MessageBox:
                    lblPasswordError.Visible = false;
                    txtPassword1.BackColor = SystemColors.Window;
                    if (!string.IsNullOrEmpty(message)) MessageBox.Show(message, "Lỗi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case ErrorDisplayStyle.InlineLabel:
                    if (string.IsNullOrEmpty(message))
                    {
                        lblPasswordError.Visible = false;
                        txtPassword1.BackColor = SystemColors.Window;
                    }
                    else
                    {
                        lblPasswordError.Text = message;
                        PositionInlineLabel();
                        lblPasswordError.Visible = true;
                        txtPassword1.BackColor = Color.MistyRose;
                    }
                    break;
                case ErrorDisplayStyle.RedBackground:
                    lblPasswordError.Visible = false;
                    txtPassword1.BackColor = string.IsNullOrEmpty(message) ? SystemColors.Window : Color.MistyRose;
                    break;
            }
        }

        private void ClearAllPasswordErrorUI()
        {
            if (errorProvider1 != null) errorProvider1.SetError(txtPassword1, string.Empty);
            if (lblPasswordError != null) lblPasswordError.Visible = false;
            if (txtPassword1 != null) txtPassword1.BackColor = SystemColors.Window;
            if (txtPassword != null) errorProvider1.SetError(txtPassword, string.Empty);
        }
        private bool ValidateRegisterForm()
        {
            

            // 1. Username
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (!IsUsernameValid(txtTenDangNhap.Text.Trim()))
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ (không dấu, không khoảng trắng)!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            // 2. Password
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtPassword1.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword1.Focus();
                return false;
            }

            // 3. Địa chỉ (BẮT BUỘC)
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }


            // 4 SĐT
            int digitCount = textBox4.Text.Count(char.IsDigit);
            if (digitCount != 10 && digitCount != 11)
            {
                MessageBox.Show("Số điện thoại phải có 10 hoặc 11 chữ số!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return false;
            }

            // 5. Giới tính (bắt buộc chọn)
            if (cmdgioitinh.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giới tính!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cmdgioitinh.Focus();
                return false;
            }
            
            // 7. Ngày sinh (bắt buộc & không được lớn hơn hiện tại)
            if (dateTimePicker1.Value.Date >= DateTime.Now.Date)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dateTimePicker1.Focus();
                return false;
            }


            // 8. Hộ chiếu / CCCD (bắt buộc, đúng 12 số)
            if (string.IsNullOrWhiteSpace(txtHoChieu.Text))
            {
                MessageBox.Show("Vui lòng nhập số hộ chiếu / CCCD!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtHoChieu.Focus();
                return false;
            }

            if (txtHoChieu.Text.Length != 12 || !txtHoChieu.Text.All(char.IsDigit))
            {
                MessageBox.Show("Hộ chiếu / CCCD phải gồm đúng 12 chữ số!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtHoChieu.Focus();
                return false;
            }

            // 9. Email
            if (!IsValidEmail(textBox5.Text.Trim()))
            {
                MessageBox.Show("Email không đúng định dạng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Focus();
                return false;
            }
            // 10. Điều khoản (BẮT BUỘC)
            if (!chkAgree.Checked)
            {
                MessageBox.Show("Bạn phải đồng ý với các điều khoản để đăng ký!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                chkAgree.Focus();
                return false;
            }

            return true; // TẤT CẢ HỢP LỆ
        }

        //kết nối spl
        private bool IsRegisterInfoExists(string username, string email, string phone)
        {
            using (SqlConnection connSql = new SqlConnection(conn))
            {
                string sql = @"
        SELECT COUNT(*)
        FROM TaiKhoan tk
        LEFT JOIN KhachHang kh ON tk.MaKH = kh.MaKH
        WHERE tk.TenDangNhap = @username
           OR kh.Email = @email
           OR kh.DienThoai = @phone ";

                using (SqlCommand cmd = new SqlCommand(sql, connSql))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    

                    connSql.Open();
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        //private bool RegisterCustomer()
        //{
        //    using (SqlConnection connSql = new SqlConnection(conn))
        //    {
        //        connSql.Open();
        //        SqlTransaction tran = connSql.BeginTransaction();

        //        try
        //        {
        //            // 1. INSERT KhachHang
        //            string sqlKH = @"
        //    INSERT INTO KhachHang (HoTen, DienThoai, Email, DiaChi)
        //    OUTPUT INSERTED.MaKH
        //    VALUES (@hoten, @phone, @email, @address)";

        //            SqlCommand cmdKH = new SqlCommand(sqlKH, connSql, tran);
        //            cmdKH.Parameters.AddWithValue("@hoten", textBox1.Text.Trim()); // HoTen
        //            cmdKH.Parameters.AddWithValue("@phone", textBox4.Text.Trim());
        //            cmdKH.Parameters.AddWithValue("@email", textBox5.Text.Trim());
        //            cmdKH.Parameters.AddWithValue("@address", textBox3.Text.Trim());

        //            int maKH = (int)cmdKH.ExecuteScalar(); // LẤY MaKH

        //            // 2. INSERT TaiKhoan
        //            string sqlTK = @"
        //    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, MaKH, MaLoaiTK)
        //    VALUES (@username, @password, @makh, @maloaitk)";

        //            SqlCommand cmdTK = new SqlCommand(sqlTK, connSql, tran);
        //            cmdTK.Parameters.AddWithValue("@username", textBox1.Text.Trim());
        //            cmdTK.Parameters.AddWithValue("@password", txtPassword.Text.Trim()); // sau này hash
        //            cmdTK.Parameters.AddWithValue("@makh", maKH);
        //            cmdTK.Parameters.AddWithValue("@maloaitk", 4); // KHÁCH HÀNG

        //            cmdTK.ExecuteNonQuery();

        //            // 3. COMMIT
        //            tran.Commit();
        //            return true;
        //        }
        //        catch
        //        {
        //            tran.Rollback();
        //            return false;
        //        }
        //    }
        //}


        private void txtHoChieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
            if (txtHoChieu.Text.Length != 12)
            {
                MessageBox.Show("Hộ chiếu phải gồm đúng 12 chữ số", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoChieu.Focus();
                return;
            }

        }


        // nút thoát
        private void button3_Click(object sender, EventArgs e)
        {
            // nút mũi tên trên cùng bên phải để thoát khỏi form đăng ký
            var result = MessageBox.Show("Bạn có muốn thoát mà không đăng ký?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}