using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;


namespace RestfulSingingClub
{
    [DataContract]
    public class SongItem
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Artist { get; set; }
        [DataMember]
        public string Disk { get; set; }
        [DataMember]
        public string IsHelper { get; set; }
        [DataMember]
        public string IsDuet { get; set; }
        [DataMember]
        public string DuetArtist { get; set; }
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public string OneDrive { get; set; }
    }
    public partial class SongItems
    {
        private static readonly SongItems _instance = new SongItems();
        private SongItems() { }
        public static SongItems Instance
        {
            get { return _instance; }
        }
        public List<SongItem> SongList
        {
            get { return songitems; }
        }
        private List<SongItem> songitems = new List<SongItem>()
        {
            new SongItem() { Title="Title1", Artist="Artist1", Disk="Disk1", DuetArtist="Duet1", FilePath="c:/temp", IsDuet="Yes", IsHelper="No", OneDrive="http://onedrive"}
        };
    }
}