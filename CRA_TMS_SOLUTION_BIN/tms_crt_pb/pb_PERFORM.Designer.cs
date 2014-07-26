namespace tms_crt_pb
{
    partial class pb_PERFORM
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
            this.progressBar_CRT = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBar_CRT
            // 
            this.progressBar_CRT.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar_CRT.Location = new System.Drawing.Point(13, 22);
            this.progressBar_CRT.Name = "progressBar_CRT";
            this.progressBar_CRT.Size = new System.Drawing.Size(425, 25);
            this.progressBar_CRT.Step = 5;
            this.progressBar_CRT.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_CRT.TabIndex = 0;
            // 
            // pb_PERFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::tms_crt_pb.Properties.Resources.img2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(450, 68);
            this.Controls.Add(this.progressBar_CRT);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Info;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "pb_PERFORM";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.pb_PERFORM_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBar_CRT;
    }
}