using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace BeatDownloader
{
    public partial class AboutForm : Form
    {
        const string HOMEPAGE = "http://beat.redphx.com";

        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHomePage_Click(object sender, EventArgs e)
        {
            Process.Start(HOMEPAGE);
        }

        private void lnkKaraholics_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://karaholics.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(HOMEPAGE);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:redphoenix89@yahoo.com");
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            txtVersion.Text = GlobalVar.CurrentVersion.ToString();
        }
    }
}
