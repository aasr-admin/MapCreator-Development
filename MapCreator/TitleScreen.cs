using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace MapCreator
{
    public partial class titleScreen : Form
    {
        Timer splashTimer;

        public titleScreen()
        {
            InitializeComponent();
        }

        private void titleScreen_Shown(object sender, EventArgs e)
        {
            splashTimer = new Timer();
            splashTimer.Interval = 1000;

            splashTimer.Start();
            splashTimer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            splashTimer.Stop();

            mainMenu mM = new mainMenu();
            mM.Show();

            this.Hide();
        }

        private void titleScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
