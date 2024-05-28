using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Xml;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace MapCreator
{
    public partial class facetBuilder : Form
    {
        private Bitmap i_Terrain;
        private readonly ClsTerrainTable iTerrain;
        private Bitmap i_Altitude;
        private readonly ClsAltitudeTable iAltitude;
        private bool i_RandomStatic;

        public facetBuilder()
        {
            iTerrain = new ClsTerrainTable();
            iAltitude = new ClsAltitudeTable();
            i_RandomStatic = true;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Visible && facetBuilder_panel_workbench.Visible)
            {
                facetBuilder_panel_workbench.Hide();
            }

            #region Workbench Loading

            /// GroupBox: Create Facet Bitmap Files

            IEnumerator enumerator = null;

            var iLogger = StaticForm<buildLogger>.Open(this);

            var x = iLogger.Location.X + 100;
            var location = iLogger.Location;
            var point = new Point(x, location.Y + 100);
            Location = point;

            iTerrain.Load();
            iAltitude.Load();

            #region Data Directory Modification

            var str = string.Format("{0}\\MapCompiler\\Engine\\{1}", Directory.GetCurrentDirectory(), "MapInfo.xml");

            #endregion

            facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();
            iTerrain.Display(facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox);

            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(str);
                try
                {
                    facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetSize_comboBox.Items.Clear();
                    try
                    {
                        enumerator = xmlDocument.SelectNodes("//Maps/Map").GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var mapInfo = new MapInfo((XmlElement)enumerator.Current);
                            _ = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetSize_comboBox.Items.Add(mapInfo);
                        }
                    }
                    finally
                    {
                        if (enumerator is IDisposable)
                        {
                            ((IDisposable)enumerator).Dispose();
                        }
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    var exception = exception1;
                    iLogger.LogMessage(string.Format("XML Error:{0}", exception.Message));
                    ProjectData.ClearProjectError();
                }
            }
            catch (Exception exception2)
            {
                ProjectData.SetProjectError(exception2);
                iLogger.LogMessage(string.Format("Unable to find:{0}", str));
                ProjectData.ClearProjectError();
            }

            /// GroupBox: Sync Your Altitude Bitmap

            facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text = Directory.GetCurrentDirectory();

            iTerrain.Load();
            iAltitude.Load();

            /// GroupBox: Compile Your New Facet

            facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text = AppDomain.CurrentDomain.BaseDirectory;

            #endregion
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Exit();
        }

        private void facetBuilder_menuStrip_button_gettingStarted_createColorTables_Click(object sender, EventArgs e)
        {
            StaticForm<colorTables>.Open();
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
            StaticForm<userPlugins>.Open();
        }

        private void facetBuilder_menuStrip_button_uploadPlugin_Click(object sender, EventArgs e)
        {

        }

        private void facetBuilder_menuStrip_button_information_Click(object sender, EventArgs e)
        {
            StaticForm<communityCredits>.Open();
        }

        private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_MouseEnter(object sender, EventArgs e)
        {
            facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles.ForeColor = Color.LimeGreen;
        }

        private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_createFacetBitmapFiles_Click(object sender, EventArgs e)
        {
            /// Show These Controls
            facetBuilder_panel_workbench.Show();
            facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles.Show();

            /// Hide These Controls
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
            var folderBrowserDialog = new FolderBrowserDialog()
            {
                SelectedPath = facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_button_createFacetBitmapFiles_Click(object sender, EventArgs e)
        {
            var iLogger = StaticForm<buildLogger>.Open(this);

            byte altID;
            byte groupID;

            var selectedItem = (MapInfo)facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetSize_comboBox.SelectedItem;

            if (selectedItem == null)
            {
                iLogger.LogMessage("Error: Select a Map Type.");
            }
            else if (StringType.StrCmp(facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetName_textBox.Text, string.Empty, false) != 0)
            {
                var str = string.Format("{0}/{1}/Map{2}", facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_facetName_textBox.Text, selectedItem.MapNumber);

                if (!Directory.Exists(str))
                {
                    _ = Directory.CreateDirectory(str);
                }

                if (facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox.SelectedItem != null)
                {
                    var clsTerrain = (ClsTerrain)facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_baseTerrain_comboBox.SelectedItem;
                    groupID = clsTerrain.GroupID;
                    altID = clsTerrain.AltID;
                }
                else
                {
                    groupID = 9;
                    altID = 66;
                }

                iLogger.LogMessage("Creating Terrain Image.");
                iLogger.StartTask();

                try
                {
                    var str1 = string.Format("{0}/{1}", str, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_terrainBitmap_textBox.Text);
                    var palette = MakeTerrainBitmapFile(selectedItem.XSize, selectedItem.YSize, groupID, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_addDungeonArea_checkBox.Checked);
                    palette.Palette = iTerrain.GetPalette();
                    palette.Save(str1, ImageFormat.Bmp);
                    palette.Dispose();
                }
                catch (Exception exception)
                {
                    ProjectData.SetProjectError(exception);
                    iLogger.LogMessage("Error: Problem creating Terrain Image.");
                    ProjectData.ClearProjectError();
                }

                //this.iLogger.EndTask();
                iLogger.LogTimeStamp();
                iLogger.LogMessage("Creating Altitude Image.");
                iLogger.StartTask();

                try
                {
                    var str2 = string.Format("{0}/{1}", str, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_altitudeBitmap_textBox.Text);
                    var altPalette = MakeAltitudeBitmapFile(selectedItem.XSize, selectedItem.YSize, altID, facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles_label_addDungeonArea_checkBox.Checked);
                    altPalette.Palette = iAltitude.GetAltPalette();
                    altPalette.Save(str2, ImageFormat.Bmp);
                    altPalette.Dispose();
                }
                catch (Exception exception2)
                {
                    ProjectData.SetProjectError(exception2);
                    var exception1 = exception2;
                    iLogger.LogMessage("Error: Problem creating Altitude Image.");
                    iLogger.LogMessage(exception1.Message);
                    ProjectData.ClearProjectError();
                }

                //this.iLogger.EndTask();
                iLogger.LogTimeStamp();
                iLogger.LogMessage("Done.");
            }
            else
            {
                iLogger.LogMessage("Error: Enter a project Name.");
            }
        }

        public Bitmap MakeTerrainBitmapFile(int xSize, int ySize, byte DefaultTerrain, bool Dungeon)
        {
            var bitmap = new Bitmap(xSize, ySize, PixelFormat.Format8bppIndexed)
            {
                Palette = iTerrain.GetPalette()
            };

            var rectangle = new Rectangle(0, 0, xSize, ySize);
            var bitmapDatum = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var scan0 = bitmapDatum.Scan0;
            var width = checked(bitmapDatum.Width * bitmapDatum.Height);
            var defaultTerrain = new byte[checked(checked(width - 1) + 1)];

            Marshal.Copy(scan0, defaultTerrain, 0, width);

            if (!Dungeon)
            {
                var num = checked(xSize - 1);

                for (var i = 0; i <= num; i++)
                {
                    var num1 = checked(ySize - 1);

                    for (var j = 0; j <= num1; j++)
                    {
                        defaultTerrain[checked(checked(j * xSize) + i)] = DefaultTerrain;
                    }
                }
            }
            else
            {
                var num2 = checked(xSize - 1);

                for (var k = 0; k <= num2; k++)
                {
                    var num3 = checked(ySize - 1);

                    for (var l = 0; l <= num3; l++)
                    {
                        if (k <= 5119)
                        {
                            defaultTerrain[checked(checked(l * xSize) + k)] = DefaultTerrain;
                        }
                        else
                        {
                            defaultTerrain[checked(checked(l * xSize) + k)] = 19;
                        }
                    }
                }
            }

            Marshal.Copy(defaultTerrain, 0, scan0, width);
            bitmap.UnlockBits(bitmapDatum);

            return bitmap;
        }

        public Bitmap MakeAltitudeBitmapFile(int xSize, int ySize, byte DefaultAlt, bool Dungeon)
        {
            var bitmap = new Bitmap(xSize, ySize, PixelFormat.Format8bppIndexed)
            {
                Palette = iAltitude.GetAltPalette()
            };
            var rectangle = new Rectangle(0, 0, xSize, ySize);
            var bitmapDatum = bitmap.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var scan0 = bitmapDatum.Scan0;
            var width = checked(bitmapDatum.Width * bitmapDatum.Height);
            var defaultAlt = new byte[width];
            Marshal.Copy(scan0, defaultAlt, 0, width);
            if (!Dungeon)
            {
                var num = xSize - 1;
                for (var i = 0; i <= num; i++)
                {
                    var num1 = ySize - 1;
                    for (var j = 0; j <= num1; j++)
                    {
                        defaultAlt[(j * xSize) + i] = DefaultAlt;
                    }
                }
            }
            else
            {
                var num2 = xSize - 1;
                for (var k = 0; k <= num2; k++)
                {
                    var num3 = ySize - 1;
                    for (var l = 0; l <= num3; l++)
                    {
                        if (k <= 5119)
                        {
                            defaultAlt[(l * xSize) + k] = (byte)DefaultAlt;
                        }
                        else
                        {
                            defaultAlt[(l * xSize) + k] = 72;
                        }
                    }
                }
            }

            Marshal.Copy(defaultAlt, 0, scan0, width);
            bitmap.UnlockBits(bitmapDatum);
            return bitmap;
        }

        #endregion

        private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_MouseEnter(object sender, EventArgs e)
        {
            facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap.ForeColor = Color.LimeGreen;
        }

        private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_syncYourAltitudeBitmap_Click(object sender, EventArgs e)
        {
            /// Show These Controls
            facetBuilder_panel_workbench.Show();
            facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap.Show();

            /// Hide These Controls
            facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles.Hide();
            facetBuilder_panel_workbench_groupBox_compileYourNewFacet.Hide();
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

        private async void facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_button_syncAltitudeBitmapFile_Click(object sender, EventArgs e)
        {
            var iLogger = StaticForm<buildLogger>.Open(this);

            IProgress<string> logger = new Progress<string>(iLogger.LogMessage);

            IProgress<int> progress = new Progress<int>(i => facetBuilder_panel_workbench_progressBar.Value = Math.Abs(i)); // TODO: temporary fix, i didn't get why it put -73
            
            await Task.Run(() => EncodeAltitudeBitmapHelper.MakeAltitudeImage(facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_terrainBitmap_textBox.Text, facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap_label_altitudeBitmap_textBox.Text, iAltitude, iTerrain, progress, logger));
            
            progress.Report(0);
        }

        #endregion

        private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_MouseEnter(object sender, EventArgs e)
        {
            facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet.ForeColor = Color.LimeGreen;
        }

        private void facetBuilder_panel_workbench_selection_groupBox_createYourWorld_button_compileYourNewFacet_Click(object sender, EventArgs e)
        {
            /// Show These Controls
            facetBuilder_panel_workbench.Show();
            facetBuilder_panel_workbench_groupBox_compileYourNewFacet.Show();

            /// Hide These Controls
            facetBuilder_panel_workbench_groupBox_createFacetBitmapFiles.Hide();
            facetBuilder_panel_workbench_groupBox_syncYourAltitudeBitmap.Hide();
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

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_toggleFacetStatics_radioButton_on_CheckedChanged(object sender, EventArgs e)
        {
            i_RandomStatic = true;
            /// Form NotificationAlertOn = new NotificationAlertOn();
            /// NotificationAlertOn.Show();
            System.Media.SystemSounds.Beep.Play();
        }

        private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_toggleFacetStatics_radioButton_off_CheckedChanged(object sender, EventArgs e)
        {
            i_RandomStatic = false;
            /// Form NotificationAlertOff = new NotificationAlertOff();
            /// NotificationAlertOff.Show();
            System.Media.SystemSounds.Beep.Play();
        }

        private void facetBuilder_panel_workbench_groupBox_compileYourNewFacet_button_createFacetFiles_Click(object sender, EventArgs e)
        {
            if (Interaction.MsgBox("You are about to create the Mul Files\r\nAre you sure ?", MsgBoxStyle.YesNo, "Make UO Map") == MsgBoxResult.Yes)
            {
                var mc_MapMake = this;
                ///CompileYourNewMap uOMapMake = this;
                ///
                new Thread(new ThreadStart(CreateFacet_mul_Files)).Start();
            }
        }

        private void CreateFacet_mul_Files()
        {
            var iLogger = StaticForm<buildLogger>.Open(this);

            sbyte altID = 0;
            string str;
            IEnumerator enumerator = null;
            var transitionTable = new TransitionTable();
            var now = DateTime.Now;
            iLogger.StartTask();
            iLogger.LogMessage("Loading Terrain Image.");
            try
            {
                // Reading Terrain.bmp (Bitmap File)
                str = string.Format("{0}\\{1}", facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_terrainBitmap_textBox.Text);
                iLogger.LogMessage(str);
                i_Terrain = new Bitmap(str);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                var exception = exception1;
                iLogger.LogMessage("Problem with Loading Terrain Image.");
                iLogger.LogMessage(exception.Message);
                ProjectData.ClearProjectError();
                return;
            }

            iLogger.LogMessage("Loading Altitude Image.");
            try
            {
                // Reading Altitude.bmp (Bitmap File)
                str = string.Format("{0}\\{1}", facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_altitudeBitmap_textBox.Text);
                iLogger.LogMessage(str);
                i_Altitude = new Bitmap(str);
            }
            catch (Exception exception3)
            {
                ProjectData.SetProjectError(exception3);
                var exception2 = exception3;
                iLogger.LogMessage("Problem with Loading Altitude Image.");
                iLogger.LogMessage(exception2.Message);
                ProjectData.ClearProjectError();
                return;
            }
            //this.iLogger.EndTask();
            iLogger.LogTimeStamp();
            iLogger.LogMessage("Preparing Image Files.");
            iLogger.StartTask();
            var width = i_Terrain.Width;
            var height = i_Terrain.Height;
            var rectangle = new Rectangle(0, 0, width, height);
            var bitmapDatum = i_Terrain.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var scan0 = bitmapDatum.Scan0;
            var width1 = bitmapDatum.Width * bitmapDatum.Height;
            var numArray = new byte[width1];
            Marshal.Copy(scan0, numArray, 0, width1);
            var bitmapDatum1 = i_Altitude.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            var intPtr = bitmapDatum1.Scan0;
            var width2 = bitmapDatum1.Width * bitmapDatum1.Height;
            var numArray1 = new byte[width2];
            Marshal.Copy(intPtr, numArray1, 0, width2);
            //this.iLogger.EndTask();
            iLogger.LogTimeStamp();
            iLogger.LogMessage("Creating Master Terrian Table.");
            iLogger.StartTask();
            var mapCell = new MapCell[width + 1, height + 1];
            var clsAltitudeTable = new ClsAltitudeTable();
            clsAltitudeTable.Load();
            try
            {
                var num5 = width - 1;
                for (var i = 0; i <= num5; i++)
                {
                    var num6 = height - 1;
                    for (var j = 0; j <= num6; j++)
                    {
                        var num7 = (j * width) + i;
                        var getAltitude = clsAltitudeTable.GetAltitude(numArray1[num7]);
                        mapCell[i, j] = new MapCell(numArray[num7], getAltitude.GetAltitude);
                    }
                }
            }
            catch (Exception exception4)
            {
                ProjectData.SetProjectError(exception4);
                iLogger.LogMessage("Altitude image needs to be rebuilt");
                ProjectData.ClearProjectError();
                return;
            }

            i_Terrain.Dispose();
            i_Altitude.Dispose();
            iLogger.LogTimeStamp();
            width--;
            height--;
            var num8 = (int)Math.Round((width / 8.0) - 1);
            var num9 = (int)Math.Round((height / 8.0) - 1);
            iLogger.LogMessage("Load Transition Tables.");
            iLogger.StartTask();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            #region Data Directory Modification

            baseDirectory = string.Concat(baseDirectory, "MapCompiler\\Engine\\Transitions\\");

            #endregion

            if (Directory.Exists(baseDirectory))
            {
                transitionTable.MassLoad(baseDirectory);
                iLogger.LogTimeStamp();
                iLogger.LogMessage("Preparing Static Tables");
                var collections = new Collection[num8 + 1, num9 + 1];
                var num10 = num8;
                for (var k = 0; k <= num10; k++)
                {
                    var num11 = num9;
                    for (var l = 0; l <= num11; l++)
                    {
                        collections[k, l] = new Collection();
                    }
                }

                iLogger.LogMessage("Applying Transition Tables.");
                iLogger.StartTask();
                facetBuilder_panel_workbench_progressBar.Invoke(() => { facetBuilder_panel_workbench_progressBar.Maximum = width; });
                var clsTerrainTable = new ClsTerrainTable();
                clsTerrainTable.Load();
                var mapTile = new MapTile();
                var transition = new Transition();

                for (var x = 0; x <= width; x++)
                {
                    var num1 = x != 0 ? (x - 1) : width;
                    var num2 = x != width ? (x + 1) : 0;

                    for (var y = 0; y <= height; y++)
                    {
                        var num4 = y != 0 ? (y - 1) : height;
                        var num3 = y != height ? (y + 1) : 0;

                        var groupID = new object[] { mapCell[num1, num4].GroupID, mapCell[x, num4].GroupID, mapCell[num2, num4].GroupID, mapCell[num1, y].GroupID, mapCell[x, y].GroupID, mapCell[num2, y].GroupID, mapCell[num1, num3].GroupID, mapCell[x, num3].GroupID, mapCell[num2, num3].GroupID };
                        var str1 = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}{4:X2}{5:X2}{6:X2}{7:X2}{8:X2}", groupID);
                        try
                        {
                            transition = (Transition)transitionTable.GetTransitionTable[str1];

                            if (transition == null)
                            {
                                var terrianGroup = clsTerrainTable.TerrianGroup(mapCell[x, y].GroupID);
                                mapCell[x, y].TileID = terrianGroup.TileID;
                                mapCell[x, y].AltID = altID;
                                terrianGroup = null;
                            }
                            else
                            {
                                altID = mapCell[x, y].AltID;
                                mapTile = transition.GetRandomMapTile();
                                if (mapTile == null)
                                {
                                    var clsTerrain = clsTerrainTable.TerrianGroup(mapCell[x, y].GroupID);
                                    mapCell[x, y].TileID = clsTerrain.TileID;
                                    mapCell[x, y].ChangeAltID(clsTerrain.AltID);
                                    clsTerrain = null;
                                }
                                else
                                {
                                    var mapTile1 = mapTile;
                                    mapCell[x, y].TileID = mapTile1.TileID;
                                    mapCell[x, y].ChangeAltID(mapTile1.AltIDMod);
                                    mapTile1 = null;
                                }

                                transition.GetRandomStaticTiles((byte)x, (byte)y, altID, collections, i_RandomStatic);
                            }

                            if (mapCell[x, y].GroupID == 254)
                            {
                                mapCell[x, y].TileID = 1078;
                                mapCell[x, y].AltID = 0;
                            }
                        }
                        catch (Exception exception6)
                        {
                            ProjectData.SetProjectError(exception6);
                            var exception5 = exception6;

                            var loggerForm = iLogger;

                            groupID = new object[] { x, y, altID, str1 };

                            loggerForm.LogMessage(string.Format("\r\nLocation: X:{0}, Y:{1}, Z:{2} Hkey:{3}", groupID));
                            iLogger.LogMessage(exception5.ToString());

                            ProjectData.ClearProjectError();
                            return;
                        }
                    }

                    facetBuilder_panel_workbench_progressBar.Invoke(() => { facetBuilder_panel_workbench_progressBar.Value = x; });
                }

                iLogger.LogTimeStamp();
                iLogger.LogMessage("Second Pass.");
                iLogger.StartTask();
                var altID1 = new sbyte[9];
                var roughEdge = new RoughEdge();

                for (var o = 0; o <= width; o++)
                {
                    var num1 = o != 0 ? (o - 1) : width;
                    var num2 = o != width ? (o + 1) : 0;

                    for (var p = 0; p <= height; p++)
                    {
                        var num4 = p != 0 ? (p - 1) : height;
                        var num3 = p != height ? (p + 1) : 0;

                        mapCell[o, p].ChangeAltID(roughEdge.CheckCorner(mapCell[num1, num4].TileID));
                        mapCell[o, p].ChangeAltID(roughEdge.CheckLeft(mapCell[num1, p].TileID));
                        mapCell[o, p].ChangeAltID(roughEdge.CheckTop(mapCell[o, num4].TileID));
                        if (mapCell[o, p].GroupID == 20)
                        {
                            altID1[0] = mapCell[num1, num4].AltID;
                            altID1[1] = mapCell[o, num4].AltID;
                            altID1[2] = mapCell[num2, num4].AltID;
                            altID1[3] = mapCell[num1, p].AltID;
                            altID1[4] = mapCell[o, p].AltID;
                            altID1[5] = mapCell[num2, p].AltID;
                            altID1[6] = mapCell[num1, num3].AltID;
                            altID1[7] = mapCell[o, num3].AltID;
                            altID1[8] = mapCell[num2, num3].AltID;
                            Array.Sort(altID1);
                            var single = 10f * VBMath.Rnd();
                            if (single == 0f)
                            {
                                mapCell[o, p].AltID = (sbyte)(altID1[8] - 4);
                            }
                            else if (single is >= 1f and <= 2f)
                            {
                                mapCell[o, p].AltID = (sbyte)(altID1[8] - 2);
                            }
                            else if (single is >= 3f and <= 7f)
                            {
                                mapCell[o, p].AltID = altID1[8];
                            }
                            else if (single is >= 8f and <= 9f)
                            {
                                mapCell[o, p].AltID = (sbyte)(altID1[8] + 2);
                            }
                            else if (single == 10f)
                            {
                                mapCell[o, p].AltID = (sbyte)(altID1[8] + 4);
                            }
                        }

                        if (clsTerrainTable.TerrianGroup(mapCell[o, p].GroupID).RandAlt)
                        {
                            var single1 = 10f * VBMath.Rnd();
                            if (single1 == 0f)
                            {
                                mapCell[o, p].ChangeAltID(-4);
                            }
                            else if (single1 is >= 1f and <= 2f)
                            {
                                mapCell[o, p].ChangeAltID(-2);
                            }
                            else if (single1 is >= 8f and <= 9f)
                            {
                                mapCell[o, p].ChangeAltID(2);
                            }
                            else if (single1 == 10f)
                            {
                                mapCell[o, p].ChangeAltID(4);
                            }
                        }
                    }

                    _ = facetBuilder_panel_workbench_progressBar.Invoke(() => facetBuilder_panel_workbench_progressBar.Value = o);
                }

                iLogger.LogTimeStamp();

                var num = 0;

                if (width == 6143)
                {
                    num = 0;
                }
                else if (width == 2303)
                {
                    num = 2;
                }
                else if (width == 2559)
                {
                    num = 3;
                }

                iLogger.LogMessage("\r\n");
                iLogger.LogMessage("Load . . . . . Import Tiles.");
                iLogger.StartTask();
                var importTile = new ImportTiles(collections, facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text);
                iLogger.LogTimeStamp();
                iLogger.LogMessage("\r\n");
                iLogger.LogMessage("Write Mul Files.");
                iLogger.StartTask();
                str = string.Format("{0}/Map{1}.mul", facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, num);
                iLogger.LogMessage(str);
                var fileStream = new FileStream(str, FileMode.Create);
                var binaryWriter = new BinaryWriter(fileStream);

                for (var q = 0; q <= width; q += 8)
                {
                    for (var r = 0; r <= height; r += 8)
                    {
                        binaryWriter.Write(1);
                        var num20 = 0;
                        do
                        {
                            var num21 = 0;
                            do
                            {
                                mapCell[q + num21, r + num20].WriteMapMul(binaryWriter);
                            }
                            while (++num21 < 8);
                        }
                        while (++num20 < 8);
                    }
                }

                binaryWriter.Close();
                fileStream.Close();
                str = string.Format("{0}/StaIdx{1}.mul", facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, num);
                var fileStream1 = new FileStream(str, FileMode.Create);
                iLogger.LogMessage(str);
                str = string.Format("{0}/Statics{1}.mul", facetBuilder_panel_workbench_groupBox_compileYourNewFacet_label_projectFolderLocation_textBox.Text, num);
                var fileStream2 = new FileStream(str, FileMode.Create);
                iLogger.LogMessage(str);
                var binaryWriter1 = new BinaryWriter(fileStream1);
                var binaryWriter2 = new BinaryWriter(fileStream2);

                for (var s = 0; s <= num8; s++)
                {
                    for (var t = 0; t <= num9; t++)
                    {
                        var num24 = 0;
                        var position = checked((int)binaryWriter2.BaseStream.Position);
                        try
                        {
                            enumerator = collections[s, t].GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                ((StaticCell)enumerator.Current).Write(binaryWriter2);
                                num24 += 7;
                            }
                        }
                        finally
                        {
                            if (enumerator is IDisposable)
                            {
                                ((IDisposable)enumerator).Dispose();
                            }
                        }

                        if (num24 == 0)
                        {
                            position = -1;
                        }

                        binaryWriter1.Write(position);
                        binaryWriter1.Write(num24);
                        binaryWriter1.Write(1);
                    }
                }

                binaryWriter2.Close();
                binaryWriter1.Close();
                fileStream2.Close();
                fileStream1.Close();
                iLogger.LogTimeStamp();
                iLogger.LogMessage("Done.");
            }
            else
            {
                iLogger.LogMessage("Unable to find Transition Data files in the following path: ");
                iLogger.LogMessage(baseDirectory);
            }
        }

        #endregion
    }
}