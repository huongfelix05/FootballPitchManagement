//namespace FootballPitchManagement.Forms.Admin
//{
//    partial class frmdoanhthuu
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
//            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
//            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
//            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
//            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
//            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
//            this.chartTyTrong = new System.Windows.Forms.DataVisualization.Charting.Chart();
//            this.DoanhThu = new System.Windows.Forms.TableLayoutPanel();
//            this.groupBox7 = new System.Windows.Forms.GroupBox();
//            this.chartDoanhThuNam = new System.Windows.Forms.DataVisualization.Charting.Chart();
//            this.groupBox8 = new System.Windows.Forms.GroupBox();
//            this.lblTongDoanhThu = new System.Windows.Forms.Label();
//            this.pnlTongDoanhThu = new System.Windows.Forms.Panel();
//            this.lblSoTienThangNay = new System.Windows.Forms.Label();
//            this.pnlThangNay = new System.Windows.Forms.Panel();
//            this.groupBox2 = new System.Windows.Forms.GroupBox();
//            this.pnlHomNay = new System.Windows.Forms.Panel();
//            this.lblSoTienHomNay = new System.Windows.Forms.Label();
//            this.lblSoTienTuanNay = new System.Windows.Forms.Label();
//            this.pnlTuanNay = new System.Windows.Forms.Panel();
//            this.groupBox3 = new System.Windows.Forms.GroupBox();
//            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
//            this.groupBox4 = new System.Windows.Forms.GroupBox();
//            this.groupBox5 = new System.Windows.Forms.GroupBox();
//            this.label2 = new System.Windows.Forms.Label();
//            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
//            this.label1 = new System.Windows.Forms.Label();
//            this.btnXuatBaoCao = new System.Windows.Forms.Button();
//            this.btnThongKe = new System.Windows.Forms.Button();
//            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
//            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
//            this.groupBox1 = new System.Windows.Forms.GroupBox();
//            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
//            this.panel3 = new System.Windows.Forms.Panel();
//            this.groupBox9 = new System.Windows.Forms.GroupBox();
//            this.panel2 = new System.Windows.Forms.Panel();
//            this.label4 = new System.Windows.Forms.Label();
//            this.label3 = new System.Windows.Forms.Label();
//            this.groupBox10 = new System.Windows.Forms.GroupBox();
//            this.panel1 = new System.Windows.Forms.Panel();
//            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
//            this.groupBox6 = new System.Windows.Forms.GroupBox();
//            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
//            this.groupBox11 = new System.Windows.Forms.GroupBox();
//            this.groupBox12 = new System.Windows.Forms.GroupBox();
//            this.panel4 = new System.Windows.Forms.Panel();
//            ((System.ComponentModel.ISupportInitialize)(this.chartTyTrong)).BeginInit();
//            this.DoanhThu.SuspendLayout();
//            this.groupBox7.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuNam)).BeginInit();
//            this.groupBox8.SuspendLayout();
//            this.pnlTongDoanhThu.SuspendLayout();
//            this.pnlThangNay.SuspendLayout();
//            this.groupBox2.SuspendLayout();
//            this.pnlHomNay.SuspendLayout();
//            this.pnlTuanNay.SuspendLayout();
//            this.groupBox3.SuspendLayout();
//            this.tableLayoutPanel1.SuspendLayout();
//            this.groupBox4.SuspendLayout();
//            this.groupBox5.SuspendLayout();
//            this.groupBox1.SuspendLayout();
//            this.tableLayoutPanel3.SuspendLayout();
//            this.panel3.SuspendLayout();
//            this.groupBox9.SuspendLayout();
//            this.panel2.SuspendLayout();
//            this.groupBox10.SuspendLayout();
//            this.panel1.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
//            this.groupBox6.SuspendLayout();
//            this.tableLayoutPanel2.SuspendLayout();
//            this.groupBox11.SuspendLayout();
//            this.groupBox12.SuspendLayout();
//            this.panel4.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // chartTyTrong
//            // 
//            chartArea7.Name = "ChartArea1";
//            this.chartTyTrong.ChartAreas.Add(chartArea7);
//            this.chartTyTrong.Dock = System.Windows.Forms.DockStyle.Fill;
//            legend7.Name = "Doanh Thu Đồ Ăn";
//            this.chartTyTrong.Legends.Add(legend7);
//            this.chartTyTrong.Location = new System.Drawing.Point(3, 26);
//            this.chartTyTrong.Name = "chartTyTrong";
//            series7.ChartArea = "ChartArea1";
//            series7.Legend = "Doanh Thu Đồ Ăn";
//            series7.Name = "Series1";
//            this.chartTyTrong.Series.Add(series7);
//            this.chartTyTrong.Size = new System.Drawing.Size(357, 213);
//            this.chartTyTrong.TabIndex = 1;
//            this.chartTyTrong.Text = "chart1";
//            // 
//            // DoanhThu
//            // 
//            this.DoanhThu.ColumnCount = 2;
//            this.DoanhThu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
//            this.DoanhThu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
//            this.DoanhThu.Controls.Add(this.groupBox7, 0, 0);
//            this.DoanhThu.Controls.Add(this.groupBox8, 1, 0);
//            this.DoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.DoanhThu.Location = new System.Drawing.Point(3, 103);
//            this.DoanhThu.Name = "DoanhThu";
//            this.DoanhThu.RowCount = 1;
//            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.DoanhThu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248F));
//            this.DoanhThu.Size = new System.Drawing.Size(1228, 248);
//            this.DoanhThu.TabIndex = 6;
//            this.DoanhThu.Paint += new System.Windows.Forms.PaintEventHandler(this.DoanhThu_Paint);
//            // 
//            // groupBox7
//            // 
//            this.groupBox7.BackColor = System.Drawing.Color.Cornsilk;
//            this.groupBox7.Controls.Add(this.chartDoanhThuNam);
//            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox7.Location = new System.Drawing.Point(3, 3);
//            this.groupBox7.Name = "groupBox7";
//            this.groupBox7.Size = new System.Drawing.Size(853, 242);
//            this.groupBox7.TabIndex = 2;
//            this.groupBox7.TabStop = false;
//            this.groupBox7.Text = "BIỂU ĐỒ DOANH THU NĂM";
//            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
//            // 
//            // chartDoanhThuNam
//            // 
//            chartArea8.Name = "ChartArea1";
//            this.chartDoanhThuNam.ChartAreas.Add(chartArea8);
//            this.chartDoanhThuNam.Dock = System.Windows.Forms.DockStyle.Fill;
//            legend8.Name = "Legend1";
//            this.chartDoanhThuNam.Legends.Add(legend8);
//            this.chartDoanhThuNam.Location = new System.Drawing.Point(3, 26);
//            this.chartDoanhThuNam.Name = "chartDoanhThuNam";
//            series8.ChartArea = "ChartArea1";
//            series8.Legend = "Legend1";
//            series8.Name = "Series1";
//            this.chartDoanhThuNam.Series.Add(series8);
//            this.chartDoanhThuNam.Size = new System.Drawing.Size(847, 213);
//            this.chartDoanhThuNam.TabIndex = 2;
//            this.chartDoanhThuNam.Text = "chart1";
//            // 
//            // groupBox8
//            // 
//            this.groupBox8.BackColor = System.Drawing.Color.Cornsilk;
//            this.groupBox8.Controls.Add(this.chartTyTrong);
//            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox8.Location = new System.Drawing.Point(862, 3);
//            this.groupBox8.Name = "groupBox8";
//            this.groupBox8.Size = new System.Drawing.Size(363, 242);
//            this.groupBox8.TabIndex = 3;
//            this.groupBox8.TabStop = false;
//            this.groupBox8.Text = "DOANH THU THEO DỊCH VỤ";
//            this.groupBox8.Enter += new System.EventHandler(this.groupBox8_Enter);
//            // 
//            // lblTongDoanhThu
//            // 
//            this.lblTongDoanhThu.AutoSize = true;
//            this.lblTongDoanhThu.Location = new System.Drawing.Point(3, 1);
//            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
//            this.lblTongDoanhThu.Size = new System.Drawing.Size(35, 23);
//            this.lblTongDoanhThu.TabIndex = 3;
//            this.lblTongDoanhThu.Text = "0 đ";
//            this.lblTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // pnlTongDoanhThu
//            // 
//            this.pnlTongDoanhThu.BackColor = System.Drawing.SystemColors.Control;
//            this.pnlTongDoanhThu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.pnlTongDoanhThu.Controls.Add(this.lblTongDoanhThu);
//            this.pnlTongDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlTongDoanhThu.Location = new System.Drawing.Point(3, 26);
//            this.pnlTongDoanhThu.Name = "pnlTongDoanhThu";
//            this.pnlTongDoanhThu.Size = new System.Drawing.Size(295, 22);
//            this.pnlTongDoanhThu.TabIndex = 0;
//            // 
//            // lblSoTienThangNay
//            // 
//            this.lblSoTienThangNay.AutoSize = true;
//            this.lblSoTienThangNay.Location = new System.Drawing.Point(3, 1);
//            this.lblSoTienThangNay.Name = "lblSoTienThangNay";
//            this.lblSoTienThangNay.Size = new System.Drawing.Size(35, 23);
//            this.lblSoTienThangNay.TabIndex = 3;
//            this.lblSoTienThangNay.Text = "0 đ";
//            this.lblSoTienThangNay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // pnlThangNay
//            // 
//            this.pnlThangNay.BackColor = System.Drawing.SystemColors.Control;
//            this.pnlThangNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.pnlThangNay.Controls.Add(this.lblSoTienThangNay);
//            this.pnlThangNay.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlThangNay.Location = new System.Drawing.Point(3, 26);
//            this.pnlThangNay.Name = "pnlThangNay";
//            this.pnlThangNay.Size = new System.Drawing.Size(293, 22);
//            this.pnlThangNay.TabIndex = 0;
//            // 
//            // groupBox2
//            // 
//            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox2.Controls.Add(this.pnlHomNay);
//            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox2.Location = new System.Drawing.Point(3, 3);
//            this.groupBox2.Name = "groupBox2";
//            this.groupBox2.Size = new System.Drawing.Size(299, 51);
//            this.groupBox2.TabIndex = 0;
//            this.groupBox2.TabStop = false;
//            this.groupBox2.Text = "DoanhThu Hôm Nay";
//            // 
//            // pnlHomNay
//            // 
//            this.pnlHomNay.BackColor = System.Drawing.SystemColors.Control;
//            this.pnlHomNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.pnlHomNay.Controls.Add(this.lblSoTienHomNay);
//            this.pnlHomNay.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlHomNay.Location = new System.Drawing.Point(3, 26);
//            this.pnlHomNay.Name = "pnlHomNay";
//            this.pnlHomNay.Size = new System.Drawing.Size(293, 22);
//            this.pnlHomNay.TabIndex = 0;
//            // 
//            // lblSoTienHomNay
//            // 
//            this.lblSoTienHomNay.AutoSize = true;
//            this.lblSoTienHomNay.Location = new System.Drawing.Point(3, 0);
//            this.lblSoTienHomNay.Name = "lblSoTienHomNay";
//            this.lblSoTienHomNay.Size = new System.Drawing.Size(35, 23);
//            this.lblSoTienHomNay.TabIndex = 1;
//            this.lblSoTienHomNay.Text = "0 đ";
//            this.lblSoTienHomNay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // lblSoTienTuanNay
//            // 
//            this.lblSoTienTuanNay.AutoSize = true;
//            this.lblSoTienTuanNay.Location = new System.Drawing.Point(3, 1);
//            this.lblSoTienTuanNay.Name = "lblSoTienTuanNay";
//            this.lblSoTienTuanNay.Size = new System.Drawing.Size(35, 23);
//            this.lblSoTienTuanNay.TabIndex = 2;
//            this.lblSoTienTuanNay.Text = "0 đ";
//            this.lblSoTienTuanNay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // pnlTuanNay
//            // 
//            this.pnlTuanNay.BackColor = System.Drawing.SystemColors.Control;
//            this.pnlTuanNay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.pnlTuanNay.Controls.Add(this.lblSoTienTuanNay);
//            this.pnlTuanNay.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlTuanNay.Location = new System.Drawing.Point(3, 26);
//            this.pnlTuanNay.Name = "pnlTuanNay";
//            this.pnlTuanNay.Size = new System.Drawing.Size(293, 22);
//            this.pnlTuanNay.TabIndex = 2;
//            // 
//            // groupBox3
//            // 
//            this.groupBox3.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox3.Controls.Add(this.pnlTuanNay);
//            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
//            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox3.Location = new System.Drawing.Point(308, 3);
//            this.groupBox3.Name = "groupBox3";
//            this.groupBox3.Size = new System.Drawing.Size(299, 51);
//            this.groupBox3.TabIndex = 1;
//            this.groupBox3.TabStop = false;
//            this.groupBox3.Text = "Doanh Thu Tuần Này";
//            // 
//            // tableLayoutPanel1
//            // 
//            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
//            this.tableLayoutPanel1.ColumnCount = 4;
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
//            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
//            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
//            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 2, 0);
//            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 3, 0);
//            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 34);
//            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
//            this.tableLayoutPanel1.RowCount = 1;
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.tableLayoutPanel1.Size = new System.Drawing.Size(1222, 57);
//            this.tableLayoutPanel1.TabIndex = 5;
//            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
//            // 
//            // groupBox4
//            // 
//            this.groupBox4.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox4.Controls.Add(this.pnlThangNay);
//            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox4.Location = new System.Drawing.Point(613, 3);
//            this.groupBox4.Name = "groupBox4";
//            this.groupBox4.Size = new System.Drawing.Size(299, 51);
//            this.groupBox4.TabIndex = 2;
//            this.groupBox4.TabStop = false;
//            this.groupBox4.Text = "Doanh Thu Tháng Này";
//            // 
//            // groupBox5
//            // 
//            this.groupBox5.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox5.Controls.Add(this.pnlTongDoanhThu);
//            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox5.Location = new System.Drawing.Point(918, 3);
//            this.groupBox5.Name = "groupBox5";
//            this.groupBox5.Size = new System.Drawing.Size(301, 51);
//            this.groupBox5.TabIndex = 3;
//            this.groupBox5.TabStop = false;
//            this.groupBox5.Text = "Tổng Doanh Thu";
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(4, 38);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(100, 23);
//            this.label2.TabIndex = 2;
//            this.label2.Text = "Đến Ngày:";
//            // 
//            // dtpTuNgay
//            // 
//            this.dtpTuNgay.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
//            this.dtpTuNgay.Location = new System.Drawing.Point(110, 2);
//            this.dtpTuNgay.Name = "dtpTuNgay";
//            this.dtpTuNgay.Size = new System.Drawing.Size(282, 30);
//            this.dtpTuNgay.TabIndex = 1;
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(13, 7);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(91, 23);
//            this.label1.TabIndex = 0;
//            this.label1.Text = "Từ Ngày:";
//            // 
//            // btnXuatBaoCao
//            // 
//            this.btnXuatBaoCao.BackColor = System.Drawing.SystemColors.ControlLight;
//            this.btnXuatBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnXuatBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnXuatBaoCao.Location = new System.Drawing.Point(3, 4);
//            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
//            this.btnXuatBaoCao.Size = new System.Drawing.Size(298, 51);
//            this.btnXuatBaoCao.TabIndex = 7;
//            this.btnXuatBaoCao.Text = "Xuất Excel";
//            this.btnXuatBaoCao.UseVisualStyleBackColor = false;
//            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
//            // 
//            // btnThongKe
//            // 
//            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnThongKe.Location = new System.Drawing.Point(178, 39);
//            this.btnThongKe.Name = "btnThongKe";
//            this.btnThongKe.Size = new System.Drawing.Size(309, 30);
//            this.btnThongKe.TabIndex = 6;
//            this.btnThongKe.Text = "Lọc Dữ Liệu";
//            this.btnThongKe.UseVisualStyleBackColor = true;
//            // 
//            // cboChiNhanh
//            // 
//            this.cboChiNhanh.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.cboChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cboChiNhanh.FormattingEnabled = true;
//            this.cboChiNhanh.Location = new System.Drawing.Point(178, 4);
//            this.cboChiNhanh.Name = "cboChiNhanh";
//            this.cboChiNhanh.Size = new System.Drawing.Size(309, 31);
//            this.cboChiNhanh.TabIndex = 5;
//            // 
//            // dtpDenNgay
//            // 
//            this.dtpDenNgay.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
//            this.dtpDenNgay.Location = new System.Drawing.Point(110, 37);
//            this.dtpDenNgay.Name = "dtpDenNgay";
//            this.dtpDenNgay.Size = new System.Drawing.Size(282, 30);
//            this.dtpDenNgay.TabIndex = 3;
//            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.dtpDenNgay_ValueChanged);
//            // 
//            // groupBox1
//            // 
//            this.groupBox1.BackColor = System.Drawing.Color.Cornsilk;
//            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
//            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox1.Location = new System.Drawing.Point(3, 357);
//            this.groupBox1.Name = "groupBox1";
//            this.groupBox1.Size = new System.Drawing.Size(1228, 135);
//            this.groupBox1.TabIndex = 4;
//            this.groupBox1.TabStop = false;
//            this.groupBox1.Text = "BỘ LỌC DỮ LIỆU";
//            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
//            // 
//            // tableLayoutPanel3
//            // 
//            this.tableLayoutPanel3.ColumnCount = 3;
//            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
//            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.16203F));
//            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.53192F));
//            this.tableLayoutPanel3.Controls.Add(this.panel3, 2, 0);
//            this.tableLayoutPanel3.Controls.Add(this.groupBox9, 1, 0);
//            this.tableLayoutPanel3.Controls.Add(this.groupBox10, 0, 0);
//            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 26);
//            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
//            this.tableLayoutPanel3.RowCount = 1;
//            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
//            this.tableLayoutPanel3.Size = new System.Drawing.Size(1222, 106);
//            this.tableLayoutPanel3.TabIndex = 0;
//            // 
//            // panel3
//            // 
//            this.panel3.BackColor = System.Drawing.Color.Cornsilk;
//            this.panel3.Controls.Add(this.groupBox12);
//            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panel3.Location = new System.Drawing.Point(912, 3);
//            this.panel3.Name = "panel3";
//            this.panel3.Size = new System.Drawing.Size(307, 100);
//            this.panel3.TabIndex = 2;
//            // 
//            // groupBox9
//            // 
//            this.groupBox9.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox9.Controls.Add(this.panel2);
//            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox9.Location = new System.Drawing.Point(410, 3);
//            this.groupBox9.Name = "groupBox9";
//            this.groupBox9.Size = new System.Drawing.Size(496, 100);
//            this.groupBox9.TabIndex = 3;
//            this.groupBox9.TabStop = false;
//            this.groupBox9.Text = "Chi Nhánh";
//            // 
//            // panel2
//            // 
//            this.panel2.BackColor = System.Drawing.SystemColors.Control;
//            this.panel2.Controls.Add(this.label4);
//            this.panel2.Controls.Add(this.label3);
//            this.panel2.Controls.Add(this.btnThongKe);
//            this.panel2.Controls.Add(this.cboChiNhanh);
//            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panel2.Location = new System.Drawing.Point(3, 26);
//            this.panel2.Name = "panel2";
//            this.panel2.Size = new System.Drawing.Size(490, 71);
//            this.panel2.TabIndex = 0;
//            // 
//            // label4
//            // 
//            this.label4.AutoSize = true;
//            this.label4.Location = new System.Drawing.Point(111, 44);
//            this.label4.Name = "label4";
//            this.label4.Size = new System.Drawing.Size(61, 23);
//            this.label4.TabIndex = 8;
//            this.label4.Text = "Click:";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(3, 7);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(154, 23);
//            this.label3.TabIndex = 7;
//            this.label3.Text = "Chọn Chi Nhánh:";
//            // 
//            // groupBox10
//            // 
//            this.groupBox10.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox10.Controls.Add(this.panel1);
//            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox10.Location = new System.Drawing.Point(3, 3);
//            this.groupBox10.Name = "groupBox10";
//            this.groupBox10.Size = new System.Drawing.Size(401, 100);
//            this.groupBox10.TabIndex = 4;
//            this.groupBox10.TabStop = false;
//            this.groupBox10.Text = "Ngày";
//            // 
//            // panel1
//            // 
//            this.panel1.BackColor = System.Drawing.SystemColors.Control;
//            this.panel1.Controls.Add(this.dtpTuNgay);
//            this.panel1.Controls.Add(this.label2);
//            this.panel1.Controls.Add(this.dtpDenNgay);
//            this.panel1.Controls.Add(this.label1);
//            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panel1.Location = new System.Drawing.Point(3, 26);
//            this.panel1.Name = "panel1";
//            this.panel1.Size = new System.Drawing.Size(395, 71);
//            this.panel1.TabIndex = 0;
//            // 
//            // dgvDoanhThu
//            // 
//            this.dgvDoanhThu.BackgroundColor = System.Drawing.Color.White;
//            this.dgvDoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.dgvDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dgvDoanhThu.Location = new System.Drawing.Point(3, 26);
//            this.dgvDoanhThu.Name = "dgvDoanhThu";
//            this.dgvDoanhThu.ReadOnly = true;
//            this.dgvDoanhThu.RowHeadersVisible = false;
//            this.dgvDoanhThu.RowHeadersWidth = 51;
//            this.dgvDoanhThu.RowTemplate.Height = 24;
//            this.dgvDoanhThu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.dgvDoanhThu.Size = new System.Drawing.Size(1222, 149);
//            this.dgvDoanhThu.TabIndex = 7;
//            this.dgvDoanhThu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoanhThu_CellContentClick);
//            // 
//            // groupBox6
//            // 
//            this.groupBox6.BackColor = System.Drawing.Color.Cornsilk;
//            this.groupBox6.Controls.Add(this.tableLayoutPanel1);
//            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox6.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox6.Location = new System.Drawing.Point(3, 3);
//            this.groupBox6.Name = "groupBox6";
//            this.groupBox6.Size = new System.Drawing.Size(1228, 94);
//            this.groupBox6.TabIndex = 8;
//            this.groupBox6.TabStop = false;
//            this.groupBox6.Text = "DOANH THU";
//            // 
//            // tableLayoutPanel2
//            // 
//            this.tableLayoutPanel2.ColumnCount = 1;
//            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 0, 0);
//            this.tableLayoutPanel2.Controls.Add(this.DoanhThu, 0, 1);
//            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 2);
//            this.tableLayoutPanel2.Controls.Add(this.groupBox11, 0, 3);
//            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
//            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
//            this.tableLayoutPanel2.RowCount = 4;
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.24859F));
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.75141F));
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
//            this.tableLayoutPanel2.Size = new System.Drawing.Size(1234, 679);
//            this.tableLayoutPanel2.TabIndex = 9;
//            // 
//            // groupBox11
//            // 
//            this.groupBox11.Controls.Add(this.dgvDoanhThu);
//            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.groupBox11.Location = new System.Drawing.Point(3, 498);
//            this.groupBox11.Name = "groupBox11";
//            this.groupBox11.Size = new System.Drawing.Size(1228, 178);
//            this.groupBox11.TabIndex = 9;
//            this.groupBox11.TabStop = false;
//            this.groupBox11.Text = "DOANH THU THEO CHI NHÁNH";
//            // 
//            // groupBox12
//            // 
//            this.groupBox12.BackColor = System.Drawing.SystemColors.Window;
//            this.groupBox12.Controls.Add(this.panel4);
//            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.groupBox12.Location = new System.Drawing.Point(0, 0);
//            this.groupBox12.Name = "groupBox12";
//            this.groupBox12.Size = new System.Drawing.Size(307, 100);
//            this.groupBox12.TabIndex = 0;
//            this.groupBox12.TabStop = false;
//            this.groupBox12.Text = "Xuất Doanh Thu";
//            // 
//            // panel4
//            // 
//            this.panel4.BackColor = System.Drawing.SystemColors.Control;
//            this.panel4.Controls.Add(this.btnXuatBaoCao);
//            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.panel4.Location = new System.Drawing.Point(3, 26);
//            this.panel4.Name = "panel4";
//            this.panel4.Size = new System.Drawing.Size(301, 71);
//            this.panel4.TabIndex = 0;
//            // 
//            // frmdoanhthuu
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(1234, 679);
//            this.Controls.Add(this.tableLayoutPanel2);
//            this.Name = "frmdoanhthuu";
//            this.Text = "frmdoanhthuu";
//            ((System.ComponentModel.ISupportInitialize)(this.chartTyTrong)).EndInit();
//            this.DoanhThu.ResumeLayout(false);
//            this.groupBox7.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThuNam)).EndInit();
//            this.groupBox8.ResumeLayout(false);
//            this.pnlTongDoanhThu.ResumeLayout(false);
//            this.pnlTongDoanhThu.PerformLayout();
//            this.pnlThangNay.ResumeLayout(false);
//            this.pnlThangNay.PerformLayout();
//            this.groupBox2.ResumeLayout(false);
//            this.pnlHomNay.ResumeLayout(false);
//            this.pnlHomNay.PerformLayout();
//            this.pnlTuanNay.ResumeLayout(false);
//            this.pnlTuanNay.PerformLayout();
//            this.groupBox3.ResumeLayout(false);
//            this.tableLayoutPanel1.ResumeLayout(false);
//            this.groupBox4.ResumeLayout(false);
//            this.groupBox5.ResumeLayout(false);
//            this.groupBox1.ResumeLayout(false);
//            this.tableLayoutPanel3.ResumeLayout(false);
//            this.panel3.ResumeLayout(false);
//            this.groupBox9.ResumeLayout(false);
//            this.panel2.ResumeLayout(false);
//            this.panel2.PerformLayout();
//            this.groupBox10.ResumeLayout(false);
//            this.panel1.ResumeLayout(false);
//            this.panel1.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
//            this.groupBox6.ResumeLayout(false);
//            this.tableLayoutPanel2.ResumeLayout(false);
//            this.groupBox11.ResumeLayout(false);
//            this.groupBox12.ResumeLayout(false);
//            this.panel4.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        #endregion
//        private System.Windows.Forms.DataVisualization.Charting.Chart chartTyTrong;
//        private System.Windows.Forms.TableLayoutPanel DoanhThu;
//        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThuNam;
//        private System.Windows.Forms.Label lblTongDoanhThu;
//        private System.Windows.Forms.Panel pnlTongDoanhThu;
//        private System.Windows.Forms.Label lblSoTienThangNay;
//        private System.Windows.Forms.Panel pnlThangNay;
//        private System.Windows.Forms.GroupBox groupBox2;
//        private System.Windows.Forms.Panel pnlHomNay;
//        private System.Windows.Forms.Label lblSoTienHomNay;
//        private System.Windows.Forms.Label lblSoTienTuanNay;
//        private System.Windows.Forms.Panel pnlTuanNay;
//        private System.Windows.Forms.GroupBox groupBox3;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
//        private System.Windows.Forms.GroupBox groupBox4;
//        private System.Windows.Forms.GroupBox groupBox5;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.DateTimePicker dtpTuNgay;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.Button btnXuatBaoCao;
//        private System.Windows.Forms.Button btnThongKe;
//        private System.Windows.Forms.ComboBox cboChiNhanh;
//        private System.Windows.Forms.DateTimePicker dtpDenNgay;
//        private System.Windows.Forms.GroupBox groupBox1;
//        private System.Windows.Forms.DataGridView dgvDoanhThu;
//        private System.Windows.Forms.GroupBox groupBox6;
//        private System.Windows.Forms.GroupBox groupBox7;
//        private System.Windows.Forms.GroupBox groupBox8;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
//        private System.Windows.Forms.Panel panel3;
//        private System.Windows.Forms.GroupBox groupBox9;
//        private System.Windows.Forms.GroupBox groupBox10;
//        private System.Windows.Forms.Panel panel1;
//        private System.Windows.Forms.Panel panel2;
//        private System.Windows.Forms.Label label4;
//        private System.Windows.Forms.Label label3;
//        private System.Windows.Forms.GroupBox groupBox11;
//        private System.Windows.Forms.GroupBox groupBox12;
//        private System.Windows.Forms.Panel panel4;
//    }
//}