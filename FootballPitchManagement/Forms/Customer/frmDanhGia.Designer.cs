namespace FootballPitchManagement.Forms.Customer
{
    partial class frmDanhGia : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDanhGia));
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
            // pnlCenter
            // 
            this.pnlCenter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenter.BackColor = System.Drawing.Color.Transparent;
            this.pnlCenter.BorderRadius = 20;
            this.pnlCenter.Controls.Add(this.btnGui);
            this.pnlCenter.Controls.Add(this.txtNhanXet);
            this.pnlCenter.Controls.Add(this.rtSao);
            this.pnlCenter.Controls.Add(this.cboSanBong);
            this.pnlCenter.Controls.Add(this.lblChonSan);
            this.pnlCenter.Controls.Add(this.lblTieuDe);
            this.pnlCenter.FillColor = System.Drawing.Color.White;
            this.pnlCenter.Location = new System.Drawing.Point(200, 80);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.ShadowDecoration.Depth = 20;
            this.pnlCenter.ShadowDecoration.Enabled = true;
            this.pnlCenter.Size = new System.Drawing.Size(500, 550);
            this.pnlCenter.TabIndex = 0;
            // 
            // btnGui
            // 
            this.btnGui.BorderRadius = 25;
            this.btnGui.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGui.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnGui.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(145)))), ((int)(((byte)(80)))));
            this.btnGui.Location = new System.Drawing.Point(100, 450);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(300, 50);
            this.btnGui.TabIndex = 5;
            this.btnGui.Text = "GỬI ĐÁNH GIÁ NGAY";
            // 
            // txtNhanXet
            // 
            this.txtNhanXet.BorderColor = System.Drawing.Color.Silver;
            this.txtNhanXet.BorderRadius = 10;
            this.txtNhanXet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhanXet.DefaultText = "";
            this.txtNhanXet.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNhanXet.Location = new System.Drawing.Point(40, 260);
            this.txtNhanXet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNhanXet.Multiline = true;
            this.txtNhanXet.Name = "txtNhanXet";
            this.txtNhanXet.PlaceholderText = "Hãy chia sẻ trải nghiệm của bạn về sân bóng, thái độ nhân viên...";
            this.txtNhanXet.SelectedText = "";
            this.txtNhanXet.Size = new System.Drawing.Size(420, 150);
            this.txtNhanXet.TabIndex = 4;
            // 
            // rtSao
            // 
            this.rtSao.BackColor = System.Drawing.Color.Transparent;
            this.rtSao.BorderColor = System.Drawing.Color.Gold;
            this.rtSao.Location = new System.Drawing.Point(140, 190);
            this.rtSao.Name = "rtSao";
            this.rtSao.RatingColor = System.Drawing.Color.Gold;
            this.rtSao.Size = new System.Drawing.Size(220, 50);
            this.rtSao.TabIndex = 3;
            this.rtSao.Value = 5F;
            // 
            // cboSanBong
            // 
            this.cboSanBong.BackColor = System.Drawing.Color.Transparent;
            this.cboSanBong.BorderColor = System.Drawing.Color.Silver;
            this.cboSanBong.BorderRadius = 10;
            this.cboSanBong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSanBong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanBong.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSanBong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSanBong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboSanBong.ForeColor = System.Drawing.Color.Black;
            this.cboSanBong.ItemHeight = 35;
            this.cboSanBong.Location = new System.Drawing.Point(40, 120);
            this.cboSanBong.Name = "cboSanBong";
            this.cboSanBong.Size = new System.Drawing.Size(420, 41);
            this.cboSanBong.TabIndex = 2;
            // 
            // lblChonSan
            // 
            this.lblChonSan.AutoSize = true;
            this.lblChonSan.BackColor = System.Drawing.Color.Transparent;
            this.lblChonSan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChonSan.ForeColor = System.Drawing.Color.Gray;
            this.lblChonSan.Location = new System.Drawing.Point(40, 90);
            this.lblChonSan.Name = "lblChonSan";
            this.lblChonSan.Size = new System.Drawing.Size(162, 23);
            this.lblChonSan.TabIndex = 1;
            this.lblChonSan.Text = "Bạn đã đá sân nào?";
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.BackColor = System.Drawing.Color.Transparent;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTieuDe.Location = new System.Drawing.Point(100, 30);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(300, 41);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐÁNH GIÁ DỊCH VỤ";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.pnlCenter);
            this.Name = "frmDanhGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đánh Giá";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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