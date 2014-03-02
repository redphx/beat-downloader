using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BeatDownloader
{
    class MP3
    {
        public static void ExtractFromSwf(string filePath)
        {
            FileStream myFStream = null;

            try
            {
                SWF swfParser = new SWF(filePath + ".swf");
                swfParser.ReadHeader();
                byte[] tmp;

                //byte[] mp3 = "";
                TagInfo info;

                myFStream = new FileStream(filePath + ".mp3", FileMode.Create, FileAccess.Write);

                do
                {
                    info = swfParser.ReadTag();
                    if (info.Size > 0)
                    {
                        tmp = swfParser.ReadBytes((int)info.Size);
                        if (info.Id == 19)
                        {
                            myFStream.Write(tmp, 4, tmp.Length - 4);
                        }
                    }
                }
                while (info.Id > 0);

            }
            catch
            {
                throw;
            }
            finally
            {
                if (myFStream != null)
                {
                    myFStream.Close();
                }
            }
        }

        public static void UpdateInfo(string filePath, BeatInfo info)
        {
            TagLib.File mp3File = TagLib.File.Create(filePath + ".mp3");
            TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag)mp3File.GetTag(TagLib.TagTypes.Id3v2);


            tag.Title = info.Title;
            tag.Performers = new string[] { info.Artist };
            tag.Comment = "Beat Downloader - redphx\nhttp://beat.redphx.com\nhttp://www.karaholics.com";

            mp3File.Save();

        }
    }
}
