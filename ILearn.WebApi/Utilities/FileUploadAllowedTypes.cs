namespace ILearn.WebApi.Utilities
{
    using System.Collections.Generic;
    using System.Text;

    public static class FileUploadAllowedTypes
    {
        private static readonly Dictionary<string, List<string>> extentionsDictionary;
        private static readonly Dictionary<string, List<byte[]>> fileHeadersDictionary;
        private static readonly Dictionary<string, List<string>> mimeTypeDictionary;

        static FileUploadAllowedTypes()
        {
            #region Extentions Dictionary
            extentionsDictionary = new Dictionary<string, List<string>>();
            extentionsDictionary.Add("image", new List<string>() { ".jpg", ".gif", ".bmp", ".png" });
            #endregion

            #region fileHeaders Dictionary
            fileHeadersDictionary = new Dictionary<string, List<byte[]>>();
            fileHeadersDictionary.Add(".jpg", new List<byte[]>() { new byte[] { 255, 216, 255, 224 }, new byte[] { 255, 216, 255, 225 } });
            fileHeadersDictionary.Add(".gif", new List<byte[]>() { Encoding.ASCII.GetBytes("GIF") });
            fileHeadersDictionary.Add(".bmp", new List<byte[]>() { new byte[] { 66, 77 } });
            fileHeadersDictionary.Add(".pdf", new List<byte[]>() { new byte[] { 37, 80, 68, 70 } });
            #endregion

            #region Mime Type Dictionary
            mimeTypeDictionary = new Dictionary<string, List<string>>();
            mimeTypeDictionary.Add(".jpg", new List<string>() { "image/jpg", "image/jpeg", "image/pjpeg" });
            mimeTypeDictionary.Add(".gif", new List<string>() { "image/gif" });
            mimeTypeDictionary.Add(".bmp", new List<string>() { "image/bmp" });
            mimeTypeDictionary.Add(".png", new List<string>() { "image/png" });
            #endregion

        }

        public static List<string> GetAllowedExtentions(string context)
        {
            List<string> list = null;
            extentionsDictionary.TryGetValue(context, out list);
            return list != null ? list : new List<string>();
        }

        public static List<byte[]> GetAllFileTypes(string fileExtention)
        {
            List<byte[]> list = null;
            fileHeadersDictionary.TryGetValue(fileExtention, out list);
            return list != null ? list : new List<byte[]>();
        }

        public static List<string> GetAllMimeTypes(string fileExtention)
        {
            List<string> list = null;
            mimeTypeDictionary.TryGetValue(fileExtention, out list);
            return list != null ? list :  new List<string>();
        }
    }
}
