using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace BeatDownloader
{
    class IconList
    {
        private static ImageList _Icons;

        static IconList()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Main));

            Icons = new ImageList();

            Icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));

        }

        public static ImageList Icons
        {
            get { return _Icons; }
            set { _Icons = value; }
        }

        public static int GetIconIndex(string site)
        {
            if (site == "sannhac.com")
            {
                return 0;
            }

            return 1;
        }

        public static Image GetIcon(int index)
        {
            return Icons.Images[index];
        }

        
    }
}
