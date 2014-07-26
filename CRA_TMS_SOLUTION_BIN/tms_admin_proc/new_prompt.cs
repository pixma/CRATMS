using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tms_admin_proc
{
    public partial class new_prompt : Form
    {
        tms_crt_pb.pb_PERFORM pb_handle = new tms_crt_pb.pb_PERFORM();
        tms_wb_ui.wb_ui wbui_ref = new tms_wb_ui.wb_ui();

        public tms_g_L.tms_g_l_class.MARGINS margins_win;

        public new_prompt()
        {
            InitializeComponent();
        }

        private void new_prompt_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                MessageBox.Show("This Application requires Vista, with Aero enabled.");
                Application.Exit();
            }
            SetGlassRegion();
        }

        private void SetGlassRegion()
        {
            // Set up the glass effect using padding as the defining glass region
            if (tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                margins_win = new tms_g_L.tms_g_l_class.MARGINS();
                margins_win.Top = this.Height;
                margins_win.Left = this.Width;
                margins_win.Bottom = this.Height;
                margins_win.Right = this.Width;
                tms_g_L.tms_g_l_class.DwmExtendFrameIntoClientArea(this.Handle, ref margins_win);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
                // put back the original form background for non-glass area
                Rectangle clientArea = new Rectangle(
                margins_win.Left,
                margins_win.Top,
                this.ClientRectangle.Width - margins_win.Left - margins_win.Right,
                this.ClientRectangle.Height - margins_win.Top - margins_win.Bottom);
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);
            }
        }

        private void comboBox_role_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_role_list.SelectedIndex == 1)
            {
                maskedTextBox_password_admin.Enabled = true;
            }
            else
            {
                maskedTextBox_password_admin.Enabled = false;
            }
        }

        private void button_RolePlay_Click(object sender, EventArgs e)
        {
            if(comboBox_role_list.SelectedIndex==1 && maskedTextBox_password_admin.Text== tms_admin_proc.Properties.Resources.STRING_ADMIN_PASSWORD.ToString())
            {
                this.Visible = false;
                this.Enabled = false;                
                pb_handle.Enabled = true;
                pb_handle.Visible = true;
                pb_handle.progressBar_CRT.Value = 0;
                pb_handle.progressBar_CRT.PerformStep();
                non_admin_call();
                pb_handle.progressBar_CRT.PerformStep();
                wbui_ref.contextMenuStrip_WBUI_CW_DETAIL_GRID_MENU.Items[3].Enabled = true;
                pb_handle.progressBar_CRT.PerformStep();                
                pb_handle.progressBar_CRT.PerformStep();
                wbui_ref.contextMenuStrip_WBUI_SEARCH_ONTEXTMENU.Items[3].Enabled = true;
                pb_handle.progressBar_CRT.PerformStep();
                wbui_ref.admin_bool = true;
                pb_handle.progressBar_CRT.PerformStep();                
                pb_handle.Enabled = false;
                pb_handle.Visible = false;
                wbui_ref.TopMost = true;
                wbui_ref.refresh_window();
                
            }
            else if (comboBox_role_list.SelectedIndex == 0)
            {
                this.Visible = false;
                this.Enabled = false;
                pb_handle.progressBar_CRT.Value = 0;
                pb_handle.Enabled = true;
                pb_handle.Visible = true;
                timer_from_admin.Enabled = true;
            }
            else
            {
                MessageBox.Show(tms_admin_proc.Properties.Resources.STRING_ERROR_MSG.ToString(), tms_admin_proc.Properties.Resources.STRING_FAILED_ADMIN.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void non_admin_call()
        {
            wbui_ref.admin_bool = false;
            wbui_ref.Enabled = true;
            wbui_ref.Visible = true;
            wbui_ref.TopMost = true;                              
            
        }        

        private void new_prompt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void timer_from_admin_Tick(object sender, EventArgs e)
        {            
            pb_handle.progressBar_CRT.PerformStep();
            pb_handle.TopMost = true;
            if (pb_handle.progressBar_CRT.Value == 80)
            {
                timer_from_admin.Enabled = false;                
                pb_handle.Enabled = false;
                pb_handle.Visible = false;
                non_admin_call();                
            }
        }
    }
}
