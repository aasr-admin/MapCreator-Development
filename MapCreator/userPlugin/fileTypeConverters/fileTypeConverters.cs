using Cartography.userPlugin.fileTypeConverters;

using System.Drawing.Imaging;
using System.IO;

namespace MapCreator.userPlugin
{
	public partial class FileTypeConverters : Form
	{
		private int m_Total, m_Success;

		public FileTypeConverters()
		{
			MaximizeBox = false;
			MinimizeBox = false;

			InitializeComponent();
		}

		/// .MUL -> .BMP
		private void Mul2bmpConverter_searchButton01_compiledFacetLocation_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				Filter = "map files (map?.mul)|map?.mul"
			};
			var result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				mul2bmpConverter_textBox01_compiledFacetLocation.Text = dialog.FileName;
			}
		}

		private void Mul2bmpConverter_searchButton02_radarcolFileLocation_Click(object sender, EventArgs e)
		{
			var dialog = new OpenFileDialog
			{
				Filter = "radarcol.mul|radarcol.mul"
			};
			var result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				mul2bmpConverter_textBox02_radarcolFileLocation.Text = dialog.FileName;
			}
		}

		private unsafe void Mul2bmpConverter_convertFacet2BitmapButton_Click(object sender, EventArgs e)
		{
			if (mul2bmpConverter_textBox01_compiledFacetLocation.Text.Length == 0 || mul2bmpConverter_textBox02_radarcolFileLocation.Text.Length == 0 || mul2bmpConverter_textBox03_mapDimensions_width.Text.Length == 0 || mul2bmpConverter_textBox04_mapDimensions_height.Text.Length == 0)
			{
				_ = MessageBox.Show("Please Locate Your Map.mul And Radarcol.mul Before Proceeding...");
			}
			else
			{
				var dialog = new SaveFileDialog
				{
					Filter = "*.bmp|*.bmp"
				};
				var result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					var output = dialog.FileName;
					try
					{
						var mapWidth = Int32.Parse(mul2bmpConverter_textBox03_mapDimensions_width.Text);
						var mapHeight = Int32.Parse(mul2bmpConverter_textBox04_mapDimensions_height.Text);
						var bitmap = new Bitmap(mapWidth, mapHeight);
						var bd = bitmap.LockBits(new Rectangle(0, 0, mapWidth, mapHeight), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb555);

						var colors = new RadarColReader(mul2bmpConverter_textBox02_radarcolFileLocation.Text).Colors;
						var tiles = new MapReader(mul2bmpConverter_textBox01_compiledFacetLocation.Text, mapWidth, mapHeight).Tiles;

						// TODO: Load and make use of MapDif, Statics and StaticsDif

						var bdPtr = (ushort*)bd.Scan0;

						for (var i = 0; i < tiles.Length; i++)
						{
							bdPtr[i] = colors[tiles[i]];
						}

						bitmap.UnlockBits(bd);
						bitmap.Save(output);
					}
					catch (Exception exception)
					{
						_ = MessageBox.Show(exception.ToString());
					}
				}
			}
		}

		private void SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Hide();

			var facetBuilderForm = new FacetBuilder();
			facetBuilderForm.Show();
		}

		private void SToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			var communityCreditsForm = new CommunityCredits();
			communityCreditsForm.Show();
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
				{
					return;
				}

				outFile = FixPath(outFile);

				if (File.Exists(outFile))
				{
					return;
				}

				inIdx = FixPath(inIdx);
				++m_Total;

				MUL2UOPConverter.ToUOP(inFile, inIdx, outFile, type, typeIndex);
				++m_Success;

			}
			catch (Exception)
			{
				_ = MessageBox.Show("An error occured while performing the action");
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
				{
					return;
				}

				outFile = FixPath(outFile);

				if (File.Exists(outFile))
				{
					return;
				}

				outIdx = FixPath(outIdx);
				++m_Total;

				MUL2UOPConverter.FromUOP(inFile, outFile, outIdx, type, typeIndex);
				++m_Success;
			}
			catch (Exception)
			{
				_ = MessageBox.Show("An error occured while performing the action");
			}
		}

		private void Mul2uopConverter_searchButton01_mulLocation_Click(object sender, EventArgs e)
		{
			var folderBrowserDialog = new FolderBrowserDialog()
			{
				SelectedPath = mul2uopConverter_textBox01_facetFileLocation.Text
			};
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				mul2uopConverter_textBox01_facetFileLocation.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void Mul2uopConverter_textBox01_mulLocation_Load(object sender, EventArgs e)
		{
			mul2uopConverter_textBox01_facetFileLocation.Text = Directory.GetCurrentDirectory();
		}

		private void Mul2uopConverter_convertFacet2UOPButton_Click(object sender, EventArgs e)
		{
			if (mul2uopConverter_textBox01_facetFileLocation.Text == String.Empty || mul2uopConverter_textBox01_facetFileLocation.Text == null)
			{
				_ = MessageBox.Show(" ERROR: You Must Specify The Location Path\n Of The [.mul] Files You Want Converted!");
				return;
			}

			if (mul2uopConverter_custom255Selection.Checked)
			{
				for (var i = 0; i <= 255; ++i)
				{
					var map = String.Format("map{0}", i);

					Pack(map + ".mul", null, map + "LegacyMUL.uop", FileType.MapLegacyMUL, i);
					Pack(map + "x.mul", null, map + "xLegacyMUL.uop", FileType.MapLegacyMUL, i);
				}

				mul2uopConverter_statusInfoText.Text = String.Format("Done ({0}/{1} files converted)", m_Success, m_Total);
			}
			else if (mul2uopConverter_original6Selection.Checked)
			{
				for (var i = 0; i <= 5; ++i)
				{
					var map = String.Format("map{0}", i);

					Pack(map + ".mul", null, map + "LegacyMUL.uop", FileType.MapLegacyMUL, i);
					Pack(map + "x.mul", null, map + "xLegacyMUL.uop", FileType.MapLegacyMUL, i);
				}

				mul2uopConverter_statusInfoText.Text = String.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
			}
			else if (mul2uopConverter_custom255Selection.Checked == false || mul2uopConverter_original6Selection.Checked == false)
			{
				_ = MessageBox.Show("   ERROR: Please Select A Facet Allowance Type Before This Program Can Proceed!\n");
				return;
			}
		}

		private void Mul2uopConverter_convertFacet2MULButton_Click(object sender, EventArgs e)
		{
			if (mul2uopConverter_textBox01_facetFileLocation.Text == String.Empty || mul2uopConverter_textBox01_facetFileLocation.Text == null)
			{
				_ = MessageBox.Show(" ERROR: You Must Specify The Location Path\n Of The [.uop] Files You Want Converted!");
				return;
			}

			if (mul2uopConverter_custom255Selection.Checked)
			{
				for (var i = 0; i <= 255; ++i)
				{
					var map = String.Format("map{0}", i);

					Extract(map + "LegacyMUL.uop", map + ".mul", null, FileType.MapLegacyMUL, i);
					Extract(map + "xLegacyMUL.uop", map + "x.mul", null, FileType.MapLegacyMUL, i);
				}

				mul2uopConverter_statusInfoText.Text = String.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
			}
			else if (mul2uopConverter_original6Selection.Checked)
			{
				for (var i = 0; i <= 5; ++i)
				{
					var map = String.Format("map{0}", i);

					Extract(map + "LegacyMUL.uop", map + ".mul", null, FileType.MapLegacyMUL, i);
					Extract(map + "xLegacyMUL.uop", map + "x.mul", null, FileType.MapLegacyMUL, i);
				}

				mul2uopConverter_statusInfoText.Text = String.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
			}
			else if (mul2uopConverter_custom255Selection.Checked == false || mul2uopConverter_original6Selection.Checked == false)
			{
				_ = MessageBox.Show("   ERROR: Please Select A Facet Allowance Type Before This Program Can Proceed!\n");
				return;
			}
		}
	}
}