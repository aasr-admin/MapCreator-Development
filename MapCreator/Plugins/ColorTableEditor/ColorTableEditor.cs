using Altitude;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terrain;
using UltimaSDK_v432;

namespace MapCreator
{
    public partial class colorTableEditor : Form
    {
        private int i_Menu;
        private Art i_UOArt;

        private ClsAltitudeTable i_Altitude;
        private ClsTerrainTable i_Terrain;

        public colorTableEditor()
        {
            MaximizeBox = false;
            MinimizeBox = false;

            colorTableEditor cTE = this;
            base.Load += new EventHandler(cTE.colorTableEditor_Load);

            this.i_Menu = 0;
            this.i_Altitude = new ClsAltitudeTable();
            this.i_Terrain = new ClsTerrainTable();

            InitializeComponent();
        }

        private void colorTableEditor_Load(object sender, EventArgs e)
        {
            this.i_Menu = 0;
            this.groupBox1.Text = "Terrain List";

            this.TerrainPictureBox.Visible = false;
            this.pictureBox3.Visible = true;

            this.ColorTablePictureBox.Show();
            this.PhotoshopLabel.Show();
            this.AltitudeLabel.Hide();
            this.Terrainlabel.Hide();
            this.AltitudePictureBox.Hide();
        }


        /// Terrain
        private void MenuItem2_Click(object sender, EventArgs e)
        {
            this.i_Menu = 0;
            this.groupBox1.Text = "Terrain List";

            this.i_Terrain.Load();
            this.i_Terrain.Display(this.listBox1);

            this.TerrainPictureBox.Visible = true;
            this.pictureBox3.Visible = false;

            this.PhotoshopLabel.Hide();
            this.AltitudeLabel.Hide();
            this.Terrainlabel.Show();
            this.AltitudePictureBox.Hide();
            this.ColorTablePictureBox.Show();
        }

        private void MenuItem9_Click(object sender, EventArgs e)
        {
            this.i_Terrain.SaveACT();
        }

        private void MenuItem13_Click(object sender, EventArgs e)
        {
            this.i_Terrain.SaveACO();
        }


        /// Altitude
        private void MenuItem5_Click(object sender, EventArgs e)
        {
            this.i_Menu = 1;
            this.groupBox1.Text = "Altitude List";

            this.i_Altitude.Load();
            this.i_Altitude.Display(this.listBox1);

            this.TerrainPictureBox.Visible = false;
            this.pictureBox3.Visible = true;

            this.PhotoshopLabel.Hide();
            this.AltitudeLabel.Show();
            this.Terrainlabel.Hide();
            this.AltitudePictureBox.Show();
            this.ColorTablePictureBox.Show();
        }

        private void MenuItem12_Click(object sender, EventArgs e)
        {
            this.i_Altitude.SaveACT();
        }

        private void MenuItem14_Click(object sender, EventArgs e)
        {
            this.i_Altitude.SaveACO();
        }

        /// Color Information Display
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            {
                switch (this.i_Menu)
                {
                    case 0:
                        {
                            ClsTerrain selectedItem = (ClsTerrain)this.listBox1.SelectedItem;
                            this.propertyGrid1.SelectedObject = selectedItem;
                            this.TerrainPictureBox.Image = Art.GetLand(selectedItem.TileID);
                            break;
                        }
                    case 1:
                        {
                            ClsAltitude clsAltitude = (ClsAltitude)this.listBox1.SelectedItem;
                            this.propertyGrid1.SelectedObject = clsAltitude;
                            break;
                        }
                }
            }
        }


        /// MainMenu
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
