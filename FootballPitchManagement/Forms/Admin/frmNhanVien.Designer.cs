namespace FootballPitchManagement.Forms.Admin
{
    partial class frmNhanVien
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
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtHo = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.rdoNu = new System.Windows.Forms.RadioButton();
            this.labelMaNV = new System.Windows.Forms.Label();
            this.labelNgaySinh = new System.Windows.Forms.Label();
            this.labelDiaChi = new System.Windows.Forms.Label();
            this.labelHo = new System.Windows.Forms.Label();
            this.labelGioiTinh = new System.Windows.Forms.Label();
            this.rdoNam = new System.Windows.Forms.RadioButton();
            this.labelTen = new System.Windows.Forms.Label();
            this.lblDanhSachNhanVien = new System.Windows.Forms.Label();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.Button();
            this.grpThongTinNhanVien = new System.Windows.Forms.GroupBox();
            this.btnSua = new System.Windows.Forms.Button();
            this.butLuu = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.grpThongTinNhanVien.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(127, 213);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(100, 22);
            this.txtDiaChi.TabIndex = 13;
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(127, 102);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(100, 22);
            this.txtTen.TabIndex = 12;
            // 
            // txtHo
            // 
            this.txtHo.Location = new System.Drawing.Point(127, 71);
            this.txtHo.Name = "txtHo";
            this.txtHo.Size = new System.Drawing.Size(100, 22);
            this.txtHo.TabIndex = 11;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(127, 34);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(100, 22);
            this.txtMaNV.TabIndex = 10;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(127, 131);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(200, 22);
            this.dtpNgaySinh.TabIndex = 9;
            // 
            // rdoNu
            // 
            this.rdoNu.AutoSize = true;
            this.rdoNu.Location = new System.Drawing.Point(203, 170);
            this.rdoNu.Name = "rdoNu";
            this.rdoNu.Size = new System.Drawing.Size(45, 20);
            this.rdoNu.TabIndex = 8;
            this.rdoNu.TabStop = true;
            this.rdoNu.Text = "Nữ";
            this.rdoNu.UseVisualStyleBackColor = true;
            // 
            // labelMaNV
            // 
            this.labelMaNV.AutoSize = true;
            this.labelMaNV.Location = new System.Drawing.Point(22, 34);
            this.labelMaNV.Name = "labelMaNV";
            this.labelMaNV.Size = new System.Drawing.Size(89, 16);
            this.labelMaNV.TabIndex = 0;
            this.labelMaNV.Text = "Mã nhân viên:";
            // 
            // labelNgaySinh
            // 
            this.labelNgaySinh.AutoSize = true;
            this.labelNgaySinh.Location = new System.Drawing.Point(22, 136);
            this.labelNgaySinh.Name = "labelNgaySinh";
            this.labelNgaySinh.Size = new System.Drawing.Size(70, 16);
            this.labelNgaySinh.TabIndex = 5;
            this.labelNgaySinh.Text = "Ngày sinh:";
            // 
            // labelDiaChi
            // 
            this.labelDiaChi.AutoSize = true;
            this.labelDiaChi.Location = new System.Drawing.Point(22, 219);
            this.labelDiaChi.Name = "labelDiaChi";
            this.labelDiaChi.Size = new System.Drawing.Size(47, 16);
            this.labelDiaChi.TabIndex = 7;
            this.labelDiaChi.Text = "Địa chỉ";
            // 
            // labelHo
            // 
            this.labelHo.AutoSize = true;
            this.labelHo.Location = new System.Drawing.Point(22, 71);
            this.labelHo.Name = "labelHo";
            this.labelHo.Size = new System.Drawing.Size(28, 16);
            this.labelHo.TabIndex = 3;
            this.labelHo.Text = "Họ:";
            // 
            // labelGioiTinh
            // 
            this.labelGioiTinh.AutoSize = true;
            this.labelGioiTinh.Location = new System.Drawing.Point(22, 174);
            this.labelGioiTinh.Name = "labelGioiTinh";
            this.labelGioiTinh.Size = new System.Drawing.Size(57, 16);
            this.labelGioiTinh.TabIndex = 6;
            this.labelGioiTinh.Text = "Giới tính:";
            // 
            // rdoNam
            // 
            this.rdoNam.AutoSize = true;
            this.rdoNam.Location = new System.Drawing.Point(110, 170);
            this.rdoNam.Name = "rdoNam";
            this.rdoNam.Size = new System.Drawing.Size(57, 20);
            this.rdoNam.TabIndex = 1;
            this.rdoNam.TabStop = true;
            this.rdoNam.Text = "Nam";
            this.rdoNam.UseVisualStyleBackColor = true;
            // 
            // labelTen
            // 
            this.labelTen.AutoSize = true;
            this.labelTen.Location = new System.Drawing.Point(22, 102);
            this.labelTen.Name = "labelTen";
            this.labelTen.Size = new System.Drawing.Size(34, 16);
            this.labelTen.TabIndex = 4;
            this.labelTen.Text = "Tên:";
            // 
            // lblDanhSachNhanVien
            // 
            this.lblDanhSachNhanVien.AutoSize = true;
            this.lblDanhSachNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSachNhanVien.ForeColor = System.Drawing.Color.Blue;
            this.lblDanhSachNhanVien.Location = new System.Drawing.Point(362, 432);
            this.lblDanhSachNhanVien.Name = "lblDanhSachNhanVien";
            this.lblDanhSachNhanVien.Size = new System.Drawing.Size(252, 29);
            this.lblDanhSachNhanVien.TabIndex = 20;
            this.lblDanhSachNhanVien.Text = "Danh sách nhân viên";
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Location = new System.Drawing.Point(61, 464);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersWidth = 51;
            this.dgvNhanVien.RowTemplate.Height = 24;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(1059, 150);
            this.dgvNhanVien.TabIndex = 19;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(710, 361);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 57);
            this.btnXoa.TabIndex = 18;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // grpThongTinNhanVien
            // 
            this.grpThongTinNhanVien.Controls.Add(this.txtDiaChi);
            this.grpThongTinNhanVien.Controls.Add(this.txtTen);
            this.grpThongTinNhanVien.Controls.Add(this.txtHo);
            this.grpThongTinNhanVien.Controls.Add(this.txtMaNV);
            this.grpThongTinNhanVien.Controls.Add(this.dtpNgaySinh);
            this.grpThongTinNhanVien.Controls.Add(this.rdoNu);
            this.grpThongTinNhanVien.Controls.Add(this.labelMaNV);
            this.grpThongTinNhanVien.Controls.Add(this.labelNgaySinh);
            this.grpThongTinNhanVien.Controls.Add(this.labelDiaChi);
            this.grpThongTinNhanVien.Controls.Add(this.labelHo);
            this.grpThongTinNhanVien.Controls.Add(this.labelGioiTinh);
            this.grpThongTinNhanVien.Controls.Add(this.rdoNam);
            this.grpThongTinNhanVien.Controls.Add(this.labelTen);
            this.grpThongTinNhanVien.Location = new System.Drawing.Point(61, 73);
            this.grpThongTinNhanVien.Name = "grpThongTinNhanVien";
            this.grpThongTinNhanVien.Size = new System.Drawing.Size(1059, 271);
            this.grpThongTinNhanVien.TabIndex = 15;
            this.grpThongTinNhanVien.TabStop = false;
            this.grpThongTinNhanVien.Text = "Thông tin nhân viên";
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(522, 361);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 57);
            this.btnSua.TabIndex = 17;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // butLuu
            // 
            this.butLuu.Location = new System.Drawing.Point(335, 361);
            this.butLuu.Name = "butLuu";
            this.butLuu.Size = new System.Drawing.Size(75, 57);
            this.butLuu.TabIndex = 16;
            this.butLuu.Text = "Lưu";
            this.butLuu.UseVisualStyleBackColor = true;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(188, 361);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 57);
            this.btnThem.TabIndex = 14;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // frmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 686);
            this.Controls.Add(this.lblDanhSachNhanVien);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.grpThongTinNhanVien);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.butLuu);
            this.Controls.Add(this.btnThem);
            this.Name = "frmNhanVien";
            this.Text = "frmNhanVien";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.grpThongTinNhanVien.ResumeLayout(false);
            this.grpThongTinNhanVien.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.RadioButton rdoNu;
        private System.Windows.Forms.Label labelMaNV;
        private System.Windows.Forms.Label labelNgaySinh;
        private System.Windows.Forms.Label labelDiaChi;
        private System.Windows.Forms.Label labelHo;
        private System.Windows.Forms.Label labelGioiTinh;
        private System.Windows.Forms.RadioButton rdoNam;
        private System.Windows.Forms.Label labelTen;
        private System.Windows.Forms.Label lblDanhSachNhanVien;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.GroupBox grpThongTinNhanVien;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button butLuu;
        private System.Windows.Forms.Button btnThem;
    }
}