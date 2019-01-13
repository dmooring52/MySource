using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Web;

namespace QueueStation
{
    public class Utility
    {
        public const int NumberOfRounds = 6;
        public Utility()
        {
        }

        public static bool IsValidXml(string xml)
        {
            if (xml == null || xml.Trim().Length == 0)
                return true;
            if (!(xml.Contains('<') && xml.Contains('>')))
                return false;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string SqlString(string cmd)
        {
            return cmd.Replace("'", "''");
        }

        public static int GetInt(string s)
        {
            if (s == null || s.Trim().Length == 0)
                return -1;
            int r = -1;
            int.TryParse(s, out r);
            return r;
        }

        public static string NotNullString(string s)
        {
            string r = "";
            if (s != null && s.Trim().Length > 0)
                r = s.Trim();
            return r;
        }

        public static bool NotNullBool(string s)
        {
            string r = NotNullString(s);
            if (r.Trim().ToLower().StartsWith("t"))
                return true;
            else
                return false;
        }
        public static string GetXmlString(XmlNode node, string nodename, bool isattribute = false)
        {
            if (node != null)
            {
                if (isattribute == true)
                {
                    if (node.Attributes != null)
                    {
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            if (attr.Name == nodename)
                                return attr.Value;
                        }
                    }
                }
                else
                {
                    XmlNode subnode = node.SelectSingleNode(nodename);
                    if (subnode != null)
                        return subnode.InnerText;
                }
            }
            return "";
        }

        public static int GetXmlInteger(XmlNode node, string nodename, bool isattribute = false)
        {
            string number = "";
            int rtn = 0;
            if (node != null)
            {
                if (isattribute == true)
                {
                    if (node.Attributes != null && node.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            if (attr.Name == nodename)
                            {
                                number = attr.Value;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    XmlNode subnode = node.SelectSingleNode(nodename);
                    if (subnode != null)
                        number = subnode.InnerText;
                }
            }
            if (number.Length > 0)
            {
                try
                {
                    rtn = int.Parse(number);
                }
                catch
                {
                }
            }
            return rtn;
        }

        public static DateTime GetXmlDateTime(XmlNode node, string nodename)
        {
            string dt = "";
            DateTime rtn = DateTime.MinValue;
            if (node != null)
            {
                XmlNode subnode = node.SelectSingleNode(nodename);
                if (subnode != null)
                    dt = subnode.InnerText;
            }
            if (dt.Length > 0)
            {
                try
                {
                    rtn = DateTime.Parse(dt);
                }
                catch
                {
                }
            }
            return rtn;
        }

        public static string SortableDate(DateTime dt)
        {
            return dt.Year.ToString() + "-" + d2(dt.Month) + "-" + d2(dt.Day);
        }

        private static string d2(int i)
        {
            if (i < 10)
                return "0" + i.ToString();
            return i.ToString();
        }

        public static string XmlEscape(string xml)
        {
            if (xml != null && xml.Trim().Length > 0)
                return xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
            else
                return xml;
        }

        public static string Encode4bit(string code)
        {
            if (code == null || code.Trim().Length == 0)
                return "";
            string rtn = "";
            int x;
            int l = 240;
            int r = 15;
            int l4;
            int r4;
            foreach (char c in code)
            {
                x = (int)c;
                l4 = (x & l) >> 4;
                r4 = x & r;
                rtn += (char)(l4 + 0x41);
                rtn += (char)(r4 + 0x41);
            }
            return rtn;
        }

        public static string Decode4bit(string code)
        {
            string rtn = "";
            int x;
            int l4 = 0;

            int ix = 0;
            foreach (char c in code)
            {
                x = ((int)c) - 0x41;
                if (ix == 0)
                {
                    l4 = x << 4;
                    ix++;
                }
                else
                {
                    rtn += (char)(l4 | x);
                    ix = 0;
                }
            }
            return rtn;
        }
        public static bool IsNumber(string number)
        {
            if (number == null || number.Trim().Length == 0)
                return true;
            foreach (char c in number)
                if (!char.IsNumber(c))
                    return false;
            return true;
        }
    }
}