using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FootballPitchManagement.Common;

namespace FootballPitchManagement.Forms.Admin
{
    public partial class frmCaidat : Form
    {
        private DataTable dtChiNhanh;
        private Panel pnlCaiDatChung; // Panel cho cài đặt chung

        public frmCaidat()
        {   
            InitializeComponent();
        }

        private void frmCaidat_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra kết nối database
                if (!DatabaseConnection.TestConnection(out string error))
                {
                    DatabaseConnection.ShowConnectionError(error);
                    return;
                }

                CreateCaiDatChungPanel(); // Tạo panel cài đặt chung
                SetupDataGridView();
                LoadBranchData();
                
                // Gán sự kiện cho các nút
                btnChinhanh.Click += BtnChinhanh_Click;
                btnCaidatchung.Click += BtnCaidatchung_Click;
                
                // Mặc định hiển thị Quản lý chi nhánh
                ShowChiNhanhPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tạo Panel Cài đặt chung
        private void CreateCaiDatChungPanel()
        {
            pnlCaiDatChung = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false
            };

            // GroupBox Thông tin doanh nghiệp
            GroupBox gbThongTin = new GroupBox
            {
                Text = "Thông Tin Doanh Nghiệp",
                Location = new Point(20, 20),
                Size = new Size(700, 400),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            // Tên doanh nghiệp
            Label lblTenDN = new Label { Text = "Tên Doanh Nghiệp:", Location = new Point(30, 50), Size = new Size(150, 25) };
            TextBox txtTenDN = new TextBox { Name = "txtTenDoanhNghiep", Location = new Point(200, 47), Size = new Size(450, 25) };

            // Số điện thoại
            Label lblSDT = new Label { Text = "Số Điện Thoại:", Location = new Point(30, 90), Size = new Size(150, 25) };
            TextBox txtSDT = new TextBox { Name = "txtSoDienThoai", Location = new Point(200, 87), Size = new Size(450, 25) };

            // Email
            Label lblEmail = new Label { Text = "Email:", Location = new Point(30, 130), Size = new Size(150, 25) };
            TextBox txtEmail = new TextBox { Name = "txtEmail", Location = new Point(200, 127), Size = new Size(450, 25) };

            // Địa chỉ
            Label lblDiaChi = new Label { Text = "Địa Chỉ:", Location = new Point(30, 170), Size = new Size(150, 25) };
            TextBox txtDiaChi = new TextBox { 
                Name = "txtDiaChi", 
                Location = new Point(200, 167), 
                Size = new Size(450, 80),
                Multiline = true
            };

            // Nút Lưu
            Button btnLuu = new Button
            {
                Text = "💾 Lưu Thay Đổi",
                Location = new Point(200, 300),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnLuu.Click += BtnLuuCaiDat_Click;

            // Nút Hủy
            Button btnHuy = new Button
            {
                Text = "❌ Hủy",
                Location = new Point(370, 300),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnHuy.Click += (s, e) => LoadCaiDatChung();

            // Thêm controls vào GroupBox
            gbThongTin.Controls.AddRange(new Control[] {
                lblTenDN, txtTenDN, lblSDT, txtSDT, lblEmail, txtEmail,
                lblDiaChi, txtDiaChi, btnLuu, btnHuy
            });

            pnlCaiDatChung.Controls.Add(gbThongTin);

            // Thêm panel vào TableLayoutPanel
            tblMain.Controls.Add(pnlCaiDatChung, 1, 1);
            tblMain.SetColumnSpan(pnlCaiDatChung, 2);
            tblMain.SetRowSpan(pnlCaiDatChung, 3);
        }

        // Load dữ liệu cài đặt chung
        private void LoadCaiDatChung()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Giả sử có bảng DoanhNghiep trong database
                    string sql = "SELECT TOP 1 * FROM DoanhNghiep ORDER BY MaCauHinh DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FindTextBox("txtTenDoanhNghiep").Text = reader["TenDoanhNghiep"]?.ToString() ?? "Sân Bóng ABC";
                                FindTextBox("txtSoDienThoai").Text = reader["SoDienThoai"]?.ToString() ?? "1900-xxxx";
                                FindTextBox("txtEmail").Text = reader["Email"]?.ToString() ?? "contact@sanbongabc.com";
                                FindTextBox("txtDiaChi").Text = reader["DiaChi"]?.ToString() ?? "123 Đường ABC, Quận 1, TP.HCM";
                            }
                            else
                            {
                                // Nếu chưa có dữ liệu, tạo mẫu
                                InsertDefaultSettings(conn);
                                LoadCaiDatChung();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load cài đặt: {ex.Message}\n\nĐang sử dụng dữ liệu mặc định.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Set giá trị mặc định
                FindTextBox("txtTenDoanhNghiep").Text = "Sân Bóng ABC";
                FindTextBox("txtSoDienThoai").Text = "1900-xxxx";
                FindTextBox("txtEmail").Text = "contact@sanbongabc.com";
                FindTextBox("txtDiaChi").Text = "123 Đường ABC, Quận 1, TP.HCM";
            }
        }

