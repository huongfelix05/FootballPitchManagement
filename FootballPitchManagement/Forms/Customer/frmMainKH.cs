using FootballPitchManagement.Forms.Customer;
using FootballPitchManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace FootballPitchManagement
{
    public partial class frmMainKH : Form
    {
        public string TenKhachHang { get; set; }
        
        // ✅ BIẾN VÀ CLASS CHI NHÁNH
        private List<ChiNhanhInfo> danhSachChiNhanh = new List<ChiNhanhInfo>();
        
        public class ChiNhanhInfo
        {
            public int MaChiNhanh { get; set; }
            public string TenChiNhanh { get; set; }
            public string DiaChi { get; set; }
            public string DienThoai { get; set; }
            public int SoSan { get; set; }
        }

        private Form currentFormChild;

        public frmMainKH()
        {
            InitializeComponent();
            this.Load += frmMainKH_Load;
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            tableLayoutPanel1.Visible = false;
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
           
            pnlTopBa.Controls.Add(childForm);
            pnlTopBa.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void frmMainKH_Load(object sender, EventArgs e)
        {
            // Hiển thị tên khách hàng
            if (!string.IsNullOrEmpty(TenKhachHang))
            {
                txtName_Load.Text = TenKhachHang;
            }
            else
            {
                txtName_Load.Text = "Khách hàng";
            }

            // ✅ LOAD VÀ HIỂN THỊ CHI NHÁNH LÊN CÁC LABEL CÓ SẴN
            LoadDanhSachChiNhanh();
            HienThiThongTinChiNhanhLenLabel();
            
            // ✅ GÁN SỰ KIỆN CHO CÁC NÚT ĐẶT NGAY
            GanSuKienChoNutDatNgay();
        }

        private void GanSuKienChoNutDatNgay()
        {
            try
            {
                // ✅ XÓA VÀ GÁN LẠI SỰ KIỆN
                btnDatSan1.Click -= btnDatSan1_Click;
                btnDatSan1.Click += btnDatSan1_Click;
                
                btnDatSan2.Click -= btnDatSan2_Click;
                btnDatSan2.Click += btnDatSan2_Click;
                
                btnDatSan3.Click -= btnDatSan3_Click_New;
                btnDatSan3.Click += btnDatSan3_Click_New;
            }
            catch (Exception ex)
            {
                // Bỏ luôn thông báo lỗi này nếu muốn
                // MessageBox.Show($"Lỗi gán sự kiện nút đặt ngay: {ex.Message}", "Lỗi", 
                //     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ SỰ KIỆN NÚT ĐẶT NGAY CHI NHÁNH 1 - BỎ THÔNG BÁO
        private void btnDatSan1_Click(object sender, EventArgs e)
        {
            try
            {
                if (danhSachChiNhanh.Count >= 1)
                {
                    var chiNhanh1 = danhSachChiNhanh[0];
                    
                    // ✅ CHUYỂN THẲNG ĐẾN FORM ĐẶT SÂN - KHÔNG HIỂN THỊ THÔNG BÁO
                    frmLichdatsan formDatSan = new frmLichdatsan(chiNhanh1.MaChiNhanh, chiNhanh1.TenChiNhanh);
                    OpenChildForm(formDatSan);
                }
                else
                {
                    // ✅ MỞ FORM ĐẶT SÂN CHUNG NẾU KHÔNG CÓ CHI NHÁNH CỤ THỂ
                    frmLichdatsan formDatSan = new frmLichdatsan();
                    OpenChildForm(formDatSan);
                }
            }
            catch (Exception ex)
            {
                // ✅ BỎ THÔNG BÁO LỖI - CHUYỂN THẲNG ĐẾN FORM ĐẶT SÂN CHUNG
                frmLichdatsan formDatSan = new frmLichdatsan();
                OpenChildForm(formDatSan);
            }
        }

        // ✅ SỰ KIỆN NÚT ĐẶT NGAY CHI NHÁNH 2 - BỎ THÔNG BÁO
        private void btnDatSan2_Click(object sender, EventArgs e)
        {
            try
            {
                if (danhSachChiNhanh.Count >= 2)
                {
                    var chiNhanh2 = danhSachChiNhanh[1];
                    
                    // ✅ CHUYỂN THẲNG ĐẾN FORM ĐẶT SÂN - KHÔNG HIỂN THỊ THÔNG BÁO
                    frmLichdatsan formDatSan = new frmLichdatsan(chiNhanh2.MaChiNhanh, chiNhanh2.TenChiNhanh);
                    OpenChildForm(formDatSan);
                }
                else
                {
                    // ✅ MỞ FORM ĐẶT SÂN CHUNG NẾU KHÔNG CÓ CHI NHÁNH CỤ THỂ
                    frmLichdatsan formDatSan = new frmLichdatsan();
                    OpenChildForm(formDatSan);
                }
            }
            catch (Exception ex)
            {
                // ✅ BỎ THÔNG BÁO LỖI - CHUYỂN THẲNG ĐẾN FORM ĐẶT SÂN CHUNG
                frmLichdatsan formDatSan = new frmLichdatsan();
                OpenChildForm(formDatSan);
            }
        }

        // ✅ SỰ KIỆN NÚT ĐẶT NGAY CHI NHÁNH 3 - BỎ THÔNG BÁO
        private void btnDatSan3_Click_New(object sender, EventArgs e)
        {
            try
            {
                if (danhSachChiNhanh.Count >= 3)
                {
                    var chiNhanh3 = danhSachChiNhanh[2];
                    
                    // ✅ CHUYỂN THẲNG ĐẾN FORM ĐẶT SÂN - KHÔNG HIỂN THỊ THÔNG BÁO
                    frmLichdatsan formDatSan = new frmLichdatsan(chiNhanh3.MaChiNhanh, chiNhanh3.TenChiNhanh);
                    OpenChildForm(formDatSan);
                }
                else
                {
                    // ✅ MỞ FORM ĐẶT SÂN CHUNG NẾU KHÔNG CÓ CHI NHÁNH CỤ THỂ
                    frmLichdatsan formDatSan = new frmLichdatsan();
                    OpenChildForm(formDatSan);
                }
            }
            catch (Exception ex)
            {
                // ✅ BỎ THÔNG BÁO LỖI - CHUYỂN THẲNG ĐẾN FORM ĐẶT SÂN CHUNG
                frmLichdatsan formDatSan = new frmLichdatsan();
                OpenChildForm(formDatSan);
            }
        }

        // ✅ LOAD DỮ LIỆU CHI NHÁNH TỪ DATABASE
        private void LoadDanhSachChiNhanh()
        {
            using (SqlConnection conn = DatabaseConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            cn.MaChiNhanh,
                            cn.TenChiNhanh,
                            cn.DiaChi,
                            cn.DienThoai,
                            COUNT(s.MaSan) as SoSan
                        FROM ChiNhanh cn
                        LEFT JOIN San s ON cn.MaChiNhanh = s.MaChiNhanh AND s.TrangThai = 1
                        WHERE cn.TrangThai = 1
                        GROUP BY cn.MaChiNhanh, cn.TenChiNhanh, cn.DiaChi, cn.DienThoai
                        ORDER BY cn.TenChiNhanh";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            danhSachChiNhanh.Clear();
                            while (reader.Read())
                            {
                                danhSachChiNhanh.Add(new ChiNhanhInfo
                                {
                                    MaChiNhanh = Convert.ToInt32(reader["MaChiNhanh"]),
                                    TenChiNhanh = reader["TenChiNhanh"].ToString(),
                                    DiaChi = reader["DiaChi"]?.ToString() ?? "",
                                    DienThoai = reader["DienThoai"]?.ToString() ?? "",
                                    SoSan = Convert.ToInt32(reader["SoSan"])
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ✅ BỎ THÔNG BÁO LỖI NẾU MUỐN
                    // MessageBox.Show($"Lỗi load chi nhánh: {ex.Message}", "Lỗi", 
                    //     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HienThiThongTinChiNhanhLenLabel()
        {
            try
            {
                // Nếu có ít nhất 1 chi nhánh → hiển thị lên lblChinhanh1
                if (danhSachChiNhanh.Count >= 1)
                {
                    var chiNhanh1 = danhSachChiNhanh[0];
                    lblChinhanh1.Text = $"📍 {chiNhanh1.DiaChi}\n📞 {chiNhanh1.DienThoai}";
                    lblSosan1.Text = chiNhanh1.SoSan.ToString();
                    label2.Text = chiNhanh1.TenChiNhanh;
                }

                // Nếu có ít nhất 2 chi nhánh → hiển thị lên lblChinhanh2
                if (danhSachChiNhanh.Count >= 2)
                {
                    var chiNhanh2 = danhSachChiNhanh[1];
                    lblChinhanh2.Text = $"📍 {chiNhanh2.DiaChi}\n📞 {chiNhanh2.DienThoai}";
                    lblSosan2.Text = chiNhanh2.SoSan.ToString();
                    lblPitchNABC2.Text = chiNhanh2.TenChiNhanh;
                }

                // Nếu có ít nhất 3 chi nhánh → hiển thị lên lblChinhanh3
                if (danhSachChiNhanh.Count >= 3)
                {
                    var chiNhanh3 = danhSachChiNhanh[2];
                    lblChinhanh3.Text = $"📍 {chiNhanh3.DiaChi}\n📞 {chiNhanh3.DienThoai}";
                    label4.Text = chiNhanh3.SoSan.ToString();
                    lblPitchNABC3.Text = chiNhanh3.TenChiNhanh;
                }
            }
            catch (Exception ex)
            {
                // ✅ BỎ THÔNG BÁO LỖI NẾU MUỐN
                // MessageBox.Show($"Lỗi hiển thị thông tin chi nhánh: {ex.Message}", "Lỗi",
                //     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
                currentFormChild = null;
                tableLayoutPanel1.Visible = true;
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLichdatsan());
        }

        // Các phương thức khác giữ nguyên
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void btnProfile_Click(object sender, EventArgs e) { OpenChildForm(new frmHoSo(1)); }
        private void pnlSidebar_Paint(object sender, PaintEventArgs e) { }
        private void btnDoAn_Click(object sender, EventArgs e) { }
        private void btnFeedBack_Click(object sender, EventArgs e) { }
        private void btnBill_Click(object sender, EventArgs e) { OpenChildForm(new frmHoaDon1()); }
        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e) { }
        private void picFood_Click(object sender, EventArgs e) { }
        private void pictureBox6_Click(object sender, EventArgs e) { }
    }
}
