namespace MapCreator
{
    partial class userPlugins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userPlugins));
            userPlugins_pictureBox_topDivider = new PictureBox();
            userPlugins_statusStrip = new StatusStrip();
            userPlugins_statusStrip_sizeElevenSpacer = new ToolStripStatusLabel();
            userPlugins_pictureBox_bottomDivider = new PictureBox();
            userPlugins_statusStrip_label_mapCreatorVersioning = new Label();
            userPlugins_statusStrip_label_mapCreatorBuildDate = new Label();
            userPlugins_pictureBox_backDrop = new PictureBox();
            userPlugins_panel_formLauncher_button_createTileTransitions = new Button();
            userPlugins_panel_formLauncher_button_createTerrainTypes = new Button();
            userPlugins_panel_formLauncher = new Panel();
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet = new Button();
            userPlugins_panel_formLauncher_button_fileTypeConverters = new Button();
            userPlugins_pictureBox_vDivider = new PictureBox();
            userPlugins_menuStrip_button_facetBuilder = new ToolStripMenuItem();
            userPlugins_menuStrip_button_information = new ToolStripMenuItem();
            userPlugins_menuStrip = new MenuStrip();
            userPlugins_pictureBox_pluginDescriptionDisplay = new PictureBox();
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions = new PictureBox();
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes = new PictureBox();
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet = new PictureBox();
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_topDivider).BeginInit();
            userPlugins_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_bottomDivider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_backDrop).BeginInit();
            userPlugins_panel_formLauncher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_vDivider).BeginInit();
            userPlugins_menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters).BeginInit();
            SuspendLayout();
            // 
            // userPlugins_pictureBox_topDivider
            // 
            userPlugins_pictureBox_topDivider.BackColor = SystemColors.ActiveCaptionText;
            userPlugins_pictureBox_topDivider.Image = (Image)resources.GetObject("userPlugins_pictureBox_topDivider.Image");
            userPlugins_pictureBox_topDivider.ImeMode = ImeMode.NoControl;
            userPlugins_pictureBox_topDivider.Location = new Point(0, 28);
            userPlugins_pictureBox_topDivider.Name = "userPlugins_pictureBox_topDivider";
            userPlugins_pictureBox_topDivider.Size = new Size(773, 12);
            userPlugins_pictureBox_topDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_topDivider.TabIndex = 5;
            userPlugins_pictureBox_topDivider.TabStop = false;
            // 
            // userPlugins_statusStrip
            // 
            userPlugins_statusStrip.Items.AddRange(new ToolStripItem[] { userPlugins_statusStrip_sizeElevenSpacer });
            userPlugins_statusStrip.Location = new Point(0, 419);
            userPlugins_statusStrip.Name = "userPlugins_statusStrip";
            userPlugins_statusStrip.Size = new Size(773, 25);
            userPlugins_statusStrip.TabIndex = 6;
            userPlugins_statusStrip.Text = "statusStrip1";
            // 
            // userPlugins_statusStrip_sizeElevenSpacer
            // 
            userPlugins_statusStrip_sizeElevenSpacer.BackColor = SystemColors.Control;
            userPlugins_statusStrip_sizeElevenSpacer.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_statusStrip_sizeElevenSpacer.Name = "userPlugins_statusStrip_sizeElevenSpacer";
            userPlugins_statusStrip_sizeElevenSpacer.Size = new Size(13, 20);
            userPlugins_statusStrip_sizeElevenSpacer.Text = " ";
            // 
            // userPlugins_pictureBox_bottomDivider
            // 
            userPlugins_pictureBox_bottomDivider.BackColor = SystemColors.ActiveCaptionText;
            userPlugins_pictureBox_bottomDivider.Image = (Image)resources.GetObject("userPlugins_pictureBox_bottomDivider.Image");
            userPlugins_pictureBox_bottomDivider.ImeMode = ImeMode.NoControl;
            userPlugins_pictureBox_bottomDivider.Location = new Point(0, 410);
            userPlugins_pictureBox_bottomDivider.Name = "userPlugins_pictureBox_bottomDivider";
            userPlugins_pictureBox_bottomDivider.Size = new Size(773, 12);
            userPlugins_pictureBox_bottomDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_bottomDivider.TabIndex = 7;
            userPlugins_pictureBox_bottomDivider.TabStop = false;
            // 
            // userPlugins_statusStrip_label_mapCreatorVersioning
            // 
            userPlugins_statusStrip_label_mapCreatorVersioning.AutoSize = true;
            userPlugins_statusStrip_label_mapCreatorVersioning.BackColor = SystemColors.Control;
            userPlugins_statusStrip_label_mapCreatorVersioning.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_statusStrip_label_mapCreatorVersioning.ForeColor = Color.SlateGray;
            userPlugins_statusStrip_label_mapCreatorVersioning.ImeMode = ImeMode.NoControl;
            userPlugins_statusStrip_label_mapCreatorVersioning.Location = new Point(12, 424);
            userPlugins_statusStrip_label_mapCreatorVersioning.Name = "userPlugins_statusStrip_label_mapCreatorVersioning";
            userPlugins_statusStrip_label_mapCreatorVersioning.Size = new Size(158, 19);
            userPlugins_statusStrip_label_mapCreatorVersioning.TabIndex = 12;
            userPlugins_statusStrip_label_mapCreatorVersioning.Text = "MapCreator: Version 3.5";
            // 
            // userPlugins_statusStrip_label_mapCreatorBuildDate
            // 
            userPlugins_statusStrip_label_mapCreatorBuildDate.AutoSize = true;
            userPlugins_statusStrip_label_mapCreatorBuildDate.BackColor = SystemColors.Control;
            userPlugins_statusStrip_label_mapCreatorBuildDate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_statusStrip_label_mapCreatorBuildDate.ForeColor = Color.SlateGray;
            userPlugins_statusStrip_label_mapCreatorBuildDate.ImeMode = ImeMode.NoControl;
            userPlugins_statusStrip_label_mapCreatorBuildDate.Location = new Point(640, 424);
            userPlugins_statusStrip_label_mapCreatorBuildDate.Name = "userPlugins_statusStrip_label_mapCreatorBuildDate";
            userPlugins_statusStrip_label_mapCreatorBuildDate.Size = new Size(117, 19);
            userPlugins_statusStrip_label_mapCreatorBuildDate.TabIndex = 13;
            userPlugins_statusStrip_label_mapCreatorBuildDate.Text = "Build: 08132023a";
            // 
            // userPlugins_pictureBox_backDrop
            // 
            userPlugins_pictureBox_backDrop.BackColor = Color.Black;
            userPlugins_pictureBox_backDrop.Dock = DockStyle.Fill;
            userPlugins_pictureBox_backDrop.Image = (Image)resources.GetObject("userPlugins_pictureBox_backDrop.Image");
            userPlugins_pictureBox_backDrop.Location = new Point(0, 0);
            userPlugins_pictureBox_backDrop.Name = "userPlugins_pictureBox_backDrop";
            userPlugins_pictureBox_backDrop.Size = new Size(773, 444);
            userPlugins_pictureBox_backDrop.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_backDrop.TabIndex = 14;
            userPlugins_pictureBox_backDrop.TabStop = false;
            // 
            // userPlugins_panel_formLauncher_button_createTileTransitions
            // 
            userPlugins_panel_formLauncher_button_createTileTransitions.BackColor = Color.Transparent;
            userPlugins_panel_formLauncher_button_createTileTransitions.BackgroundImage = (Image)resources.GetObject("userPlugins_panel_formLauncher_button_createTileTransitions.BackgroundImage");
            userPlugins_panel_formLauncher_button_createTileTransitions.BackgroundImageLayout = ImageLayout.Stretch;
            userPlugins_panel_formLauncher_button_createTileTransitions.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_panel_formLauncher_button_createTileTransitions.ForeColor = Color.SlateGray;
            userPlugins_panel_formLauncher_button_createTileTransitions.Location = new Point(16, 22);
            userPlugins_panel_formLauncher_button_createTileTransitions.Name = "userPlugins_panel_formLauncher_button_createTileTransitions";
            userPlugins_panel_formLauncher_button_createTileTransitions.Size = new Size(186, 43);
            userPlugins_panel_formLauncher_button_createTileTransitions.TabIndex = 1;
            userPlugins_panel_formLauncher_button_createTileTransitions.Text = "Create Tile Transitions";
            userPlugins_panel_formLauncher_button_createTileTransitions.UseVisualStyleBackColor = false;
            userPlugins_panel_formLauncher_button_createTileTransitions.Click += userPlugins_panel_formLauncher_button_createTileTransitions_Click;
            userPlugins_panel_formLauncher_button_createTileTransitions.MouseEnter += userPlugins_panel_formLauncher_button_createTileTransitions_MouseEnter;
            userPlugins_panel_formLauncher_button_createTileTransitions.MouseLeave += userPlugins_panel_formLauncher_button_createTileTransitions_MouseLeave;
            // 
            // userPlugins_panel_formLauncher_button_createTerrainTypes
            // 
            userPlugins_panel_formLauncher_button_createTerrainTypes.BackColor = Color.Transparent;
            userPlugins_panel_formLauncher_button_createTerrainTypes.BackgroundImage = (Image)resources.GetObject("userPlugins_panel_formLauncher_button_createTerrainTypes.BackgroundImage");
            userPlugins_panel_formLauncher_button_createTerrainTypes.BackgroundImageLayout = ImageLayout.Stretch;
            userPlugins_panel_formLauncher_button_createTerrainTypes.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_panel_formLauncher_button_createTerrainTypes.ForeColor = Color.SlateGray;
            userPlugins_panel_formLauncher_button_createTerrainTypes.Location = new Point(16, 79);
            userPlugins_panel_formLauncher_button_createTerrainTypes.Name = "userPlugins_panel_formLauncher_button_createTerrainTypes";
            userPlugins_panel_formLauncher_button_createTerrainTypes.Size = new Size(186, 43);
            userPlugins_panel_formLauncher_button_createTerrainTypes.TabIndex = 0;
            userPlugins_panel_formLauncher_button_createTerrainTypes.Text = "Create Terrain Types";
            userPlugins_panel_formLauncher_button_createTerrainTypes.UseVisualStyleBackColor = false;
            userPlugins_panel_formLauncher_button_createTerrainTypes.Click += userPlugins_panel_formLauncher_button_createTerrainTypes_Click;
            userPlugins_panel_formLauncher_button_createTerrainTypes.MouseEnter += userPlugins_panel_formLauncher_button_createTerrainTypes_MouseEnter;
            userPlugins_panel_formLauncher_button_createTerrainTypes.MouseLeave += userPlugins_panel_formLauncher_button_createTerrainTypes_MouseLeave;
            // 
            // userPlugins_panel_formLauncher
            // 
            userPlugins_panel_formLauncher.AutoScroll = true;
            userPlugins_panel_formLauncher.BackColor = Color.Black;
            userPlugins_panel_formLauncher.BackgroundImage = (Image)resources.GetObject("userPlugins_panel_formLauncher.BackgroundImage");
            userPlugins_panel_formLauncher.BackgroundImageLayout = ImageLayout.Stretch;
            userPlugins_panel_formLauncher.Controls.Add(userPlugins_panel_formLauncher_button_viewFacetAsPlanet);
            userPlugins_panel_formLauncher.Controls.Add(userPlugins_panel_formLauncher_button_fileTypeConverters);
            userPlugins_panel_formLauncher.Controls.Add(userPlugins_panel_formLauncher_button_createTileTransitions);
            userPlugins_panel_formLauncher.Controls.Add(userPlugins_panel_formLauncher_button_createTerrainTypes);
            userPlugins_panel_formLauncher.Location = new Point(0, 35);
            userPlugins_panel_formLauncher.Name = "userPlugins_panel_formLauncher";
            userPlugins_panel_formLauncher.Size = new Size(236, 374);
            userPlugins_panel_formLauncher.TabIndex = 16;
            // 
            // userPlugins_panel_formLauncher_button_viewFacetAsPlanet
            // 
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.BackColor = Color.Transparent;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.BackgroundImage = (Image)resources.GetObject("userPlugins_panel_formLauncher_button_viewFacetAsPlanet.BackgroundImage");
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.BackgroundImageLayout = ImageLayout.Stretch;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.ForeColor = Color.SlateGray;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.Location = new Point(16, 137);
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.Name = "userPlugins_panel_formLauncher_button_viewFacetAsPlanet";
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.Size = new Size(186, 43);
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.TabIndex = 4;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.Text = "View Facet As Planet";
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.UseVisualStyleBackColor = false;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.Click += userPlugins_panel_formLauncher_button_viewFacetAsPlanet_Click;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.MouseEnter += userPlugins_panel_formLauncher_button_viewFacetAsPlanet_MouseEnter;
            userPlugins_panel_formLauncher_button_viewFacetAsPlanet.MouseLeave += userPlugins_panel_formLauncher_button_viewFacetAsPlanet_MouseLeave;
            // 
            // userPlugins_panel_formLauncher_button_fileTypeConverters
            // 
            userPlugins_panel_formLauncher_button_fileTypeConverters.BackColor = Color.Transparent;
            userPlugins_panel_formLauncher_button_fileTypeConverters.BackgroundImage = (Image)resources.GetObject("userPlugins_panel_formLauncher_button_fileTypeConverters.BackgroundImage");
            userPlugins_panel_formLauncher_button_fileTypeConverters.BackgroundImageLayout = ImageLayout.Stretch;
            userPlugins_panel_formLauncher_button_fileTypeConverters.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_panel_formLauncher_button_fileTypeConverters.ForeColor = Color.SlateGray;
            userPlugins_panel_formLauncher_button_fileTypeConverters.Location = new Point(16, 194);
            userPlugins_panel_formLauncher_button_fileTypeConverters.Name = "userPlugins_panel_formLauncher_button_fileTypeConverters";
            userPlugins_panel_formLauncher_button_fileTypeConverters.Size = new Size(186, 43);
            userPlugins_panel_formLauncher_button_fileTypeConverters.TabIndex = 2;
            userPlugins_panel_formLauncher_button_fileTypeConverters.Text = "File Type Converters";
            userPlugins_panel_formLauncher_button_fileTypeConverters.UseVisualStyleBackColor = false;
            userPlugins_panel_formLauncher_button_fileTypeConverters.Click += userPlugins_panel_formLauncher_button_fileTypeConverters_Click;
            userPlugins_panel_formLauncher_button_fileTypeConverters.MouseEnter += userPlugins_panel_formLauncher_button_fileTypeConverters_MouseEnter;
            userPlugins_panel_formLauncher_button_fileTypeConverters.MouseLeave += userPlugins_panel_formLauncher_button_fileTypeConverters_MouseLeave;
            // 
            // userPlugins_pictureBox_vDivider
            // 
            userPlugins_pictureBox_vDivider.Image = (Image)resources.GetObject("userPlugins_pictureBox_vDivider.Image");
            userPlugins_pictureBox_vDivider.Location = new Point(764, 0);
            userPlugins_pictureBox_vDivider.Name = "userPlugins_pictureBox_vDivider";
            userPlugins_pictureBox_vDivider.Size = new Size(10, 444);
            userPlugins_pictureBox_vDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_vDivider.TabIndex = 19;
            userPlugins_pictureBox_vDivider.TabStop = false;
            // 
            // userPlugins_menuStrip_button_facetBuilder
            // 
            userPlugins_menuStrip_button_facetBuilder.Image = (Image)resources.GetObject("userPlugins_menuStrip_button_facetBuilder.Image");
            userPlugins_menuStrip_button_facetBuilder.Margin = new Padding(679, 0, 0, 0);
            userPlugins_menuStrip_button_facetBuilder.Name = "userPlugins_menuStrip_button_facetBuilder";
            userPlugins_menuStrip_button_facetBuilder.Size = new Size(32, 24);
            userPlugins_menuStrip_button_facetBuilder.Click += userPlugins_menuStrip_button_facetBuilder_Click;
            // 
            // userPlugins_menuStrip_button_information
            // 
            userPlugins_menuStrip_button_information.Image = (Image)resources.GetObject("userPlugins_menuStrip_button_information.Image");
            userPlugins_menuStrip_button_information.Margin = new Padding(5, 0, 0, 0);
            userPlugins_menuStrip_button_information.Name = "userPlugins_menuStrip_button_information";
            userPlugins_menuStrip_button_information.Size = new Size(32, 24);
            userPlugins_menuStrip_button_information.Click += userPlugins_menuStrip_button_information_Click;
            // 
            // userPlugins_menuStrip
            // 
            userPlugins_menuStrip.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            userPlugins_menuStrip.ImageScalingSize = new Size(20, 20);
            userPlugins_menuStrip.Items.AddRange(new ToolStripItem[] { userPlugins_menuStrip_button_facetBuilder, userPlugins_menuStrip_button_information });
            userPlugins_menuStrip.Location = new Point(0, 0);
            userPlugins_menuStrip.Name = "userPlugins_menuStrip";
            userPlugins_menuStrip.Size = new Size(773, 28);
            userPlugins_menuStrip.TabIndex = 0;
            userPlugins_menuStrip.Text = "menuStrip1";
            // 
            // userPlugins_pictureBox_pluginDescriptionDisplay
            // 
            userPlugins_pictureBox_pluginDescriptionDisplay.BorderStyle = BorderStyle.FixedSingle;
            userPlugins_pictureBox_pluginDescriptionDisplay.Image = (Image)resources.GetObject("userPlugins_pictureBox_pluginDescriptionDisplay.Image");
            userPlugins_pictureBox_pluginDescriptionDisplay.Location = new Point(253, 54);
            userPlugins_pictureBox_pluginDescriptionDisplay.Name = "userPlugins_pictureBox_pluginDescriptionDisplay";
            userPlugins_pictureBox_pluginDescriptionDisplay.Size = new Size(493, 338);
            userPlugins_pictureBox_pluginDescriptionDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_pluginDescriptionDisplay.TabIndex = 20;
            userPlugins_pictureBox_pluginDescriptionDisplay.TabStop = false;
            // 
            // userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions
            // 
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.BorderStyle = BorderStyle.FixedSingle;
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Image = (Image)resources.GetObject("userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Image");
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Location = new Point(253, 54);
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Name = "userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions";
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.Size = new Size(493, 338);
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.TabIndex = 21;
            userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions.TabStop = false;
            // 
            // userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes
            // 
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.BorderStyle = BorderStyle.FixedSingle;
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Image = (Image)resources.GetObject("userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Image");
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Location = new Point(253, 54);
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Name = "userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes";
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.Size = new Size(493, 338);
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.TabIndex = 22;
            userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes.TabStop = false;
            // 
            // userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet
            // 
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.BorderStyle = BorderStyle.FixedSingle;
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Image = (Image)resources.GetObject("userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Image");
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Location = new Point(253, 54);
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Name = "userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet";
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.Size = new Size(493, 338);
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.TabIndex = 23;
            userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet.TabStop = false;
            // 
            // userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters
            // 
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.BorderStyle = BorderStyle.FixedSingle;
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Image = (Image)resources.GetObject("userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Image");
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Location = new Point(253, 54);
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Name = "userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters";
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.Size = new Size(493, 338);
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.SizeMode = PictureBoxSizeMode.StretchImage;
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.TabIndex = 24;
            userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters.TabStop = false;
            // 
            // userPlugins
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(773, 444);
            Controls.Add(userPlugins_pictureBox_bottomDivider);
            Controls.Add(userPlugins_pictureBox_topDivider);
            Controls.Add(userPlugins_panel_formLauncher);
            Controls.Add(userPlugins_statusStrip_label_mapCreatorBuildDate);
            Controls.Add(userPlugins_statusStrip_label_mapCreatorVersioning);
            Controls.Add(userPlugins_statusStrip);
            Controls.Add(userPlugins_menuStrip);
            Controls.Add(userPlugins_pictureBox_vDivider);
            Controls.Add(userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters);
            Controls.Add(userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet);
            Controls.Add(userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes);
            Controls.Add(userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions);
            Controls.Add(userPlugins_pictureBox_pluginDescriptionDisplay);
            Controls.Add(userPlugins_pictureBox_backDrop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = userPlugins_menuStrip;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "userPlugins";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MapCreator: User Plugins";
            TopMost = true;
            Load += userPlugins_Load;
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_topDivider).EndInit();
            userPlugins_statusStrip.ResumeLayout(false);
            userPlugins_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_bottomDivider).EndInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_backDrop).EndInit();
            userPlugins_panel_formLauncher.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_vDivider).EndInit();
            userPlugins_menuStrip.ResumeLayout(false);
            userPlugins_menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions).EndInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet).EndInit();
            ((System.ComponentModel.ISupportInitialize)userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox userPlugins_pictureBox_topDivider;
        private StatusStrip userPlugins_statusStrip;
        private PictureBox userPlugins_pictureBox_bottomDivider;
        private ToolStripStatusLabel userPlugins_statusStrip_sizeElevenSpacer;
        private Label userPlugins_statusStrip_label_mapCreatorVersioning;
        private Label userPlugins_statusStrip_label_mapCreatorBuildDate;
        private PictureBox userPlugins_pictureBox_backDrop;
        private Button userPlugins_panel_formLauncher_button_createTerrainTypes;
        private Button userPlugins_panel_formLauncher_button_createTileTransitions;
        private Panel userPlugins_panel_formLauncher;
        private Button userPlugins_panel_formLauncher_button_fileTypeConverters;
        private PictureBox userPlugins_pictureBox_vDivider;
        private Button userPlugins_panel_formLauncher_button_viewFacetAsPlanet;
        private Button button2;
        private Button button1;
        private Button button3;
        private Button button4;
        private ToolStripMenuItem userPlugins_menuStrip_button_facetBuilder;
        private ToolStripMenuItem userPlugins_menuStrip_button_information;
        private MenuStrip userPlugins_menuStrip;
        private PictureBox userPlugins_pictureBox_pluginDescriptionDisplay;
        private PictureBox userPlugins_pictureBox_pluginDescriptionDisplay_createTileTransitions;
        private PictureBox userPlugins_pictureBox_pluginDescriptionDisplay_createTerrainTypes;
        private PictureBox userPlugins_pictureBox_pluginDescriptionDisplay_viewFacetAsPlanet;
        private PictureBox userPlugins_pictureBox_pluginDescriptionDisplay_fileTypeConverters;
    }
}