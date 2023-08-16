namespace Compiler
{
    partial class buildLogger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disables The Close [X] Button In The Titlebar
        /// </summary>
        private const int CP_DISABLE_CLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CP_DISABLE_CLOSE_BUTTON;
                return cp;
            }
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(buildLogger));
            this.buildLogger_menuStrip = new System.Windows.Forms.MenuStrip();
            this.buildLogger_menuStrip_button_saveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.buildLogger_menuStrip_button_printLog = new System.Windows.Forms.ToolStripMenuItem();
            this.buildLogger_pictureBox_backDrop = new System.Windows.Forms.PictureBox();
            this.buildLogger_textBox_logDisplay = new System.Windows.Forms.TextBox();
            this.buildLogger_menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildLogger_pictureBox_backDrop)).BeginInit();
            this.SuspendLayout();
            // 
            // buildLogger_menuStrip
            // 
            this.buildLogger_menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.buildLogger_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildLogger_menuStrip_button_saveLog,
            this.buildLogger_menuStrip_button_printLog});
            this.buildLogger_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.buildLogger_menuStrip.Name = "buildLogger_menuStrip";
            this.buildLogger_menuStrip.Size = new System.Drawing.Size(413, 32);
            this.buildLogger_menuStrip.TabIndex = 0;
            this.buildLogger_menuStrip.Text = "menuStrip1";
            // 
            // buildLogger_menuStrip_button_saveLog
            // 
            this.buildLogger_menuStrip_button_saveLog.Image = ((System.Drawing.Image)(resources.GetObject("buildLogger_menuStrip_button_saveLog.Image")));
            this.buildLogger_menuStrip_button_saveLog.Margin = new System.Windows.Forms.Padding(313, 0, 0, 0);
            this.buildLogger_menuStrip_button_saveLog.Name = "buildLogger_menuStrip_button_saveLog";
            this.buildLogger_menuStrip_button_saveLog.Size = new System.Drawing.Size(36, 28);
            this.buildLogger_menuStrip_button_saveLog.Click += new System.EventHandler(this.buildLogger_menuStrip_button_saveLog_Click);
            // 
            // buildLogger_menuStrip_button_printLog
            // 
            this.buildLogger_menuStrip_button_printLog.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buildLogger_menuStrip_button_printLog.Image = ((System.Drawing.Image)(resources.GetObject("buildLogger_menuStrip_button_printLog.Image")));
            this.buildLogger_menuStrip_button_printLog.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.buildLogger_menuStrip_button_printLog.Name = "buildLogger_menuStrip_button_printLog";
            this.buildLogger_menuStrip_button_printLog.Size = new System.Drawing.Size(36, 28);
            this.buildLogger_menuStrip_button_printLog.Click += new System.EventHandler(this.buildLogger_menuStrip_button_printLog_Click);
            // 
            // buildLogger_pictureBox_backDrop
            // 
            this.buildLogger_pictureBox_backDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildLogger_pictureBox_backDrop.Image = ((System.Drawing.Image)(resources.GetObject("buildLogger_pictureBox_backDrop.Image")));
            this.buildLogger_pictureBox_backDrop.Location = new System.Drawing.Point(0, 32);
            this.buildLogger_pictureBox_backDrop.Name = "buildLogger_pictureBox_backDrop";
            this.buildLogger_pictureBox_backDrop.Size = new System.Drawing.Size(413, 335);
            this.buildLogger_pictureBox_backDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buildLogger_pictureBox_backDrop.TabIndex = 1;
            this.buildLogger_pictureBox_backDrop.TabStop = false;
            // 
            // buildLogger_textBox_logDisplay
            // 
            this.buildLogger_textBox_logDisplay.Location = new System.Drawing.Point(10, 41);
            this.buildLogger_textBox_logDisplay.Multiline = true;
            this.buildLogger_textBox_logDisplay.Name = "buildLogger_textBox_logDisplay";
            this.buildLogger_textBox_logDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.buildLogger_textBox_logDisplay.Size = new System.Drawing.Size(393, 319);
            this.buildLogger_textBox_logDisplay.TabIndex = 2;
            // 
            // buildLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 367);
            this.Controls.Add(this.buildLogger_textBox_logDisplay);
            this.Controls.Add(this.buildLogger_pictureBox_backDrop);
            this.Controls.Add(this.buildLogger_menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.buildLogger_menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "buildLogger";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapCreator: Logger";
            this.Load += new System.EventHandler(this.buildLogger_Load);
            this.buildLogger_menuStrip.ResumeLayout(false);
            this.buildLogger_menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildLogger_pictureBox_backDrop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip buildLogger_menuStrip;
        private ToolStripMenuItem buildLogger_menuStrip_button_saveLog;
        private ToolStripMenuItem buildLogger_menuStrip_button_printLog;
        private PictureBox buildLogger_pictureBox_backDrop;
        private TextBox buildLogger_textBox_logDisplay;
    }
}