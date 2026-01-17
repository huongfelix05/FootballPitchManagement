using FootballPitchManagement.Forms.Admin;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new QuenMatKhau());
           //Application.Run(new frmMainKH());
            //Application.Run(new frmLogin());
           //Application.Run(new frmAdmin());
            
          // Application.Run(new frmQuanLyDatSan());

            // Application.Run(new frmDoAn());
          // Application.Run(new Nhap1());
         //Application.Run(new frmLogin());
          // Application.Run(new frmAdmin());
            //Application.Run(new frmLichdatsan());
           Application.Run(new frmQuanlysan()); 
        }
    }
}
