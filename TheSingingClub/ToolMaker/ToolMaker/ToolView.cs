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
	public partial class ToolView : Form
	{
		public ToolView()
		{
			InitializeComponent();
			UserInitialize();
		}
		private void UserInitialize()
		{
			richTextBoxView.ContextMenuStrip = new ContextMenuStrip();
			ToolStripMenuItem eSelect = new ToolStripMenuItem("&Select All");
			ToolStripMenuItem eCopy = new ToolStripMenuItem("&Copy");
			richTextBoxView.ContextMenuStrip.Items.Add(eSelect);
			richTextBoxView.ContextMenuStrip.Items.Add(eCopy);
			eSelect.Click += eSelect_Click;
			eCopy.Click += eCopy_Click;
		}

		void eCopy_Click(object sender, EventArgs e)
		{
			richTextBoxView.Copy();
		}

		void eSelect_Click(object sender, EventArgs e)
		{
			richTextBoxView.Select(0, richTextBoxView.Text.Length);
		}
		public void SetContent(string content)
		{
			richTextBoxView.Text = content;
		}
	}
}
