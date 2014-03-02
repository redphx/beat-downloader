using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.IO.Compression;
//using ComponentAce.Compression.Libs.zlib;

namespace BeatDownloader
{
    class TagInfo
    {
        public ushort Tag;
        public int Id;
        public uint Size;

    }


    class SWF
    {
        //int buff;
        //String swfContent;
        private FileStream fsIn;
        //private FileStream fsOut;
        //private Hashtable swfInfo = new Hashtable();
        private BinaryReader fileReader;
        private string filePath;
        //private bool isCompressed = false;

        public SWF(string path)
        {
            filePath = path;

            fsIn = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        /*
        public void Uncompress()
        {
            const int bufferSize = 4096;
            int count = 0;
            byte[] buffer = new byte[bufferSize];
            byte[] head = new byte[8];

            fsIn.Seek(0, SeekOrigin.Begin);
            fsIn.Read(head, 0, 8);
            head[0] = 0x46;

            ZInputStream zlib = new ZInputStream(fsIn);

            fsOut = new FileStream(filePath+".tmp", FileMode.Create, FileAccess.Write, FileShare.None);

            fsOut.Write(head, 0, 8);

            try
            {
                while (true)
                {
                    count = zlib.read(buffer, 0, bufferSize);
                    
                    if (count > 0)
                    {
                        fsOut.Write(buffer, 0, count);
                    }
                    else {
                        // have reached the end
                        break;
                    }
                }
            }
            catch
            {
                //System.Diagnostics.Debug.Assert(false, ex.ToString());
            }

            zlib.Close();
            fsIn.Close();
            fileReader.Close();
            fsOut.Close();

            File.Delete(filePath);
            File.Move(filePath + ".tmp", filePath);

            fsIn = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            fileReader = new BinaryReader(fsIn);
            fileReader.ReadChars(3);
        }
        */

        public void ReadHeader()
        {
            char[] buff = new char[3];
            buff[0] = (char)fsIn.ReadByte();
            buff[1] = (char)fsIn.ReadByte();
            buff[2] = (char)fsIn.ReadByte();

            string sig = new String(buff);

            if (sig == "CWS")
            {
                fsIn.Seek(10, SeekOrigin.Begin);

                fileReader = new BinaryReader(new DeflateStream(fsIn, CompressionMode.Decompress));

            }
            else
            {
                fsIn.Seek(3, SeekOrigin.Begin);
                fileReader = new BinaryReader(fsIn);

                // version
                fileReader.ReadByte();
                // file size
                fileReader.ReadUInt32();
            }

            // framesize
            ReadBox();
            // framerate
            fileReader.ReadUInt16();
            // frame count
            fileReader.ReadUInt16();

        }

        public TagInfo ReadTag()
        {
            
            TagInfo info = new TagInfo();

            ushort tag = fileReader.ReadUInt16();
            int id = (int)tag >> 6;
            uint size = (uint)tag & 0x3F;
            if (size == 0x3F)
            {
                size = fileReader.ReadUInt32();
            }

            info.Tag = tag;
            info.Id = id;
            info.Size = size;

            if (id == 0)
            {
                fileReader.Close();
            }

            return info;
        }

        public byte[] ReadBytes(int size)
        {
            return fileReader.ReadBytes(size);
        }

        public void ReadBox()
        {
            uint[] c = {0,0,0,0,0};

            uint current = Convert.ToUInt32(fileReader.ReadByte());
            int size = (int)current >> 3;
            int off = 3;

            for (int i=0; i<4; i+=1) {
                c[i] = current << (32-off) >> (32-size);
                off -= size;
                while (off < 0) {
                    current = fileReader.ReadByte();
                    c[i] |= (off < -8)?current << (- off - 8):current >> (- off - 8);
                    off += 8;
                }
            }
            
        }
    }
}
