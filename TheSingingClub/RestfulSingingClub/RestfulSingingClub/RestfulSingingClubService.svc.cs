using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Services;

namespace RestfulSingingClub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestfulSingingClubService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestfulSingingClubService.svc or RestfulSingingClubService.svc.cs at the Solution Explorer and start debugging.
    public class RestfulSingingClubService : IRestfulSingingClubService
    {
        public List<SongItem> GetSongList()
        {
            return SongItems.Instance.SongList;
        }
    }
}
