using Assets;

using Cartography;

using System.Diagnostics;

namespace MapCreator
{
	public partial class ColorTables : Form
	{
		private int i_Menu;

		private readonly AltitudeTable i_Altitude = new();
		private readonly TerrainTable i_Terrain = new();

		public ColorTables()
		{
			MaximizeBox = false;
			MinimizeBox = false;

			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			colorTables_pictureBox_tileDisplay.Visible = false;
			colorTables_pictureBox_altitudeDisplay.Visible = false;

			colorTables_pictureBox_notificationBox_label_fileUsability.Show();
			colorTables_pictureBox_notificationBox_label_altitudeGradient.Hide();

			colorTables_pictureBox_colorPalette.Show();

			/// Label Transparency: Adobe Photoshop Color Palette
			colorTables_label_adobePhotoshopColorPalette.FlatStyle = FlatStyle.Standard;
			colorTables_label_adobePhotoshopColorPalette.Parent = colorTables_pictureBox_backDrop;
			colorTables_label_adobePhotoshopColorPalette.BackColor = Color.Transparent;

			/// Label Transparency: Load Terrain Color Tables Label
			colorTables_button_loadTerrainColorTables_label.FlatStyle = FlatStyle.Standard;
			colorTables_button_loadTerrainColorTables_label.Parent = colorTables_pictureBox_backDrop;
			colorTables_button_loadTerrainColorTables_label.BackColor = Color.Transparent;

			/// Label Transparency: Load Altitude Color Tables Label
			colorTables_button_loadAltitudeColorTables_label.FlatStyle = FlatStyle.Standard;
			colorTables_button_loadAltitudeColorTables_label.Parent = colorTables_pictureBox_backDrop;
			colorTables_button_loadAltitudeColorTables_label.BackColor = Color.Transparent;
		}

		private void colorTables_menuStrip_button_getAdobePhotoshop_Click(object sender, EventArgs e)
		{
			var getAdobePhotoshop = new ProcessStartInfo
			{
				FileName = "https://www.adobe.com/products/photoshop/",
				UseShellExecute = true
			};

			_ = Process.Start(getAdobePhotoshop);
		}

		private void colorTables_menuStrip_button_openExportLocation_Click(object sender, EventArgs e)
		{
			var path = Path.Combine("Development", "DrawingTools", "AdobePhotoshop");

			_ = Directory.CreateDirectory(path);

			_ = Process.Start("explorer.exe", path);
		}

		#region Export Buttons For Adobe Photoshop Plugins

		private void colorTables_menuStrip_button_export_terrain_adobeColorTableACT_Click(object sender, EventArgs e)
		{
			//i_Terrain.SaveTable();
		}

		private void colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO_Click(object sender, EventArgs e)
		{
			//i_Terrain.SaveSwatch();
		}

		private void colorTables_menuStrip_button_export_altitude_adobeColorTableACT_Click(object sender, EventArgs e)
		{
			//i_Altitude.SaveTable();
		}

		private void colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO_Click(object sender, EventArgs e)
		{
			//i_Altitude.SaveSwatch();
		}

		#endregion

		private void colorTables_menuStrip_button_facetBuilder_Click(object sender, EventArgs e)
		{
			Hide();

			var facetBuilderForm = new FacetBuilder();
			facetBuilderForm.Show();
		}

		private void colorTables_menuStrip_button_information_Click(object sender, EventArgs e)
		{
			var communityCreditsForm = new CommunityCredits();
			communityCreditsForm.Show();
		}

		private void colorTables_button_loadTerrainColorTables_Click(object sender, EventArgs e)
		{
			i_Menu = 0;
			colorTables_label_adobePhotoshopColorPalette.Text = "Terrain Color Table";

			//i_Terrain.LoadXml();

			colorTables_listBox_colorTableList.Fill(i_Terrain);

			colorTables_pictureBox_colorPalette.Hide();
			colorTables_pictureBox_altitudeDisplay.Visible = false;
			colorTables_pictureBox_tileDisplay.Visible = true;

			colorTables_pictureBox_notificationBox_label_fileUsability.Show();
			colorTables_pictureBox_notificationBox_label_altitudeGradient.Hide();
			colorTables_pictureBox_altitudeDisplay.Hide();
		}

		private void colorTables_button_loadAltitudeColorTables_Click(object sender, EventArgs e)
		{
			i_Menu = 1;
			colorTables_label_adobePhotoshopColorPalette.Text = "Altitude Color Table";

			//i_Altitude.LoadXml();

			colorTables_listBox_colorTableList.Fill(i_Altitude);

			colorTables_pictureBox_colorPalette.Hide();
			colorTables_pictureBox_tileDisplay.Visible = false;
			colorTables_pictureBox_altitudeDisplay.Visible = true;

			colorTables_pictureBox_notificationBox_label_fileUsability.Hide();
			colorTables_pictureBox_notificationBox_label_altitudeGradient.Show();
		}

		private void colorTables_listBox_colorTableList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (colorTables_listBox_colorTableList.SelectedItem != null)
			{
				switch (i_Menu)
				{
					case 0:
						{
							colorTables_propertyGrid_colorTableProperties.SelectedObject = colorTables_listBox_colorTableList.SelectedItem;

							if (colorTables_listBox_colorTableList.SelectedItem is Terrain terrain)
							{
								colorTables_pictureBox_tileDisplay.Image = AssetData.Art.GetLand(terrain.TileID);
							}

							break;
						}
					case 1:
						{
							colorTables_propertyGrid_colorTableProperties.SelectedObject = colorTables_listBox_colorTableList.SelectedItem;

							break;
						}
				}
			}
		}
	}
}
