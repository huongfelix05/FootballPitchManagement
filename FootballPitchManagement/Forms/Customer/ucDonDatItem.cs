using System;
using System.Drawing;
using System.Windows.Forms;

namespace FootballPitchManagement
{
    public partial class ucDonDatItem : UserControl
    {
        // Sự kiện để Form cha bắt
        public event EventHandler OnThanhToanClick;
        public event EventHandler OnHuyClick;

        public int MaDatSan { get; set; } // Lưu mã đơn

        public ucDonDatItem()
        {
            InitializeComponent();
            // Gán sự kiện click cho 2 nút
            if (btnThanhToan != null)
                btnThanhToan.Click += (s, e) => OnThanhToanClick?.Invoke(this, e);

            if (btnHuy != null)
                btnHuy.Click += (s, e) => OnHuyClick?.Invoke(this, e);
        }

        // Trong file ucDonDatItem.cs

        // Trong file ucDonDatItem.cs

        // Trong file ucDonDatItem.cs

        public void HienThiThongTin(int maDon, string tenKhach, string tenSan, DateTime ngayDat, string thoiGian, decimal tien, string trangThai)
        {
            this.MaDatSan = maDon;

            // 1. HIỆN NGÀY (Vào Label bạn mới tạo)
            // Bạn nhớ kiểm tra (Name) bên Design có đúng là lblNgayDat không nhé
            if (lblNgayDat != null)
            {
                lblNgayDat.Text = ngayDat.ToString("dd/MM/yyyy");
                // Mẹo: Có thể đổi màu chữ ngày cho nhạt hơn chút (vd: Color.Gray) để đỡ rối
            }

            // 2. HIỆN TÊN KHÁCH (In đậm, to rõ)
            if (lblTenKhach != null)
            {
                lblTenKhach.Text = tenKhach.ToUpper();
            }

            // 3. HIỆN TÊN SÂN
            if (lblThongTin != null)
            {
                lblThongTin.Text = tenSan;
            }

            // 4. HIỆN GIỜ VÀ TIỀN (Giữ nguyên)
            if (lblGio != null) lblGio.Text = thoiGian;
            if (lblGia != null) lblGia.Text = tien.ToString("N0");

            // 5. MÀU SẮC (Giữ nguyên logic cũ)
            if (trangThai == "HOAN_THANH" || trangThai == "DA_THANH_TOAN")
            {
                this.BackColor = Color.LightGreen;
                if (btnThanhToan != null) btnThanhToan.Visible = false;
                if (btnHuy != null) btnHuy.Visible = false;
                if (lblGia != null) lblGia.Text += " (Đã TT)";
            }
            else
            {
                this.BackColor = Color.LightYellow;
                if (btnThanhToan != null) btnThanhToan.Visible = true;
                if (btnHuy != null) btnHuy.Visible = true;
            }
        }
    }
}