namespace FootballPitchManagement
{
    partial class ucSanBong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblGiaTien = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblTenSan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(0, 31);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(181, 82);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 7;
            this.picIcon.TabStop = false;
            // 
            // lblGiaTien
            // 
            this.lblGiaTien.AutoSize = true;
            this.lblGiaTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGiaTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaTien.Location = new System.Drawing.Point(0, 125);
            this.lblGiaTien.Name = "lblGiaTien";
            this.lblGiaTien.Size = new System.Drawing.Size(63, 16);
            this.lblGiaTien.TabIndex = 6;
            this.lblGiaTien.Text = "300.000 đ";
            this.lblGiaTien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGiaTien.Click += new System.EventHandler(this.lblGiaTien_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.Green;
            this.lblTrangThai.Location = new System.Drawing.Point(0, 141);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(52, 20);
            this.lblTrangThai.TabIndex = 5;
            this.lblTrangThai.Text = "Trống";
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenSan
            // 
            this.lblTenSan.AutoSize = true;
            this.lblTenSan.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTenSan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenSan.Location = new System.Drawing.Point(0, 0);
            this.lblTenSan.Name = "lblTenSan";
            this.lblTenSan.Size = new System.Drawing.Size(78, 28);
            this.lblTenSan.TabIndex = 4;
            this.lblTenSan.Text = "Sân A1";
            this.lblTenSan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucSanBong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.lblGiaTien);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblTenSan);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ucSanBong";
            this.Size = new System.Drawing.Size(181, 161);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblGiaTien;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblTenSan;
    }
}
