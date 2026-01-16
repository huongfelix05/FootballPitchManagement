namespace FootballPitchManagement.Forms.Customer
{
    partial class frmHoSo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.rdoNu = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdoNam = new Guna.UI2.WinForms.Guna2RadioButton();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaKH = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();

            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlMain (Panel chính - Giờ sẽ chiếm toàn bộ Form)
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.btnThoat);
            this.pnlMain.Controls.Add(this.btnLuu);
            this.pnlMain.Controls.Add(this.txtDiaChi);
            this.pnlMain.Controls.Add(this.lblDiaChi);
            this.pnlMain.Controls.Add(this.dtpNgaySinh);
            this.pnlMain.Controls.Add(this.lblNgaySinh);
            this.pnlMain.Controls.Add(this.rdoNu);
            this.pnlMain.Controls.Add(this.rdoNam);
            this.pnlMain.Controls.Add(this.lblGioiTinh);
            this.pnlMain.Controls.Add(this.txtEmail);
            this.pnlMain.Controls.Add(this.lblEmail);
            this.pnlMain.Controls.Add(this.txtSDT);
            this.pnlMain.Controls.Add(this.lblSDT);
            this.pnlMain.Controls.Add(this.txtHoTen);
            this.pnlMain.Controls.Add(this.lblHoTen);
            this.pnlMain.Controls.Add(this.txtMaKH);
            this.pnlMain.Controls.Add(this.lblMaKH);
            this.pnlMain.Controls.Add(this.labelHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill; // Lấp đầy toàn bộ
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1000, 650);
            this.pnlMain.TabIndex = 1;

            // --- CÁC CONTROL NHẬP LIỆU (Đã căn chỉnh lại vị trí cho cân đối hơn) ---

            // Header
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.labelHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.labelHeader.Location = new System.Drawing.Point(40, 30);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(355, 46);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "CÀI ĐẶT TÀI KHOẢN";

            // Mã KH
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMaKH.ForeColor = System.Drawing.Color.Gray;
            this.lblMaKH.Location = new System.Drawing.Point(50, 100);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(71, 25);
            this.lblMaKH.TabIndex = 1;
            this.lblMaKH.Text = "Mã KH";

            this.txtMaKH.BorderRadius = 8;
            this.txtMaKH.Enabled = false;
            this.txtMaKH.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtMaKH.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMaKH.Location = new System.Drawing.Point(50, 130);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(150, 45);
            this.txtMaKH.TabIndex = 2;

            // Họ Tên
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.Gray;
            this.lblHoTen.Location = new System.Drawing.Point(230, 100);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(98, 25);
            this.lblHoTen.TabIndex = 3;
            this.lblHoTen.Text = "Họ và tên";

            this.txtHoTen.BorderRadius = 8;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHoTen.Location = new System.Drawing.Point(230, 130);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(500, 45);
            this.txtHoTen.TabIndex = 4;

            // Số điện thoại
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSDT.ForeColor = System.Drawing.Color.Gray;
            this.lblSDT.Location = new System.Drawing.Point(50, 200);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(129, 25);
            this.lblSDT.TabIndex = 5;
            this.lblSDT.Text = "Số điện thoại";

            this.txtSDT.BorderRadius = 8;
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSDT.Location = new System.Drawing.Point(50, 230);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(320, 45);
            this.txtSDT.TabIndex = 6;

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.Gray;
            this.lblEmail.Location = new System.Drawing.Point(410, 200);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(59, 25);
            this.lblEmail.TabIndex = 7;
            this.lblEmail.Text = "Email";

            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.Location = new System.Drawing.Point(410, 230);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(320, 45);
            this.txtEmail.TabIndex = 8;

            // Giới tính
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.ForeColor = System.Drawing.Color.Gray;
            this.lblGioiTinh.Location = new System.Drawing.Point(50, 300);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(88, 25);
            this.lblGioiTinh.TabIndex = 9;
            this.lblGioiTinh.Text = "Giới tính";

            this.rdoNam.AutoSize = true;
            this.rdoNam.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.rdoNam.CheckedState.BorderThickness = 0;
            this.rdoNam.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.rdoNam.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdoNam.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rdoNam.Location = new System.Drawing.Point(50, 335);
            this.rdoNam.Name = "rdoNam";
            this.rdoNam.Size = new System.Drawing.Size(73, 29);
            this.rdoNam.TabIndex = 10;
            this.rdoNam.Text = "Nam";

            this.rdoNu.AutoSize = true;
            this.rdoNu.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.rdoNu.CheckedState.BorderThickness = 0;
            this.rdoNu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.rdoNu.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdoNu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rdoNu.Location = new System.Drawing.Point(140, 335);
            this.rdoNu.Name = "rdoNu";
            this.rdoNu.Size = new System.Drawing.Size(59, 29);
            this.rdoNu.TabIndex = 11;
            this.rdoNu.Text = "Nữ";

            // Ngày sinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.Gray;
            this.lblNgaySinh.Location = new System.Drawing.Point(410, 300);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(100, 25);
            this.lblNgaySinh.TabIndex = 12;
            this.lblNgaySinh.Text = "Ngày sinh";

            this.dtpNgaySinh.BorderRadius = 8;
            this.dtpNgaySinh.Checked = true;
            this.dtpNgaySinh.FillColor = System.Drawing.Color.White;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(410, 330);
            this.dtpNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(320, 45);
            this.dtpNgaySinh.TabIndex = 13;
            this.dtpNgaySinh.Value = new System.DateTime(2023, 10, 27, 0, 0, 0, 0);
            this.dtpNgaySinh.BorderThickness = 1;
            this.dtpNgaySinh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));

            // Địa chỉ
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.Gray;
            this.lblDiaChi.Location = new System.Drawing.Point(50, 400);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(71, 25);
            this.lblDiaChi.TabIndex = 14;
            this.lblDiaChi.Text = "Địa chỉ";

            this.txtDiaChi.BorderRadius = 8;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiaChi.Location = new System.Drawing.Point(50, 430);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(680, 100);
            this.txtDiaChi.TabIndex = 15;

            // Nút Lưu
            this.btnLuu.BorderRadius = 25;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96))))); // Xanh lá
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(400, 560);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(200, 50);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "LƯU THAY ĐỔI";
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;

            // Nút Thoát
            this.btnThoat.BorderRadius = 25;
            this.btnThoat.BorderThickness = 1;
            this.btnThoat.BorderColor = System.Drawing.Color.Gray;
            this.btnThoat.FillColor = System.Drawing.Color.White;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.Gray;
            this.btnThoat.Location = new System.Drawing.Point(620, 560);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(110, 50);
            this.btnThoat.TabIndex = 17;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;

            // Form HoSo
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlMain);
            this.Name = "frmHoSo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hồ sơ cá nhân";

            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private System.Windows.Forms.Label labelHeader;

        private System.Windows.Forms.Label lblMaKH;
        private Guna.UI2.WinForms.Guna2TextBox txtMaKH;

        private System.Windows.Forms.Label lblHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;

        private System.Windows.Forms.Label lblSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;

        private System.Windows.Forms.Label lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;

        private System.Windows.Forms.Label lblGioiTinh;
        private Guna.UI2.WinForms.Guna2RadioButton rdoNam;
        private Guna.UI2.WinForms.Guna2RadioButton rdoNu;

        private System.Windows.Forms.Label lblNgaySinh;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgaySinh;

        private System.Windows.Forms.Label lblDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;

        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
    }
}