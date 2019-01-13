using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesActor
{
    public partial class TreeForm : Form
    {
        public TreeView tv = null;
        private List<TreeNode> _folders;
        private TreeNode _topVis = null;
        public TreeForm()
        {
            InitializeComponent();
            tv = impactView;
        }

        private void impactView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        public void Setup(List<TreeNode> folders, string title)
        {
            _folders = folders;
            txtFolder.Text = title;
            if (_folders != null && _folders.Count > 0)
                _folders[0].ExpandAll();
            _folders.ForEach(n => SetExpand(n));
        }
        private void SetExpand(TreeNode node)
        {
            bool collapse = true;
            foreach (TreeNode tn in node.Nodes)
            {
                if (tn.ForeColor != Color.Black)
                {
                    collapse = false;
                    break;
                }
            }
            if (collapse == true && node.Parent != null)
                node.Collapse( true );
        }

        private void impactView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.Node != null && e.Node.Tag != null && e.Node.Tag is ActorFile)
                {
                    ActorFile af = e.Node.Tag as ActorFile;
                    if (af.leaf == false)
                    {
                        _topVis = impactView.TopNode;
                        contextMenuStripExp.Items["toolStripMenuItemCollapse"].Visible = e.Node.IsExpanded;
                        contextMenuStripExp.Items["toolStripMenuItemExpandAll"].Visible =
                            contextMenuStripExp.Items["toolStripMenuItemExpandDiff"].Visible = true;
                        contextMenuStripExp.Tag = e.Node;
                        e.Node.ContextMenuStrip = contextMenuStripExp;
                        contextMenuStripExp.Visible = true;
                        contextMenuStripExp.Refresh();
                    }
                }
            }
        }

        private void toolStripMenuItemExpandAll_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem cms = sender as ToolStripMenuItem;
            if (cms != null && contextMenuStripExp.Tag != null && contextMenuStripExp.Tag is TreeNode)
            {
                (contextMenuStripExp.Tag as TreeNode).ExpandAll();
                if (_topVis != null)
                    impactView.TopNode = _topVis;
            }
        }

        private void toolStripMenuItemExpandDiff_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem cms = sender as ToolStripMenuItem;
            if (cms != null && contextMenuStripExp.Tag != null && contextMenuStripExp.Tag is TreeNode)
            {
                (contextMenuStripExp.Tag as TreeNode).ExpandAll();
                SetExpand(contextMenuStripExp.Tag as TreeNode);
                if (_folders != null && _folders.Count > 0)
                    _folders.ForEach(n => SetExpand(n));
                if (_topVis != null)
                    impactView.TopNode = _topVis;
            }
        }

        private void toolStripMenuItemCollapse_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem cms = sender as ToolStripMenuItem;
            if (cms != null && contextMenuStripExp.Tag != null && contextMenuStripExp.Tag is TreeNode)
            {
                (contextMenuStripExp.Tag as TreeNode).Collapse();
                if (_topVis != null)
                    impactView.TopNode = _topVis;
            }
        }
    }
}
