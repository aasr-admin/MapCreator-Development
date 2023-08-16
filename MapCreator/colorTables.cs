using Cartography.compiler;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using UltimaSDK;

namespace MapCreator
{
    public partial class colorTables : Form
    {
        private int i_Menu;

        private ClsAltitudeTable i_Altitude;
        private ClsTerrainTable i_Terrain;

        public colorTables()
        {
            MaximizeBox = false;
            MinimizeBox = false;

            colorTables cTWB = this;

            base.Load += new EventHandler(cTWB.colorTables_Load);
            this.i_Menu = 0;
            this.i_Altitude = new ClsAltitudeTable();
            this.i_Terrain = new ClsTerrainTable();

            InitializeComponent();
        }

        private void colorTables_Load(object? sender, EventArgs e)
        {
            this.i_Menu = 0;

            this.colorTables_pictureBox_tileDisplay.Visible = false;
            this.colorTables_pictureBox_altitudeDisplay.Visible = false;

            this.colorTables_pictureBox_notificationBox_label_fileUsability.Show();
            this.colorTables_pictureBox_notificationBox_label_altitudeGradient.Hide();

            this.colorTables_pictureBox_colorPalette.Show();

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
            ProcessStartInfo getAdobePhotoshop = new ProcessStartInfo
            {
                FileName = "https://www.adobe.com/products/photoshop/",
                UseShellExecute = true
            };

            Process.Start(getAdobePhotoshop);
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
            this.i_Terrain.SaveACT();
        }

        private void colorTables_menuStrip_button_export_terrain_adobeSwatchFileACO_Click(object sender, EventArgs e)
        {
            this.i_Terrain.SaveACO();
        }

        private void colorTables_menuStrip_button_export_altitude_adobeColorTableACT_Click(object sender, EventArgs e)
        {
            this.i_Altitude.SaveACT();
        }

        private void colorTables_menuStrip_button_export_altitude_adobeSwatchFileACO_Click(object sender, EventArgs e)
        {
            this.i_Altitude.SaveACO();
        }

        #endregion

        private void colorTables_menuStrip_button_facetBuilder_Click(object sender, EventArgs e)
        {
            this.Hide();

            facetBuilder facetBuilderForm = new facetBuilder();
            facetBuilderForm.Show();
        }

        private void colorTables_menuStrip_button_information_Click(object sender, EventArgs e)
        {
            communityCredits communityCreditsForm = new communityCredits();
            communityCreditsForm.Show();
        }

        private void colorTables_button_loadTerrainColorTables_Click(object sender, EventArgs e)
        {
            this.i_Menu = 0;
            this.colorTables_label_adobePhotoshopColorPalette.Text = "Terrain Color Table";

            this.i_Terrain.Load();
            this.i_Terrain.Display(this.colorTables_listBox_colorTableList);

            this.colorTables_pictureBox_colorPalette.Hide();
            this.colorTables_pictureBox_altitudeDisplay.Visible = false;
            this.colorTables_pictureBox_tileDisplay.Visible = true;

            this.colorTables_pictureBox_notificationBox_label_fileUsability.Show();
            this.colorTables_pictureBox_notificationBox_label_altitudeGradient.Hide();
            this.colorTables_pictureBox_altitudeDisplay.Hide();
        }

        private void colorTables_button_loadAltitudeColorTables_Click(object sender, EventArgs e)
        {
            this.i_Menu = 1;
            this.colorTables_label_adobePhotoshopColorPalette.Text = "Altitude Color Table";

            this.i_Altitude.Load();
            this.i_Altitude.Display(this.colorTables_listBox_colorTableList);

            this.colorTables_pictureBox_colorPalette.Hide();
            this.colorTables_pictureBox_tileDisplay.Visible = false;
            this.colorTables_pictureBox_altitudeDisplay.Visible = true;

            this.colorTables_pictureBox_notificationBox_label_fileUsability.Hide();
            this.colorTables_pictureBox_notificationBox_label_altitudeGradient.Show();
        }

        private void colorTables_listBox_colorTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.colorTables_listBox_colorTableList.SelectedItem != null)
            {
                switch (this.i_Menu)
                {
                    case 0:
                        {
                            ClsTerrain selectedItem = (ClsTerrain)this.colorTables_listBox_colorTableList.SelectedItem;
                            this.colorTables_propertyGrid_colorTableProperties.SelectedObject = selectedItem;
                            this.colorTables_pictureBox_tileDisplay.Image = Art.GetLand(selectedItem.TileID);
                            break;
                        }
                    case 1:
                        {
                            ClsAltitude clsAltitude = (ClsAltitude)this.colorTables_listBox_colorTableList.SelectedItem;
                            this.colorTables_propertyGrid_colorTableProperties.SelectedObject = clsAltitude;
                            break;
                        }
                }
            }
        }
    }
}
