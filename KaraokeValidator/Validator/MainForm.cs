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

using LiveConnectValidator;

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

using Microsoft.Win32;
using System.Windows.Forms;

using KaraokeValidator;

namespace Microsoft.Live.Validator
{
    public delegate void runtimer();
    public partial class MainForm : Form, IRefreshTokenHandler
    {
        // Update the ClientID with your app client Id that you created from https://account.live.com/developers/applications.
        private const string ClientID = "0000000048122D4E";
        private LiveAuthForm authForm;
        private LiveAuthClient liveAuthClient;
        private LiveConnectClient liveConnectClient;
        private RefreshTokenInfo refreshTokenInfo;
        private Dictionary<string, string> parents = null;
        private string OneDriveRoot = "";
        private string OneDriveError = "";
        private List<OneDriveItem> onedriveitems = null;
        private SpreadsheetValidator sv = null;
        public StringBuilder sb = new StringBuilder();
        private bool blink;
        private bool bzip;
        private bool bfiles;
        private bool bstop;

        public MainForm()
        {
            if (ClientID.Contains('%'))
            {
                throw new ArgumentException("Update the ClientID with your app client Id that you created from https://account.live.com/developers/applications.");
            }

            InitializeComponent();
            txtExcel.Text = "sl.xlsm";
            richTextStatus.ReadOnly = true;
            SyncTimer.Interval = 30000;
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
            FindOneDriveRoot();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SyncTimer.Stop();
            if (sv != null)
                sv.Wrapup();
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

        private async void ReadyCheck()
        {
            if (this.liveConnectClient == null)
            {
                SigninButton_Click(this, new EventArgs());
                return;
            }
            SetStateRunning();
            parents = new Dictionary<string, string>();
            sb.Clear();
            sb.AppendLine(String.Format("Started: {0}", DateTime.Now.ToShortTimeString()));

            richTextStatus.Text = sb.ToString();
            richTextStatus.Refresh();

            try
            {
                string excelfile = string.Format(@"{0}\Karaoke\CDG_MP3\Index\{1}", txtOneDriveRoot.Text.Trim(), txtExcel.Text.Trim());
                if (txtExcel.Text.Trim().Length == 0 || txtOneDriveRoot.Text.Trim().Length == 0 || File.Exists(excelfile) == false)
                {
                    MessageBox.Show(string.Format("Excel document not found\n{0}", excelfile), "Path not found");
                    SetStateReady();
                    return;
                }
                LiveOperationResult rootresult = await this.liveConnectClient.GetAsync("me/skydrive");
                if (rootresult == null || rootresult.Result == null)
                {
                    MessageBox.Show("OneDrive root not found", "Path not found");
                    SetStateReady();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected error");
                SetStateReady();
                return;
            }
            finally
            {
            }
            SetStateReady();
            SyncTimer.Stop();
            if (bfiles == true)
                SetTimer(ValidateFiles);
            else
                SetTimer(ValidateExcel);
        }

        private void ValidateFiles()
        {
            SyncTimer.Stop();
            SetStateRunning();
            string excelfile = string.Format(@"{0}\Karaoke\CDG_MP3\Index\{1}", txtOneDriveRoot.Text.Trim(), txtExcel.Text.Trim());
            if (File.Exists(excelfile) == true)
            {
                try
                {
                    onedriveitems = new List<OneDriveItem>();
                    sv = new SpreadsheetValidator(excelfile, blink);
                    List<string> cdgfiles = new List<string>();
                    List<string> zipfiles = new List<string>();

                    int row = 2;
                    int nrows = sv.nCnt;
                    while (row < nrows)
                    {
                        Application.DoEvents();
                        if (bstop == true)
                        {
                            bstop = false;
                            sb.AppendLine("Stopped by user");
                            richTextStatus.Text = sb.ToString();
                            richTextStatus.Refresh();
                            SetStateReady();
                            return;
                        }
                        OneDriveItem cellpath = sv.Run(row);
                        if (cellpath != null && cellpath.Path != null && cellpath.Path.Trim().Length > 0)
                        {
                            cdgfiles.Add(cellpath.PathCDG.ToLower());
                            zipfiles.Add(cellpath.PathZIP.ToLower());
                        }
                        richTextStatus.Text = sb.ToString() + "\n\n" + sv.status;
                        row++;
                    }
                    if (bzip == true)
                    {
                        string rootfile = string.Format(@"{0}\Karaoke\CDG_ZIP", txtOneDriveRoot.Text.Trim());
                        string[] files = Directory.GetFiles(rootfile, "*.zip", SearchOption.AllDirectories);
                        var missingzips = files.Where(n => zipfiles.Contains(n.ToLower()) == false);
                        foreach (string file in missingzips)
                            sb.AppendLine(file);
                    }
                    else
                    {
                        string rootfile = string.Format(@"{0}\Karaoke\CDG_MP3", txtOneDriveRoot.Text.Trim());
                        string[] files = Directory.GetFiles(rootfile, "*.cdg", SearchOption.AllDirectories);
                        var missingcdgs = files.Where(n => cdgfiles.Contains(n.ToLower()) == false);
                        foreach (string file in missingcdgs)
                            sb.AppendLine(file);
                    }
                    richTextStatus.Text = sb.ToString();
                    richTextStatus.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unexpected error");
                }
            }
            SetStateReady();
            SetTimer(WrapUp);
        }
        private void ValidateExcel()
        {
            SyncTimer.Stop();
            SetStateRunning();
            string excelfile = string.Format(@"{0}\Karaoke\CDG_MP3\Index\{1}", txtOneDriveRoot.Text.Trim(), txtExcel.Text.Trim());
            if (File.Exists(excelfile) == true)
            {
                try
                {
                    onedriveitems = new List<OneDriveItem>();
                    sv = new SpreadsheetValidator(excelfile, blink);

                    int row = 2;
                    int nrows = sv.nCnt;
                    while (row < nrows)
                    {
                        Application.DoEvents();
                        if (bstop == true)
                        {
                            bstop = false;
                            sb.AppendLine("Stopped by user");
                            richTextStatus.Text = sb.ToString();
                            richTextStatus.Refresh();
                            SetStateReady();
                            return;
                        }
                        OneDriveItem cellpath = sv.Run(row);
                        if (blink == true)
                            if (cellpath != null && cellpath.Path != null && cellpath.Path.Trim().Length > 0)
                                onedriveitems.Add(cellpath);
                        richTextStatus.Text = sb.ToString() + "\n\n" + sv.status;
                        row++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unexpected error");
                }
            }
            SetStateReady();
            if (blink == false)
                SetTimer(WrapUp);
            else
                SetTimer(SetOneDriveLinks);
        }

        private void SetStateRunning()
        {
            signinButton.Enabled = false;
            signOutButton.Enabled = false;
            btnValidateMP3.Enabled = false;
            //btnValidateZip.Enabled = false;
            btnValidateMP3Files.Enabled = false;
            btnValidateZipFiles.Enabled = false;
            btnZIPLink.Enabled = false;
            txtExcel.Enabled = false;
            btnStop.Enabled = true;
        }

        private void SetStateReady()
        {
            signinButton.Enabled = liveConnectClient != null;
            signOutButton.Enabled = liveConnectClient != null;
            btnValidateMP3.Enabled = true;
            //btnValidateZip.Enabled = true;
            btnValidateMP3Files.Enabled = true;
            btnValidateZipFiles.Enabled = true;
            btnZIPLink.Enabled = true;
            txtExcel.Enabled = true;
            btnStop.Enabled = false;
        }

        private void WrapUp()
        {
            SyncTimer.Stop();
            SetStateRunning();
            if (sv != null)
            {
                sv.Wrapup();
                sb.AppendLine();
                sb.AppendLine("Rows: " + sv.Rows);
                sb.AppendLine("CDGs: " + sv.CDGs);
                sb.AppendLine("ZIPs: " + sv.ZIPs);
                sb.AppendLine("URLs: " + sv.URLs);
                sb.AppendLine("Bads: " + sv.Bads);
                sb.AppendLine("Not Found: " + sv.NotFound);
                if (sv.PathNotFound.Count > 0 && sv.PathNotFound.Count < 100)
                {
                    foreach (string path in sv.PathNotFound)
                        sb.AppendLine(path);
                }
                richTextStatus.Text = sb.ToString();
                richTextStatus.Refresh();
                //StreamWriter swd = File.CreateText(@"c:\temp\zipd.txt");
                //StreamWriter swa = File.CreateText(@"c:\temp\zipa.txt");
                //StreamWriter swi = File.CreateText(@"c:\temp\zid3.txt");
                //swd.Write(sv.zipdelete.ToString());
                //swa.Write(sv.zipadd.ToString());
                //swi.Write(sv.id3.ToString());
                //swd.Close();
                //swa.Close();
                //swi.Close();
            }

            SetStateReady();
        }

        public async void SetOneDriveLinks()
        {
            SyncTimer.Stop();
            SetStateRunning();
            OneDriveFolder rootfolder = null;
            if (onedriveitems != null && onedriveitems.Count > 0)
            {
                int titems = onedriveitems.Count;
                sb.AppendLine("Item Count: " + titems.ToString());
                int nitems = 0;
                int nerrors = 0;
                foreach (OneDriveItem item in onedriveitems)
                {
                    nitems++;
                    if (bstop == true)
                    {
                        bstop = false;
                        sb.AppendLine("Stopped by user");
                        richTextStatus.Text = sb.ToString();
                        richTextStatus.Refresh();
                        SetStateReady();
                        return;
                    }
                    string karaoke = "/karaoke/";
                    string lowpath = item.Path.Trim().ToLower();
                    string therootpath = item.Path.Trim().Replace('\\', '/');
                    string lowrootpath = therootpath.ToLower();
                    if (lowrootpath.StartsWith("http") == true || lowrootpath.StartsWith("www") == true)
                    {
                        item.Link = item.Path;
                        continue;
                    }
                    int ixi = lowrootpath.IndexOf(karaoke);
                    int ixe = therootpath.LastIndexOf('/');
                    if (!(ixi > 0 && ixi < ixe))
                    {
                        item.Link = item.Path;
                        sb.AppendLine(string.Format("Path not found: {0}", therootpath));
                        richTextStatus.Text = sb.ToString();
                        richTextStatus.Refresh();
                        if (nerrors++ < 20)
                        {
                            continue;
                        }
                        else
                        {
                            sb.AppendLine("Excessive not found errors - aborting...");
                            richTextStatus.Text = sb.ToString();
                            richTextStatus.Refresh();
                            SetStateReady();
                            return;
                        }
                    }
                    if (item.Link != null && item.Link.Trim().Length > 0)
                    {
                        signinButton.Enabled = true;
                        signOutButton.Enabled = true;
                    }
                    else
                    {
                        string thefile = Path.GetFileName(therootpath);
                        string rootpath = therootpath.Substring(ixi + 1, ixe - ixi - 1);
                        string[] rootpaths = rootpath.Split('/');
                        rootpath = "me/skydrive";
                        foreach (string root in rootpaths)
                        {
                            if (bstop == true)
                            {
                                bstop = false;
                                sb.AppendLine("Stopped by user");
                                richTextStatus.Text = sb.ToString();
                                richTextStatus.Refresh();
                                SetStateReady();
                                return;
                            }
                            if (parents.ContainsKey(root) == true)
                            {
                                rootfolder = new OneDriveFolder("", parents[root], root, "");
                                rootpath = parents[root];
                            }
                            else
                            {
                                richTextStatus.Text = sb.ToString() + String.Format("Finding root: {0}", root);
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
                                            if (bstop == true)
                                            {
                                                bstop = false;
                                                sb.AppendLine("Stopped by user");
                                                richTextStatus.Text = sb.ToString();
                                                richTextStatus.Refresh();
                                                SetStateReady();
                                                return;
                                            }
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
                                    sb.AppendLine(ex.Message);
                                    richTextStatus.Text = sb.ToString();
                                    richTextStatus.Refresh();
                                    if (nerrors++ > 20)
                                    {
                                        SetStateReady();
                                        return;
                                    }
                                    else
                                        continue;
                                }
                                bool found = false;
                                foreach (OneDriveFolder f in rootfolders)
                                {
                                    if (f.name == root)
                                    {
                                        parents.Add(root, f.id);
                                        rootfolder = f;
                                        rootpath = f.id;
                                        found = true;
                                        break;
                                    }
                                }
                                if (found == false)
                                    rootfolder = null;
                            }
                        }

                        if (rootfolder == null)
                        {
                            sb.AppendLine(string.Format("Root folder not found: {0}", rootpath));
                            richTextStatus.Text = sb.ToString();
                            richTextStatus.Refresh();
                            continue;
                        }

                        //folder.a645bfc91d3b795a.A645BFC91D3B795A!41650/files //Sample path (using id)
                        //https://onedrive.live.com/redir?resid=A645BFC91D3B795A!41650&authkey=!ABx2Nb-uPkoa7RA&ithint=folder%2ctxt //Sample link
                        try
                        {
                            LiveOperationResult result = await this.liveConnectClient.GetAsync(string.Format("{0}/files", rootfolder.id));

                            if (result != null && result.Result != null && result.Result["data"] != null)
                            {
                                foreach (KeyValuePair<string, object> o in result.Result)
                                {
                                    if (bstop == true)
                                    {
                                        bstop = false;
                                        sb.AppendLine("Stopped by user");
                                        richTextStatus.Text = sb.ToString();
                                        richTextStatus.Refresh();
                                        SetStateReady();
                                        return;
                                    }
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
                                                if (type == "file")
                                                {
                                                    if (name.ToLower().Trim() == thefile.ToLower().Trim())
                                                    {
                                                        string ic = string.Format("Item {0} of {1} -> {2}", nitems, titems, name);
                                                        richTextStatus.Text = sb.ToString() + ic;
                                                        richTextStatus.Refresh();
                                                        LiveOperationResult resultlink = null;
                                                        for (int i = 0; i < 3; i++)
                                                        {
                                                            try
                                                            {
                                                                resultlink = await this.liveConnectClient.GetAsync(string.Format("{0}/shared_read_link", id));
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
                                                                    item.Link = raw.Substring(iof, end - iof);
                                                                    item.isnew = true;
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
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
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
                            //btnCreateMap.Enabled = true;
                            //txtRoot.Enabled = true;
                        }
                    }
                }
            }
            SetTimer(WriteLinks);
        }

        private void WriteLinks()
        {
            SyncTimer.Stop();
            if (sv != null && onedriveitems != null && onedriveitems.Count > 0)
            {
                var newdriveitems = onedriveitems.Where(n => n.isnew == true);
                foreach (OneDriveItem item in newdriveitems)
                {
                    if (bstop == true)
                    {
                        bstop = false;
                        sb.AppendLine("Stopped by user");
                        richTextStatus.Text = sb.ToString();
                        richTextStatus.Refresh();
                        SetStateReady();
                        return;
                    }
                    sv.WriteLink(item.Row, item.Link);
                    sb.AppendLine();
                    sb.AppendLine(item.Path);
                    sb.AppendLine(item.Link);
                }
            }
            SetTimer(WrapUp);
        }

        private void FindOneDriveRoot()
        {
            object defreg = null;
            object oreg = null;
            try
            {
                oreg = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SkyDrive", "UserFolder", defreg);
                if (oreg != null)
                {
                    OneDriveRoot = oreg.ToString();
                }
            }
            catch (Exception ex)
            {
                OneDriveError = ex.Message;
            }
            if (OneDriveRoot == "" || Directory.Exists(OneDriveRoot) == false)
            {
                try
                {
                    oreg = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\OneDrive", "UserFolder", defreg);
                    if (oreg != null)
                        OneDriveRoot = oreg.ToString();
                }
                catch (Exception ex)
                {
                    OneDriveError = ex.Message;
                }
            }
            if (OneDriveRoot.Trim().Length > 0)
            {
                if (Directory.Exists(OneDriveRoot) == false)
                {
                    OneDriveRoot = "";
                    OneDriveError = "Path not found: " + OneDriveRoot;
                }
                else
                {
                    txtOneDriveRoot.ReadOnly = true;
                    txtOneDriveRoot.Text = OneDriveRoot.Trim();
                }
            }
            if (OneDriveRoot.Trim().Length == 0)
            {
                txtOneDriveRoot.ReadOnly = false;
                if (OneDriveError.Trim().Length == 0)
                    OneDriveError = "Unexpected errr";
                txtOneDriveRoot.Text = OneDriveError;
            }
        }

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

        private void btnValidateMP3_Click(object sender, EventArgs e)
        {
            blink = false;
            bfiles = false;
            bzip = false;
            SetTimer(ReadyCheck);
        }

        private void btnValidateMP3Files_Click(object sender, EventArgs e)
        {
            blink = false;
            bfiles = true;
            bzip = false;
            SetTimer(ReadyCheck);
        }

        private void btnValidateZipFiles_Click(object sender, EventArgs e)
        {
            blink = false;
            bfiles = true;
            bzip = true;
            SetTimer(ReadyCheck);
        }

        private void btnZIPLink_Click(object sender, EventArgs e)
        {
            blink = true;
            bfiles = false;
            bzip = false;
            SetTimer(ReadyCheck);
        }

        private void SetTimer(runtimer t)
        {
            SyncTimer.Stop();
            SyncTimer.Tag = t;
            SyncTimer.Interval = 10;
            SyncTimer.Start();
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            SyncTimer.Stop();
            SyncTimer.Interval = 30000;
            if (SyncTimer.Tag != null && SyncTimer.Tag is runtimer)
            {
                SyncTimer.Start();
                (SyncTimer.Tag as runtimer).Invoke();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            bstop = true;
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}
