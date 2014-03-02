using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace BeatDownloader
{
    class GlobalCode
    {
        // get XmlDocument from url
        public static XmlDocument GetXML(string url)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url);
            }
            catch
            {
                throw;
            }
            return xmlDoc;
        }

        // get web content from url, unGzip = false
        public static string GetContent(string url)
        {
            try
            {
                return GetContent(url, false);
            }
            catch
            {
                throw;
            }
        }

        // get web content from url
        public static string GetContent(string url, bool unGzip)
        {
            string result = "";

            WebRequest http;
            WebResponse response = null;
            StreamReader stream = null;
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
                    // un-Gzip
                    stream = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.Default);
                }

                result = stream.ReadToEnd();

            }
            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }

                if (stream != null)
                {
                    stream.Close();
                }
            }

            return result;
        }
    }
}
