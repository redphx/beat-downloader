using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;


namespace BeatDownloader
{
    public enum DownloaderState : byte
    {
        NotStarted = 0,
        Queuing,
        Downloading,
        Stopped,
        Ended,
        EndedWithError
    }

    public class UpdateProgressEventArgs : EventArgs
    {
        protected decimal _FileSize, _DownloadedSize;

        public decimal FileSize
        {
            get { return _FileSize; }
        }

        public decimal DownloadSize
        {
            get { return _DownloadedSize; }
        }

        public UpdateProgressEventArgs(decimal fSize, decimal dSize)
        {
            _FileSize = fSize;
            _DownloadedSize = dSize;
        }

    }

    

    class Downloader
    {
        #region Events & Delegates
        
        public event UpdateProgressHandler UpdateProgress;
        public event CompletedHandler Completed;
        public event StoppedHandler Stopped;
        public event ErrorHandler Error;

        ManualResetEvent m_EventStopThread;
        ManualResetEvent m_EventThreadStopped;

        public delegate void CompletedHandler(object sender, EventArgs e);
        public delegate void StoppedHandler(object sender, EventArgs e);
        public delegate void ErrorHandler(object sender, EventArgs e);
        public delegate void UpdateProgressHandler(object sender, UpdateProgressEventArgs e);

        #endregion

        private BeatInfo _BeatInfo;

        private Thread thrDownload;

        private string _SavePath;


        // The stream of data retrieved from the web server
        private Stream strResponse;
        // The stream of data that we write to the harddrive
        private Stream strLocal;

        private Int64 downloadedSize;

        // The request to the web server for file information
        private HttpWebRequest webRequest;
        // The response from the web server containing information about the file
        private HttpWebResponse webResponse;
        

        public string SavePath
        {
            get { return _SavePath; }
            set { _SavePath = value; }
        }


        public Downloader(BeatInfo info)
        {
            thrDownload = null;

            _BeatInfo = info;

            m_EventStopThread = new ManualResetEvent(false);
            m_EventStopThread.Reset();
            m_EventThreadStopped = new ManualResetEvent(false);
            m_EventThreadStopped.Reset();
        }

        protected virtual void OnUpdateProgress(UpdateProgressEventArgs e)
        {
            
            if (UpdateProgress != null)
            {
                UpdateProgress(this, e);
            }
        }

        protected virtual void OnError()
        {
            if (Error != null)
            {
                Error(this, EventArgs.Empty);
            }
        }

        protected virtual void OnCompleted()
        {
            if (Completed != null)
            {
                Completed(this, EventArgs.Empty);
            }
        }

        protected virtual void OnStopped()
        {
            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            thrDownload = new Thread(Download);
            thrDownload.IsBackground = true;
            thrDownload.Start();
        }

        public void Stop()
        {
            if (thrDownload != null && thrDownload.IsAlive)
            {
                m_EventStopThread.Set();

                while (thrDownload.IsAlive)
                {
                    if (WaitHandle.WaitAll((new ManualResetEvent[] { m_EventThreadStopped }), 100, true))
                    {
                        OnStopped();
                        break;
                    }
                    
                }
            }
        }

