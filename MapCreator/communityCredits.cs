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

namespace MapCreator
{
    public partial class communityCredits : Form
    {
        public communityCredits()
        {
            InitializeComponent();
        }

        private void communityCredits_button_close_MouseEnter(object sender, EventArgs e)
        {
            communityCredits_button_close.ForeColor = System.Drawing.Color.LimeGreen;
        }

        private void communityCredits_button_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void communityCredits_button_close_MouseLeave(object sender, EventArgs e)
        {
            communityCredits_button_close.ForeColor = System.Drawing.Color.SlateGray;
        }

        private void communityCredits_linkLabel_uoAvocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo uoAvocation = new ProcessStartInfo
            {
                FileName = "http://www.uoavocation.net",
                UseShellExecute = true
            };

            Process.Start(uoAvocation);
        }
    }
}
