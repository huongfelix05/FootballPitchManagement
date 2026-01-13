using FootballPitchManagement.Forms.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FootballPitchManagement
{
    public partial class frmMainKH : Form
    {

        public string TenKhachHang { get; set; }
        public frmMainKH()
        {
            InitializeComponent();
            this.Load += frmMain_Load;
        }


        private Form currentFormChild;

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
        private void frmMain_Load(object sender, EventArgs e)
        {
            
            //Hiển thị tên khách hàng lên textBox1
            if (!string.IsNullOrEmpty(TenKhachHang))
            {
                txtName_Load.Text = TenKhachHang;
            }
            else
            {
                txtName_Load.Text = "Khách hàng"; // Giá trị mặc định
            }
        }
        //private UserControl currentControl;
      

        private void btnBooking_Click(object sender, EventArgs e)
        {

           OpenChildForm(new frmLichdatsan());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
                tableLayoutPanel1.Visible = true;
            }
        }

        

        private void btnDatSan1_Click(object sender, EventArgs e)
        {
            
            //OpenChildForm(new frmLichdatsan());
        }

        private void btnDatSan2_Click(object sender, EventArgs e)
        {
           
            //OpenChildForm(new frmLichdatsan());
        }

        private void btnDatSan3_Click(object sender, EventArgs e)
        {
           // OpenChildForm(new frmLichdatsan());
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

//<<<<<<< HEAD
        private void btnProfile_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoSo(1));
        }
//=======
        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDoAn_Click(object sender, EventArgs e)
        {
            // Tạo form đồ ăn mới
   //         frmDoAn fDoAn = new frmDoAn();

            // Gọi cái hàm vừa viết ở trên để nhét nó vào
  //          OpenChildForm(fDoAn);
        }

        private void btnFeedBack_Click(object sender, EventArgs e)
        {

        }

        //>>>>>>> ff74be947abc8f35b04d7f8733f1d21935322cd4
    }
}
