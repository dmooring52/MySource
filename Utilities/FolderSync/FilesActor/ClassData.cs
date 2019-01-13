using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilesActor
{
    class ActorFile
    {
        private string _basePath = "";
        private string _filePath = "";
        private string _destBasePath = "";
        private bool _leaf = false;
        public string filePath { get { return _filePath; } set { _filePath = value; } }
        public string basePath { get { return _basePath; } set { _basePath = value; } }
        public string destBasePath { get { return _destBasePath; } set { _destBasePath = value; } }
        public bool leaf { get { return _leaf; } set { _leaf = value; } }
        public string relativePath
        {
            get
            {
                return _filePath.Substring(_basePath.Length);
            }
        }
        public string destFilePath { get { return Path.Combine(_destBasePath, relativePath); } }
        public string fileName
        {
            get
            {
                return Path.GetFileName(_filePath.Trim(new char[] { '\\', ' ' }));
            }
        }

        public ActorFile(string basepath, string destbasepath, string file, bool isleaf)
        {
            _basePath = basepath;
            _destBasePath = destbasepath;
            _filePath = file;
            _leaf = isleaf;
        }

        public bool Matches(object o)
        {
            if (o is ActorFile)
            {
                ActorFile af = o as ActorFile;
                if (af.relativePath == relativePath)
                    return true;
            }
            return false;
        }
    }
}
