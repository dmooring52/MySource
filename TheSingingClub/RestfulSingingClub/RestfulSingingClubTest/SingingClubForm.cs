using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace RestfulSingingClubTest
{
    public partial class SingingClubForm : Form
    {
        public SingingClubForm()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //Get method
                WebRequest req = WebRequest.Create(@"http://localhost:54416/RestfulSingingClubService.svc/GetSongList/");

                req.Method = "GET";
                StringBuilder sb = new StringBuilder();
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream respStream = resp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                        sb.AppendLine(reader.ReadToEnd());
                    }
                    MessageBox.Show(sb.ToString(), "Success");
                }
                else
                {
                    MessageBox.Show(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription), "Return code error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Occurred");
            }
        }
    }
}
