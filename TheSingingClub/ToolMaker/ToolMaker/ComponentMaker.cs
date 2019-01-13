using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ToolMaker
{
	class ComponentMaker
	{
		private string _codeFile = "";
		public ComponentMaker()
		{
		}

		public string Build(string codefile)
		{
			_codeFile = codefile;
			StringBuilder builder = new StringBuilder();
			mytable myTable = null;
			mytemplate template = null;
			myTable = GetTable("TSCQueue");
			template = new mytemplate(myTable);
			string classQueue = GetClass(template);
			myTable = GetTable("TSCEvents");
			template = new mytemplate(myTable);
			string classEvents = GetClass(template);
			myTable = GetTable("TSCSingers");
			template = new mytemplate(myTable);
			string classSingers = GetClass(template);
			myTable = GetTable("TSCVenues");
			template = new mytemplate(myTable);
			string classVenues = GetClass(template);
			builder.Append(classQueue);
			builder.AppendLine();
			builder.AppendLine();
			builder.Append(classEvents);
			builder.AppendLine();
			builder.AppendLine();
			builder.Append(classSingers);
			builder.AppendLine();
			builder.AppendLine();
			builder.Append(classVenues);
			return builder.ToString();
		}

		private string GetClass(mytemplate template)
		{
			StringBuilder sb = new StringBuilder();
			StreamReader sr = File.OpenText(_codeFile);
			if (sr != null)
			{
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					if (line != null)
					{
						bool bFound = false;
						if (line.Contains(template.vstaticconstructor))
						{
							BuildConstructor(sb, line, template);
							continue;
						}
						if (line.Contains(template.vstaticfieldequals))
						{
							BuildKeyCompare(sb, line, template);
							continue;
						}
						foreach (string v in template.vrepeat)
						{
							if (line.Contains(v))
							{
								bFound = true;
								break;
							}
						}
						if (line.Contains(template.vclass.VType))
							line = line.Replace(template.vclass.VType, template.vclass.VBase);
						if (line.Contains(template.vclassname.VType))
							line = line.Replace(template.vclassname.VType, template.vclassname.VBase);
						if (line.Contains(template.vshortclass.VType))
							line = line.Replace(template.vshortclass.VType, template.vshortclass.VBase);
						if (bFound == true)
						{
							foreach (myvariable t in template.vbase)
							{
								string newline = line;
								foreach (string v in template.vrepeat)
								{
									if (newline.Contains(v))
									{
										newline = template.vreplace(newline, v, t);
									}
								}
								sb.AppendLine(newline);
							}
						}
						else
							sb.AppendLine(line);
					}
				}
			}
			sr.Close();
			return sb.ToString();
		}

		private mytable GetTable(string table)
		{
			mytable mtable = new mytable(table);
			SqlConnection conn = new SqlConnection("Server=dmmlenovo1;Database=TheSingingClub;Integrated Security=true");
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "Select COLUMN_NAME, DATA_TYPE from information_schema.COLUMNS where Table_name = '" + table + "' FOR XML Path('Data'), ROOT('Root')";

			conn.Open();
			SqlDataReader sdr = cmd.ExecuteReader();
			while (sdr.Read())
			{
				object o = sdr.GetValue(0);
				if (o != null)
					mtable.xml_column += o.ToString();
			}

			conn.Close();
			cmd.CommandText = "Select COLUMN_NAME from information_schema.KEY_COLUMN_USAGE where Table_name = '" + table + "' FOR XML Path('Data'), ROOT('Root')";
			conn.Open();
			sdr = cmd.ExecuteReader();
			while (sdr.Read())
			{
				object o = sdr.GetValue(0);
				if (o != null)
					mtable.xml_index += o.ToString();
			}
			conn.Close();
			return mtable;
		}

		private void BuildKeyCompare(StringBuilder sb, string line, mytemplate t)
		{
			string newline = "";
			newline = line.Replace(t.vstaticfieldequals, "public bool KeyEquals(" + t.vclass.VBase + " a)");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticfieldequals, "{");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticfieldequals, "\tif ((object)a == null)");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticfieldequals, "\t\treturn false;");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticfieldequals, "\treturn (");
			sb.AppendLine(newline);
			for (int i = 0; i < t.vconstprimary.Count; i++)
			{
				string amp = " &&";
				if (i == t.vconstprimary.Count - 1)
					amp = "";
				myvariable variable = t.vconstprimary[i];
				if (variable.VType == "string")
				{
					newline = line.Replace(t.vstaticfieldequals, "\t(a." + variable.VPublic + ".Trim().ToLower() == " + variable.VPublic + ".Trim().ToLower())" + amp);
					sb.AppendLine(newline);
				}
				else
				{
					newline = line.Replace(t.vstaticfieldequals, "\t(a." + variable.VPublic + " == " + variable.VPublic + ")" + amp);
					sb.AppendLine(newline);
				}
			}
			newline = line.Replace(t.vstaticfieldequals, "\t);");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticfieldequals, "}");
			sb.AppendLine(newline);
		}

		private void BuildConstructor(StringBuilder sb, string line, mytemplate t)
		{
			string newline = "";
			newline = line.Replace(t.vstaticconstructor, "public " + t.vclass.VBase + "()");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticconstructor, "{");
			sb.AppendLine(newline);
			foreach (myvariable variable in t.vbase)
			{
				newline = line.Replace(t.vstaticconstructor, "\tHeaders.Add(\"" + variable.VPublic + "\", \"" + variable.VHValue + "\");");
				sb.AppendLine(newline);
			}
			sb.AppendLine();
			foreach (myvariable variable in t.vbase)
			{
				newline = line.Replace(t.vstaticconstructor, "\tColumns.Add(\"" + variable.VPublic + "\", " + variable.VPrivate + ");");
				sb.AppendLine(newline);
			}
			sb.AppendLine();
			foreach (myvariable variable in t.vconstprimary)
			{
				newline = line.Replace(t.vstaticconstructor, "\tPrimary.Add(\"" + variable.VPublic + "\", " + variable.VPrivate + ");");
				sb.AppendLine(newline);
			}
			
			string comma = "";
			string plist = "";
			foreach (myvariable param in t.vconstprimary)
			{
				plist += (comma + param.VType + " " + param.VParam);
				comma = ", ";
			}
			newline = line.Replace(t.vstaticconstructor, "}");
			sb.AppendLine(newline);
			sb.AppendLine();

			newline = line.Replace(t.vstaticconstructor, "public " + t.vclass.VBase + "(" + plist + ") : this()");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticconstructor, "{");
			sb.AppendLine(newline);
			foreach (myvariable param in t.vconstprimary)
			{
				newline = line.Replace(t.vstaticconstructor, "\t" + param.VPrivate + " = new ColumnClass(\"" + param.VPublic + "\", ColumnType._" + param.VType + ", " + param.VParam + ");");
				sb.AppendLine(newline);
			}
			newline = line.Replace(t.vstaticconstructor, "}");
			sb.AppendLine(newline);
			sb.AppendLine();

			newline = line.Replace(t.vstaticconstructor, "public " + t.vclass.VBase + "(" + t.vclass.VBase + " " + t.vclassname.VParam + ") : this()");
			sb.AppendLine(newline);
			newline = line.Replace(t.vstaticconstructor, "{");
			sb.AppendLine(newline);
			foreach (myvariable param in t.vbase)
			{
				if (param.VType.Trim().ToLower() == "string")
				{
					newline = line.Replace(t.vstaticconstructor, "\t" + param.VPublic + " = new string(" + t.vclassname.VParam + "." + param.VPublic + ".ToArray());");
				}
				else
				{
					newline = line.Replace(t.vstaticconstructor, "\t" + param.VPublic + " = " + t.vclassname.VParam + "." + param.VPublic + ";");
				}
				sb.AppendLine(newline);
			}
			newline = line.Replace(t.vstaticconstructor, "}");
			sb.AppendLine(newline);
			sb.AppendLine();
		}
	}
}
