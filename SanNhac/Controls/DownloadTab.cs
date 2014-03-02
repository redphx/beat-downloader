using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BeatDownloader
{
    public partial class DownloadTab : UserControl
    {
        private Downloader dlBeat;

        
        private bool isDownloading = false;

        public DownloadTab()
        {
            InitializeComponent();
        }

        private void DownloadTab_Load(object sender, EventArgs e)
        {
            lbDownloadPercent.Text = "";
            lbDownloadSize.Text = "";
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm frmAbout = new AboutForm();
            frmAbout.StartPosition = FormStartPosition.CenterParent;
            frmAbout.ShowDialog(this);
        }

        private void dgvDownload_SelectionChanged(object sender, EventArgs e)
        {
            int count = dgvDownload.SelectedRows.Count;
            if (count == 0)
            {
                btnDownload.Enabled = false;
                btnDelete.Enabled = false;
                btnStop.Enabled = false;
                return;
            }
            
            bool enableDownload = false;
            bool enableStop = false;

            for (int i = 0; i < count; i++)
            {
                DataGridViewCell cell = dgvDownload.SelectedRows[i].Cells[5];

                if (!enableDownload)
                {
                    if (cell.Tag == null || (DownloaderState)cell.Tag != DownloaderState.Downloading)
                    {
                        enableDownload = true;
                    }
                }

                if (!enableStop)
                {
                    if (cell.Tag != null && (DownloaderState)cell.Tag == DownloaderState.Downloading)
                    {
                        enableStop = true;
                    }
                }
            }


            btnDownload.Enabled = enableDownload;
            btnStop.Enabled = enableStop;
            btnDelete.Enabled = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDownload.SelectedRows.Count == 0)
            {
                return;
            }

            bool deleteDownloading = false;

            for (int i = dgvDownload.SelectedRows.Count - 1; i >= 0; i--)
            {
                DownloaderState state = (DownloaderState) dgvDownload.SelectedRows[i].Cells[5].Tag;

                if (state == DownloaderState.Downloading)
                {
                    dlBeat.Stop();
                    dlBeat = null;
                    deleteDownloading = true;
                    //SetStatus(row.Index, DownloaderState.Stopped);
                }

                dgvDownload.Rows.RemoveAt(dgvDownload.SelectedRows[i].Index);
            }

            if (deleteDownloading)
            {
                DoDownload();
            }
        }


        public void AddRow(BeatInfo info)
        {
            Main frmMain = (Main)Parent.Parent.Parent;

            // find beat
            for (int i = 0; i < dgvDownload.Rows.Count; i++)
            {
                if (dgvDownload.Rows[i].Cells[3].Value.ToString() == info.Link)
                {
                    return;
                }
            }

            object[] row = new object[] {
                IconList.GetIcon(IconList.GetIconIndex(info.Site)),
                info.Title,
                info.Artist,
                info.Link,
                "",
                "Not started"
            };

            dgvDownload.Rows.Add(row);

            dgvDownload.Rows[dgvDownload.Rows.Count - 1].Cells[0].Tag = info.Site;
            dgvDownload.Rows[dgvDownload.Rows.Count - 1].Cells[5].Tag = DownloaderState.NotStarted;

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDownload.SelectedRows.Count; i++)
            {
                DataGridViewRow row = dgvDownload.SelectedRows[i];

                DownloaderState state = (DownloaderState)row.Cells[5].Tag;

                // queuing
                if (state != DownloaderState.Downloading && state != DownloaderState.Ended)
                {
                    SetStatus(row.Index, DownloaderState.Queuing);
                }
            }

            if (!isDownloading)
            {
                DoDownload();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDownload.SelectedRows.Count; i++)
            {
                DataGridViewRow row = dgvDownload.SelectedRows[i];

                if ((DownloaderState)row.Cells[5].Tag == DownloaderState.Downloading)
                {
                    dlBeat.Stop();
                    SetStatus(row.Index, DownloaderState.Stopped);
                    //dgvDownload_SelectionChanged(sender, e);
                }
                else
                {
                    SetStatus(row.Index, DownloaderState.NotStarted);
                }
            }

        }


        private void DoDownload()
        {
            if (isDownloading)
            {
                return;
            }

            int index = FindIndexByStatus(DownloaderState.Queuing);
            
            if (index == -1)
            {
                return;
            }

            isDownloading = true;

            btnDownload.Enabled = false;
            btnStop.Enabled = true;


            BeatInfo beatInfo = new BeatInfo();
            beatInfo.Site = dgvDownload.Rows[index].Cells[0].Tag.ToString();
            beatInfo.Title = dgvDownload.Rows[index].Cells[1].Value.ToString();
            beatInfo.Artist = dgvDownload.Rows[index].Cells[2].Value.ToString();
            beatInfo.Link = dgvDownload.Rows[index].Cells[3].Value.ToString();
            beatInfo.Beat = dgvDownload.Rows[index].Cells[4].Value.ToString();

            // downloading
            SetStatus(index, DownloaderState.Downloading);

            // status label
            lbDownloadStatus.Text = "Downloading \"" + beatInfo.Artist + " - " + beatInfo.Title + "\" ...";

            DownloadBeat(beatInfo);
        }

        delegate void DownloadBeatCallBack(Object info);
        private void DownloadBeat(Object info)
        {
            if (InvokeRequired)
            {
                Invoke(new DownloadBeatCallBack(DownloadBeat), info);
                return;
            }

            prgDownload.Value = 0;
            lbDownloadPercent.Text = "";
            //lbDownloadStatus.Text = "";
            lbDownloadSize.Text = "";

            if (dlBeat != null)
            {
                dlBeat.Stop();
                dlBeat = null;
            }

            
            dlBeat = new Downloader((BeatInfo)info);
            dlBeat.SavePath = GlobalVar.SaveFolder;
            dlBeat.Completed += new Downloader.CompletedHandler(dlBeat_Completed);
            dlBeat.Stopped += new Downloader.StoppedHandler(dlBeat_Stopped);
            dlBeat.Error += new Downloader.ErrorHandler(dlBeat_Error);
            dlBeat.UpdateProgress += new Downloader.UpdateProgressHandler(dlBeat_UpdateProgress);

            dlBeat.Start();
        }

        private void dlBeat_Stopped(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Downloader.StoppedHandler(dlBeat_Stopped), new object[] { sender, e });
                return;
            }

            int index = FindIndexByStatus(DownloaderState.Downloading);
            SetStatus(index, DownloaderState.Stopped);

            lbDownloadStatus.Text = "Stopped";

            dlBeat = null;
            isDownloading = false;
            DoDownload();
            //dgvDownload_SelectionChanged(sender, e);
        }

        private void dlBeat_Completed(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Downloader.CompletedHandler(dlBeat_Completed), new object[] { sender, e });
                return;
            }

            lbDownloadStatus.Text = "Done";

            int index = FindIndexByStatus(DownloaderState.Downloading);
            SetStatus(index, DownloaderState.Ended);

            dlBeat = null;
            isDownloading = false;
            //dgvDownload_SelectionChanged(sender, e);

            DoDownload();
        }


        private void dlBeat_Error(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Downloader.ErrorHandler(dlBeat_Error), new object[] { sender, e });
                return;
            }

            
            
            int index = FindIndexByStatus(DownloaderState.Downloading);
            if (index != -1)
            {
                SetStatus(index, DownloaderState.EndedWithError);
            }

            lbDownloadStatus.Text = "Error";
            btnDownload.Enabled = true;
            btnStop.Enabled = false;

            isDownloading = false;
            dlBeat = null;
            DoDownload();
            //dgvDownload_SelectionChanged(sender, e);
        }

        
        private void dlBeat_UpdateProgress(object sender, UpdateProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Downloader.UpdateProgressHandler(dlBeat_UpdateProgress), new object[] { sender, e });
                return;
            }

            int percentProgress = Convert.ToInt32((e.DownloadSize * 100) / e.FileSize);

            lbDownloadSize.Text = Math.Round(e.DownloadSize / 1024 / 1024, 2) + " MB / " + Math.Round(e.FileSize / 1024 / 1024, 2) + " MB";
            lbDownloadPercent.Text = percentProgress + "%";

            prgDownload.Value = percentProgress;

        }

        private int FindIndexByStatus(DownloaderState state)
        {
            for (int i = 0; i < dgvDownload.Rows.Count; i++)
            {
                if ((DownloaderState)dgvDownload.Rows[i].Cells[5].Tag == state)
                {
                    return i;
                }
            }
            return -1;
        }

        private void SetStatus(int rowIndex, DownloaderState state)
        {
            string status;
            switch (state)
            {
                case DownloaderState.Queuing:
                    status = "Queuing";
                    break;
                case DownloaderState.Downloading:
                    status = "Downloading";
                    break;
                case DownloaderState.Stopped:
                    status = "Stopped";
                    break;
                case DownloaderState.Ended:
                    status = "Done";
                    break;
                case DownloaderState.EndedWithError:
                    status = "Error";
                    break;
                default:
                    status = "Not started";
                    break;
            }

            dgvDownload.Rows[rowIndex].Cells[5].Value = status;
            dgvDownload.Rows[rowIndex].Cells[5].Tag = state;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            OptionsForm frmOptions = new OptionsForm();
            frmOptions.StartPosition = FormStartPosition.CenterParent;
            frmOptions.ShowDialog(this);
        }

    }
}
