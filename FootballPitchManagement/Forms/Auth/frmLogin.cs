using FootballPitchManagement.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FootballPitchManagement
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Test kết nối khi form load
            TestDatabaseConnection();
        }

        private void TestDatabaseConnection()
        {
            if (!DatabaseConnection.TestConnection(out string error))
            {
                DatabaseConnection.ShowConnectionError(error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập hoặc email!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                // ✅ SỬ DỤNG DatabaseConnection
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    
                    // ✅ SỬA QUERY ĐỂ HỖ TRỢ ĐĂNG NHẬP BẰNG CẢ TÊN ĐĂNG NHẬP VÀ EMAIL
                    string query = @"SELECT tk.TenDangNhap, tk.MaLoaiTK, kh.HoTen, kh.Email
                                    FROM TaiKhoan tk
                                    LEFT JOIN KhachHang kh ON tk.MaKH = kh.MaKH
                                    WHERE (tk.TenDangNhap = @loginInput OR kh.Email = @loginInput)
                                    AND tk.MatKhau = @password
                                    AND tk.TrangThai = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@loginInput", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tenKhachHang = reader["HoTen"]?.ToString() ?? "Người dùng";
                                string email = reader["Email"]?.ToString() ?? "";
                                int maLoaiTK = Convert.ToInt32(reader["MaLoaiTK"]);
                                
                                // ✅ HIỂN THỊ THÔNG TIN ĐĂNG NHẬP THÀNH CÔNG
                                string loginMethod = !string.IsNullOrEmpty(email) && txtUsername.Text.Trim().Contains("@") 
                                    ? "email" : "tên đăng nhập";
                                
                                MessageBox.Show($"Xin chào {tenKhachHang}!\nĐăng nhập thành công bằng {loginMethod}!", 
                                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                this.Hide();
                                
                                // Mở form tương ứng với loại tài khoản
                                if (maLoaiTK == 1) // Admin
                                {
                                    frmAdmin frm = new frmAdmin();
                                    frm.ShowDialog();
                                }   
                                else // Khách hàng
                                {
                                    frmMainKH frm = new frmMainKH();
                                    frm.TenKhachHang = tenKhachHang;
                                    frm.ShowDialog();
                                }
                                
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập/Email hoặc mật khẩu không đúng!", 
                                    "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtPassword.Clear();
                                txtPassword.Focus();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi SQL:\n\n{ex.Message}\n\nMã lỗi: {ex.Number}",
                    "Lỗi Database",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picHide_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                picShow.BringToFront();
                txtPassword.PasswordChar = '\0';
            }
        }

        private void picShow_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                picHide.BringToFront();
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegister register = new frmRegister();
            this.Hide();
            register.ShowDialog();
            this.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmQuenMatKhau qmk = new frmQuenMatKhau();
            this.Hide();
            qmk.ShowDialog();
            this.Show();
        }
    }
}

