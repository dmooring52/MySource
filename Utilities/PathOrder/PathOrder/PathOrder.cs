using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathOrder
{
	public partial class PathOrder : Form
	{
		public PathOrder()
		{
			InitializeComponent();
		}

		private void PathOrder_Load(object sender, EventArgs e)
		{
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				if (Directory.Exists(args[1]) == true)
				{
					//txtPath.Text = args[1];
					//txtDirectory.Text = args[1];
                    Close();
				}
				else if (File.Exists(args[1]) == true)
				{
                    string s = Clipboard.GetText();
                    int n = IsFileno(s);
                    if (n < 0)
                    {
                        Clipboard.SetText("0001");
                        s = Clipboard.GetText();
                    }
                    n = IsFileno(s);
                    if (n > 0)
                    {
                        string filename = Path.GetFileNameWithoutExtension(args[1]);
                        string fileext = Path.GetExtension(args[1]);
                        string path = Path.GetDirectoryName(args[1]);
                        string destfile = GetFileFromNo(n);
                        n++;
                        Clipboard.SetText(GetFileFromNo(n));
                        string destpath = Path.Combine(path, destfile) + fileext;
                        if (File.Exists(destpath) == false)
                        {
                            File.Move(args[1], destpath);
                            if (File.Exists(destpath))
                            { 
                                Close();
                                return;
                            }
                            MessageBox.Show(destpath, "File Not Found");
                        }
                        MessageBox.Show(destpath, "File Exists");
                        //txtPath.Text = args[1];
                        //txtDirectory.Text = Path.GetDirectoryName(args[1]);
                    }
                    else
                    {
                        Close();
                    }
				}
				else
					Close();
			}
			else
				Close();
		}
        private string GetFileFromNo(int n)
        {
            if (n < 1 || n > 9999)
                return "";
            if (n < 10)
                return "000" + n.ToString();
            if (n < 100)
                return "00" + n.ToString();
            if (n < 1000)
                return "0" + n.ToString();
            return n.ToString();
        }
        private int IsFileno(string s)
        {
            string num = "0123456789";
            if (s.Length == 4)
            {
                foreach (char c in s)
                    if (num.Contains(c) == false)
                        return -1;
                int n = -1;
                int.TryParse(s, out n);
                if (n > 0 && n < 9999)
                    return n;
            }
            return -1;
        }
    }
}
