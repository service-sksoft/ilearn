namespace ILearn.Services.Common
    {
    using System.IO;
    using System.Web;
    using System.IO.Compression;
    using System.Drawing;
    using System;

    public static class Cmn
        {

        public static string UploadFolder = "/Uploads/";
        public static string TopicFolder = "Topics/";

        public static int MaxQuestionPerPage = 10;

        public static string ImageToBase64(string filePath)
            {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath("~" + filePath)));
            }

        public static string getTopicImageURL(int topicID)
            {

            string filePath = UploadFolder + TopicFolder + "topic_" + topicID + ".png";
            if (File.Exists(HttpContext.Current.Server.MapPath("~" + filePath)))
                {
                return ImageToBase64(filePath);
                }
            return "";

            }

        public static string getDefaultValueForString(string val, string defaultValue)
            {
            return !string.IsNullOrWhiteSpace(val) ? val : defaultValue;

            }
        public static byte[] GetCompressed(string str, string CompressionType = "gzip")
            {
            CompressionType = getDefaultValueForString(CompressionType, "gzip");

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(str);
            MemoryStream ms = new MemoryStream();

            switch (CompressionType)
                {
                case "gzip":
                    {
                    GZipStream gz = new GZipStream(ms, CompressionMode.Compress, true);
                    gz.Write(buffer, 0, buffer.Length);
                    gz.Close();

                    }
                break;
                case "deflate":
                    {
                    DeflateStream dz = new DeflateStream(ms, CompressionMode.Compress);
                    dz.Write(buffer, 0, buffer.Length);
                    dz.Close();
                    }
                break;
                }

            byte[] compressedData = (byte[])ms.ToArray();
            return compressedData;

            }

        public static string GetUnCompressed(byte[] Data, int Size)
            {
            if (Data == null)
                return string.Empty;
            MemoryStream ms = new MemoryStream(Data);
            GZipStream gz = null;
            try
                {

                gz = new GZipStream(ms, CompressionMode.Decompress);
                byte[] decompressedBuffer = new byte[Size];
                int DataLength = gz.Read(decompressedBuffer, 0, Size);
                using (MemoryStream msDec = new MemoryStream())
                    {
                    msDec.Write(decompressedBuffer, 0, DataLength);
                    msDec.Position = 0;
                    string s = new StreamReader(msDec).ReadToEnd();
                    return s;
                    }
                }
            catch
                {
                //return ex.Message;
                }
            finally
                {
                ms.Close();
                gz.Close();
                }

            return string.Empty;
            }

        }

    }
