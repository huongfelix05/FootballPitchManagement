namespace FootballPitchManagement.Forms.Admin
{
    partial class frmDoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlHomNay = new System.Windows.Forms.Panel();
            this.lblTieuDe1 = new System.Windows.Forms.Label();
            this.lblSoTienHomNay = new System.Windows.Forms.Label();
            this.pnlTuanNay = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblTieuDe2 = new System.Windows.Forms.Label();
            this.lblSoTienTuanNay = new System.Windows.Forms.Label();
            this.pnlThangNay = new System.Windows.Forms.Panel();
            this.pnlTongDoanhThu = new System.Windows.Forms.Panel();
            this.lblTieuDe3 = new System.Windows.Forms.Label();
            this.lblTieuDe4 = new System.Windows.Forms.Label();
            this.lblSoTienThangNay = new System.Windows.Forms.Label();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.DoanhThu = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnlHomNay.SuspendLayout();
            this.pnlTuanNay.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.pnlThangNay.SuspendLayout();
            this.pnlTongDoanhThu.SuspendLayout();
            this.DoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXuatBaoCao);
            this.groupBox1.Controls.Add(this.btnThongKe);
            this.groupBox1.Controls.Add(this.cboChiNhanh);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpDenNgay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpTuNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1021, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bộ Lọc Dữ Liệu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(79, 19);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(170, 22);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(79, 47);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(170, 22);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chi Nhánh:";
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Location = new System.Drawing.Point(355, 21);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(121, 24);
            this.cboChiNhanh.TabIndex = 5;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(498, 22);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(106, 23);
            this.btnThongKe.TabIndex = 6;
            this.btnThongKe.Text = "Lọc Dữ Liệu";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Location = new System.Drawing.Point(894, 12);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(75, 23);
            this.btnXuatBaoCao.TabIndex = 7;
            this.btnXuatBaoCao.Text = "Xuất Excel";
            this.btnXuatBaoCao.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 68);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1021, 64);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlHomNay);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 58);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // pnlHomNay
            // 
            this.pnlHomNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHomNay.Controls.Add(this.lblSoTienHomNay);
            this.pnlHomNay.Controls.Add(this.lblTieuDe1);
            this.pnlHomNay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHomNay.Location = new System.Drawing.Point(3, 18);
            this.pnlHomNay.Name = "pnlHomNay";
            this.pnlHomNay.Size = new System.Drawing.Size(243, 37);
            this.pnlHomNay.TabIndex = 0;
            // 
            // lblTieuDe1
            // 
            this.lblTieuDe1.AutoSize = true;
            this.lblTieuDe1.Location = new System.Drawing.Point(7, 0);
            this.lblTieuDe1.Name = "lblTieuDe1";
            this.lblTieuDe1.Size = new System.Drawing.Size(127, 16);
            this.lblTieuDe1.TabIndex = 0;
            this.lblTieuDe1.Text = "Doanh thu Hôm Nay";
            // 
            // lblSoTienHomNay
            // 
            this.lblSoTienHomNay.AutoSize = true;
            this.lblSoTienHomNay.Location = new System.Drawing.Point(21, 16);
            this.lblSoTienHomNay.Name = "lblSoTienHomNay";
            this.lblSoTienHomNay.Size = new System.Drawing.Size(25, 16);
            this.lblSoTienHomNay.TabIndex = 1;
            this.lblSoTienHomNay.Text = "0 đ";
            // 
            // pnlTuanNay
            // 
            this.pnlTuanNay.Controls.Add(this.lblSoTienTuanNay);
            this.pnlTuanNay.Controls.Add(this.lblTieuDe2);
            this.pnlTuanNay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTuanNay.Location = new System.Drawing.Point(3, 18);
            this.pnlTuanNay.Name = "pnlTuanNay";
            this.pnlTuanNay.Size = new System.Drawing.Size(243, 37);
            this.pnlTuanNay.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlTuanNay);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(258, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 58);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pnlThangNay);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(513, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 58);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pnlTongDoanhThu);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(768, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(250, 58);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "groupBox5";
            // 
            // lblTieuDe2
            // 
            this.lblTieuDe2.AutoSize = true;
            this.lblTieuDe2.Location = new System.Drawing.Point(17, 1);
            this.lblTieuDe2.Name = "lblTieuDe2";
            this.lblTieuDe2.Size = new System.Drawing.Size(129, 16);
            this.lblTieuDe2.TabIndex = 1;
            this.lblTieuDe2.Text = "Doanh thu Tuần Này";
            // 
            // lblSoTienTuanNay
            // 
            this.lblSoTienTuanNay.AutoSize = true;
            this.lblSoTienTuanNay.Location = new System.Drawing.Point(36, 17);
            this.lblSoTienTuanNay.Name = "lblSoTienTuanNay";
            this.lblSoTienTuanNay.Size = new System.Drawing.Size(25, 16);
            this.lblSoTienTuanNay.TabIndex = 2;
            this.lblSoTienTuanNay.Text = "0 đ";
            // 
            // pnlThangNay
            // 
            this.pnlThangNay.Controls.Add(this.lblSoTienThangNay);
            this.pnlThangNay.Controls.Add(this.lblTieuDe3);
            this.pnlThangNay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlThangNay.Location = new System.Drawing.Point(3, 18);
            this.pnlThangNay.Name = "pnlThangNay";
            this.pnlThangNay.Size = new System.Drawing.Size(243, 37);
            this.pnlThangNay.TabIndex = 0;
            // 
            // pnlTongDoanhThu
            // 
            this.pnlTongDoanhThu.Controls.Add(this.lblTongDoanhThu);
            this.pnlTongDoanhThu.Controls.Add(this.lblTieuDe4);
            this.pnlTongDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTongDoanhThu.Location = new System.Drawing.Point(3, 18);
            this.pnlTongDoanhThu.Name = "pnlTongDoanhThu";
            this.pnlTongDoanhThu.Size = new System.Drawing.Size(244, 37);
            this.pnlTongDoanhThu.TabIndex = 0;
            // 
            // lblTieuDe3
            // 
            this.lblTieuDe3.AutoSize = true;
            this.lblTieuDe3.Location = new System.Drawing.Point(15, 1);
            this.lblTieuDe3.Name = "lblTieuDe3";
            this.lblTieuDe3.Size = new System.Drawing.Size(137, 16);
            this.lblTieuDe3.TabIndex = 2;
            this.lblTieuDe3.Text = "Doanh thu Tháng Này";
            // 
            // lblTieuDe4
            // 
            this.lblTieuDe4.AutoSize = true;
            this.lblTieuDe4.Location = new System.Drawing.Point(14, 1);
            this.lblTieuDe4.Name = "lblTieuDe4";
            this.lblTieuDe4.Size = new System.Drawing.Size(108, 16);
            this.lblTieuDe4.TabIndex = 2;
            this.lblTieuDe4.Text = "Tổng Doanh Thu";
            // 
            // lblSoTienThangNay
            // 
            this.lblSoTienThangNay.AutoSize = true;
            this.lblSoTienThangNay.Location = new System.Drawing.Point(27, 17);
            this.lblSoTienThangNay.Name = "lblSoTienThangNay";
            this.lblSoTienThangNay.Size = new System.Drawing.Size(25, 16);
            this.lblSoTienThangNay.TabIndex = 3;
            this.lblSoTienThangNay.Text = "0 đ";
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(28, 17);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(25, 16);
            this.lblTongDoanhThu.TabIndex = 3;
            this.lblTongDoanhThu.Text = "0 đ";
            // 
            // DoanhThu
            // 
            this.DoanhThu.ColumnCount = 2;
            this.DoanhThu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.DoanhThu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.DoanhThu.Controls.Add(this.chart1, 0, 0);
            this.DoanhThu.Dock = System.Windows.Forms.DockStyle.Top;
            this.DoanhThu.Location = new System.Drawing.Point(0, 132);
            this.DoanhThu.Name = "DoanhThu";
            this.DoanhThu.RowCount = 1;
            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DoanhThu.Size = new System.Drawing.Size(1021, 202);
            this.DoanhThu.TabIndex = 2;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(708, 196);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // frmDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 738);
            this.Controls.Add(this.DoanhThu);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDoanhThu";
            this.Text = "frmDoanhThu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.pnlHomNay.ResumeLayout(false);
            this.pnlHomNay.PerformLayout();
            this.pnlTuanNay.ResumeLayout(false);
            this.pnlTuanNay.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.pnlThangNay.ResumeLayout(false);
            this.pnlThangNay.PerformLayout();
            this.pnlTongDoanhThu.ResumeLayout(false);
            this.pnlTongDoanhThu.PerformLayout();
            this.DoanhThu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXuatBaoCao;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlHomNay;
        private System.Windows.Forms.Label lblSoTienHomNay;
        private System.Windows.Forms.Label lblTieuDe1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnlTuanNay;
        private System.Windows.Forms.Label lblSoTienTuanNay;
        private System.Windows.Forms.Label lblTieuDe2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel pnlThangNay;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel pnlTongDoanhThu;
        private System.Windows.Forms.Label lblSoTienThangNay;
        private System.Windows.Forms.Label lblTieuDe3;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Label lblTieuDe4;
        private System.Windows.Forms.TableLayoutPanel DoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}