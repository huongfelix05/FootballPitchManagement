using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballPitchManagement
{
    public partial class ucSanBong : UserControl
    {
        // 1. Tạo sự kiện Click riêng để Form cha bắt được
        // Khi bấm vào UserControl này, nó sẽ báo cho Form LichDatSan biết
        public event EventHandler OnSelect = null;

        // 2. Các thuộc tính để lưu dữ liệu ngầm
        public int MaSan { get; set; }
        public string TenSan { get; set; }
        public decimal GiaMacDinh { get; set; }
        public int MaChiNhanh { get; set; }

        public string TrangThai { get; set; }

        public ucSanBong()
        {
            InitializeComponent();

            // Gán sự kiện Click cho tất cả thành phần con
            // Để dù người dùng bấm vào Label hay Hình thì vẫn tính là bấm vào Sân
            this.Click += UcSanBong_Click;
            lblTenSan.Click += UcSanBong_Click;
            lblGiaTien.Click += UcSanBong_Click;
            lblTrangThai.Click += UcSanBong_Click;
            if (picIcon != null) picIcon.Click += UcSanBong_Click;
            // Gán sự kiện click cho chính nó và các control con
            this.Click += UcSanBong_Click;
            foreach (Control c in this.Controls)
            {
                c.Click += UcSanBong_Click;
            }
        }

        private void UcSanBong_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện OnSelect để Form cha xử lý
            OnSelect?.Invoke(this, e);
        }

        // 3. Hàm quan trọng: Nhận dữ liệu từ SQL đổ vào giao diện
        public void ThietLapThongTin(int id, string ten, string trangThai, decimal gia)
        {
            MaSan = id;
            TenSan = ten;
            GiaMacDinh = gia;

            // --- [MỚI THÊM 2] Lưu trạng thái vào biến để dùng sau này-- -
            this.TrangThai = trangThai;
            // -----------------------------------------------------------

            // Hiển thị ra Label (Nếu bạn đặt tên đúng như thiết kế)
            if (Controls.ContainsKey("lblTenSan")) Controls["lblTenSan"].Text = ten;
            if (Controls.ContainsKey("lblGiaTien")) Controls["lblGiaTien"].Text = gia.ToString("N0") + " đ";
            if (Controls.ContainsKey("lblTrangThai")) Controls["lblTrangThai"].Text = trangThai;

            // Đổi màu nền theo trạng thái


            lblTenSan.Text = ten;
            lblGiaTien.Text = gia.ToString("N0") + " đ";
            lblTrangThai.Text = trangThai;

            // Xử lý màu sắc
            switch (trangThai)
            {
                case "Trống":
                    this.BackColor = Color.LightGreen; // Xanh
                    break;
                case "Đã đặt":
                    this.BackColor = Color.Salmon;     // Đỏ cam
                    break;
                case "Bảo trì":
                    this.BackColor = Color.LightGray;  // Xám
                    break;
                default:
                    this.BackColor = Color.White;
                    break;
            }
        }
        private void picIcon_Click(object sender, EventArgs e)
        {

        }

        private void lblGiaTien_Click(object sender, EventArgs e)
        {

        }
    }
}
