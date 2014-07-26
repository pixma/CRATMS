using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace tms_hc_proc
{
    public class tms_bill_proc_Class
    {
        public PrintDocument printdoc = new PrintDocument();
        public PageSettings pgSettings = new PageSettings();
        public PrinterSettings prtSettings = new PrinterSettings();

        //string STUDENTID_S,string REGISTERATIONID_S,string NAME_S,string ADDRESS_S,string DISTRICT_S,string PROVINCE_S,string NATIONALITY_S,string DOP_S,string DOR_S,string COURSEWAREID_S,string COURSEWARENAME_S,string MOP_S,string GENDER,decimal TOTALFEE_S,decimal calculated_fee, decimal paydown,int topay, int paidoff,string semisters,string durationsof
        public void hc_procedure_event_invoke()
        {
            printdoc.PrintPage += new PrintPageEventHandler(print_page_now);
            print_preview_dialog_invoke();
        }
        public void print_preview_dialog_invoke()
        {
            PrintPreviewDialog dlg_view = new PrintPreviewDialog();
            printdoc.DefaultPageSettings = pgSettings;
            dlg_view.Document = printdoc;
            dlg_view.ShowDialog();
        }
        private void print_page_now(Object sender, PrintPageEventArgs prt_evnt)
        {
            Pen mypen = new Pen(System.Drawing.Color.Black, 5);
            Font print_font = new Font("Arial", 14);
            int left_margin = prt_evnt.MarginBounds.Left;
            int top_margin = prt_evnt.MarginBounds.Top;
            //prt_evnt.Graphics.DrawString(richTextBox1.Text, print_font, Brushes.Black, left_margin, top_margin);           

            //prt_evnt.Graphics.DrawRectangle(mypen, 10, 10, 835, 240);
            //prt_evnt.Graphics.DrawString("Student Name : Annim.", print_font, Brushes.Black, left_margin, top_margin);
            
        }
    }
}
