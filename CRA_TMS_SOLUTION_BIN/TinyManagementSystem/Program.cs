using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TinyManagementSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Form1());
                Application.Run(new tms_spl.spl_frm_tms());
            }
            catch (Exception thread_main_tms)
            {
                MessageBox.Show(thread_main_tms.Message+"\n"+",This Application is Only for Windows 7 Platform!\n or might be any dependenies are not meet to this application.\nDOT NET FRAMEWORK 3.5, SqlServer Compact Edition Required!\n Else, please notify to the company about this bug!","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }
    }
}
