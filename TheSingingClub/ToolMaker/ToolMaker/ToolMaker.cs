using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolMaker
{
	public partial class ToolMaker : Form
	{
		public ToolMaker()
		{
			InitializeComponent();
		}

		private void btnComponent_Click(object sender, EventArgs e)
		{
			ViewComponent(@"..\..\CodeFileComponent.txt");
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			ViewComponent(@"..\..\CodeFileLoad.txt");
		}

		private void ViewComponent(string path)
		{
			ComponentMaker cm = new ComponentMaker();
			ToolView tv = new ToolView();
			tv.SetContent(cm.Build(path));
			tv.ShowDialog(this);
		}
	}
}
