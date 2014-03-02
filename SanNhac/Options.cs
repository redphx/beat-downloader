using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.IO;
using System.Security.Principal;

namespace BeatDownloader
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            txtSaveFolder.Text = GlobalVar.SaveFolder;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fbdSaveFolder.SelectedPath = txtSaveFolder.Text;

            if (fbdSaveFolder.ShowDialog() == DialogResult.OK)
            {
                
                txtSaveFolder.Text = fbdSaveFolder.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            GlobalVar.SaveFolder = txtSaveFolder.Text;
            this.Close();
        }

        
    }
}
