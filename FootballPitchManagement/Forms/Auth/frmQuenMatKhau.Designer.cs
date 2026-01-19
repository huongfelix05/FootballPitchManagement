namespace FootballPitchManagement
{
    partial class frmQuenMatKhau
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmailTenDN = new System.Windows.Forms.TextBox();
            this.panelDoiMK = new System.Windows.Forms.Panel();
            this.btnLuuMatKhau = new Guna.UI2.WinForms.Guna2GradientButton();
            this.txtNhapLaiMK = new System.Windows.Forms.TextBox();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnLayMa = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnThoat = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.panelDoiMK.SuspendLayout();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(124, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đặt lại mật khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email/Tên đăng nhập";
            // 
            // txtEmailTenDN
            // 
            this.txtEmailTenDN.Location = new System.Drawing.Point(218, 122);
            this.txtEmailTenDN.Name = "txtEmailTenDN";
            this.txtEmailTenDN.Size = new System.Drawing.Size(315, 26);
            this.txtEmailTenDN.TabIndex = 2;
            this.txtEmailTenDN.TextChanged += new System.EventHandler(this.txtEmailTenDN_TextChanged_1);
            // 
            // panelDoiMK
            // 
            this.panelDoiMK.Controls.Add(this.btnLuuMatKhau);
            this.panelDoiMK.Controls.Add(this.txtNhapLaiMK);
            this.panelDoiMK.Controls.Add(this.txtMatKhauMoi);
            this.panelDoiMK.Controls.Add(this.label5);
            this.panelDoiMK.Controls.Add(this.label4);
            this.panelDoiMK.Location = new System.Drawing.Point(57, 267);
            this.panelDoiMK.Name = "panelDoiMK";
            this.panelDoiMK.Size = new System.Drawing.Size(476, 212);
            this.panelDoiMK.TabIndex = 4;
            this.panelDoiMK.Visible = false;
            // 
            // btnLuuMatKhau
            // 
            this.btnLuuMatKhau.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuuMatKhau.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuuMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuuMatKhau.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuuMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuuMatKhau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(155)))));
            this.btnLuuMatKhau.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(201)))), ((int)(((byte)(61)))));
            this.btnLuuMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuuMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnLuuMatKhau.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.btnLuuMatKhau.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(201)))), ((int)(((byte)(61)))));
            this.btnLuuMatKhau.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(155)))));
            this.btnLuuMatKhau.Location = new System.Drawing.Point(17, 156);
            this.btnLuuMatKhau.Name = "btnLuuMatKhau";
            this.btnLuuMatKhau.Size = new System.Drawing.Size(168, 35);
            this.btnLuuMatKhau.TabIndex = 4;
            this.btnLuuMatKhau.Text = "Lưu Mật Khẩu";
            this.btnLuuMatKhau.Click += new System.EventHandler(this.btnLuuMatKhau_Click);
            // 
            // txtNhapLaiMK
            // 
            this.txtNhapLaiMK.Location = new System.Drawing.Point(161, 99);
            this.txtNhapLaiMK.Name = "txtNhapLaiMK";
            this.txtNhapLaiMK.PasswordChar = '*';
            this.txtNhapLaiMK.Size = new System.Drawing.Size(301, 26);
            this.txtNhapLaiMK.TabIndex = 3;
            this.txtNhapLaiMK.TextChanged += new System.EventHandler(this.txtNhapLaiMK_TextChanged);
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Location = new System.Drawing.Point(161, 41);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '*';
            this.txtMatKhauMoi.Size = new System.Drawing.Size(301, 26);
            this.txtMatKhauMoi.TabIndex = 2;
            this.txtMatKhauMoi.TextChanged += new System.EventHandler(this.txtMatKhauMoi_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Nhập lại mật khẩu:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Đặt mật khẩu mới:";
            // 
            // lblKetQua
            // 
            this.lblKetQua.AutoSize = true;
            this.lblKetQua.Location = new System.Drawing.Point(53, 210);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(72, 20);
            this.lblKetQua.TabIndex = 5;
            this.lblKetQua.Text = "Kết quả!!";
            this.lblKetQua.Visible = false;
            this.lblKetQua.Click += new System.EventHandler(this.lblKetQua_Click);
            // 
            // txtOTP
            // 
            this.txtOTP.Location = new System.Drawing.Point(167, 169);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(197, 26);
            this.txtOTP.TabIndex = 7;
            this.txtOTP.TextChanged += new System.EventHandler(this.txtOTP_TextChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nhập mã OTP";
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.btnXacNhan);
            this.guna2ShadowPanel1.Controls.Add(this.btnLayMa);
            this.guna2ShadowPanel1.Controls.Add(this.btnThoat);
            this.guna2ShadowPanel1.Controls.Add(this.txtEmailTenDN);
            this.guna2ShadowPanel1.Controls.Add(this.lblKetQua);
            this.guna2ShadowPanel1.Controls.Add(this.txtOTP);
            this.guna2ShadowPanel1.Controls.Add(this.label3);
            this.guna2ShadowPanel1.Controls.Add(this.panelDoiMK);
            this.guna2ShadowPanel1.Controls.Add(this.label1);
            this.guna2ShadowPanel1.Controls.Add(this.label2);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(290, 75);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 20;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(579, 538);
            this.guna2ShadowPanel1.TabIndex = 11;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.FillColor = System.Drawing.Color.OrangeRed;
            this.btnXacNhan.FillColor2 = System.Drawing.Color.Gold;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnXacNhan.HoverState.FillColor = System.Drawing.Color.Gold;
            this.btnXacNhan.HoverState.FillColor2 = System.Drawing.Color.OrangeRed;
            this.btnXacNhan.Location = new System.Drawing.Point(391, 220);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(142, 35);
            this.btnXacNhan.TabIndex = 12;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnLayMa
            // 
            this.btnLayMa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLayMa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLayMa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLayMa.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLayMa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLayMa.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnLayMa.FillColor2 = System.Drawing.Color.Cyan;
            this.btnLayMa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLayMa.ForeColor = System.Drawing.Color.White;
            this.btnLayMa.HoverState.FillColor = System.Drawing.Color.Cyan;
            this.btnLayMa.HoverState.FillColor2 = System.Drawing.Color.DodgerBlue;
            this.btnLayMa.Location = new System.Drawing.Point(370, 169);
            this.btnLayMa.Name = "btnLayMa";
            this.btnLayMa.Size = new System.Drawing.Size(163, 26);
            this.btnLayMa.TabIndex = 11;
            this.btnLayMa.Text = "Lấy mã";
            this.btnLayMa.Click += new System.EventHandler(this.btnLayMa_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThoat.BackColor = System.Drawing.Color.Transparent;
            this.btnThoat.FillColor = System.Drawing.Color.Transparent;
            this.btnThoat.IconColor = System.Drawing.Color.Black;
            this.btnThoat.Location = new System.Drawing.Point(506, 30);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(45, 29);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // guna2AnimateWindow1
            // 
            this.guna2AnimateWindow1.TargetForm = this;
            // 
            // frmQuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.BackgroundImage = global::FootballPitchManagement.Properties.Resources.pngtree_soccer_ball_lying_on_vibrant_green_grass_field_image_16734728;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1231, 753);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQuenMatKhau";
            this.Text = "QuenMatKhau";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmQuenMatKhau_FormClosing);
            this.Load += new System.EventHandler(this.frmQuenMatKhau_Load);
            this.panelDoiMK.ResumeLayout(false);
            this.panelDoiMK.PerformLayout();
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmailTenDN;
        private System.Windows.Forms.Panel panelDoiMK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.TextBox txtNhapLaiMK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2ControlBox btnThoat;
        private Guna.UI2.WinForms.Guna2GradientButton btnLayMa;
        private Guna.UI2.WinForms.Guna2GradientButton btnXacNhan;
        private Guna.UI2.WinForms.Guna2GradientButton btnLuuMatKhau;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
    }
}