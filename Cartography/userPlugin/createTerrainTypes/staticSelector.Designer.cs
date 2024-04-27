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
            components = new System.ComponentModel.Container();
            closeButton = new Button();
            valueSelector = new NumericUpDown();
            flowPanel1 = new FlowLayoutPanel();
            searchBox = new TextBox();
            searchButton = new Button();
            imageList = new ImageList(components);
            listView = new ListView();
            toolsPanel = new TableLayoutPanel();
            flowPanel2 = new FlowLayoutPanel();
            progressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)valueSelector).BeginInit();
            flowPanel1.SuspendLayout();
            toolsPanel.SuspendLayout();
            flowPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.AutoSize = true;
            closeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            closeButton.Location = new Point(8, 3);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(55, 30);
            closeButton.TabIndex = 2;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += OnCloseClick;
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
            // flowPanel1
            // 
            flowPanel1.AutoSize = true;
            flowPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowPanel1.Controls.Add(valueSelector);
            flowPanel1.Controls.Add(searchBox);
            flowPanel1.Controls.Add(searchButton);
            flowPanel1.Dock = DockStyle.Fill;
            flowPanel1.Location = new Point(0, 0);
            flowPanel1.Margin = new Padding(0);
            flowPanel1.Name = "flowPanel1";
            flowPanel1.Size = new Size(338, 36);
            flowPanel1.TabIndex = 4;
            flowPanel1.WrapContents = false;
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
            searchButton.AutoSize = true;
            searchButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            searchButton.Location = new Point(272, 3);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(63, 30);
            searchButton.TabIndex = 5;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth16Bit;
            imageList.ImageSize = new Size(64, 64);
            imageList.TransparentColor = Color.Transparent;
            // 
            // listView
            // 
            listView.Alignment = ListViewAlignment.SnapToGrid;
            listView.Dock = DockStyle.Fill;
            listView.GridLines = true;
            listView.HeaderStyle = ColumnHeaderStyle.None;
            listView.LargeImageList = imageList;
            listView.Location = new Point(0, 0);
            listView.MultiSelect = false;
            listView.Name = "listView";
            listView.ShowGroups = false;
            listView.Size = new Size(404, 368);
            listView.SmallImageList = imageList;
            listView.Sorting = SortOrder.Ascending;
            listView.TabIndex = 0;
            listView.TileSize = new Size(64, 64);
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Tile;
            listView.VirtualListSize = 65535;
            listView.ItemSelectionChanged += OnItemSelectionChanged;
            // 
            // toolsPanel
            // 
            toolsPanel.AutoSize = true;
            toolsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            toolsPanel.ColumnCount = 2;
            toolsPanel.ColumnStyles.Add(new ColumnStyle());
            toolsPanel.ColumnStyles.Add(new ColumnStyle());
            toolsPanel.Controls.Add(flowPanel1, 0, 0);
            toolsPanel.Controls.Add(flowPanel2, 1, 0);
            toolsPanel.Dock = DockStyle.Bottom;
            toolsPanel.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            toolsPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            toolsPanel.Location = new Point(0, 368);
            toolsPanel.Name = "toolsPanel";
            toolsPanel.RowCount = 1;
            toolsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            toolsPanel.Size = new Size(404, 36);
            toolsPanel.TabIndex = 5;
            // 
            // flowPanel2
            // 
            flowPanel2.AutoSize = true;
            flowPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowPanel2.Controls.Add(closeButton);
            flowPanel2.Dock = DockStyle.Fill;
            flowPanel2.FlowDirection = FlowDirection.RightToLeft;
            flowPanel2.Location = new Point(338, 0);
            flowPanel2.Margin = new Padding(0);
            flowPanel2.Name = "flowPanel2";
            flowPanel2.Size = new Size(66, 36);
            flowPanel2.TabIndex = 5;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Top;
            progressBar.Location = new Point(0, 0);
            progressBar.Maximum = 65535;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(404, 23);
            progressBar.Step = 1;
            progressBar.TabIndex = 6;
            // 
            // staticSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(404, 404);
            ControlBox = false;
            Controls.Add(progressBar);
            Controls.Add(listView);
            Controls.Add(toolsPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(420, 120);
            Name = "staticSelector";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)valueSelector).EndInit();
            flowPanel1.ResumeLayout(false);
            flowPanel1.PerformLayout();
            toolsPanel.ResumeLayout(false);
            toolsPanel.PerformLayout();
            flowPanel2.ResumeLayout(false);
            flowPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button closeButton;
        private NumericUpDown valueSelector;
        private FlowLayoutPanel flowPanel1;
        private TextBox searchBox;
        private Button searchButton;
        private ImageList imageList;
        private ListView listView;
        private TableLayoutPanel toolsPanel;
        private FlowLayoutPanel flowPanel2;
        private ProgressBar progressBar;
    }
}