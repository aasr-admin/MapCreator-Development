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
            tileView = new FastTileView();
            contentTable = new TableLayoutPanel();
            previewImage = new PictureBox();
            controlsPanel = new FlowLayoutPanel();
            clearButton = new Button();
            searchBox = new TextBox();
            searchButton = new Button();
            valueSelector = new NumericUpDown();
            closeButton = new Button();
            progressBar = new FastProgressBar();
            contentTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewImage).BeginInit();
            controlsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valueSelector).BeginInit();
            SuspendLayout();
            // 
            // tileView
            // 
            tileView.Dock = DockStyle.Fill;
            tileView.Location = new Point(103, 3);
            tileView.Name = "tileView";
            tileView.Size = new Size(378, 249);
            tileView.TabIndex = 0;
            tileView.TileSize = new Size(44, 44);
            tileView.UseCompatibleStateImageBehavior = false;
            tileView.View = View.Tile;
            tileView.ItemSelectionChanged += OnItemSelectionChanged;
            tileView.DoubleClick += OnCloseClick;
            // 
            // contentTable
            // 
            contentTable.ColumnCount = 2;
            contentTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            contentTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            contentTable.Controls.Add(previewImage, 0, 0);
            contentTable.Controls.Add(tileView, 1, 0);
            contentTable.Controls.Add(controlsPanel, 1, 1);
            contentTable.Controls.Add(progressBar, 0, 1);
            contentTable.Dock = DockStyle.Fill;
            contentTable.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            contentTable.Location = new Point(0, 0);
            contentTable.Name = "contentTable";
            contentTable.RowCount = 2;
            contentTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentTable.RowStyles.Add(new RowStyle());
            contentTable.Size = new Size(484, 284);
            contentTable.TabIndex = 0;
            // 
            // previewImage
            // 
            previewImage.BorderStyle = BorderStyle.Fixed3D;
            previewImage.Dock = DockStyle.Fill;
            previewImage.Location = new Point(3, 3);
            previewImage.Name = "previewImage";
            previewImage.Size = new Size(94, 249);
            previewImage.SizeMode = PictureBoxSizeMode.CenterImage;
            previewImage.TabIndex = 0;
            previewImage.TabStop = false;
            // 
            // controlsPanel
            // 
            controlsPanel.AutoSize = true;
            controlsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlsPanel.Controls.Add(clearButton);
            controlsPanel.Controls.Add(searchBox);
            controlsPanel.Controls.Add(searchButton);
            controlsPanel.Controls.Add(valueSelector);
            controlsPanel.Controls.Add(closeButton);
            controlsPanel.Dock = DockStyle.Right;
            controlsPanel.Location = new Point(140, 255);
            controlsPanel.Margin = new Padding(0);
            controlsPanel.Name = "controlsPanel";
            controlsPanel.Size = new Size(344, 29);
            controlsPanel.TabIndex = 0;
            controlsPanel.WrapContents = false;
            // 
            // clearButton
            // 
            clearButton.BackgroundImage = (Image)resources.GetObject("clearButton.BackgroundImage");
            clearButton.BackgroundImageLayout = ImageLayout.Zoom;
            clearButton.Location = new Point(3, 3);
            clearButton.Margin = new Padding(3, 3, 0, 3);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(23, 23);
            clearButton.TabIndex = 0;
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += OnClearClick;
            // 
            // searchBox
            // 
            searchBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            searchBox.Location = new Point(26, 3);
            searchBox.Margin = new Padding(0, 3, 0, 3);
            searchBox.MaxLength = 20;
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Name";
            searchBox.Size = new Size(140, 23);
            searchBox.TabIndex = 0;
            searchBox.WordWrap = false;
            // 
            // searchButton
            // 
            searchButton.BackgroundImage = (Image)resources.GetObject("searchButton.BackgroundImage");
            searchButton.BackgroundImageLayout = ImageLayout.Zoom;
            searchButton.Location = new Point(166, 3);
            searchButton.Margin = new Padding(0, 3, 3, 3);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(23, 23);
            searchButton.TabIndex = 0;
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += OnSearchClick;
            // 
            // valueSelector
            // 
            valueSelector.Location = new Point(195, 3);
            valueSelector.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            valueSelector.Name = "valueSelector";
            valueSelector.Size = new Size(117, 23);
            valueSelector.TabIndex = 0;
            valueSelector.ValueChanged += OnValueSelectionChanged;
            // 
            // closeButton
            // 
            closeButton.BackgroundImage = (Image)resources.GetObject("closeButton.BackgroundImage");
            closeButton.BackgroundImageLayout = ImageLayout.Zoom;
            closeButton.Location = new Point(318, 3);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(23, 23);
            closeButton.TabIndex = 0;
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += OnCloseClick;
            // 
            // progressBar
            // 
            progressBar.BarColor = SystemColors.Highlight;
            progressBar.BorderStyle = BorderStyle.FixedSingle;
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(3, 258);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(94, 23);
            progressBar.TabIndex = 0;
            progressBar.Text = "";
            // 
            // staticSelector
            // 
            AcceptButton = closeButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = closeButton;
            ClientSize = new Size(484, 284);
            ControlBox = false;
            Controls.Add(contentTable);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(500, 300);
            Name = "staticSelector";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Static Selector";
            contentTable.ResumeLayout(false);
            contentTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)previewImage).EndInit();
            controlsPanel.ResumeLayout(false);
            controlsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)valueSelector).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private FastTileView tileView;
        private TableLayoutPanel contentTable;
        private PictureBox previewImage;
        private FlowLayoutPanel controlsPanel;
        private NumericUpDown valueSelector;
        private TextBox searchBox;
        private Button searchButton;
        private Button closeButton;
        private Button clearButton;
        private FastProgressBar progressBar;
    }
}