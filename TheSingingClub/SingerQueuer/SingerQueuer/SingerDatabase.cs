using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingerQueuer.SingingClubDataReference;

namespace SingerQueuer
{
	public class SingerDatabase
	{
        
        private WcfSingingClub.SingingClub _scc = null;
		public SingerDatabase()
		{
            _scc = new WcfSingingClub.SingingClub();
		}

		public string GeneralStore(string database, string action, string xml)
		{
			return _scc.GeneralStore(database, action, xml);
		}

		public string GetSingerHistoryForVenue(string VenueKey)
		{
			return _scc.GetSingerHistoryForVenue(VenueKey);
		}
	}
}
