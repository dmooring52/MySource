using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WS = IWshRuntimeLibrary;

namespace mp3Name
{
    class Program
    {
        static void Main(string[] args)
        {
            WS.FileSystemObject fso = new WS.FileSystemObject();
            string cd = fso.GetAbsolutePathName(".");
            WS.Folder fld = fso.GetFolder(cd);
            string fname = fld.Name;
            /*
            string[] files = Directory.GetFiles(".", "*.mp3");
            foreach (string file in files)
            {
                FileAttributes fa = File.GetAttributes(file);
                
            }*/
        }
    }
}
