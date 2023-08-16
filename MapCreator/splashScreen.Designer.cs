namespace MapCreator
{
    partial class splashScreen
    {
        /// <summary>
        ///  Required designer variable.
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
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Timer splashScreen_closeTimer;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(splashScreen));
            this.splashScreen_pictureBox_titleImage = new System.Windows.Forms.PictureBox();
            splashScreen_closeTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splashScreen_pictureBox_titleImage)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreen_closeTimer
            // 
            splashScreen_closeTimer.Interval = 1500;
            splashScreen_closeTimer.Tick += new System.EventHandler(this.splashScreen_closeTimer_Tick);
            // 
            // splashScreen_pictureBox_titleImage
            // 
            this.splashScreen_pictureBox_titleImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.splashScreen_pictureBox_titleImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splashScreen_pictureBox_titleImage.Image = ((System.Drawing.Image)(resources.GetObject("splashScreen_pictureBox_titleImage.Image")));
            this.splashScreen_pictureBox_titleImage.Location = new System.Drawing.Point(0, 0);
            this.splashScreen_pictureBox_titleImage.Name = "splashScreen_pictureBox_titleImage";
            this.splashScreen_pictureBox_titleImage.Size = new System.Drawing.Size(546, 224);
            this.splashScreen_pictureBox_titleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.splashScreen_pictureBox_titleImage.TabIndex = 0;
            this.splashScreen_pictureBox_titleImage.TabStop = false;
            // 
            // splashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 224);
            this.ControlBox = false;
            this.Controls.Add(this.splashScreen_pictureBox_titleImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "splashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.splashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.splashScreen_pictureBox_titleImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox splashScreen_pictureBox_titleImage;
        private System.Windows.Forms.Timer splashScreen_closeTimer;
    }
}