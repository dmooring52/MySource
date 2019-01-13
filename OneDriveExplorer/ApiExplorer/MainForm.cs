// ------------------------------------------------------------------------------
// Copyright (c) 2014 Microsoft Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// ------------------------------------------------------------------------------

using LiveConnectDesktopSample;

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Microsoft.Live.Desktop.Samples.ApiExplorer
{

    public partial class MainForm : Form, IRefreshTokenHandler
    {
        // Update the ClientID with your app client Id that you created from https://account.live.com/developers/applications.
        private const string ClientID = "0000000048122D4E";
        private LiveAuthForm authForm;
        private LiveAuthClient liveAuthClient;
        private LiveConnectClient liveConnectClient;
        private RefreshTokenInfo refreshTokenInfo;

        public MainForm()
        {
            if (ClientID.Contains('%'))
            {
                throw new ArgumentException("Update the ClientID with your app client Id that you created from https://account.live.com/developers/applications.");
            }

            InitializeComponent();
            txtRoot.Text = "KaraokeMP4/Christmas";//"Karaoke/CDG_ZIP/Christmas";
            richTextStatus.ReadOnly = true;
        }

        private LiveAuthClient AuthClient
        {
            get
            {
                if (this.liveAuthClient == null)
                {
                    this.AuthClient = new LiveAuthClient(ClientID, this);
                }

                return this.liveAuthClient;
            }

            set
            {
                if (this.liveAuthClient != null)
                {
                    this.liveAuthClient.PropertyChanged -= this.liveAuthClient_PropertyChanged;
                }

                this.liveAuthClient = value;
                if (this.liveAuthClient != null)
                {
                    this.liveAuthClient.PropertyChanged += this.liveAuthClient_PropertyChanged;
                }

                this.liveConnectClient = null;
            }
        }

        private LiveConnectSession AuthSession
        {
            get
            {
                return this.AuthClient.Session;
            }
        }

        private void liveAuthClient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Session")
            {
                this.UpdateUIElements();
            }
        }

        private void UpdateUIElements()
        {
            LiveConnectSession session = this.AuthSession;
            bool isSignedIn = session != null;
            this.signOutButton.Enabled = isSignedIn;
            this.connectGroupBox.Enabled = isSignedIn;
            this.currentScopeTextBox.Text = isSignedIn ? string.Join(" ", session.Scopes) : string.Empty;
            if (!isSignedIn)
            {
                this.meNameLabel.Text = string.Empty;
                this.mePictureBox.Image = null;
            }
        }

        private void SigninButton_Click(object sender, EventArgs e)
        {
            if (this.authForm == null)
            {
                string startUrl = this.AuthClient.GetLoginUrl(this.GetAuthScopes());
                string endUrl = "https://login.live.com/oauth20_desktop.srf";
                this.authForm = new LiveAuthForm(
                    startUrl,
                    endUrl,
                    this.OnAuthCompleted);
                this.authForm.FormClosed += AuthForm_FormClosed;
                this.authForm.ShowDialog(this);
            }
        }

        private string[] GetAuthScopes()
        {
            string[] scopes = new string[] { "wl.signin" };
            return scopes;
        }

        void AuthForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.CleanupAuthForm();
        }

        private void CleanupAuthForm()
        {
            if (this.authForm != null)
            {
                this.authForm.Dispose();
                this.authForm = null;
            }
        }

        private async void OnAuthCompleted(AuthResult result)
        {
            this.CleanupAuthForm();
            if (result.AuthorizeCode != null)
            {
                try
                {
                    LiveConnectSession session = await this.AuthClient.ExchangeAuthCodeAsync(result.AuthorizeCode);
                    this.liveConnectClient = new LiveConnectClient(session);
                    LiveOperationResult meRs = await this.liveConnectClient.GetAsync("me");
                    dynamic meData = meRs.Result;
                    this.meNameLabel.Text = meData.name;

                    LiveDownloadOperationResult meImgResult = await this.liveConnectClient.DownloadAsync("me/picture");
                    this.mePictureBox.Image = Image.FromStream(meImgResult.Stream);
                }
                catch (LiveAuthException aex)
                {
                    MessageBox.Show("Failed to retrieve access token. Error: " + aex.Message);
                }
                catch (LiveConnectException cex)
                {
                    MessageBox.Show("Failed to retrieve the user's data. Error: " + cex.Message);
                }
            }
            else
            {
                MessageBox.Show(string.Format("Error received. Error: {0} Detail: {1}", result.ErrorCode, result.ErrorDescription));
            }
        }

        private void SignOutButton_Click(object sender, EventArgs e)
        {
            this.signOutWebBrowser.Navigate(this.AuthClient.GetLogoutUrl());
            this.AuthClient = null;
            this.UpdateUIElements();
        }

        private async Task<LiveOperationResult> UploadFile(string path)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            Stream stream = null;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                throw new InvalidOperationException("No file is picked to upload.");
            }
            try
            {
                if ((stream = dialog.OpenFile()) == null)
                {
                    throw new Exception("Unable to open the file selected to upload.");
                }

                using (stream)
                {
                    return await this.liveConnectClient.UploadAsync(path, dialog.SafeFileName, stream, OverwriteOption.DoNotOverwrite);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task DownloadFile(string path)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            Stream stream = null;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                throw new InvalidOperationException("No file is picked to upload.");
            }
            try
            {
                if ((stream = dialog.OpenFile()) == null)
                {
                    throw new Exception("Unable to open the file selected to upload.");
                }

                using (stream)
                {
                    LiveDownloadOperationResult result = await this.liveConnectClient.DownloadAsync(path);
                    if (result.Stream != null)
                    {
                        using (result.Stream)
                        {
                            await result.Stream.CopyToAsync(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LiveLoginResult loginResult = await this.AuthClient.InitializeAsync();
                if (loginResult.Session != null)
                {
                    this.liveConnectClient = new LiveConnectClient(loginResult.Session);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Received an error during initializing. " + ex.Message);
            }
        }
        private void MainForm_ClientSizeChange(object sender, EventArgs e)
        {
            this.connectGroupBox.SetBounds(
                this.connectGroupBox.Bounds.X,
                this.connectGroupBox.Bounds.Y,
                Width - 43 > 0 ? Width - 43 : 658,
                Height - 200 > 0 ? Height - 200 : 394);
        }

        Task IRefreshTokenHandler.SaveRefreshTokenAsync(RefreshTokenInfo tokenInfo)
        {
            // Note: 
            // 1) In order to receive refresh token, wl.offline_access scope is needed.
            // 2) Alternatively, we can persist the refresh token.
            return Task.Factory.StartNew(() =>
            {
                this.refreshTokenInfo = tokenInfo;
            });
        }

        Task<RefreshTokenInfo> IRefreshTokenHandler.RetrieveRefreshTokenAsync()
        {
            return Task.Factory.StartNew<RefreshTokenInfo>(() =>
            {
                return this.refreshTokenInfo;
            });
        }

        private async void btnCreateMap_Click(object sender, EventArgs e)
        {
            if (this.liveConnectClient == null)
            {
                SigninButton_Click(sender, e);
                return;
            }
            signinButton.Enabled = false;
            signOutButton.Enabled = false;
            btnCreateMap.Enabled = false;
            txtRoot.Enabled = false;
            StringBuilder sb = new StringBuilder();
            string status = "";
            sb.AppendLine(String.Format("Started: {0}", DateTime.Now.ToShortTimeString()));

            richTextStatus.Text = sb.ToString();
            richTextStatus.Refresh();

            if (txtRoot.Text.Trim().Length == 0)
            {
                MessageBox.Show("The root path is the relative path of the top folder to map\n Example: Karaoke/CDG_ZIP/Christmas", "Enter root path in the text box");
                signinButton.Enabled = true;
                signOutButton.Enabled = true;
                btnCreateMap.Enabled = true;
                txtRoot.Enabled = true;
                return;
            }
            string rootpath = txtRoot.Text.Trim();
            string[] rootpaths = rootpath.Split('/');
            rootpath = "me/skydrive";
            OneDriveFolder rootfolder = null;
            foreach (string root in rootpaths)
            {
                sb.AppendLine(String.Format("Finding root: {0}", root));
                richTextStatus.Text = sb.ToString();
                richTextStatus.Refresh();
                LiveOperationResult rootresult = null;
                List<OneDriveFolder> rootfolders = new List<OneDriveFolder>();
                try
                {
                    rootresult = await this.liveConnectClient.GetAsync(string.Format("{0}/files", rootpath));
                    if (rootresult != null && rootresult.Result != null && rootresult.Result["data"] != null)
                    {
                        foreach (KeyValuePair<string, object> o in rootresult.Result)
                        {
                            if (o.Key == "data")
                            {
                                if (o.Value != null && o.Value is List<object>)
                                {
                                    foreach (object obj in o.Value as List<object>)
                                    {
                                        string id = "";
                                        string name = "";
                                        string type = "";
                                        string link = "";
                                        string source = "";
                                        foreach (KeyValuePair<string, object> kvp in obj as IDictionary<string, object>)
                                        {
                                            switch (kvp.Key)
                                            {
                                                case "id":
                                                    id = kvp.Value.ToString();
                                                    break;
                                                case "name":
                                                    name = kvp.Value.ToString();
                                                    break;
                                                case "type":
                                                    type = kvp.Value.ToString();
                                                    break;
                                                case "link":
                                                    link = kvp.Value.ToString();
                                                    break;
                                                case "source":
                                                    source = kvp.Value.ToString();
                                                    break;
                                            }
                                        }
                                        if (type == "folder")
                                        {
                                            rootfolders.Add(new OneDriveFolder("root", id, name, link));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                bool found = false;
                foreach (OneDriveFolder f in rootfolders)
                {
                    if (f.name == root)
                    {
                        rootfolder = f;
                        rootpath = f.id;
                        found = true;
                        break;
                    }
                }
                if (found == false)
                    rootfolder = null;
            }

            if (rootfolder == null)
            {
                MessageBox.Show("Root Folder Not Found: " + txtRoot.Text, "Root not found");
                return;
            }

            //folder.a645bfc91d3b795a.A645BFC91D3B795A!41650/files //Sample path (using id)
            //https://onedrive.live.com/redir?resid=A645BFC91D3B795A!41650&authkey=!ABx2Nb-uPkoa7RA&ithint=folder%2ctxt //Sample link
            List<OneDriveFolder> folders = new List<OneDriveFolder>();
            folders.Add(rootfolder);
            int ix = 0;
            try
            {
                while (ix < folders.Count)
                {
                    LiveOperationResult result = await this.liveConnectClient.GetAsync(string.Format("{0}/files", folders[ix].id));

                    if (result != null && result.Result != null && result.Result["data"] != null)
                    {
                        foreach (KeyValuePair<string, object> o in result.Result)
                        {
                            if (o.Key == "data")
                            {
                                if (o.Value != null && o.Value is List<object>)
                                {
                                    foreach (object obj in o.Value as List<object>)
                                    {
                                        string id = "";
                                        string name = "";
                                        string type = "";
                                        string link = "";
                                        string source = "";
                                        foreach (KeyValuePair<string, object> kvp in obj as IDictionary<string, object>)
                                        {
                                            switch (kvp.Key)
                                            {
                                                case "id":
                                                    id = kvp.Value.ToString();
                                                    break;
                                                case "name":
                                                    name = kvp.Value.ToString();
                                                    break;
                                                case "type":
                                                    type = kvp.Value.ToString();
                                                    break;
                                                case "link":
                                                    link = kvp.Value.ToString();
                                                    break;
                                                case "source":
                                                    source = kvp.Value.ToString();
                                                    break;
                                            }
                                        }
                                        if (type == "folder")
                                        {
                                            folders.Add(new OneDriveFolder(folders[ix].id, id, name, link));
                                            status = String.Format("\nFolder: {0}\n", name);
                                            richTextStatus.Text = sb.ToString() + status;
                                            richTextStatus.Refresh();
                                        }
                                        else if (type == "file" || type == "video")
                                        {
                                            status = String.Format("\nFile: {0}\n", name);
                                            richTextStatus.Text = sb.ToString() + status;
                                            richTextStatus.Refresh();
                                            OneDriveFile odf = new OneDriveFile(folders[ix].id, id, name, link, source);
                                            if (odf.name.ToLower().Contains(".mp4"))
                                                folders[ix].files.Add(odf);
                                            /*
                                            LiveOperationResult resultlink = null;
                                            for (int i = 0; i < 3; i++)
                                            {
                                                try
                                                {
                                                    resultlink = await this.liveConnectClient.GetAsync(string.Format("{0}/shared_read_link", folders[ix].id));
                                                    if (resultlink != null && resultlink.RawResult != null && resultlink.RawResult.Length > 0)
                                                        break;
                                                    else
                                                        Thread.Sleep(5000);
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message, "Navigation failed - will try to continue");
                                                    Thread.Sleep(5000);
                                                }
                                            }
                                            if (resultlink != null && resultlink.RawResult != null && resultlink.RawResult.Length > 0)
                                            {
                                                string ilink = "link\": ";
                                                string raw = resultlink.RawResult.Trim();
                                                int iof = raw.IndexOf(ilink);
                                                if (iof > 0)
                                                {
                                                    iof += ilink.Length + 1;
                                                    int end = raw.LastIndexOf('"');
                                                    if (end > 0 && end > iof)
                                                    {
                                                        odf.link = raw.Substring(iof, end - iof);
                                                        folders[ix].files.Add(odf);
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Operation will continue", "Unexpected start/end result");
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Operation will continue", "Unexpected parsing result");
                                                }
                                            }
                                            */
                                        }
                                        else
                                        {
                                            MessageBox.Show("Unknow file type: " + type, "Unexpected file type");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ix++;
                }
                if (folders != null && folders.Count > 0 && folders[0].name != null && folders[0].name.Trim().Length > 0)
                {
                    sb.AppendLine(String.Format("Writing: {0}", string.Format(@"C:\Temp\{0}.txt", folders[0].name)));
                    richTextStatus.Text = sb.ToString();
                    richTextStatus.Refresh();
                    StreamWriter sw = File.CreateText(string.Format(@"C:\Temp\{0}.txt", folders[0].name));
                    int nfiles = 0;
                    foreach (OneDriveFolder folder in folders)
                    {
                        string path = GetFolderPath(folder, folders);
                        foreach (OneDriveFile file in folder.files)
                        {
                            if (file.name.Trim().EndsWith(".mp4", StringComparison.CurrentCultureIgnoreCase) == true)
                            {
                                sw.WriteLine(string.Format("{0}\t{1}", GetPath(path, file), file.link));
                                nfiles++;
                            }
                        }
                    }
                    /*
                    sw.WriteLine();
                    sw.WriteLine("----");
                    sw.WriteLine();
                    int nfiles = 0;
                    foreach (OneDriveFolder folder in folders)
                    {
                        string path = GetFolderPath(folder, folders);
                        sw.WriteLine(string.Format("{0}", path));
                        foreach (OneDriveFile file in folder.files)
                        {
                            if (file.name.Trim().EndsWith(".mp4", StringComparison.CurrentCultureIgnoreCase) == true)
                            {
                                sw.WriteLine(string.Format("\t{0}\t{1}", GetPath(path, file), file.link));
                                nfiles++;
                            }
                        }
                    }
                    */
                    sw.Close();
                    sb.AppendLine();
                    sb.AppendLine(String.Format("Folders: {0}", string.Format(@"C:\Temp\{0}.txt", folders.Count)));
                    sb.AppendLine(String.Format("Files  : {0}", string.Format(@"C:\Temp\{0}.txt", nfiles)));
                    sb.AppendLine();
                    sb.AppendLine(String.Format("Write completed: {0}", string.Format(@"C:\Temp\{0}.txt", folders[0].name)));
                    richTextStatus.Text = sb.ToString();
                    richTextStatus.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Received an error. " + ex.Message);
            }
            finally
            {
                signinButton.Enabled = true;
                signOutButton.Enabled = true;
                btnCreateMap.Enabled = true;
                txtRoot.Enabled = true;
            }
        }
        /*
        public string GetPath(OneDriveFile file, List<OneDriveFolder> folders)
        {
            string path = file.name;
            string top = folders[0].id;
            OneDriveFolder searchfolder = GetFolder(file.parent, folders);
            while (searchfolder != null)
            {
                path = searchfolder.name + "/" + path;
                searchfolder = GetFolder(searchfolder.parent, folders);
            }
            return "$/" + path;
        }
        */
        public string GetPath(string path, OneDriveFile file)
        {
            return path + "/" + file.name;
        }
        public string GetFolderPath(OneDriveFolder folder, List<OneDriveFolder> folders)
        {
            string path = "";
            string top = folders[0].id;
            OneDriveFolder searchfolder = folder;
            while (searchfolder != null)
            {
                path = searchfolder.name + "/" + path;
                searchfolder = GetFolder(searchfolder.parent, folders);
            }
            return "$/" + path;
        }
        public OneDriveFolder GetFolder(string id, List<OneDriveFolder> folders)
        {
            foreach (OneDriveFolder folder in folders)
                if (folder.id == id)
                    return folder;
            return null;
        }
    }

    public class OneDriveFile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string source { get; set; }
        public string parent { get; set; }
        public OneDriveFile(string _parent, string _id, string _name, string _link, string _source)
        {
            parent = _parent;
            id = _id;
            name = _name;
            link = _link;
            source = _source;
        }
    }
    public class OneDriveFolder
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string parent { get; set; }

        private List<OneDriveFile> _files = new List<OneDriveFile>();
        public List<OneDriveFile> files { get { return _files; } }
        public OneDriveFolder(string _parent, string _id, string _name, string _link)
        {
            parent = _parent;
            id = _id;
            name = _name;
            link = _link;
        }
    }
}
