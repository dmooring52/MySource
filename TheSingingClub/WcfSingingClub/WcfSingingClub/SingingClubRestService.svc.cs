using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfSingingClub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SingingClubRestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SingingClubRestService.svc or SingingClubRestService.svc.cs at the Solution Explorer and start debugging.
    public class SingingClubRestService : ISingingClubRestService
    {
        private GeneralStore gs = null;

        public SingingClubRestService()
        {
            gs = new GeneralStore();
        }
        public List<SongItem> XMLData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
        {
            return gs.XMLData(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
        }
        public string JSONData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
        {
            return gs.JSONData(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
        }
    }
}
