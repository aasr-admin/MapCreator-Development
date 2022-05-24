using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terrain;
using Transition;
using UltimaSDK_v432;

namespace MapCreator
{
    public partial class TerrainTypeEditor : Form
    {
        private ClsTerrainTable iTerrain;
        private RandomStatics iRandomStatic;

        private Art UOArt;
        private TileData UOStatic;
        private Point[,] StaticGrid;

        public TerrainTypeEditor()
        {
            MaximizeBox = false;
            MinimizeBox = false;

            TerrainTypeEditor tTE = this;
            base.Load += new EventHandler(tTE.TerrainTypeEditor_Load);

            this.iTerrain = new ClsTerrainTable();
            this.iRandomStatic = new RandomStatics();
            this.StaticGrid = new Point[13, 13];

            int num = 302;
            int num1 = 246;
            int num2 = 0;
            do
            {
                int num3 = 0;
                do
                {
                    Point[,] staticGrid = this.StaticGrid;
                    Point point = new Point(checked(num - checked(num3 * 22)), checked(num1 + checked(num3 * 22)));
                    staticGrid[num3, num2] = point;
                    num3++;
                }
                while (num3 <= 12);
                num = checked(num + 22);
                num1 = checked(num1 + 22);
                num2++;
            }
            while (num2 <= 12);

            InitializeComponent();
        }

        /// What Is Displayed When Plugin Starts?
        private void TerrainTypeEditor_Load(object sender, EventArgs e)
        {
            this.iTerrain.Load();
            this.iTerrain.Display(this.comboBox1);

            this.pictureBox7.Visible = true;
            this.pictureBox8.Visible = false;
        }

        /// Menu Selection: New Terrain Type
        private void newTerrainTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TerrainTypeEditor().Show();
            this.Hide();
        }

