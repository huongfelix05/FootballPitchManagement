namespace FootballPitchManagement.Forms.Customer
{
    partial class frmDanhGia
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
            this.cboSanBong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.rtSao = new Guna.UI2.WinForms.Guna2RatingStar();
            this.txtNhanXet = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnGui = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(285, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐÁNH GIÁ DỊCH VỤ";
            // 
            // cboSanBong
            // 
            this.cboSanBong.BackColor = System.Drawing.Color.Transparent;
            this.cboSanBong.BorderRadius = 8;
            this.cboSanBong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSanBong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanBong.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSanBong.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSanBong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSanBong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSanBong.ItemHeight = 30;
            this.cboSanBong.Location = new System.Drawing.Point(84, 101);
            this.cboSanBong.Name = "cboSanBong";
            this.cboSanBong.Size = new System.Drawing.Size(140, 36);
            this.cboSanBong.TabIndex = 1;
            // 
            // rtSao
            // 
            this.rtSao.Location = new System.Drawing.Point(84, 204);
            this.rtSao.Name = "rtSao";
            this.rtSao.Size = new System.Drawing.Size(120, 28);
            this.rtSao.TabIndex = 0;
            // 
            // txtNhanXet
            // 
            this.txtNhanXet.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhanXet.DefaultText = "";
            this.txtNhanXet.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNhanXet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNhanXet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanXet.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanXet.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNhanXet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNhanXet.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNhanXet.Location = new System.Drawing.Point(84, 239);
            this.txtNhanXet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNhanXet.Multiline = true;
            this.txtNhanXet.Name = "txtNhanXet";
            this.txtNhanXet.PlaceholderText = "Nhận Xét";
            this.txtNhanXet.SelectedText = "";
            this.txtNhanXet.Size = new System.Drawing.Size(229, 48);
            this.txtNhanXet.TabIndex = 2;
            // 
            // btnGui
            // 
            this.btnGui.BorderRadius = 20;
            this.btnGui.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGui.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGui.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGui.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGui.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.Location = new System.Drawing.Point(486, 324);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(180, 45);
            this.btnGui.TabIndex = 3;
            this.btnGui.Text = "Gửi Đánh Giá";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(88, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Chọn Sân";
            // 
            // frmDanhGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.txtNhanXet);
            this.Controls.Add(this.rtSao);
            this.Controls.Add(this.cboSanBong);
            this.Controls.Add(this.label1);
            this.Name = "frmDanhGia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDanhGia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cboSanBong;
        private Guna.UI2.WinForms.Guna2RatingStar rtSao;
        private Guna.UI2.WinForms.Guna2TextBox txtNhanXet;
        private Guna.UI2.WinForms.Guna2Button btnGui;
        private System.Windows.Forms.Label label2;
    }
}