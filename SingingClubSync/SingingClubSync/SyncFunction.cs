using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Xml;
using System.Threading.Tasks;

using QueueStation.Models;

namespace SingingClubSync
{
    public class SingingClub
    {
        private string _xml = "";
        private string _connectionstring = "";

        public SingingClub(string connectionstring)
        {
            _connectionstring = connectionstring;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "select TABLE_NAME from INFORMATION_SCHEMA.TABLES for XML Path('Data'), Root('Root')";

                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            object o = sdr.GetValue(0);
                            if (o != null)
                                _xml += o.ToString();
                        }
                    }
                }
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        private string ServiceInitialized(string table)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                if (_xml == null || _xml.Trim().Length == 0)
                    return "Failed to initialize SingingClub data service - xml empty";

                if (Utility.IsValidXml(_xml) == false)
                    return "Failed to properly initialize SingingClub data service: " + _xml;
                doc.LoadXml(_xml);
                XmlNodeList nodelist = doc.SelectNodes("/Root/Data");
                if (nodelist == null || nodelist.Count == 0)
                    return "Failed to find root data in SingingClub data service: " + _xml;
                string test = "";
                bool bFound = false;
                foreach (XmlNode node in nodelist)
                {
                    test = Utility.GetXmlString(node, "TABLE_NAME");
                    if (test.Trim().ToLower() == table.Trim().ToLower())
                    {
                        bFound = true;
                        break;
                    }
                }
                if (bFound == false)
                    return "Unknown table name: " + table;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }

        public string GetSingerHistoryForVenue(string VenueKey)
        {
            string result = "GetSingerHistoryForVenue failed for: " + VenueKey;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetSingerHistoryForVenue";
                    cmd.Parameters.Add(new SqlParameter("VenueKey", VenueKey));
                    conn.Open();
                    result = "";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            object o = sdr.GetValue(0);
                            if (o != null)
                                result += o.ToString();
                        }
                        return result;
                    }
                }
            }
        }

        public string GeneralStore(TableName tablename, TableAction tableaction, string xml)
        {
            string table = tablename.ToString();
            string action = tableaction.ToString();
            string initialized = ServiceInitialized(table);
            if (initialized != "OK")
                return initialized;
            switch (action.Trim().ToUpper())
            {
                case "GET":
                    return RunGet(table, xml);
                case "INSERT":
                    return RunInsert(table, xml);
                case "UPDATE":
                    return RunUpdate(table, xml);
                case "DELETE":
                    return RunDelete(table, xml);
                default:
                    return "Action not recognized: " + action;
            }
        }

        private string RunGet(string table, string xml)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("select * from {0} for XML Path('Data'), Root('Root')", table);
                    string result = "";
                    if (Utility.IsValidXml(xml) == false)
                        return "Invalid XML data sent to RunGet method";

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc.SelectNodes("/Root/Data/KEYS");
                    if (nodelist == null || nodelist.Count == 0)
                        return "No primary key fields found in XML content for RunGet method";
                    StringBuilder cmds = new StringBuilder();
                    StringBuilder keys = new StringBuilder();
                    StringBuilder ordr = new StringBuilder();
                    cmds.AppendLine(string.Format("select * from {0}", table));
                    string comma = "";
                    string amp = " AND";

                    List<XmlNode> nodes = new List<XmlNode>();
                    foreach (XmlNode node in nodelist)
                    {
                        string columnname = Utility.GetXmlString(node, "COLUMN_NAME");
                        string columntype = Utility.GetXmlString(node, "COLUMN_TYPE");
                        string columnvalue = Utility.SqlString(Utility.Decode4bit(Utility.GetXmlString(node, "COLUMN_VALUE")));

                        if (columnvalue.Trim().Length > 0)
                        {
                            if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
                            {
                                nodes.Add(node);
                            }
                            else
                            {
                                int number = 0;
                                if (int.TryParse(columnvalue, out number) == true)
                                    if (number >= 0)
                                        nodes.Add(node);
                            }
                        }
                    }

                    int cnt = 0;
                    foreach (XmlNode node in nodes)
                    {
                        cnt++;
                        if (cnt == nodes.Count)
                            amp = "";
                        string columnname = Utility.GetXmlString(node, "COLUMN_NAME");
                        string columntype = Utility.GetXmlString(node, "COLUMN_TYPE");
                        string columnvalue = Utility.SqlString(Utility.Decode4bit(Utility.GetXmlString(node, "COLUMN_VALUE")));

                        if (columnvalue.Trim().Length > 0)
                        {
                            if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
                            {
                                keys.AppendLine(string.Format("{0} = '{1}'{2}", columnname, columnvalue, amp));
                            }
                            else
                            {
                                keys.AppendLine(string.Format("{0} = {1}{2}", columnname, columnvalue, amp));
                            }
                        }
                        ordr.AppendLine(string.Format("\t{0}[{1}]", comma, columnname));
                        comma = ",";
                    }
                    if (keys.ToString().Trim().Length > 0)
                    {
                        cmds.AppendLine("where ");
                        cmds.Append(keys.ToString());
                    }
                    if (ordr.ToString().Trim().Length > 0)
                    {
                        cmds.AppendLine("ORDER BY ");
                        cmds.Append(ordr.ToString());
                    }
                    cmds.AppendLine(" for XML Path('Data'), Root('Root')");
                    cmd.CommandText = cmds.ToString();
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            object o = sdr.GetValue(0);
                            if (o != null)
                                result += o.ToString();
                        }
                        return result;
                    }
                }
            }
        }

        private string RunInsert(string table, string xml)
        {
            if (Utility.IsValidXml(xml) == false)
                return "Invalid XML data sent to RunInsert method";
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc.SelectNodes("/Root/Data/COLUMNS");
                    if (nodelist == null || nodelist.Count == 0)
                        return "No columns found in XML content for RunInsert method";
                    StringBuilder cols = new StringBuilder();
                    StringBuilder vals = new StringBuilder();
                    cols.AppendLine(string.Format("INSERT INTO {0}", table));
                    cols.AppendLine("(");
                    vals.AppendLine("VALUES");
                    vals.AppendLine("(");
                    string comma = "";
                    foreach (XmlNode node in nodelist)
                    {
                        string columnname = Utility.GetXmlString(node, "COLUMN_NAME");
                        string columntype = Utility.GetXmlString(node, "COLUMN_TYPE");
                        string columnvalue = Utility.SqlString(Utility.Decode4bit(Utility.GetXmlString(node, "COLUMN_VALUE")));

                        cols.AppendLine(string.Format("\t{0}[{1}]", comma, columnname));
                        if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
                            vals.AppendLine(string.Format("\t{0}'{1}'", comma, columnvalue));
                        else
                            vals.AppendLine(string.Format("\t{0}{1}", comma, columnvalue));
                        comma = ",";
                    }
                    cols.AppendLine(")");
                    vals.AppendLine(")");
                    cmd.CommandText = cols.ToString() + vals.ToString();
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    rows = cmd.ExecuteNonQuery();
                }
            }
            return rows.ToString();
        }

        public string GetSong(TSCSongListItem sli)
        {
            string error = "<Root><Error>{0}</Error></Root>";
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        string fmt = "SELECT ";
                        fmt += "[Title]";
                        fmt += ",[Artist]";
                        fmt += ",[Disk]";
                        fmt += ",[IsHelper]";
                        fmt += ",[IsDuet]";
                        fmt += ",[DuetArtist]";
                        fmt += ",[OneDriveMP4]";
                        fmt += ",[OneDriveZip]";
                        fmt += ",[FilePath]";
                        fmt += " FROM [TSCSongList_Main] WHERE ";
                        fmt += "[Title] = '{0}' AND ";
                        fmt += "[Artist] = '{1}' AND";
                        fmt += "[Disk] = '{2}' ";
                        fmt += "FOR XML Path('Data'), Root('Root')";
                        string command = string.Format(fmt,
                            Utility.SqlString(sli.Title),
                            Utility.SqlString(sli.Artist),
                            Utility.SqlString(sli.Disk));
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        string result = "";
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                object o = sdr.GetValue(0);
                                if (o != null)
                                    result += o.ToString();
                            }
                            return result;
                        }

                    }
                    catch (Exception ex)
                    {
                        return string.Format(error, ex.Message);
                    }
                }
            }
        }

        public List<TSCSongListItem> GetSongList()
        {
            List<TSCSongListItem> items = new List<TSCSongListItem>();
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        string fmt = "SELECT ";
                        fmt += "[Title]";
                        fmt += ",[Artist]";
                        fmt += ",[Disk]";
                        fmt += ",[IsHelper]";
                        fmt += ",[IsDuet]";
                        fmt += ",[DuetArtist]";
                        fmt += ",[OneDriveMP4]";
                        fmt += ",[OneDriveZip]";
                        fmt += ",[FilePath]";
                        fmt += " FROM [TSCSongList_Main]";
                        cmd.CommandText = fmt;
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        string result = "";
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                items.Add(new TSCSongListItem(
                                    sdr.GetString(0),
                                    sdr.GetString(1),
                                    sdr.GetString(2),
                                    sdr.GetString(3),
                                    sdr.GetString(4),
                                    sdr.GetString(5),
                                    sdr.GetString(6),
                                    sdr.GetString(7),
                                    sdr.GetString(8)));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        items.Add(new TSCSongListItem("", "", "", "", "", "", "", "", ex.Message));
                    }
                }
            }
            return items;
        }
        public string AddSong(TSCSongListItem sli)
        {
            int rows = -1;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        string fmt = "INSERT INTO [TSCSongList_Main]";
                        fmt += "([Title]";
                        fmt += ",[Artist]";
                        fmt += ",[Disk]";
                        fmt += ",[IsHelper]";
                        fmt += ",[IsDuet]";
                        fmt += ",[DuetArtist]";
                        fmt += ",[OneDriveMP4]";
                        fmt += ",[OneDriveZip]";
                        fmt += ",[FilePath])";
                        fmt += " VALUES ";
                        fmt += "('{0}'";
                        fmt += ",'{1}'";
                        fmt += ",'{2}'";
                        fmt += ",'{3}'";
                        fmt += ",'{4}'";
                        fmt += ",'{5}'";
                        fmt += ",'{6}'";
                        fmt += ",'{7}'";
                        fmt += ",'{8}')";
                        string command = string.Format(fmt,
                            Utility.SqlString(sli.Title),
                            Utility.SqlString(sli.Artist),
                            Utility.SqlString(sli.Disk),
                            Utility.SqlString(sli.IsHelper),
                            Utility.SqlString(sli.IsDuet),
                            Utility.SqlString(sli.DuetArtist),
                            Utility.SqlString(sli.OneDriveMP4),
                            Utility.SqlString(sli.OneDriveZip),
                            Utility.SqlString(sli.FilePath));
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
            return rows.ToString();
        }

        private string RunUpdate(string table, string xml)
        {
            if (Utility.IsValidXml(xml) == false)
                return "Invalid XML data sent to RunUpdate method";
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc.SelectNodes("/Root/Data/COLUMNS");
                    if (nodelist == null || nodelist.Count == 0)
                        return "No columns found in XML content for RunUpdate method";
                    StringBuilder cols = new StringBuilder();
                    cols.AppendLine(string.Format("UPDATE {0}", table));
                    cols.AppendLine("SET");
                    string comma = "";
                    foreach (XmlNode node in nodelist)
                    {
                        string columnname = Utility.GetXmlString(node, "COLUMN_NAME");
                        string columntype = Utility.GetXmlString(node, "COLUMN_TYPE");
                        string columnvalue = Utility.SqlString(Utility.Decode4bit(Utility.GetXmlString(node, "COLUMN_VALUE")));

                        if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
                            cols.AppendLine(string.Format("\t{0}[{1}] = '{2}'", comma, columnname, columnvalue));
                        else
                            cols.AppendLine(string.Format("\t{0}[{1}] = {2}", comma, columnname, columnvalue));
                        comma = ",";
                    }
                    cols.AppendLine("WHERE ");
                    nodelist = doc.SelectNodes("/Root/Data/KEYS");
                    if (nodelist == null || nodelist.Count == 0)
                        return "No primary key fields found in XML content for RunUpdate method";
                    comma = "";
                    string amp = " AND";
                    int cnt = 0;
                    foreach (XmlNode node in nodelist)
                    {
                        cnt++;
                        if (cnt == nodelist.Count)
                            amp = "";
                        string columnname = Utility.GetXmlString(node, "COLUMN_NAME");
                        string columntype = Utility.GetXmlString(node, "COLUMN_TYPE");
                        string columnvalue = Utility.SqlString(Utility.Decode4bit(Utility.GetXmlString(node, "COLUMN_VALUE")));

                        if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
                            cols.AppendLine(string.Format("\t[{0}] = '{1}'{2}", columnname, columnvalue, amp));
                        else
                            cols.AppendLine(string.Format("\t[{0}] = {1}{2}", columnname, columnvalue, amp));
                        comma = ",";
                    }
                    cmd.CommandText = cols.ToString();
                    conn.Open();
                    rows = cmd.ExecuteNonQuery();
                }
            }
            return rows.ToString();
        }

        private string RunDelete(string table, string xml)
        {
            if (Utility.IsValidXml(xml) == false)
                return "Invalid XML data sent to RunDelete method";
            int rows = 0;
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodelist = doc.SelectNodes("/Root/Data/KEYS");
                    if (nodelist == null || nodelist.Count == 0)
                        return "No primary key fields found in XML content for RunUpdate method";
                    StringBuilder keys = new StringBuilder();
                    keys.AppendLine(string.Format("DELETE {0}", table));
                    keys.AppendLine("WHERE ");
                    string amp = " AND";
                    int cnt = 0;
                    foreach (XmlNode node in nodelist)
                    {
                        cnt++;
                        if (cnt == nodelist.Count)
                            amp = "";
                        string columnname = Utility.GetXmlString(node, "COLUMN_NAME");
                        string columntype = Utility.GetXmlString(node, "COLUMN_TYPE");
                        string columnvalue = Utility.SqlString(Utility.Decode4bit(Utility.GetXmlString(node, "COLUMN_VALUE")));

                        if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
                            keys.AppendLine(string.Format("\t[{0}] = '{1}'{2}", columnname, columnvalue, amp));
                        else
                            keys.AppendLine(string.Format("\t[{0}] = {1}{2}", columnname, columnvalue, amp));
                    }
                    cmd.CommandText = keys.ToString();
                    conn.Open();
                    rows = cmd.ExecuteNonQuery();
                }
            }
            return rows.ToString();
        }
    }
}
