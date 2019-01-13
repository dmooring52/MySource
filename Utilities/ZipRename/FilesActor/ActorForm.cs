using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace FilesActor
{
    public partial class ActorForm : Form
    {
        private string root = @"C:\Karaoke\CarolBackup\Carol\";
        private string fout = @"C:\Temp\output.txt";
        private const string zipr = @"C:\Program Files (x86)\7-zip\7z.exe";
        private TreeForm tf = null;
        public ActorForm()
        {
            InitializeComponent();
            txtMaster.Text = root;
            txtOutput.Text = fout;

            btnRun.Enabled = true;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //btnRun.Enabled = false;
            try
            {
                root = txtMaster.Text.Trim();
                fout = txtOutput.Text.Trim();
                if (root != null && root.Length > 0 && Directory.Exists(txtMaster.Text) == true)
                {
                    root = root.Replace('/', '\\').Trim('\\') + "\\";
                    List<TreeNode> foldersMaster = new List<TreeNode>();
                    List<TreeNode> leafMaster = new List<TreeNode>();

                    GetTree(root, foldersMaster, leafMaster, Color.DarkGreen);

                    if (foldersMaster.Count > 0 && leafMaster.Count > 0)
                    {
                        leafMaster.ForEach(n => ProcessZip(n));
                        tf = new TreeForm();
                        tf.tv.Nodes.Add(foldersMaster[0]);
                    }
                    else
                    {
                        MessageBox.Show("No folders found", "Not found");
                        return;
                    }

                    if (fout != null && fout.Trim().Length > 0)
                    {
                        try
                        {
                            StreamWriter sw = File.CreateText(fout);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception occurred");
            }
        }

        private bool ProcessZip(TreeNode tn)
        {
            bool found = false;
            if (tn != null && tn.Tag != null && tn.Tag is ActorFile)
            {
                ActorFile af = tn.Tag as ActorFile;
                using (ZipArchive za = ZipFile.Open(af.filePath, ZipArchiveMode.Update))
                {
                    if (za.Entries != null && za.Entries.Count == 2)
                    {
                        List<ZipArchiveEntry> dentries = new List<ZipArchiveEntry>();
                        List<string> files = new List<string>();
                        foreach (ZipArchiveEntry entry in za.Entries)
                        {
                            if (string.Compare(Path.GetFileNameWithoutExtension(af.fileName), Path.GetFileNameWithoutExtension(entry.Name), true) > 0)
                            {
                                string ext = Path.GetExtension(entry.Name);
                                string file = Path.GetFileNameWithoutExtension(af.fileName);
                                string path = Path.Combine(Path.GetDirectoryName(af.filePath), file + ext);
                                entry.ExtractToFile(path);
                                dentries.Add(entry);
                                files.Add(path);
                            }
                        }
                        if (files.Count == 2 && dentries.Count == 2)
                        {
                            dentries[0].Delete();
                            dentries[1].Delete();
                            za.CreateEntryFromFile(files[0], Path.GetFileName(files[0]));
                            za.CreateEntryFromFile(files[1], Path.GetFileName(files[1]));
                            File.Delete(files[0]);
                            File.Delete(files[1]);
                            tn.ForeColor = Color.DarkRed;
                        }
                        else
                            tn.ForeColor = Color.DarkBlue;
                        found = true;
                    }
                }
            }
            return found;
        }

        private void GetTree(string troot, List<TreeNode> folders, List<TreeNode> leafs, Color color)
        {
            ActorFile ad = new ActorFile(troot, troot, false);
            TreeNode tn = new TreeNode(ad.fileName);
            tn.ForeColor = color;
            tn.Tag = ad;
            folders.Add(tn);
            int ix = 0;
            AddFiles(troot, tn, leafs, color);
            while (ix < folders.Count)
            {
                string[] dirs = Directory.GetDirectories((folders[ix].Tag as ActorFile).filePath);
                if (dirs.Length > 0)
                    foreach (string dir in dirs)
                    {
                        ad = new ActorFile(troot, dir, false);
                        tn = new TreeNode(ad.fileName);
                        tn.ForeColor = color;
                        tn.Tag = ad;
                        folders.Add(tn);
                        folders[ix].Nodes.Add(tn);
                        AddFiles(troot, tn, leafs, color);
                    }
                ix++;
            }
        }

        private void AddFiles(string troot, TreeNode tn, List<TreeNode> leafs, Color color)
        {
            var files = Directory.EnumerateFiles((tn.Tag as ActorFile).filePath, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(s => s.EndsWith(".zip"));
            foreach (var file in files)
            {
                ActorFile af = new ActorFile(troot, file.ToString(), true);
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
