namespace MapCreator
{
    partial class titleScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(titleScreen));
            this.titleScreen_pictureBox01_splashImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.titleScreen_pictureBox01_splashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // titleScreen_pictureBox01_splashImage
            // 
            this.titleScreen_pictureBox01_splashImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleScreen_pictureBox01_splashImage.Image = ((System.Drawing.Image)(resources.GetObject("titleScreen_pictureBox01_splashImage.Image")));
            this.titleScreen_pictureBox01_splashImage.Location = new System.Drawing.Point(0, 0);
            this.titleScreen_pictureBox01_splashImage.Name = "titleScreen_pictureBox01_splashImage";
            this.titleScreen_pictureBox01_splashImage.Size = new System.Drawing.Size(546, 224);
            this.titleScreen_pictureBox01_splashImage.TabIndex = 0;
            this.titleScreen_pictureBox01_splashImage.TabStop = false;
            this.titleScreen_pictureBox01_splashImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.titleScreen_pictureBox01_splashImage_MouseClick);
            // 
            // titleScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(546, 224);
            this.Controls.Add(this.titleScreen_pictureBox01_splashImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "titleScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.titleScreen_pictureBox01_splashImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox titleScreen_pictureBox01_splashImage;
    }
}

