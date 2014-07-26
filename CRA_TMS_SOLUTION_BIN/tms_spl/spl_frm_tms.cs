using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tms_spl
{
    public partial class spl_frm_tms : Form
    {
        int i, j;
        public spl_frm_tms()
        {
            InitializeComponent();
        }

        private void spl_frm_tms_Load(object sender, EventArgs e)
        {
            i = 0; j = 0;
            startup_timer.Enabled = true;
        }

        private void startup_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                initlializing_label.Text = initlializing_label.Text + ".";
                i++;
                if (i == 5)
                {
                    initlializing_label.Text = tms_wb_ui.Properties.Resources.STRING_INIT.ToString();
                    j++;
                    i = 0;
                    if (j == 5)
                    {
                        startup_timer.Enabled = false;
                        tms_admin_proc.admin_proc_class get_prompt = new tms_admin_proc.admin_proc_class();
                        this.Enabled = false;
                        this.Visible = false;
                        get_prompt.prompt_admin();
                    }
                }
            }
            catch (Exception thisexception_onspl)
            {
                MessageBox.Show(thisexception_onspl.Message + "\n" + ", or might be any dependenies are not meet to this application.\nDOT NET FRAMEWORK 3.5, SqlServer Compact Edition Required!\n Else, please notify to the company about this bug!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
