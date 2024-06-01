namespace MapCreator
{
    partial class BuildLogger
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildLogger));
            buildLogger_menuStrip = new MenuStrip();
            buildLogger_menuStrip_button_saveLog = new ToolStripMenuItem();
            buildLogger_menuStrip_button_printLog = new ToolStripMenuItem();
            buildLogger_pictureBox_backDrop = new PictureBox();
            buildLogger_textBox_logDisplay = new TextBox();
            buildLogger_menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)buildLogger_pictureBox_backDrop).BeginInit();
            SuspendLayout();
            // 
            // buildLogger_menuStrip
            // 
            buildLogger_menuStrip.ImageScalingSize = new Size(24, 24);
            buildLogger_menuStrip.Items.AddRange(new ToolStripItem[] { buildLogger_menuStrip_button_saveLog, buildLogger_menuStrip_button_printLog });
            buildLogger_menuStrip.Location = new Point(0, 0);
            buildLogger_menuStrip.Name = "buildLogger_menuStrip";
            buildLogger_menuStrip.Size = new Size(413, 32);
            buildLogger_menuStrip.TabIndex = 0;
            buildLogger_menuStrip.Text = "menuStrip1";
            // 
            // buildLogger_menuStrip_button_saveLog
            // 
            buildLogger_menuStrip_button_saveLog.Image = (Image)resources.GetObject("buildLogger_menuStrip_button_saveLog.Image");
            buildLogger_menuStrip_button_saveLog.Margin = new Padding(313, 0, 0, 0);
            buildLogger_menuStrip_button_saveLog.Name = "buildLogger_menuStrip_button_saveLog";
            buildLogger_menuStrip_button_saveLog.Size = new Size(36, 28);
            buildLogger_menuStrip_button_saveLog.Click += buildLogger_menuStrip_button_saveLog_Click;
            // 
            // buildLogger_menuStrip_button_printLog
            // 
            buildLogger_menuStrip_button_printLog.Font = new Font("Segoe UI", 12F);
            buildLogger_menuStrip_button_printLog.Image = (Image)resources.GetObject("buildLogger_menuStrip_button_printLog.Image");
            buildLogger_menuStrip_button_printLog.Margin = new Padding(5, 0, 0, 0);
            buildLogger_menuStrip_button_printLog.Name = "buildLogger_menuStrip_button_printLog";
            buildLogger_menuStrip_button_printLog.Size = new Size(36, 28);
            buildLogger_menuStrip_button_printLog.Click += buildLogger_menuStrip_button_printLog_Click;
            // 
            // buildLogger_pictureBox_backDrop
            // 
            buildLogger_pictureBox_backDrop.Dock = DockStyle.Fill;
            buildLogger_pictureBox_backDrop.Image = (Image)resources.GetObject("buildLogger_pictureBox_backDrop.Image");
            buildLogger_pictureBox_backDrop.Location = new Point(0, 32);
            buildLogger_pictureBox_backDrop.Name = "buildLogger_pictureBox_backDrop";
            buildLogger_pictureBox_backDrop.Size = new Size(413, 335);
            buildLogger_pictureBox_backDrop.SizeMode = PictureBoxSizeMode.StretchImage;
            buildLogger_pictureBox_backDrop.TabIndex = 1;
            buildLogger_pictureBox_backDrop.TabStop = false;
            // 
            // buildLogger_textBox_logDisplay
            // 
            buildLogger_textBox_logDisplay.Location = new Point(10, 41);
            buildLogger_textBox_logDisplay.Multiline = true;
            buildLogger_textBox_logDisplay.Name = "buildLogger_textBox_logDisplay";
            buildLogger_textBox_logDisplay.ScrollBars = ScrollBars.Vertical;
            buildLogger_textBox_logDisplay.Size = new Size(393, 319);
            buildLogger_textBox_logDisplay.TabIndex = 2;
            // 
            // buildLogger
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 367);
            ControlBox = false;
            Controls.Add(buildLogger_textBox_logDisplay);
            Controls.Add(buildLogger_pictureBox_backDrop);
            Controls.Add(buildLogger_menuStrip);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = buildLogger_menuStrip;
            MaximizeBox = false;
            Name = "buildLogger";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Logger";
            buildLogger_menuStrip.ResumeLayout(false);
            buildLogger_menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)buildLogger_pictureBox_backDrop).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip buildLogger_menuStrip;
        private ToolStripMenuItem buildLogger_menuStrip_button_saveLog;
        private ToolStripMenuItem buildLogger_menuStrip_button_printLog;
        private PictureBox buildLogger_pictureBox_backDrop;
        private TextBox buildLogger_textBox_logDisplay;
    }
}