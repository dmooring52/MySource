using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace ToolMaker
{
	public class mytable
	{
		public mytable()
		{
		}
		public mytable(string classname)
		{
			xml_class = classname;
		}
		public mytable(string classname, string column, string index)
		{
			xml_class = classname;
			xml_column = column;
			xml_index = index;
		}
		public string xml_class;
		public string xml_column;
		public string xml_index;
	}

	public class myvariable
	{
		public string VType;
		public string VBase;
		public myvariable() { }
		public myvariable(string vtype, string vbase)
		{
			VType = vtype;
			VBase = vbase;
		}
		public myvariable(XmlNode node)
		{
			VType = XLateType(GetXmlString(node, "DATA_TYPE"));
			VBase = GetXmlString(node, "COLUMN_NAME");
		}
		public myvariable(XmlNode node, XmlNodeList xcheck)
		{
			VBase = GetXmlString(node, "COLUMN_NAME");
			VType = XLateType(GetXmlString(node, xcheck, "COLUMN_NAME", "DATA_TYPE"));
		}
		public string VPrivate { get { return "_" + VBase[0].ToString().ToLower() + VBase.Substring(1, VBase.Length - 1); } }
		public string VPublic { get { return VBase[0].ToString().ToUpper() + VBase.Substring(1, VBase.Length - 1); } }
		public string VParam { get { return VBase[0].ToString().ToLower() + VBase.Substring(1, VBase.Length - 1); } }
		public string VGetXml { get { return GetXml(VType); } }
		public string VHValue { get { return Camel(VBase[0].ToString().ToUpper() + VBase.Substring(1, VBase.Length - 1)); } }
		private string XLateType(string sqlType)
		{
			switch (sqlType.ToLower())
			{
				case "varchar":
					return "string";
				case "int":
					return "int";
				case "datetime":
					return "DateTime";
				default:
					return sqlType;
			}
		}
		private string Camel(string variable)
		{
			bool start = false;
			if (variable.Length > 1)
			{
				for (int i = 1; i < variable.Length; i++)
				{
					if (variable[i].ToString().ToUpper() != variable[i].ToString())
						start = true;
					if (start == true)
						if (variable[i].ToString().ToUpper() == variable[i].ToString())
							return (variable.Substring(i, variable.Length - i));
				}
			}
			return variable;
		}
		private string GetXmlString(XmlNode node, string nodename)
		{
			if (node != null)
			{
				XmlNode subnode = node.SelectSingleNode(nodename);
				if (subnode != null)
					return subnode.InnerText;
			}
			return "";
		}
		private string GetXmlString(XmlNode node, XmlNodeList xcheck, string nodename, string checkname)
		{
			string nodevalue = GetXmlString(node, nodename);
			foreach (XmlNode n in xcheck)
			{
				if (nodevalue == GetXmlString(n, nodename))
					return GetXmlString(n, checkname);
			}
			return "";
		}
		private string GetXml(string vtype)
		{
			switch (vtype.ToLower())
			{
				case "string":
					return "GetXmlString";
				case "int":
					return "GetXmlInteger";
				case "datetime":
					return "GetXmlDateTime";
				case "bool":
					return "GetXmlBoolean";
				default:
					return vtype;
			}
		}
	}

	public class myconstructor
	{
		public List<myvariable> VParams = new List<myvariable>();
		public List<myvariable> VConsts = new List<myvariable>();
		public myconstructor(List<myvariable> vparams, List<myvariable> vconsts)
		{
			VParams = vparams;
			VConsts = vconsts;
		}
	}

	public class mytemplate
	{
		public List<myvariable> vbase = new List<myvariable>();
		public List<myvariable> vconstprimary = new List<myvariable>();
		public List<myvariable> vconstcopy = new List<myvariable>();
		public string[] vrepeat = { "<variabletype>", "<privatevariable>", "<publicvariable>", "<getxml>" };
		public string vstaticconstructor = "<constructors>";
		public string vstaticfieldequals = "<fieldequals>";
		public myvariable vclass = null;
		public myvariable vclassname = null;
		public myvariable vshortclass = null;
		public mytemplate(mytable table)
		{
			XmlDocument cols = new XmlDocument();
			cols.LoadXml(table.xml_column);
			XmlDocument keys = new XmlDocument();
			keys.LoadXml(table.xml_index);

			XmlNodeList colNodes = cols.SelectNodes("/Root/Data");
			XmlNodeList keyNodes = keys.SelectNodes("/Root/Data");
			vclass = new myvariable("<class>", table.xml_class);
			vclassname = new myvariable("<classname>", table.xml_class.ToLower());
			vshortclass = new myvariable("<shortclass>", table.xml_class.Substring(3));
			foreach (XmlNode node in colNodes)
				vbase.Add(new myvariable(node));

			foreach (XmlNode node in keyNodes)
				vconstprimary.Add(new myvariable(node, colNodes));
		}
		public string vreplace(string target, string tmp, myvariable myvar)
		{
			string rtn = target;
			switch (tmp)
			{
				case "<variabletype>":
					rtn = target.Replace(tmp, myvar.VType);
					break;
				case "<privatevariable>":
					rtn = target.Replace(tmp, myvar.VPrivate);
					break;
				case "<publicvariable>":
					rtn = target.Replace(tmp, myvar.VPublic);
					break;
				case "<getxml>":
					rtn = target.Replace(tmp, myvar.VGetXml);
					break;
				default:
					rtn = "ERROR";
					break;
			}
			return rtn;
		}
	}
}
