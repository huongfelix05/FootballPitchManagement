namespace FootballPitchManagement.Forms.Customer
{
    partial class frmBaocao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btntim = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSanTotNhat = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flpKhungGio = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanTotNhat)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.flpKhungGio.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.groupBox1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(1243, 180);
            this.pnlHeader.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1223, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BÁO CÁO";
            // 
            // btntim
            // 
            this.btntim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btntim.Location = new System.Drawing.Point(110, 47);
            this.btntim.Name = "btntim";
            this.btntim.Size = new System.Drawing.Size(268, 30);
            this.btntim.TabIndex = 7;
            this.btntim.Text = "tìm";
            this.btntim.UseVisualStyleBackColor = true;
            this.btntim.Click += new System.EventHandler(this.btntim_Click);
            // 
            // btnXem
            // 
            this.btnXem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.Location = new System.Drawing.Point(0, 0);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(395, 72);
            this.btnXem.TabIndex = 6;
            this.btnXem.Text = "Tạo Báo Cáo";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXemBaoCao_Click);
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Location = new System.Drawing.Point(110, 3);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(268, 31);
            this.cboChiNhanh.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chi nhánh:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến Ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(112, 51);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(281, 30);
            this.dtpDenNgay.TabIndex = 2;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(109, 6);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(281, 30);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Controls.Add(this.groupBox2, 0, 0);
            this.tblMain.Controls.Add(this.groupBox3, 1, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 180);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Size = new System.Drawing.Size(1243, 534);
            this.tblMain.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox2.Controls.Add(this.dgvSanTotNhat);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(615, 528);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SÂN HOẠT ĐỘNG TỐT NHẤT";
            // 
            // dgvSanTotNhat
            // 
            this.dgvSanTotNhat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanTotNhat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSanTotNhat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSanTotNhat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanTotNhat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvSanTotNhat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanTotNhat.EnableHeadersVisualStyles = false;
            this.dgvSanTotNhat.Location = new System.Drawing.Point(3, 32);
            this.dgvSanTotNhat.Name = "dgvSanTotNhat";
            this.dgvSanTotNhat.ReadOnly = true;
            this.dgvSanTotNhat.RowHeadersVisible = false;
            this.dgvSanTotNhat.RowHeadersWidth = 51;
            this.dgvSanTotNhat.RowTemplate.Height = 35;
            this.dgvSanTotNhat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanTotNhat.Size = new System.Drawing.Size(609, 493);
            this.dgvSanTotNhat.TabIndex = 0;
            this.dgvSanTotNhat.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvSanTotNhat_CellPainting);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Cornsilk;
            this.groupBox3.Controls.Add(this.flpKhungGio);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(624, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(616, 528);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "KHUNG GIỜ CAO ĐIỂM";
            // 
            // flpKhungGio
            // 
            this.flpKhungGio.AutoScroll = true;
            this.flpKhungGio.BackColor = System.Drawing.SystemColors.Window;
            this.flpKhungGio.Controls.Add(this.panel1);
            this.flpKhungGio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpKhungGio.Location = new System.Drawing.Point(3, 32);
            this.flpKhungGio.Name = "flpKhungGio";
            this.flpKhungGio.Size = new System.Drawing.Size(610, 493);
            this.flpKhungGio.TabIndex = 0;
            this.flpKhungGio.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 0);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1217, 123);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(399, 117);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ngày";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox5.Controls.Add(this.panel3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(408, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(399, 117);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Chi Nhánh";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox6.Controls.Add(this.panel4);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(813, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(401, 117);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Xuất";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpTuNgay);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtpDenNgay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(393, 86);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btntim);
            this.panel3.Controls.Add(this.cboChiNhanh);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(3, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(393, 86);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.btnXem);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(3, 28);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(395, 86);
            this.panel4.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Click:";
            // 
            // frmBaocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 714);
            this.Controls.Add(this.tblMain);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmBaocao";
            this.Text = "frmBaocao";
            this.Load += new System.EventHandler(this.FrmBaoCao_Load);
            this.pnlHeader.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanTotNhat)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.flpKhungGio.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSanTotNhat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btntim;
        private System.Windows.Forms.FlowLayoutPanel flpKhungGio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
    }
}