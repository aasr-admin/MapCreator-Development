using System.Windows.Forms;

namespace MapCreator
{
    partial class userPlugin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userPlugin));
            this.userPlugin_menuStrip = new System.Windows.Forms.MenuStrip();
            this.userPlugin_menuStrip_button = new System.Windows.Forms.ToolStripMenuItem();
            this.userPlugin_statusStrip = new System.Windows.Forms.StatusStrip();
            this.statustext = new System.Windows.Forms.ToolStripStatusLabel();
            this.userPlugin_progressBar = new System.Windows.Forms.ProgressBar();
            this.userPlugin_pictureBox04_bottomDivider = new System.Windows.Forms.PictureBox();
            this.userPlugin_pictureBox03_middleDivider = new System.Windows.Forms.PictureBox();
            this.userPlugin_pictureBox02_topDivider = new System.Windows.Forms.PictureBox();
            this.userPlugin_pluginPanel01 = new System.Windows.Forms.Panel();
            this.userPlugin_pluginPanel01_button01_mul2uop = new System.Windows.Forms.Button();
            this.userPlugin_pictureBox01 = new System.Windows.Forms.PictureBox();
            this.userPlugin_panel02_mul2uop_workBench = new System.Windows.Forms.Panel();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit = new System.Windows.Forms.Label();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01 = new System.Windows.Forms.LinkLabel();
            this.userPlugin_panel02_mul2uop_workBench_groupBox = new System.Windows.Forms.GroupBox();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert = new System.Windows.Forms.Label();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert = new System.Windows.Forms.Label();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert = new System.Windows.Forms.RadioButton();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert = new System.Windows.Forms.Label();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert = new System.Windows.Forms.RadioButton();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions = new System.Windows.Forms.Label();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider = new System.Windows.Forms.PictureBox();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation = new System.Windows.Forms.Label();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath = new System.Windows.Forms.TextBox();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile = new System.Windows.Forms.Button();
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath = new System.Windows.Forms.Button();
            this.userPlugin_pictureBox05_bottomDivider = new System.Windows.Forms.PictureBox();
            this.userPlugin_menuStrip.SuspendLayout();
            this.userPlugin_statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox04_bottomDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox03_middleDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox02_topDivider)).BeginInit();
            this.userPlugin_pluginPanel01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox01)).BeginInit();
            this.userPlugin_panel02_mul2uop_workBench.SuspendLayout();
            this.userPlugin_panel02_mul2uop_workBench_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox05_bottomDivider)).BeginInit();
            this.SuspendLayout();
            // 
            // userPlugin_menuStrip
            // 
            this.userPlugin_menuStrip.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.userPlugin_menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.userPlugin_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userPlugin_menuStrip_button});
            this.userPlugin_menuStrip.Location = new System.Drawing.Point(0, 0);
            this.userPlugin_menuStrip.Name = "userPlugin_menuStrip";
            this.userPlugin_menuStrip.Size = new System.Drawing.Size(704, 32);
            this.userPlugin_menuStrip.TabIndex = 1;
            this.userPlugin_menuStrip.Text = "menuStrip1";
            // 
            // userPlugin_menuStrip_button
            // 
            this.userPlugin_menuStrip_button.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_menuStrip_button.Image")));
            this.userPlugin_menuStrip_button.Margin = new System.Windows.Forms.Padding(565, 0, 0, 0);
            this.userPlugin_menuStrip_button.Name = "userPlugin_menuStrip_button";
            this.userPlugin_menuStrip_button.Size = new System.Drawing.Size(125, 28);
            this.userPlugin_menuStrip_button.Text = "Main Menu";
            this.userPlugin_menuStrip_button.Click += new System.EventHandler(this.mainMenuToolStripMenuItem_Click);
            // 
            // userPlugin_statusStrip
            // 
            this.userPlugin_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statustext});
            this.userPlugin_statusStrip.Location = new System.Drawing.Point(0, 449);
            this.userPlugin_statusStrip.Name = "userPlugin_statusStrip";
            this.userPlugin_statusStrip.Size = new System.Drawing.Size(704, 22);
            this.userPlugin_statusStrip.TabIndex = 2;
            this.userPlugin_statusStrip.Text = "statusStrip1";
            // 
            // statustext
            // 
            this.statustext.Name = "statustext";
            this.statustext.Size = new System.Drawing.Size(39, 17);
            this.statustext.Text = "Status";
            // 
            // userPlugin_progressBar
            // 
            this.userPlugin_progressBar.Location = new System.Drawing.Point(0, 424);
            this.userPlugin_progressBar.Name = "userPlugin_progressBar";
            this.userPlugin_progressBar.Size = new System.Drawing.Size(704, 17);
            this.userPlugin_progressBar.TabIndex = 3;
            // 
            // userPlugin_pictureBox04_bottomDivider
            // 
            this.userPlugin_pictureBox04_bottomDivider.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_pictureBox04_bottomDivider.Image")));
            this.userPlugin_pictureBox04_bottomDivider.Location = new System.Drawing.Point(0, 441);
            this.userPlugin_pictureBox04_bottomDivider.Name = "userPlugin_pictureBox04_bottomDivider";
            this.userPlugin_pictureBox04_bottomDivider.Size = new System.Drawing.Size(704, 10);
            this.userPlugin_pictureBox04_bottomDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPlugin_pictureBox04_bottomDivider.TabIndex = 16;
            this.userPlugin_pictureBox04_bottomDivider.TabStop = false;
            // 
            // userPlugin_pictureBox03_middleDivider
            // 
            this.userPlugin_pictureBox03_middleDivider.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_pictureBox03_middleDivider.Image")));
            this.userPlugin_pictureBox03_middleDivider.Location = new System.Drawing.Point(0, 414);
            this.userPlugin_pictureBox03_middleDivider.Name = "userPlugin_pictureBox03_middleDivider";
            this.userPlugin_pictureBox03_middleDivider.Size = new System.Drawing.Size(704, 10);
            this.userPlugin_pictureBox03_middleDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPlugin_pictureBox03_middleDivider.TabIndex = 17;
            this.userPlugin_pictureBox03_middleDivider.TabStop = false;
            // 
            // userPlugin_pictureBox02_topDivider
            // 
            this.userPlugin_pictureBox02_topDivider.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_pictureBox02_topDivider.Image")));
            this.userPlugin_pictureBox02_topDivider.Location = new System.Drawing.Point(0, 32);
            this.userPlugin_pictureBox02_topDivider.Name = "userPlugin_pictureBox02_topDivider";
            this.userPlugin_pictureBox02_topDivider.Size = new System.Drawing.Size(704, 10);
            this.userPlugin_pictureBox02_topDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPlugin_pictureBox02_topDivider.TabIndex = 18;
            this.userPlugin_pictureBox02_topDivider.TabStop = false;
            // 
            // userPlugin_pluginPanel01
            // 
            this.userPlugin_pluginPanel01.AutoScroll = true;
            this.userPlugin_pluginPanel01.BackColor = System.Drawing.Color.Black;
            this.userPlugin_pluginPanel01.Controls.Add(this.userPlugin_pluginPanel01_button01_mul2uop);
            this.userPlugin_pluginPanel01.Location = new System.Drawing.Point(0, 41);
            this.userPlugin_pluginPanel01.Name = "userPlugin_pluginPanel01";
            this.userPlugin_pluginPanel01.Size = new System.Drawing.Size(189, 377);
            this.userPlugin_pluginPanel01.TabIndex = 19;
            // 
            // userPlugin_pluginPanel01_button01_mul2uop
            // 
            this.userPlugin_pluginPanel01_button01_mul2uop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_pluginPanel01_button01_mul2uop.ForeColor = System.Drawing.Color.Navy;
            this.userPlugin_pluginPanel01_button01_mul2uop.Location = new System.Drawing.Point(12, 18);
            this.userPlugin_pluginPanel01_button01_mul2uop.Name = "userPlugin_pluginPanel01_button01_mul2uop";
            this.userPlugin_pluginPanel01_button01_mul2uop.Size = new System.Drawing.Size(163, 28);
            this.userPlugin_pluginPanel01_button01_mul2uop.TabIndex = 20;
            this.userPlugin_pluginPanel01_button01_mul2uop.Text = "Convert .mul To .uop";
            this.userPlugin_pluginPanel01_button01_mul2uop.UseVisualStyleBackColor = true;
            this.userPlugin_pluginPanel01_button01_mul2uop.Click += new System.EventHandler(this.userPlugin_pluginPanel01_button01_mul2uop_Click);
            // 
            // userPlugin_pictureBox01
            // 
            this.userPlugin_pictureBox01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userPlugin_pictureBox01.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_pictureBox01.Image")));
            this.userPlugin_pictureBox01.Location = new System.Drawing.Point(0, 0);
            this.userPlugin_pictureBox01.Name = "userPlugin_pictureBox01";
            this.userPlugin_pictureBox01.Size = new System.Drawing.Size(704, 471);
            this.userPlugin_pictureBox01.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPlugin_pictureBox01.TabIndex = 0;
            this.userPlugin_pictureBox01.TabStop = false;
            // 
            // userPlugin_panel02_mul2uop_workBench
            // 
            this.userPlugin_panel02_mul2uop_workBench.BackColor = System.Drawing.Color.Black;
            this.userPlugin_panel02_mul2uop_workBench.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("userPlugin_panel02_mul2uop_workBench.BackgroundImage")));
            this.userPlugin_panel02_mul2uop_workBench.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit);
            this.userPlugin_panel02_mul2uop_workBench.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01);
            this.userPlugin_panel02_mul2uop_workBench.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox);
            this.userPlugin_panel02_mul2uop_workBench.Location = new System.Drawing.Point(273, 59);
            this.userPlugin_panel02_mul2uop_workBench.Name = "userPlugin_panel02_mul2uop_workBench";
            this.userPlugin_panel02_mul2uop_workBench.Size = new System.Drawing.Size(330, 337);
            this.userPlugin_panel02_mul2uop_workBench.TabIndex = 20;
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.ForeColor = System.Drawing.Color.DimGray;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.Location = new System.Drawing.Point(99, 312);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.Size = new System.Drawing.Size(80, 15);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.TabIndex = 22;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit.Text = "Developer(s):";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.ForeColor = System.Drawing.Color.DimGray;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.LinkColor = System.Drawing.Color.DimGray;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.Location = new System.Drawing.Point(179, 311);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01" +
    "_dev01";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.Size = new System.Drawing.Size(49, 15);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.TabIndex = 21;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.TabStop = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01.Text = "dKnight";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Controls.Add(this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox.ForeColor = System.Drawing.Color.Red;
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Location = new System.Drawing.Point(10, 9);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Name = "userPlugin_panel02_mul2uop_workBench_groupBox";
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Size = new System.Drawing.Size(310, 298);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.TabIndex = 13;
            this.userPlugin_panel02_mul2uop_workBench_groupBox.TabStop = false;
            this.userPlugin_panel02_mul2uop_workBench_groupBox.Text = "Convert .mul To .uop";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.ForeColor = System.Drawing.Color.Lime;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.Location = new System.Drawing.Point(277, 164);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.Size = new System.Drawing.Size(21, 15);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.TabIndex = 33;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert.Text = "10";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.ForeColor = System.Drawing.Color.Lime;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.Location = new System.Drawing.Point(283, 142);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.Size = new System.Drawing.Size(14, 15);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.TabIndex = 31;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert.Text = "1";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Location = new System.Drawing.Point(165, 162);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert" +
    "";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Size = new System.Drawing.Size(113, 19);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.TabIndex = 32;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.TabStop = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Text = "Max To Convert:";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.UseVisualStyleBackColor = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.CheckedChanged += new System.EventHandler(this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert_CheckedChanged);
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.ForeColor = System.Drawing.Color.Silver;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.Location = new System.Drawing.Point(10, 141);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.Size = new System.Drawing.Size(146, 15);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.TabIndex = 30;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert.Text = "Number Of Game Facets:";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Location = new System.Drawing.Point(165, 140);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert" +
    "";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Size = new System.Drawing.Size(113, 19);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.TabIndex = 29;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.TabStop = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Text = "Max To Convert:";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.UseVisualStyleBackColor = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.CheckedChanged += new System.EventHandler(this.userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert_CheckedChanged);
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.ForeColor = System.Drawing.Color.DarkGray;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Location = new System.Drawing.Point(24, 210);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Size = new System.Drawing.Size(263, 75);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.TabIndex = 28;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Text = resources.GetString("userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions.Text");
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.Image")));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.Location = new System.Drawing.Point(11, 187);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.Size = new System.Drawing.Size(288, 21);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.TabIndex = 13;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider.TabStop = false;
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.AutoSize = true;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.ForeColor = System.Drawing.Color.Silver;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.Location = new System.Drawing.Point(10, 28);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.Size = new System.Drawing.Size(136, 15);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.TabIndex = 15;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation.Text = "Project Folder Location:";
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Location = new System.Drawing.Point(11, 49);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Size = new System.Drawing.Size(237, 20);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.TabIndex = 13;
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.BackColor = System.Drawing.Color.Black;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.BackgroundImag" +
        "e")));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.ForeColor = System.Drawing.Color.Black;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.Location = new System.Drawing.Point(260, 92);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.Size = new System.Drawing.Size(41, 39);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.TabIndex = 14;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.UseVisualStyleBackColor = false;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile.Click += new System.EventHandler(this.userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile_Click);
            // 
            // userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath
            // 
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.BackColor = System.Drawing.Color.Black;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.Ba" +
        "ckgroundImage")));
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.ForeColor = System.Drawing.Color.Black;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.Location = new System.Drawing.Point(260, 40);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.Name = "userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath";
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.Size = new System.Drawing.Size(41, 39);
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.TabIndex = 13;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.UseVisualStyleBackColor = false;
            this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath.Click += new System.EventHandler(this.userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath_Click);
            // 
            // userPlugin_pictureBox05_bottomDivider
            // 
            this.userPlugin_pictureBox05_bottomDivider.Image = ((System.Drawing.Image)(resources.GetObject("userPlugin_pictureBox05_bottomDivider.Image")));
            this.userPlugin_pictureBox05_bottomDivider.Location = new System.Drawing.Point(189, 41);
            this.userPlugin_pictureBox05_bottomDivider.Name = "userPlugin_pictureBox05_bottomDivider";
            this.userPlugin_pictureBox05_bottomDivider.Size = new System.Drawing.Size(515, 374);
            this.userPlugin_pictureBox05_bottomDivider.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.userPlugin_pictureBox05_bottomDivider.TabIndex = 21;
            this.userPlugin_pictureBox05_bottomDivider.TabStop = false;
            // 
            // userPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 471);
            this.Controls.Add(this.userPlugin_panel02_mul2uop_workBench);
            this.Controls.Add(this.userPlugin_pictureBox05_bottomDivider);
            this.Controls.Add(this.userPlugin_pictureBox02_topDivider);
            this.Controls.Add(this.userPlugin_pictureBox03_middleDivider);
            this.Controls.Add(this.userPlugin_pictureBox04_bottomDivider);
            this.Controls.Add(this.userPlugin_progressBar);
            this.Controls.Add(this.userPlugin_statusStrip);
            this.Controls.Add(this.userPlugin_menuStrip);
            this.Controls.Add(this.userPlugin_pluginPanel01);
            this.Controls.Add(this.userPlugin_pictureBox01);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.userPlugin_menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "userPlugin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MapCreator: User Submitted Plugins";
            this.Load += new System.EventHandler(this.userPlugin_Load);
            this.userPlugin_menuStrip.ResumeLayout(false);
            this.userPlugin_menuStrip.PerformLayout();
            this.userPlugin_statusStrip.ResumeLayout(false);
            this.userPlugin_statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox04_bottomDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox03_middleDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox02_topDivider)).EndInit();
            this.userPlugin_pluginPanel01.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox01)).EndInit();
            this.userPlugin_panel02_mul2uop_workBench.ResumeLayout(false);
            this.userPlugin_panel02_mul2uop_workBench.PerformLayout();
            this.userPlugin_panel02_mul2uop_workBench_groupBox.ResumeLayout(false);
            this.userPlugin_panel02_mul2uop_workBench_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPlugin_pictureBox05_bottomDivider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip userPlugin_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem userPlugin_menuStrip_button;
        private System.Windows.Forms.StatusStrip userPlugin_statusStrip;
        private System.Windows.Forms.ProgressBar userPlugin_progressBar;
        private System.Windows.Forms.PictureBox userPlugin_pictureBox04_bottomDivider;
        private System.Windows.Forms.PictureBox userPlugin_pictureBox03_middleDivider;
        private System.Windows.Forms.PictureBox userPlugin_pictureBox02_topDivider;
        private Panel userPlugin_pluginPanel01;
        private PictureBox userPlugin_pictureBox01;
        private Button userPlugin_pluginPanel01_button01_mul2uop;
        public Panel userPlugin_panel02_mul2uop_workBench;
        private GroupBox userPlugin_panel02_mul2uop_workBench_groupBox;
        private PictureBox userPlugin_panel02_mul2uop_workBench_groupBox_pictureBox_divider;
        private Label userPlugin_panel02_mul2uop_workBench_groupBox_label01_projectFolderLocation;
        private TextBox userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath;
        private Button userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile;
        private Button userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath;
        private Label userPlugin_panel02_mul2uop_workBench_groupBox_label03_directions;
        private Label userPlugin_panel02_mul2uop_workBench_groupBox_label02_facetsToConvert;
        private RadioButton userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert;
        private Label userPlugin_panel02_mul2uop_workBench_groupBox_label05_facetNumberToConvert;
        private Label userPlugin_panel02_mul2uop_workBench_groupBox_label04_facetNumberToConvert;
        private RadioButton userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert;
        private Label userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit;
        private LinkLabel userPlugin_panel02_mul2uop_workBench_groupBox_label04_developerCredit_linkLabel01_dev01;
        private ToolStripStatusLabel statustext;
        private PictureBox userPlugin_pictureBox05_bottomDivider;
    }
}