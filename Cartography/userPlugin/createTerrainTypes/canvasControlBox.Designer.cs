namespace MapCreator.userPlugin
{
    partial class canvasControlBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(canvasControlBox));
            CanvasNavControl = new ToolStrip();
            NorthWestButton = new ToolStripButton();
            NorthButton = new ToolStripButton();
            NorthEastButton = new ToolStripButton();
            WestButton = new ToolStripButton();
            NavIcon = new ToolStripButton();
            EastButton = new ToolStripButton();
            SouthWestButton = new ToolStripButton();
            SouthButton = new ToolStripButton();
            SouthEastButton = new ToolStripButton();
            xAxis_label_numUpDown = new NumericUpDown();
            yAxis_label_numUpDown = new NumericUpDown();
            zAxis_label_numUpDown = new NumericUpDown();
            xAxis_label = new Label();
            yAxis_label = new Label();
            zAxis_label = new Label();
            CanvasNavControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)xAxis_label_numUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)yAxis_label_numUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zAxis_label_numUpDown).BeginInit();
            SuspendLayout();
            // 
            // CanvasNavControl
            // 
            CanvasNavControl.AutoSize = false;
            CanvasNavControl.Dock = DockStyle.None;
            CanvasNavControl.ImageScalingSize = new Size(32, 32);
            CanvasNavControl.Items.AddRange(new ToolStripItem[] { NorthWestButton, NorthButton, NorthEastButton, WestButton, NavIcon, EastButton, SouthWestButton, SouthButton, SouthEastButton });
            CanvasNavControl.LayoutStyle = ToolStripLayoutStyle.Flow;
            CanvasNavControl.Location = new Point(16, 19);
            CanvasNavControl.Name = "CanvasNavControl";
            CanvasNavControl.Size = new Size(110, 118);
            CanvasNavControl.TabIndex = 0;
            CanvasNavControl.Text = "toolStrip1";
            // 
            // NorthWestButton
            // 
            NorthWestButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NorthWestButton.Image = (Image)resources.GetObject("NorthWestButton.Image");
            NorthWestButton.ImageTransparentColor = Color.Magenta;
            NorthWestButton.Name = "NorthWestButton";
            NorthWestButton.Size = new Size(36, 36);
            NorthWestButton.Tag = 1;
            NorthWestButton.Text = "NorthWest";
            NorthWestButton.Click += NorthWestButton_Click;
            // 
            // NorthButton
            // 
            NorthButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NorthButton.Image = (Image)resources.GetObject("NorthButton.Image");
            NorthButton.ImageTransparentColor = Color.Magenta;
            NorthButton.Name = "NorthButton";
            NorthButton.Size = new Size(36, 36);
            NorthButton.Tag = 2;
            NorthButton.Text = "North";
            NorthButton.Click += NorthButton_Click;
            // 
            // NorthEastButton
            // 
            NorthEastButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NorthEastButton.Image = (Image)resources.GetObject("NorthEastButton.Image");
            NorthEastButton.ImageTransparentColor = Color.Magenta;
            NorthEastButton.Name = "NorthEastButton";
            NorthEastButton.Size = new Size(36, 36);
            NorthEastButton.Tag = 3;
            NorthEastButton.Text = "NorthEast";
            NorthEastButton.Click += NorthEastButton_Click;
            // 
            // WestButton
            // 
            WestButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            WestButton.Image = (Image)resources.GetObject("WestButton.Image");
            WestButton.ImageTransparentColor = Color.Magenta;
            WestButton.Name = "WestButton";
            WestButton.Size = new Size(36, 36);
            WestButton.Tag = 4;
            WestButton.Text = "West";
            WestButton.Click += WestButton_Click;
            // 
            // NavIcon
            // 
            NavIcon.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NavIcon.Image = (Image)resources.GetObject("NavIcon.Image");
            NavIcon.ImageTransparentColor = Color.Magenta;
            NavIcon.Name = "NavIcon";
            NavIcon.Size = new Size(36, 36);
            NavIcon.Tag = 5;
            NavIcon.Click += NavIcon_Click;
            // 
            // EastButton
            // 
            EastButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EastButton.Image = (Image)resources.GetObject("EastButton.Image");
            EastButton.ImageTransparentColor = Color.Magenta;
            EastButton.Name = "EastButton";
            EastButton.Size = new Size(36, 36);
            EastButton.Tag = 6;
            EastButton.Text = "East";
            EastButton.Click += EastButton_Click;
            // 
            // SouthWestButton
            // 
            SouthWestButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SouthWestButton.Image = (Image)resources.GetObject("SouthWestButton.Image");
            SouthWestButton.ImageTransparentColor = Color.Magenta;
            SouthWestButton.Name = "SouthWestButton";
            SouthWestButton.Size = new Size(36, 36);
            SouthWestButton.Tag = 7;
            SouthWestButton.Text = "SouthWest";
            SouthWestButton.Click += SouthWestButton_Click;
            // 
            // SouthButton
            // 
            SouthButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SouthButton.Image = (Image)resources.GetObject("SouthButton.Image");
            SouthButton.ImageTransparentColor = Color.Magenta;
            SouthButton.Name = "SouthButton";
            SouthButton.Size = new Size(36, 36);
            SouthButton.Tag = 8;
            SouthButton.Text = "South";
            SouthButton.Click += SouthButton_Click;
            // 
            // SouthEastButton
            // 
            SouthEastButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SouthEastButton.Image = (Image)resources.GetObject("SouthEastButton.Image");
            SouthEastButton.ImageTransparentColor = Color.Magenta;
            SouthEastButton.Name = "SouthEastButton";
            SouthEastButton.Size = new Size(36, 36);
            SouthEastButton.Tag = 9;
            SouthEastButton.Text = "SouthEast";
            SouthEastButton.Click += SouthEastButton_Click;
            // 
            // xAxis_label_numUpDown
            // 
            xAxis_label_numUpDown.Location = new Point(176, 23);
            xAxis_label_numUpDown.Maximum = 6;
            xAxis_label_numUpDown.Minimum = -6;
            xAxis_label_numUpDown.Name = "xAxis_label_numUpDown";
            xAxis_label_numUpDown.Size = new Size(60, 23);
            xAxis_label_numUpDown.TabIndex = 1;
            xAxis_label_numUpDown.TextAlign = HorizontalAlignment.Right;
            xAxis_label_numUpDown.ValueChanged += xAxis_label_numUpDown_ValueChanged;
            // 
            // yAxis_label_numUpDown
            // 
            yAxis_label_numUpDown.Location = new Point(176, 66);
            yAxis_label_numUpDown.Maximum = 6;
            yAxis_label_numUpDown.Minimum = -6;
            yAxis_label_numUpDown.Name = "yAxis_label_numUpDown";
            yAxis_label_numUpDown.Size = new Size(60, 23);
            yAxis_label_numUpDown.TabIndex = 2;
            yAxis_label_numUpDown.TextAlign = HorizontalAlignment.Right;
            yAxis_label_numUpDown.ValueChanged += yAxis_label_numUpDown_ValueChanged;
            // 
            // zAxis_label_numUpDown
            // 
            zAxis_label_numUpDown.Location = new Point(176, 107);
            zAxis_label_numUpDown.Maximum = 127;
            zAxis_label_numUpDown.Minimum = -128;
            zAxis_label_numUpDown.Name = "zAxis_label_numUpDown";
            zAxis_label_numUpDown.Size = new Size(60, 23);
            zAxis_label_numUpDown.TabIndex = 3;
            zAxis_label_numUpDown.TextAlign = HorizontalAlignment.Right;
            zAxis_label_numUpDown.ValueChanged += zAxis_label_numUpDown_ValueChanged;
            // 
            // xAxis_label
            // 
            xAxis_label.AutoSize = true;
            xAxis_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            xAxis_label.Location = new Point(148, 23);
            xAxis_label.Name = "xAxis_label";
            xAxis_label.Size = new Size(22, 21);
            xAxis_label.TabIndex = 4;
            xAxis_label.Text = "X:";
            // 
            // yAxis_label
            // 
            yAxis_label.AutoSize = true;
            yAxis_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            yAxis_label.Location = new Point(148, 66);
            yAxis_label.Name = "yAxis_label";
            yAxis_label.Size = new Size(22, 21);
            yAxis_label.TabIndex = 5;
            yAxis_label.Text = "Y:";
            // 
            // zAxis_label
            // 
            zAxis_label.AutoSize = true;
            zAxis_label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            zAxis_label.Location = new Point(148, 107);
            zAxis_label.Name = "zAxis_label";
            zAxis_label.Size = new Size(22, 21);
            zAxis_label.TabIndex = 6;
            zAxis_label.Text = "Z:";
            // 
            // canvasControlBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(256, 151);
            Controls.Add(zAxis_label);
            Controls.Add(yAxis_label);
            Controls.Add(xAxis_label);
            Controls.Add(zAxis_label_numUpDown);
            Controls.Add(yAxis_label_numUpDown);
            Controls.Add(xAxis_label_numUpDown);
            Controls.Add(CanvasNavControl);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "canvasControlBox";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MapCreator: Static Placement";
            TopMost = true;
            MouseDown += canvasControlBox_MouseDown;
            MouseMove += canvasControlBox_MouseMove;
            MouseUp += canvasControlBox_MouseUp;
            CanvasNavControl.ResumeLayout(false);
            CanvasNavControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)xAxis_label_numUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)yAxis_label_numUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)zAxis_label_numUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip CanvasNavControl;
        public ToolStripButton NorthWestButton;
        public ToolStripButton NorthButton;
        public ToolStripButton NorthEastButton;
        public ToolStripButton WestButton;
        private ToolStripButton NavIcon;
        public ToolStripButton EastButton;
        public ToolStripButton SouthWestButton;
        public ToolStripButton SouthButton;
        public ToolStripButton SouthEastButton;
        public NumericUpDown xAxis_label_numUpDown;
        public NumericUpDown yAxis_label_numUpDown;
        public NumericUpDown zAxis_label_numUpDown;
        private Label xAxis_label;
        private Label yAxis_label;
        private Label zAxis_label;
    }
}