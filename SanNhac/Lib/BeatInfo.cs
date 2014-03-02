using System;
using System.Collections.Generic;
using System.Text;

namespace BeatDownloader
{
    public class BeatInfo
    {
        private string _Title;
        private string _Artist;
        private string _Genre;
        private string _Site;
        private string _Link;
        private string _Beat;
        private string _BeatType;

        public BeatInfo()
        {
            _Title = "";
            _Artist = "";
            _Genre = "";
            _Site = "";
            _Link = "";
            _Beat = "";
            _BeatType = "";
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Artist
        {
            get { return _Artist; }
            set { _Artist = value; }
        }

        public string Genre
        {
            get { return _Genre; }
            set { _Genre = value; }
        }

        public string Site
        {
            get { return _Site; }
            set { _Site = value; }
        }

        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }

        public string Beat
        {
            get { return _Beat; }
            set { _Beat = value; }
        }

        public string BeatType
        {
            get { return _BeatType; }
            set { _BeatType = value; }
        }
    }
}