        // Thêm dữ liệu mặc định vào bảng DoanhNghiep
        private void InsertDefaultSettings(SqlConnection conn)
        {
            try
            {
                string sql = @"
                    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DoanhNghiep')
                    BEGIN
                        CREATE TABLE DoanhNghiep (
                            MaCauHinh INT PRIMARY KEY IDENTITY(1,1),
                            TenDoanhNghiep NVARCHAR(200),
                            SoDienThoai VARCHAR(20),
                            Email VARCHAR(100),
                            DiaChi NVARCHAR(500),
                            NgayCapNhat DATETIME DEFAULT GETDATE()
                        )
                    END

                    IF NOT EXISTS (SELECT 1 FROM DoanhNghiep)
                    BEGIN
                        INSERT INTO DoanhNghiep (TenDoanhNghiep, SoDienThoai, Email, DiaChi)
                        VALUES (N'Sân Bóng ABC', '1900-xxxx', 'contact@sanbongabc.com', N'123 Đường ABC, Quận 1, TP.HCM')
                    END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        // Lưu cài đặt
        private void BtnLuuCaiDat_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDN = FindTextBox("txtTenDoanhNghiep").Text.Trim();
                string sdt = FindTextBox("txtSoDienThoai").Text.Trim();
                string email = FindTextBox("txtEmail").Text.Trim();
                string diaChi = FindTextBox("txtDiaChi").Text.Trim();

                if (string.IsNullOrEmpty(tenDN))
                {
                    MessageBox.Show("Vui lòng nhập tên doanh nghiệp!", "Cảnh báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        UPDATE DoanhNghiep 
                        SET TenDoanhNghiep = @TenDN,
                            SoDienThoai = @SDT,
                            Email = @Email,
                            DiaChi = @DiaChi,
                            NgayCapNhat = GETDATE()
                        WHERE MaCauHinh = (SELECT MAX(MaCauHinh) FROM DoanhNghiep)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDN", tenDN);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);

                        int result = cmd.ExecuteNonQuery();
                        
                        if (result > 0)
                        {
                            MessageBox.Show("✅ Lưu cài đặt thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu cài đặt: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method để tìm TextBox
        private TextBox FindTextBox(string name)
        {
            foreach (Control ctrl in pnlCaiDatChung.Controls)
            {
                if (ctrl is GroupBox gb)
                {
                    foreach (Control c in gb.Controls)
                    {
                        if (c is TextBox txt && txt.Name == name)
                            return txt;
                    }
                }
            }
            return new TextBox();
        }

        // Sự kiện nút Chi nhánh
        private void BtnChinhanh_Click(object sender, EventArgs e)
        {
            ShowChiNhanhPanel();
        }

        // Sự kiện nút Cài đặt chung
        private void BtnCaidatchung_Click(object sender, EventArgs e)
        {
            ShowCaiDatChungPanel();
        }

        // Hiển thị panel Chi nhánh
        private void ShowChiNhanhPanel()
        {
            btnChinhanh.FillColor = Color.FromArgb(94, 148, 255);
            btnCaidatchung.FillColor = Color.FromArgb(125, 137, 149);
            
            lblThongtin.Text = "QUẢN LÝ CHI NHÁNH";
            
            guna2Panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            guna2DataGridView1.Visible = true;
            pnlCaiDatChung.Visible = false;
        }

        // Hiển thị panel Cài đặt chung
        private void ShowCaiDatChungPanel()
        {
            btnCaidatchung.FillColor = Color.FromArgb(94, 148, 255);
            btnChinhanh.FillColor = Color.FromArgb(125, 137, 149);
            
            lblThongtin.Text = "CÀI ĐẶT CHUNG";
            
            guna2Panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            guna2DataGridView1.Visible = false;
            pnlCaiDatChung.Visible = true;
            
            LoadCaiDatChung();
        }

        private void SetupDataGridView()
        {
            // Cấu hình cơ bản DataGridView
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            guna2DataGridView1.MultiSelect = false;
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Sự kiện
            guna2DataGridView1.CellFormatting += Guna2DataGridView1_CellFormatting;
            guna2DataGridView1.CellContentClick += Guna2DataGridView1_CellContentClick;
        }

        private void LoadBranchData()
        {
            try
            {
                string sql = @"
                    SELECT 
                        cn.MaChiNhanh,
                        cn.TenChiNhanh,
                        cn.DiaChi,
                        cn.DienThoai,
                        cn.NguoiQuanLy,
                        cn.TrangThai,
                        COUNT(s.MaSan) as SoSan
                    FROM ChiNhanh cn
                    LEFT JOIN San s ON cn.MaChiNhanh = s.MaChiNhanh
                    GROUP BY cn.MaChiNhanh, cn.TenChiNhanh, cn.DiaChi, cn.DienThoai, 
                             cn.NguoiQuanLy, cn.TrangThai
                    ORDER BY cn.MaChiNhanh";

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                    {
                        dtChiNhanh = new DataTable();
                        adapter.Fill(dtChiNhanh);
                    }
                }

                DisplayBranchData();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayBranchData()
        {
            if (dtChiNhanh == null) return;

            // Clear columns
            guna2DataGridView1.Columns.Clear();

            // Add columns
            guna2DataGridView1.Columns.Add(CreateColumn("MaChiNhanh", "ID", 70));
            guna2DataGridView1.Columns.Add(CreateColumn("TenChiNhanh", "Tên Chi Nhánh", 200));
            guna2DataGridView1.Columns.Add(CreateColumn("DiaChi", "Địa Chỉ", 250));
            guna2DataGridView1.Columns.Add(CreateColumn("DienThoai", "Điện Thoại", 120));
            guna2DataGridView1.Columns.Add(CreateColumn("NguoiQuanLy", "Người Quản Lý", 150));
            guna2DataGridView1.Columns.Add(CreateColumn("SoSan", "Số Sân", 80));
            guna2DataGridView1.Columns.Add(CreateColumn("TrangThai", "Trạng Thái", 120));
            
            // Add edit button
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
            {
                Name = "btnEdit",
                HeaderText = "Thao Tác",
                Text = "Sửa",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            guna2DataGridView1.Columns.Add(btnEdit);

            // Bind data
            guna2DataGridView1.DataSource = dtChiNhanh;
        }

        private DataGridViewTextBoxColumn CreateColumn(string name, string headerText, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = headerText,
                Width = width,
                ReadOnly = true
            };
        }

        private void UpdateStatistics()
        {
            if (dtChiNhanh == null) return;

            int tongChiNhanh = dtChiNhanh.Rows.Count;
            int dangHoatDong = dtChiNhanh.AsEnumerable().Count(row => row.Field<bool>("TrangThai"));

            // Cập nhật label
            lblSochinhanh.Text = tongChiNhanh.ToString();
            label4.Text = dangHoatDong.ToString();
        }

        private void Guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                var colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;

                // Format ID
                if (colName == "MaChiNhanh" && e.Value != null)
                {
                    e.Value = $"#{e.Value:000}";
                    e.FormattingApplied = true;
                }

                // Format trạng thái
                if (colName == "TrangThai" && e.Value != null)
                {
                    bool trangThai = Convert.ToBoolean(e.Value);
                    e.Value = trangThai ? "Hoạt động" : "Tạm dừng";
                    e.FormattingApplied = true;
                }
            }
            catch { }
        }

        private void Guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            try
            {
                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    var row = guna2DataGridView1.Rows[e.RowIndex];
                    int maChiNhanh = Convert.ToInt32(row.Cells["MaChiNhanh"].Value);
                    string tenChiNhanh = row.Cells["TenChiNhanh"].Value?.ToString();
                    string diaChi = row.Cells["DiaChi"].Value?.ToString();
                    string dienThoai = row.Cells["DienThoai"].Value?.ToString();
                    string nguoiQuanLy = row.Cells["NguoiQuanLy"].Value?.ToString();
                    bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);

                    ShowEditForm(maChiNhanh, tenChiNhanh, diaChi, dienThoai, nguoiQuanLy, trangThai);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEditForm(int maChiNhanh, string tenChiNhanh, string diaChi, 
                                 string dienThoai, string nguoiQuanLy, bool trangThai)
        {
            Form editForm = new Form
            {
                Text = $"Sửa Chi Nhánh #{maChiNhanh:000}",
                Size = new Size(500, 320),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblTen = new Label { Text = "Tên Chi Nhánh:", Location = new Point(20, 30), Size = new Size(120, 23) };
            TextBox txtTen = new TextBox { Text = tenChiNhanh, Location = new Point(150, 27), Size = new Size(300, 23) };

            Label lblDiaChi = new Label { Text = "Địa Chỉ:", Location = new Point(20, 70), Size = new Size(120, 23) };
            TextBox txtDiaChi = new TextBox { Text = diaChi, Location = new Point(150, 67), Size = new Size(300, 23) };

            Label lblSDT = new Label { Text = "Điện Thoại:", Location = new Point(20, 110), Size = new Size(120, 23) };
            TextBox txtSDT = new TextBox { Text = dienThoai, Location = new Point(150, 107), Size = new Size(300, 23) };

            Label lblQL = new Label { Text = "Người Quản Lý:", Location = new Point(20, 150), Size = new Size(120, 23) };
            TextBox txtQL = new TextBox { Text = nguoiQuanLy, Location = new Point(150, 147), Size = new Size(300, 23) };

            Label lblTrangThai = new Label { Text = "Trạng Thái:", Location = new Point(20, 190), Size = new Size(120, 23) };
            CheckBox chkTrangThai = new CheckBox { 
                Text = "Hoạt động", 
                Checked = trangThai, 
                Location = new Point(150, 187), 
                Size = new Size(100, 23) 
            };

            Button btnSave = new Button { 
                Text = "Lưu", 
                Location = new Point(250, 240), 
                Size = new Size(90, 30)
            };

            Button btnCancel = new Button { 
                Text = "Hủy", 
                Location = new Point(360, 240), 
                Size = new Size(90, 30)
            };

            editForm.Controls.AddRange(new Control[] { 
                lblTen, txtTen, lblDiaChi, txtDiaChi, lblSDT, txtSDT, 
                lblQL, txtQL, lblTrangThai, chkTrangThai, btnSave, btnCancel 
            });

            btnSave.Click += (s, e) => {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtTen.Text))
                    {
                        MessageBox.Show("Tên chi nhánh không được để trống!", "Cảnh báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    UpdateBranch(maChiNhanh, txtTen.Text, txtDiaChi.Text, 
                               txtSDT.Text, txtQL.Text, chkTrangThai.Checked);
                    LoadBranchData();
                    editForm.Close();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, e) => editForm.Close();

            editForm.ShowDialog(this);
        }

        private void UpdateBranch(int maChiNhanh, string tenChiNhanh, string diaChi, 
                                 string dienThoai, string nguoiQuanLy, bool trangThai)
        {
            string sql = @"
                UPDATE ChiNhanh 
                SET TenChiNhanh = @TenChiNhanh,
                    DiaChi = @DiaChi,
                    DienThoai = @DienThoai,
                    NguoiQuanLy = @NguoiQuanLy,
                    TrangThai = @TrangThai
                WHERE MaChiNhanh = @MaChiNhanh";

            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaChiNhanh", maChiNhanh);
                    cmd.Parameters.AddWithValue("@TenChiNhanh", tenChiNhanh);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi ?? "");
                    cmd.Parameters.AddWithValue("@DienThoai", dienThoai ?? "");
                    cmd.Parameters.AddWithValue("@NguoiQuanLy", nguoiQuanLy ?? "");
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
