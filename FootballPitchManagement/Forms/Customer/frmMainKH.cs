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

        private void btnFood_Click(object sender, EventArgs e)
        {
            if (currentFormChild is Nhap1) { return; }
            OpenChildForm(new Nhap1());
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
    }
}
