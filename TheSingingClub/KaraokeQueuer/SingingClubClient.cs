using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.IO;

using XmlUtility;

namespace KaraokeQueuer
{
	class SingingClubClient
	{
		private string _path = "";
		public SingingClubClient()
		{
			_path = ConfigurationManager.AppSettings["DataSourcePath"];
		}

		public XmlDocument GetTable(string table, string eventkey)
		{
			string path = "";
			XmlDocument doc = null;
			if (_path.Trim().Length > 0)
			{
				if (Directory.Exists(_path) == true)
				{
					if (table.Trim().Length > 0)
					{
						if (eventkey.Trim().Length > 0)
							path = Path.Combine(_path, eventkey, table.Trim() + ".xml");
						else
							path = Path.Combine(_path, table.Trim() + ".xml");
						if (File.Exists(path) == false)
						{
							StreamWriter sw = File.CreateText(path);
							sw.Write("<top/>");
							sw.Close();
						}
					}
				}
			}
			if (path.Trim().Length > 0)
			{
				if (File.Exists(path) == true)
				{
					doc = new XmlDocument();
					doc.Load(path);
				}
			}
			return doc;
		}

		public string GeneralStore(string table, XmlDocument doc, string eventkey, string action, string xml)
		{
			switch (action.Trim().ToUpper())
			{
				case "INSERT":
					return RunInsert(doc, xml);
				case "UPDATE":
					return "";//RunUpdate(table, xml);
				case "DELETE":
					return "";//RunDelete(table, xml);
				default:
					return "Action not recognized: " + action;
			}
		}

		public string GetSingerHistoryForVenue(string VenueKey)
		{
			return "";
		}

		private string RunInsert(XmlDocument table, string xml)
		{
			if (Utility.IsValidXml(xml) == false)
				return "Invalid XML data sent to RunInsert method";
			//int rows = 0;
			if (table != null)
			{
				if (xml.Trim().Length > 0)
				{
					XmlDocument doc = new XmlDocument();
					try
					{
						doc.LoadXml(xml);
						XmlNode nodekey = doc.SelectSingleNode("/Root/Data/XPath/Key");
						if (nodekey == null)
							return "XPath key node not found";
						string xpath_key = nodekey.InnerText.Trim();
						if (xpath_key.Length == 0)
							return "XPath for key not found";
						if (table.SelectNodes(xpath_key) != null)
							return "Cannot insert - item already exists: " + xpath_key;

						XmlNode noderoot = doc.SelectSingleNode("/Root/Data/XPath/Root");
						if (noderoot == null)
							return "XPath root node not found";
						string xpath_root = noderoot.InnerText.Trim();
						if (xpath_root.Length == 0)
							return "XPath for root not found";
						noderoot = table.SelectSingleNode(xpath_root);
						if (noderoot == null)
							return "Cannot insert - root not found: " + xpath_root;

						XmlNode noderootname = doc.SelectSingleNode("/Root/Data/XPath/RootNode");
						if (noderootname == null)
							return "XPath for node root name not found";
						string xpath_noderootname = noderootname.InnerText.Trim();
						if (xpath_noderootname.Length == 0)
							return "XPath for root name not found";

						XmlNode tableroot = table.CreateNode(XmlNodeType.Element, xpath_noderootname, table.NamespaceURI);
						noderoot.AppendChild(tableroot);

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

							XmlNode child = table.CreateNode(XmlNodeType.Element, columnname, table.NamespaceURI);
							child.InnerText = columnvalue;
							tableroot.AppendChild(child);

							cols.AppendLine(string.Format("\t{0}[{1}]", comma, columnname));
							if (columntype.Trim().ToLower() == "string" || columntype.Trim().ToLower() == "datetime")
								vals.AppendLine(string.Format("\t{0}'{1}'", comma, columnvalue));
							else
								vals.AppendLine(string.Format("\t{0}{1}", comma, columnvalue));
							comma = ",";
						}
						cols.AppendLine(")");
						vals.AppendLine(")");
						if (table.BaseURI != null && table.BaseURI.Trim().Length > 0)
						{
							Uri uri = new Uri(table.BaseURI);
							if (File.Exists(uri.LocalPath))
							{
								table.Save(uri.LocalPath);
							}
						}
						//cmd.CommandText = cols.ToString() + vals.ToString();
						//cmd.CommandType = CommandType.Text;
						//conn.Open();
						//rows = cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						return ex.Message;
					}
				}
			}
			return "1";
			//return rows.ToString();
		}
		/*
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
		*/
	}
}
