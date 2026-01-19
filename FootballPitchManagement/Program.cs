using FootballPitchManagement.Forms.Admin;
using FootballPitchManagement.Forms.Auth;
using FootballPitchManagement.Forms.Customer;
using FootballPitchManagement.Forms.Customer;
//using QuanLySanBong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FootballPitchManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmQuenMatKhau());
            // Application.Run(new frmMainKH());
            //  Application.Run(new frmDanhGia(1));

            // Application.Run(new frmLogin());
            //Application.Run(new frmAdmin());
            // Application.Run(new frmLichdatsan());
            //Application.Run(new frmHoSo());

           // Application.Run(new frmThemSuaSan());

            //Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new QuenMatKhau());
            //Application.Run(new frmMainKH());
            //Application.Run(new frmLogin());
            // Application.Run(new frmAdmin());
            Application.Run(new frmLichdatsan());
           // Application.Run(new frmQuanLyDatSan());
                     //Application.Run(new frmdoanhthuu());

           // Application.Run(new frmNhanVien());


            //Application.Run(new frmBaocao());

        }
    }
}
