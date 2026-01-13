namespace FootballPitchManagement.Forms.Customer
{
    partial class frmHoadon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.colCT_TenSan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCT_GioKetThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpDoAn = new System.Windows.Forms.GroupBox();
            this.dgvDoAn = new System.Windows.Forms.DataGridView();
            this.colDA_TenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDA_SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDA_DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDA_ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.colMaHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenChiNhanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.dtNgayLap = new System.Windows.Forms.DateTimePicker();
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.cboChiNhanh = new System.Windows.Forms.ComboBox();
            this.cboKH = new System.Windows.Forms.ComboBox();
            this.txtNhanVien = new System.Windows.Forms.TextBox();
            this.colCT_GioBatDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblKH = new System.Windows.Forms.Label();
            this.llblTrangThai = new System.Windows.Forms.Label();
            this.lblNV = new System.Windows.Forms.Label();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.grpThanhToan = new System.Windows.Forms.GroupBox();
            this.lblPhuongThucThanhToan = new System.Windows.Forms.Label();
            this.cboPhuongThuc = new System.Windows.Forms.ComboBox();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.txtTienDoAn = new System.Windows.Forms.TextBox();
            this.txtVAT = new System.Windows.Forms.TextBox();
            this.txtGiamGia = new System.Windows.Forms.TextBox();
            this.txtTienSan = new System.Windows.Forms.TextBox();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCN = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpSan = new System.Windows.Forms.GroupBox();
            this.dgvChiTietSan = new System.Windows.Forms.DataGridView();
            this.colCT_SoGio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCT_DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCT_ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.grpDoAn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.grpDanhSach.SuspendLayout();
            this.grpThanhToan.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpSan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietSan)).BeginInit();
            this.grpThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // colCT_TenSan
            // 
            this.colCT_TenSan.DataPropertyName = "TenSan";
            this.colCT_TenSan.HeaderText = "Tên Sân";
            this.colCT_TenSan.Name = "colCT_TenSan";
            this.colCT_TenSan.ReadOnly = true;
            // 
            // colCT_GioKetThuc
            // 
            this.colCT_GioKetThuc.DataPropertyName = "GioKetThuc";
            this.colCT_GioKetThuc.HeaderText = "Giờ Kết Thúc";
            this.colCT_GioKetThuc.Name = "colCT_GioKetThuc";
            this.colCT_GioKetThuc.ReadOnly = true;
            // 
            // grpDoAn
            // 
            this.grpDoAn.Controls.Add(this.dgvDoAn);
            this.grpDoAn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDoAn.Location = new System.Drawing.Point(409, 3);
            this.grpDoAn.Name = "grpDoAn";
            this.grpDoAn.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.grpDoAn.Size = new System.Drawing.Size(401, 128);
            this.grpDoAn.TabIndex = 4;
            this.grpDoAn.TabStop = false;
            this.grpDoAn.Text = "ĐỒ ĂN / NƯỚC UỐNG";
            this.grpDoAn.Enter += new System.EventHandler(this.grpDoAn_Enter);
            // 
            // dgvDoAn
            // 
            this.dgvDoAn.AllowUserToAddRows = false;
            this.dgvDoAn.AllowUserToDeleteRows = false;
            this.dgvDoAn.AllowUserToResizeColumns = false;
            this.dgvDoAn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDoAn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDoAn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDoAn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoAn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDA_TenHang,
            this.colDA_SoLuong,
            this.colDA_DonGia,
            this.colDA_ThanhTien});
            this.dgvDoAn.Location = new System.Drawing.Point(32, 38);
            this.dgvDoAn.MultiSelect = false;
            this.dgvDoAn.Name = "dgvDoAn";
            this.dgvDoAn.ReadOnly = true;
            this.dgvDoAn.RowHeadersVisible = false;
            this.dgvDoAn.RowTemplate.Height = 28;
            this.dgvDoAn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoAn.Size = new System.Drawing.Size(343, 77);
            this.dgvDoAn.TabIndex = 0;
            this.dgvDoAn.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoAn_CellContentClick);
            // 
            // colDA_TenHang
            // 
            this.colDA_TenHang.HeaderText = "Tên hàng";
            this.colDA_TenHang.Name = "colDA_TenHang";
            this.colDA_TenHang.ReadOnly = true;
            // 
            // colDA_SoLuong
            // 
            this.colDA_SoLuong.DataPropertyName = "SoLuong";
            this.colDA_SoLuong.HeaderText = "Số lượng";
            this.colDA_SoLuong.Name = "colDA_SoLuong";
            this.colDA_SoLuong.ReadOnly = true;
            // 
            // colDA_DonGia
            // 
            this.colDA_DonGia.DataPropertyName = "DonGia";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            this.colDA_DonGia.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDA_DonGia.HeaderText = "Đơn giá";
            this.colDA_DonGia.Name = "colDA_DonGia";
            this.colDA_DonGia.ReadOnly = true;
            // 
            // colDA_ThanhTien
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colDA_ThanhTien.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDA_ThanhTien.HeaderText = "ThanhTien";
            this.colDA_ThanhTien.Name = "colDA_ThanhTien";
            this.colDA_ThanhTien.ReadOnly = true;
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AllowUserToResizeColumns = false;
            this.dgvHoaDon.AllowUserToResizeRows = false;
            this.dgvHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaHoaDon,
            this.colNgayLap,
            this.colHoTen,
            this.colTenChiNhanh,
            this.colThanhTien,
            this.colTrangThai});
            this.dgvHoaDon.Location = new System.Drawing.Point(10, 38);
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.ReadOnly = true;
            this.dgvHoaDon.RowHeadersVisible = false;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(748, 95);
            this.dgvHoaDon.TabIndex = 1;
            this.dgvHoaDon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHoaDon_CellContentClick);
            // 
            // colMaHoaDon
            // 
            this.colMaHoaDon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMaHoaDon.FillWeight = 80F;
            this.colMaHoaDon.HeaderText = "Mã Hóa Đơn";
            this.colMaHoaDon.Name = "colMaHoaDon";
            this.colMaHoaDon.ReadOnly = true;
            // 
            // colNgayLap
            // 
            this.colNgayLap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNgayLap.FillWeight = 90F;
            this.colNgayLap.HeaderText = "Ngày lập";
            this.colNgayLap.Name = "colNgayLap";
            this.colNgayLap.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colHoTen.FillWeight = 150F;
            this.colHoTen.HeaderText = "Khách hàng";
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colTenChiNhanh
            // 
            this.colTenChiNhanh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenChiNhanh.FillWeight = 120F;
            this.colTenChiNhanh.HeaderText = "Chi nhánh";
            this.colTenChiNhanh.Name = "colTenChiNhanh";
            this.colTenChiNhanh.ReadOnly = true;
            // 
            // colThanhTien
            // 
            this.colThanhTien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colThanhTien.FillWeight = 120F;
            this.colThanhTien.HeaderText = "Thành Tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            // 
            // colTrangThai
            // 
            this.colTrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTrangThai.HeaderText = "Trạng thái";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.ReadOnly = true;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(473, 119);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(210, 25);
            this.cboTrangThai.TabIndex = 10;
            this.cboTrangThai.SelectedIndexChanged += new System.EventHandler(this.cboTrangThai_SelectedIndexChanged);
            // 
            // dtNgayLap
            // 
            this.dtNgayLap.Enabled = false;
            this.dtNgayLap.Location = new System.Drawing.Point(94, 71);
            this.dtNgayLap.Name = "dtNgayLap";
            this.dtNgayLap.Size = new System.Drawing.Size(210, 25);
            this.dtNgayLap.TabIndex = 9;
            this.dtNgayLap.ValueChanged += new System.EventHandler(this.dtNgayLap_ValueChanged);
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnInHoaDon.Location = new System.Drawing.Point(556, 659);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(120, 23);
            this.btnInHoaDon.TabIndex = 29;
            this.btnInHoaDon.Text = "In hóa đơn";
            this.btnInHoaDon.UseVisualStyleBackColor = false;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.LightCoral;
            this.btnDong.Location = new System.Drawing.Point(695, 659);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 23);
            this.btnDong.TabIndex = 28;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.LightGreen;
            this.btnThanhToan.Location = new System.Drawing.Point(414, 659);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(120, 23);
            this.btnThanhToan.TabIndex = 27;
            this.btnThanhToan.Text = "Thanh Toán";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // cboChiNhanh
            // 
            this.cboChiNhanh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChiNhanh.FormattingEnabled = true;
            this.cboChiNhanh.Location = new System.Drawing.Point(473, 30);
            this.cboChiNhanh.Name = "cboChiNhanh";
            this.cboChiNhanh.Size = new System.Drawing.Size(210, 25);
            this.cboChiNhanh.TabIndex = 9;
            this.cboChiNhanh.SelectedIndexChanged += new System.EventHandler(this.cboChiNhanh_SelectedIndexChanged);
            // 
            // cboKH
            // 
            this.cboKH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKH.FormattingEnabled = true;
            this.cboKH.Location = new System.Drawing.Point(94, 119);
            this.cboKH.Name = "cboKH";
            this.cboKH.Size = new System.Drawing.Size(210, 25);
            this.cboKH.TabIndex = 3;
            this.cboKH.SelectedIndexChanged += new System.EventHandler(this.cboKH_SelectedIndexChanged);
            // 
            // txtNhanVien
            // 
            this.txtNhanVien.Location = new System.Drawing.Point(473, 74);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.ReadOnly = true;
            this.txtNhanVien.Size = new System.Drawing.Size(210, 25);
            this.txtNhanVien.TabIndex = 8;
            this.txtNhanVien.TextChanged += new System.EventHandler(this.txtNhanVien_TextChanged);
            // 
            // colCT_GioBatDau
            // 
            this.colCT_GioBatDau.DataPropertyName = "GioBatDau";
            this.colCT_GioBatDau.HeaderText = "Giờ Bắt Đầu";
            this.colCT_GioBatDau.Name = "colCT_GioBatDau";
            this.colCT_GioBatDau.ReadOnly = true;
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(94, 24);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.ReadOnly = true;
            this.txtMaHD.Size = new System.Drawing.Size(210, 25);
            this.txtMaHD.TabIndex = 6;
            this.txtMaHD.TextChanged += new System.EventHandler(this.txtMaHD_TextChanged);
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgay.Location = new System.Drawing.Point(6, 71);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(70, 19);
            this.lblNgay.TabIndex = 4;
            this.lblNgay.Text = "Ngày lập";
            // 
            // lblKH
            // 
            this.lblKH.AutoSize = true;
            this.lblKH.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKH.Location = new System.Drawing.Point(2, 119);
            this.lblKH.Name = "lblKH";
            this.lblKH.Size = new System.Drawing.Size(86, 19);
            this.lblKH.TabIndex = 3;
            this.lblKH.Text = "Khách hàng";
            // 
            // llblTrangThai
            // 
            this.llblTrangThai.AutoSize = true;
            this.llblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llblTrangThai.Location = new System.Drawing.Point(384, 125);
            this.llblTrangThai.Name = "llblTrangThai";
            this.llblTrangThai.Size = new System.Drawing.Size(76, 19);
            this.llblTrangThai.TabIndex = 2;
            this.llblTrangThai.Text = "Trạng thái";
            // 
            // lblNV
            // 
            this.lblNV.AutoSize = true;
            this.lblNV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNV.Location = new System.Drawing.Point(384, 77);
            this.lblNV.Name = "lblNV";
            this.lblNV.Size = new System.Drawing.Size(76, 19);
            this.lblNV.TabIndex = 1;
            this.lblNV.Text = "Nhân viên";
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaHD.Location = new System.Drawing.Point(6, 30);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(55, 19);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Mã HĐ";
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.AutoSize = true;
            this.grpDanhSach.Controls.Add(this.dgvHoaDon);
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDanhSach.Location = new System.Drawing.Point(5, 12);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.grpDanhSach.Size = new System.Drawing.Size(813, 164);
            this.grpDanhSach.TabIndex = 30;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "DANH SÁCH HÓA ĐƠN";
            this.grpDanhSach.Enter += new System.EventHandler(this.grpDanhSach_Enter);
            // 
            // grpThanhToan
            // 
            this.grpThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpThanhToan.Controls.Add(this.lblPhuongThucThanhToan);
            this.grpThanhToan.Controls.Add(this.cboPhuongThuc);
            this.grpThanhToan.Controls.Add(this.txtThanhTien);
            this.grpThanhToan.Controls.Add(this.txtTienDoAn);
            this.grpThanhToan.Controls.Add(this.txtVAT);
            this.grpThanhToan.Controls.Add(this.txtGiamGia);
            this.grpThanhToan.Controls.Add(this.txtTienSan);
            this.grpThanhToan.Controls.Add(this.lblThanhTien);
            this.grpThanhToan.Controls.Add(this.label5);
            this.grpThanhToan.Controls.Add(this.label4);
            this.grpThanhToan.Controls.Add(this.label3);
            this.grpThanhToan.Controls.Add(this.label2);
            this.grpThanhToan.Location = new System.Drawing.Point(5, 524);
            this.grpThanhToan.Name = "grpThanhToan";
            this.grpThanhToan.Padding = new System.Windows.Forms.Padding(10, 25, 10, 10);
            this.grpThanhToan.Size = new System.Drawing.Size(813, 116);
            this.grpThanhToan.TabIndex = 25;
            this.grpThanhToan.TabStop = false;
            this.grpThanhToan.Text = "TỔNG TIỀN & THANH TOÁN";
            // 
            // lblPhuongThucThanhToan
            // 
            this.lblPhuongThucThanhToan.AutoSize = true;
            this.lblPhuongThucThanhToan.ForeColor = System.Drawing.Color.Black;
            this.lblPhuongThucThanhToan.Location = new System.Drawing.Point(437, 70);
            this.lblPhuongThucThanhToan.Name = "lblPhuongThucThanhToan";
            this.lblPhuongThucThanhToan.Size = new System.Drawing.Size(85, 13);
            this.lblPhuongThucThanhToan.TabIndex = 12;
            this.lblPhuongThucThanhToan.Text = "Phương thức TT";
            // 
            // cboPhuongThuc
            // 
            this.cboPhuongThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongThuc.FormattingEnabled = true;
            this.cboPhuongThuc.Location = new System.Drawing.Point(538, 67);
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Size = new System.Drawing.Size(121, 21);
            this.cboPhuongThuc.TabIndex = 11;
            this.cboPhuongThuc.SelectedIndexChanged += new System.EventHandler(this.cboPhuongThuc_SelectedIndexChanged);
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhTien.ForeColor = System.Drawing.Color.DarkRed;
            this.txtThanhTien.Location = new System.Drawing.Point(538, 20);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(121, 25);
            this.txtThanhTien.TabIndex = 9;
            this.txtThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThanhTien.TextChanged += new System.EventHandler(this.txtThanhTien_TextChanged);
            // 
            // txtTienDoAn
            // 
            this.txtTienDoAn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTienDoAn.Location = new System.Drawing.Point(87, 67);
            this.txtTienDoAn.Name = "txtTienDoAn";
            this.txtTienDoAn.ReadOnly = true;
            this.txtTienDoAn.Size = new System.Drawing.Size(100, 20);
            this.txtTienDoAn.TabIndex = 8;
            this.txtTienDoAn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienDoAn.TextChanged += new System.EventHandler(this.txtTienDoAn_TextChanged);
            // 
            // txtVAT
            // 
            this.txtVAT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtVAT.Location = new System.Drawing.Point(299, 23);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.ReadOnly = true;
            this.txtVAT.Size = new System.Drawing.Size(100, 20);
            this.txtVAT.TabIndex = 7;
            this.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVAT.TextChanged += new System.EventHandler(this.txtVAT_TextChanged);
            // 
            // txtGiamGia
            // 
            this.txtGiamGia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtGiamGia.Location = new System.Drawing.Point(299, 64);
            this.txtGiamGia.Name = "txtGiamGia";
            this.txtGiamGia.ReadOnly = true;
            this.txtGiamGia.Size = new System.Drawing.Size(100, 20);
            this.txtGiamGia.TabIndex = 6;
            this.txtGiamGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGiamGia.TextChanged += new System.EventHandler(this.txtGiamGia_TextChanged);
            // 
            // txtTienSan
            // 
            this.txtTienSan.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTienSan.Location = new System.Drawing.Point(87, 28);
            this.txtTienSan.Name = "txtTienSan";
            this.txtTienSan.ReadOnly = true;
            this.txtTienSan.Size = new System.Drawing.Size(100, 20);
            this.txtTienSan.TabIndex = 5;
            this.txtTienSan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTienSan.TextChanged += new System.EventHandler(this.txtTienSan_TextChanged);
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThanhTien.ForeColor = System.Drawing.Color.Black;
            this.lblThanhTien.Location = new System.Drawing.Point(437, 23);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(88, 17);
            this.lblThanhTien.TabIndex = 5;
            this.lblThanhTien.Text = "THÀNH TIỀN";
            this.lblThanhTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "VAT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(224, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Giảm giá";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tiền đồ ăn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tiền sân";
            // 
            // lblCN
            // 
            this.lblCN.AutoSize = true;
            this.lblCN.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCN.Location = new System.Drawing.Point(384, 30);
            this.lblCN.Name = "lblCN";
            this.lblCN.Size = new System.Drawing.Size(74, 19);
            this.lblCN.TabIndex = 5;
            this.lblCN.Text = "Chi nhánh";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grpSan, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpDoAn, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 355);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(813, 146);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // grpSan
            // 
            this.grpSan.Controls.Add(this.dgvChiTietSan);
            this.grpSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSan.Location = new System.Drawing.Point(3, 3);
            this.grpSan.Name = "grpSan";
            this.grpSan.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.grpSan.Size = new System.Drawing.Size(400, 128);
            this.grpSan.TabIndex = 3;
            this.grpSan.TabStop = false;
            this.grpSan.Text = "CHI TIẾT SÂN";
            // 
            // dgvChiTietSan
            // 
            this.dgvChiTietSan.AllowUserToResizeColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvChiTietSan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvChiTietSan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTietSan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvChiTietSan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietSan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCT_TenSan,
            this.colCT_GioBatDau,
            this.colCT_GioKetThuc,
            this.colCT_SoGio,
            this.colCT_DonGia,
            this.colCT_ThanhTien});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DarkRed;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTietSan.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvChiTietSan.Location = new System.Drawing.Point(-1, 38);
            this.dgvChiTietSan.Name = "dgvChiTietSan";
            this.dgvChiTietSan.RowHeadersVisible = false;
            this.dgvChiTietSan.RowTemplate.Height = 28;
            this.dgvChiTietSan.Size = new System.Drawing.Size(376, 72);
            this.dgvChiTietSan.TabIndex = 0;
            this.dgvChiTietSan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTietSan_CellContentClick);
            // 
            // colCT_SoGio
            // 
            this.colCT_SoGio.DataPropertyName = "SoGio";
            this.colCT_SoGio.HeaderText = "Số Giờ";
            this.colCT_SoGio.Name = "colCT_SoGio";
            this.colCT_SoGio.ReadOnly = true;
            // 
            // colCT_DonGia
            // 
            this.colCT_DonGia.DataPropertyName = "DonGia";
            dataGridViewCellStyle6.Format = "N0";
            this.colCT_DonGia.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCT_DonGia.HeaderText = "Đơn Giá";
            this.colCT_DonGia.Name = "colCT_DonGia";
            this.colCT_DonGia.ReadOnly = true;
            // 
            // colCT_ThanhTien
            // 
            this.colCT_ThanhTien.DataPropertyName = "ThanhTien";
            dataGridViewCellStyle7.Format = "N0";
            this.colCT_ThanhTien.DefaultCellStyle = dataGridViewCellStyle7;
            this.colCT_ThanhTien.HeaderText = "Thành Tiền";
            this.colCT_ThanhTien.Name = "colCT_ThanhTien";
            this.colCT_ThanhTien.ReadOnly = true;
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.cboTrangThai);
            this.grpThongTin.Controls.Add(this.dtNgayLap);
            this.grpThongTin.Controls.Add(this.cboChiNhanh);
            this.grpThongTin.Controls.Add(this.cboKH);
            this.grpThongTin.Controls.Add(this.txtNhanVien);
            this.grpThongTin.Controls.Add(this.txtMaHD);
            this.grpThongTin.Controls.Add(this.lblCN);
            this.grpThongTin.Controls.Add(this.lblNgay);
            this.grpThongTin.Controls.Add(this.lblKH);
            this.grpThongTin.Controls.Add(this.llblTrangThai);
            this.grpThongTin.Controls.Add(this.lblNV);
            this.grpThongTin.Controls.Add(this.lblMaHD);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTin.Location = new System.Drawing.Point(5, 182);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.grpThongTin.Size = new System.Drawing.Size(813, 157);
            this.grpThongTin.TabIndex = 24;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "THÔNG TIN HÓA ĐƠN";
            // 
            // frmHoadon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 722);
            this.Controls.Add(this.btnInHoaDon);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.grpThanhToan);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.grpThongTin);
            this.Name = "frmHoadon";
            this.Text = "frmHoadon";
            this.grpDoAn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.grpDanhSach.ResumeLayout(false);
            this.grpThanhToan.ResumeLayout(false);
            this.grpThanhToan.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpSan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietSan)).EndInit();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn colCT_TenSan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCT_GioKetThuc;
        private System.Windows.Forms.GroupBox grpDoAn;
        private System.Windows.Forms.DataGridView dgvDoAn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDA_TenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDA_SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDA_DonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDA_ThanhTien;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenChiNhanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrangThai;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.DateTimePicker dtNgayLap;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.ComboBox cboChiNhanh;
        private System.Windows.Forms.ComboBox cboKH;
        private System.Windows.Forms.TextBox txtNhanVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCT_GioBatDau;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblKH;
        private System.Windows.Forms.Label llblTrangThai;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.GroupBox grpThanhToan;
        private System.Windows.Forms.Label lblPhuongThucThanhToan;
        private System.Windows.Forms.ComboBox cboPhuongThuc;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.TextBox txtTienDoAn;
        private System.Windows.Forms.TextBox txtVAT;
        private System.Windows.Forms.TextBox txtGiamGia;
        private System.Windows.Forms.TextBox txtTienSan;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCN;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpSan;
        private System.Windows.Forms.DataGridView dgvChiTietSan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCT_SoGio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCT_DonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCT_ThanhTien;
        private System.Windows.Forms.GroupBox grpThongTin;
    }
}