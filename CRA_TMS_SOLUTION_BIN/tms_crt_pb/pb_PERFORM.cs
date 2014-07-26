using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tms_crt_pb
{
    public partial class pb_PERFORM : Form
    {

        public tms_g_L.tms_g_l_class.MARGINS margin_pb_srt;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                MessageBox.Show("This Application requires Vista, with Aero enabled.");
                Application.Exit();
            }
            else
            {
                margin_pb_srt = new tms_g_L.tms_g_l_class.MARGINS();
                margin_pb_srt.Bottom = this.Height;
                margin_pb_srt.Left = this.Width;
                margin_pb_srt.Top = this.Height;
                margin_pb_srt.Right = this.Width;
                tms_g_L.tms_g_l_class.DwmExtendFrameIntoClientArea(this.Handle, ref margin_pb_srt);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
                Rectangle cA_Rect = new Rectangle(margin_pb_srt.Left, margin_pb_srt.Top, 0, 0);
                Brush br = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(br, cA_Rect);
            }
        }

        public pb_PERFORM()
        {
            InitializeComponent();
        }

        private void pb_PERFORM_Load(object sender, EventArgs e)
        {

        }
    }
}
