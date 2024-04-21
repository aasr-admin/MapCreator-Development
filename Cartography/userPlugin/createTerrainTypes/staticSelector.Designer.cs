namespace MapCreator.userPlugin
{
    partial class staticSelector
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
            panel1 = new Panel();
            staticSelector_staticPreview = new Panel();
            vScrollBar1 = new VScrollBar();
            staticSelector_closeButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(staticSelector_staticPreview);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(332, 394);
            panel1.TabIndex = 0;
            // 
            // staticSelector_staticPreview
            // 
            staticSelector_staticPreview.BorderStyle = BorderStyle.FixedSingle;
            staticSelector_staticPreview.Location = new Point(-1, -1);
            staticSelector_staticPreview.Name = "staticSelector_staticPreview";
            staticSelector_staticPreview.Size = new Size(334, 543);
            staticSelector_staticPreview.TabIndex = 0;
            staticSelector_staticPreview.Paint += staticSelector_staticPreview_Paint;
            staticSelector_staticPreview.MouseDown += staticSelector_staticPreview_MouseDown;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(351, 9);
            vScrollBar1.Maximum = 65535;
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(17, 397);
            vScrollBar1.TabIndex = 0;
            vScrollBar1.Scroll += vScrollBar1_Scroll;
            // 
            // staticSelector_closeButton
            // 
            staticSelector_closeButton.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            staticSelector_closeButton.Location = new Point(12, 411);
            staticSelector_closeButton.Name = "staticSelector_closeButton";
            staticSelector_closeButton.Size = new Size(332, 41);
            staticSelector_closeButton.TabIndex = 2;
            staticSelector_closeButton.Text = "Close Static Selector";
            staticSelector_closeButton.UseVisualStyleBackColor = true;
            staticSelector_closeButton.Click += staticSelector_closeButton_Click;
            // 
            // staticSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 457);
            ControlBox = false;
            Controls.Add(staticSelector_closeButton);
            Controls.Add(vScrollBar1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            TopMost = true;
            Name = "staticSelector";
            StartPosition = FormStartPosition.CenterScreen;
            MouseDown += staticSelector_MouseDown;
            MouseMove += staticSelector_MouseMove;
            MouseUp += staticSelector_MouseUp;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button staticSelector_closeButton;
        public Panel staticSelector_staticPreview;
        public VScrollBar vScrollBar1;
    }
}