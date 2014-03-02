using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace BeatDownloader
{
    public static class GlobalVar
    {
        private static string _SaveFolder;

        public static Version CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;

        static GlobalVar()
        {
            _SaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public static string SaveFolder
        {
            get { return _SaveFolder; }
            set { _SaveFolder = value; }
        }
    }
}
