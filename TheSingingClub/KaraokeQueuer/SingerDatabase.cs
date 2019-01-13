using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KaraokeQueuer
{
	public class SingerDatabase
	{
		private SingingClubClient _scc = null;
		public SingerDatabase()
		{
			_scc = new SingingClubClient();
		}

		public XmlDocument GetTable(string table, string eventkey)
		{
			return _scc.GetTable(table, eventkey);
		}

		public string GeneralStore(string database, XmlDocument doc, string action, string xml)
		{
			return _scc.GeneralStore(database, doc, "", action, xml);
		}

		public string GeneralStore(string database, XmlDocument doc, string eventkey, string action, string xml)
		{
			return _scc.GeneralStore(database, doc, eventkey, action, xml);
		}

		public string GetSingerHistoryForVenue(string VenueKey)
		{
			return _scc.GetSingerHistoryForVenue(VenueKey);
		}
	}
}
