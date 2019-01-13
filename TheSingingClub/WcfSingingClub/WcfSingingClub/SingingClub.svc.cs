using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using XmlUtility;

namespace WcfSingingClub
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SingingClub" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select SingingClub.svc or SingingClub.svc.cs at the Solution Explorer and start debugging.
	public class SingingClub : ISingingClub
	{
        private GeneralStore gs = null;

		public SingingClub()
		{
            gs = new GeneralStore();
		}

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

		[WebGet]
		public string GetSingerHistoryForVenue(string VenueKey)
		{
            return gs.GetSingerHistoryForVenue(VenueKey);
		}

		[WebGet]
		public string GeneralStore(string table, string action, string xml)
		{
            return gs.RunStore(table, action, xml);
		}
	}
}
