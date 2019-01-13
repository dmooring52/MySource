using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;



namespace WcfSingingClub
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISingingClubRestService" in both code and config file together.
    [ServiceContract]
    public interface ISingingClubRestService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "xml/{table}/action/{action}/pageoffset/{pageoffset}/pagereturn/{pagereturn}/pageorderby/{pageorderby}/pagewhereclause/{pagewhereclause}")]
        List<SongItem> XMLData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause);
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/{table}/action/{action}/pageoffset/{pageoffset}/pagereturn/{pagereturn}/pageorderby/{pageorderby}/pagewhereclause/{pagewhereclause}")]
        string JSONData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause);
    }
}
