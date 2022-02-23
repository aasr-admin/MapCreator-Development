using System.Windows.Forms;

namespace BuildLogger
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
            this.buildLogger_pictureBox_backDrop = new System.Windows.Forms.PictureBox();
            this.buildLogger_textBox_logOutput = new System.Windows.Forms.TextBox();
            this.buildLogger_menuStrip = new System.Windows.Forms.MenuStrip();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.buildLogger_pictureBox_backDrop)).BeginInit();
            this.buildLogger_menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buildLogger_pictureBox_backDrop
            // 
            this.buildLogger_pictureBox_backDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildLogger_pictureBox_backDrop.Image = ((System.Drawing.Image)(resources.GetObject("buildLogger_pictureBox_backDrop.Image")));
            this.buildLogger_pictureBox_backDrop.Location = new System.Drawing.Point(0, 29);
            this.buildLogger_pictureBox_backDrop.Name = "buildLogger_pictureBox_backDrop";
            this.buildLogger_pictureBox_backDrop.Size = new System.Drawing.Size(372, 339);
            this.buildLogger_pictureBox_backDrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buildLogger_pictureBox_backDrop.TabIndex = 0;
            this.buildLogger_pictureBox_backDrop.TabStop = false;
            // 
            // buildLogger_textBox_logOutput
            // 
            this.buildLogger_textBox_logOutput.Location = new System.Drawing.Point(10, 39);
            this.buildLogger_textBox_logOutput.Multiline = true;
            this.buildLogger_textBox_logOutput.Name = "buildLogger_textBox_logOutput";
            this.buildLogger_textBox_logOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.buildLogger_textBox_logOutput.Size = new System.Drawing.Size(352, 321);
            this.buildLogger_textBox_logOutput.TabIndex = 1;
            // 
            // buildLogger_menuStrip
            // 
            this.buildLogger_menuStrip.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.buildLogger_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.printToolStripMenuItem});
            this.buildLogger_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.buildLogger_menuStrip.Name = "buildLogger_menuStrip";
            this.buildLogger_menuStrip.Size = new System.Drawing.Size(372, 29);
            this.buildLogger_menuStrip.TabIndex = 2;
            this.buildLogger_menuStrip.Text = "menuStrip1";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Margin = new System.Windows.Forms.Padding(190, 0, 11, 0);
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(92, 25);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(71, 25);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // buildLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 368);
            this.Controls.Add(this.buildLogger_textBox_logOutput);
            this.Controls.Add(this.buildLogger_pictureBox_backDrop);
            this.Controls.Add(this.buildLogger_menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.buildLogger_menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "buildLogger";
            this.Text = "MapCreator Logger";
            this.Load += new System.EventHandler(this.buildLogger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.buildLogger_pictureBox_backDrop)).EndInit();
            this.buildLogger_menuStrip.ResumeLayout(false);
            this.buildLogger_menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox buildLogger_pictureBox_backDrop;
        private System.Windows.Forms.TextBox buildLogger_textBox_logOutput;
        private System.Windows.Forms.MenuStrip buildLogger_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    }
}