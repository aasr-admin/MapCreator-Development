using Compiler;

using MapCreator.Interface;

using System.Diagnostics;

namespace MapCreator
{
	public partial class FacetBuilder : Form
	{
		private readonly BuildLogger _Logs = new();

		private readonly UserPlugins _UserPlugins = new();
		private readonly CommunityCredits _CommunityCredits = new();

		private readonly ColorTables _ColorTables = new();

		public Project CurrentProject { get; }

		public FacetBuilder(Project project)
		{
			CurrentProject = project;

			if (!CurrentProject.Loaded)
			{
				CurrentProject.Load();
			}

			InitializeComponent();

			MaximizeBox = false;
			MinimizeBox = false;
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);

			MapCreatorUI.Instance.Show();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (Visible)
			{
				facetBuilder_panel_workbench.Hide();
			}

			_Logs.Show();

			Location = new Point(_Logs.Location.X + 100, _Logs.Location.Y + 100);

			#region Workbench Loading

			facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();
			facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();
			facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();

			//facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetSize_comboBox.Fill(CurrentProject.Facet);
			facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox.Fill(CurrentProject.Terrains);

			#endregion
		}

		private void facetBuilder_menuStrip_button_gettingStarted_createColorTables_Click(object sender, EventArgs e)
		{
			//Hide();

			//_Logs.Hide();

			_ = _ColorTables.ShowDialog(this);
		}

		private void facetBuilder_menuStrip_button_gettingStarted_mapCreatorManual_Click(object sender, EventArgs e)
		{

		}

		private void facetBuilder_menuStrip_button_drawingTools_adobePhotoshop_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		private void facetBuilder_menuStrip_button_drawingTools_freePaintSoftware_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "DrawingTools", "FreePaintSoftware");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		private void facetBuilder_menuStrip_button_facetTutorials_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "FacetTutorials");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		private void facetBuilder_menuStrip_button_userPlugins_Click(object sender, EventArgs e)
		{
			//Hide();

			//_Logs.Hide();

			_ = _UserPlugins.ShowDialog(this);
		}

		private void facetBuilder_menuStrip_button_uploadPlugin_Click(object sender, EventArgs e)
		{

		}

		private void facetBuilder_menuStrip_button_information_Click(object sender, EventArgs e)
		{
			//Hide();

			//_Logs.Hide();

			_ = _CommunityCredits.ShowDialog(this);
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_MouseEnter(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles.ForeColor = Color.LimeGreen;
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_Click(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench.Show();
			facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles.Show();

			facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap.Hide();
			facetBuilder_panel_workbench_groupBox_compileYourNewFacet.Hide();
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_MouseLeave(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles.ForeColor = Color.SlateGray;
		}

		#region GroupBox Functionality

		private void facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_button_locateProject_Click(object sender, EventArgs e)
		{
			var path = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text;

			if (String.IsNullOrWhiteSpace(path))
			{
				path = Directory.GetCurrentDirectory();
			}

			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = path
			};

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_button_createFacetBitmapFiles_Click(object sender, EventArgs e)
		{

		}

		#endregion

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_MouseEnter(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap.ForeColor = Color.LimeGreen;
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_Click(object sender, EventArgs e)
		{
			/// Show These Controls
			Control fBPW = facetBuilder_panel_workbench;
			Thread.Sleep(25);
			fBPW.Show();

			Control sYAB = facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap;
			Thread.Sleep(25);
			sYAB.Show();

			/// Hide These Controls
			Control cFBF = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles;
			Thread.Sleep(25);
			cFBF.Hide();

			Control cYNF = facetBuilder_panel_workbench_groupBox_compileYourNewFacet;
			Thread.Sleep(25);
			cYNF.Hide();
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_MouseLeave(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap.ForeColor = Color.SlateGray;
		}

		#region GroupBox Functionality

		private void facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_button_locateProject_Click(object sender, EventArgs e)
		{
			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text
			};
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_button_syncAltitudeBitmapFile_Click(object sender, EventArgs e)
		{
			/*
			var progress = new Progress<int>(i => { facetBuilder_panel_workbench_progressBar.Value = Math.Abs(i); }); // TODO: temporary fix, i didn't get why it put -73
			var logger = new Progress<string>(_Logs.LogMessage);
			var resetProgress = new Task(() =>
			{
				Thread.Sleep(1000);
				((IProgress<int>)progress).Report(0);
			});
			await Task.Run(() => EncodeAltitudeBitmapHelper.MakeAltitudeImage(facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_terrainBitmap_textBox.Text, facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_altitudeBitmap_textBox.Text, _AltitudeTable, _TerrainTable, progress, logger)).ContinueWith(c => resetProgress.Start());
			*/
		}

		#endregion

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_MouseEnter(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet.ForeColor = Color.LimeGreen;
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_Click(object sender, EventArgs e)
		{
			/// Show These Controls
			Control fBPW = facetBuilder_panel_workbench;
			Thread.Sleep(25);
			fBPW.Show();

			Control cYNF = facetBuilder_panel_workbench_groupBox_compileYourNewFacet;
			Thread.Sleep(25);
			cYNF.Show();

			/// Hide These Controls
			Control cFBF = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles;
			Thread.Sleep(25);
			cFBF.Hide();

			Control sYAB = facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap;
			Thread.Sleep(25);
			sYAB.Hide();
		}

		private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_MouseLeave(object sender, EventArgs e)
		{
			facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet.ForeColor = Color.SlateGray;
		}

		#region GroupBox Functionality

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_button_locateProject_Click(object sender, EventArgs e)
		{
			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text
			};

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_toggleFacetStatics_radioButton_on_CheckedChanged(object sender, EventArgs e)
		{
			CurrentProject.Settings.RandomStatics = true;
			/// Form NotificationAlertOn = new NotificationAlertOn();
			/// NotificationAlertOn.Show();
			System.Media.SystemSounds.Beep.Play();
		}

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_toggleFacetStatics_radioButton_off_CheckedChanged(object sender, EventArgs e)
		{
			CurrentProject.Settings.RandomStatics = false;
			/// Form NotificationAlertOff = new NotificationAlertOff();
			/// NotificationAlertOff.Show();
			System.Media.SystemSounds.Beep.Play();
		}

		private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_button_createFacetFiles_Click(object sender, EventArgs e)
		{
			/*
			if (Interaction.MsgBox("You are about to create the Mul Files\r\nAre you sure ?", MsgBoxStyle.YesNo, "Make UO Map") == MsgBoxResult.Yes)
			{
				var mc_MapMake = this;
				///CompileYourNewMap uOMapMake = this;
				///
				new Thread(new ThreadStart(CreateFacet_mul_Files)).Start();
			}
			*/
		}

		private void CreateFacet_mul_Files()
		{
			CurrentProject.Compile();
		}

		#endregion

		private void facetBuilder_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}