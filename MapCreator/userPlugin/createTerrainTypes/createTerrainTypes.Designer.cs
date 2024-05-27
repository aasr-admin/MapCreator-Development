
namespace MapCreator
{
    partial class CreateTerrainTypes
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTerrainTypes));
            headerMenu = new MenuStrip();
            headerTerrainNewButton = new ToolStripMenuItem();
            headerTerrainLoadButton = new ToolStripMenuItem();
            headerTerrainSaveButton = new ToolStripMenuItem();
            headerCreditsButton = new ToolStripMenuItem();
            headerFacetBuilderButton = new ToolStripMenuItem();
            headerDivider = new PictureBox();
            contentTabs = new TabControl();
            titlePage = new TabPage();
            titleBanner = new PictureBox();
            titleCreditsDecorTop = new PictureBox();
            titleCreditsText = new Label();
            titleCreditsDecorBottom = new PictureBox();
            terrainPage = new TabPage();
            terrainPageLayout = new SplitContainer();
            terrainContentContainer = new Panel();
            terrainPreviewView = new Panel();
            terrainPreviewGrid = new ResponsivePanel();
            terrainPreviewMarker = new Panel();
            terrainInputLayout = new TableLayoutPanel();
            terrainTypeHint = new Label();
            terrainTypeValue = new TextBox();
            terrainBaseHint = new Label();
            terrainBaseValue = new ComboBox();
            staticTabsContainer = new Panel();
            staticTabs = new TabControl();
            staticEntriesPage = new TabPage();
            staticEntriesView = new ResponsiveGridView();
            staticEntriesDescriptionColumn = new DataGridViewTextBoxColumn();
            staticEntriesFrequencyColumn = new DataGridViewTextBoxColumn();
            staticEntriesCountColumn = new DataGridViewTextBoxColumn();
            staticEntriesBindingSource = new BindingSource(components);
            staticComponentsPage = new TabPage();
            staticComponentsLayout = new SplitContainer();
            staticComponentsView = new ResponsiveGridView();
            staticComponentsTileIDColumn = new DataGridViewTextBoxColumn();
            staticComponentsXColumn = new DataGridViewTextBoxColumn();
            staticComponentsYColumn = new DataGridViewTextBoxColumn();
            staticComponentsZColumn = new DataGridViewTextBoxColumn();
            staticComponentsHueColumn = new DataGridViewTextBoxColumn();
            staticComponentsBindingSource = new BindingSource(components);
            staticPropertiesView = new PropertyGrid();
            staticSelectorDescription = new TextBox();
            staticSelectorValue = new VScrollBar();
            staticComponentsPreviewContainer = new Panel();
            staticSelectorPreview = new PictureBox();
            staticSelectorButton = new Button();
            footerDivider = new PictureBox();
            footerStatus = new StatusStrip();
            versionInfo = new ToolStripStatusLabel();
            versionNumber = new ToolStripStatusLabel();
            contentTabsContainer = new Panel();
            headerMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)headerDivider).BeginInit();
            contentTabs.SuspendLayout();
            titlePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)titleBanner).BeginInit();
            ((System.ComponentModel.ISupportInitialize)titleCreditsDecorTop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)titleCreditsDecorBottom).BeginInit();
            terrainPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)terrainPageLayout).BeginInit();
            terrainPageLayout.Panel1.SuspendLayout();
            terrainPageLayout.Panel2.SuspendLayout();
            terrainPageLayout.SuspendLayout();
            terrainContentContainer.SuspendLayout();
            terrainPreviewView.SuspendLayout();
            terrainInputLayout.SuspendLayout();
            staticTabsContainer.SuspendLayout();
            staticTabs.SuspendLayout();
            staticEntriesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)staticEntriesView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)staticEntriesBindingSource).BeginInit();
            staticComponentsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)staticComponentsLayout).BeginInit();
            staticComponentsLayout.Panel1.SuspendLayout();
            staticComponentsLayout.Panel2.SuspendLayout();
            staticComponentsLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)staticComponentsView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)staticComponentsBindingSource).BeginInit();
            staticComponentsPreviewContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)staticSelectorPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)footerDivider).BeginInit();
            footerStatus.SuspendLayout();
            contentTabsContainer.SuspendLayout();
            SuspendLayout();
            // 
            // headerMenu
            // 
            headerMenu.ImageScalingSize = new Size(24, 24);
            headerMenu.Items.AddRange(new ToolStripItem[] { headerTerrainNewButton, headerTerrainLoadButton, headerTerrainSaveButton, headerCreditsButton, headerFacetBuilderButton });
            headerMenu.Location = new Point(0, 0);
            headerMenu.Name = "headerMenu";
            headerMenu.Size = new Size(784, 32);
            headerMenu.TabIndex = 3;
            // 
            // headerTerrainNewButton
            // 
            headerTerrainNewButton.Image = (Image)resources.GetObject("headerTerrainNewButton.Image");
            headerTerrainNewButton.Name = "headerTerrainNewButton";
            headerTerrainNewButton.Size = new Size(36, 28);
            headerTerrainNewButton.ToolTipText = "New Terrain Type";
            headerTerrainNewButton.Click += OnTerrainNewButtonClick;
            // 
            // headerTerrainLoadButton
            // 
            headerTerrainLoadButton.Image = (Image)resources.GetObject("headerTerrainLoadButton.Image");
            headerTerrainLoadButton.Name = "headerTerrainLoadButton";
            headerTerrainLoadButton.Size = new Size(36, 28);
            headerTerrainLoadButton.ToolTipText = "Load Terrain Type";
            headerTerrainLoadButton.Click += OnTerrainLoadButtonClick;
            // 
            // headerTerrainSaveButton
            // 
            headerTerrainSaveButton.Image = (Image)resources.GetObject("headerTerrainSaveButton.Image");
            headerTerrainSaveButton.Name = "headerTerrainSaveButton";
            headerTerrainSaveButton.Size = new Size(36, 28);
            headerTerrainSaveButton.ToolTipText = "Save Terrain Type";
            headerTerrainSaveButton.Click += OnTerrainSaveButtonClick;
            // 
            // headerCreditsButton
            // 
            headerCreditsButton.Alignment = ToolStripItemAlignment.Right;
            headerCreditsButton.Image = (Image)resources.GetObject("headerCreditsButton.Image");
            headerCreditsButton.Name = "headerCreditsButton";
            headerCreditsButton.Size = new Size(36, 28);
            headerCreditsButton.ToolTipText = "Community Credits";
            headerCreditsButton.Click += OnCommunityCreditsButtonClick;
            // 
            // headerFacetBuilderButton
            // 
            headerFacetBuilderButton.Alignment = ToolStripItemAlignment.Right;
            headerFacetBuilderButton.Image = (Image)resources.GetObject("headerFacetBuilderButton.Image");
            headerFacetBuilderButton.Name = "headerFacetBuilderButton";
            headerFacetBuilderButton.Size = new Size(36, 28);
            headerFacetBuilderButton.ToolTipText = "Facet Builder";
            headerFacetBuilderButton.Click += OnFacetBuilderButtonClick;
            // 
            // headerDivider
            // 
            headerDivider.BackColor = SystemColors.ActiveCaptionText;
            headerDivider.Dock = DockStyle.Top;
            headerDivider.Image = (Image)resources.GetObject("headerDivider.Image");
            headerDivider.ImeMode = ImeMode.NoControl;
            headerDivider.Location = new Point(0, 32);
            headerDivider.Name = "headerDivider";
            headerDivider.Size = new Size(784, 12);
            headerDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            headerDivider.TabIndex = 2;
            headerDivider.TabStop = false;
            // 
            // contentTabs
            // 
            contentTabs.Appearance = TabAppearance.Buttons;
            contentTabs.Controls.Add(titlePage);
            contentTabs.Controls.Add(terrainPage);
            contentTabs.Dock = DockStyle.Fill;
            contentTabs.Location = new Point(0, 3);
            contentTabs.Name = "contentTabs";
            contentTabs.SelectedIndex = 0;
            contentTabs.Size = new Size(784, 380);
            contentTabs.SizeMode = TabSizeMode.FillToRight;
            contentTabs.TabIndex = 0;
            // 
            // titlePage
            // 
            titlePage.BackColor = Color.Black;
            titlePage.Controls.Add(titleBanner);
            titlePage.Controls.Add(titleCreditsDecorTop);
            titlePage.Controls.Add(titleCreditsText);
            titlePage.Controls.Add(titleCreditsDecorBottom);
            titlePage.Location = new Point(4, 27);
            titlePage.Name = "titlePage";
            titlePage.Size = new Size(776, 349);
            titlePage.TabIndex = 0;
            titlePage.Text = "Intro";
            // 
            // titleBanner
            // 
            titleBanner.Image = (Image)resources.GetObject("titleBanner.Image");
            titleBanner.Location = new Point(35, 59);
            titleBanner.Name = "titleBanner";
            titleBanner.Size = new Size(446, 76);
            titleBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            titleBanner.TabIndex = 0;
            titleBanner.TabStop = false;
            // 
            // titleCreditsDecorTop
            // 
            titleCreditsDecorTop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            titleCreditsDecorTop.Image = (Image)resources.GetObject("titleCreditsDecorTop.Image");
            titleCreditsDecorTop.Location = new Point(229, 287);
            titleCreditsDecorTop.Name = "titleCreditsDecorTop";
            titleCreditsDecorTop.Size = new Size(530, 5);
            titleCreditsDecorTop.SizeMode = PictureBoxSizeMode.StretchImage;
            titleCreditsDecorTop.TabIndex = 1;
            titleCreditsDecorTop.TabStop = false;
            // 
            // titleCreditsText
            // 
            titleCreditsText.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            titleCreditsText.AutoSize = true;
            titleCreditsText.Font = new Font("Segoe UI", 15F);
            titleCreditsText.ForeColor = SystemColors.ControlDarkDark;
            titleCreditsText.Location = new Point(271, 297);
            titleCreditsText.Name = "titleCreditsText";
            titleCreditsText.Size = new Size(440, 28);
            titleCreditsText.TabIndex = 2;
            titleCreditsText.Text = "Developer(s): DKnight (Creator of UOLandscaper)";
            // 
            // titleCreditsDecorBottom
            // 
            titleCreditsDecorBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            titleCreditsDecorBottom.Image = (Image)resources.GetObject("titleCreditsDecorBottom.Image");
            titleCreditsDecorBottom.Location = new Point(229, 334);
            titleCreditsDecorBottom.Name = "titleCreditsDecorBottom";
            titleCreditsDecorBottom.Size = new Size(530, 5);
            titleCreditsDecorBottom.SizeMode = PictureBoxSizeMode.StretchImage;
            titleCreditsDecorBottom.TabIndex = 3;
            titleCreditsDecorBottom.TabStop = false;
            // 
            // terrainPage
            // 
            terrainPage.Controls.Add(terrainPageLayout);
            terrainPage.Location = new Point(4, 27);
            terrainPage.Name = "terrainPage";
            terrainPage.Size = new Size(776, 349);
            terrainPage.TabIndex = 1;
            terrainPage.Text = "Configure Terrain";
            terrainPage.UseVisualStyleBackColor = true;
            terrainPage.Enter += OnTerrainPageEnter;
            terrainPage.Leave += OnTerrainPageLeave;
            // 
            // terrainPageLayout
            // 
            terrainPageLayout.BorderStyle = BorderStyle.Fixed3D;
            terrainPageLayout.Dock = DockStyle.Fill;
            terrainPageLayout.FixedPanel = FixedPanel.Panel2;
            terrainPageLayout.Location = new Point(0, 0);
            terrainPageLayout.Margin = new Padding(0);
            terrainPageLayout.Name = "terrainPageLayout";
            // 
            // terrainPageLayout.Panel1
            // 
            terrainPageLayout.Panel1.Controls.Add(terrainContentContainer);
            terrainPageLayout.Panel1MinSize = 450;
            // 
            // terrainPageLayout.Panel2
            // 
            terrainPageLayout.Panel2.Controls.Add(staticTabsContainer);
            terrainPageLayout.Panel2MinSize = 150;
            terrainPageLayout.Size = new Size(776, 349);
            terrainPageLayout.SplitterDistance = 450;
            terrainPageLayout.TabIndex = 0;
            // 
            // terrainContentContainer
            // 
            terrainContentContainer.Controls.Add(terrainPreviewView);
            terrainContentContainer.Controls.Add(terrainInputLayout);
            terrainContentContainer.Dock = DockStyle.Fill;
            terrainContentContainer.Location = new Point(0, 0);
            terrainContentContainer.Name = "terrainContentContainer";
            terrainContentContainer.Size = new Size(446, 345);
            terrainContentContainer.TabIndex = 2;
            // 
            // terrainPreviewView
            // 
            terrainPreviewView.AutoScroll = true;
            terrainPreviewView.AutoScrollMargin = new Size(44, 44);
            terrainPreviewView.BackColor = Color.Black;
            terrainPreviewView.Controls.Add(terrainPreviewGrid);
            terrainPreviewView.Controls.Add(terrainPreviewMarker);
            terrainPreviewView.Dock = DockStyle.Fill;
            terrainPreviewView.Location = new Point(0, 58);
            terrainPreviewView.Name = "terrainPreviewView";
            terrainPreviewView.Size = new Size(446, 287);
            terrainPreviewView.TabIndex = 0;
            // 
            // terrainPreviewGrid
            // 
            terrainPreviewGrid.BackColor = Color.Transparent;
            terrainPreviewGrid.Location = new Point(0, 0);
            terrainPreviewGrid.Margin = new Padding(0);
            terrainPreviewGrid.Name = "terrainPreviewGrid";
            terrainPreviewGrid.Size = new Size(11220, 11220);
            terrainPreviewGrid.TabIndex = 1;
            terrainPreviewGrid.Paint += OnTerrainPreviewGridPaint;
            terrainPreviewGrid.MouseClick += OnTerrainPreviewGridMouseClick;
            // 
            // terrainPreviewMarker
            // 
            terrainPreviewMarker.BackColor = Color.Transparent;
            terrainPreviewMarker.Location = new Point(0, 0);
            terrainPreviewMarker.Margin = new Padding(0);
            terrainPreviewMarker.MaximumSize = new Size(44, 44);
            terrainPreviewMarker.MinimumSize = new Size(44, 44);
            terrainPreviewMarker.Name = "terrainPreviewMarker";
            terrainPreviewMarker.Size = new Size(44, 44);
            terrainPreviewMarker.TabIndex = 0;
            // 
            // terrainInputLayout
            // 
            terrainInputLayout.AutoSize = true;
            terrainInputLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            terrainInputLayout.ColumnCount = 3;
            terrainInputLayout.ColumnStyles.Add(new ColumnStyle());
            terrainInputLayout.ColumnStyles.Add(new ColumnStyle());
            terrainInputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            terrainInputLayout.Controls.Add(terrainTypeHint, 0, 0);
            terrainInputLayout.Controls.Add(terrainTypeValue, 1, 0);
            terrainInputLayout.Controls.Add(terrainBaseHint, 0, 1);
            terrainInputLayout.Controls.Add(terrainBaseValue, 1, 1);
            terrainInputLayout.Dock = DockStyle.Top;
            terrainInputLayout.Location = new Point(0, 0);
            terrainInputLayout.Name = "terrainInputLayout";
            terrainInputLayout.RowCount = 2;
            terrainInputLayout.RowStyles.Add(new RowStyle());
            terrainInputLayout.RowStyles.Add(new RowStyle());
            terrainInputLayout.Size = new Size(446, 58);
            terrainInputLayout.TabIndex = 1;
            // 
            // terrainTypeHint
            // 
            terrainTypeHint.Dock = DockStyle.Fill;
            terrainTypeHint.ImageAlign = ContentAlignment.MiddleRight;
            terrainTypeHint.Location = new Point(3, 0);
            terrainTypeHint.Name = "terrainTypeHint";
            terrainTypeHint.Size = new Size(72, 29);
            terrainTypeHint.TabIndex = 0;
            terrainTypeHint.Text = "Terrain Type";
            terrainTypeHint.TextAlign = ContentAlignment.MiddleRight;
            // 
            // terrainTypeValue
            // 
            terrainTypeValue.Dock = DockStyle.Fill;
            terrainTypeValue.Location = new Point(81, 3);
            terrainTypeValue.Name = "terrainTypeValue";
            terrainTypeValue.Size = new Size(239, 23);
            terrainTypeValue.TabIndex = 1;
            // 
            // terrainBaseHint
            // 
            terrainBaseHint.Dock = DockStyle.Fill;
            terrainBaseHint.Location = new Point(3, 29);
            terrainBaseHint.Name = "terrainBaseHint";
            terrainBaseHint.Size = new Size(72, 29);
            terrainBaseHint.TabIndex = 2;
            terrainBaseHint.Text = "Terrain Base";
            terrainBaseHint.TextAlign = ContentAlignment.MiddleRight;
            // 
            // terrainBaseValue
            // 
            terrainBaseValue.Dock = DockStyle.Fill;
            terrainBaseValue.DropDownStyle = ComboBoxStyle.DropDownList;
            terrainBaseValue.FormattingEnabled = true;
            terrainBaseValue.Location = new Point(81, 32);
            terrainBaseValue.Name = "terrainBaseValue";
            terrainBaseValue.Size = new Size(239, 23);
            terrainBaseValue.Sorted = true;
            terrainBaseValue.TabIndex = 3;
            terrainBaseValue.SelectedIndexChanged += OnBaseTerrainSelectionChanged;
            // 
            // staticTabsContainer
            // 
            staticTabsContainer.Controls.Add(staticTabs);
            staticTabsContainer.Dock = DockStyle.Fill;
            staticTabsContainer.Location = new Point(0, 0);
            staticTabsContainer.Name = "staticTabsContainer";
            staticTabsContainer.Padding = new Padding(0, 3, 0, 0);
            staticTabsContainer.Size = new Size(318, 345);
            staticTabsContainer.TabIndex = 1;
            // 
            // staticTabs
            // 
            staticTabs.Appearance = TabAppearance.Buttons;
            staticTabs.Controls.Add(staticEntriesPage);
            staticTabs.Controls.Add(staticComponentsPage);
            staticTabs.Dock = DockStyle.Fill;
            staticTabs.Location = new Point(0, 3);
            staticTabs.Name = "staticTabs";
            staticTabs.SelectedIndex = 0;
            staticTabs.Size = new Size(318, 342);
            staticTabs.SizeMode = TabSizeMode.FillToRight;
            staticTabs.TabIndex = 0;
            // 
            // staticEntriesPage
            // 
            staticEntriesPage.Controls.Add(staticEntriesView);
            staticEntriesPage.Location = new Point(4, 27);
            staticEntriesPage.Name = "staticEntriesPage";
            staticEntriesPage.Size = new Size(310, 311);
            staticEntriesPage.TabIndex = 0;
            staticEntriesPage.Text = "Static Entries";
            staticEntriesPage.UseVisualStyleBackColor = true;
            // 
            // staticEntriesView
            // 
            staticEntriesView.AllowUserToResizeColumns = false;
            staticEntriesView.AllowUserToResizeRows = false;
            staticEntriesView.AutoGenerateColumns = false;
            staticEntriesView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            staticEntriesView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            staticEntriesView.BorderStyle = BorderStyle.None;
            staticEntriesView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            staticEntriesView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            staticEntriesView.Columns.AddRange(new DataGridViewColumn[] { staticEntriesDescriptionColumn, staticEntriesFrequencyColumn, staticEntriesCountColumn });
            staticEntriesView.DataSource = staticEntriesBindingSource;
            staticEntriesView.Dock = DockStyle.Fill;
            staticEntriesView.Location = new Point(0, 0);
            staticEntriesView.MultiSelect = false;
            staticEntriesView.Name = "staticEntriesView";
            staticEntriesView.RowHeadersVisible = false;
            staticEntriesView.RowHeadersWidth = 20;
            staticEntriesView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            staticEntriesView.SelectedItem = null;
            staticEntriesView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            staticEntriesView.ShowCellToolTips = false;
            staticEntriesView.Size = new Size(310, 311);
            staticEntriesView.TabIndex = 0;
            staticEntriesView.RowsAdded += OnStaticEntriesRowsChanged;
            staticEntriesView.RowsRemoved += OnStaticEntriesRowsChanged;
            staticEntriesView.SelectionChanged += OnStaticEntriesSelectionChanged;
            // 
            // staticEntriesDescriptionColumn
            // 
            staticEntriesDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staticEntriesDescriptionColumn.DataPropertyName = "Description";
            staticEntriesDescriptionColumn.HeaderText = "Description";
            staticEntriesDescriptionColumn.MaxInputLength = 128;
            staticEntriesDescriptionColumn.Name = "staticEntriesDescriptionColumn";
            staticEntriesDescriptionColumn.Resizable = DataGridViewTriState.False;
            // 
            // staticEntriesFrequencyColumn
            // 
            staticEntriesFrequencyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            staticEntriesFrequencyColumn.DataPropertyName = "Frequency";
            staticEntriesFrequencyColumn.HeaderText = "Frequency";
            staticEntriesFrequencyColumn.MaxInputLength = 3;
            staticEntriesFrequencyColumn.Name = "staticEntriesFrequencyColumn";
            staticEntriesFrequencyColumn.Resizable = DataGridViewTriState.False;
            staticEntriesFrequencyColumn.Width = 87;
            // 
            // staticEntriesCountColumn
            // 
            staticEntriesCountColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            staticEntriesCountColumn.DataPropertyName = "Count";
            staticEntriesCountColumn.HeaderText = "Count";
            staticEntriesCountColumn.Name = "staticEntriesCountColumn";
            staticEntriesCountColumn.ReadOnly = true;
            staticEntriesCountColumn.Width = 65;
            // 
            // staticEntriesBindingSource
            // 
            staticEntriesBindingSource.DataSource = typeof(RandomStatics);
            // 
            // staticComponentsPage
            // 
            staticComponentsPage.Controls.Add(staticComponentsLayout);
            staticComponentsPage.Location = new Point(4, 27);
            staticComponentsPage.Name = "staticComponentsPage";
            staticComponentsPage.Size = new Size(310, 311);
            staticComponentsPage.TabIndex = 1;
            staticComponentsPage.Text = "Entry Components";
            // 
            // staticComponentsLayout
            // 
            staticComponentsLayout.BorderStyle = BorderStyle.Fixed3D;
            staticComponentsLayout.Dock = DockStyle.Fill;
            staticComponentsLayout.FixedPanel = FixedPanel.Panel2;
            staticComponentsLayout.Location = new Point(0, 0);
            staticComponentsLayout.Name = "staticComponentsLayout";
            staticComponentsLayout.Orientation = Orientation.Horizontal;
            // 
            // staticComponentsLayout.Panel1
            // 
            staticComponentsLayout.Panel1.Controls.Add(staticComponentsView);
            staticComponentsLayout.Panel1MinSize = 100;
            // 
            // staticComponentsLayout.Panel2
            // 
            staticComponentsLayout.Panel2.Controls.Add(staticPropertiesView);
            staticComponentsLayout.Panel2.Controls.Add(staticSelectorDescription);
            staticComponentsLayout.Panel2.Controls.Add(staticSelectorValue);
            staticComponentsLayout.Panel2.Controls.Add(staticComponentsPreviewContainer);
            staticComponentsLayout.Panel2.Controls.Add(staticSelectorButton);
            staticComponentsLayout.Panel2MinSize = 160;
            staticComponentsLayout.Size = new Size(310, 311);
            staticComponentsLayout.SplitterDistance = 147;
            staticComponentsLayout.TabIndex = 2;
            // 
            // staticComponentsView
            // 
            staticComponentsView.AllowUserToResizeColumns = false;
            staticComponentsView.AllowUserToResizeRows = false;
            staticComponentsView.AutoGenerateColumns = false;
            staticComponentsView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            staticComponentsView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            staticComponentsView.BorderStyle = BorderStyle.None;
            staticComponentsView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            staticComponentsView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            staticComponentsView.Columns.AddRange(new DataGridViewColumn[] { staticComponentsTileIDColumn, staticComponentsXColumn, staticComponentsYColumn, staticComponentsZColumn, staticComponentsHueColumn });
            staticComponentsView.DataSource = staticComponentsBindingSource;
            staticComponentsView.Dock = DockStyle.Fill;
            staticComponentsView.Location = new Point(0, 0);
            staticComponentsView.MultiSelect = false;
            staticComponentsView.Name = "staticComponentsView";
            staticComponentsView.RowHeadersVisible = false;
            staticComponentsView.RowHeadersWidth = 20;
            staticComponentsView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            staticComponentsView.SelectedItem = null;
            staticComponentsView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            staticComponentsView.ShowCellToolTips = false;
            staticComponentsView.Size = new Size(306, 143);
            staticComponentsView.TabIndex = 1;
            staticComponentsView.RowsAdded += OnStaticComponentsRowsChanged;
            staticComponentsView.RowsRemoved += OnStaticComponentsRowsChanged;
            staticComponentsView.SelectionChanged += OnStaticComponentsSelectionChanged;
            // 
            // staticComponentsTileIDColumn
            // 
            staticComponentsTileIDColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staticComponentsTileIDColumn.DataPropertyName = "TileID";
            staticComponentsTileIDColumn.HeaderText = "TileID";
            staticComponentsTileIDColumn.MaxInputLength = 5;
            staticComponentsTileIDColumn.Name = "staticComponentsTileIDColumn";
            staticComponentsTileIDColumn.Resizable = DataGridViewTriState.False;
            // 
            // staticComponentsXColumn
            // 
            staticComponentsXColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            staticComponentsXColumn.DataPropertyName = "X";
            staticComponentsXColumn.HeaderText = "X";
            staticComponentsXColumn.MaxInputLength = 3;
            staticComponentsXColumn.Name = "staticComponentsXColumn";
            staticComponentsXColumn.Resizable = DataGridViewTriState.False;
            staticComponentsXColumn.Width = 39;
            // 
            // staticComponentsYColumn
            // 
            staticComponentsYColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            staticComponentsYColumn.DataPropertyName = "Y";
            staticComponentsYColumn.HeaderText = "Y";
            staticComponentsYColumn.MaxInputLength = 3;
            staticComponentsYColumn.Name = "staticComponentsYColumn";
            staticComponentsYColumn.Resizable = DataGridViewTriState.False;
            staticComponentsYColumn.Width = 39;
            // 
            // staticComponentsZColumn
            // 
            staticComponentsZColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            staticComponentsZColumn.DataPropertyName = "Z";
            staticComponentsZColumn.HeaderText = "Z";
            staticComponentsZColumn.MaxInputLength = 3;
            staticComponentsZColumn.Name = "staticComponentsZColumn";
            staticComponentsZColumn.Resizable = DataGridViewTriState.False;
            staticComponentsZColumn.Width = 39;
            // 
            // staticComponentsHueColumn
            // 
            staticComponentsHueColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            staticComponentsHueColumn.DataPropertyName = "Hue";
            staticComponentsHueColumn.HeaderText = "Hue";
            staticComponentsHueColumn.MaxInputLength = 4;
            staticComponentsHueColumn.Name = "staticComponentsHueColumn";
            staticComponentsHueColumn.Resizable = DataGridViewTriState.False;
            // 
            // staticComponentsBindingSource
            // 
            staticComponentsBindingSource.DataSource = typeof(RandomStaticCollection);
            // 
            // staticPropertiesView
            // 
            staticPropertiesView.Dock = DockStyle.Fill;
            staticPropertiesView.HelpVisible = false;
            staticPropertiesView.Location = new Point(132, 35);
            staticPropertiesView.Name = "staticPropertiesView";
            staticPropertiesView.PropertySort = PropertySort.Alphabetical;
            staticPropertiesView.Size = new Size(174, 98);
            staticPropertiesView.TabIndex = 4;
            staticPropertiesView.ToolbarVisible = false;
            // 
            // staticSelectorDescription
            // 
            staticSelectorDescription.Dock = DockStyle.Bottom;
            staticSelectorDescription.Location = new Point(132, 133);
            staticSelectorDescription.Name = "staticSelectorDescription";
            staticSelectorDescription.ReadOnly = true;
            staticSelectorDescription.Size = new Size(174, 23);
            staticSelectorDescription.TabIndex = 3;
            staticSelectorDescription.WordWrap = false;
            // 
            // staticSelectorValue
            // 
            staticSelectorValue.Dock = DockStyle.Left;
            staticSelectorValue.Location = new Point(115, 35);
            staticSelectorValue.Maximum = 65535;
            staticSelectorValue.Name = "staticSelectorValue";
            staticSelectorValue.Size = new Size(17, 121);
            staticSelectorValue.TabIndex = 1;
            staticSelectorValue.ValueChanged += OnStaticSelectorValueChanged;
            // 
            // staticComponentsPreviewContainer
            // 
            staticComponentsPreviewContainer.BorderStyle = BorderStyle.FixedSingle;
            staticComponentsPreviewContainer.Controls.Add(staticSelectorPreview);
            staticComponentsPreviewContainer.Dock = DockStyle.Left;
            staticComponentsPreviewContainer.Location = new Point(0, 35);
            staticComponentsPreviewContainer.Name = "staticComponentsPreviewContainer";
            staticComponentsPreviewContainer.Padding = new Padding(3);
            staticComponentsPreviewContainer.Size = new Size(115, 121);
            staticComponentsPreviewContainer.TabIndex = 5;
            // 
            // staticSelectorPreview
            // 
            staticSelectorPreview.Cursor = Cursors.Hand;
            staticSelectorPreview.Dock = DockStyle.Fill;
            staticSelectorPreview.Location = new Point(3, 3);
            staticSelectorPreview.Name = "staticSelectorPreview";
            staticSelectorPreview.Size = new Size(107, 113);
            staticSelectorPreview.SizeMode = PictureBoxSizeMode.Zoom;
            staticSelectorPreview.TabIndex = 2;
            staticSelectorPreview.TabStop = false;
            staticSelectorPreview.Click += OnStaticSelectorButtonClick;
            staticSelectorPreview.MouseEnter += OnStaticSelectorButtonMouseEnter;
            staticSelectorPreview.MouseLeave += OnStaticSelectorButtonMouseLeave;
            // 
            // staticSelectorButton
            // 
            staticSelectorButton.AutoSize = true;
            staticSelectorButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            staticSelectorButton.BackgroundImage = (Image)resources.GetObject("staticSelectorButton.BackgroundImage");
            staticSelectorButton.BackgroundImageLayout = ImageLayout.Stretch;
            staticSelectorButton.Cursor = Cursors.Hand;
            staticSelectorButton.Dock = DockStyle.Top;
            staticSelectorButton.FlatAppearance.BorderSize = 0;
            staticSelectorButton.FlatStyle = FlatStyle.Flat;
            staticSelectorButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            staticSelectorButton.ForeColor = Color.PaleGoldenrod;
            staticSelectorButton.Image = (Image)resources.GetObject("staticSelectorButton.Image");
            staticSelectorButton.ImageAlign = ContentAlignment.MiddleLeft;
            staticSelectorButton.Location = new Point(0, 0);
            staticSelectorButton.Name = "staticSelectorButton";
            staticSelectorButton.Size = new Size(306, 35);
            staticSelectorButton.TabIndex = 0;
            staticSelectorButton.Text = "Static Browser";
            staticSelectorButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            staticSelectorButton.UseVisualStyleBackColor = true;
            staticSelectorButton.Click += OnStaticSelectorButtonClick;
            staticSelectorButton.MouseEnter += OnStaticSelectorButtonMouseEnter;
            staticSelectorButton.MouseLeave += OnStaticSelectorButtonMouseLeave;
            // 
            // footerDivider
            // 
            footerDivider.BackColor = SystemColors.ActiveCaptionText;
            footerDivider.Dock = DockStyle.Bottom;
            footerDivider.Image = (Image)resources.GetObject("footerDivider.Image");
            footerDivider.ImeMode = ImeMode.NoControl;
            footerDivider.Location = new Point(0, 427);
            footerDivider.Name = "footerDivider";
            footerDivider.Size = new Size(784, 12);
            footerDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            footerDivider.TabIndex = 1;
            footerDivider.TabStop = false;
            // 
            // footerStatus
            // 
            footerStatus.AllowMerge = false;
            footerStatus.Items.AddRange(new ToolStripItem[] { versionInfo, versionNumber });
            footerStatus.Location = new Point(0, 439);
            footerStatus.Name = "footerStatus";
            footerStatus.Size = new Size(784, 22);
            footerStatus.SizingGrip = false;
            footerStatus.TabIndex = 4;
            // 
            // versionInfo
            // 
            versionInfo.Name = "versionInfo";
            versionInfo.Size = new Size(73, 17);
            versionInfo.Text = "Map Creator";
            // 
            // versionNumber
            // 
            versionNumber.Name = "versionNumber";
            versionNumber.Size = new Size(22, 17);
            versionNumber.Text = "3.5";
            // 
            // contentTabsContainer
            // 
            contentTabsContainer.Controls.Add(contentTabs);
            contentTabsContainer.Dock = DockStyle.Fill;
            contentTabsContainer.Location = new Point(0, 44);
            contentTabsContainer.Name = "contentTabsContainer";
            contentTabsContainer.Padding = new Padding(0, 3, 0, 0);
            contentTabsContainer.Size = new Size(784, 383);
            contentTabsContainer.TabIndex = 5;
            // 
            // CreateTerrainTypes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 461);
            Controls.Add(contentTabsContainer);
            Controls.Add(footerDivider);
            Controls.Add(headerDivider);
            Controls.Add(headerMenu);
            Controls.Add(footerStatus);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = headerMenu;
            MinimumSize = new Size(800, 500);
            Name = "CreateTerrainTypes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Terrain Types";
            headerMenu.ResumeLayout(false);
            headerMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)headerDivider).EndInit();
            contentTabs.ResumeLayout(false);
            titlePage.ResumeLayout(false);
            titlePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)titleBanner).EndInit();
            ((System.ComponentModel.ISupportInitialize)titleCreditsDecorTop).EndInit();
            ((System.ComponentModel.ISupportInitialize)titleCreditsDecorBottom).EndInit();
            terrainPage.ResumeLayout(false);
            terrainPageLayout.Panel1.ResumeLayout(false);
            terrainPageLayout.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)terrainPageLayout).EndInit();
            terrainPageLayout.ResumeLayout(false);
            terrainContentContainer.ResumeLayout(false);
            terrainContentContainer.PerformLayout();
            terrainPreviewView.ResumeLayout(false);
            terrainInputLayout.ResumeLayout(false);
            terrainInputLayout.PerformLayout();
            staticTabsContainer.ResumeLayout(false);
            staticTabs.ResumeLayout(false);
            staticEntriesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)staticEntriesView).EndInit();
            ((System.ComponentModel.ISupportInitialize)staticEntriesBindingSource).EndInit();
            staticComponentsPage.ResumeLayout(false);
            staticComponentsLayout.Panel1.ResumeLayout(false);
            staticComponentsLayout.Panel2.ResumeLayout(false);
            staticComponentsLayout.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)staticComponentsLayout).EndInit();
            staticComponentsLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)staticComponentsView).EndInit();
            ((System.ComponentModel.ISupportInitialize)staticComponentsBindingSource).EndInit();
            staticComponentsPreviewContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)staticSelectorPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)footerDivider).EndInit();
            footerStatus.ResumeLayout(false);
            footerStatus.PerformLayout();
            contentTabsContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip headerMenu;
        private ToolStripMenuItem headerTerrainNewButton;
        private ToolStripMenuItem headerTerrainLoadButton;
        private ToolStripMenuItem headerTerrainSaveButton;
        private ToolStripMenuItem headerFacetBuilderButton;
        private ToolStripMenuItem headerCreditsButton;
        private PictureBox headerDivider;

        private TabControl contentTabs;

        private TabPage titlePage;
        private PictureBox titleBanner;
        private PictureBox titleCreditsDecorTop;
        private Label titleCreditsText;
        private PictureBox titleCreditsDecorBottom;

        private TabPage terrainPage;
        private SplitContainer terrainPageLayout;
        private Panel terrainContentContainer;
        private TableLayoutPanel terrainInputLayout;
        private Label terrainTypeHint;
        private TextBox terrainTypeValue;
        private Label terrainBaseHint;
        private ComboBox terrainBaseValue;

        private Panel contentTabsContainer;
        private Panel terrainPreviewView;
        private Panel terrainPreviewMarker;
        private ResponsivePanel terrainPreviewGrid;

        private Panel staticTabsContainer;
        private TabControl staticTabs;

        private TabPage staticEntriesPage;
        private ResponsiveGridView staticEntriesView;
        private DataGridViewTextBoxColumn staticEntriesDescriptionColumn;
        private DataGridViewTextBoxColumn staticEntriesFrequencyColumn;
        private DataGridViewTextBoxColumn staticEntriesCountColumn;
        private BindingSource staticEntriesBindingSource;

        private TabPage staticComponentsPage;
        private SplitContainer staticComponentsLayout;
        private ResponsiveGridView staticComponentsView;
        private DataGridViewTextBoxColumn staticComponentsTileIDColumn;
        private DataGridViewTextBoxColumn staticComponentsXColumn;
        private DataGridViewTextBoxColumn staticComponentsYColumn;
        private DataGridViewTextBoxColumn staticComponentsZColumn;
        private DataGridViewTextBoxColumn staticComponentsHueColumn;
        private BindingSource staticComponentsBindingSource;
        private PictureBox staticSelectorPreview;
        private VScrollBar staticSelectorValue;
        private Button staticSelectorButton;
        private TextBox staticSelectorDescription;
        private PropertyGrid staticPropertiesView;

        private PictureBox footerDivider;
        private StatusStrip footerStatus;
        private ToolStripStatusLabel versionInfo;
        private ToolStripStatusLabel versionNumber;
        private Panel staticComponentsPreviewContainer;
    }
}