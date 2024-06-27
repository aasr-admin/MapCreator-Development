using Assets;

using System.Diagnostics;

namespace MapCreator
{
	public partial class ColorTables : Form
	{
		private int i_Menu;

		private readonly ClsAltitudeTable i_Altitude;
		private readonly ClsTerrainTable i_Terrain;

		public ColorTables()
		{
			i_Altitude = new ClsAltitudeTable();
			i_Terrain = new ClsTerrainTable();

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

			ClsAltitude.GlobalPropertyChanged += (o, e) =>
			{
				if (o is ClsAltitude a && i_Altitude.Contains(a))
				{
					var selected = colorTables_listBox_colorTableList.SelectedItem == o;

					i_Altitude.Display(colorTables_listBox_colorTableList);

					if (selected)
					{
						colorTables_listBox_colorTableList.SelectedItem = o;
					}
				}
			};

			ClsTerrain.GlobalPropertyChanged += (o, e) =>
			{
				if (o is ClsTerrain t && i_Terrain.Contains(t))
				{
					var selected = colorTables_listBox_colorTableList.SelectedItem == o;

					i_Terrain.Display(colorTables_listBox_colorTableList);

					if (selected)
					{
						colorTables_listBox_colorTableList.SelectedItem = o;
					}
				}
			};
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
			var success = i_Terrain.SaveACT(out var path);

			if (success)
			{
				_ = MessageBox.Show(this, $"SAVED:\r\n{path}", "Terrain Color Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				_ = MessageBox.Show(this, $"UNSAVED:\r\n{path}", "Terrain Color Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO_Click(object sender, EventArgs e)
		{
			var success = i_Terrain.SaveACO(out var path);

			if (success)
			{
				_ = MessageBox.Show(this, $"SAVED:\r\n{path}", "Terrain Color Swatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				_ = MessageBox.Show(this, $"UNSAVED:\r\n{path}", "Terrain Color Swatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void colorTables_menuStrip_button_export_altitude_adobeColorTableACT_Click(object sender, EventArgs e)
		{
			var success = i_Altitude.SaveACT(out var path);

			if (success)
			{
				_ = MessageBox.Show(this, $"SAVED:\r\n{path}", "Altitude Color Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				_ = MessageBox.Show(this, $"UNSAVED:\r\n{path}", "Altitude Color Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO_Click(object sender, EventArgs e)
		{
			var success = i_Altitude.SaveACO(out var path);

			if (success)
			{
				_ = MessageBox.Show(this, $"SAVED:\r\n{path}", "Altitude Color Swatch", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				_ = MessageBox.Show(this, $"UNSAVED:\r\n{path}", "Altitude Color Swatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		private void colorTables_menuStrip_button_facetBuilder_Click(object sender, EventArgs e)
		{
			Hide();

			_ = StaticForm<FacetBuilder>.Open();
		}

		private void colorTables_menuStrip_button_information_Click(object sender, EventArgs e)
		{
			_ = StaticForm<CommunityCredits>.Open();
		}

		private void colorTables_button_loadTerrainColorTables_Click(object sender, EventArgs e)
		{
			i_Menu = 0;
			colorTables_label_adobePhotoshopColorPalette.Text = "Terrain Color Table";

			i_Terrain.Load();
			i_Terrain.Display(colorTables_listBox_colorTableList);

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

			i_Altitude.Load();
			i_Altitude.Display(colorTables_listBox_colorTableList);

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
							var selectedItem = (ClsTerrain)colorTables_listBox_colorTableList.SelectedItem;
							colorTables_propertyGrid_colorTableProperties.SelectedObject = selectedItem;
							colorTables_pictureBox_tileDisplay.Image = AssetData.Art.GetLand(selectedItem.TileID);
							break;
						}
					case 1:
						{
							var clsAltitude = (ClsAltitude)colorTables_listBox_colorTableList.SelectedItem;
							colorTables_propertyGrid_colorTableProperties.SelectedObject = clsAltitude;
							break;
						}
				}
			}
		}
	}
}
