using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tms_admin_proc
{
    public class admin_proc_class
    {
        tms_admin_proc.new_prompt frm_ref = new new_prompt();
        public void prompt_admin()
        {            
            frm_ref.Visible = true;
            frm_ref.Enabled = true;            
        }
    }
}
