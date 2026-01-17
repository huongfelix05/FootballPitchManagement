namespace FootballPitchManagement.Forms.Customer
{
    partial class frmDanhGia
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
            this.pnlCenter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnGui = new Guna.UI2.WinForms.Guna2Button();
            this.txtNhanXet = new Guna.UI2.WinForms.Guna2TextBox();
            this.rtSao = new Guna.UI2.WinForms.Guna2RatingStar();
            this.cboSanBong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblChonSan = new System.Windows.Forms.Label();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.pnlCenter.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlCenter (Tấm thẻ trắng chứa nội dung)
            // 
            this.pnlCenter.Anchor = System.Windows.Forms.AnchorStyles.None; // Tự động căn giữa khi phóng to
            this.pnlCenter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenter.BorderRadius = 25; // Bo tròn góc thẻ
            this.pnlCenter.Controls.Add(this.btnGui);
            this.pnlCenter.Controls.Add(this.txtNhanXet);
            this.pnlCenter.Controls.Add(this.rtSao);
            this.pnlCenter.Controls.Add(this.cboSanBong);
            this.pnlCenter.Controls.Add(this.lblChonSan);
            this.pnlCenter.Controls.Add(this.lblTieuDe);
            this.pnlCenter.FillColor = System.Drawing.Color.White; // Nền trắng tinh khôi
            this.pnlCenter.Location = new System.Drawing.Point(192, 60); // Vị trí áng chừng (sẽ tự căn giữa nhờ Anchor)
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.ShadowDecoration.BorderRadius = 25;
            this.pnlCenter.ShadowDecoration.Color = System.Drawing.Color.Silver;
            this.pnlCenter.ShadowDecoration.Depth = 20; // Đổ bóng nhẹ tạo độ nổi
            this.pnlCenter.ShadowDecoration.Enabled = true;
            this.pnlCenter.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.pnlCenter.Size = new System.Drawing.Size(600, 580); // Kích thước thẻ
            this.pnlCenter.TabIndex = 0;

            // 
            // btnGui
            // 
            this.btnGui.BorderRadius = 25;
            this.btnGui.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGui.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGui.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGui.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGui.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGui.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148))))); // Màu xanh bạc hà đẹp
            this.btnGui.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(130)))));
            this.btnGui.Location = new System.Drawing.Point(125, 480);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(350, 55);
            this.btnGui.TabIndex = 5;
            this.btnGui.Text = "GỬI ĐÁNH GIÁ";

            // 
            // txtNhanXet
            // 
            this.txtNhanXet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
            this.txtNhanXet.BorderRadius = 10;
            this.txtNhanXet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhanXet.DefaultText = "";
            this.txtNhanXet.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNhanXet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNhanXet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanXet.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanXet.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNhanXet.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNhanXet.ForeColor = System.Drawing.Color.Black;
            this.txtNhanXet.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNhanXet.Location = new System.Drawing.Point(50, 270);
            this.txtNhanXet.Margin = new System.Windows.Forms.Padding(4);
            this.txtNhanXet.Multiline = true;
            this.txtNhanXet.Name = "txtNhanXet";
            this.txtNhanXet.PasswordChar = '\0';
            this.txtNhanXet.PlaceholderText = "Chia sẻ trải nghiệm của bạn về sân bóng, thái độ nhân viên...";
            this.txtNhanXet.SelectedText = "";
            this.txtNhanXet.Size = new System.Drawing.Size(500, 150);
            this.txtNhanXet.TabIndex = 4;

            // 
            // rtSao
            // 
            this.rtSao.BorderColor = System.Drawing.Color.Gold;
            this.rtSao.Location = new System.Drawing.Point(175, 200);
            this.rtSao.Name = "rtSao";
            this.rtSao.RatingColor = System.Drawing.Color.Gold;
            this.rtSao.Size = new System.Drawing.Size(250, 50); // Sao to rõ
            this.rtSao.TabIndex = 3;
            this.rtSao.Value = 5F;

            // 
            // cboSanBong
            // 
            this.cboSanBong.BackColor = System.Drawing.Color.Transparent;
            this.cboSanBong.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
            this.cboSanBong.BorderRadius = 10;
            this.cboSanBong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSanBong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanBong.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSanBong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSanBong.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cboSanBong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSanBong.ItemHeight = 40;
            this.cboSanBong.Location = new System.Drawing.Point(50, 130);
            this.cboSanBong.Name = "cboSanBong";
            this.cboSanBong.Size = new System.Drawing.Size(500, 46);
            this.cboSanBong.TabIndex = 2;

            // 
            // lblChonSan
            // 
            this.lblChonSan.AutoSize = true;
            this.lblChonSan.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonSan.ForeColor = System.Drawing.Color.Gray;
            this.lblChonSan.Location = new System.Drawing.Point(50, 100);
            this.lblChonSan.Name = "lblChonSan";
            this.lblChonSan.Size = new System.Drawing.Size(182, 25);
            this.lblChonSan.TabIndex = 1;
            this.lblChonSan.Text = "Bạn đã đá sân nào?";

            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(54)))));
            this.lblTieuDe.Location = new System.Drawing.Point(120, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(361, 50);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐÁNH GIÁ DỊCH VỤ";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // frmDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.pnlCenter);
            this.DoubleBuffered = true;
            this.Name = "frmDanhGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đánh Giá";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized; // Tự phóng to toàn màn hình
            this.pnlCenter.ResumeLayout(false);
            this.pnlCenter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlCenter;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblChonSan;
        private Guna.UI2.WinForms.Guna2ComboBox cboSanBong;
        private Guna.UI2.WinForms.Guna2RatingStar rtSao;
        private Guna.UI2.WinForms.Guna2TextBox txtNhanXet;
        private Guna.UI2.WinForms.Guna2Button btnGui;
    }
}