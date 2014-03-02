using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
//using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace BeatDownloader
{
    public partial class MainForm : Form
    {
        Version curVersion = Assembly.GetExecutingAssembly().GetName().Version;


        private string searchStatus = "done";
        private string downloadStatus = "done";

        

        private string searchQuery;

        // tên file
        private string fileName = "";
        // đường dẫn đầy đủ
        private string fullPath = "";

        //Thread thrUpdate;
        private string updateURL = "";


        private string beatTitle;
        private string beatSinger;
        private string beatSite;
        private string beatURL;
        private string beatPage;
        private string beatType;

        // The thread inside which the download happens
        //private Thread thrDownload;
        // The stream of data retrieved from the web server
        private Stream strResponse;
        // The stream of data that we write to the harddrive
        private Stream strLocal;

        private long downloadedSize;

        // The request to the web server for file information
        private HttpWebRequest webRequest;
        // The response from the web server containing information about the file
        private HttpWebResponse webResponse;
        // The progress of the download in percentage
        private static int PercentProgress;
        // The delegate which we will call from the thread to update the form
        //private delegate void UpdateProgessCallback(decimal BytesRead, decimal TotalBytes);


        const string HTML_TAG_PATTERN = "<.*?>";
        // xóa các thẻ HTML
        static string StripHTML(string inputString)
        {
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }

        // download trang beat
        private string DownloadBeatPage()
        {
            string strResult = "";

            WebRequest objRequest = WebRequest.Create(beatPage);

            WebResponse objResponse = objRequest.GetResponse();
            Stream responseStream = new GZipStream(objResponse.GetResponseStream(), CompressionMode.Decompress);

            using (StreamReader sr = new StreamReader(responseStream, Encoding.Default))
            {
                strResult = sr.ReadToEnd();
                // Close and clean up the StreamReader
                sr.Close();
            }

            return strResult;
        }


        // bắt đầu download
        private void StartDownload(DoWorkEventArgs evt)
        {
            ChangeDownloadStatus("download");

            DataGridViewRow currentRow = dgvResult.CurrentRow;

            // tên bài hát
            beatTitle = currentRow.Cells[0].Value.ToString();
            // ca sỹ
            beatSinger = currentRow.Cells[1].Value.ToString();
            // tên file
            fileName = beatSinger + " - " + beatTitle;
            // đường dẫn lưu file
            fullPath = txtSaveFolder.Text.TrimEnd('\\') + "\\" + fileName;

            beatSite = currentRow.Cells[3].Value.ToString();
            beatPage = currentRow.Cells[4].Value.ToString();

            txtDownloadStatus.Text = stlInfo.Text = "Đang tìm link beat ...";

            // tìm link beat
            string webContent;
            Match matchURL;
            beatURL = currentRow.Cells[5].Value.ToString();
            if (beatURL == "" || beatURL == null)
            {
                if (beatSite == "s")
                {
                    webContent = GetContent(beatPage, true);

                    matchURL = Regex.Match(webContent, @"<div class=""ttct\-download""><a href=""([^""]+)""", RegexOptions.IgnoreCase);
                    if (matchURL.Groups.Count != 0)
                    {
                        beatURL = matchURL.Groups[1].Value;

                    }
                }
                else
                {
                    beatPage = "http://star.zing.vn/includes/fnGetSongInfo.php?id=" + beatPage;
                    webContent = GetContent(beatPage);

                    matchURL = Regex.Match(webContent, @"<karaokelink>([^<]+)</karaokelink>", RegexOptions.IgnoreCase);
                    if (matchURL.Groups.Count != 0)
                    {
                        beatURL = matchURL.Groups[1].Value;
                    }
                }
            }
            if (beatURL == "" || beatURL == null)
            {
                MessageBox.Show("Không tìm thấy link beat");
                txtDownloadStatus.Text = "Không tìm thấy link beat";
                ChangeDownloadStatus("done");
                return;
            }

            currentRow.Cells[5].Value = beatURL;

            if (beatURL.EndsWith(".mp3", StringComparison.CurrentCultureIgnoreCase) || beatURL.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase))
            {
                beatType = ".mp3";
            }
            else {
                beatType = ".swf";
            }


            if (File.Exists(fullPath + beatType))
            {
                if (MessageBox.Show("Đã có file " + fileName + beatType + " trong thư mục lưu beat. Bạn có muốn download lại không ?", "Download", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    ChangeDownloadStatus("done");
                    return;
                }
            }


            stlInfo.Text = "Đang download ...";

            txtDownloadStatus.Text = @"Đang download """+ fileName +@""" ...";
            
            lbDownloadSize.Visible = true;
            lbDownloadSize.Text = "";
            lbDownloadPercent.Visible = true;
            lbDownloadPercent.Text = "";
            
            string url = beatURL;

            using (WebClient wcDownload = new WebClient())
            {
                Int64 fileSize = 0;
                try
                {
                    // Create a request to the file we are downloading
                    webRequest = (HttpWebRequest)WebRequest.Create(url);
                    // Set default authentication for retrieving the file
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    // Ask the server for the file size and store it
                    fileSize = webResponse.ContentLength;

                    // Open the URL for download
                    strResponse = webResponse.GetResponseStream();

                    // Create a new file stream where we will be saving the data (local drive)
                    strLocal = new FileStream(fullPath + beatType, FileMode.Create, FileAccess.Write, FileShare.None);

                    // It will store the current number of bytes we retrieved from the server
                    int bytesSize = 0;
                    // A buffer for storing and writing the data retrieved from the server
                    byte[] downBuffer = new byte[2048];

                    // Loop through the buffer until the buffer is empty
                    while ((downloadStatus == "download") && (bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        if (bgwDownload.CancellationPending == true)
                        {
                            evt.Cancel = true;
                            break;
                        }

                        // Write the data from the buffer to the local hard drive
                        strLocal.Write(downBuffer, 0, bytesSize);

                        downloadedSize = strLocal.Length;

                        // Invoke the method that updates the form's label and progress bar

                        bgwDownload.ReportProgress(0, new decimal[]{downloadedSize, fileSize});

                        //this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { (decimal)strLocal.Length, (decimal)fileSize });
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                finally
                {
                    // When the above code has ended, close the streams

                    bool hasError = (downloadedSize != fileSize);

                    strResponse.Close();
                    strLocal.Close();

                    if (downloadStatus == "download")
                    {
                        if (hasError)
                        {
                            MessageBox.Show("Download lỗi. Xin hãy thử lại");
                            txtDownloadStatus.Text = @"Download """ + fileName + @""" lỗi. Xin hãy thử lại";
                            stlInfo.Text = "Download lỗi";
                        }
                        else
                        {
                            if (beatType == ".swf")
                            {
                                stlInfo.Text = "Đang chuyển sang mp3 ...";
                                ExtractMp3();
                                
                            }
                            UpdateMp3();

                            txtDownloadStatus.Text = @"Download beat """+ fileName +@""" thành công";
                            stlInfo.Text = "Hoàn tất";
                        }
                        ChangeDownloadStatus("done");
                        //thrDownload.Abort();
                    }
                    
                }
            }
        }


        private void StopDownload()
        {
            ChangeDownloadStatus("stop");
        }

        private void UpdateProgress(decimal BytesRead, decimal TotalBytes)
        {
            if (downloadStatus != "download")
            {
                return;
            }

            // Calculate the download progress in percentages
            PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);

            lbDownloadSize.Text = Math.Round(BytesRead / 1024 / 1024, 2) + " MB / " + Math.Round(TotalBytes / 1024 / 1024, 2) + " MB";
            lbDownloadPercent.Text = PercentProgress + "%";
            
            // Make progress on the progress bar
            prgDownload.Value = PercentProgress;
            // Display the current progress on the form

            
            //stlInfo.Text = "Đang download ...";
        }

        private void ExtractMp3()
        {
            SWF swfParser = new SWF(fullPath + ".swf");
            swfParser.ReadHeader();
            byte[] tmp;

            //byte[] mp3 = "";
            TagInfo info;

            FileStream myFStream = new FileStream(fullPath + ".mp3", FileMode.Create, FileAccess.Write);

            do
            {
                info = swfParser.ReadTag();
                if (info.Size > 0)
                {
                    tmp = swfParser.ReadBytes((int)info.Size);
                    if (info.Id == 19)
                    {
                        myFStream.Write(tmp, 4, tmp.Length - 4);
                        //Console.WriteLine(BitConverter.ToString(tmp));
                    }
                }
            }
            while (info.Id > 0);

            //TextWriter tw = new StreamWriter(@"C:\test.mp3");
            //tw.Write(mp3);
            myFStream.Close();

        }

        private void UpdateMp3()
        {
            TagLib.File mp3File = TagLib.File.Create(fullPath + ".mp3");
            TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag) mp3File.GetTag(TagLib.TagTypes.Id3v2);
            

            tag.Title = beatTitle;
            tag.Performers = new string[] {beatSinger};
            //tag.Album = "SanNhac.com";
            tag.Comment = "Beat Downloader - redphx\nhttp://www.karaholics.com";

            mp3File.Save();

        }

        // kiểm tra phiên bản mới
        private void CheckForUpdate()
        {
            Version newVersion = null;
            string url = "";

            try
            {
                string xmlURL = "http://beat.redphx.com/version.xml?" + DateTime.Now.ToFileTime().ToString();

                XmlTextReader reader = new XmlTextReader(xmlURL);
                reader.MoveToContent();

                string elementName = "";

                if ((reader.NodeType == XmlNodeType.Element) &&
                    (reader.Name == "beat"))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                            elementName = reader.Name;
                        else
                        {
                            if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                            {
                                switch (elementName)
                                {
                                    case "version":
                                        newVersion = new Version(reader.Value);

                                        break;
                                    case "url":
                                        url = reader.Value;
                                        break;
                                }
                            }
                        }
                    }
                }

                reader.Close();

                // compare the versions
                if (curVersion.CompareTo(newVersion) < 0)
                {
                    bgwCheckForUpdate.ReportProgress(0, newVersion.ToString());
                    updateURL = url;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            
        }

        //private delegate void ShowUpdateLabelCallBack(string ver);
        private void ShowUpdateLabel(string ver)
        {
            /*
            if (this.InvokeRequired)
            {
                this.Invoke(new ShowUpdateLabelCallBack(ShowUpdateLabel), ver);
                return;
            }
            */
            lnkUpdate.Text += " " + ver;
            lnkUpdate.Visible = true;
        }

        // ---------------------------------------------
        // ---------------------------------------------
        // ---------------------------------------------
        // ---------------------------------------------

        public MainForm()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchQuery = txtSearchQuery.Text.Trim();

            if (searchQuery.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập từ khóa tìm kiếm");
                txtSearchQuery.Text = "";
                txtSearchQuery.Focus();
                return;
            }

            string site = (rdoSanNhac.Checked) ? "sannhac" : "zing";

            bgwSearch.RunWorkerAsync(site);
        }

        private void ChangeSearchStatus(string stt)
        {
            
            searchStatus = stt;
            if (searchStatus == "search")
            {
                btnSearch.Enabled = false;
                btnStopSearch.Enabled = true;
            }
            else {
                Thread.Sleep(1000);
                txtResult.Text = dgvResult.Rows.Count.ToString() + " kết quả";
                stlInfo.Text = "Tìm được " + dgvResult.Rows.Count.ToString() + " kết quả";

                btnSearch.Enabled = true;
                btnStopSearch.Enabled = false;

                
                //bgwSearch.CancelAsync();
                
            }
        }

        //private delegate void ChangeDownloadStatusCallBack(string stt);
        private void ChangeDownloadStatus(string stt)
        {
            /*
            if (this.InvokeRequired)
            {
                this.Invoke(new ChangeDownloadStatusCallBack(ChangeDownloadStatus), stt);
                return;
            }
            */

            downloadStatus = stt;
            if (downloadStatus == "download")
            {
                btnDownload.Enabled = false;
                btnStopDownload.Enabled = true;
            }
            else
            {
                stlInfo.Text = "Hoàn tất";
                if (downloadStatus == "stop")
                {
                    bgwDownload.CancelAsync();

                    lbDownloadPercent.Text = "";
                    lbDownloadSize.Text = "";
                    prgDownload.Value = 0;

                    if (strResponse != null)
                    {
                        strResponse.Close();
                    }
                    if (strLocal != null)
                    {
                        strLocal.Close();
                    }

                    try
                    {
                        if (beatType == ".swf")
                        {
                            File.Delete(fullPath + ".swf");
                        }

                        File.Delete(fullPath + ".mp3");
                    }
                    catch
                    {

                    }
                }

                btnDownload.Enabled = (dgvResult.SelectedRows.Count > 0);
                btnStopDownload.Enabled = false;

            }
        }


        private void SearchAndShow(object obj, DoWorkEventArgs evt)
        {
            string site = (string)obj;

            ChangeSearchStatus("search");
            
            string query = HttpUtility.UrlPathEncode(searchQuery);

            string searchURL;
            XmlDocument xmlDoc;
            
            int total = 0;
            int page = 1;
            int pages = 1;

            dgvResult.Rows.Clear();

            if (site == "sannhac")
            {
                searchURL = "http://sannhac.com/ajax.php?cmd=search&x_strSearch=" + query + "&typep=2&x_order=1&ord=ASC&page=";

                try
                {

                    while (true)
                    {
                        if (bgwSearch.CancellationPending)
                        {
                            evt.Cancel = true;
                            break;
                        }

                        if (searchStatus != "search")
                        {
                            break;
                        }

                        if (page > pages)
                        {
                            break;
                        }

                        xmlDoc = GetXML(searchURL + page);

                        if (bgwSearch.CancellationPending)
                        {
                            evt.Cancel = true;
                            break;
                        }

                        if (page == 1)
                        {
                            XmlNodeList xnlConfig = xmlDoc.GetElementsByTagName("config");
                            total = Convert.ToInt32(xnlConfig[0].Attributes["total"].Value);

                            if (total == 0)
                            {
                                break;
                            }

                            pages = (int)Math.Ceiling((decimal)total / 25);
                        }

                        XmlNodeList xnlRec = xmlDoc.GetElementsByTagName("rec");
                        foreach (XmlNode node in xnlRec)
                        {
                            XmlAttributeCollection attr = node.Attributes;
                            string[] data = new string[] {
                                attr["song_name"].Value.Replace("&amp;","&"),
                                attr["singer_name"].Value.Replace("&amp;","&"),
                                MainForm.StripHTML(attr["genre"].Value),
                                "s",
                                attr["linkSong"].Value,
                                ""
                            };

                            bgwSearch.ReportProgress(0, data);
                            //AddSanNhacResult(node);
                        }


                        ++page;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            // zing
            else
            {
                query = query.Replace("%20", "+");
                string webContent;

                MatchCollection patternMatches;
                try
                {
                    while (true)
                    {
                        if (bgwSearch.CancellationPending)
                        {
                            evt.Cancel = true;
                            break;
                        }

                        if (searchStatus != "search")
                        {
                            break;
                        }

                        searchURL = "http://star.zing.vn/star/search/search." + page + ".html?q=" + query;

                        webContent = GetContent(searchURL);


                        if (bgwSearch.CancellationPending)
                        {
                            break;
                        }

                        if (page == 1)
                        {
                            // tìm tổng số trang
                            patternMatches = Regex.Matches(webContent, @"href='/star/search/[^\.]+\.([0-9]+)\.html\?q=[^']+' class=''><img src='[^']+icon_lastpage\.gif'", RegexOptions.IgnoreCase);
                            if (patternMatches.Count == 0)
                            {
                                pages = 1;
                            }
                            else
                            {
                                pages = Convert.ToInt32(patternMatches[0].Groups[1].Value);
                            }
                        }

                        patternMatches = Regex.Matches(webContent, @"<tr class=""[^""]*"">[^<]+(<td ><a onMouseOut=""hideddrivetip\(\)""(?:[^<]+|<(?!/tr>))*)</tr>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                        
                        // ko có kết quả
                        if (patternMatches.Count == 0)
                        {
                            evt.Cancel = true;
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
                                
                                // tên bài hát
                                data[0] = matchTemp.Groups[1].Value.Replace(@"\&#039;", "'").Replace("&amp;", "&");
                                // ca sỹ
                                data[1] = matchTemp.Groups[3].Value.Replace(@"\&#039;", "'").Replace("&amp;", "&");
                                // thể loại
                                data[2] = matchTemp.Groups[4].Value.Replace("&amp;", "&");
                                // site
                                data[3] = "z";
                                // id
                                data[4] = matchTemp.Groups[2].Value;
                                // beat
                                data[5] = "";

                                bgwSearch.ReportProgress(0, data);
                            }
                        }
                        ++page;
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            evt.Result = true;
            //ChangeSearchStatus("done");
        }

        private void AddSearchResult(string[] data)
        {
            dgvResult.Rows.Add(data);

            txtResult.Text = dgvResult.Rows.Count + " kết quả";
        }

        private XmlDocument GetXML(string searchURL)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(searchURL);
            }
            catch
            {
            }
            return xmlDoc;
        }

        private string GetContent(string url)
        {
            return GetContent(url, false);
        }

        private string GetContent(string url, bool unGzip)
        {
            string result = "";
            
            WebRequest http;
            WebResponse response;
            StreamReader stream;
            try
            {
                http = HttpWebRequest.Create(url);
                response = (HttpWebResponse)http.GetResponse();
                
                if (!unGzip)
                {
                    stream = new StreamReader(response.GetResponseStream());
                }
                else
                {
                    stream = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.Default);
                }

                result = stream.ReadToEnd();

                response.Close();
                stream.Close();
            }
            catch
            {
            }

            return result;
        }
        
        private void btnStopSearching_Click(object sender, EventArgs e)
        {
            btnStopSearch.Enabled = false;
            bgwSearch.CancelAsync();
            //ChangeSearchStatus("stop");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text += " " + curVersion.ToString();

            dgvResult.Sort(dgvResult.Columns[0], ListSortDirection.Ascending);

            bgwCheckForUpdate.RunWorkerAsync();
            //thrUpdate = new Thread(CheckForUpdate);
            //thrUpdate.Start();
        }

        private void btnSaveFolder_Click(object sender, EventArgs e)
        {
            if (fbdSaveFolder.ShowDialog() == DialogResult.OK)
            {
                txtSaveFolder.Text = fbdSaveFolder.SelectedPath;
            }
        }

        private void dgvResult_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count > 0)
                {
                    if (downloadStatus != "download")
                    {
                        btnDownload.Enabled = true;
                    }
                }
                else
                {
                    btnDownload.Enabled = false;
                }
            }
            catch
            {
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            bgwDownload.RunWorkerAsync();
            /*
            thrDownload = new Thread(StartDownload);
            // Start the thread, and thus call Download()
            thrDownload.Start();
             */
        }

        private void btnStopDownload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn dừng download không ?", "Dừng download", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                prgDownload.Value = 0;
                txtDownloadStatus.Text = @"Đã dừng download """+ fileName +@"""";
                stlInfo.Text = "Đã dừng download";

                StopDownload();
            }
        }

        private void btnExplorer_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtSaveFolder.Text);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (downloadStatus == "download")
                {
                    if (MessageBox.Show("Chương trình đang download. Bạn có muốn thoát không ?", "Thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        StopDownload();
                        Application.Exit();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.StartPosition = FormStartPosition.CenterParent;
            frmAbout.ShowDialog(this);
        }

        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(updateURL);
        }

        private void bgwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            stlInfo.Text = "Đang tìm ...";

            SearchAndShow(e.Argument, e);
        }

        private void bgwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeSearchStatus("done");
        }

        private void bgwSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string[] data = e.UserState as string[];
            AddSearchResult(data);
        }

        private void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            StartDownload(e);
        }

        private void bgwDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            decimal[] args = e.UserState as decimal[];
            UpdateProgress(args[0], args[1]);
        }

        private void bgwCheckForUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckForUpdate();
        }

        private void bgwCheckForUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string newVersion = e.UserState as string;
            ShowUpdateLabel(newVersion);
        }

        private void txtSaveFolder_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
