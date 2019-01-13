using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace LaunchCDG
{
	public partial class LaunchForm : Form
	{
		private string[] _args;
		private string playerPath = "";
		private const string SunFlyPlayer = "SingSunflyPlayer";
		private const string SunFlyPath = @"C:\Program Files (x86)\SingSunflyPlayer\SingSunflyPlayer.exe";
		private const string KBPlayer = "kbplayer.exe";
		private const string KBPath = @"C:\Program Files (x86)\Karaoke Builder Studio\kbplayer.exe";


		public LaunchForm(string[] Args)
		{
			_args = Args;
			InitializeComponent();
		}

		private void btn_Select_Click(object sender, EventArgs e)
		{
			playerPath = SunFlyPath;
			if (radio_KBPlayer.Checked == true)
			{
				playerPath = KBPath;
			}

			KillPlayer(SunFlyPlayer);
			KillPlayer(KBPlayer);

			ProcessStartInfo start = null;

			if (_args.Length > 1)
				start = new ProcessStartInfo(playerPath, string.Format("\"{0}\"", _args[1]));
			else
				start = new ProcessStartInfo(playerPath);

			Process.Start(start);
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
			radio_KBPlayer.Checked = true;
		}
	}
}
