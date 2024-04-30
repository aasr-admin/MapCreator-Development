namespace MapCreator
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(staticSelector));
            listView = new ListView();
            toolsPanel = new TableLayoutPanel();
            previewImage = new PictureBox();
            flowPanel1 = new FlowLayoutPanel();
            valueSelector = new NumericUpDown();
            searchBox = new TextBox();
            searchButton = new Button();
            closeButton = new Button();
            progressBar = new ProgressBar();
            progressLabel = new Label();
            panel1 = new Panel();
            progressLayout = new TableLayoutPanel();
            toolsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewImage).BeginInit();
            flowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valueSelector).BeginInit();
            progressLayout.SuspendLayout();
            SuspendLayout();
            // 
            // listView
            // 
            listView.Alignment = ListViewAlignment.Default;
            listView.Dock = DockStyle.Fill;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.Location = new Point(0, 0);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.OwnerDraw = true;
            listView.ShowGroups = false;
            listView.ShowItemToolTips = true;
            listView.Size = new Size(584, 311);
            listView.TabIndex = 0;
            listView.TileSize = new Size(44, 44);
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Tile;
            listView.VirtualListSize = 65536;
            listView.DrawItem += OnDrawItem;
            listView.ItemSelectionChanged += OnItemSelectionChanged;
            // 
            // toolsPanel
            // 
            toolsPanel.AutoSize = true;
            toolsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            toolsPanel.ColumnCount = 2;
            toolsPanel.ColumnStyles.Add(new ColumnStyle());
            toolsPanel.ColumnStyles.Add(new ColumnStyle());
            toolsPanel.Controls.Add(previewImage, 0, 0);
            toolsPanel.Controls.Add(flowPanel1, 1, 0);
            toolsPanel.Dock = DockStyle.Bottom;
            toolsPanel.Font = new Font("Segoe UI", 11F);
            toolsPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            toolsPanel.Location = new Point(0, 311);
            toolsPanel.Name = "toolsPanel";
            toolsPanel.RowCount = 1;
            toolsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            toolsPanel.Size = new Size(584, 50);
            toolsPanel.TabIndex = 5;
            // 
            // previewImage
            // 
            previewImage.Dock = DockStyle.Fill;
            previewImage.Location = new Point(3, 3);
            previewImage.Name = "previewImage";
            previewImage.Size = new Size(44, 44);
            previewImage.TabIndex = 6;
            previewImage.TabStop = false;
            // 
            // flowPanel1
            // 
            flowPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flowPanel1.AutoSize = true;
            flowPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowPanel1.Controls.Add(valueSelector);
            flowPanel1.Controls.Add(searchBox);
            flowPanel1.Controls.Add(searchButton);
            flowPanel1.Controls.Add(closeButton);
            flowPanel1.Location = new Point(185, 17);
            flowPanel1.Margin = new Padding(0);
            flowPanel1.Name = "flowPanel1";
            flowPanel1.Size = new Size(399, 33);
            flowPanel1.TabIndex = 4;
            flowPanel1.WrapContents = false;
            // 
            // valueSelector
            // 
            valueSelector.Location = new Point(3, 3);
            valueSelector.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            valueSelector.Name = "valueSelector";
            valueSelector.Size = new Size(117, 27);
            valueSelector.TabIndex = 3;
            valueSelector.ValueChanged += OnValueSelectionChanged;
            // 
            // searchBox
            // 
            searchBox.Location = new Point(126, 3);
            searchBox.MaxLength = 20;
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Name";
            searchBox.Size = new Size(140, 27);
            searchBox.TabIndex = 4;
            searchBox.WordWrap = false;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(272, 3);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(63, 27);
            searchButton.TabIndex = 5;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += OnSearchClick;
            // 
            // closeButton
            // 
            closeButton.Location = new Point(341, 3);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(55, 27);
            closeButton.TabIndex = 2;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += OnCloseClick;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(3, 23);
            progressBar.Maximum = 65536;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(578, 23);
            progressBar.Step = 1;
            progressBar.TabIndex = 6;
            // 
            // progressLabel
            // 
            progressLabel.AutoSize = true;
            progressLabel.Dock = DockStyle.Fill;
            progressLabel.Font = new Font("Segoe UI", 11F);
            progressLabel.Location = new Point(3, 0);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(578, 20);
            progressLabel.TabIndex = 7;
            progressLabel.Text = "65536";
            progressLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Location = new Point(173, 204);
            panel1.Name = "panel1";
            panel1.Size = new Size(0, 0);
            panel1.TabIndex = 8;
            // 
            // progressLayout
            // 
            progressLayout.AutoSize = true;
            progressLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            progressLayout.ColumnCount = 1;
            progressLayout.ColumnStyles.Add(new ColumnStyle());
            progressLayout.Controls.Add(progressLabel, 0, 0);
            progressLayout.Controls.Add(progressBar, 0, 1);
            progressLayout.Dock = DockStyle.Top;
            progressLayout.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            progressLayout.Location = new Point(0, 0);
            progressLayout.Name = "progressLayout";
            progressLayout.RowCount = 2;
            progressLayout.RowStyles.Add(new RowStyle());
            progressLayout.RowStyles.Add(new RowStyle());
            progressLayout.Size = new Size(584, 49);
            progressLayout.TabIndex = 10;
            // 
            // staticSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 361);
            ControlBox = false;
            Controls.Add(progressLayout);
            Controls.Add(panel1);
            Controls.Add(listView);
            Controls.Add(toolsPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(420, 120);
            Name = "staticSelector";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Static Selector";
            toolsPanel.ResumeLayout(false);
            toolsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewImage).EndInit();
            flowPanel1.ResumeLayout(false);
            flowPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)valueSelector).EndInit();
            progressLayout.ResumeLayout(false);
            progressLayout.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView listView;
        private TableLayoutPanel toolsPanel;
        private ProgressBar progressBar;
        private PictureBox previewImage;
        private FlowLayoutPanel flowPanel1;
        private NumericUpDown valueSelector;
        private TextBox searchBox;
        private Button searchButton;
        private Button closeButton;
        private Label label1;
        private Panel panel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel progressLayout;
        private Label progressLabel;
    }
}