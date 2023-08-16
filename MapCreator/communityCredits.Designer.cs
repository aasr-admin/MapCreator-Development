namespace MapCreator
{
    partial class communityCredits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(communityCredits));
            communityCredits_pictureBox_backDrop = new PictureBox();
            communityCredits_label_thankDeveloper = new Label();
            communityCredits_label_thankSubmitter = new Label();
            communityCredits_pictureBox_mapCreatorLogo = new PictureBox();
            communityCredits_label_thankDeveloper_textBox = new TextBox();
            communityCredits_label_thankSubmitter_textBox = new TextBox();
            communityCredits_pictureBox_dividerBottom = new PictureBox();
            communityCredits_pictureBox_dividerTop = new PictureBox();
            communityCredits_label_mapCreator = new Label();
            communityCredits_label_mapCreatorVersioning = new Label();
            communityCredits_label_mapCreatorBuildDate = new Label();
            communityCredits_linkLabel_uoAvocation = new LinkLabel();
            communityCredits_button_close = new Button();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_backDrop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_mapCreatorLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_dividerBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_dividerTop).BeginInit();
            SuspendLayout();
            // 
            // communityCredits_pictureBox_backDrop
            // 
            communityCredits_pictureBox_backDrop.Image = (Image)resources.GetObject("communityCredits_pictureBox_backDrop.Image");
            communityCredits_pictureBox_backDrop.Location = new Point(0, 0);
            communityCredits_pictureBox_backDrop.Name = "communityCredits_pictureBox_backDrop";
            communityCredits_pictureBox_backDrop.Size = new Size(246, 360);
            communityCredits_pictureBox_backDrop.SizeMode = PictureBoxSizeMode.StretchImage;
            communityCredits_pictureBox_backDrop.TabIndex = 1;
            communityCredits_pictureBox_backDrop.TabStop = false;
            // 
            // communityCredits_label_thankDeveloper
            // 
            communityCredits_label_thankDeveloper.AutoSize = true;
            communityCredits_label_thankDeveloper.BackColor = Color.Transparent;
            communityCredits_label_thankDeveloper.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_thankDeveloper.ForeColor = Color.LightSlateGray;
            communityCredits_label_thankDeveloper.Location = new Point(4, 14);
            communityCredits_label_thankDeveloper.Name = "communityCredits_label_thankDeveloper";
            communityCredits_label_thankDeveloper.Size = new Size(300, 20);
            communityCredits_label_thankDeveloper.TabIndex = 2;
            communityCredits_label_thankDeveloper.Text = "Thank You To The Developer(s) Who Assited\r\n";
            // 
            // communityCredits_label_thankSubmitter
            // 
            communityCredits_label_thankSubmitter.AutoSize = true;
            communityCredits_label_thankSubmitter.BackColor = Color.Transparent;
            communityCredits_label_thankSubmitter.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_thankSubmitter.ForeColor = Color.LightSlateGray;
            communityCredits_label_thankSubmitter.Location = new Point(6, 187);
            communityCredits_label_thankSubmitter.Name = "communityCredits_label_thankSubmitter";
            communityCredits_label_thankSubmitter.Size = new Size(298, 20);
            communityCredits_label_thankSubmitter.TabIndex = 3;
            communityCredits_label_thankSubmitter.Text = "Thank You To Those Who Submitted Plugins";
            // 
            // communityCredits_pictureBox_mapCreatorLogo
            // 
            communityCredits_pictureBox_mapCreatorLogo.Image = (Image)resources.GetObject("communityCredits_pictureBox_mapCreatorLogo.Image");
            communityCredits_pictureBox_mapCreatorLogo.Location = new Point(257, 51);
            communityCredits_pictureBox_mapCreatorLogo.Name = "communityCredits_pictureBox_mapCreatorLogo";
            communityCredits_pictureBox_mapCreatorLogo.Size = new Size(83, 83);
            communityCredits_pictureBox_mapCreatorLogo.SizeMode = PictureBoxSizeMode.Zoom;
            communityCredits_pictureBox_mapCreatorLogo.TabIndex = 4;
            communityCredits_pictureBox_mapCreatorLogo.TabStop = false;
            // 
            // communityCredits_label_thankDeveloper_textBox
            // 
            communityCredits_label_thankDeveloper_textBox.BackColor = Color.LightGray;
            communityCredits_label_thankDeveloper_textBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_thankDeveloper_textBox.ForeColor = Color.Navy;
            communityCredits_label_thankDeveloper_textBox.Location = new Point(10, 51);
            communityCredits_label_thankDeveloper_textBox.Multiline = true;
            communityCredits_label_thankDeveloper_textBox.Name = "communityCredits_label_thankDeveloper_textBox";
            communityCredits_label_thankDeveloper_textBox.ScrollBars = ScrollBars.Vertical;
            communityCredits_label_thankDeveloper_textBox.Size = new Size(225, 122);
            communityCredits_label_thankDeveloper_textBox.TabIndex = 5;
            communityCredits_label_thankDeveloper_textBox.TabStop = false;
            communityCredits_label_thankDeveloper_textBox.Text = "☺ aasr-sva\r\n--------------\r\n☺ dknight\r\n--------------\r\n☺ Deragon\r\n--------------\r\n☺ KARASHO'\r\n--------------\r\n☺ Praxiiz\r\n--------------\r\n☺ Punt\r\n--------------\r\n☺ Voxpire";
            // 
            // communityCredits_label_thankSubmitter_textBox
            // 
            communityCredits_label_thankSubmitter_textBox.BackColor = Color.LightGray;
            communityCredits_label_thankSubmitter_textBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_thankSubmitter_textBox.ForeColor = Color.Navy;
            communityCredits_label_thankSubmitter_textBox.Location = new Point(10, 223);
            communityCredits_label_thankSubmitter_textBox.Multiline = true;
            communityCredits_label_thankSubmitter_textBox.Name = "communityCredits_label_thankSubmitter_textBox";
            communityCredits_label_thankSubmitter_textBox.ScrollBars = ScrollBars.Vertical;
            communityCredits_label_thankSubmitter_textBox.Size = new Size(225, 122);
            communityCredits_label_thankSubmitter_textBox.TabIndex = 6;
            communityCredits_label_thankSubmitter_textBox.TabStop = false;
            communityCredits_label_thankSubmitter_textBox.Text = "☺ Asylum\r\n--------------\r\n☺ deccer\r\n--------------\r\n☺ dknight";
            // 
            // communityCredits_pictureBox_dividerBottom
            // 
            communityCredits_pictureBox_dividerBottom.Image = (Image)resources.GetObject("communityCredits_pictureBox_dividerBottom.Image");
            communityCredits_pictureBox_dividerBottom.Location = new Point(10, 208);
            communityCredits_pictureBox_dividerBottom.Name = "communityCredits_pictureBox_dividerBottom";
            communityCredits_pictureBox_dividerBottom.Size = new Size(345, 10);
            communityCredits_pictureBox_dividerBottom.SizeMode = PictureBoxSizeMode.StretchImage;
            communityCredits_pictureBox_dividerBottom.TabIndex = 7;
            communityCredits_pictureBox_dividerBottom.TabStop = false;
            // 
            // communityCredits_pictureBox_dividerTop
            // 
            communityCredits_pictureBox_dividerTop.Image = (Image)resources.GetObject("communityCredits_pictureBox_dividerTop.Image");
            communityCredits_pictureBox_dividerTop.Location = new Point(10, 35);
            communityCredits_pictureBox_dividerTop.Name = "communityCredits_pictureBox_dividerTop";
            communityCredits_pictureBox_dividerTop.Size = new Size(345, 10);
            communityCredits_pictureBox_dividerTop.SizeMode = PictureBoxSizeMode.StretchImage;
            communityCredits_pictureBox_dividerTop.TabIndex = 9;
            communityCredits_pictureBox_dividerTop.TabStop = false;
            // 
            // communityCredits_label_mapCreator
            // 
            communityCredits_label_mapCreator.AutoSize = true;
            communityCredits_label_mapCreator.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_mapCreator.ForeColor = Color.LightSlateGray;
            communityCredits_label_mapCreator.Location = new Point(256, 290);
            communityCredits_label_mapCreator.Name = "communityCredits_label_mapCreator";
            communityCredits_label_mapCreator.Size = new Size(88, 20);
            communityCredits_label_mapCreator.TabIndex = 10;
            communityCredits_label_mapCreator.Text = "MapCreator";
            // 
            // communityCredits_label_mapCreatorVersioning
            // 
            communityCredits_label_mapCreatorVersioning.AutoSize = true;
            communityCredits_label_mapCreatorVersioning.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_mapCreatorVersioning.ForeColor = Color.LightSlateGray;
            communityCredits_label_mapCreatorVersioning.Location = new Point(266, 310);
            communityCredits_label_mapCreatorVersioning.Name = "communityCredits_label_mapCreatorVersioning";
            communityCredits_label_mapCreatorVersioning.Size = new Size(72, 17);
            communityCredits_label_mapCreatorVersioning.TabIndex = 11;
            communityCredits_label_mapCreatorVersioning.Text = "Version 3.5";
            // 
            // communityCredits_label_mapCreatorBuildDate
            // 
            communityCredits_label_mapCreatorBuildDate.AutoSize = true;
            communityCredits_label_mapCreatorBuildDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_label_mapCreatorBuildDate.ForeColor = Color.LightSlateGray;
            communityCredits_label_mapCreatorBuildDate.Location = new Point(248, 328);
            communityCredits_label_mapCreatorBuildDate.Name = "communityCredits_label_mapCreatorBuildDate";
            communityCredits_label_mapCreatorBuildDate.Size = new Size(106, 17);
            communityCredits_label_mapCreatorBuildDate.TabIndex = 12;
            communityCredits_label_mapCreatorBuildDate.Text = "Build: 08132023a";
            // 
            // communityCredits_linkLabel_uoAvocation
            // 
            communityCredits_linkLabel_uoAvocation.AutoSize = true;
            communityCredits_linkLabel_uoAvocation.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_linkLabel_uoAvocation.ForeColor = Color.LightSlateGray;
            communityCredits_linkLabel_uoAvocation.LinkColor = Color.FromArgb(192, 192, 255);
            communityCredits_linkLabel_uoAvocation.Location = new Point(257, 127);
            communityCredits_linkLabel_uoAvocation.Name = "communityCredits_linkLabel_uoAvocation";
            communityCredits_linkLabel_uoAvocation.Size = new Size(86, 19);
            communityCredits_linkLabel_uoAvocation.TabIndex = 13;
            communityCredits_linkLabel_uoAvocation.TabStop = true;
            communityCredits_linkLabel_uoAvocation.Text = "uoAvocation";
            communityCredits_linkLabel_uoAvocation.LinkClicked += communityCredits_linkLabel_uoAvocation_LinkClicked;
            // 
            // communityCredits_button_close
            // 
            communityCredits_button_close.BackgroundImage = (Image)resources.GetObject("communityCredits_button_close.BackgroundImage");
            communityCredits_button_close.BackgroundImageLayout = ImageLayout.Stretch;
            communityCredits_button_close.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            communityCredits_button_close.ForeColor = Color.SlateGray;
            communityCredits_button_close.Location = new Point(249, 240);
            communityCredits_button_close.Name = "communityCredits_button_close";
            communityCredits_button_close.Size = new Size(101, 36);
            communityCredits_button_close.TabIndex = 14;
            communityCredits_button_close.Text = "Close";
            communityCredits_button_close.UseVisualStyleBackColor = true;
            communityCredits_button_close.Click += communityCredits_button_close_Click;
            communityCredits_button_close.MouseEnter += communityCredits_button_close_MouseEnter;
            communityCredits_button_close.MouseLeave += communityCredits_button_close_MouseLeave;
            // 
            // communityCredits
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(361, 358);
            Controls.Add(communityCredits_button_close);
            Controls.Add(communityCredits_linkLabel_uoAvocation);
            Controls.Add(communityCredits_label_mapCreatorBuildDate);
            Controls.Add(communityCredits_label_mapCreatorVersioning);
            Controls.Add(communityCredits_label_mapCreator);
            Controls.Add(communityCredits_pictureBox_dividerTop);
            Controls.Add(communityCredits_pictureBox_dividerBottom);
            Controls.Add(communityCredits_label_thankSubmitter_textBox);
            Controls.Add(communityCredits_label_thankDeveloper_textBox);
            Controls.Add(communityCredits_pictureBox_mapCreatorLogo);
            Controls.Add(communityCredits_label_thankSubmitter);
            Controls.Add(communityCredits_label_thankDeveloper);
            Controls.Add(communityCredits_pictureBox_backDrop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "communityCredits";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MapCreator: Credits";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_backDrop).EndInit();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_mapCreatorLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_dividerBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)communityCredits_pictureBox_dividerTop).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox communityCredits_pictureBox_backDrop;
        private Label communityCredits_label_thankDeveloper;
        private Label communityCredits_label_thankSubmitter;
        private PictureBox communityCredits_pictureBox_mapCreatorLogo;
        private TextBox communityCredits_label_thankDeveloper_textBox;
        private TextBox communityCredits_label_thankSubmitter_textBox;
        private PictureBox communityCredits_pictureBox_dividerBottom;
        private PictureBox communityCredits_pictureBox_dividerTop;
        private Label communityCredits_label_mapCreator;
        private Label communityCredits_label_mapCreatorVersioning;
        private Label communityCredits_label_mapCreatorBuildDate;
        private LinkLabel communityCredits_linkLabel_uoAvocation;
        private Button communityCredits_button_close;
    }
}