using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    class MimeTypeHelper
    {
        protected static Dictionary<string, string> _mimeTypes;
        protected static void InitializeMimeTypes()
        {
            _mimeTypes = new Dictionary<string, string>();

            try
            {
                string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "mime.dat");
                if (File.Exists(dataPath))
                {
                    StreamReader reader = new StreamReader(dataPath);
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length < 1) continue;

                        if (line.StartsWith("#")) continue;
                        
                        string[] lineParts = line.Split(new char[] { ';' });
                        if (lineParts.Length < 2) continue;
                        
                        string mime = lineParts[1].Trim();
                        if (mime.Length < 1) continue;
                        
                        string[] extensions = lineParts[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (extensions.Length < 1) continue;

                        foreach (string rawExtension in extensions)
                        {
                            string extension = rawExtension.Trim().TrimStart(new char[] { '.' });
                            if (extension.Length < 1) continue;
                            if (_mimeTypes.ContainsKey(extension)) _mimeTypes[extension] = mime;
                            else _mimeTypes.Add(extension, mime);
                        }
                    }
                }
            }
            catch { }
        }
        public static string GetMimeTypeByExtension(string fileExtension)
        {
            if (_mimeTypes == null) InitializeMimeTypes();

            string extension = fileExtension.TrimStart(new char[] { '.' });
            if (_mimeTypes.ContainsKey(extension)) return _mimeTypes[extension];

            else return null;
        }
        public static string GetMimeTypeByFilePath(FileInfo fileInfo)
        {
            return GetMimeTypeByExtension(fileInfo.Extension);
        }
        public static string GetMimeTypeByFilePath(string filePath)
        {
            return GetMimeTypeByFilePath(new FileInfo(filePath));
        }
        public static string[] GetExtensions(string mimetype)
        {
            string mime = mimetype.ToLower();
            if (_mimeTypes == null) InitializeMimeTypes();
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, string> pair in _mimeTypes)
            {
                if (pair.Value == mime) ret.Add(pair.Key);
            }
            return ret.ToArray();
        }
    }
}