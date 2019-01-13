using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Xml;
using System.Data.SqlClient;

namespace XyproFilter
{
    public partial class XyproFilterForm : Form
    {
        public Random random = new Random(1000);
        public enum ColType{
            jobDescription,
            departmentName,
            managerId,
            dateHired,
            lastPasswordChange
        }
        public string[] Job = { "Manager", "Analyst", "Developer", "Tester" };
        public string[] Dept = { "Development", "QA", "Support", "Senior Management" };
        public SqlConnection sqlConn = null;
        public SqlCommand sqlCommand = null;
        public DateTime TodayDate = DateTime.Now.AddDays(-30);
        public XyproFilterForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int success = 0;
            int fail = 0;
            try
            {
                sqlConn = new SqlConnection("Server=DMMLENOVO1;Database=Xypro;Trusted_Connection=true;");
                sqlConn.Open();
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConn;
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Users\dmooring\OneDrive\VS2013\Xypro\Names.xml");
                foreach (XmlNode node in doc.SelectNodes("/records/record"))
                {
                    string lastname = "";
                    string firstname = "";
                    int Id = -1;
                    XmlNode nnode = node.SelectSingleNode("Id");
                    if (nnode != null)
                    {
                        string id = nnode.InnerText;
                        if (id != null && id.Trim().Length > 0)
                        {
                            int.TryParse(id, out Id);
                            if (Id >= 0)
                                Id += (401000);
                        }
                    }
                    nnode = node.SelectSingleNode("First_Name");
                    if (nnode != null)
                    {
                        firstname = SqlString(nnode.InnerText);
                    }
                    nnode = node.SelectSingleNode("Last_Name");
                    if (nnode != null)
                    {
                        lastname = SqlString(nnode.InnerText) + "Y";
                    }
                    if (Id >= 0 && IsValid(firstname) && IsValid(lastname))
                    {
                        int rndJob = RandomString(ColType.jobDescription);
                        int rndDept = RandomString(ColType.departmentName);
                        int rows = -1;
                        if (rndJob >= 0 && rndDept >= 0)
                        {
                            int rndManager = RandomManager(rndDept);
                            DateTime hire = RandomHire();
                            DateTime pwd = RandomPwd();
                            if (hire > pwd)
                                pwd = hire.AddDays(1);
                            sqlCommand.CommandText = string.Format("Insert Employee (employeeId, firstName, lastName, jobDescription, departmentName, managerId, dateHired, lastPasswordChange) values ({0}, '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}')", Id, firstname, lastname, Job[rndJob], Dept[rndDept], rndManager, hire, pwd);
                            rows = sqlCommand.ExecuteNonQuery();
                        }
                        else
                            rows = -1;
                        if (rows > 0)
                            success++;
                        else
                            fail++;
                    }
                }
            }
            catch (Exception ex)
            {
                btnStart.Enabled = false;
                MessageBox.Show(ex.Message, "Exception occurred");
            }
            btnStart.Enabled = false;
            MessageBox.Show("Done", "Operation completed");
        }
        private string SqlString(string s)
        {
            if (s != null && s.Trim().Length > 0)
            {
                char[] escapes = {'\'', '"', '\\', '\n', '\r', '\t', '%', '_'};
                if (s.Contains('\\'))
                    s = s.Replace("\\", "\\\\");
                if (s.Contains('\b'))
                    s = s.Replace("\b", "");
                if (s.Contains('\''))
                    s = s.Replace("'", "\'\'");
                if (s.Contains('"'))
                    s = s.Replace("\"", "\\\"");
                if (s.Contains('\n'))
                    s = s.Replace("\n", "");
                if (s.Contains('\r'))
                    s = s.Replace("\r", "");
                if (s.Contains('\t'))
                    s = s.Replace("\t", " ");
                if (s.Contains('%'))
                    s = s.Replace("%", "");
                if (s.Contains('_'))
                    s = s.Replace("_", "");
            }
            return s;
        }
        private bool IsValid(string s)
        {
            if (s != null && s.Trim().Length > 0)
                return true;
            else
                return false;
        }
        private int RandomString(ColType typ)
        {
            if (typ == ColType.departmentName)
            {
                int i = Dept.Length;
                i = random.Next(i);
                return i;
            }
            if (typ == ColType.jobDescription)
            {
                int i = Job.Length;
                i = random.Next(i);
                return i;
            }
            return -1;
        }
        private int RandomManager(int dept)
        {
            return ((dept + 1) * 1000) + random.Next(10);
        }
        private DateTime RandomHire()
        {
            return TodayDate.AddDays((-1) * random.Next(365));
        }
        private DateTime RandomPwd()
        {
            return TodayDate.AddDays((-1) * random.Next(90));
        }
    }
}
