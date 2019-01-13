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

namespace PathDisplay
{
	public partial class PathDisplay : Form
	{
		public PathDisplay()
		{
			InitializeComponent();
		}

		private void PathDisplay_Load(object sender, EventArgs e)
		{
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				if (Directory.Exists(args[1]) == true)
				{
					txtPath.Text = args[1];
					txtDirectory.Text = args[1];
				}
				else if (File.Exists(args[1]) == true)
				{
					txtPath.Text = args[1];
					txtDirectory.Text = Path.GetDirectoryName(args[1]);
				}
				else
					Close();
			}
			else
				Close();
		}

		private void txtPath_Enter(object sender, EventArgs e)
		{
			Clipboard.SetText(txtPath.Text);
		}

		private void txtDirectory_Enter(object sender, EventArgs e)
		{
			Clipboard.SetText(txtDirectory.Text);
		}
	}
}
