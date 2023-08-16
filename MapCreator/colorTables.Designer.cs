namespace MapCreator
{
    partial class colorTables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(colorTables));
            colorTables_menuStrip = new MenuStrip();
            colorTables_menuStrip_button_getAdobePhotoshop = new ToolStripMenuItem();
            colorTables_menuStrip_button_openExportLocation = new ToolStripMenuItem();
            colorTables_menuStrip_button_export = new ToolStripMenuItem();
            colorTables_menuStrip_button_export_terrain = new ToolStripMenuItem();
            colorTables_menuStrip_button_export_terrain_adobeColorTableACT = new ToolStripMenuItem();
            colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO = new ToolStripMenuItem();
            colorTables_menuStrip_button_export_altitude = new ToolStripMenuItem();
            colorTables_menuStrip_button_export_altitude_adobeColorTableACT = new ToolStripMenuItem();
            colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO = new ToolStripMenuItem();
            colorTables_menuStrip_button_facetBuilder = new ToolStripMenuItem();
            colorTables_menuStrip_button_information = new ToolStripMenuItem();
            colorTables_pictureBox_topDivider = new PictureBox();
            colorTables_pictureBox_bottomDivider = new PictureBox();
            colorTables_statusStrip = new StatusStrip();
            colorTables_statusStrip_sizeElevenSpacer = new ToolStripStatusLabel();
            colorTables_pictureBox_backDrop = new PictureBox();
            colorTables_label_adobePhotoshopColorPalette = new Label();
            colorTables_listBox_colorTableList = new ListBox();
            colorTables_propertyGrid_colorTableProperties = new PropertyGrid();
            colorTables_statusStrip_label_mapCreatorBuildDate = new Label();
            colorTables_statusStrip_label_mapCreatorVersioning = new Label();
            colorTables_pictureBox_notificationBox = new PictureBox();
            colorTables_pictureBox_notificationBox_label_fileUsability = new Label();
            colorTables_pictureBox_notificationBox_label_altitudeGradient = new Label();
            colorTables_pictureBox_altitudeDisplay = new PictureBox();
            colorTables_pictureBox_colorPalette = new PictureBox();
            colorTables_pictureBox_tileDisplay = new PictureBox();
            colorTables_button_loadTerrainColorTables = new Button();
            colorTables_button_loadAltitudeColorTables = new Button();
            colorTables_button_loadTerrainColorTables_label = new Label();
            colorTables_button_loadAltitudeColorTables_label = new Label();
            colorTables_menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_topDivider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_bottomDivider).BeginInit();
            colorTables_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_backDrop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_notificationBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_altitudeDisplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_colorPalette).BeginInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_tileDisplay).BeginInit();
            SuspendLayout();
            // 
            // colorTables_menuStrip
            // 
            colorTables_menuStrip.Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_menuStrip.ImageScalingSize = new Size(20, 20);
            colorTables_menuStrip.Items.AddRange(new ToolStripItem[] { colorTables_menuStrip_button_getAdobePhotoshop, colorTables_menuStrip_button_openExportLocation, colorTables_menuStrip_button_export, colorTables_menuStrip_button_facetBuilder, colorTables_menuStrip_button_information });
            colorTables_menuStrip.Location = new Point(0, 0);
            colorTables_menuStrip.Name = "colorTables_menuStrip";
            colorTables_menuStrip.Size = new Size(773, 28);
            colorTables_menuStrip.TabIndex = 0;
            colorTables_menuStrip.Text = "menuStrip1";
            // 
            // colorTables_menuStrip_button_getAdobePhotoshop
            // 
            colorTables_menuStrip_button_getAdobePhotoshop.Image = (Image)resources.GetObject("colorTables_menuStrip_button_getAdobePhotoshop.Image");
            colorTables_menuStrip_button_getAdobePhotoshop.Margin = new Padding(5, 0, 0, 0);
            colorTables_menuStrip_button_getAdobePhotoshop.Name = "colorTables_menuStrip_button_getAdobePhotoshop";
            colorTables_menuStrip_button_getAdobePhotoshop.Size = new Size(32, 24);
            colorTables_menuStrip_button_getAdobePhotoshop.Click += colorTables_menuStrip_button_getAdobePhotoshop_Click;
            // 
            // colorTables_menuStrip_button_openExportLocation
            // 
            colorTables_menuStrip_button_openExportLocation.Image = (Image)resources.GetObject("colorTables_menuStrip_button_openExportLocation.Image");
            colorTables_menuStrip_button_openExportLocation.Margin = new Padding(5, 0, 0, 0);
            colorTables_menuStrip_button_openExportLocation.Name = "colorTables_menuStrip_button_openExportLocation";
            colorTables_menuStrip_button_openExportLocation.Size = new Size(32, 24);
            colorTables_menuStrip_button_openExportLocation.Click += colorTables_menuStrip_button_openExportLocation_Click;
            // 
            // colorTables_menuStrip_button_export
            // 
            colorTables_menuStrip_button_export.DropDownItems.AddRange(new ToolStripItem[] { colorTables_menuStrip_button_export_terrain, colorTables_menuStrip_button_export_altitude });
            colorTables_menuStrip_button_export.Image = (Image)resources.GetObject("colorTables_menuStrip_button_export.Image");
            colorTables_menuStrip_button_export.Margin = new Padding(568, 0, 0, 0);
            colorTables_menuStrip_button_export.Name = "colorTables_menuStrip_button_export";
            colorTables_menuStrip_button_export.Size = new Size(32, 24);
            // 
            // colorTables_menuStrip_button_export_terrain
            // 
            colorTables_menuStrip_button_export_terrain.DropDownItems.AddRange(new ToolStripItem[] { colorTables_menuStrip_button_export_terrain_adobeColorTableACT, colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO });
            colorTables_menuStrip_button_export_terrain.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_menuStrip_button_export_terrain.Name = "colorTables_menuStrip_button_export_terrain";
            colorTables_menuStrip_button_export_terrain.Size = new Size(203, 26);
            colorTables_menuStrip_button_export_terrain.Text = "Save Terrain As...";
            // 
            // colorTables_menuStrip_button_export_terrain_adobeColorTableACT
            // 
            colorTables_menuStrip_button_export_terrain_adobeColorTableACT.Name = "colorTables_menuStrip_button_export_terrain_adobeColorTableACT";
            colorTables_menuStrip_button_export_terrain_adobeColorTableACT.Size = new Size(260, 26);
            colorTables_menuStrip_button_export_terrain_adobeColorTableACT.Text = "Adobe™ Color Table (.act)";
            colorTables_menuStrip_button_export_terrain_adobeColorTableACT.Click += colorTables_menuStrip_button_export_terrain_adobeColorTableACT_Click;
            // 
            // colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO
            // 
            colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO.Name = "colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO";
            colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO.Size = new Size(260, 26);
            colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO.Text = "Adobe™ Swatch File (.aco)";
            colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO.Click += colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO_Click;
            // 
            // colorTables_menuStrip_button_export_altitude
            // 
            colorTables_menuStrip_button_export_altitude.DropDownItems.AddRange(new ToolStripItem[] { colorTables_menuStrip_button_export_altitude_adobeColorTableACT, colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO });
            colorTables_menuStrip_button_export_altitude.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_menuStrip_button_export_altitude.Name = "colorTables_menuStrip_button_export_altitude";
            colorTables_menuStrip_button_export_altitude.Size = new Size(203, 26);
            colorTables_menuStrip_button_export_altitude.Text = "Save Altitude As...";
            // 
            // colorTables_menuStrip_button_export_altitude_adobeColorTableACT
            // 
            colorTables_menuStrip_button_export_altitude_adobeColorTableACT.Name = "colorTables_menuStrip_button_export_altitude_adobeColorTableACT";
            colorTables_menuStrip_button_export_altitude_adobeColorTableACT.Size = new Size(260, 26);
            colorTables_menuStrip_button_export_altitude_adobeColorTableACT.Text = "Adobe™ Color Table (.act)";
            colorTables_menuStrip_button_export_altitude_adobeColorTableACT.Click += colorTables_menuStrip_button_export_altitude_adobeColorTableACT_Click;
            // 
            // colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO
            // 
            colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO.Name = "colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO";
            colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO.Size = new Size(260, 26);
            colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO.Text = "Adobe™ Swatch File (.aco)";
            colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO.Click += colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO_Click;
            // 
            // colorTables_menuStrip_button_facetBuilder
            // 
            colorTables_menuStrip_button_facetBuilder.Image = (Image)resources.GetObject("colorTables_menuStrip_button_facetBuilder.Image");
            colorTables_menuStrip_button_facetBuilder.Margin = new Padding(5, 0, 0, 0);
            colorTables_menuStrip_button_facetBuilder.Name = "colorTables_menuStrip_button_facetBuilder";
            colorTables_menuStrip_button_facetBuilder.Size = new Size(32, 24);
            colorTables_menuStrip_button_facetBuilder.Click += colorTables_menuStrip_button_facetBuilder_Click;
            // 
            // colorTables_menuStrip_button_information
            // 
            colorTables_menuStrip_button_information.Image = (Image)resources.GetObject("colorTables_menuStrip_button_information.Image");
            colorTables_menuStrip_button_information.Margin = new Padding(5, 0, 0, 0);
            colorTables_menuStrip_button_information.Name = "colorTables_menuStrip_button_information";
            colorTables_menuStrip_button_information.Size = new Size(32, 24);
            colorTables_menuStrip_button_information.Click += colorTables_menuStrip_button_information_Click;
            // 
            // colorTables_pictureBox_topDivider
            // 
            colorTables_pictureBox_topDivider.BackColor = SystemColors.ActiveCaptionText;
            colorTables_pictureBox_topDivider.Image = (Image)resources.GetObject("colorTables_pictureBox_topDivider.Image");
            colorTables_pictureBox_topDivider.ImeMode = ImeMode.NoControl;
            colorTables_pictureBox_topDivider.Location = new Point(0, 28);
            colorTables_pictureBox_topDivider.Name = "colorTables_pictureBox_topDivider";
            colorTables_pictureBox_topDivider.Size = new Size(773, 12);
            colorTables_pictureBox_topDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            colorTables_pictureBox_topDivider.TabIndex = 4;
            colorTables_pictureBox_topDivider.TabStop = false;
            // 
            // colorTables_pictureBox_bottomDivider
            // 
            colorTables_pictureBox_bottomDivider.BackColor = SystemColors.ActiveCaptionText;
            colorTables_pictureBox_bottomDivider.Image = (Image)resources.GetObject("colorTables_pictureBox_bottomDivider.Image");
            colorTables_pictureBox_bottomDivider.ImeMode = ImeMode.NoControl;
            colorTables_pictureBox_bottomDivider.Location = new Point(0, 410);
            colorTables_pictureBox_bottomDivider.Name = "colorTables_pictureBox_bottomDivider";
            colorTables_pictureBox_bottomDivider.Size = new Size(773, 12);
            colorTables_pictureBox_bottomDivider.SizeMode = PictureBoxSizeMode.StretchImage;
            colorTables_pictureBox_bottomDivider.TabIndex = 5;
            colorTables_pictureBox_bottomDivider.TabStop = false;
            // 
            // colorTables_statusStrip
            // 
            colorTables_statusStrip.ImageScalingSize = new Size(20, 20);
            colorTables_statusStrip.Items.AddRange(new ToolStripItem[] { colorTables_statusStrip_sizeElevenSpacer });
            colorTables_statusStrip.Location = new Point(0, 419);
            colorTables_statusStrip.Name = "colorTables_statusStrip";
            colorTables_statusStrip.Size = new Size(773, 25);
            colorTables_statusStrip.TabIndex = 6;
            colorTables_statusStrip.Text = "statusStrip1";
            // 
            // colorTables_statusStrip_sizeElevenSpacer
            // 
            colorTables_statusStrip_sizeElevenSpacer.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_statusStrip_sizeElevenSpacer.Name = "colorTables_statusStrip_sizeElevenSpacer";
            colorTables_statusStrip_sizeElevenSpacer.Size = new Size(13, 20);
            colorTables_statusStrip_sizeElevenSpacer.Text = " ";
            // 
            // colorTables_pictureBox_backDrop
            // 
            colorTables_pictureBox_backDrop.Dock = DockStyle.Fill;
            colorTables_pictureBox_backDrop.Image = (Image)resources.GetObject("colorTables_pictureBox_backDrop.Image");
            colorTables_pictureBox_backDrop.Location = new Point(0, 0);
            colorTables_pictureBox_backDrop.Name = "colorTables_pictureBox_backDrop";
            colorTables_pictureBox_backDrop.Size = new Size(773, 444);
            colorTables_pictureBox_backDrop.SizeMode = PictureBoxSizeMode.StretchImage;
            colorTables_pictureBox_backDrop.TabIndex = 7;
            colorTables_pictureBox_backDrop.TabStop = false;
            // 
            // colorTables_label_adobePhotoshopColorPalette
            // 
            colorTables_label_adobePhotoshopColorPalette.AutoSize = true;
            colorTables_label_adobePhotoshopColorPalette.BackColor = Color.Transparent;
            colorTables_label_adobePhotoshopColorPalette.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_label_adobePhotoshopColorPalette.ForeColor = Color.Lavender;
            colorTables_label_adobePhotoshopColorPalette.Location = new Point(13, 48);
            colorTables_label_adobePhotoshopColorPalette.Name = "colorTables_label_adobePhotoshopColorPalette";
            colorTables_label_adobePhotoshopColorPalette.Size = new Size(291, 25);
            colorTables_label_adobePhotoshopColorPalette.TabIndex = 8;
            colorTables_label_adobePhotoshopColorPalette.Text = "Adobe Photoshop™ Color Palette";
            // 
            // colorTables_listBox_colorTableList
            // 
            colorTables_listBox_colorTableList.FormattingEnabled = true;
            colorTables_listBox_colorTableList.ItemHeight = 15;
            colorTables_listBox_colorTableList.Location = new Point(12, 84);
            colorTables_listBox_colorTableList.Name = "colorTables_listBox_colorTableList";
            colorTables_listBox_colorTableList.Size = new Size(335, 229);
            colorTables_listBox_colorTableList.TabIndex = 9;
            colorTables_listBox_colorTableList.SelectedIndexChanged += colorTables_listBox_colorTableList_SelectedIndexChanged;
            // 
            // colorTables_propertyGrid_colorTableProperties
            // 
            colorTables_propertyGrid_colorTableProperties.HelpVisible = false;
            colorTables_propertyGrid_colorTableProperties.Location = new Point(398, 59);
            colorTables_propertyGrid_colorTableProperties.Name = "colorTables_propertyGrid_colorTableProperties";
            colorTables_propertyGrid_colorTableProperties.Size = new Size(362, 254);
            colorTables_propertyGrid_colorTableProperties.TabIndex = 10;
            // 
            // colorTables_statusStrip_label_mapCreatorBuildDate
            // 
            colorTables_statusStrip_label_mapCreatorBuildDate.AutoSize = true;
            colorTables_statusStrip_label_mapCreatorBuildDate.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_statusStrip_label_mapCreatorBuildDate.ForeColor = Color.SlateGray;
            colorTables_statusStrip_label_mapCreatorBuildDate.ImeMode = ImeMode.NoControl;
            colorTables_statusStrip_label_mapCreatorBuildDate.Location = new Point(640, 424);
            colorTables_statusStrip_label_mapCreatorBuildDate.Name = "colorTables_statusStrip_label_mapCreatorBuildDate";
            colorTables_statusStrip_label_mapCreatorBuildDate.Size = new Size(117, 19);
            colorTables_statusStrip_label_mapCreatorBuildDate.TabIndex = 12;
            colorTables_statusStrip_label_mapCreatorBuildDate.Text = "Build: 08122023a";
            // 
            // colorTables_statusStrip_label_mapCreatorVersioning
            // 
            colorTables_statusStrip_label_mapCreatorVersioning.AutoSize = true;
            colorTables_statusStrip_label_mapCreatorVersioning.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_statusStrip_label_mapCreatorVersioning.ForeColor = Color.SlateGray;
            colorTables_statusStrip_label_mapCreatorVersioning.ImeMode = ImeMode.NoControl;
            colorTables_statusStrip_label_mapCreatorVersioning.Location = new Point(12, 424);
            colorTables_statusStrip_label_mapCreatorVersioning.Name = "colorTables_statusStrip_label_mapCreatorVersioning";
            colorTables_statusStrip_label_mapCreatorVersioning.Size = new Size(158, 19);
            colorTables_statusStrip_label_mapCreatorVersioning.TabIndex = 11;
            colorTables_statusStrip_label_mapCreatorVersioning.Text = "MapCreator: Version 3.5";
            // 
            // colorTables_pictureBox_notificationBox
            // 
            colorTables_pictureBox_notificationBox.BackColor = Color.Transparent;
            colorTables_pictureBox_notificationBox.Image = (Image)resources.GetObject("colorTables_pictureBox_notificationBox.Image");
            colorTables_pictureBox_notificationBox.Location = new Point(12, 326);
            colorTables_pictureBox_notificationBox.Name = "colorTables_pictureBox_notificationBox";
            colorTables_pictureBox_notificationBox.Size = new Size(335, 77);
            colorTables_pictureBox_notificationBox.SizeMode = PictureBoxSizeMode.StretchImage;
            colorTables_pictureBox_notificationBox.TabIndex = 13;
            colorTables_pictureBox_notificationBox.TabStop = false;
            // 
            // colorTables_pictureBox_notificationBox_label_fileUsability
            // 
            colorTables_pictureBox_notificationBox_label_fileUsability.AutoSize = true;
            colorTables_pictureBox_notificationBox_label_fileUsability.BackColor = Color.Black;
            colorTables_pictureBox_notificationBox_label_fileUsability.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_pictureBox_notificationBox_label_fileUsability.ForeColor = Color.Ivory;
            colorTables_pictureBox_notificationBox_label_fileUsability.Location = new Point(31, 341);
            colorTables_pictureBox_notificationBox_label_fileUsability.Name = "colorTables_pictureBox_notificationBox_label_fileUsability";
            colorTables_pictureBox_notificationBox_label_fileUsability.Size = new Size(282, 51);
            colorTables_pictureBox_notificationBox_label_fileUsability.TabIndex = 14;
            colorTables_pictureBox_notificationBox_label_fileUsability.Text = "This Program Exports Adobe Photoshop™\r\n(.act) And (.aco) File Types\r\nThese Files May Not Work WIth Other Software";
            colorTables_pictureBox_notificationBox_label_fileUsability.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // colorTables_pictureBox_notificationBox_label_altitudeGradient
            // 
            colorTables_pictureBox_notificationBox_label_altitudeGradient.AutoSize = true;
            colorTables_pictureBox_notificationBox_label_altitudeGradient.BackColor = Color.Black;
            colorTables_pictureBox_notificationBox_label_altitudeGradient.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_pictureBox_notificationBox_label_altitudeGradient.ForeColor = Color.Ivory;
            colorTables_pictureBox_notificationBox_label_altitudeGradient.Location = new Point(44, 341);
            colorTables_pictureBox_notificationBox_label_altitudeGradient.Name = "colorTables_pictureBox_notificationBox_label_altitudeGradient";
            colorTables_pictureBox_notificationBox_label_altitudeGradient.Size = new Size(254, 51);
            colorTables_pictureBox_notificationBox_label_altitudeGradient.TabIndex = 15;
            colorTables_pictureBox_notificationBox_label_altitudeGradient.Text = "Altitude Colors Will Be Used On The \r\n** Altitude Bitmap (.bmp) File **\r\nAltitudes Should Gradient By Five (5) Pixels";
            colorTables_pictureBox_notificationBox_label_altitudeGradient.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // colorTables_pictureBox_altitudeDisplay
            // 
            colorTables_pictureBox_altitudeDisplay.Image = (Image)resources.GetObject("colorTables_pictureBox_altitudeDisplay.Image");
            colorTables_pictureBox_altitudeDisplay.Location = new Point(400, 326);
            colorTables_pictureBox_altitudeDisplay.Name = "colorTables_pictureBox_altitudeDisplay";
            colorTables_pictureBox_altitudeDisplay.Size = new Size(90, 77);
            colorTables_pictureBox_altitudeDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            colorTables_pictureBox_altitudeDisplay.TabIndex = 16;
            colorTables_pictureBox_altitudeDisplay.TabStop = false;
            // 
            // colorTables_pictureBox_colorPalette
            // 
            colorTables_pictureBox_colorPalette.Image = (Image)resources.GetObject("colorTables_pictureBox_colorPalette.Image");
            colorTables_pictureBox_colorPalette.Location = new Point(400, 326);
            colorTables_pictureBox_colorPalette.Name = "colorTables_pictureBox_colorPalette";
            colorTables_pictureBox_colorPalette.Size = new Size(90, 77);
            colorTables_pictureBox_colorPalette.SizeMode = PictureBoxSizeMode.CenterImage;
            colorTables_pictureBox_colorPalette.TabIndex = 17;
            colorTables_pictureBox_colorPalette.TabStop = false;
            // 
            // colorTables_pictureBox_tileDisplay
            // 
            colorTables_pictureBox_tileDisplay.BackColor = Color.LightGray;
            colorTables_pictureBox_tileDisplay.Location = new Point(400, 326);
            colorTables_pictureBox_tileDisplay.Name = "colorTables_pictureBox_tileDisplay";
            colorTables_pictureBox_tileDisplay.Size = new Size(90, 77);
            colorTables_pictureBox_tileDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            colorTables_pictureBox_tileDisplay.TabIndex = 18;
            colorTables_pictureBox_tileDisplay.TabStop = false;
            // 
            // colorTables_button_loadTerrainColorTables
            // 
            colorTables_button_loadTerrainColorTables.BackgroundImage = (Image)resources.GetObject("colorTables_button_loadTerrainColorTables.BackgroundImage");
            colorTables_button_loadTerrainColorTables.BackgroundImageLayout = ImageLayout.Stretch;
            colorTables_button_loadTerrainColorTables.Location = new Point(575, 326);
            colorTables_button_loadTerrainColorTables.Name = "colorTables_button_loadTerrainColorTables";
            colorTables_button_loadTerrainColorTables.Size = new Size(35, 35);
            colorTables_button_loadTerrainColorTables.TabIndex = 19;
            colorTables_button_loadTerrainColorTables.UseVisualStyleBackColor = true;
            colorTables_button_loadTerrainColorTables.Click += colorTables_button_loadTerrainColorTables_Click;
            // 
            // colorTables_button_loadAltitudeColorTables
            // 
            colorTables_button_loadAltitudeColorTables.BackgroundImage = (Image)resources.GetObject("colorTables_button_loadAltitudeColorTables.BackgroundImage");
            colorTables_button_loadAltitudeColorTables.BackgroundImageLayout = ImageLayout.Stretch;
            colorTables_button_loadAltitudeColorTables.Location = new Point(575, 369);
            colorTables_button_loadAltitudeColorTables.Name = "colorTables_button_loadAltitudeColorTables";
            colorTables_button_loadAltitudeColorTables.Size = new Size(35, 35);
            colorTables_button_loadAltitudeColorTables.TabIndex = 20;
            colorTables_button_loadAltitudeColorTables.UseVisualStyleBackColor = true;
            colorTables_button_loadAltitudeColorTables.Click += colorTables_button_loadAltitudeColorTables_Click;
            // 
            // colorTables_button_loadTerrainColorTables_label
            // 
            colorTables_button_loadTerrainColorTables_label.AutoSize = true;
            colorTables_button_loadTerrainColorTables_label.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_button_loadTerrainColorTables_label.ForeColor = Color.Ivory;
            colorTables_button_loadTerrainColorTables_label.Location = new Point(617, 335);
            colorTables_button_loadTerrainColorTables_label.Name = "colorTables_button_loadTerrainColorTables_label";
            colorTables_button_loadTerrainColorTables_label.Size = new Size(134, 17);
            colorTables_button_loadTerrainColorTables_label.TabIndex = 21;
            colorTables_button_loadTerrainColorTables_label.Text = "Load Terrain Colors";
            // 
            // colorTables_button_loadAltitudeColorTables_label
            // 
            colorTables_button_loadAltitudeColorTables_label.AutoSize = true;
            colorTables_button_loadAltitudeColorTables_label.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point);
            colorTables_button_loadAltitudeColorTables_label.ForeColor = Color.Ivory;
            colorTables_button_loadAltitudeColorTables_label.Location = new Point(616, 378);
            colorTables_button_loadAltitudeColorTables_label.Name = "colorTables_button_loadAltitudeColorTables_label";
            colorTables_button_loadAltitudeColorTables_label.Size = new Size(135, 17);
            colorTables_button_loadAltitudeColorTables_label.TabIndex = 22;
            colorTables_button_loadAltitudeColorTables_label.Text = "Load Altitude Colors";
            // 
            // colorTables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(773, 444);
            Controls.Add(colorTables_button_loadAltitudeColorTables_label);
            Controls.Add(colorTables_button_loadTerrainColorTables_label);
            Controls.Add(colorTables_button_loadAltitudeColorTables);
            Controls.Add(colorTables_button_loadTerrainColorTables);
            Controls.Add(colorTables_pictureBox_tileDisplay);
            Controls.Add(colorTables_pictureBox_colorPalette);
            Controls.Add(colorTables_pictureBox_altitudeDisplay);
            Controls.Add(colorTables_pictureBox_notificationBox_label_altitudeGradient);
            Controls.Add(colorTables_statusStrip_label_mapCreatorBuildDate);
            Controls.Add(colorTables_statusStrip_label_mapCreatorVersioning);
            Controls.Add(colorTables_propertyGrid_colorTableProperties);
            Controls.Add(colorTables_listBox_colorTableList);
            Controls.Add(colorTables_label_adobePhotoshopColorPalette);
            Controls.Add(colorTables_pictureBox_bottomDivider);
            Controls.Add(colorTables_pictureBox_topDivider);
            Controls.Add(colorTables_menuStrip);
            Controls.Add(colorTables_statusStrip);
            Controls.Add(colorTables_pictureBox_notificationBox_label_fileUsability);
            Controls.Add(colorTables_pictureBox_notificationBox);
            Controls.Add(colorTables_pictureBox_backDrop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = colorTables_menuStrip;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "colorTables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MapCreator: Create Color Tables";
            TopMost = true;
            Load += colorTables_Load;
            colorTables_menuStrip.ResumeLayout(false);
            colorTables_menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_topDivider).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_bottomDivider).EndInit();
            colorTables_statusStrip.ResumeLayout(false);
            colorTables_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_backDrop).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_notificationBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_altitudeDisplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_colorPalette).EndInit();
            ((System.ComponentModel.ISupportInitialize)colorTables_pictureBox_tileDisplay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip colorTables_menuStrip;
        private ToolStripMenuItem colorTables_menuStrip_button_getAdobePhotoshop;
        private ToolStripMenuItem colorTables_menuStrip_button_export;
        private ToolStripMenuItem colorTables_menuStrip_button_openExportLocation;
        private ToolStripMenuItem colorTables_menuStrip_button_facetBuilder;
        private PictureBox colorTables_pictureBox_topDivider;
        private PictureBox colorTables_pictureBox_bottomDivider;
        private StatusStrip colorTables_statusStrip;
        private ToolStripStatusLabel colorTables_statusStrip_sizeElevenSpacer;
        private ToolStripMenuItem colorTables_menuStrip_button_export_terrain;
        private ToolStripMenuItem colorTables_menuStrip_button_export_terrain_adobeColorTableACT;
        private ToolStripMenuItem colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO;
        private ToolStripMenuItem colorTables_menuStrip_button_export_altitude;
        private ToolStripMenuItem colorTables_menuStrip_button_export_altitude_adobeColorTableACT;
        private ToolStripMenuItem colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO;
        private PictureBox colorTables_pictureBox_backDrop;
        private Label colorTables_label_adobePhotoshopColorPalette;
        private ListBox colorTables_listBox_colorTableList;
        private PropertyGrid colorTables_propertyGrid_colorTableProperties;
        private Label colorTables_statusStrip_label_mapCreatorBuildDate;
        private Label colorTables_statusStrip_label_mapCreatorVersioning;
        private ToolStripMenuItem colorTables_menuStrip_button_information;
        private PictureBox colorTables_pictureBox_notificationBox;
        private Label colorTables_pictureBox_notificationBox_label_fileUsability;
        private Label colorTables_pictureBox_notificationBox_label_altitudeGradient;
        private PictureBox colorTables_pictureBox_altitudeDisplay;
        private PictureBox colorTables_pictureBox_colorPalette;
        private PictureBox colorTables_pictureBox_tileDisplay;
        private Button colorTables_button_loadTerrainColorTables;
        private Button colorTables_button_loadAltitudeColorTables;
        private Label colorTables_button_loadTerrainColorTables_label;
        private Label colorTables_button_loadAltitudeColorTables_label;
    }
}