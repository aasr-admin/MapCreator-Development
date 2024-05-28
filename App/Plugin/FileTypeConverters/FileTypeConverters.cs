using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Formats.Tar;

namespace MapCreator
{
    public partial class fileTypeConverters : Form
    {
        MUL2UOPConverter conv;

        private int m_Total, m_Success;

        public fileTypeConverters()
        {
            MaximizeBox = false;
            MinimizeBox = false;

            conv = new MUL2UOPConverter();

            InitializeComponent();
        }

        /// .MUL -> .BMP
        private void mul2bmpConverter_searchButton01_compiledFacetLocation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "map files (map?.mul)|map?.mul";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mul2bmpConverter_textBox01_compiledFacetLocation.Text = dialog.FileName;
            }
        }

        private void mul2bmpConverter_searchButton02_radarcolFileLocation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "radarcol.mul|radarcol.mul";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mul2bmpConverter_textBox02_radarcolFileLocation.Text = dialog.FileName;
            }
        }

        private unsafe void mul2bmpConverter_convertFacet2BitmapButton_Click(object sender, EventArgs e)
        {
            if (mul2bmpConverter_textBox01_compiledFacetLocation.Text.Length == 0 || mul2bmpConverter_textBox02_radarcolFileLocation.Text.Length == 0 || mul2bmpConverter_textBox03_mapDimensions_width.Text.Length == 0 || mul2bmpConverter_textBox04_mapDimensions_height.Text.Length == 0)
            {
                MessageBox.Show("Please Locate Your Map.mul And Radarcol.mul Before Proceeding...");
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "*.bmp|*.bmp";
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string output = dialog.FileName;
                    try
                    {
                        int mapWidth = Int32.Parse(mul2bmpConverter_textBox03_mapDimensions_width.Text);
                        int mapHeight = Int32.Parse(mul2bmpConverter_textBox04_mapDimensions_height.Text);
                        Bitmap bitmap = new Bitmap(mapWidth, mapHeight);
                        BitmapData bd = bitmap.LockBits(new Rectangle(0, 0, mapWidth, mapHeight), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb555);

                        ushort[] colors = new RadarColReader(mul2bmpConverter_textBox02_radarcolFileLocation.Text).Colors;
                        ushort[] tiles = new MapReader(mul2bmpConverter_textBox01_compiledFacetLocation.Text, mapWidth, mapHeight).Tiles;

                        // TODO: Load and make use of MapDif, Statics and StaticsDif

                        ushort* bdPtr = (ushort*)bd.Scan0;

                        for (int i = 0; i < tiles.Length; i++)
                            bdPtr[i] = colors[tiles[i]];

                        bitmap.UnlockBits(bd);
                        bitmap.Save(output);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
            }
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();

            StaticForm<facetBuilder>.Open();
        }

        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StaticForm<communityCredits>.Open();
        }

        /// .MUL <-> .UOP
        private string FixPath(string file)
        {
            return (file == null) ? null : Path.Combine(mul2uopConverter_textBox01_facetFileLocation.Text, file);
        }

        private void Pack(string inFile, string inIdx, string outFile, FileType type, int typeIndex)
        {
            try
            {
                mul2uopConverter_statusInfoText.Text = inFile;

                Refresh();
                inFile = FixPath(inFile);

                if (!File.Exists(inFile))
                    return;

                outFile = FixPath(outFile);

                if (File.Exists(outFile))
                {
                    return;
                }

                inIdx = FixPath(inIdx);
                ++m_Total;

                conv.ToUOP(inFile, inIdx, outFile, type, typeIndex);
                ++m_Success;

            }
            catch
            {
                MessageBox.Show("An error occured while performing the action");
            }
        }

        private void Extract(string inFile, string outFile, string outIdx, FileType type, int typeIndex)
        {
            try
            {
                mul2uopConverter_statusInfoText.Text = inFile;

                Refresh();
                inFile = FixPath(inFile);

                if (!File.Exists(inFile))
                    return;

                outFile = FixPath(outFile);

                if (File.Exists(outFile))
                {
                    return;
                }

                outIdx = FixPath(outIdx);
                ++m_Total;

                conv.FromUOP(inFile, outFile, outIdx, type, typeIndex);
                ++m_Success;
            }
            catch 
            {
                MessageBox.Show("An error occured while performing the action");
            }
        }

        private void mul2uopConverter_searchButton01_mulLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                SelectedPath = this.mul2uopConverter_textBox01_facetFileLocation.Text
            };
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.mul2uopConverter_textBox01_facetFileLocation.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void mul2uopConverter_textBox01_mulLocation_Load(object sender, EventArgs e)
        {
            this.mul2uopConverter_textBox01_facetFileLocation.Text = Directory.GetCurrentDirectory();
        }

        private void mul2uopConverter_convertFacet2UOPButton_Click(object sender, EventArgs e)
        {
            if (mul2uopConverter_textBox01_facetFileLocation.Text == string.Empty || mul2uopConverter_textBox01_facetFileLocation.Text == null)
            {
                MessageBox.Show(" ERROR: You Must Specify The Location Path\n Of The [.mul] Files You Want Converted!");
                return;
            }

            if (mul2uopConverter_custom255Selection.Checked)
            {
                for (int i = 0; i <= 255; ++i)
                {
                    string map = String.Format("map{0}", i);

                    Pack(map + ".mul", null, map + "LegacyMUL.uop", FileType.MapLegacyMUL, i);
                    Pack(map + "x.mul", null, map + "xLegacyMUL.uop", FileType.MapLegacyMUL, i);
                }

                mul2uopConverter_statusInfoText.Text = string.Format("Done ({0}/{1} files converted)", m_Success, m_Total);
            }
            else if (mul2uopConverter_original6Selection.Checked)
            {
                for (int i = 0; i <= 5; ++i)
                {
                    string map = String.Format("map{0}", i);

                    Pack(map + ".mul", null, map + "LegacyMUL.uop", FileType.MapLegacyMUL, i);
                    Pack(map + "x.mul", null, map + "xLegacyMUL.uop", FileType.MapLegacyMUL, i);
                }


                mul2uopConverter_statusInfoText.Text = string.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
            }
            else if (mul2uopConverter_custom255Selection.Checked == false || mul2uopConverter_original6Selection.Checked == false)
            {
                MessageBox.Show("   ERROR: Please Select A Facet Allowance Type Before This Program Can Proceed!\n");
                return;
            }
        }

        private void mul2uopConverter_convertFacet2MULButton_Click(object sender, EventArgs e)
        {
            if (mul2uopConverter_textBox01_facetFileLocation.Text == string.Empty || mul2uopConverter_textBox01_facetFileLocation.Text == null)
            {
                MessageBox.Show(" ERROR: You Must Specify The Location Path\n Of The [.uop] Files You Want Converted!");
                return;
            }

            if (mul2uopConverter_custom255Selection.Checked)
            {
                for (int i = 0; i <= 255; ++i)
                {
                    string map = String.Format("map{0}", i);

                    Extract(map + "LegacyMUL.uop", map + ".mul", null, FileType.MapLegacyMUL, i);
                    Extract(map + "xLegacyMUL.uop", map + "x.mul", null, FileType.MapLegacyMUL, i);
                }

                mul2uopConverter_statusInfoText.Text = string.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
            }
            else if (mul2uopConverter_original6Selection.Checked)
            {
                for (int i = 0; i <= 5; ++i)
                {
                    string map = String.Format("map{0}", i);

                    Extract(map + "LegacyMUL.uop", map + ".mul", null, FileType.MapLegacyMUL, i);
                    Extract(map + "xLegacyMUL.uop", map + "x.mul", null, FileType.MapLegacyMUL, i);
                }

                mul2uopConverter_statusInfoText.Text = string.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
            }
            else if (mul2uopConverter_custom255Selection.Checked == false || mul2uopConverter_original6Selection.Checked == false)
            {
                MessageBox.Show("   ERROR: Please Select A Facet Allowance Type Before This Program Can Proceed!\n");
                return;
            }
        }
    }
}