namespace FootballPitchManagement.Forms.Customer
{
    partial class frmHoaDon1
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
            this.pnlDaThanhToan = new System.Windows.Forms.Panel();
            this.lblTongDaThanhToan = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTongChoThanhToan = new System.Windows.Forms.Label();
            this.lblChoThanhToan = new System.Windows.Forms.Label();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSapXep = new System.Windows.Forms.ComboBox();
            this.cboLoaiSan = new System.Windows.Forms.ComboBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.pnlDaThanhToan.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDaThanhToan
            // 
            this.pnlDaThanhToan.BackColor = System.Drawing.Color.White;
            this.pnlDaThanhToan.Controls.Add(this.lblTongDaThanhToan);
            this.pnlDaThanhToan.Controls.Add(this.lblTitle1);
            this.pnlDaThanhToan.Location = new System.Drawing.Point(79, 13);
            this.pnlDaThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDaThanhToan.Name = "pnlDaThanhToan";
            this.pnlDaThanhToan.Size = new System.Drawing.Size(467, 115);
            this.pnlDaThanhToan.TabIndex = 0;
            // 
            // lblTongDaThanhToan
            // 
            this.lblTongDaThanhToan.AutoSize = true;
            this.lblTongDaThanhToan.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDaThanhToan.ForeColor = System.Drawing.Color.Green;
            this.lblTongDaThanhToan.Location = new System.Drawing.Point(256, 70);
            this.lblTongDaThanhToan.Name = "lblTongDaThanhToan";
            this.lblTongDaThanhToan.Size = new System.Drawing.Size(77, 30);
            this.lblTongDaThanhToan.TabIndex = 1;
            this.lblTongDaThanhToan.Text = "0 VNĐ";
            this.lblTongDaThanhToan.Click += new System.EventHandler(this.lblTongDaThanhToan_Click);
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle1.Location = new System.Drawing.Point(62, 26);
            this.lblTitle1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(150, 18);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "✔ ĐÃ THANH TOÁN";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblTongChoThanhToan);
            this.panel1.Controls.Add(this.lblChoThanhToan);
            this.panel1.Location = new System.Drawing.Point(586, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 115);
            this.panel1.TabIndex = 1;
            // 
            // lblTongChoThanhToan
            // 
            this.lblTongChoThanhToan.AutoSize = true;
            this.lblTongChoThanhToan.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongChoThanhToan.ForeColor = System.Drawing.Color.Green;
            this.lblTongChoThanhToan.Location = new System.Drawing.Point(265, 70);
            this.lblTongChoThanhToan.Name = "lblTongChoThanhToan";
            this.lblTongChoThanhToan.Size = new System.Drawing.Size(77, 30);
            this.lblTongChoThanhToan.TabIndex = 1;
            this.lblTongChoThanhToan.Text = "0 VNĐ";
            // 
            // lblChoThanhToan
            // 
            this.lblChoThanhToan.AutoSize = true;
            this.lblChoThanhToan.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblChoThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoThanhToan.ForeColor = System.Drawing.Color.Orange;
            this.lblChoThanhToan.Location = new System.Drawing.Point(62, 26);
            this.lblChoThanhToan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChoThanhToan.Name = "lblChoThanhToan";
            this.lblChoThanhToan.Size = new System.Drawing.Size(142, 18);
            this.lblChoThanhToan.TabIndex = 1;
            this.lblChoThanhToan.Text = "CHỜ THANH TOÁN";
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Location = new System.Drawing.Point(79, 264);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(974, 327);
            this.dgvHoaDon.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(128, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chi nhánh:";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Location = new System.Drawing.Point(240, 212);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(172, 26);
            this.cboChiNhanh.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(128, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Loại sân";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(647, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sắp xếp";
            // 
            // cboSapXep
            // 
            this.cboSapXep.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSapXep.FormattingEnabled = true;
            this.cboSapXep.Items.AddRange(new object[] {
            " Mới nhất",
            "Tổng tiền: Cao -> Thấp",
            "Tổng tiền: Thấp -> Cao)."});
            this.cboSapXep.Location = new System.Drawing.Point(786, 217);
            this.cboSapXep.Name = "cboSapXep";
            this.cboSapXep.Size = new System.Drawing.Size(172, 26);
            this.cboSapXep.TabIndex = 7;
            // 
            // cboLoaiSan
            // 
            this.cboLoaiSan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoaiSan.FormattingEnabled = true;
            this.cboLoaiSan.Location = new System.Drawing.Point(240, 147);
            this.cboLoaiSan.Name = "cboLoaiSan";
            this.cboLoaiSan.Size = new System.Drawing.Size(172, 26);
            this.cboLoaiSan.TabIndex = 9;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(786, 156);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(172, 26);
            this.cboTrangThai.TabIndex = 10;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblTrangThai.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.Location = new System.Drawing.Point(647, 156);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(78, 18);
            this.lblTrangThai.TabIndex = 11;
            this.lblTrangThai.Text = "Trạng Thái";
            this.lblTrangThai.Click += new System.EventHandler(this.label4_Click);
            // 
            // frmHoaDon1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1096, 606);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.cboLoaiSan);
            this.Controls.Add(this.cboSapXep);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboChiNhanh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDaThanhToan);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Green;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmHoaDon1";
            this.Text = "Hóa Đơn";
            this.Load += new System.EventHandler(this.frmHoaDon1_Load);
            this.pnlDaThanhToan.ResumeLayout(false);
            this.pnlDaThanhToan.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDaThanhToan;
        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblTongDaThanhToan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTongChoThanhToan;
        private System.Windows.Forms.Label lblChoThanhToan;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSapXep;
        private System.Windows.Forms.ComboBox cboLoaiSan;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
    }
}