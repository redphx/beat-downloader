using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace BeatDownloader
{
    public partial class SearchTab : UserControl
    {
        #region Fields

        private string searchQuery;
        private string searchSite;

        private DataTable tableResults;
        // search thread
        private Thread thrSearch;
        
        private const string HTML_TAG_PATTERN = "<.*?>";

        #endregion

        // constructor
        public SearchTab(string query, string site)
        {
            this.searchQuery = query;
            this.searchSite = site;

            InitializeComponent();

        }

        #region Events

        private void SearchTab_Load(object sender, EventArgs e)
        {
            // resize tabpage
            this.Size = ((TabPage)Parent).Size;

            tableResults = new DataTable();

            // copy columns in dgvResult to tableResult
            foreach (DataGridViewColumn dgvCol in dgvResult.Columns)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = dgvCol.Name;

                tableResults.Columns.Add(col);
                
            }
            

            // start search thread
            StartSearching();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            TabPage pageParent = (TabPage)Parent;
            TabControl controlParent = (TabControl)pageParent.Parent;

            controlParent.SelectedIndex = controlParent.SelectedIndex - 1;

            controlParent.Controls.Remove(pageParent);

            StopSearching();
        }

        private void txtRefine_TextChanged(object sender, EventArgs e)
        {
            if (txtRefine.ForeColor == Color.Black)
            {
                RefineResults(txtRefine.Text);
            }
        }

        private void txtRefine_Enter(object sender, EventArgs e)
        {
            if (txtRefine.ForeColor == Color.Gray)
            {
                txtRefine.Text = "";
                txtRefine.ForeColor = Color.Black;
            }
        }

        private void txtRefine_Leave(object sender, EventArgs e)
        {
            if (txtRefine.Text.Trim().Length == 0)
            {
                txtRefine.ForeColor = Color.Gray;
                txtRefine.Text = "refine results ...";
            }
        }

        private void dgvResult_SelectionChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = (dgvResult.SelectedRows.Count != 0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddToDownloadList();
        }

        private void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddToDownloadList();
        }

        #endregion

        #region Custom Codes

        private static string StripHTML(string inputString)
        {
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        // start search thread
        private void StartSearching()
        {
            imgLoading.Visible = true;

            thrSearch = new Thread(SearchAndShow);
            thrSearch.IsBackground = true;
            thrSearch.Start();

        }

        // stop search thread
        delegate void StopSearchingCallBack();
        private void StopSearching()
        {
            if (this.dgvResult.InvokeRequired)
            {
                this.Invoke(new StopSearchingCallBack(StopSearching));
                return;
            }
            
            // hide loading image
            imgLoading.Visible = false;
            // focus refine results
            txtRefine.Focus();


            //TabPage tabPage = (TabPage)Parent;
            
            try
            {
                thrSearch.Abort();
            }
            catch
            {
                throw;
            }
        }

        
        // search and show
        private void SearchAndShow()
        {
            string query = HttpUtility.UrlPathEncode(searchQuery);

            string searchURL;
            XmlDocument xmlDoc;

            int total = 0;
            int page = 1;
            int totalPages = 1;


            if (searchSite == "sannhac.com")
            {
                searchURL = "http://sannhac.com/ajax.php?cmd=search&x_strSearch=" + query + "&typep=2&x_order=1&ord=ASC&page=";

                try
                {
                    while (true)
                    {
                        if (page > totalPages)
                        {
                            break;
                        }

                        xmlDoc = GlobalCode.GetXML(searchURL + page);

                        if (page == 1)
                        {
                            XmlNodeList xmlConfig = xmlDoc.GetElementsByTagName("config");
                            total = Convert.ToInt32(xmlConfig[0].Attributes["total"].Value);

                            if (total == 0)
                            {
                                AddSearchResult(null);
                                break;
                            }

                            totalPages = (int)Math.Ceiling((decimal)total / 25);
                        }

                        XmlNodeList xnlRec = xmlDoc.GetElementsByTagName("rec");
                        foreach (XmlNode node in xnlRec)
                        {
                            XmlAttributeCollection attr = node.Attributes;

                            BeatInfo beatInfo = new BeatInfo();
                            beatInfo.Title = attr["song_name"].Value.Replace("&amp;", "&");
                            beatInfo.Artist = attr["singer_name"].Value.Replace("&amp;", "&");
                            beatInfo.Genre = SearchTab.StripHTML(attr["genre"].Value);
                            beatInfo.Link = attr["linkSong"].Value;


                            AddSearchResult(beatInfo);
                        }


                        ++page;
                    }
                }

                catch (Exception ex)
                {
                    StopSearching();
                }
            }
            else
            {
                query = query.Replace("%20", "+");
                string webContent;

                MatchCollection patternMatches;
                try
                {
                    while (true)
                    {
                        searchURL = "http://star.zing.vn/star/search/search." + page + ".html?q=" + query;

                        webContent = GlobalCode.GetContent(searchURL);

                        if (page == 1)
                        {
                            // tìm tổng số trang
                            patternMatches = Regex.Matches(webContent, @"href='/star/search/[^\.]+\.([0-9]+)\.html\?q=[^']+' class=''><img src='[^']+icon_lastpage\.gif'", RegexOptions.IgnoreCase);
                            if (patternMatches.Count == 0)
                            {
                                totalPages = 1;
                            }
                            else
                            {
                                totalPages = Convert.ToInt32(patternMatches[0].Groups[1].Value);
                            }
                        }

                        patternMatches = Regex.Matches(webContent, @"<tr class=""[^""]*"">[^<]+(<td ><a onMouseOut=""hideddrivetip\(\)""(?:[^<]+|<(?!/tr>))*)</tr>", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                        // ko có kết quả
                        if (patternMatches.Count == 0)
                        {
                            AddSearchResult(null);
                            break;
                        }
                        else
                        {
                            Match matchTemp;
                            string[] data = new string[5];
                            foreach (Match matchRow in patternMatches)
                            {
                                data = new string[6];

                                matchTemp = Regex.Match(matchRow.Groups[1].Value, @"ddrivetip\('<b>([^<]+)</b>[\w\W]*\.([0-9]+)\.html[\w\W]*<a title=""([^""]+)""[\w\W]*>([^<]+)</a></td>", RegexOptions.Multiline | RegexOptions.IgnoreCase);


                                BeatInfo beatInfo = new BeatInfo();
                                beatInfo.Title = matchTemp.Groups[1].Value.Replace(@"\&#039;", "'").Replace("&amp;", "&");
                                beatInfo.Artist = matchTemp.Groups[3].Value.Replace(@"\&#039;", "'").Replace("&amp;", "&");
                                beatInfo.Genre = matchTemp.Groups[4].Value.Replace("&amp;", "&");
                                beatInfo.Link = matchTemp.Groups[2].Value;

                                AddSearchResult(beatInfo);
                            }
                        }
                        ++page;

                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    StopSearching();
                }
            }

            StopSearching();
        }

        delegate void AddSearchResultCallBack(BeatInfo info);
        private void AddSearchResult(BeatInfo info)
        {
            if (this.dgvResult.InvokeRequired)
            {
                AddSearchResultCallBack cb = new AddSearchResultCallBack(AddSearchResult);
                this.Invoke(cb, new object[] {info});
                return;
            }

            if (info != null)
            {
                string[] data = new string[] {
                    info.Title,
                    info.Artist,
                    info.Genre,
                    info.Link,
                };

                tableResults.Rows.Add(data);

                dgvResult.Rows.Add(data);
                // sorting
                dgvResult.Sort(dgvResult.Columns[0], ListSortDirection.Ascending);
            }

            // update results on tab header
            ((TabPage)Parent).Text = searchQuery + " (" + dgvResult.Rows.Count  + ")";

            //txtResult.Text = dgvResult.Rows.Count + " kết quả";
        }

        // http://www.csharp-examples.net/dataview-rowfilter/
        public static string EscapeLikeValue(string valueWithoutWildcards)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < valueWithoutWildcards.Length; i++)
            {
                char c = valueWithoutWildcards[i];
                if (c == '*' || c == '%' || c == '[' || c == ']')
                {
                    sb.Append("[").Append(c).Append("]");
                }
                else if (c == '\'')
                {
                    sb.Append("''");
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }


        private void RefineResults(string text)
        {
            text = EscapeLikeValue(text);

            DataView dv = new DataView(tableResults);

            if (text.Length > 0)
            {
                try
                {
                    dv.RowFilter = "colTitle LIKE '%" + text + "%' OR colArtist LIKE '%" + text + "%'";
                }
                catch
                {
                }
            }

            dv.Sort = "colTitle ASC";


            DataTable dt = dv.ToTable();

            dgvResult.Rows.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvResult.Rows.Add(dt.Rows[i].ItemArray);
            }
        }

        

        private void AddToDownloadList()
        {
            if (dgvResult.SelectedRows.Count == 0)
            {
                return;
            }

            TabControl tabContainer = (TabControl)Parent.Parent;
            DownloadTab dlTab = (DownloadTab)tabContainer.TabPages[0].Controls[0];

            for (int i = 0; i < dgvResult.SelectedRows.Count; i++)
            {
                DataGridViewCellCollection cellCol = dgvResult.SelectedRows[i].Cells;

                BeatInfo beatInfo = new BeatInfo();
                beatInfo.Site = searchSite;
                beatInfo.Title = cellCol[0].Value.ToString();
                beatInfo.Artist = cellCol[1].Value.ToString();
                beatInfo.Link = cellCol[3].Value.ToString();

                dlTab.AddRow(beatInfo);

            }
            tabContainer.SelectTab(0);
        }

        #endregion
    }
}