        public void Download()
        {
            Int64 fileSize = 0;
            string fullSavePath = _SavePath.TrimEnd('\\') + @"\" + _BeatInfo.Artist + " - " + _BeatInfo.Title;

            try
            {
                if (m_EventStopThread.WaitOne(0, true))
                {
                    throw new Exception();
                }

                // get beat
                if (_BeatInfo.Beat.Length == 0 || _BeatInfo.Beat == null)
                {
                    string webContent;
                    Match matchURL;

                    if (_BeatInfo.Site == "sannhac.com")
                    {
                        // get content + ungzip
                        webContent = GlobalCode.GetContent(_BeatInfo.Link, true);

                        matchURL = Regex.Match(webContent, @"<div class=""ttct\-download""><a href=""([^""]+)""", RegexOptions.IgnoreCase);
                        if (matchURL.Groups.Count != 0)
                        {
                            _BeatInfo.Beat = matchURL.Groups[1].Value;
                        }
                    }
                    // zing
                    else
                    {
                        webContent = GlobalCode.GetContent("http://star.zing.vn/includes/fnGetSongInfo.php?id=" + _BeatInfo.Link);

                        matchURL = Regex.Match(webContent, @"<karaokelink>([^<]+)</karaokelink>", RegexOptions.IgnoreCase);
                        if (matchURL.Groups.Count != 0)
                        {
                            _BeatInfo.Beat = matchURL.Groups[1].Value;
                        }
                    }
                    
                }

                // download beat
                if (_BeatInfo.Beat.Length == 0 || _BeatInfo.Beat == null)
                {
                    OnError();
                    return;
                }


                if (_BeatInfo.Beat.EndsWith(".mp3", StringComparison.CurrentCultureIgnoreCase) || _BeatInfo.Beat.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase))
                {
                    _BeatInfo.BeatType = ".mp3";
                }
                else
                {
                    _BeatInfo.BeatType = ".swf";
                }


                if (m_EventStopThread.WaitOne(0, true))
                {
                    throw new Exception();
                }


                // DOWNLOAD

                using (WebClient wcDownload = new WebClient())
                {

                    

                    webRequest = (HttpWebRequest) HttpWebRequest.Create(_BeatInfo.Beat);
                    webRequest.Credentials = CredentialCache.DefaultCredentials;

                    webRequest.KeepAlive = false;
                    //webRequest.Timeout = 30000;
                    
                    webResponse = (HttpWebResponse)webRequest.GetResponse();

                    if (m_EventStopThread.WaitOne(0, true))
                    {
                        throw new Exception();
                    }

                    fileSize = webResponse.ContentLength;

                    // Open the URL for download
                    strResponse = webResponse.GetResponseStream();

                    OnUpdateProgress(new UpdateProgressEventArgs(fileSize, 0));

                    if (!m_EventStopThread.WaitOne(0, true))
                    {
                        strLocal = new FileStream(fullSavePath + _BeatInfo.BeatType, FileMode.Create, FileAccess.Write, FileShare.None);

                        int bytesSize = 0;
                        byte[] downBuffer = new byte[2048];

                        while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                        {
                            if (m_EventStopThread.WaitOne(0, true))
                            {
                                m_EventThreadStopped.Set();
                            }
                            else
                            {
                                // write to file
                                strLocal.Write(downBuffer, 0, bytesSize);

                                // update progress bar
                                downloadedSize = strLocal.Length;
                                OnUpdateProgress(new UpdateProgressEventArgs(fileSize, downloadedSize));
                            }

                        }
                    }
                }

            }
            catch
            {
                //m_EventThreadStopped.Set();
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (webRequest != null)
                {
                    webRequest.Abort();
                }

                if (strResponse != null)
                {
                    strResponse.Close();
                }

                if (strLocal != null)
                {
                    strLocal.Close();
                }


                if (m_EventThreadStopped.WaitOne(0, true))
                {
                    /*
                    try
                    {
                        //File.Delete(fullSavePath + _BeatInfo.BeatType);
                    }
                    catch
                    {
                    }
                    */

                    OnStopped();
                }
                else
                {
                    bool hasError = (downloadedSize != fileSize);

                    if (hasError)
                    {
                        OnError();
                        /*
                        try
                        {
                            //File.Delete(fullSavePath + _BeatInfo.BeatType);
                        }
                        catch
                        {
                        }
                         */

                    }
                    else
                    {
                        try
                        {
                            if (_BeatInfo.BeatType == ".swf")
                            {
                                MP3.ExtractFromSwf(fullSavePath);
                            }

                            MP3.UpdateInfo(fullSavePath, _BeatInfo);

                            OnCompleted();
                        }
                        catch
                        {
                            //File.Delete(fullSavePath + ".mp3");
                            OnError();
                        }
                    }
                }
            }
        }
    }
}