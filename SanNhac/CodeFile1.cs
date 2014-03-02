using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SanNhac
{
    class Test
    {
        private Test()
        {
            SWF swfParser = new SWF(@"C:\test.swf");
            swfParser.ReadHeader();
            byte[] tmp;

            //byte[] mp3 = "";
            TagInfo info;

            FileStream myFStream = new FileStream(@"C:\test.mp3", FileMode.Create, FileAccess.Write);

            do {
                info = swfParser.ReadTag();
                if (info.Size > 0) {
                    tmp = swfParser.ReadBytes((int)info.Size);
                    if (info.Id == 19) {
                        myFStream.Write(tmp, 4, tmp.Length-4);
                        //Console.WriteLine(BitConverter.ToString(tmp));
                    }
                }
            }
            while (info.Id > 0);

            //TextWriter tw = new StreamWriter(@"C:\test.mp3");
            //tw.Write(mp3);
            myFStream.Close();
            
        }

        static void Main(string[] args)
        {
            new Test();
        }
    }
}
