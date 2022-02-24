using BuildLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using LegacyMUL;
using System.Threading;

namespace MapCreator
{
    public partial class userPlugin : Form
    {
        MUL2UOPConverter mul2uop;            /// Plugin01

        private int m_Total, m_Success;  

        public userPlugin()
        {
            MaximizeBox = false;
            MinimizeBox = false;

            mul2uop = new MUL2UOPConverter(); /// Plugin01

            InitializeComponent();
        }

        private void userPlugin_Load(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.userPlugin_pictureBox05_bottomDivider.Show();

                Control c2UOP = this.userPlugin_panel02_mul2uop_workBench;  /// Plugin01: Hide On Load
                Thread.Sleep(25);
                c2UOP.Hide();
            }

            this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Text = Directory.GetCurrentDirectory();  /// Plugin01
        }

        #region Plugin01: Convert File Type: [.mul] To [.uop]

        private void userPlugin_pluginPanel01_button01_mul2uop_Click(object sender, EventArgs e)
        {
            /// These Statements Show Plugin01 On Button Press
            Control c2UOP = this.userPlugin_panel02_mul2uop_workBench;
            Thread.Sleep(25);
            c2UOP.Show();

            this.userPlugin_pictureBox05_bottomDivider.Hide();
        }

        private void userPlugin_panel02_mul2uop_workBench_groupBox_button01_locateProjectFolderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog()
            {
                SelectedPath = this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Text
            };

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert_CheckedChanged(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void userPlugin_panel02_mul2uop_workBench_groupBox_button02_convertFile_Click(object sender, EventArgs e)
        {
            if (userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Text == string.Empty || userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Text == null)
            {
                MessageBox.Show(" ERROR: You Must Specify The Location Path\n Of The [.mul] Files You Want Converted!");
                return;
            }

            //Pack("art.mul", "artidx.mul", "artLegacyMUL.uop", FileType.ArtLegacyMUL, 0);
            //Pack("gumpart.mul", "gumpidx.mul", "gumpartLegacyMUL.uop", FileType.GumpartLegacyMUL, 0);
            //Pack("sound.mul", "soundidx.mul", "soundLegacyMUL.uop", FileType.SoundLegacyMUL, 0);

            if (userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Checked)
            {
                for (int i = 0; i <= 100; ++i) /// Total of 100 Facets At A Time Can Be Converted
                {
                    string map = String.Format("map{0}", i);

                    Pack(map + ".mul", null, map + "LegacyMUL.uop", FileType.MapLegacyMUL, i);
                    Pack(map + "x.mul", null, map + "xLegacyMUL.uop", FileType.MapLegacyMUL, i);
                }

                statustext.Text = string.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
            }
            else if (userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Checked)
            {
                for (int i = 0; i <= 10; ++i)  /// Total of 10 Facet At A Time Can Be Converted
                {
                    string map = String.Format("map{0}", i);

                    Pack(map + ".mul", null, map + "LegacyMUL.uop", FileType.MapLegacyMUL, i);
                    Pack(map + "x.mul", null, map + "xLegacyMUL.uop", FileType.MapLegacyMUL, i);
                }

                statustext.Text = string.Format("Done ({0}/{1} files extracted)", m_Success, m_Total);
            }
            else if (userPlugin_panel02_mul2uop_workBench_groupBox_radioButton02_officialMapsToConvert.Checked == false || userPlugin_panel02_mul2uop_workBench_groupBox_radioButton01_officialMapsToConvert.Checked == false)
            {
                MessageBox.Show("   ERROR: Please Select A Facet Allowance Type Before This Program Can Proceed!\n");
                return;
            }
        }

        private string FixPath(string file)
        {
            return (file == null) ? null : Path.Combine(userPlugin_panel02_mul2uop_workBench_groupBox_textBox01_projectPath.Text, file);
        }

        private void Pack(string inFile, string inIdx, string outFile, FileType type, int typeIndex)
        {
            try
            {
                statustext.Text = inFile;
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

                mul2uop.ToUOP(inFile, inIdx, outFile, type, typeIndex);
                ++m_Success;

            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while performing the action");
            }
        }

        #endregion

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form mainMenu = new mainMenu();
            mainMenu.Show();
        }
    }
}
