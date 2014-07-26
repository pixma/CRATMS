using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;



namespace tms_wb_ui
{
    public partial class wb_ui : Form
    {
        string query_string, selected_data;
        SqlCeConnection connection_courseware_detail_onload = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
        SqlCeConnection connection_student_detail_onload = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
        SqlCeConnection global_connection = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");

        DataTable dataTable_courseware_onload = new DataTable();
        DataTable dataTable_student_onload = new DataTable();
        tms_crt_pb.pb_PERFORM pbhndl = new tms_crt_pb.pb_PERFORM();

        //      int STUDENTID_INT_MAUPULATE, REGISTRATIONID_INT_MANUPULATE;
        string STUDENTID_STRING_MANUPULATE, REGISTRATIONID_STRING_MANUPULATE, COURSEWARE_STRING_MANUPULATE;
        int i, job_control;
        public bool admin_bool,change_theme;

        public tms_g_L.tms_g_l_class.MARGINS wbui_margin;
        

        public wb_ui()
        {
            InitializeComponent();
        }

        private void wb_ui_Load(object sender, EventArgs e)
        {
            try
            {
                change_theme = true;
                if (!tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
                {
                    MessageBox.Show("This Application requires Vista, with Aero enabled.\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    wbui_margin = new tms_g_L.tms_g_l_class.MARGINS();
                    wbui_margin.Top = this.Height;
                    wbui_margin.Bottom = this.Height;
                    wbui_margin.Left = this.Width;
                    wbui_margin.Right = this.Width;
                    tms_g_L.tms_g_l_class.DwmExtendFrameIntoClientArea(this.Handle, ref wbui_margin);
                }

                onload_CWDETAILfill();
                onload_SDDETAILFILL();
                onload_comboxbx_fill_CWNAME();
                about_box_init_details();
                fill_registerationform_procedure();
                refresh_window();                
            }
            catch (Exception wbui_loadexe)
            {
                MessageBox.Show(wbui_loadexe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
                Rectangle wbui_rect = new Rectangle(wbui_margin.Left, wbui_margin.Top, 0, 0);
                Brush wbui_brush = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(wbui_brush, wbui_rect);
            }
        }

        public void refresh_window()
        {
            this.Enabled = false;
            this.Visible = false;
            this.Enabled = true;
            this.Visible = true;

        }

        private void toolStripMenuItem_refresh_Click(object sender, EventArgs e)
        {
            refresh_window();
        }

        private void toolStripMenuItem_new_registration_Click(object sender, EventArgs e)
        {
            get_current_datetime_fill_reg_form();
            tabControl_wb_ui.SelectedIndex = 3;
        }

        private void toolStripMenuItem_paymententry_Click(object sender, EventArgs e)
        {
            tabControl_wb_ui.SelectedIndex = 4;
        }

        private void toolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void wb_ui_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        public void about_box_init_details()
        {
            try
            {
                dataGridView_aboutbox.Columns.Add("field1", " ");
                dataGridView_aboutbox.Columns.Add("field2", " ");
                dataGridView_aboutbox.Columns.Add("field3", " ");
                dataGridView_aboutbox.Columns.Add("field4", " ");

                dataGridView_aboutbox.Rows.Add("Platform:", System.Environment.OSVersion.Platform.ToString(), "Application Name:", "TinyManagementSystem");
                dataGridView_aboutbox.Rows.Add("ServicePack:", System.Environment.OSVersion.ServicePack.ToString(), "Company :", "CRA SoftSystems Prvt Ltd (R)");
                dataGridView_aboutbox.Rows.Add("OS Version:", System.Environment.OSVersion.Version.ToString(), "Developed By:", "Annim Banerjee (C) 2011");
                dataGridView_aboutbox.Rows.Add("Current User Registered :", System.Environment.MachineName.ToString(), "Description :", "This Application is for Educational Purpose Only. All Rights are Reserved.");
            }
            catch (Exception abtbx_onload_exe)
            {
                MessageBox.Show(abtbx_onload_exe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl_wb_ui.SelectedIndex = 5;
            refresh_window();
        }
        private void onload_CWDETAILfill()
        {
            try
            {
                connection_courseware_detail_onload.Open();
                SqlCeCommand command_coursewaredetail_onload = new SqlCeCommand("SELECT [COURSEWARE INFO].* FROM [COURSEWARE INFO]", connection_courseware_detail_onload);
                SqlCeDataAdapter dadaAdapter_courseware_onload = new SqlCeDataAdapter(command_coursewaredetail_onload);
                dadaAdapter_courseware_onload.Fill(dataTable_courseware_onload);
                dataGridView_wbui_dg_cwdetail.DataSource = dataTable_courseware_onload;
                connection_courseware_detail_onload.Close();
            }
            catch (Exception onload_CWDETAILEXE)
            {
                MessageBox.Show(onload_CWDETAILEXE.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void onload_SDDETAILFILL()
        {
            try
            {
                global_connection.Open();
                SqlCeCommand command_student_detail_onload = new SqlCeCommand("SELECT [STUDENT DETAIL TABLE].* FROM [STUDENT DETAIL TABLE]", global_connection);
                SqlCeDataAdapter dataAdapter_studentdetail_onload = new SqlCeDataAdapter(command_student_detail_onload);
                dataAdapter_studentdetail_onload.Fill(dataTable_student_onload);
                dataGridView_wbui_student_detail.DataSource = dataTable_student_onload;
                global_connection.Close();
            }
            catch (Exception onload_SDDETAILEXEC)
            {
                MessageBox.Show(onload_SDDETAILEXEC.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void onload_comboxbx_fill_CWNAME()
        {
            try
            {
                SqlCeConnection connection_onload_comboxbx_fill_CWNAME = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                query_string = "SELECT [COURSEWARE INFO].[COURSEWARE NAME] FROM [COURSEWARE INFO]";
                SqlCeCommand command_onload_comboxbx_fill_CWNAME = new SqlCeCommand(query_string, connection_onload_comboxbx_fill_CWNAME);
                connection_onload_comboxbx_fill_CWNAME.Open();
                SqlCeDataReader dataReader_onload_comboxbx_fill_CWNAME = command_onload_comboxbx_fill_CWNAME.ExecuteReader();
                while (dataReader_onload_comboxbx_fill_CWNAME.Read())
                {
                    comboBox_CW_LIST_WBUI.Items.Add(dataReader_onload_comboxbx_fill_CWNAME[0].ToString());
                    comboBox_CW_NEW_CW_REGISTER_CWLIST.Items.Add(dataReader_onload_comboxbx_fill_CWNAME[0].ToString());
                }
                connection_onload_comboxbx_fill_CWNAME.Close();
            }
            catch (Exception onload_combobx_exe)
            {
                MessageBox.Show(onload_combobx_exe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView_wbui_dg_cwdetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selected_data = dataGridView_wbui_dg_cwdetail.Columns[dataGridView_wbui_dg_cwdetail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_dg_cwdetail.CurrentCell.RowIndex].Cells[1].Value.ToString();
            query_string = "SELECT [STUDENT DETAIL TABLE].[STUDENT ID], [STUDENT DETAIL TABLE].[STUDENT NAME], [STUDENT DETAIL TABLE].ADDRESS, [STUDENT DETAIL TABLE].DISTRICT, [STUDENT DETAIL TABLE].PROVINCE, [STUDENT DETAIL TABLE].NATIONALITY, [STUDENT DETAIL TABLE].[CONTAT NOS], [STUDENT DETAIL TABLE].DOR, [TABLE SDID REGID].[REGISTRATION ID], [TABLE SDID REGID].[COURSEWARE ID], [TABLE SDID REGID].[COURSEWARE NAME] FROM [STUDENT DETAIL TABLE] INNER JOIN [TABLE SDID REGID] ON [STUDENT DETAIL TABLE].[STUDENT ID] = [TABLE SDID REGID].[STUDENT ID] WHERE ([TABLE SDID REGID].[COURSEWARE ID] = N'" + selected_data + "')";
            student_enrolled_handler_from_datagrid_wbui(query_string);
        }

        private void studentEnrolledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl_wb_ui.SelectedIndex == 0)
            {
                selected_data = dataGridView_wbui_dg_cwdetail.Columns[dataGridView_wbui_dg_cwdetail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_dg_cwdetail.CurrentCell.RowIndex].Cells[1].Value.ToString();
                query_string = "SELECT [STUDENT DETAIL TABLE].*, [TABLE SDID REGID].[REGISTRATION ID], [TABLE SDID REGID].[COURSEWARE ID], [TABLE SDID REGID].[COURSEWARE NAME] FROM [STUDENT DETAIL TABLE] INNER JOIN [TABLE SDID REGID] ON [STUDENT DETAIL TABLE].[STUDENT ID] = [TABLE SDID REGID].[STUDENT ID] WHERE ([TABLE SDID REGID].[COURSEWARE ID] = N'" + selected_data + "')";
                student_enrolled_handler_from_datagrid_wbui(query_string);
            }
            else if (tabControl_wb_ui.SelectedIndex == 1)
            {
                selected_data = dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[2].Value.ToString();
                query_string = "SELECT [STUDENT DETAIL TABLE].*, [TABLE SDID REGID].[REGISTRATION ID], [TABLE SDID REGID].[COURSEWARE ID], [TABLE SDID REGID].[COURSEWARE NAME] FROM [STUDENT DETAIL TABLE] INNER JOIN [TABLE SDID REGID] ON [STUDENT DETAIL TABLE].[STUDENT ID] = [TABLE SDID REGID].[STUDENT ID] WHERE ([TABLE SDID REGID].[COURSEWARE ID] = N'" + selected_data + "')";
                student_enrolled_handler_from_datagrid_wbui(query_string);
            }
        }

        private void courseWareEnrolledInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                selected_data = dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString();
                query_string = "SELECT [TABLE SDID REGID].* FROM [TABLE SDID REGID] WHERE [TABLE SDID REGID].[STUDENT ID]='" + selected_data + "'";
                handler_student_enrolled_onto(query_string);
            }
            catch (Exception courseWareEnrolledInToolStripMenuItem_Click_exe)
            {
                MessageBox.Show("Select any cell then proceed, or\n" + courseWareEnrolledInToolStripMenuItem_Click_exe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView_wbui_student_detail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selected_data = dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString();
            query_string = "SELECT [TABLE SDID REGID].* FROM [TABLE SDID REGID] WHERE [TABLE SDID REGID].[STUDENT ID]='" + selected_data + "'";
            handler_student_enrolled_onto(query_string);
        }
        private void student_enrolled_handler_from_datagrid_wbui(string command_query)
        {
            try
            {
                SqlCeConnection connection_enrolled_student_oncontextmenu = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                SqlCeCommand command_enrolled_student_oncontextmenu = new SqlCeCommand(command_query, connection_enrolled_student_oncontextmenu);
                SqlCeDataAdapter dataAdapter_enrolled_student_onontextmenu = new SqlCeDataAdapter(command_enrolled_student_oncontextmenu);
                DataTable dt_enrolled_student_oncontextmenu = new DataTable();
                dataAdapter_enrolled_student_onontextmenu.Fill(dt_enrolled_student_oncontextmenu);
                connection_enrolled_student_oncontextmenu.Close();
                tabControl_wb_ui.SelectedIndex = 1;
                dataGridView_wbui_student_detail.DataSource = dt_enrolled_student_oncontextmenu;
                refresh_window();

            }
            catch (Exception exec_enrolled_student_oncontextmenu)
            {
                MessageBox.Show(exec_enrolled_student_oncontextmenu.Message + "\nSelect any cell then RightClick over it,\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void handler_student_enrolled_onto(string comand)
        {
            try
            {
                SqlCeConnection connection_dataGridView_wbui_student_detail_CellDoubleClick = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                SqlCeCommand command_dataGridView_wbui_student_detail_CellDoubleClick = new SqlCeCommand(comand, connection_dataGridView_wbui_student_detail_CellDoubleClick);
                SqlCeDataAdapter dataAdapter_dataGridView_wbui_student_detail_CellDoubleClick = new SqlCeDataAdapter(command_dataGridView_wbui_student_detail_CellDoubleClick);
                DataTable dt_dataGridView_wbui_student_detail_CellDoubleClick = new DataTable();
                dataAdapter_dataGridView_wbui_student_detail_CellDoubleClick.Fill(dt_dataGridView_wbui_student_detail_CellDoubleClick);
                connection_dataGridView_wbui_student_detail_CellDoubleClick.Close();
                dataGridView_wbui_studentdetail_bottom.DataSource = dt_dataGridView_wbui_student_detail_CellDoubleClick;
                refresh_window();
            }
            catch (Exception dataGridView_wbui_student_detail_CellDoubleClick_exe)
            {
                MessageBox.Show(dataGridView_wbui_student_detail_CellDoubleClick_exe.Message + "\nDouble Click on any cell Properly,\n " + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void get_search_procedure()
        {
            try
            {
                query_string = "SELECT [STUDENT DETAIL TABLE].* FROM [STUDENT DETAIL TABLE] WHERE [STUDENT NAME] LIKE '" + textBox_WBUI_SDDETAIL_SDNAME_SEARCH.Text + "%'";
                SqlCeConnection connection_get_search_procedure = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                SqlCeCommand command_get_search_procedure = new SqlCeCommand(query_string, connection_get_search_procedure);
                connection_get_search_procedure.Open();
                SqlCeDataAdapter dataAdapter_get_search_procedure = new SqlCeDataAdapter(command_get_search_procedure);
                DataTable dt_get_search_procedure = new DataTable();
                dataAdapter_get_search_procedure.Fill(dt_get_search_procedure);
                dataGridView_wbui_student_detail.DataSource = dt_get_search_procedure;
                connection_get_search_procedure.Close();
                dataGridView_wbui_student_detail.Refresh();

            }
            catch (Exception get_search_procedure_exe)
            {
                MessageBox.Show(get_search_procedure_exe.Message + "\n, Check weather you have specified sufficient information.\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_WBUI_STUDENTDETAIL_SEARCH_BUTTON_Click(object sender, EventArgs e)
        {
            get_search_procedure();
        }

        private void tabControl_wb_ui_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_current_datetime_fill_reg_form();
            refresh_window();
            if (tabControl_wb_ui.SelectedIndex == 1)
            {
                contextMenuStrip_WBUI_CW_DETAIL_GRID_MENU.Items[1].Enabled = true;
                if (admin_bool == true)
                {
                    contextMenuStrip_WBUI_CW_DETAIL_GRID_MENU.Items[3].Enabled = false;                    
                    contextMenuStrip_WBUI_SEARCH_ONTEXTMENU.Items[3].Enabled = true;
                }
            
            }
            else
            {
                contextMenuStrip_WBUI_CW_DETAIL_GRID_MENU.Items[1].Enabled = false;
                if (admin_bool == true)
                {
                    contextMenuStrip_WBUI_CW_DETAIL_GRID_MENU.Items[3].Enabled = true;                    
                    contextMenuStrip_WBUI_SEARCH_ONTEXTMENU.Items[3].Enabled = false;
                }
            }

        }

        private void textBox_WBUI_SDDETAIL_SDNAME_SEARCH_TextChanged(object sender, EventArgs e)
        {
            get_search_procedure();
        }

        private void paymentEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PAYMENT_EVENT_INVOKE_PROCEDURE();
            }
            catch(Exception paymentEntryToolStripMenuItem_Click_exe)
            {
                MessageBox.Show(paymentEntryToolStripMenuItem_Click_exe.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void comboBox_status_of_payment_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh_window();
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns[4].DataGridView.Rows[3].Cells[4].Value = comboBox_status_of_payment.SelectedItem.ToString();
            if (comboBox_status_of_payment.SelectedIndex == 1)
            {
                BUTTON_REGISTER.Enabled = true;
            }
            else
            {
                BUTTON_REGISTER.Enabled = false;
            }
        }

        private void comboBox_CW_LIST_WBUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox_mop_wbui_list.Enabled = true;

                COURSEWARE_GRID_FILL_INVOKE();

                SqlCeConnection connection_courseware_name_only_changin = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                query_string = "SELECT [COURSEWARE INFO].[COURSEWARE ID] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_LIST_WBUI.SelectedItem.ToString() + "')";
                SqlCeCommand command_courseware_name_only_changin = new SqlCeCommand(query_string, connection_courseware_name_only_changin);
                connection_courseware_name_only_changin.Open();
                SqlCeDataReader dr_courseware_name_only_changin = command_courseware_name_only_changin.ExecuteReader();
                if (dr_courseware_name_only_changin.Read())
                {
                    dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[2].Cells[3].Value = comboBox_CW_LIST_WBUI.SelectedItem.ToString();
                    dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value = dr_courseware_name_only_changin[0].ToString();
                }
                connection_courseware_name_only_changin.Close();
            }
            catch (Exception comboBox_CW_LIST_WBUI_SelectedIndexChanged_exe)
            {
                MessageBox.Show(comboBox_CW_LIST_WBUI_SelectedIndexChanged_exe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void fill_registerationform_procedure()
        {
            try
            {

                SqlCeConnection connection_retrive_sdid = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                query_string = "SELECT [TEMP DETAIL].* FROM [TEMP DETAIL]";
                SqlCeCommand command_retrive_sdid = new SqlCeCommand(query_string, connection_retrive_sdid);
                connection_retrive_sdid.Open();
                SqlCeDataReader dr_fill_registerationform_procedure = command_retrive_sdid.ExecuteReader();
                if (dr_fill_registerationform_procedure.Read())
                {
                    STUDENTID_STRING_MANUPULATE = dr_fill_registerationform_procedure[0].ToString();
                    REGISTRATIONID_STRING_MANUPULATE = dr_fill_registerationform_procedure[1].ToString();
                }
                connection_retrive_sdid.Close();
                dataGridView_REGISTRATION_FORM_GRID.Rows.Add("Student Name :", "", "Student ID :", "TMSSDID"+STUDENTID_STRING_MANUPULATE);
                dataGridView_REGISTRATION_FORM_GRID.Rows.Add("Address :", "", "Registration ID :", "TMSREGID"+REGISTRATIONID_STRING_MANUPULATE);
                dataGridView_REGISTRATION_FORM_GRID.Rows.Add("District :", "", "CourseWare Name :", "");
                dataGridView_REGISTRATION_FORM_GRID.Rows.Add("Province :", "", "CourseWare ID :", "");
                dataGridView_REGISTRATION_FORM_GRID.Rows.Add("Nationality :", "", "Date Of Registration :", System.DateTime.Now.ToString());
                dataGridView_REGISTRATION_FORM_GRID.Rows.Add("Contact Nos :", "+91-0000000000", "Date Of Payment :", System.DateTime.Now.ToString());

            }
            catch (Exception fill_registerationform_procedure_exe)
            {
                MessageBox.Show(fill_registerationform_procedure_exe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void get_current_datetime_fill_reg_form()
        {
            dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value = System.DateTime.Now.ToString();
            dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[5].Cells[3].Value = System.DateTime.Now.ToString();
        }

        private void comboBox_mop_wbui_list_SelectedIndexChanged(object sender, EventArgs e) 
        {
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Clear();
            refresh_window();
            BILL_GENERATION_ENGINE_INVOKE();
        }
        public void BILL_GENERATION_ENGINE_INVOKE()
        {
            try
            {                
                SqlCeConnection connection_totalfee_BILL_GENERATION_ENGINE_INVOKE = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
                query_string = "SELECT [COURSEWARE INFO].[COST] FROM [COURSEWARE INFO] WHERE ([COURSEWARE INFO].[COURSEWARE NAME]=N'" + comboBox_CW_LIST_WBUI.SelectedItem.ToString() + "')";
                SqlCeCommand command_totalfee_GENERATION_ENGINE_INVOKE = new SqlCeCommand(query_string, connection_totalfee_BILL_GENERATION_ENGINE_INVOKE);
                connection_totalfee_BILL_GENERATION_ENGINE_INVOKE.Open();
                SqlCeDataReader dr_totalfee_GENERATION_ENGINE_INVOKE = command_totalfee_GENERATION_ENGINE_INVOKE.ExecuteReader();
                if (dr_totalfee_GENERATION_ENGINE_INVOKE.Read())
                {
                    calculated_TextBox_DOWNPAYMENTAMOUNT.Text = dr_totalfee_GENERATION_ENGINE_INVOKE[0].ToString();                    
                }
                connection_totalfee_BILL_GENERATION_ENGINE_INVOKE.Close();
                modewise_generation_document();
            }
            catch (Exception BILL_GENERATION_ENGINE_INVOKE_EXEC)
            {
                MessageBox.Show(BILL_GENERATION_ENGINE_INVOKE_EXEC.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void COURSEWARE_GRID_FILL_INVOKE()
        {
            SqlCeConnection connection_COURSEWARE_GRID_FILL_INVOKE = new SqlCeConnection("Data Source=TMS_DATABASE_LOCAL_RESIDE.sdf;Persist Security Info=false;");
            query_string = "SELECT [COURSEWARE INFO].* FROM [COURSEWARE INFO] WHERE [COURSEWARE INFO].[COURSEWARE NAME]=N'"+comboBox_CW_LIST_WBUI.SelectedItem.ToString()+"'";
            SqlCeCommand command_COURSEWARE_GRID_FILL_INVOKE = new SqlCeCommand(query_string, connection_COURSEWARE_GRID_FILL_INVOKE);
            connection_COURSEWARE_GRID_FILL_INVOKE.Open();
            SqlCeDataAdapter dataAdapter_COURSEWARE_GRID_FILL_INVOKE = new SqlCeDataAdapter(command_COURSEWARE_GRID_FILL_INVOKE);
            DataTable dt_COURSEWARE_GRID_FILL_INVOKE = new DataTable();
            dataAdapter_COURSEWARE_GRID_FILL_INVOKE.Fill(dt_COURSEWARE_GRID_FILL_INVOKE);
            dataGridView_registration_form_cwdetail_selected.DataSource = dt_COURSEWARE_GRID_FILL_INVOKE;
            connection_COURSEWARE_GRID_FILL_INVOKE.Close();
            refresh_window();
        }
        public void modewise_generation_document()
        {      
            if(comboBox_mop_wbui_list.SelectedIndex==0)
            {                
                calculated_TextBox_DOWNPAYMENTAMOUNT.Text = Convert.ToString((Convert.ToDecimal(calculated_TextBox_DOWNPAYMENTAMOUNT.Text) * Convert.ToDecimal(0.02)) + Convert.ToDecimal(calculated_TextBox_DOWNPAYMENTAMOUNT.Text));
                LUM_SUM_GRID_FILL();
            }
            else if(comboBox_mop_wbui_list.SelectedIndex==1)
            {
                calculated_TextBox_DOWNPAYMENTAMOUNT.Text = Convert.ToString(((Convert.ToDecimal(calculated_TextBox_DOWNPAYMENTAMOUNT.Text) / Convert.ToDecimal(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString())) * Convert.ToDecimal(0.02)) + (Convert.ToDecimal(calculated_TextBox_DOWNPAYMENTAMOUNT.Text) / Convert.ToDecimal(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString())));
                SEMISTER_TYPE_GRID_FILL();
            }
            else
            {
                calculated_TextBox_DOWNPAYMENTAMOUNT.Text = Convert.ToString(((Convert.ToDecimal(calculated_TextBox_DOWNPAYMENTAMOUNT.Text) / Convert.ToDecimal(dataGridView_registration_form_cwdetail_selected.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString())) * Convert.ToDecimal(0.02)) + Convert.ToDecimal(((Convert.ToDecimal(calculated_TextBox_DOWNPAYMENTAMOUNT.Text) / Convert.ToDecimal(dataGridView_registration_form_cwdetail_selected.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString())))));
                INSTALLMENT_TYPE_GRID_FILL();
            }
        }

        public void LUM_SUM_GRID_FILL()
        {            

            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C1", "Semisters");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C2", "CourseWare Package Fee");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C3", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C4", "Amount Due");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C5", "Status");

            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "Educational Institute : TinyManagementSystem.", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "CRA SoftSystems Prvt Ltd, Annim Banerjee (C) 2011", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString(), dataGridView_registration_form_cwdetail_selected.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString(), "", calculated_TextBox_DOWNPAYMENTAMOUNT.Text,"NA");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("Mode Of Payment :", comboBox_mop_wbui_list.SelectedItem.ToString(), "", "Date of Payment :", dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString());
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("KITS AVAILED:", dataGridView_registration_form_cwdetail_selected.Columns[6].DataGridView.Rows[0].Cells[6].Value.ToString(), "", "", "");
        }
        public void SEMISTER_TYPE_GRID_FILL()
        {
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C1", "Semisters");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C2", "CourseWare Package Amount");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C3", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C4", "Amount Due");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C3", "Status");

            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "Educational Institute : TinyManagementSystem", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "CRA SoftSystems Prvt Ltd(R), Annim Banerjee (C) 2011", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("1", dataGridView_registration_form_cwdetail_selected.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString(), "", calculated_TextBox_DOWNPAYMENTAMOUNT.Text, "NA");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("Mode Of Payment:", comboBox_mop_wbui_list.SelectedItem.ToString(), "", "Date Of Payment:", dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString());
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("KITS AVAILED:", Convert.ToString(Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[6].DataGridView.Rows[0].Cells[6].Value.ToString())/Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString())), "", "", "");

            for (i = 2; i <= Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString()); i++)
            {
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add(i.ToString(), Convert.ToString(Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString()) / Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString())), "", calculated_TextBox_DOWNPAYMENTAMOUNT.Text, "Not Payed!");
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("Due Date :", dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString(), "", "", "");
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("KITS AVAILED:", Convert.ToString(Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[6].DataGridView.Rows[0].Cells[6].Value.ToString()) / Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString())), "", "", "");
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            }
        }
        public void INSTALLMENT_TYPE_GRID_FILL()
        {

            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C1", "Installments");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C2", "CourseWare Package Amount");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C3", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C4", "Amount Due");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Columns.Add("C3", "Status");

            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "Educational Institute : TinyManagementSystem", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "CRA SoftSystems Prvt Ltd(R), Annim Banerjee (C) 2011", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("1", Convert.ToString( Convert.ToDecimal(dataGridView_registration_form_cwdetail_selected.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString())/Convert.ToDecimal(dataGridView_registration_form_cwdetail_selected.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString())), "", calculated_TextBox_DOWNPAYMENTAMOUNT.Text, "NA");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("Mode Of Payment:", comboBox_mop_wbui_list.SelectedItem.ToString(), "", "Date Of Payment:", dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString());
            dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("KITS AVAILED:", "1", "", "", "");

            for (i = 2; i <= Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString()); i++)
            {
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add(i.ToString(), Convert.ToString(Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString()) / Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString())), "", calculated_TextBox_DOWNPAYMENTAMOUNT.Text, "Not Payed!");
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("Due Date :", dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString(), "", "", "");
                if (!(i % 2 == 0))
                {
                    dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("KITS AVAILED:","1", "", "", "");
                }
                else
                {
                    dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("KITS AVAILED:", "0", "", "", "");
                }
                dataGridView_LOGS_REGISTRATION_BY_MOP.Rows.Add("", "", "", "", "");
            }
        }

        private void BUTTON_REGISTER_Click(object sender, EventArgs e)
        {
            hit_registeration_action();
        }
        public void hit_registeration_action()
        {
            try
            {
                global_connection.Open();
                query_string = "INSERT INTO [STUDENT DETAIL TABLE] ([STUDENT ID], [STUDENT NAME], ADDRESS, DISTRICT, PROVINCE, NATIONALITY, [CONTAT NOS], DOR, Gender) VALUES (N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[1].DataGridView.Rows[0].Cells[1].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[1].DataGridView.Rows[1].Cells[1].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[1].DataGridView.Rows[2].Cells[1].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[1].DataGridView.Rows[3].Cells[1].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[1].DataGridView.Rows[4].Cells[1].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[1].DataGridView.Rows[5].Cells[1].Value.ToString() + "',CONVERT(DATETIME,'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString() + "',102),N'" + comboBox_gender.SelectedItem.ToString() + "')";
                SqlCeCommand command_lumsum_hit_registeration_action = new SqlCeCommand(query_string,global_connection);
                if (command_lumsum_hit_registeration_action.ExecuteNonQuery() > 0)
                {
                        global_connection.Close();
                        pbhndl.Enabled = true;
                        pbhndl.Visible = true;
                        pbhndl.progressBar_CRT.PerformStep();
                        student_id_regid_insertion();
                        payloadtable_insertion();
                        finalcall_after_insertion_all();
                }
                else
                {
                        global_connection.Close();
                        MessageBox.Show("Data Insertion Unsuccessful!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                                                     
            }
            catch (Exception hit_registeration_action_exec)
            {
                global_connection.Close();
                MessageBox.Show(hit_registeration_action_exec.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            Application.Restart();
        }
        public void student_id_regid_insertion()
        {
            global_connection.Open();
            if (comboBox_mop_wbui_list.SelectedIndex == 0)
            {
                query_string = "INSERT INTO [TABLE SDID REGID] ([STUDENT ID], [REGISTRATION ID], [COURSEWARE ID], [COURSEWARE NAME], TTP, TPD, MOP) VALUES (N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[1].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[2].Cells[3].Value.ToString() + "',1,1,N'L')";
                SqlCeCommand COMMAND_student_id_regid_insertion = new SqlCeCommand(query_string, global_connection);
                if (COMMAND_student_id_regid_insertion.ExecuteNonQuery() > 0)
                {
                    global_connection.Close();
                    pbhndl.progressBar_CRT.PerformStep();
                }
                else
                {
                    global_connection.Close();
                    MessageBox.Show("Data Insertion Unsuccessful!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBox_mop_wbui_list.SelectedIndex == 1)
            {
                query_string = "INSERT INTO [TABLE SDID REGID] ([STUDENT ID], [REGISTRATION ID], [COURSEWARE ID], [COURSEWARE NAME], TTP, TPD, MOP) VALUES (N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[1].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[2].Cells[3].Value.ToString() + "'," + Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString()) + ",1, N'S')";
                SqlCeCommand COMMAND_student_id_regid_insertion = new SqlCeCommand(query_string, global_connection);
                if (COMMAND_student_id_regid_insertion.ExecuteNonQuery() > 0)
                {
                    global_connection.Close();
                    pbhndl.progressBar_CRT.PerformStep();
                }
                else
                {
                    global_connection.Close();
                    MessageBox.Show("Data Insertion Unsuccessful!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                query_string = "INSERT INTO [TABLE SDID REGID] ([STUDENT ID], [REGISTRATION ID], [COURSEWARE ID], [COURSEWARE NAME], TTP, TPD, MOP) VALUES (N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[1].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[2].Cells[3].Value.ToString() + "'," + Convert.ToInt32(dataGridView_registration_form_cwdetail_selected.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString()) + ",1, N'I')";
                SqlCeCommand COMMAND_student_id_regid_insertion = new SqlCeCommand(query_string, global_connection);
                if (COMMAND_student_id_regid_insertion.ExecuteNonQuery() > 0)
                {
                    global_connection.Close();
                    pbhndl.progressBar_CRT.PerformStep();
                }
                else
                {
                    global_connection.Close();
                    MessageBox.Show("Data Insertion Unsuccessful!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void payloadtable_insertion()
        {
            try
            {
                global_connection.Open();
                query_string = "INSERT INTO [PAY LOAD TABLE] ([STUDENT ID],[REGISTRATION ID], AMOUNT, DOP) VALUES (N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',N'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[1].Cells[3].Value.ToString() + "'," + calculated_TextBox_DOWNPAYMENTAMOUNT.Text.ToString() + ",CONVERT(DATETIME,'" + dataGridView_REGISTRATION_FORM_GRID.Columns[3].DataGridView.Rows[5].Cells[3].Value.ToString() + "',102))";
                SqlCeCommand command_payloadtable_insertion = new SqlCeCommand(query_string, global_connection);
                if (command_payloadtable_insertion.ExecuteNonQuery() > 0)
                {
                    global_connection.Close();
                    pbhndl.progressBar_CRT.PerformStep();
                }
                else
                    MessageBox.Show("Data Insertion Unsuccessful!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception payloadtable_insertion_exec)
            {
                global_connection.Close();
                MessageBox.Show(payloadtable_insertion_exec.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void finalcall_after_insertion_all()
        {
            global_connection.Open();
            query_string = "UPDATE [TEMP DETAIL] SET SDID= " + Convert.ToString(Convert.ToInt32(STUDENTID_STRING_MANUPULATE) + 1) + ", REGID=" + Convert.ToString(Convert.ToInt32(REGISTRATIONID_STRING_MANUPULATE) + 1);
            SqlCeCommand command_finalcall_after_insertion_all = new SqlCeCommand(query_string, global_connection);
            if (command_finalcall_after_insertion_all.ExecuteNonQuery() > 0)
            {
                global_connection.Close();
                pbhndl.progressBar_CRT.PerformStep();
                pbhndl.Enabled = false;
                pbhndl.Visible = false;
                MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_MSG_SUCCESS.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                global_connection.Close();
                MessageBox.Show("Data Insertion Unsuccessful!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        private void toolStripMenuItem_new_CW_REGISTERATION_Click(object sender, EventArgs e)
        {
            tabControl_wb_ui.SelectedIndex = 4;
            NEW_CW_REG_PROCESS_INVOKE_MODULE();
        }

        public void NEW_CW_REG_PROCESS_INVOKE_MODULE()
        {
            global_connection.Open();
            dataGridView_existingstudent_cw_entry_reg_GRID.Rows.Clear();
            dataGridView_existingstudent_cw_entry_reg_GRID.Columns.Clear();
            try
            {
                query_string = "SELECT * FROM [STUDENT DETAIL TABLE] WHERE [STUDENT DETAIL TABLE].[STUDENT ID]=N'"+dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString()+"'";
                SqlCeCommand command_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAILSELECTION = new SqlCeCommand(query_string, global_connection);
                SqlCeDataAdapter adapter_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAIL_SELECTION = new SqlCeDataAdapter(command_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAILSELECTION);
                DataTable dt_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAIL_SELECTION = new DataTable();
                adapter_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAIL_SELECTION.Fill(dt_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAIL_SELECTION);
                global_connection.Close();
                dataGridView_existingstudent_cw_entry_reg_GRID.DataSource = dt_NEW_CW_REG_PROCESS_INVOKE_MODULE_STD_DETAIL_SELECTION;                
            }
            catch (Exception NEW_CW_REG_PROCESS_INVOKE_MODULE_EXEC)
            {
                MessageBox.Show(NEW_CW_REG_PROCESS_INVOKE_MODULE_EXEC.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
                global_connection.Close();
            }
        }        
        private void comboBox_CW_NEW_CW_REGISTER_CWLIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_NEW_CW_REGISTER_MOP_LIST.Enabled = true;
        }

        private void comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Clear();
                dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Clear();
                if (comboBox_NEW_CW_REGISTER_MOP_LIST.SelectedIndex==0)
                {
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C1", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C2", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C3", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C4", "");

                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("CourseWare Name: ", comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString(), "Status of Payment:", "");
                    global_connection.Open();
                    query_string = "SELECT [COURSEWARE INFO].[COURSEWARE ID] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                    SqlCeCommand command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1 = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1 = command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1.ExecuteReader();
                    if (dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1.Read()) 
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("CourseWare ID:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1[0].ToString(), "", "");
                        global_connection.Close(); 
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }
                    
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("", "", "", "");

                    query_string = "SELECT [REGID] FROM [TEMP DETAIL]";
                    global_connection.Open();
                    SqlCeCommand command_tempdetail_forlog = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_tempdetail_forlog = command_tempdetail_forlog.ExecuteReader();
                    if(dr_tempdetail_forlog.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("Payment Scheme:", "LUM-SUM", "Registeration ID", "TMSREGID"+dr_tempdetail_forlog[0].ToString());
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }
                    global_connection.Open();
                    query_string = "SELECT [COURSEWARE INFO].[COST] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                    SqlCeCommand command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_CMD2 = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2 = command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_CMD2.ExecuteReader();
                    if (dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2.Read()) 
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("Amount Due:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString(), "Amount Paid:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString());
                        textBox_NEW_CW_REGISTER_DOWNPAYMENT_BOX.Text = dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString();
                        global_connection.Close(); 
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }
                    global_connection.Close();                    
                }
                else if (comboBox_NEW_CW_REGISTER_MOP_LIST.SelectedIndex == 1)
                {
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C1", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C2", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C3", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C4", "");

                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("CourseWare Name: ", comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString(), "Status of Payment:", "");
                    global_connection.Open();
                    query_string = "SELECT [COURSEWARE INFO].[COURSEWARE ID] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                    SqlCeCommand command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1 = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1 = command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1.ExecuteReader();
                    if (dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("CourseWare ID:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1[0].ToString(), "", "");
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }

                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("", "", "", "");

                    query_string = "SELECT [REGID] FROM [TEMP DETAIL]";
                    global_connection.Open();
                    SqlCeCommand command_tempdetail_forlog = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_tempdetail_forlog = command_tempdetail_forlog.ExecuteReader();
                    if (dr_tempdetail_forlog.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("Payment Scheme:", "Semister Wise", "Registeration ID", "TMSREGID" + dr_tempdetail_forlog[0].ToString());
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }

                    global_connection.Open();
                    query_string = "SELECT [COURSEWARE INFO].[COST], [COURSEWARE INFO].[SEMISTERS] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                    SqlCeCommand command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_CMD2 = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2 = command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_CMD2.ExecuteReader();
                    if (dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("Amount Due:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString(), "Amount To Pay Each Semister", Convert.ToString(Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString()) / Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[1].ToString())));
                        textBox_NEW_CW_REGISTER_DOWNPAYMENT_BOX.Text = Convert.ToString(Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString()) / Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[1].ToString()));
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }
                    global_connection.Close();    
                    
                }
                else
                {
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C1", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C2", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C3", "");
                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns.Add("C4", "");

                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("CourseWare Name: ", comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString(), "Status of Payment:", "");
                    global_connection.Open();
                    query_string = "SELECT [COURSEWARE INFO].[COURSEWARE ID] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                    SqlCeCommand command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1 = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1 = command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1.ExecuteReader();
                    if (dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("CourseWare ID:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1[0].ToString(), "", "");
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }

                    dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("", "", "", "");

                    query_string = "SELECT [REGID] FROM [TEMP DETAIL]";
                    global_connection.Open();
                    SqlCeCommand command_tempdetail_forlog = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_tempdetail_forlog = command_tempdetail_forlog.ExecuteReader();
                    if (dr_tempdetail_forlog.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("Payment Scheme:", "Installment", "Registeration ID", "TMSREGID" + dr_tempdetail_forlog[0].ToString());
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }

                    global_connection.Open();
                    query_string = "SELECT [COURSEWARE INFO].[COST], [COURSEWARE INFO].[DURATION] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                    SqlCeCommand command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_CMD2 = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2 = command_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_CMD2.ExecuteReader();
                    if (dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2.Read())
                    {
                        dataGridView_CW_NEW_REG_MOP_LOG_GRID.Rows.Add("Amount Due:", dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString(), "Amount To Pay Monthly:", Convert.ToString(Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString()) / Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[1].ToString())));
                        textBox_NEW_CW_REGISTER_DOWNPAYMENT_BOX.Text = Convert.ToString(Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[0].ToString()) / Convert.ToDecimal(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_DR2[1].ToString()));
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error in Reading!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }
                    global_connection.Close();    
                    
                }
                refresh_window();
            }
            catch (Exception dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_EXEC)
            {
                global_connection.Close();
                MessageBox.Show(dr_comboBox_NEW_CW_REGISTER_MOP_LIST_SelectedIndexChanged_1_EXEC.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comboBox_NEW_CW_REGISTER_STATUSOFPAYMENT_BOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_NEW_CW_REGISTER_STATUSOFPAYMENT_BOX.SelectedIndex == 0)
            {
                BUTTON_NEW_CW_REGISTER_ACTION.Enabled = false;
                dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value = comboBox_NEW_CW_REGISTER_STATUSOFPAYMENT_BOX.SelectedItem.ToString();
            }
            else
            {
                BUTTON_NEW_CW_REGISTER_ACTION.Enabled = true;
                dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[0].Cells[3].Value = comboBox_NEW_CW_REGISTER_STATUSOFPAYMENT_BOX.SelectedItem.ToString();
            }
        }

        private void BUTTON_NEW_CW_REGISTER_ACTION_Click(object sender, EventArgs e)
        {
            pbhndl.Enabled = true;
            pbhndl.Visible = true;
            pbhndl.progressBar_CRT.Value = 0;
            BUTTON_NEW_CW_REGISTER_ACTION_Click_INVOKE();
        }

        public void BUTTON_NEW_CW_REGISTER_ACTION_Click_INVOKE()
        {
            try
            {
                global_connection.Open();
                query_string = "INSERT INTO [PAY LOAD TABLE] ([STUDENT ID], [REGISTRATION ID], AMOUNT, DOP) VALUES (N'"+dataGridView_existingstudent_cw_entry_reg_GRID.Columns[0].DataGridView.Rows[0].Cells[0].Value.ToString()+"',N'"+dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString()+"',N'"+dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[4].Cells[3].Value.ToString()+"',CONVERT(DATETIME,'"+System.DateTime.Now.ToString()+"',102))";
                SqlCeCommand COMMAND_INSERTO_TO_PAYLOAD_NEW_REG_SD = new SqlCeCommand(query_string, global_connection);
                
                if (COMMAND_INSERTO_TO_PAYLOAD_NEW_REG_SD.ExecuteNonQuery()>0)
                {
                    pbhndl.progressBar_CRT.PerformStep();
                    global_connection.Close();
                    if (comboBox_NEW_CW_REGISTER_MOP_LIST.SelectedIndex == 0)
                    {
                        query_string = "INSERT INTO [TABLE SDID REGID]  ([STUDENT ID], [REGISTRATION ID], [COURSEWARE ID], [COURSEWARE NAME], TTP, TPD, MOP) VALUES (N'"+dataGridView_existingstudent_cw_entry_reg_GRID.Columns[0].DataGridView.Rows[0].Cells[0].Value.ToString()+"',N'"+dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString()+"',N'"+dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[1].DataGridView.Rows[1].Cells[1].Value.ToString()+"',N'"+comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString()+"',1,1,N'L')";
                        pbhndl.progressBar_CRT.PerformStep();
                    }
                    else if (comboBox_NEW_CW_REGISTER_MOP_LIST.SelectedIndex == 1)
                    {
                        query_string = "SELECT [COURSEWARE INFO].[SEMISTERS] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                        SqlCeCommand command_get_courseware_period = new SqlCeCommand(query_string, global_connection);
                        global_connection.Open();
                        pbhndl.progressBar_CRT.PerformStep();
                        SqlCeDataReader DR_GET_COURSEWARE_PERIOD = command_get_courseware_period.ExecuteReader();
                        if (DR_GET_COURSEWARE_PERIOD.Read())
                        {
                            query_string = "INSERT INTO [TABLE SDID REGID]  ([STUDENT ID], [REGISTRATION ID], [COURSEWARE ID], [COURSEWARE NAME], TTP, TPD, MOP) VALUES (N'" + dataGridView_existingstudent_cw_entry_reg_GRID.Columns[0].DataGridView.Rows[0].Cells[0].Value.ToString() + "',N'" + dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString() + "',N'" + dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[1].DataGridView.Rows[1].Cells[1].Value.ToString() + "',N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "'," + DR_GET_COURSEWARE_PERIOD[0].ToString() + ",1, N'S')";
                            pbhndl.progressBar_CRT.PerformStep();
                            global_connection.Close();
                        }
                    }
                    else
                    {
                        pbhndl.progressBar_CRT.PerformStep();
                        query_string = "SELECT [COURSEWARE INFO].[DURATION] FROM [COURSEWARE INFO] WHERE ([COURSEWARE NAME]=N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "')";
                        SqlCeCommand command_get_courseware_period = new SqlCeCommand(query_string, global_connection);
                        global_connection.Open();
                        pbhndl.progressBar_CRT.PerformStep();
                        SqlCeDataReader DR_GET_COURSEWARE_PERIOD = command_get_courseware_period.ExecuteReader();
                        if (DR_GET_COURSEWARE_PERIOD.Read())
                        {
                            global_connection.Close();
                            query_string = query_string = "INSERT INTO [TABLE SDID REGID]  ([STUDENT ID], [REGISTRATION ID], [COURSEWARE ID], [COURSEWARE NAME], TTP, TPD, MOP) VALUES (N'" + dataGridView_existingstudent_cw_entry_reg_GRID.Columns[0].DataGridView.Rows[0].Cells[0].Value.ToString() + "',N'" + dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[3].DataGridView.Rows[3].Cells[3].Value.ToString() + "',N'" + dataGridView_CW_NEW_REG_MOP_LOG_GRID.Columns[1].DataGridView.Rows[1].Cells[1].Value.ToString() + "',N'" + comboBox_CW_NEW_CW_REGISTER_CWLIST.SelectedItem.ToString() + "'," +DR_GET_COURSEWARE_PERIOD[0].ToString()+ ",1, N'I')";
                            pbhndl.progressBar_CRT.PerformStep();
                        }
                    }
                    pbhndl.progressBar_CRT.PerformStep();
                    global_connection.Open();
                    SqlCeCommand COMMAND_TABLE_SDID_REGID = new SqlCeCommand(query_string, global_connection);
                    if (COMMAND_TABLE_SDID_REGID.ExecuteNonQuery() > 0)
                    {
                        pbhndl.progressBar_CRT.PerformStep();
                        global_connection.Close();
                        query_string = "SELECT [TEMP DETAIL].[REGID] FROM [TEMP DETAIL]";
                        SqlCeCommand COMMAND_REGID_INCREMENT = new SqlCeCommand(query_string, global_connection);
                        global_connection.Open();
                        pbhndl.progressBar_CRT.PerformStep();
                        SqlCeDataReader dr_regid_increment = COMMAND_REGID_INCREMENT.ExecuteReader();
                        if (dr_regid_increment.Read())
                        {
                            pbhndl.progressBar_CRT.PerformStep();
                            i=Convert.ToInt32(dr_regid_increment[0].ToString());
                            i++;
                            pbhndl.progressBar_CRT.PerformStep();
                            query_string = "UPDATE [TEMP DETAIL] SET REGID = "+Convert.ToString(i);
                            global_connection.Close();
                            pbhndl.progressBar_CRT.PerformStep();
                            SqlCeCommand cmd_insert_computed_calue_regid = new SqlCeCommand(query_string, global_connection);
                            global_connection.Open();
                            pbhndl.progressBar_CRT.PerformStep();
                            if (cmd_insert_computed_calue_regid.ExecuteNonQuery() > 0)
                            {
                                global_connection.Close();
                                pbhndl.progressBar_CRT.PerformStep();
                                MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_MSG_SUCCESS.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                pbhndl.Enabled = false;
                                pbhndl.Visible = false;
                                Application.Restart();
                            }
                        }                        
                    }
                }
                else
                {
                    MessageBox.Show("Error in Inserting Values!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    global_connection.Close();
                }

            }
            catch (Exception BUTTON_NEW_CW_REGISTER_ACTION_Click_INVOKE_exe)
            {
                MessageBox.Show(BUTTON_NEW_CW_REGISTER_ACTION_Click_INVOKE_exe.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                global_connection.Close();                
            }
        }

        private void comboBox_WBUI_PE_STATUS_PAY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_WBUI_PE_STATUS_PAY.SelectedIndex == 1)
            {
                button_WBUI_PE_PAYBUTTON.Enabled = true;
            }
            else
            {
                button_WBUI_PE_PAYBUTTON.Enabled = false;
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        public void PAYMENT_EVENT_INVOKE_PROCEDURE()
        {
            try
            {
                dataGridView_wbui_pe_top.Rows.Clear();
                dataGridView_wbui_pe_top.Columns.Clear();
                if (Convert.ToInt32(dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[4].Value.ToString()) == Convert.ToInt32(dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[5].Value.ToString()))
                {
                    MessageBox.Show("Account Closed of : " + dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[0].Value.ToString() + ". \nCourseWare Package Fee Paid!", tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl_wb_ui.SelectedIndex = 0;
                }
                else
                {
                    tabControl_wb_ui.SelectedIndex = 4;
                    query_string = "SELECT COST, SEMISTERS, DURATION FROM [COURSEWARE INFO] WHERE ([COURSEWARE ID]=N'" + dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[2].Value.ToString() + "')";
                    dataGridView_wbui_pe_top.Columns.Add("C1", "");
                    dataGridView_wbui_pe_top.Columns.Add("C2", "");
                    dataGridView_wbui_pe_top.Columns.Add("C3", "");
                    dataGridView_wbui_pe_top.Columns.Add("C4", "");

                    dataGridView_wbui_pe_top.Rows.Add("Student ID:", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[0].Value.ToString(), "Registration ID:", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[1].Value.ToString());
                    dataGridView_wbui_pe_top.Rows.Add("CourseWare ID:", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[2].Value.ToString(), "CourseWare Name:", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[3].Value.ToString());
                    dataGridView_wbui_pe_top.Rows.Add("Times To Pay", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[4].Value.ToString(), "Times Paid:", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[5].Value.ToString());
                    dataGridView_wbui_pe_top.Rows.Add("", "", "", "");
                    dataGridView_wbui_pe_top.Rows.Add("Schema of payment:", dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[6].Value.ToString(), "", "");
                    dataGridView_wbui_pe_top.Rows.Add("", "", "", "");

                    global_connection.Open();
                    SqlCeCommand COMMAND_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE = new SqlCeCommand(query_string, global_connection);
                    SqlCeDataReader DR_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE = COMMAND_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE.ExecuteReader();
                    if (DR_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE.Read())
                    {
                        if (dataGridView_wbui_studentdetail_bottom.Columns[dataGridView_wbui_studentdetail_bottom.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_studentdetail_bottom.CurrentCell.RowIndex].Cells[6].Value.ToString() == "S")
                        {
                            maskedTextBox_WBUI_PE_AMOUNT.Text = Convert.ToString(Convert.ToInt32(DR_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE[0].ToString()) / Convert.ToInt32(DR_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE[1].ToString()));
                            global_connection.Close();
                        }
                        else
                        {
                            maskedTextBox_WBUI_PE_AMOUNT.Text = Convert.ToString(Convert.ToInt32(DR_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE[0].ToString()) / Convert.ToInt32(DR_PAYMENT_EVENT_INVOKE_PROCEDURE_COST_COMPUTE[2].ToString()));
                            global_connection.Close();
                        }
                    }
                    global_connection.Close();
                }
            }
            catch (Exception PAYMENT_EVENT_INVOKE_PROCEDURE_exec)
            {
                MessageBox.Show(PAYMENT_EVENT_INVOKE_PROCEDURE_exec.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                global_connection.Close();
            }
        }

        private void button_WBUI_PE_PAYBUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                pbhndl.Enabled = true;
                pbhndl.Visible = true;
                pbhndl.progressBar_CRT.Value = 0;
                if (checkBox_fine_consideration_check.Checked == true && maskedTextBox_WBUI_FINEAMOUNT.Text != " ")
                {
                    pbhndl.progressBar_CRT.PerformStep();
                    query_string = "INSERT INTO [PAY LOAD TABLE] ([STUDENT ID], [REGISTRATION ID], DOP, AMOUNT, FINE) VALUES (N'" + dataGridView_wbui_pe_top.Columns[1].DataGridView.Rows[0].Cells[1].Value.ToString() + "',N'" + dataGridView_wbui_pe_top.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',CONVERT(DATETIME,'" + System.DateTime.Now.ToString() + "',102)," + maskedTextBox_WBUI_PE_AMOUNT.Text + "," + maskedTextBox_WBUI_FINEAMOUNT.Text + ")";
                }
                else
                {
                    pbhndl.progressBar_CRT.PerformStep();
                    query_string = "INSERT INTO [PAY LOAD TABLE] ([STUDENT ID], [REGISTRATION ID], DOP, AMOUNT) VALUES (N'" + dataGridView_wbui_pe_top.Columns[1].DataGridView.Rows[0].Cells[1].Value.ToString() + "',N'" + dataGridView_wbui_pe_top.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "',CONVERT(DATETIME,'" + System.DateTime.Now.ToString() + "',102)," + maskedTextBox_WBUI_PE_AMOUNT.Text + ")";
                }
                pbhndl.progressBar_CRT.PerformStep();
                global_connection.Open();
                pbhndl.progressBar_CRT.PerformStep();
                SqlCeCommand command_button_WBUI_PE_PAYBUTTON_Click_A = new SqlCeCommand(query_string, global_connection);
                pbhndl.progressBar_CRT.PerformStep();
                if (command_button_WBUI_PE_PAYBUTTON_Click_A.ExecuteNonQuery() > 0)
                {
                    pbhndl.progressBar_CRT.PerformStep();
                    global_connection.Close();
                    pbhndl.progressBar_CRT.PerformStep();
                    query_string = "SELECT [TPD] FROM [TABLE SDID REGID] WHERE ([REGISTRATION ID]=N'"+dataGridView_wbui_pe_top.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString()+"')";
                    global_connection.Open();
                    SqlCeCommand command_button_WBUI_PE_PAYBUTTON_Click_B = new SqlCeCommand(query_string, global_connection);
                    pbhndl.progressBar_CRT.PerformStep();
                    SqlCeDataReader dr_button_WBUI_PE_PAYBUTTON_Click_maupulate_tpd = command_button_WBUI_PE_PAYBUTTON_Click_B.ExecuteReader();
                    if (dr_button_WBUI_PE_PAYBUTTON_Click_maupulate_tpd.Read())
                    {                        
                        pbhndl.progressBar_CRT.PerformStep();
                        i = Convert.ToInt32(dr_button_WBUI_PE_PAYBUTTON_Click_maupulate_tpd[0].ToString());
                        i++;
                        query_string = "UPDATE [TABLE SDID REGID] SET [TPD]=" + Convert.ToString(i) + " WHERE ([REGISTRATION ID]=N'" + dataGridView_wbui_pe_top.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString() + "')";
                        global_connection.Close();
                        SqlCeCommand COMMAND_command_button_WBUI_PE_PAYBUTTON_Click_C = new SqlCeCommand(query_string, global_connection);
                        global_connection.Open();
                        pbhndl.progressBar_CRT.PerformStep();
                        if (COMMAND_command_button_WBUI_PE_PAYBUTTON_Click_C.ExecuteNonQuery() > 0)
                        {
                            pbhndl.progressBar_CRT.PerformStep();
                            global_connection.Close();
                            MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_MSG_SUCCESS.ToString() + "\n" + tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Restart();
                        }
                        else
                        {
                            MessageBox.Show("Data Updation Process Aborted due to Error in TPD Issue during Updating,!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            global_connection.Close();
                        }
                        global_connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Data Updation Process Aborted due to Error in TPD Issue,!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        global_connection.Close();
                    }
                    global_connection.Close();
                }
                else
                {
                    MessageBox.Show("Data Updation Process Aborted due to Error!" + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    global_connection.Close();
                }

            }
            catch (Exception button_WBUI_PE_PAYBUTTON_Click_exe)
            {
                MessageBox.Show(button_WBUI_PE_PAYBUTTON_Click_exe.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
                global_connection.Close();
            }
        }

        private void insertDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                job_control = 1;
                button_ENABLEEDITING_PROC.Enabled = true;

                dataGridView_WBUI_TOP.DataSource = null;
                dataGridView_WBUI_MIDDLE.DataSource = null;
                dataGridView_WBUI_LEFT.DataSource = null;
                dataGridView_WBUI_TOP.Rows.Clear();
                dataGridView_WBUI_LEFT.Rows.Clear();
                dataGridView_WBUI_MIDDLE.Rows.Clear();

                dataGridView_WBUI_TOP.Columns.Clear();
                dataGridView_WBUI_LEFT.Columns.Clear();
                dataGridView_WBUI_MIDDLE.Columns.Clear();


                query_string = "SELECT * FROM [COURSEWARE INFO]";
                SqlCeCommand command_insertDataToolStripMenuItem_Click_A = new SqlCeCommand(query_string, global_connection);
                global_connection.Open();
                SqlCeDataAdapter dataAdapter_insertDataToolStripMenuItem_Click_A = new SqlCeDataAdapter(command_insertDataToolStripMenuItem_Click_A);
                DataTable dt_insertDataToolStripMenuItem_Click_A = new DataTable();
                dataAdapter_insertDataToolStripMenuItem_Click_A.Fill(dt_insertDataToolStripMenuItem_Click_A);
                dataGridView_WBUI_MIDDLE.DataSource = dt_insertDataToolStripMenuItem_Click_A;
                global_connection.Close();

                dataGridView_WBUI_TOP.Columns.Add("C1", "COURSEWARE NAME");
                dataGridView_WBUI_TOP.Columns.Add("C2", "COURSEWARE ID");
                dataGridView_WBUI_TOP.Columns.Add("C3", "SEMISTERS");
                dataGridView_WBUI_TOP.Columns.Add("C4", "COST");
                dataGridView_WBUI_TOP.Columns.Add("C5", "DURATION");
                dataGridView_WBUI_TOP.Columns.Add("C6", "DESCRIPTION");
                dataGridView_WBUI_TOP.Columns.Add("C7", "NOS OF KITS");

                dataGridView_WBUI_TOP.Columns[0].ReadOnly = false;
                dataGridView_WBUI_TOP.Columns[1].ReadOnly = true;
                dataGridView_WBUI_TOP.Columns[2].ReadOnly = false;
                dataGridView_WBUI_TOP.Columns[3].ReadOnly = false;
                dataGridView_WBUI_TOP.Columns[4].ReadOnly = false;
                dataGridView_WBUI_TOP.Columns[5].ReadOnly = false;
                

                query_string = "SELECT [CWID] FROM [TEMP DETAIL]";
                SqlCeCommand command_insertDataToolStripMenuItem_Click_B = new SqlCeCommand(query_string, global_connection);
                global_connection.Open();
                SqlCeDataReader dr_insertDataToolStripMenuItem_Click_B = command_insertDataToolStripMenuItem_Click_B.ExecuteReader();
                if (dr_insertDataToolStripMenuItem_Click_B.Read())
                {
                    COURSEWARE_STRING_MANUPULATE = dr_insertDataToolStripMenuItem_Click_B[0].ToString();
                    dataGridView_WBUI_TOP.Rows.Add("", "TMSCWID"+COURSEWARE_STRING_MANUPULATE, "", "", "", "", "");
                    global_connection.Close();
                }
                else 
                {
                    global_connection.Close();
                    MessageBox.Show("Unknown Error Occured...\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                tabControl_wb_ui.SelectedIndex = 2;
            }
            catch (Exception insertDataToolStripMenuItem_Click_exe)
            {
                global_connection.Close();
                MessageBox.Show("Select any cell then proceed, or \n"+insertDataToolStripMenuItem_Click_exe.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void changeThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tms_g_L.tms_g_l_class.DwmIsCompositionEnabled())
            {
                if (change_theme == true)
                {
                    wbui_margin = new tms_g_L.tms_g_l_class.MARGINS();
                    wbui_margin.Top = 8;
                    wbui_margin.Bottom = 8;
                    wbui_margin.Left = 8;
                    wbui_margin.Right = 8;
                    tms_g_L.tms_g_l_class.DwmExtendFrameIntoClientArea(this.Handle, ref wbui_margin);
                    change_theme = false;
                    refresh_window();
                }
                else if (change_theme == false)
                {
                    wbui_margin = new tms_g_L.tms_g_l_class.MARGINS();
                    wbui_margin.Top = this.Height;
                    wbui_margin.Bottom = this.Height;
                    wbui_margin.Left = this.Width;
                    wbui_margin.Right = this.Width;
                    tms_g_L.tms_g_l_class.DwmExtendFrameIntoClientArea(this.Handle, ref wbui_margin);
                    change_theme = true;
                    refresh_window();
                }

            }
        }

        private void button_ENABLEEDITING_PROC_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlgres = MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_CONFIRM_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_CONFIRM_REG_MSG_CAPTION.ToString(),MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                if (dlgres == System.Windows.Forms.DialogResult.No)
                {
                    refresh_window();
                }
                else if (dlgres == System.Windows.Forms.DialogResult.Yes)
                {
                    button_MDIFY_POC.Enabled = true;
                    button_ENABLEEDITING_PROC.Enabled = false;
                    refresh_window();
                }

            }
            catch (Exception button_ENABLEEDITING_PROC_Click_EXE)
            {
                MessageBox.Show(button_ENABLEEDITING_PROC_Click_EXE.Message+"\n"+tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button_MDIFY_POC_Click(object sender, EventArgs e)
        {
            try
            {
                if (job_control == 1)   //insert new courseware details into Database.
                {
                    pbhndl.Enabled = true;
                    pbhndl.Visible = true;
                    pbhndl.progressBar_CRT.Value = 0;
                    pbhndl.progressBar_CRT.PerformStep();
                    query_string = "INSERT INTO [COURSEWARE INFO] ([COURSEWARE NAME],[COURSEWARE ID], SEMISTERS, COST, DURATION, DESCRIPTION, KITS) VALUES (N'"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[0].Value.ToString()+"', N'"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[1].Value.ToString()+"',"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[2].Value.ToString()+" ,"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[3].Value.ToString()+" ,"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[4].Value.ToString()+" , N'"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[5].Value.ToString()+"',"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[6].Value.ToString()+" )";
                    pbhndl.progressBar_CRT.PerformStep();
                    SqlCeCommand command_button_MDIFY_POC_Click_jobA = new SqlCeCommand(query_string, global_connection);
                    pbhndl.progressBar_CRT.PerformStep();
                    global_connection.Open();
                    pbhndl.progressBar_CRT.PerformStep();
                    if (command_button_MDIFY_POC_Click_jobA.ExecuteNonQuery() > 0)
                    {
                        pbhndl.progressBar_CRT.PerformStep();
                        global_connection.Close();
                        query_string = "SELECT [CWID] FROM [TEMP DETAIL]";
                        SqlCeCommand command_button_MDIFY_POC_Clic_B = new SqlCeCommand(query_string, global_connection);
                        global_connection.Open();
                        SqlCeDataReader dr_button_MDIFY_POC_Click_B = command_button_MDIFY_POC_Clic_B.ExecuteReader();
                        pbhndl.progressBar_CRT.PerformStep();
                        if (dr_button_MDIFY_POC_Click_B.Read())
                        {
                            pbhndl.progressBar_CRT.PerformStep();
                            i = Convert.ToInt32(dr_button_MDIFY_POC_Click_B[0].ToString());
                            pbhndl.progressBar_CRT.PerformStep();
                            i++;
                            pbhndl.progressBar_CRT.PerformStep();
                            query_string = "UPDATE [TEMP DETAIL] SET [CWID] = "+ i.ToString();
                            SqlCeCommand command_button_MDIFY_POC_Click_C = new SqlCeCommand(query_string, global_connection);
                            pbhndl.progressBar_CRT.PerformStep();
                            if (command_button_MDIFY_POC_Click_C.ExecuteNonQuery() > 0)
                            {
                                pbhndl.progressBar_CRT.PerformStep();
                                global_connection.Close();
                                pbhndl.progressBar_CRT.PerformStep();
                                pbhndl.Visible = false;
                                pbhndl.Enabled = false;
                                MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_MSG_SUCCESS.ToString() + "\n This Application will Restart to Reflect the updated Values!", tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Application.Restart();
                            }
                            else
                            {
                                global_connection.Close();
                                MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK,MessageBoxIcon.Error);
                                refresh_window();
                            }
                        }                        
                    }
                    else
                    {
                        pbhndl.progressBar_CRT.PerformStep();
                        global_connection.Close();
                        pbhndl.progressBar_CRT.PerformStep();
                        MessageBox.Show("Data Insertion Failed!\n Check the data fields or Fill all the field correctly,\n"+ tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(),tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                if (job_control == 2)
                {
                    query_string = "UPDATE [STUDENT DETAIL TABLE] SET [STUDENT NAME]= N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[0].Cells[1].Value.ToString()+"', [ADDRESS]=N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[1].Cells[1].Value.ToString()+"', [DISTRICT]=N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[2].Cells[1].Value.ToString()+"', [PROVINCE]=N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[3].Cells[1].Value.ToString()+"', [NATIONALITY]=N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[4].Cells[1].Value.ToString()+"', [CONTAT NOS]=N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[5].Cells[1].Value.ToString()+"', [GENDER]=N'"+dataGridView_WBUI_LEFT.Columns[1].DataGridView.Rows[6].Cells[1].Value.ToString()+"' WHERE([STUDENT ID]= N'"+dataGridView_WBUI_TOP.Columns[0].DataGridView.Rows[0].Cells[0].Value.ToString()+"')";
                    SqlCeCommand command_button_MDIFY_POC_Click_2A = new SqlCeCommand(query_string, global_connection);
                    global_connection.Open();
                    if (command_button_MDIFY_POC_Click_2A.ExecuteNonQuery() > 0)
                    {
                        global_connection.Close();
                        MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_MSG_SUCCESS.ToString() + "\n This Application will Restart to Reflect the updated Values!", tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                    }
                }
            }
            catch (Exception button_MDIFY_POC_Click_EXEC)
            {
                pbhndl.Visible = false;
                pbhndl.Enabled = false;
                MessageBox.Show(button_MDIFY_POC_Click_EXEC.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Error);
                global_connection.Close();
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl_wb_ui.SelectedIndex = 2;

                dataGridView_WBUI_TOP.DataSource = null;
                dataGridView_WBUI_MIDDLE.DataSource = null;
                dataGridView_WBUI_LEFT.DataSource = null;
                dataGridView_WBUI_TOP.Rows.Clear();
                dataGridView_WBUI_LEFT.Rows.Clear();
                dataGridView_WBUI_MIDDLE.Rows.Clear();
                dataGridView_WBUI_TOP.Columns.Clear();
                dataGridView_WBUI_LEFT.Columns.Clear();
                dataGridView_WBUI_MIDDLE.Columns.Clear();
                
                
                query_string = "SELECT * FROM [STUDENT DETAIL TABLE] WHERE ([STUDENT ID] = N'"+dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString()+"')";
                SqlCeCommand command_viewToolStripMenuItem_Click_A = new SqlCeCommand(query_string, global_connection);
                global_connection.Open();
                SqlCeDataAdapter dataAdapter_viewToolStripMenuItem_Click_A = new SqlCeDataAdapter(command_viewToolStripMenuItem_Click_A);
                DataTable dt_viewToolStripMenuItem_Click_A = new DataTable();
                dataAdapter_viewToolStripMenuItem_Click_A.Fill(dt_viewToolStripMenuItem_Click_A);
                dataGridView_WBUI_TOP.DataSource = dt_viewToolStripMenuItem_Click_A;
                global_connection.Close();

                for (i = 0; i < dataGridView_WBUI_TOP.Columns.Count; i++)
                {
                    dataGridView_WBUI_TOP.Columns[i].ReadOnly = true;
                }

                query_string = "SELECT * FROM [TABLE SDID REGID] WHERE ([STUDENT ID] = N'"+dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString()+"')";
                SqlCeCommand COMMAND_viewToolStripMenuItem_Click_B = new SqlCeCommand(query_string, global_connection);
                global_connection.Open();
                SqlCeDataAdapter dataAdapter_viewToolStripMenuItem_Click_B = new SqlCeDataAdapter(COMMAND_viewToolStripMenuItem_Click_B);
                DataTable dt_viewToolStripMenuItem_Click_B = new DataTable();
                dataAdapter_viewToolStripMenuItem_Click_B.Fill(dt_viewToolStripMenuItem_Click_B);
                dataGridView_WBUI_LEFT.DataSource = dt_viewToolStripMenuItem_Click_B;
                global_connection.Close();

                query_string = "SELECT * FROM [PAY LOAD TABLE] WHERE ([STUDENT ID]= N'"+dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString()+"')";
                global_connection.Open();
                SqlCeCommand command_viewToolStripMenuItem_Click_C = new SqlCeCommand(query_string, global_connection);
                SqlCeDataAdapter dataAdapter_viewToolStripMenuItem_Click_C = new SqlCeDataAdapter(command_viewToolStripMenuItem_Click_C);
                DataTable dt_viewToolStripMenuItem_Click_C = new DataTable();
                dataAdapter_viewToolStripMenuItem_Click_C.Fill(dt_viewToolStripMenuItem_Click_C);
                dataGridView_WBUI_MIDDLE.DataSource = dt_viewToolStripMenuItem_Click_C;
                global_connection.Close();
            }
            catch (Exception viewToolStripMenuItem_Click_exec)
            {
                MessageBox.Show(viewToolStripMenuItem_Click_exec.Message + "\n" + tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString(), tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                global_connection.Close();                
            }
        }

        private void modifyDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                job_control = 2;
                button_ENABLEEDITING_PROC.Enabled = true;
                if (tabControl_wb_ui.SelectedIndex == 1)
                {
                    tabControl_wb_ui.SelectedIndex = 2;

                    dataGridView_WBUI_TOP.DataSource = null;
                    dataGridView_WBUI_LEFT.DataSource = null;
                    dataGridView_WBUI_MIDDLE.DataSource = null;

                    dataGridView_WBUI_TOP.Rows.Clear();
                    dataGridView_WBUI_LEFT.Rows.Clear();
                    dataGridView_WBUI_MIDDLE.Rows.Clear();
                    dataGridView_WBUI_TOP.Columns.Clear();
                    dataGridView_WBUI_LEFT.Columns.Clear();
                    dataGridView_WBUI_MIDDLE.Columns.Clear();

                    query_string = "SELECT * FROM [STUDENT DETAIL TABLE] WHERE [STUDENT ID] = N'"+dataGridView_wbui_student_detail.Columns[dataGridView_wbui_student_detail.CurrentCell.ColumnIndex].DataGridView.Rows[dataGridView_wbui_student_detail.CurrentCell.RowIndex].Cells[0].Value.ToString()+"'";
                    SqlCeCommand command_modifyDataToolStripMenuItem1_Click_A = new SqlCeCommand(query_string, global_connection);
                    global_connection.Open();
                    SqlCeDataAdapter dataAdapter_modifyDataToolStripMenuItem1_Click_A = new SqlCeDataAdapter(command_modifyDataToolStripMenuItem1_Click_A);
                    DataTable dt_modifyDataToolStripMenuItem1_Click_A = new DataTable();
                    dataAdapter_modifyDataToolStripMenuItem1_Click_A.Fill(dt_modifyDataToolStripMenuItem1_Click_A);
                    dataGridView_WBUI_TOP.DataSource = dt_modifyDataToolStripMenuItem1_Click_A;
                    global_connection.Close();

                    for (i = 0; i<dataGridView_WBUI_TOP.Columns.Count; i++)
                    {
                        dataGridView_WBUI_TOP.Columns[i].ReadOnly = true;
                    }

                    dataGridView_WBUI_LEFT.Columns.Add("C1", " ");
                    dataGridView_WBUI_LEFT.Columns.Add("C2", " ");

                    dataGridView_WBUI_LEFT.Columns[0].ReadOnly = true;
                    dataGridView_WBUI_LEFT.Columns[1].ReadOnly = false;

                    dataGridView_WBUI_LEFT.Rows.Add("Student Name :", dataGridView_WBUI_TOP.Columns[1].DataGridView.Rows[0].Cells[1].Value.ToString());
                    dataGridView_WBUI_LEFT.Rows.Add("Address :", dataGridView_WBUI_TOP.Columns[2].DataGridView.Rows[0].Cells[2].Value.ToString());
                    dataGridView_WBUI_LEFT.Rows.Add("District :", dataGridView_WBUI_TOP.Columns[3].DataGridView.Rows[0].Cells[3].Value.ToString());
                    dataGridView_WBUI_LEFT.Rows.Add("Province :", dataGridView_WBUI_TOP.Columns[4].DataGridView.Rows[0].Cells[4].Value.ToString());
                    dataGridView_WBUI_LEFT.Rows.Add("Nationality :", dataGridView_WBUI_TOP.Columns[5].DataGridView.Rows[0].Cells[5].Value.ToString());
                    dataGridView_WBUI_LEFT.Rows.Add("Contact Nos:", dataGridView_WBUI_TOP.Columns[6].DataGridView.Rows[0].Cells[6].Value.ToString());
                    dataGridView_WBUI_LEFT.Rows.Add("Gender : (M/F)", dataGridView_WBUI_TOP.Columns[8].DataGridView.Rows[0].Cells[8].Value.ToString());
                }
            }
            catch (Exception modifyDataToolStripMenuItem1_Click_Exc)
            {
                global_connection.Close();
                MessageBox.Show(tms_wb_ui.Properties.Resources.STRING_UNHANDLED_MSG.ToString() + "\n" + modifyDataToolStripMenuItem1_Click_Exc.Message, tms_wb_ui.Properties.Resources.STRING_MSG_CAPTION_ERROR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                refresh_window();
            }
        }    
    }    
}
   

