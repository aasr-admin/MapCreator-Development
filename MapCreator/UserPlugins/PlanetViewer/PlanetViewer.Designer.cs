namespace MapCreator
{
    partial class PlanetViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanetViewer));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSolidFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.flipTextureHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipTextureVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.setWireFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.flipMeshInsideOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flipMeshOutsideInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.setBackgroundImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseASolidColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAColorGradientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.clearImageAndRevertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startStopRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adjustRotationSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.singleFrameRotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomTowardPlanetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutOfPlanetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu_pictureBox02_topDivider = new System.Windows.Forms.PictureBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.tToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu_pictureBox04_bottomDivider = new System.Windows.Forms.PictureBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.XAxisRotation = new System.Windows.Forms.VScrollBar();
            this.XAxisAngleTextBox = new System.Windows.Forms.TextBox();
            this.ZAxisRotation = new System.Windows.Forms.VScrollBar();
            this.XAxisLabel = new System.Windows.Forms.Label();
            this.ZAxisLabel = new System.Windows.Forms.Label();
            this.ZAxisAngleTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu_pictureBox02_topDivider)).BeginInit();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu_pictureBox04_bottomDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.MainMenu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.setBackgroundToolStripMenuItem,
            this.controlsToolStripMenuItem,
            this.mainMenuToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1002, 42);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(225, 36);
            this.openToolStripMenuItem.Text = "Open Facet Image";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // setBackgroundToolStripMenuItem
            // 
            this.setBackgroundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSolidFrameToolStripMenuItem,
            this.toolStripSeparator4,
            this.flipTextureHorizontalToolStripMenuItem,
            this.flipTextureVerticalToolStripMenuItem,
            this.toolStripSeparator3,
            this.setWireFrameToolStripMenuItem,
            this.toolStripSeparator5,
            this.flipMeshInsideOutToolStripMenuItem,
            this.flipMeshOutsideInToolStripMenuItem,
            this.toolStripSeparator6,
            this.setBackgroundImageToolStripMenuItem,
            this.setBackgroundColorToolStripMenuItem,
            this.toolStripSeparator7,
            this.clearImageAndRevertToolStripMenuItem});
            this.setBackgroundToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.setBackgroundToolStripMenuItem.Name = "setBackgroundToolStripMenuItem";
            this.setBackgroundToolStripMenuItem.Size = new System.Drawing.Size(137, 36);
            this.setBackgroundToolStripMenuItem.Text = "Configure";
            // 
            // setSolidFrameToolStripMenuItem
            // 
            this.setSolidFrameToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.setSolidFrameToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.setSolidFrameToolStripMenuItem.Name = "setSolidFrameToolStripMenuItem";
            this.setSolidFrameToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.setSolidFrameToolStripMenuItem.Text = "Set Solid Frame";
            this.setSolidFrameToolStripMenuItem.Click += new System.EventHandler(this.setSolidFrameToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(323, 6);
            // 
            // flipTextureHorizontalToolStripMenuItem
            // 
            this.flipTextureHorizontalToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flipTextureHorizontalToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.flipTextureHorizontalToolStripMenuItem.Name = "flipTextureHorizontalToolStripMenuItem";
            this.flipTextureHorizontalToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.flipTextureHorizontalToolStripMenuItem.Text = "Flip Texture (Horizontal)";
            this.flipTextureHorizontalToolStripMenuItem.Click += new System.EventHandler(this.flipTextureHorizontalToolStripMenuItem_Click);
            // 
            // flipTextureVerticalToolStripMenuItem
            // 
            this.flipTextureVerticalToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flipTextureVerticalToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.flipTextureVerticalToolStripMenuItem.Name = "flipTextureVerticalToolStripMenuItem";
            this.flipTextureVerticalToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.flipTextureVerticalToolStripMenuItem.Text = "Flip Texture (Vertical)";
            this.flipTextureVerticalToolStripMenuItem.Click += new System.EventHandler(this.flipTextureVerticalToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(323, 6);
            // 
            // setWireFrameToolStripMenuItem
            // 
            this.setWireFrameToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.setWireFrameToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.setWireFrameToolStripMenuItem.Name = "setWireFrameToolStripMenuItem";
            this.setWireFrameToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.setWireFrameToolStripMenuItem.Text = "Set Wire Frame";
            this.setWireFrameToolStripMenuItem.Click += new System.EventHandler(this.setWireFrameToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(323, 6);
            // 
            // flipMeshInsideOutToolStripMenuItem
            // 
            this.flipMeshInsideOutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flipMeshInsideOutToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.flipMeshInsideOutToolStripMenuItem.Name = "flipMeshInsideOutToolStripMenuItem";
            this.flipMeshInsideOutToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.flipMeshInsideOutToolStripMenuItem.Text = "Flip Mesh (Inside Out)";
            this.flipMeshInsideOutToolStripMenuItem.Click += new System.EventHandler(this.flipMeshInsideOutToolStripMenuItem_Click);
            // 
            // flipMeshOutsideInToolStripMenuItem
            // 
            this.flipMeshOutsideInToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.flipMeshOutsideInToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.flipMeshOutsideInToolStripMenuItem.Name = "flipMeshOutsideInToolStripMenuItem";
            this.flipMeshOutsideInToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.flipMeshOutsideInToolStripMenuItem.Text = "Flip Mesh (Outside In)";
            this.flipMeshOutsideInToolStripMenuItem.Click += new System.EventHandler(this.flipMeshOutsideInToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(323, 6);
            // 
            // setBackgroundImageToolStripMenuItem
            // 
            this.setBackgroundImageToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.setBackgroundImageToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.setBackgroundImageToolStripMenuItem.Name = "setBackgroundImageToolStripMenuItem";
            this.setBackgroundImageToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.setBackgroundImageToolStripMenuItem.Text = "Set Background Image";
            this.setBackgroundImageToolStripMenuItem.Click += new System.EventHandler(this.setBackgroundImageToolStripMenuItem_Click);
            // 
            // setBackgroundColorToolStripMenuItem
            // 
            this.setBackgroundColorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseASolidColorToolStripMenuItem,
            this.selectAColorGradientToolStripMenuItem});
            this.setBackgroundColorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.setBackgroundColorToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.setBackgroundColorToolStripMenuItem.Name = "setBackgroundColorToolStripMenuItem";
            this.setBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.setBackgroundColorToolStripMenuItem.Text = "Set Background Color";
            // 
            // chooseASolidColorToolStripMenuItem
            // 
            this.chooseASolidColorToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.chooseASolidColorToolStripMenuItem.Name = "chooseASolidColorToolStripMenuItem";
            this.chooseASolidColorToolStripMenuItem.Size = new System.Drawing.Size(321, 36);
            this.chooseASolidColorToolStripMenuItem.Text = "Choose Any Solid Color";
            this.chooseASolidColorToolStripMenuItem.Click += new System.EventHandler(this.chooseASolidColorToolStripMenuItem_Click);
            // 
            // selectAColorGradientToolStripMenuItem
            // 
            this.selectAColorGradientToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.selectAColorGradientToolStripMenuItem.Name = "selectAColorGradientToolStripMenuItem";
            this.selectAColorGradientToolStripMenuItem.Size = new System.Drawing.Size(321, 36);
            this.selectAColorGradientToolStripMenuItem.Text = "Select A Color Gradient";
            this.selectAColorGradientToolStripMenuItem.Click += new System.EventHandler(this.selectAColorGradientToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(323, 6);
            // 
            // clearImageAndRevertToolStripMenuItem
            // 
            this.clearImageAndRevertToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.clearImageAndRevertToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.clearImageAndRevertToolStripMenuItem.Name = "clearImageAndRevertToolStripMenuItem";
            this.clearImageAndRevertToolStripMenuItem.Size = new System.Drawing.Size(326, 36);
            this.clearImageAndRevertToolStripMenuItem.Text = "Revert To Initial Settings";
            this.clearImageAndRevertToolStripMenuItem.Click += new System.EventHandler(this.clearImageAndRevertToolStripMenuItem_Click);
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStopRotationToolStripMenuItem,
            this.adjustRotationSpeedToolStripMenuItem,
            this.toolStripSeparator1,
            this.singleFrameRotationToolStripMenuItem,
            this.toolStripSeparator2,
            this.zoomTowardPlanetToolStripMenuItem,
            this.zoomOutOfPlanetToolStripMenuItem});
            this.controlsToolStripMenuItem.ForeColor = System.Drawing.Color.Navy;
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(120, 36);
            this.controlsToolStripMenuItem.Text = "Controls";
            // 
            // startStopRotationToolStripMenuItem
            // 
            this.startStopRotationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.startStopRotationToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.startStopRotationToolStripMenuItem.Name = "startStopRotationToolStripMenuItem";
            this.startStopRotationToolStripMenuItem.Size = new System.Drawing.Size(348, 36);
            this.startStopRotationToolStripMenuItem.Text = "Start And Stop Planet Spin";
            this.startStopRotationToolStripMenuItem.Click += new System.EventHandler(this.startStopRotationToolStripMenuItem_Click);
            // 
            // adjustRotationSpeedToolStripMenuItem
            // 
            this.adjustRotationSpeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speedToolStripMenuItem,
            this.speedToolStripMenuItem1});
            this.adjustRotationSpeedToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.adjustRotationSpeedToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.adjustRotationSpeedToolStripMenuItem.Name = "adjustRotationSpeedToolStripMenuItem";
            this.adjustRotationSpeedToolStripMenuItem.Size = new System.Drawing.Size(348, 36);
            this.adjustRotationSpeedToolStripMenuItem.Text = "Rotation Speed";
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(270, 36);
            this.speedToolStripMenuItem.Text = "Speed  +";
            this.speedToolStripMenuItem.Click += new System.EventHandler(this.speedToolStripMenuItem_Click);
            // 
            // speedToolStripMenuItem1
            // 
            this.speedToolStripMenuItem1.ForeColor = System.Drawing.Color.Green;
            this.speedToolStripMenuItem1.Name = "speedToolStripMenuItem1";
            this.speedToolStripMenuItem1.Size = new System.Drawing.Size(270, 36);
            this.speedToolStripMenuItem1.Text = "Speed  ‒";
            this.speedToolStripMenuItem1.Click += new System.EventHandler(this.speedToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(345, 6);
            // 
            // singleFrameRotationToolStripMenuItem
            // 
            this.singleFrameRotationToolStripMenuItem.Enabled = false;
            this.singleFrameRotationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.singleFrameRotationToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.singleFrameRotationToolStripMenuItem.Name = "singleFrameRotationToolStripMenuItem";
            this.singleFrameRotationToolStripMenuItem.Size = new System.Drawing.Size(348, 36);
            this.singleFrameRotationToolStripMenuItem.Text = "Frame-by-Frame Rotation";
            this.singleFrameRotationToolStripMenuItem.Click += new System.EventHandler(this.singleFrameRotationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(345, 6);
            // 
            // zoomTowardPlanetToolStripMenuItem
            // 
            this.zoomTowardPlanetToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.zoomTowardPlanetToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.zoomTowardPlanetToolStripMenuItem.Name = "zoomTowardPlanetToolStripMenuItem";
            this.zoomTowardPlanetToolStripMenuItem.Size = new System.Drawing.Size(348, 36);
            this.zoomTowardPlanetToolStripMenuItem.Text = "Zoom Toward Planet   [ ↑ ]";
            this.zoomTowardPlanetToolStripMenuItem.Click += new System.EventHandler(this.zoomTowardPlanetToolStripMenuItem_Click);
            // 
            // zoomOutOfPlanetToolStripMenuItem
            // 
            this.zoomOutOfPlanetToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.zoomOutOfPlanetToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.zoomOutOfPlanetToolStripMenuItem.Name = "zoomOutOfPlanetToolStripMenuItem";
            this.zoomOutOfPlanetToolStripMenuItem.Size = new System.Drawing.Size(348, 36);
            this.zoomOutOfPlanetToolStripMenuItem.Text = "Zoom Out Of Planet   [ ↓ ]";
            this.zoomOutOfPlanetToolStripMenuItem.Click += new System.EventHandler(this.zoomOutOfPlanetToolStripMenuItem_Click);
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mainMenuToolStripMenuItem.Image")));
            this.mainMenuToolStripMenuItem.Margin = new System.Windows.Forms.Padding(288, 0, 0, 0);
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(40, 38);
            this.mainMenuToolStripMenuItem.Click += new System.EventHandler(this.mainMenuToolStripMenuItem_Click);
            // 
            // mainMenu_pictureBox02_topDivider
            // 
            this.mainMenu_pictureBox02_topDivider.Image = ((System.Drawing.Image)(resources.GetObject("mainMenu_pictureBox02_topDivider.Image")));
            this.mainMenu_pictureBox02_topDivider.Location = new System.Drawing.Point(0, 45);
            this.mainMenu_pictureBox02_topDivider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainMenu_pictureBox02_topDivider.Name = "mainMenu_pictureBox02_topDivider";
            this.mainMenu_pictureBox02_topDivider.Size = new System.Drawing.Size(1002, 15);
            this.mainMenu_pictureBox02_topDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainMenu_pictureBox02_topDivider.TabIndex = 3;
            this.mainMenu_pictureBox02_topDivider.TabStop = false;
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(0, 58);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(862, 413);
            this.MainPanel.TabIndex = 4;
            this.MainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseDown);
            this.MainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseMove);
            this.MainPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseUp);
            this.MainPanel.Resize += new System.EventHandler(this.PlanetViewer_Resize);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tToolStripMenuItem,
            this.tToolStripMenuItem1,
            this.tToolStripMenuItem2,
            this.tToolStripMenuItem3});
            this.menuStrip2.Location = new System.Drawing.Point(0, 498);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1002, 36);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // tToolStripMenuItem
            // 
            this.tToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tToolStripMenuItem.Image")));
            this.tToolStripMenuItem.Margin = new System.Windows.Forms.Padding(517, 0, 0, 0);
            this.tToolStripMenuItem.Name = "tToolStripMenuItem";
            this.tToolStripMenuItem.Size = new System.Drawing.Size(40, 28);
            this.tToolStripMenuItem.ToolTipText = "Reset Frame(s)";
            this.tToolStripMenuItem.Click += new System.EventHandler(this.tToolStripMenuItem_Click);
            // 
            // tToolStripMenuItem1
            // 
            this.tToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("tToolStripMenuItem1.Image")));
            this.tToolStripMenuItem1.Name = "tToolStripMenuItem1";
            this.tToolStripMenuItem1.Size = new System.Drawing.Size(40, 28);
            this.tToolStripMenuItem1.ToolTipText = "Add Frame(s)";
            this.tToolStripMenuItem1.Click += new System.EventHandler(this.tToolStripMenuItem1_Click);
            // 
            // tToolStripMenuItem2
            // 
            this.tToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("tToolStripMenuItem2.Image")));
            this.tToolStripMenuItem2.Name = "tToolStripMenuItem2";
            this.tToolStripMenuItem2.Size = new System.Drawing.Size(40, 28);
            this.tToolStripMenuItem2.ToolTipText = "Delete Frame(s)";
            this.tToolStripMenuItem2.Click += new System.EventHandler(this.tToolStripMenuItem2_Click);
            // 
            // tToolStripMenuItem3
            // 
            this.tToolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("tToolStripMenuItem3.Image")));
            this.tToolStripMenuItem3.Name = "tToolStripMenuItem3";
            this.tToolStripMenuItem3.Size = new System.Drawing.Size(40, 28);
            this.tToolStripMenuItem3.ToolTipText = "Save Media";
            this.tToolStripMenuItem3.Click += new System.EventHandler(this.tToolStripMenuItem3_Click);
            // 
            // mainMenu_pictureBox04_bottomDivider
            // 
            this.mainMenu_pictureBox04_bottomDivider.Image = ((System.Drawing.Image)(resources.GetObject("mainMenu_pictureBox04_bottomDivider.Image")));
            this.mainMenu_pictureBox04_bottomDivider.Location = new System.Drawing.Point(0, 470);
            this.mainMenu_pictureBox04_bottomDivider.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainMenu_pictureBox04_bottomDivider.Name = "mainMenu_pictureBox04_bottomDivider";
            this.mainMenu_pictureBox04_bottomDivider.Size = new System.Drawing.Size(1002, 15);
            this.mainMenu_pictureBox04_bottomDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mainMenu_pictureBox04_bottomDivider.TabIndex = 16;
            this.mainMenu_pictureBox04_bottomDivider.TabStop = false;
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 16;
            // 
            // XAxisRotation
            // 
            this.XAxisRotation.Location = new System.Drawing.Point(868, 95);
            this.XAxisRotation.Name = "XAxisRotation";
            this.XAxisRotation.Size = new System.Drawing.Size(35, 316);
            this.XAxisRotation.TabIndex = 17;
            this.XAxisRotation.ValueChanged += new System.EventHandler(this.XAxisRotation_ValueChanged);
            // 
            // XAxisAngleTextBox
            // 
            this.XAxisAngleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.XAxisAngleTextBox.Location = new System.Drawing.Point(868, 439);
            this.XAxisAngleTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.XAxisAngleTextBox.Name = "XAxisAngleTextBox";
            this.XAxisAngleTextBox.Size = new System.Drawing.Size(50, 26);
            this.XAxisAngleTextBox.TabIndex = 18;
            this.XAxisAngleTextBox.TabStop = false;
            this.XAxisAngleTextBox.WordWrap = false;
            this.XAxisAngleTextBox.TextChanged += new System.EventHandler(this.XAxisAngleTextBox_TextChanged);
            // 
            // ZAxisRotation
            // 
            this.ZAxisRotation.Location = new System.Drawing.Point(945, 95);
            this.ZAxisRotation.Name = "ZAxisRotation";
            this.ZAxisRotation.Size = new System.Drawing.Size(35, 316);
            this.ZAxisRotation.TabIndex = 19;
            this.ZAxisRotation.ValueChanged += new System.EventHandler(this.ZAxisRotation_ValueChanged);
            // 
            // XAxisLabel
            // 
            this.XAxisLabel.AutoSize = true;
            this.XAxisLabel.BackColor = System.Drawing.Color.LimeGreen;
            this.XAxisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XAxisLabel.ForeColor = System.Drawing.Color.Maroon;
            this.XAxisLabel.Location = new System.Drawing.Point(864, 58);
            this.XAxisLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.XAxisLabel.Name = "XAxisLabel";
            this.XAxisLabel.Size = new System.Drawing.Size(62, 22);
            this.XAxisLabel.TabIndex = 20;
            this.XAxisLabel.Text = "X-Axis";
            this.XAxisLabel.DoubleClick += new System.EventHandler(this.XAxisLabel_DoubleClick);
            // 
            // ZAxisLabel
            // 
            this.ZAxisLabel.AutoSize = true;
            this.ZAxisLabel.BackColor = System.Drawing.Color.LimeGreen;
            this.ZAxisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZAxisLabel.ForeColor = System.Drawing.Color.Maroon;
            this.ZAxisLabel.Location = new System.Drawing.Point(942, 58);
            this.ZAxisLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ZAxisLabel.Name = "ZAxisLabel";
            this.ZAxisLabel.Size = new System.Drawing.Size(61, 22);
            this.ZAxisLabel.TabIndex = 21;
            this.ZAxisLabel.Text = "Z-Axis";
            this.ZAxisLabel.DoubleClick += new System.EventHandler(this.ZAxisLabel_DoubleClick);
            // 
            // ZAxisAngleTextBox
            // 
            this.ZAxisAngleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAxisAngleTextBox.Location = new System.Drawing.Point(946, 439);
            this.ZAxisAngleTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ZAxisAngleTextBox.Name = "ZAxisAngleTextBox";
            this.ZAxisAngleTextBox.Size = new System.Drawing.Size(50, 26);
            this.ZAxisAngleTextBox.TabIndex = 22;
            this.ZAxisAngleTextBox.TabStop = false;
            this.ZAxisAngleTextBox.TextChanged += new System.EventHandler(this.ZAxisAngleTextBox_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(926, 58);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 408);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // PlanetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 534);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ZAxisAngleTextBox);
            this.Controls.Add(this.ZAxisLabel);
            this.Controls.Add(this.XAxisLabel);
            this.Controls.Add(this.ZAxisRotation);
            this.Controls.Add(this.XAxisAngleTextBox);
            this.Controls.Add(this.XAxisRotation);
            this.Controls.Add(this.mainMenu_pictureBox04_bottomDivider);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.mainMenu_pictureBox02_topDivider);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlanetViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapCreator: PlanetViewer";
            this.Shown += new System.EventHandler(this.PlanetViewer_Shown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PlanetViewer_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.PlanetViewer_Resize);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu_pictureBox02_topDivider)).EndInit();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu_pictureBox04_bottomDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBackgroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainMenuToolStripMenuItem;
        private System.Windows.Forms.PictureBox mainMenu_pictureBox02_topDivider;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startStopRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adjustRotationSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleFrameRotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem zoomTowardPlanetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutOfPlanetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSolidFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem flipTextureHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipTextureVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem setWireFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem flipMeshInsideOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flipMeshOutsideInToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem setBackgroundImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setBackgroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseASolidColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAColorGradientToolStripMenuItem;
        private System.Windows.Forms.PictureBox mainMenu_pictureBox04_bottomDivider;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem clearImageAndRevertToolStripMenuItem;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.VScrollBar XAxisRotation;
        private System.Windows.Forms.TextBox XAxisAngleTextBox;
        private System.Windows.Forms.VScrollBar ZAxisRotation;
        private System.Windows.Forms.Label XAxisLabel;
        private System.Windows.Forms.Label ZAxisLabel;
        private System.Windows.Forms.TextBox ZAxisAngleTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}