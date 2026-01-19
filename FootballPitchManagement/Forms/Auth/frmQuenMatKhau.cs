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
using FootballPitchManagement.Common;

namespace FootballPitchManagement
{
    public partial class frmQuenMatKhau : Form
    {
        SqlConnection sqlCon = null;

        private string serverOTP = "";
        private string userDaLayMa = "";

        System.Windows.Forms.Timer timerDemNguoc; // Biến đồng hồ
        int thoiGianConLai = 60; // Thời gian đếm ngược (60s)
        public frmQuenMatKhau()
        {
            InitializeComponent();
            panelDoiMK.Enabled = false;

            timerDemNguoc = new System.Windows.Forms.Timer();
            timerDemNguoc.Interval = 1000; // 1000ms = 1 giây
            timerDemNguoc.Tick += TimerDemNguoc_Tick; // Gắn sự kiện nhảy giây
        }



       
        private void txtMatKhauMoi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNhapLaiMK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void GuiEmailThat(string toEmail, string otpCode)
        {
            try
            {
                // --- CẤU HÌNH GMAIL CỦA NÍ ---
                string fromEmail = "huynhphuthinh205@gmail.com"; // Email của ní

                // MẬT KHẨU ỨNG DỤNG 16 KÝ TỰ (Lấy từ Google, KHÔNG PHẢI pass đăng nhập)
                string password = "rqqa mndl cpee owlz";
                // -----------------------------------------------------------

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail, "Hệ thống Quản Lý Sân Bóng");
                message.To.Add(toEmail);
                message.Subject = "Mã xác thực OTP - Đặt lại mật khẩu";
                message.Body = $"Chào bạn,\n\nMã OTP của bạn là: {otpCode}\n\nVui lòng không chia sẻ mã này cho ai.";
                message.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromEmail, password);

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                // Ném lỗi ra để hàm gọi nó xử lý
                throw new Exception("Lỗi SMTP Gmail: " + ex.Message);
            }
        }

        private void txtOTP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmailTenDN_TextChanged(object sender, EventArgs e)
        {

        }
        private void TimerDemNguoc_Tick(object sender, EventArgs e)
        {
            thoiGianConLai--; // Giảm 1 giây
            btnLayMa.Text = $"Gửi lại ({thoiGianConLai}s)"; // Đổi chữ nút

            if (thoiGianConLai <= 0)
            {
                timerDemNguoc.Stop();    // Dừng đồng hồ
                btnLayMa.Enabled = true; // Cho bấm lại
                btnLayMa.Text = "Lấy mã"; // Trả về chữ cũ
                thoiGianConLai = 60;     // Reset về 60s
            }
        }

        private void lblKetQua_Click(object sender, EventArgs e)
        {

        }

        

        private void frmQuenMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            
        }

        private void txtEmailTenDN_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtOTP_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnLayMa_Click(object sender, EventArgs e)
        {
            string input = txtEmailTenDN.Text.Trim(); // Đặt tên biến là 'input' cho chuẩn

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập hoặc Email!");
                return;
            }

            try
            {
                if (sqlCon == null) sqlCon = DatabaseConnection.GetConnection();
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                // Câu lệnh SQL: Tìm người có Tên đăng nhập = input HOẶC Email = input
                string query = @"SELECT K.Email 
                                     FROM TaiKhoan T 
                                     JOIN KhachHang K ON T.MaKH = K.MaKH 
                                     WHERE T.TenDangNhap = @val OR K.Email = @val";

                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@val", input);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string emailNguoiDung = result.ToString();

                    // Tạo OTP ngẫu nhiên
                    Random rd = new Random();
                    serverOTP = rd.Next(100000, 999999).ToString();

                    // QUAN TRỌNG: Lưu lại cái mà người dùng đã nhập (input) để lát nữa kiểm tra
                    userDaLayMa = input;

                    // Gửi Email
                    try
                    {
                        GuiEmailThat(emailNguoiDung, serverOTP);

                        MessageBox.Show($"Mã OTP đã được gửi đến email: {emailNguoiDung}", "Thành công");
                        lblKetQua.Text = "Đã gửi mã. Hãy kiểm tra Email!";
                        lblKetQua.ForeColor = Color.Blue;
                        lblKetQua.Visible = true;
                        btnLayMa.Enabled = false; // Khóa nút không cho bấm nữa
                        thoiGianConLai = 60;      // Đặt lại 60s
                        timerDemNguoc.Start();    // Bắt đầu chạy đồng hồ
                        // Mở khóa ô nhập OTP
                        txtOTP.Enabled = true;
                        txtOTP.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không gửi được Email. Kiểm tra mạng hoặc Mật khẩu ứng dụng.\nLỗi: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Tên đăng nhập hoặc Email này trong hệ thống!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Database: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State != ConnectionState.Closed)
                    sqlCon.Close();
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string tenDN_HienTai = txtEmailTenDN.Text.Trim();
            string otp_NhapVao = txtOTP.Text.Trim();

            // 1. Kiểm tra xem đã bấm Lấy mã chưa
            if (string.IsNullOrEmpty(serverOTP))
            {
                MessageBox.Show("Vui lòng bấm nút 'Lấy mã' trước!");
                return;
            }

            // 2. Bảo mật: Kiểm tra xem người dùng có đổi tên đăng nhập khác sau khi lấy mã không
            if (tenDN_HienTai != userDaLayMa)
            {
                MessageBox.Show($"Bạn đang nhập mã cho tài khoản '{userDaLayMa}' nhưng lại sửa tên thành '{tenDN_HienTai}'!\nVui lòng nhập lại đúng thông tin cũ.", "Sai thông tin");
                txtEmailTenDN.Text = userDaLayMa; // Tự sửa lại cho đúng
                return;
            }
            // 3. So sánh OTP
            if (otp_NhapVao == serverOTP)
            {
                lblKetQua.Text = "Xác minh thành công!";
                lblKetQua.ForeColor = Color.Green;
                lblKetQua.Visible = true;

                // Hiện khung đổi mật khẩu
                panelDoiMK.Visible = true;
                panelDoiMK.Enabled = true;
                panelDoiMK.BringToFront();

                // Khóa các ô bên trên lại
                txtEmailTenDN.ReadOnly = true;
                txtOTP.ReadOnly = true;
                btnLayMa.Enabled = false;
                btnXacNhan.Enabled = false;
            }
            else
            {
                MessageBox.Show("Mã OTP không đúng! Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOTP.Focus();
            }
        }

        private void btnLuuMatKhau_Click(object sender, EventArgs e)
        {
            string mkMoi = txtMatKhauMoi.Text;
            string nhapLai = txtNhapLaiMK.Text;

            if (mkMoi.Length < 3)
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
                if (sqlCon == null) sqlCon = DatabaseConnection.GetConnection();
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

                // Câu lệnh UPDATE thông minh: Tìm người dùng theo Tên ĐN hoặc Email (dựa vào biến userDaLayMa đã lưu)
                string updateQuery = @"UPDATE TaiKhoan 
                                           SET MatKhau = @mk 
                                           FROM TaiKhoan T
                                           JOIN KhachHang K ON T.MaKH = K.MaKH
                                           WHERE T.TenDangNhap = @user OR K.Email = @user";

                SqlCommand cmd = new SqlCommand(updateQuery, sqlCon);
                cmd.Parameters.AddWithValue("@mk", mkMoi);
                cmd.Parameters.AddWithValue("@user", userDaLayMa); // Dùng biến toàn cục cho an toàn

                int kq = cmd.ExecuteNonQuery();

                if (kq > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.");
                    this.Hide(); // Ẩn form quên mật khẩu đi

                    frmLogin login = new frmLogin(); // Tạo mới form đăng nhập
                    login.Show(); // Hiện form đăng nhập lên
                }
                else
                {
                    MessageBox.Show("Lỗi: Không tìm thấy tài khoản để cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
            finally
            {
                if (sqlCon != null && sqlCon.State != ConnectionState.Closed)
                    sqlCon.Close();
            }
        }

        private void frmQuenMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Chỉ hỏi khi người dùng bấm nút X hoặc Alt+F4
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy và quay lại đăng nhập?",
                                                      "Xác nhận",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                   // e.Cancel = true; // Giữ nguyên form, không cho tắt
                    //this.Close();
                }
                else
                {
                    //frmLogin Login = new frmLogin();
                    //Login.Show();

                }
            }
        }
    }
}