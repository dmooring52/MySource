using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq;
using System.Web;

namespace WcfSingingClub
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "SongItem", Namespace = "http://schemas.datacontract.org/2004/07/WcfSingingClub")]
    public partial class SongItem : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ArtistField;

        private string DiskField;

        private string DuetArtistField;

        private string FilePathField;

        private string IsDuetField;

        private string IsHelperField;

        private string OneDriveField;

        private string TitleField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Artist
        {
            get
            {
                return this.ArtistField;
            }
            set
            {
                this.ArtistField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Disk
        {
            get
            {
                return this.DiskField;
            }
            set
            {
                this.DiskField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DuetArtist
        {
            get
            {
                return this.DuetArtistField;
            }
            set
            {
                this.DuetArtistField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FilePath
        {
            get
            {
                return this.FilePathField;
            }
            set
            {
                this.FilePathField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IsDuet
        {
            get
            {
                return this.IsDuetField;
            }
            set
            {
                this.IsDuetField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IsHelper
        {
            get
            {
                return this.IsHelperField;
            }
            set
            {
                this.IsHelperField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OneDrive
        {
            get
            {
                return this.OneDriveField;
            }
            set
            {
                this.OneDriveField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this.TitleField;
            }
            set
            {
                this.TitleField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ISingingClubRestService")]
public interface ISingingClubRestService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISingingClubRestService/XMLData", ReplyAction = "http://tempuri.org/ISingingClubRestService/XMLDataResponse")]
    WcfSingingClub.SongItem[] XMLData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISingingClubRestService/XMLData", ReplyAction = "http://tempuri.org/ISingingClubRestService/XMLDataResponse")]
    System.Threading.Tasks.Task<WcfSingingClub.SongItem[]> XMLDataAsync(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISingingClubRestService/JSONData", ReplyAction = "http://tempuri.org/ISingingClubRestService/JSONDataResponse")]
    string JSONData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ISingingClubRestService/JSONData", ReplyAction = "http://tempuri.org/ISingingClubRestService/JSONDataResponse")]
    System.Threading.Tasks.Task<string> JSONDataAsync(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface ISingingClubRestServiceChannel : ISingingClubRestService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class SingingClubRestServiceClient : System.ServiceModel.ClientBase<ISingingClubRestService>, ISingingClubRestService
{

    public SingingClubRestServiceClient()
    {
    }

    public SingingClubRestServiceClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    {
    }

    public SingingClubRestServiceClient(string endpointConfigurationName, string remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public SingingClubRestServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public SingingClubRestServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    {
    }

    public WcfSingingClub.SongItem[] XMLData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
    {
        return base.Channel.XMLData(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
    }

    public System.Threading.Tasks.Task<WcfSingingClub.SongItem[]> XMLDataAsync(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
    {
        return base.Channel.XMLDataAsync(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
    }

    public string JSONData(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
    {
        return base.Channel.JSONData(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
    }

    public System.Threading.Tasks.Task<string> JSONDataAsync(string table, string action, string pageoffset, string pagereturn, string pageorderby, string pagewhereclause)
    {
        return base.Channel.JSONDataAsync(table, action, pageoffset, pagereturn, pageorderby, pagewhereclause);
    }
}
