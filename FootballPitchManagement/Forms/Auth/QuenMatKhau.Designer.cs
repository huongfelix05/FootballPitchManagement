namespace FootballPitchManagement
{
    partial class QuenMatKhau
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmailTenDN = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.panelDoiMK = new System.Windows.Forms.Panel();
            this.btnLuuMatKhau = new System.Windows.Forms.Button();
            this.txtNhapLaiMK = new System.Windows.Forms.TextBox();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKetQua = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLayMa = new System.Windows.Forms.Button();
            this.panelDoiMK.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(238, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đặt lại mật khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email/Tên đăng nhập";
            // 
            // txtEmailTenDN
            // 
            this.txtEmailTenDN.Location = new System.Drawing.Point(332, 168);
            this.txtEmailTenDN.Name = "txtEmailTenDN";
            this.txtEmailTenDN.Size = new System.Drawing.Size(315, 26);
            this.txtEmailTenDN.TabIndex = 2;
         
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.AutoSize = true;
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(550, 509);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(97, 30);
            this.btnXacNhan.TabIndex = 3;
            this.btnXacNhan.Text = "Xác Nhận";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // panelDoiMK
            // 
            this.panelDoiMK.Controls.Add(this.btnLuuMatKhau);
            this.panelDoiMK.Controls.Add(this.txtNhapLaiMK);
            this.panelDoiMK.Controls.Add(this.txtMatKhauMoi);
            this.panelDoiMK.Controls.Add(this.label5);
            this.panelDoiMK.Controls.Add(this.label4);
            this.panelDoiMK.Location = new System.Drawing.Point(171, 291);
            this.panelDoiMK.Name = "panelDoiMK";
            this.panelDoiMK.Size = new System.Drawing.Size(476, 212);
            this.panelDoiMK.TabIndex = 4;
          
            // 
            // btnLuuMatKhau
            // 
            this.btnLuuMatKhau.AutoSize = true;
            this.btnLuuMatKhau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuuMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnLuuMatKhau.Location = new System.Drawing.Point(17, 157);
            this.btnLuuMatKhau.Name = "btnLuuMatKhau";
            this.btnLuuMatKhau.Size = new System.Drawing.Size(128, 30);
            this.btnLuuMatKhau.TabIndex = 4;
            this.btnLuuMatKhau.Text = "Lưu mật khẩu";
            this.btnLuuMatKhau.UseVisualStyleBackColor = false;
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
            this.lblKetQua.Location = new System.Drawing.Point(167, 256);
            this.lblKetQua.Name = "lblKetQua";
            this.lblKetQua.Size = new System.Drawing.Size(72, 20);
            this.lblKetQua.TabIndex = 5;
            this.lblKetQua.Text = "Kết quả!!";
          
            // 
            // btnThoat
            // 
            this.btnThoat.AutoSize = true;
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(171, 509);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(97, 30);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtOTP
            // 
            this.txtOTP.Location = new System.Drawing.Point(281, 215);
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(246, 26);
            this.txtOTP.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nhập mã OTP";
            // 
            // btnLayMa
            // 
            this.btnLayMa.AutoSize = true;
            this.btnLayMa.Location = new System.Drawing.Point(533, 213);
            this.btnLayMa.Name = "btnLayMa";
            this.btnLayMa.Size = new System.Drawing.Size(114, 30);
            this.btnLayMa.TabIndex = 9;
            this.btnLayMa.Text = "Lấy mã";
            this.btnLayMa.UseVisualStyleBackColor = true;
            this.btnLayMa.Click += new System.EventHandler(this.btnLayMa_Click);
            // 
            // QuenMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(831, 596);
            this.Controls.Add(this.btnLayMa);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOTP);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.panelDoiMK);
            this.Controls.Add(this.lblKetQua);
            this.Controls.Add(this.txtEmailTenDN);
            this.Controls.Add(this.label2);
            this.Name = "QuenMatKhau";
            this.Text = "QuenMatKhau";
          
            this.panelDoiMK.ResumeLayout(false);
            this.panelDoiMK.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmailTenDN;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Panel panelDoiMK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.Button btnLuuMatKhau;
        private System.Windows.Forms.TextBox txtNhapLaiMK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblKetQua;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLayMa;
    }
}