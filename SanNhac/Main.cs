using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace BeatDownloader
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        #region Events

        private void Main_Load(object sender, EventArgs e)
        {
            // set selected index
            cboSite.SelectedIndex = 0;
            
            // set tab icons
            tabContainer.ImageList = IconList.Icons;

            // add download tab
            AddDownloadTab();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            // focus search textbox
            txtSearchQuery.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // start searching
            DoSearch();
        }

        private void txtSearchQuery_KeyDown(object sender, KeyEventArgs e)
        {
            // press enter
            if (e.KeyCode == Keys.Return)
            {
                // start searching
                DoSearch();
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (tabContainer.TabPages.Count == 0)
            {
                return;
            }

            for (int i = 0; i < tabContainer.TabPages.Count; i++)
            {
                tabContainer.TabPages[i].Controls[0].Size = tabContainer.TabPages[tabContainer.SelectedIndex].Size;
            }
        }

        #endregion

        #region Custom Codes

        // start searching
        private void DoSearch()
        {
            if (txtSearchQuery.Text.Trim().Length == 0)
            {
                MessageBox.Show(null, "Please enter search query", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearchQuery.Focus();
                return;
            }

            // add search tab
            AddSearchTab(txtSearchQuery.Text.Trim(), cboSite.SelectedItem.ToString());
        }


        // add download tab
        private void AddDownloadTab()
        {
            DownloadTab dlTab = new DownloadTab();
            TabPage tab = new TabPage("Download");

            tab.Controls.Add(dlTab);
            // add tab
            tabContainer.Controls.Add(tab);
            // resize tab
            tabContainer.TabPages[tabContainer.SelectedIndex].Controls[0].Size = tabContainer.TabPages[0].Size;
        }


        // add search tab
        private void AddSearchTab(string query, string site)
        {
            // convert query to lower string
            query = query.ToLower();

            int foundIndex = -1;
            int tabIconIndex = IconList.GetIconIndex(site);

            // find tab
            for (int i = 1; i < tabContainer.TabPages.Count; i++)
            {
                if (tabContainer.TabPages[i].Tag.ToString() == query && tabContainer.TabPages[i].ImageIndex == tabIconIndex)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex == -1)
            {
                SearchTab st = new SearchTab(query, site);
                TabPage tab = new TabPage(query);

                tab.ImageIndex = tabIconIndex;

                tab.Controls.Add(st);
                // add tab
                tabContainer.Controls.Add(tab);

                // focus tab
                tabContainer.SelectTab(tab);

                // resize
                tabContainer.TabPages[tabContainer.SelectedIndex].Controls[0].Size = tabContainer.TabPages[tabContainer.SelectedIndex].Size;
                // set header text
                tabContainer.TabPages[tabContainer.SelectedIndex].Tag = query;
            }
            else
            {
                // focus tab
                tabContainer.SelectedIndex = foundIndex;
            }
        }

        #endregion
    }
}
