using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.Web;

using XmlUtility;

namespace WcfSingingClub
{
    public class GeneralStore
    {
		private string _xml = "";
		//private string _connectionstring = "Server=dmmlenovo1;Database=TheSingingClub;Integrated Security=true";
        private string _connectionstring = "Data Source=SQL5012.Smarterasp.net;Initial Catalog=DB_9BA515_LocalSingingClub;User Id=DB_9BA515_LocalSingingClub_admin;Password=Groovy52!;";

		public GeneralStore()
		{
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

        public List<SongItem> XMLData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
        {
            string xmls = GetXMLData(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
            List<SongItem> songs = new List<SongItem>();
            if (Utility.IsValidXml(xmls) == true)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmls);
                XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                if (nodes.Count > 0)
                {
                    foreach (XmlNode node in nodes)
                    {
                        songs.Add(new SongItem(node));
                    }
                }
            }
            return songs;
        }
        public string JSONData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
        {
            return GetXMLData(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
        }
        public string GetXMLData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
        {
            int offset = 0;
            int nreturn = 0;
            int.TryParse(pageoffset, out offset);
            int.TryParse(pagereturn, out nreturn);
            TSCSongs tscsongs = new TSCSongs();
            tscsongs.PageOffset = offset;
            tscsongs.PageOrder = pageorderby;
            tscsongs.PageReturn = nreturn;
            if (pagewhereclause != null && pagewhereclause.Trim().Length > 0 && (pageorderby != null && pageorderby.Trim().Length > 0))
            {
                pagewhereclause = pagewhereclause.Trim();
                string chk = pagewhereclause.Trim().ToLower();
                if (chk.StartsWith("begins"))
                {
                    int i = chk.IndexOf(' ');
                    if (i > 0)
                        pagewhereclause = string.Format("{0} like '{1}%'", pageorderby, pagewhereclause.Substring(i).Trim());
                }
                else if (chk.StartsWith("contains"))
                {
                    int i = chk.IndexOf(' ');
                    if (i > 0)
                        pagewhereclause = string.Format("{0} like '%{1}%'", pageorderby, pagewhereclause.Substring(i).Trim());
                }
                else
                    pagewhereclause = "";
            }
            else
                pagewhereclause = "";
            tscsongs.WhereClause = pagewhereclause;
            string xmls = tscsongs.GetDataXml();
            xmls = RunStore(table, action, xmls);
            return xmls;
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

		public string RunStore(string table, string action, string xml)
		{
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
                    XmlNode datanode = doc.SelectSingleNode("/Root/Data");
                    string where = "";
                    string page = "";
                    if (datanode != null)
                    {
                        where = Utility.GetXmlString(datanode, "WHERE");
                        page = Utility.GetXmlString(datanode, "PAGE");
                    }
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
                        if (where.Length > 0)
                            cmds.Append(string.Format(" {0}", where));
                        else
                            cmds.Append(keys.ToString());
                    }
                    else if (where.Length > 0)
                        cmds.AppendLine(string.Format("where {0}", where));

                    if (ordr.ToString().Trim().Length > 0)
                    {
                        cmds.AppendLine("ORDER BY ");
                        cmds.Append(ordr.ToString());
                        if (page.Length > 0)
                            cmds.Append(string.Format(" {0}", page));
                    }
                    else if (page.Length > 0)
                        cmds.AppendLine(string.Format("ORDER BY {0}", page));
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
							cols.AppendLine(string.Format("\t[{0}] = '{1}'{2}",columnname, columnvalue, amp));
						else
							cols.AppendLine(string.Format("\t[{0}] = {1}{2}",columnname, columnvalue, amp));
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