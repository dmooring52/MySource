using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FilesActor
{
    public partial class ActorForm : Form
    {
        private string root = @"C:\Users\dmooring\OneDrive\Karaoke\Karaoke Burn List\Duane\";
        private string dest = @"H:\Karaoke\Duane\";
        private string fout = @"C:\Temp\output.txt";
        private const string zipr = @"C:\Program Files (x86)\7-zip\7z.exe";
        private TreeForm tf = null;
        private Dictionary<string, string> remdirs = null;
        private Dictionary<string, string> adddirs = null;
        private Dictionary<string, string> remlfs = null;
        private Dictionary<string, string> addlfs = null;
        public ActorForm()
        {
            InitializeComponent();
            txtMaster.Text = root;
            txtTarget.Text = dest;
            txtOutput.Text = fout;

            btnRun.Enabled = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //btnRun.Enabled = false;
            try
            {
                remdirs = new Dictionary<string, string>();
                adddirs = new Dictionary<string, string>();
                remlfs = new Dictionary<string, string>();
                addlfs = new Dictionary<string, string>();
                root = txtMaster.Text.Trim();
                dest = txtTarget.Text.Trim();
                fout = txtOutput.Text.Trim();
                if (root != null && root.Length > 0 && Directory.Exists(txtMaster.Text) == true)
                {
                    if (dest != null && dest.Length > 0 && Path.IsPathRooted(dest) == true)
                    {
                        root = root.Replace('/', '\\').Trim('\\') + "\\";
                        dest = dest.Replace('/', '\\').Trim('\\') + "\\";
                        if (Directory.Exists(dest) == false)
                        {
                            DialogResult result = MessageBox.Show("Create folder?", "Folder not found", MessageBoxButtons.YesNo);
                            if (result != System.Windows.Forms.DialogResult.Yes)
                                return;
                            Directory.CreateDirectory(dest);
                            if (Directory.Exists(dest) == false)
                            {
                                MessageBox.Show("Could not create folder: " + dest, "Error creating folder");
                                return;
                            }
                        }
                        List<TreeNode> foldersMaster = new List<TreeNode>();
                        List<TreeNode> foldersTarget = new List<TreeNode>();
                        List<TreeNode> leafMaster = new List<TreeNode>();
                        List<TreeNode> leafTarget = new List<TreeNode>();

                        GetTree(root, dest, foldersMaster, leafMaster, Color.DarkGreen);
                        GetTree(dest, root, foldersTarget, leafTarget, Color.Red);

                        if (foldersMaster.Count > 0)
                        {
                            tf = new TreeForm();
                            tf.tv.Nodes.Add(foldersMaster[0]);
                        }
                        else
                        {
                            MessageBox.Show("No folders found", "Not found");
                            return;
                        }

                        var unmatchedfoldersTarget = foldersTarget.Where(n => unmatched(n, foldersMaster, Color.Black));
                        int iunmatchedfoldersTarget = unmatchedfoldersTarget.Count();
                        var unmatchedfoldersMaster = foldersMaster.Where(n => unmatched(n, foldersTarget, Color.Black));
                        int iunmatchedfoldersMaster = unmatchedfoldersMaster.Count();
                        var unmatchedleafMaster = leafMaster.Where(n => unmatched(n, leafTarget, Color.Black));
                        int iunmatchedleafMaster = unmatchedleafMaster.Count();
                        var unmatchedleafTarget = leafTarget.Where(n => unmatched(n, leafMaster, Color.Black));
                        int iunmatchedleafTarget = unmatchedleafTarget.Count();

                        foreach (TreeNode tn in unmatchedfoldersMaster)
                            AddCreateCommand(tn, foldersTarget);
                        foreach (TreeNode tn in unmatchedleafMaster)
                            AddCreateCommand(tn, foldersTarget);
                        foreach (TreeNode tn in unmatchedleafTarget)
                            SpliceDestNode(tn, foldersMaster, leafMaster);
                        foreach (TreeNode tn in unmatchedfoldersTarget)
                            SpliceDestNode(tn, foldersMaster, leafMaster);

                        if (fout != null && fout.Trim().Length > 0)
                        {
                            try
                            {
                                StreamWriter sw = File.CreateText(fout);
                                var sortedDict = from entry in adddirs orderby entry.Key ascending select entry;
                                foreach (KeyValuePair<string, string> kvp in sortedDict)
                                    sw.WriteLine(kvp.Value);
                                foreach (KeyValuePair<string, string> kvp in addlfs)
                                    sw.WriteLine(kvp.Value);
                                foreach (KeyValuePair<string, string> kvp in remlfs)
                                    sw.WriteLine(kvp.Value);
                                sortedDict = from entry in remdirs orderby entry.Key descending select entry;
                                foreach (KeyValuePair<string, string> kvp in sortedDict)
                                    sw.WriteLine(kvp.Value);
                                sw.Close();
                            }
                            catch {}
                        }

                        tf.tv.TopNode = foldersMaster[0];
                        tf.Setup(foldersMaster, root);
                        tf.ShowDialog(this);
                    }
                    else
                        MessageBox.Show("Folder Target not specified", "Folder not found");
                }
                else
                    MessageBox.Show("Folder Master must be an existing Master folder", "Folder not found");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred");
            }
        }

        private void WriteLine(string s, StreamWriter sw)
        {

        }

        private void AddCreateCommand(TreeNode node, List<TreeNode> list)
        {
            try
            {
                if (node != null && node.Tag != null && node.Tag is ActorFile && list != null && list.Count > 0 && list[0].Tag != null && list[0].Tag is ActorFile)
                {
                    ActorFile createFile = node.Tag as ActorFile;
                    ActorFile referFile = list[0].Tag as ActorFile;
                    if (createFile != null && referFile != null)
                    {
                        if (createFile.leaf == false)
                        {
                            string destfile = Path.Combine(referFile.basePath, createFile.relativePath);
                            adddirs.Add(destfile, string.Format("if not exist \"{0}\" mkdir \"{0}\"", destfile));
                        }
                        else
                        {
                            string destfile = Path.Combine(referFile.basePath, createFile.relativePath);
                            addlfs.Add(destfile, string.Format("if not exist \"{1}\" copy \"{0}\" \"{1}\"", createFile.filePath, destfile));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred: AddCreateCommand");
            }
        }
        private void AddRemoveCommand(ActorFile delFile)
        {
            try
            { 
                if (delFile != null)
                {
                    if (delFile.leaf == false)
                        remdirs.Add(delFile.filePath, string.Format("if exist \"{0}\" rmdir \"{0}\"", delFile.filePath));
                    else
                        remlfs.Add(delFile.filePath, string.Format("if exist \"{0}\" del \"{0}\"", delFile.filePath));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred: AddRemoveCommand");
            }
        }
        private void SpliceDestNode(TreeNode node, List<TreeNode> folders, List<TreeNode> leafs)
        {
            try
            {
                if (node.Tag != null && node.Tag is ActorFile)
                {
                    ActorFile af = node.Tag as ActorFile;
                    if (af != null)
                    {
                        List<TreeNode> descendents = new List<TreeNode>();
                        descendents.Add(node);
                        TreeNode parent = node.Parent;
                        while (parent != null)
                        {
                            descendents.Add(parent);
                            parent = parent.Parent;
                        }
                        descendents.Reverse();
                        TreeNode last = null;
                        List<TreeNode> matchednodes = new List<TreeNode>();
                        foreach (TreeNode descendent in descendents)
                        {
                            TreeNode matchNode = MatchNode(descendent, folders, leafs);
                            matchednodes.Add(matchNode);
                            if (matchNode != null)
                            {
                                last = descendent;
                            }
                        }
                        bool goon = false;
                        int i = 0;
                        foreach (TreeNode descendent in descendents)
                        {
                            if (goon && i > 0)
                            {
                                TreeNode tnode = descendent;
                                TreeNode mnode = matchednodes[i - 1];
                                if (mnode == null)
                                    mnode = last;
                                ActorFile mact = null;
                                ActorFile tact = tnode.Tag as ActorFile;
                                if (mnode != null)
                                    mact = mnode.Tag as ActorFile;
                                TreeNode newnode = new TreeNode(tact.fileName);
                                newnode.Tag = tact;
                                newnode.ForeColor = tnode.ForeColor;
                                if (mnode != null)
                                {
                                    mnode.Nodes.Add(newnode);
                                    if (tact.leaf == false)
                                        folders.Add(newnode);
                                }
                                if (chkSuperset.Checked == false)
                                {
                                    newnode.NodeFont = new Font(tf.tv.Font, FontStyle.Strikeout);
                                    AddRemoveCommand(tact);
                                }
                                else
                                {
                                    if (af.leaf == true)
                                        AddCreateCommand(node, leafs);
                                    else
                                        AddCreateCommand(node, folders);
                                }
                            }
                            if (descendent == last)
                            {
                                goon = true;
                                last = descendent;
                            }
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred: CheckNode");
            }
        }

        private TreeNode MatchNode(TreeNode node, List<TreeNode> folders, List<TreeNode> leafs)
        {
            if (node != null && node.Tag != null && node.Tag is ActorFile)
            {
                ActorFile tf = node.Tag as ActorFile;
                TreeNode tn = null;
                if (tf.leaf == true)
                    tn = matched(tf, leafs);
                else
                    tn = matched(tf, folders);
                if (tn != null && tn.Tag != null && tn.Tag is ActorFile)
                    return tn;
            }
            return null;
        }

        private TreeNode matched(ActorFile af, List<TreeNode> list)
        {
            var masters = list.Where(n => ismatch(n.Tag as ActorFile, af) == true);
            if (masters.Count() == 1)
            {
                foreach (TreeNode tn in masters)
                {
                    if (tn.Tag != null && tn.Tag is ActorFile)
                        return tn;
                }
            }
            return null;
        }
        private bool ismatch(ActorFile a1, ActorFile a2)
        {
            if (a1 != null && a2 != null && a1.relativePath == a2.relativePath)
                return true;
            return false;
        }

        private void GetTree(string troot, string tdest, List<TreeNode> folders, List<TreeNode> leafs, Color color)
        {
            ActorFile ad = new ActorFile(troot, tdest, troot, false);
            TreeNode tn = new TreeNode(ad.fileName);
            tn.ForeColor = color;
            tn.Tag = ad;
            folders.Add(tn);
            int ix = 0;
            AddFiles(troot, tdest, tn, leafs, color);
            while (ix < folders.Count)
            {
                string[] dirs = Directory.GetDirectories((folders[ix].Tag as ActorFile).filePath);
                if (dirs.Length > 0)
                    foreach (string dir in dirs)
                    {
                        ad = new ActorFile(troot, tdest, dir, false);
                        tn = new TreeNode(ad.fileName);
                        tn.ForeColor = color;
                        tn.Tag = ad;
                        folders.Add(tn);
                        folders[ix].Nodes.Add(tn);
                        AddFiles(troot, tdest, tn, leafs, color);
                    }
                ix++;
            }
        }
        private bool unmatched(TreeNode a, List<TreeNode> target, Color color)
        {
            ActorFile af = a.Tag as ActorFile;
            var m = target.Where(n => af.Matches(n.Tag));
            if (m.Count() > 0)
            {
                a.ForeColor = color;
                return false;
            }
            return true;
        }

        private void AddFiles(string troot, string tdest, TreeNode tn, List<TreeNode> leafs, Color color)
        {
            var files = Directory.EnumerateFiles((tn.Tag as ActorFile).filePath, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(s => s.EndsWith(".mp3") || s.EndsWith(".cdg") || s.EndsWith(".zip"));
            foreach (var file in files)
            {
                ActorFile af = new ActorFile(troot, tdest, file.ToString(), true);
                TreeNode child = new TreeNode(af.fileName);
                child.ForeColor = color;
                child.Tag = af;
                tn.Nodes.Add(child);
                leafs.Add(child);
            }
        }

        private void chkSuperset_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
