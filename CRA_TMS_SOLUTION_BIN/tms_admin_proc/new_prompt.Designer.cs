namespace tms_admin_proc
{
    partial class new_prompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox_role_list = new System.Windows.Forms.ComboBox();
            this.maskedTextBox_password_admin = new System.Windows.Forms.MaskedTextBox();
            this.button_RolePlay = new System.Windows.Forms.Button();
            this.timer_from_admin = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // comboBox_role_list
            // 
            this.comboBox_role_list.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.comboBox_role_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_role_list.FormattingEnabled = true;
            this.comboBox_role_list.ItemHeight = 16;
            this.comboBox_role_list.Items.AddRange(new object[] {
            "Non-Administrator Role",
            "Administrator Role"});
            this.comboBox_role_list.Location = new System.Drawing.Point(24, 13);
            this.comboBox_role_list.Name = "comboBox_role_list";
            this.comboBox_role_list.Size = new System.Drawing.Size(362, 24);
            this.comboBox_role_list.TabIndex = 0;
            this.comboBox_role_list.SelectedIndexChanged += new System.EventHandler(this.comboBox_role_list_SelectedIndexChanged);
            // 
            // maskedTextBox_password_admin
            // 
            this.maskedTextBox_password_admin.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.maskedTextBox_password_admin.Enabled = false;
            this.maskedTextBox_password_admin.Location = new System.Drawing.Point(24, 56);
            this.maskedTextBox_password_admin.Mask = "AAA_AAA_AAAAA";
            this.maskedTextBox_password_admin.Name = "maskedTextBox_password_admin";
            this.maskedTextBox_password_admin.PasswordChar = '*';
            this.maskedTextBox_password_admin.PromptChar = '-';
            this.maskedTextBox_password_admin.Size = new System.Drawing.Size(362, 22);
            this.maskedTextBox_password_admin.TabIndex = 1;
            // 
            // button_RolePlay
            // 
            this.button_RolePlay.BackgroundImage = global::tms_admin_proc.Properties.Resources.img2;
            this.button_RolePlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_RolePlay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_RolePlay.Location = new System.Drawing.Point(71, 85);
            this.button_RolePlay.Name = "button_RolePlay";
            this.button_RolePlay.Size = new System.Drawing.Size(253, 32);
            this.button_RolePlay.TabIndex = 2;
            this.button_RolePlay.Text = "Play Role";
            this.button_RolePlay.UseVisualStyleBackColor = true;
            this.button_RolePlay.Click += new System.EventHandler(this.button_RolePlay_Click);
            // 
            // timer_from_admin
            // 
            this.timer_from_admin.Interval = 50;
            this.timer_from_admin.Tick += new System.EventHandler(this.timer_from_admin_Tick);
            // 
            // new_prompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::tms_admin_proc.Properties.Resources.img2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(422, 129);
            this.Controls.Add(this.button_RolePlay);
            this.Controls.Add(this.maskedTextBox_password_admin);
            this.Controls.Add(this.comboBox_role_list);
            this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Info;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "new_prompt";
            this.Padding = new System.Windows.Forms.Padding(500);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TinyManagementSystem: User Role";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.new_prompt_FormClosed);
            this.Load += new System.EventHandler(this.new_prompt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBox_role_list;
        public System.Windows.Forms.MaskedTextBox maskedTextBox_password_admin;
        private System.Windows.Forms.Button button_RolePlay;
        public System.Windows.Forms.Timer timer_from_admin;

    }
}