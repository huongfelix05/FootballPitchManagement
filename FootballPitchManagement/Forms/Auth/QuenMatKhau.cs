using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballPitchManagement
{
    public partial class QuenMatKhau : Form
    {
        string strCon = @"Data Source=LAPTOP-BV9HL7MV;Initial Catalog=QuanLychuoiDatSan;Integrated Security=True";
        SqlConnection sqlCon = null;

        private string serverOTP = "";
        private string userDaLayMa = "";
        public QuenMatKhau()
        {
            InitializeComponent();
            panelDoiMK.Enabled = false;
        }

        

        private void btnLuuMatKhau_Click(object sender, EventArgs e)
        {
            string mkMoi = txtMatKhauMoi.Text;
            string nhapLai = txtNhapLaiMK.Text;

            if (string.IsNullOrEmpty(mkMoi) || mkMoi.Length < 3)
            {
                MessageBox.Show("Mật khẩu mới phải từ 3 ký tự trở lên!");
                return;
            }

            if (mkMoi != nhapLai)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }

            try
            {
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                // Cập nhật mật khẩu mới vào cột MatKhau
                string updateQuery = "UPDATE TaiKhoan SET MatKhau = @mk WHERE TenDangNhap = @ten";
                SqlCommand cmd = new SqlCommand(updateQuery, sqlCon);
                cmd.Parameters.AddWithValue("@mk", mkMoi);
                cmd.Parameters.AddWithValue("@ten", txtEmailTenDN.Text.Trim());

                int kq = cmd.ExecuteNonQuery();

                if (kq > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.");
                    //this.Close(); 
                    // Đóng form quay lại màn hình đăng nhập
                    this.Hide();
                    frmLogin fm = new frmLogin();
                    fm.ShowDialog();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
            
        }

        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNhapLaiMK_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị thông báo xác nhận nếu muốn (tùy chọn)
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát và quay lại màn hình đăng nhập không?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 1. Tìm Form đăng nhập đã ẩn trước đó
                // Giả sử Form đăng nhập của bạn tên là frmLogin
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "frmLogin") // Kiểm tra đúng tên Class của form đăng nhập
                    {
                        f.Show(); // Hiển thị lại form đăng nhập
                        this.Close(); // Đóng form Quên mật khẩu hiện tại
                        return;
                    }
                }

                // 2. Nếu không tìm thấy form cũ đang chạy ẩn, hãy khởi tạo mới
                // (Trường hợp này hiếm xảy ra nếu bạn mở QuenMatKhau từ Login)
                // frmLogin login = new frmLogin();
                // login.Show();
                // this.Close();
            }
        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLayMa_Click(object sender, EventArgs e)
        {
            string tenDN = txtEmailTenDN.Text.Trim();

            if (string.IsNullOrEmpty(tenDN))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập!");
                return;
            }

            try
            {
                // Kết nối Database
                if (sqlCon == null) sqlCon = new SqlConnection(strCon);
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                // Tìm Email dựa trên Tên đăng nhập
                string query = @"SELECT K.Email 
                         FROM TaiKhoan T
                         JOIN KhachHang K ON T.MaKH = K.MaKH
                         WHERE T.TenDangNhap = @ten";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@ten", tenDN);

                object result = cmd.ExecuteScalar();

                // Nếu tìm thấy Email
                if (result != null && result != DBNull.Value)
                {
                    string emailNguoiDung = result.ToString();

                    // 1. Tạo mã OTP mới
                    Random rd = new Random();
                    serverOTP = rd.Next(100000, 999999).ToString();

                    // 2. QUAN TRỌNG: Lưu lại tên người vừa lấy mã để lát nữa kiểm tra
                    userDaLayMa = tenDN;

                    // 3. Gửi Email (hoặc hiện thông báo Demo)
                    try
                    {
                        // Gọi hàm gửi mail thật (nếu bạn đã có)
                        // GuiEmailThat(emailNguoiDung, serverOTP); 

                        // Demo cho nhanh:
                        MessageBox.Show($"Mã OTP đã gửi đến: {emailNguoiDung}", "Đã gửi");
                    }
                    catch
                    {
                        // Nếu lỗi mạng/mail thì hiện luôn mã để test
                    }

                    // Hiện mã OTP lên màn hình (giả lập gửi mail) để bạn nhập cho dễ
                    MessageBox.Show($"[DEMO] Mã OTP của bạn là: {serverOTP}", "Tin nhắn từ hệ thống");

                    // Cập nhật giao diện
                    lblKetQua.Text = "Đã gửi mã. Vui lòng kiểm tra Email!";
                    lblKetQua.ForeColor = Color.Blue;
                    txtOTP.Enabled = true; // Mở khóa ô nhập OTP
                    txtOTP.Focus();
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại hoặc chưa có Email!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void GuiEmailThat(string toEmail, string otpCode)
        {
            var fromAddress = new MailAddress("email_demo_cua_ban@gmail.com", "Hệ thống quản lý sân bóng");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "mat_khau_ung_dung"; // Lưu ý: Dùng App Password

            string subject = "Mã xác thực OTP";
            string body = $"Mã OTP của bạn là: {otpCode}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
            {
                smtp.Send(message);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu người dùng đang nhậ
            string tenDN_HienTai = txtEmailTenDN.Text.Trim();
            string otp_NhapVao = txtOTP.Text.Trim(); // Đảm bảo ô nhập OTP tên là txtOTP

            // 1. Kiểm tra xem đã bấm nút Lấy mã chưa
            if (string.IsNullOrEmpty(serverOTP))
            {
                MessageBox.Show("Bạn chưa lấy mã OTP! Vui lòng bấm nút 'Lấy mã' trước.");
                return;
            }

            // 2. KIỂM TRA QUAN TRỌNG: 
            // Tên đang nhập bây giờ (tenDN_HienTai) CÓ PHẢI là tên lúc nãy lấy mã (userDaLayMa) không?
            if (tenDN_HienTai != userDaLayMa)
            {
                MessageBox.Show($"Bạn đã lấy mã cho tài khoản '{userDaLayMa}' nhưng lại đang nhập cho '{tenDN_HienTai}'!\nVui lòng nhập đúng tên tài khoản cũ.", "Sai thông tin");
                txtEmailTenDN.Text = userDaLayMa; // Tự sửa lại cho đúng
                return;
            }

            // 3. Kiểm tra mã OTP
            if (otp_NhapVao == serverOTP)
            {
                // === ĐÚNG HẾT ===
                lblKetQua.Text = "Xác minh thành công!";
                lblKetQua.ForeColor = Color.Green;

                // Hiện panel đổi mật khẩu lên
                panelDoiMK.Visible = true;
                panelDoiMK.Enabled = true;
                panelDoiMK.BringToFront();

                // Khóa tất cả lại không cho sửa nữa
                txtEmailTenDN.ReadOnly = true;
                txtOTP.ReadOnly = true;
                btnLayMa.Enabled = false;
                btnXacNhan.Enabled = false;
            }
            else
            {
                // === SAI MÃ OTP ===
                MessageBox.Show("Mã OTP không đúng! Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOTP.Focus();
            }
        }
    }
}
