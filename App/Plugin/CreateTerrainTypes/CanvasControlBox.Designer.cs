﻿namespace MapCreator
{
    partial class CanvasControlBox
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasControlBox));
            CanvasNavControl = new ToolStrip();
            WestButton = new ToolStripButton();
            NorthWestButton = new ToolStripButton();
            NorthButton = new ToolStripButton();
            SouthWestButton = new ToolStripButton();
            NavIcon = new ToolStripButton();
            NorthEastButton = new ToolStripButton();
            SouthButton = new ToolStripButton();
            SouthEastButton = new ToolStripButton();
            EastButton = new ToolStripButton();
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
            CanvasNavControl.Items.AddRange(new ToolStripItem[] { WestButton, NorthWestButton, NorthButton, SouthWestButton, NavIcon, NorthEastButton, SouthButton, SouthEastButton, EastButton });
            CanvasNavControl.LayoutStyle = ToolStripLayoutStyle.Flow;
            CanvasNavControl.Location = new Point(16, 19);
            CanvasNavControl.Name = "CanvasNavControl";
            CanvasNavControl.Size = new Size(110, 118);
            CanvasNavControl.TabIndex = 0;
            CanvasNavControl.Text = "toolStrip1";
            // 
            // WestButton
            // 
            WestButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            WestButton.Image = (Image)resources.GetObject("WestButton.Image");
            WestButton.ImageTransparentColor = Color.Magenta;
            WestButton.Name = "WestButton";
            WestButton.Size = new Size(36, 36);
            WestButton.Tag = 1;
            WestButton.Text = "West";
            WestButton.ToolTipText = "West";
            WestButton.Click += OnAxisButtonClick;
            // 
            // NorthWestButton
            // 
            NorthWestButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NorthWestButton.Image = (Image)resources.GetObject("NorthWestButton.Image");
            NorthWestButton.ImageTransparentColor = Color.Magenta;
            NorthWestButton.Name = "NorthWestButton";
            NorthWestButton.Size = new Size(36, 36);
            NorthWestButton.Tag = 2;
            NorthWestButton.Text = "NorthWest";
            NorthWestButton.ToolTipText = "NorthWest";
            NorthWestButton.Click += OnAxisButtonClick;
            // 
            // NorthButton
            // 
            NorthButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NorthButton.Image = (Image)resources.GetObject("NorthButton.Image");
            NorthButton.ImageTransparentColor = Color.Magenta;
            NorthButton.Name = "NorthButton";
            NorthButton.Size = new Size(36, 36);
            NorthButton.Tag = 3;
            NorthButton.Text = "North";
            NorthButton.ToolTipText = "North";
            NorthButton.Click += OnAxisButtonClick;
            // 
            // SouthWestButton
            // 
            SouthWestButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SouthWestButton.Image = (Image)resources.GetObject("SouthWestButton.Image");
            SouthWestButton.ImageTransparentColor = Color.Magenta;
            SouthWestButton.Name = "SouthWestButton";
            SouthWestButton.Size = new Size(36, 36);
            SouthWestButton.Tag = 4;
            SouthWestButton.Text = "SouthWest";
            SouthWestButton.ToolTipText = "SouthWest";
            SouthWestButton.Click += OnAxisButtonClick;
            // 
            // NavIcon
            // 
            NavIcon.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NavIcon.Image = (Image)resources.GetObject("NavIcon.Image");
            NavIcon.ImageTransparentColor = Color.Magenta;
            NavIcon.Name = "NavIcon";
            NavIcon.Size = new Size(36, 36);
            NavIcon.Tag = 5;
            NavIcon.Click += OnAxisButtonClick;
            // 
            // NorthEastButton
            // 
            NorthEastButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            NorthEastButton.Image = (Image)resources.GetObject("NorthEastButton.Image");
            NorthEastButton.ImageTransparentColor = Color.Magenta;
            NorthEastButton.Name = "NorthEastButton";
            NorthEastButton.Size = new Size(36, 36);
            NorthEastButton.Tag = 6;
            NorthEastButton.Text = "NorthEast";
            NorthEastButton.ToolTipText = "NorthEast";
            NorthEastButton.Click += OnAxisButtonClick;
            // 
            // SouthButton
            // 
            SouthButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SouthButton.Image = (Image)resources.GetObject("SouthButton.Image");
            SouthButton.ImageTransparentColor = Color.Magenta;
            SouthButton.Name = "SouthButton";
            SouthButton.Size = new Size(36, 36);
            SouthButton.Tag = 7;
            SouthButton.Text = "South";
            SouthButton.ToolTipText = "South";
            SouthButton.Click += OnAxisButtonClick;
            // 
            // SouthEastButton
            // 
            SouthEastButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            SouthEastButton.Image = (Image)resources.GetObject("SouthEastButton.Image");
            SouthEastButton.ImageTransparentColor = Color.Magenta;
            SouthEastButton.Name = "SouthEastButton";
            SouthEastButton.Size = new Size(36, 36);
            SouthEastButton.Tag = 8;
            SouthEastButton.Text = "SouthEast";
            SouthEastButton.ToolTipText = "SouthEast";
            SouthEastButton.Click += OnAxisButtonClick;
            // 
            // EastButton
            // 
            EastButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EastButton.Image = (Image)resources.GetObject("EastButton.Image");
            EastButton.ImageTransparentColor = Color.Magenta;
            EastButton.Name = "EastButton";
            EastButton.Size = new Size(36, 36);
            EastButton.Tag = 9;
            EastButton.Text = "East";
            EastButton.ToolTipText = "East";
            EastButton.Click += OnAxisButtonClick;
            // 
            // xAxis_label_numUpDown
            // 
            xAxis_label_numUpDown.Location = new Point(176, 23);
            xAxis_label_numUpDown.Maximum = sbyte.MaxValue;
            xAxis_label_numUpDown.Minimum = sbyte.MinValue;
            xAxis_label_numUpDown.Name = "xAxis_label_numUpDown";
            xAxis_label_numUpDown.Size = new Size(60, 23);
            xAxis_label_numUpDown.TabIndex = 1;
            xAxis_label_numUpDown.TextAlign = HorizontalAlignment.Right;
            xAxis_label_numUpDown.ValueChanged += OnSingleValueChanged;
            // 
            // yAxis_label_numUpDown
            // 
            yAxis_label_numUpDown.Location = new Point(176, 66);
            yAxis_label_numUpDown.Maximum = sbyte.MaxValue;
            yAxis_label_numUpDown.Minimum = sbyte.MinValue;
            yAxis_label_numUpDown.Name = "yAxis_label_numUpDown";
            yAxis_label_numUpDown.Size = new Size(60, 23);
            yAxis_label_numUpDown.TabIndex = 2;
            yAxis_label_numUpDown.TextAlign = HorizontalAlignment.Right;
            yAxis_label_numUpDown.ValueChanged += OnSingleValueChanged;
            // 
            // zAxis_label_numUpDown
            // 
            zAxis_label_numUpDown.Location = new Point(176, 107);
            zAxis_label_numUpDown.Maximum = sbyte.MaxValue;
            zAxis_label_numUpDown.Minimum = sbyte.MinValue;
            zAxis_label_numUpDown.Name = "zAxis_label_numUpDown";
            zAxis_label_numUpDown.Size = new Size(60, 23);
            zAxis_label_numUpDown.TabIndex = 3;
            zAxis_label_numUpDown.TextAlign = HorizontalAlignment.Right;
            zAxis_label_numUpDown.ValueChanged += OnSingleValueChanged;
            // 
            // xAxis_label
            // 
            xAxis_label.AutoSize = true;
            xAxis_label.Font = new Font("Segoe UI", 12F);
            xAxis_label.Location = new Point(148, 23);
            xAxis_label.Name = "xAxis_label";
            xAxis_label.Size = new Size(22, 21);
            xAxis_label.TabIndex = 4;
            xAxis_label.Text = "X:";
            // 
            // yAxis_label
            // 
            yAxis_label.AutoSize = true;
            yAxis_label.Font = new Font("Segoe UI", 12F);
            yAxis_label.Location = new Point(148, 66);
            yAxis_label.Name = "yAxis_label";
            yAxis_label.Size = new Size(22, 21);
            yAxis_label.TabIndex = 5;
            yAxis_label.Text = "Y:";
            // 
            // zAxis_label
            // 
            zAxis_label.AutoSize = true;
            zAxis_label.Font = new Font("Segoe UI", 12F);
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
            ControlBox = false;
            Controls.Add(zAxis_label);
            Controls.Add(yAxis_label);
            Controls.Add(xAxis_label);
            Controls.Add(zAxis_label_numUpDown);
            Controls.Add(yAxis_label_numUpDown);
            Controls.Add(xAxis_label_numUpDown);
            Controls.Add(CanvasNavControl);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "canvasControlBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Static Placement";
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
        private ToolStripButton WestButton;
        private ToolStripButton NorthWestButton;
        private ToolStripButton NorthButton;
        private ToolStripButton SouthWestButton;
        private ToolStripButton NavIcon;
        private ToolStripButton NorthEastButton;
        private ToolStripButton SouthButton;
        private ToolStripButton SouthEastButton;
        private ToolStripButton EastButton;
        private NumericUpDown xAxis_label_numUpDown;
        private NumericUpDown yAxis_label_numUpDown;
        private NumericUpDown zAxis_label_numUpDown;
        private Label xAxis_label;
        private Label yAxis_label;
        private Label zAxis_label;
    }
}