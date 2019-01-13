using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KaraokeQueuer
{
	public partial class LaunchForm : Form
	{
		private string playerPath = "";
		private const string SunFlyPlayer = "SingSunflyPlayer";
		private const string SunFlyPath = @"C:\Program Files (x86)\SingSunflyPlayer\SingSunflyPlayer.exe";
		private const string KBPlayer = "kbplayer.exe";
		private const string KBPath = @"C:\Program Files (x86)\Karaoke Builder Studio\kbplayer.exe";

		public string _path = "";

		public LaunchForm(string path)
		{
			InitializeComponent();
			_path = path;
		}

		private void btnPlayer_Click(object sender, EventArgs e)
		{
			playerPath = SunFlyPath;
			if (radio_KBPlayer.Checked == true)
			{
				playerPath = KBPath;
			}

			if (_path.Contains('\\') && _path.Trim().Length > 2 && _path.Trim().ToLower().Substring(_path.Length - 3) == "cdg")
			{
				KillPlayer(SunFlyPlayer);
				KillPlayer(KBPlayer);

				ProcessStartInfo start = null;

				start = new ProcessStartInfo(playerPath, string.Format("\"{0}\"", _path));

				Process.Start(start);
			}
			else
				if (_path.Trim().Length > 0)
					Process.Start(_path);
			this.Close();
		}

		private void KillPlayer(string processName)
		{
			Process playerProcess = null;
			Process[] list = Process.GetProcessesByName(processName);
			foreach (Process process in list)
			{
				if (process.ProcessName == processName)
				{
					playerProcess = process;
					break;
				}
			}
			if (playerProcess != null)
			{
				playerProcess.Kill();
				System.Threading.Thread.Sleep(2000);
			}
		}

		private void LaunchForm_Load(object sender, EventArgs e)
		{
			if (!(_path.Contains('\\') && _path.Trim().Length > 2 && _path.Trim().ToLower().Substring(_path.Length - 3) == "cdg"))
			{
				Process.Start(_path);
				this.Close();
			}
		}
	}
}