        /// Menu Selection: Load Terrain Type
        private void loadStaticTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = string.Format("{0}Data/Statics", AppDomain.CurrentDomain.BaseDirectory),
                Filter = "xml files (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                this.textBox3.Text = fileInfo.Name;
                this.iRandomStatic = new RandomStatics(fileInfo.Name);
                this.iRandomStatic.Display(this.listBox1);
                this.panel2.Refresh();
            }
        }

        /// Menu Selection: Save Terrain Type
        private void saveStaticTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = string.Format("{0}Data/Statics", AppDomain.CurrentDomain.BaseDirectory),
                Filter = "xml files (*.xml)|*.xml",
                FileName = this.textBox3.Text,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.iRandomStatic.Save(saveFileDialog.FileName);
            }
        }

        /// Menu Selection: Close/ Exit Plugin
        private void closePluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// Canvas Panel: Workbench 
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            IEnumerator enumerator = null;

            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Gray);
            ClsTerrain selectedItem = (ClsTerrain)this.comboBox1.SelectedItem;

            int num = 0;

            do
            {
                int num1 = 0;

                do
                {
                    int num2 = num1;
                    int num3 = num;

                    if (selectedItem != null)
                    {
                        e.Graphics.DrawImage(Art.GetLand(selectedItem.TileID), checked(this.StaticGrid[num2, num3].X - 22), checked(this.StaticGrid[num2, num3].Y - 22));
                    }

                    e.Graphics.DrawLine(pen, checked(this.StaticGrid[num2, num3].X - 22), this.StaticGrid[num2, num3].Y, this.StaticGrid[num2, num3].X, checked(this.StaticGrid[num2, num3].Y + 22));
                    e.Graphics.DrawLine(pen, this.StaticGrid[num2, num3].X, checked(this.StaticGrid[num2, num3].Y + 22), checked(this.StaticGrid[num2, num3].X + 22), this.StaticGrid[num2, num3].Y);
                    e.Graphics.DrawLine(pen, checked(this.StaticGrid[num2, num3].X + 22), this.StaticGrid[num2, num3].Y, this.StaticGrid[num2, num3].X, checked(this.StaticGrid[num2, num3].Y - 22));
                    e.Graphics.DrawLine(pen, this.StaticGrid[num2, num3].X, checked(this.StaticGrid[num2, num3].Y - 22), checked(this.StaticGrid[num2, num3].X - 22), this.StaticGrid[num2, num3].Y);
                    num1++;
                }
                while (num1 <= 12);
                num++;
            }
            while (num <= 12);

            pen = new Pen(Color.Red);
            int num4 = Convert.ToInt32(decimal.Add(new decimal(6L), this.numericUpDown1.Value)); // Y-Axis
            int num5 = Convert.ToInt32(decimal.Add(new decimal(6L), this.numericUpDown3.Value)); // X-Axis

            e.Graphics.DrawLine(pen, checked(this.StaticGrid[num4, num5].X - 22), this.StaticGrid[num4, num5].Y, this.StaticGrid[num4, num5].X, checked(this.StaticGrid[num4, num5].Y + 22));
            e.Graphics.DrawLine(pen, this.StaticGrid[num4, num5].X, checked(this.StaticGrid[num4, num5].Y + 22), checked(this.StaticGrid[num4, num5].X + 22), this.StaticGrid[num4, num5].Y);
            e.Graphics.DrawLine(pen, checked(this.StaticGrid[num4, num5].X + 22), this.StaticGrid[num4, num5].Y, this.StaticGrid[num4, num5].X, checked(this.StaticGrid[num4, num5].Y - 22));
            e.Graphics.DrawLine(pen, this.StaticGrid[num4, num5].X, checked(this.StaticGrid[num4, num5].Y - 22), checked(this.StaticGrid[num4, num5].X - 22), this.StaticGrid[num4, num5].Y);

            try
            {
                enumerator = this.listBox2.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    RandomStatic current = (RandomStatic)enumerator.Current;
                    int y = checked(6 + current.Y);
                    int x = checked(6 + current.X);
                    Bitmap @static = Art.GetStatic(current.TileID);
                    Point point = new Point(checked((int)Math.Round((double)this.StaticGrid[y, x].X - (double)@static.Width / 2)), checked(checked(this.StaticGrid[y, x].Y - @static.Height) + 22));

                    e.Graphics.DrawImage(@static, point);
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }

            graphics = null;
        }

        /// Canvas Panel: UpdatePanel Method
        private void UpdatePanel()
        {
            Panel panel3 = this.panel2;
            Point point = new Point(checked(this.hScrollBar1.Value * -1), checked(this.vScrollBar1.Value * -1));
            panel3.Location = point;
        }

        /// Canvas Panel: Vertical Scrollbar Update
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdatePanel();
        }

        /// Canvas Panel: Horizontal Scrollbar Update
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdatePanel();
        }

        /// Canvas Control: X-Axis Value Changed
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            this.panel2.Refresh();
        }

        /// Canvas Control: Y-Axis Value Changed
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.panel2.Refresh();
        }

        /// Canvas Control: Base Terrain Modified
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel2.Refresh();
        }

        /// Canvas Control: Frequency Value Changed
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.iRandomStatic.Freq = Convert.ToInt32(this.numericUpDown2.Value);
        }

        /// Static Item Selector
        private void button1_Click(object sender, EventArgs e)
        {
            (new StaticSelector()
            {
                Tag = this.vScrollBar2
            }).Show();
        }

        private void UpdateVScrollBar2()
        {
            if (Art.GetStatic(this.vScrollBar2.Value) != null && this.vScrollBar2.Value < TileData.ItemTable.Length)
            {
                this.textBox1.Text = this.vScrollBar2.Value.ToString(); // TileID Text
                this.pictureBox8.Image = Art.GetStatic(this.vScrollBar2.Value); // Static Item Selector Preview
                this.propertyGrid1.SelectedObject = TileData.ItemTable[this.vScrollBar2.Value]; // Properties Tab
                this.textBox5.Text = string.Format("{0} ({1})", TileData.ItemTable[this.vScrollBar2.Value].Name, this.vScrollBar2.Value); // Static Item Selector Description
            }
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateVScrollBar2();
        }

        private void vScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            UpdateVScrollBar2();
        }

        /// Static List: ListBox1 Selected Index Changed
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBox8.Image = null; // Static Item Selector Preview
            RandomStaticCollection selectedItem = (RandomStaticCollection)this.listBox1.SelectedItem;

            if (selectedItem != null)
            {
                this.textBox4.Text = selectedItem.Description;
                this.numericUpDown5.Value = new decimal(selectedItem.Freq);
                selectedItem.Display(this.listBox2); // Components
                this.panel2.Refresh();
            }
        }

        /// Static List: Frequency Value Changed
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            RandomStaticCollection selectedItem = (RandomStaticCollection)this.listBox1.SelectedItem;
            if (selectedItem != null)
            {
                selectedItem.Freq = Convert.ToInt32(this.numericUpDown5.Value);
            }
        }

        /// Static List: Toolbar  (Experimental - May Need To Revert To Normal ButtonClick Event)
        private void menuStrip2_ButtonClick(object sender, EventArgs e)
        {
            MenuStrip menuStrip2Button = sender as MenuStrip;

            if (menuStrip2Button == null)
            {
                return;
            }

            object tag = menuStrip2Button.Tag;

            if (ObjectType.ObjTst(tag, "Add", false) == 0)
            {
                if (StringType.StrCmp(this.textBox4.Text, string.Empty, false) == 0)
                {
                    return;
                }
                this.iRandomStatic.Add(new RandomStaticCollection(this.textBox4.Text, Convert.ToInt32(this.numericUpDown5.Value))); // Frequency
                this.iRandomStatic.Display(this.listBox1);
                this.panel2.Refresh();
            }

            else if (ObjectType.ObjTst(tag, "Delete", false) == 0)
            {
                this.iRandomStatic.Remove((RandomStaticCollection)this.listBox1.SelectedItem);
                this.iRandomStatic.Display(this.listBox1);
                this.panel2.Refresh();
            }

            else if (ObjectType.ObjTst(tag, "Refresh", false) == 0)
            {
                this.listBox1.Items.Clear();
                this.panel2.Refresh();
            }
        }

        /// Components: Selected Index Changed
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RandomStatic selectedItem = (RandomStatic)this.listBox2.SelectedItem;

            if (selectedItem != null)
            {
                RandomStatic randomStatic = selectedItem;
                this.vScrollBar2.Value = randomStatic.TileID;

                if (Art.GetStatic(randomStatic.TileID) != null)
                {
                    this.pictureBox8.Image = Art.GetStatic(randomStatic.TileID); // Static Item Selector Preview
                    this.propertyGrid1.SelectedObject = TileData.ItemTable[randomStatic.TileID];
                }

                this.textBox1.Text = StringType.FromInteger(randomStatic.TileID); // TileID Text
                this.numericUpDown3.Value = new decimal(randomStatic.X); // X-Axis
                this.numericUpDown1.Value = new decimal(randomStatic.Y); // Y-Axis
                this.numericUpDown4.Value = new decimal(randomStatic.Z); // Z-Axis
                this.textBox2.Text = StringType.FromInteger(randomStatic.Hue); // HueID

                randomStatic = null;
            }
        }

        /// Components: Toolbar  (Experimental - May Need To Revert To Normal ButtonClick Event)
        private void menuStrip3_ButtonClick(object sender, EventArgs e)
        {
            MenuStrip menuStrip3Button = sender as MenuStrip;

            if (menuStrip3Button == null)
            {
                return;
            }

            RandomStaticCollection selectedItem = (RandomStaticCollection)this.listBox1.SelectedItem;

            if (selectedItem != null)
            {
                object tag = menuStrip3Button.Tag;

                if (ObjectType.ObjTst(tag, "Add", false) == 0)
                {
                    selectedItem.Add(new RandomStatic(ShortType.FromString(this.textBox1.Text), Convert.ToInt16(this.numericUpDown3.Value), Convert.ToInt16(this.numericUpDown1.Value), Convert.ToInt16(this.numericUpDown4.Value), ShortType.FromString(this.textBox2.Text)));
                    selectedItem.Display(this.listBox2);
                    this.panel2.Refresh();
                }

                else if (ObjectType.ObjTst(tag, "Delete", false) == 0)
                {
                    selectedItem.Remove((RandomStatic)this.listBox2.SelectedItem);
                    selectedItem.Display(this.listBox2);
                    this.panel2.Refresh();
                }

                else if (ObjectType.ObjTst(tag, "Refresh", false) == 0)
                {
                    this.listBox2.Items.Clear();
                    this.panel2.Refresh();
                }
            }
        }

        /// Nav: Toolbar  (Experimental - May Need To Revert To Normal ButtonClick Event)
        private void NavToolbar_ButtonClick(object sender, EventArgs e)
        {
            ToolStripButton navButton = sender as ToolStripButton;

            if (navButton == null)
            {
                return;
            }

            short num = Convert.ToInt16(this.numericUpDown3.Value); // X-Axis
            short y = Convert.ToInt16(this.numericUpDown1.Value); // Y-Axis
            RandomStatic selectedItem = (RandomStatic)this.listBox2.SelectedItem;

            if (selectedItem != null)
            {
                num = selectedItem.X;
                y = selectedItem.Y;
            }

            object tag = navButton.Tag;

            if (ObjectType.ObjTst(tag, 1, false) == 0) // West
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                y = checked((short)(checked(y - 1)));
                num = checked((short)(checked(num - 1)));
            }

            else if (ObjectType.ObjTst(tag, 2, false) == 0) // NorthWest
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                y = checked((short)(checked(y - 1)));
            }

            else if (ObjectType.ObjTst(tag, 3, false) == 0) // North
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                y = checked((short)(checked(y - 1)));
                num = checked((short)(checked(num + 1)));
            }

            else if (ObjectType.ObjTst(tag, 4, false) == 0) // SouthWest
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                num = checked((short)(checked(num - 1)));
            }

            else if (ObjectType.ObjTst(tag, 5, false) != 0) // Compass
            {
                // Todo Hide pictureBox8 and Show pictureBox7
                this.pictureBox7.Visible = false;
                this.pictureBox8.Visible = true;
            }

            else if (ObjectType.ObjTst(tag, 6, false) == 0) // NorthEast
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                num = checked((short)(checked(num + 1)));
            }

            else if (ObjectType.ObjTst(tag, 7, false) == 0) // South
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                y = checked((short)(checked(y + 1)));
                num = checked((short)(checked(num - 1)));
            }

            else if (ObjectType.ObjTst(tag, 8, false) == 0) // SouthEast
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                y = checked((short)(checked(y + 1)));
            }

            else if (ObjectType.ObjTst(tag, 9, false) == 0) // East
            {
                this.pictureBox7.Visible = true;
                this.pictureBox8.Visible = false;

                y = checked((short)(checked(y + 1)));
                num = checked((short)(checked(num + 1)));
            }

            this.numericUpDown3.Value = new decimal(num); // X-Axis
            this.numericUpDown1.Value = new decimal(y); // Y-Axis

            if (selectedItem != null)
            {
                selectedItem.X = num;
                selectedItem.Y = y;
            }

            this.panel2.Refresh();
        }
    }
}