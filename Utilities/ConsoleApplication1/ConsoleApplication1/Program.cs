using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@"C:\Users\dmooring\OneDrive\Pictures\Memorial\", "*.jpg", SearchOption.TopDirectoryOnly);
            List<string> filelist = new List<string>();
            Random r = new Random();
            foreach (string s in files)
                filelist.Add(s);
            var result = filelist.OrderBy(item => r.Next());
            int i = 76;
            foreach (string s in result)
            {
                string path = Path.GetDirectoryName(s);
                string file = Path.GetFileName(s);
                string destfile = GetFileFromNo(i++);
                string destpath = Path.Combine(path, destfile);
                File.Move(s, destpath);
                string ss = s;
            }
        }
        static string GetFileFromNo(int i)
        {
            if (i < 10)
                return "000" + i.ToString() + ".jpg";
            if (i < 100)
                return "00" + i.ToString() + ".jpg";
            if (i < 1000)
                return "0" + i.ToString() + ".jpg";
            return i.ToString() + ".jpg";
        }
    }
}
