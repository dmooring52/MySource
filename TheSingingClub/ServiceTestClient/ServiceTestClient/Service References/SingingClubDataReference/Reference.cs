﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceTestClient.SingingClubDataReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/WcfSingingClub")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SingingClubDataReference.ISingingClub")]
    public interface ISingingClub {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GetData", ReplyAction="http://tempuri.org/ISingingClub/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GetData", ReplyAction="http://tempuri.org/ISingingClub/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ISingingClub/GetDataUsingDataContractResponse")]
        ServiceTestClient.SingingClubDataReference.CompositeType GetDataUsingDataContract(ServiceTestClient.SingingClubDataReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ISingingClub/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<ServiceTestClient.SingingClubDataReference.CompositeType> GetDataUsingDataContractAsync(ServiceTestClient.SingingClubDataReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GeneralStore", ReplyAction="http://tempuri.org/ISingingClub/GeneralStoreResponse")]
        string GeneralStore(string table, string action, string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GeneralStore", ReplyAction="http://tempuri.org/ISingingClub/GeneralStoreResponse")]
        System.Threading.Tasks.Task<string> GeneralStoreAsync(string table, string action, string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GetSingerHistoryForVenue", ReplyAction="http://tempuri.org/ISingingClub/GetSingerHistoryForVenueResponse")]
        string GetSingerHistoryForVenue(string VenueKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISingingClub/GetSingerHistoryForVenue", ReplyAction="http://tempuri.org/ISingingClub/GetSingerHistoryForVenueResponse")]
        System.Threading.Tasks.Task<string> GetSingerHistoryForVenueAsync(string VenueKey);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISingingClubChannel : ServiceTestClient.SingingClubDataReference.ISingingClub, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SingingClubClient : System.ServiceModel.ClientBase<ServiceTestClient.SingingClubDataReference.ISingingClub>, ServiceTestClient.SingingClubDataReference.ISingingClub {
        
        public SingingClubClient() {
        }
        
        public SingingClubClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SingingClubClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SingingClubClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SingingClubClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public ServiceTestClient.SingingClubDataReference.CompositeType GetDataUsingDataContract(ServiceTestClient.SingingClubDataReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<ServiceTestClient.SingingClubDataReference.CompositeType> GetDataUsingDataContractAsync(ServiceTestClient.SingingClubDataReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public string GeneralStore(string table, string action, string xml) {
            return base.Channel.GeneralStore(table, action, xml);
        }
        
        public System.Threading.Tasks.Task<string> GeneralStoreAsync(string table, string action, string xml) {
            return base.Channel.GeneralStoreAsync(table, action, xml);
        }
        
        public string GetSingerHistoryForVenue(string VenueKey) {
            return base.Channel.GetSingerHistoryForVenue(VenueKey);
        }
        
        public System.Threading.Tasks.Task<string> GetSingerHistoryForVenueAsync(string VenueKey) {
            return base.Channel.GetSingerHistoryForVenueAsync(VenueKey);
        }
    }
}
