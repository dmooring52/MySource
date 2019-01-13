using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Diagnostics;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Web.Mvc;

using QueueStation.Models;

namespace QueueStation.Controllers
{
    public class MVCQueue : TSCQueue
    {
        private MvcList _singerList = new MvcList();
        public MvcList SingerList { get { return _singerList; } set { _singerList = value; } }

        public MVCQueue() : base() { }
        public MVCQueue(XmlNode node) : base(node) { }
    }
    public class MVCEvents : TSCEvents
    {
        private MvcList _venueList = new MvcList();
        public MvcList VenueList { get { return _venueList; } set { _venueList = value; } }
        public MVCEvents() : base() { }
        public MVCEvents(XmlNode node) : base(node) { }
    }
    public class MVCVenues : TSCVenues
    {
        private string _command = "";
        public string command { get { return _command; } set { _command = value; } }
        public MVCVenues() : base() { }
        public MVCVenues(XmlNode node) : base(node) { }
    }
    public class MVCSongs : TSCSongs
    {
        private string _command = "";
        public string command { get { return _command; } set { _command = value; } }
        public MVCSongs() : base() { }
        public MVCSongs(XmlNode node) : base(node) { }
    }
    public class MVCSingersSongs : TSCSingersSongs
    {
        private string _command = "";
        private List<TSCSongs> _tags = new List<TSCSongs>();
        public string command { get { return _command; } set { _command = value; } }
        public List<TSCSongs> tags { get { return _tags; } set { _tags = value; } }
        public MVCSingersSongs() : base() { }
        public MVCSingersSongs(XmlNode node) : base(node) { }
    }
    public class MVCSingers : TSCSingers
    {
        private string _command = "";
        private List<TSCSongs> _tags = new List<TSCSongs>();
        public string command { get { return _command; } set { _command = value; } }
        public List<TSCSongs> tags { get { return _tags; } set { _tags = value; } }
        public MVCSingers() : base() { }
        public MVCSingers(XmlNode node) : base(node) { }
    }
    public class SingerRound
    {
        public int round {get; set;}
        public string command { get; set; }
        public string singerkey { get; set; }
        public List<string> singerlist { get; set; }
    }
    public static class QueueRoundData
    {
        public static List<MVCQueue> GetRounds(XmlDocument doc, int round)
        {
            List<MVCQueue> qlist = new List<MVCQueue>();
            Models.MvcList list = GetRound(doc, round);
            foreach (MVCQueue item in list)
            {
                qlist.Add(item);
            }
            return qlist;
        }

        public static List<MVCQueue> GetRounds(string xml, int round)
        {
            List<MVCQueue> qlist = new List<MVCQueue>();
            Models.MvcList list = GetRound(xml, round);
            foreach (MVCQueue item in list)
            {
                qlist.Add(item);
            }
            return qlist;
        }

        public static QueueStation.Models.MvcList GetRound(XmlDocument doc, int QueueRound)
        {
            QueueStation.Models.MvcList roundx = new Models.MvcList();
            XmlNodeList nodes = doc.SelectNodes("/Root/Data");
            foreach (XmlNode node in nodes)
            {
                if (Utility.GetXmlInteger(node, "QueueRound") == QueueRound)
                    roundx.Add(new MVCQueue(node));
            }
            return roundx;
        }
        public static QueueStation.Models.MvcList GetRound(string xml, int QueueRound)
        {
            XmlDocument doc = new XmlDocument();
            if (xml != null && xml is string)
            {
                doc.LoadXml(xml);
            }
            return GetRound(doc, QueueRound);
        }
    }

    public class RootData
    {
        public string VenueKey { get; set; }
        public string VenueName { get; set; }
        public string EventKey { get; set; }
        public string EventName { get; set; }

        public IEnumerable<SelectListItem> OnDeckList  { get; set; }

        public string OnDeckSelection { get; set; }
        public IEnumerable<SelectListItem> EventList { get; set; }
        public string EventSelection { get; set; }

        public RootData() : this("", "") { }
        public RootData(string venuekey, string eventkey) { VenueKey = venuekey; EventKey = eventkey; }
    }
    public class EventData
    {
        private XmlDocument _doc = null;
        private string _venuekey = "";
        private string _eventkey = "";
        public XmlDocument doc {get { return _doc;} set { _doc = value;}}
        public string venuekey { get { return _venuekey; } set { _venuekey = value; } }
        public string eventkey { get { return _eventkey; } set { _eventkey = value; } }
        public EventData() { }
        public EventData(string venueKey, string eventKey, XmlDocument eventDoc)
        {
            initialize(venueKey, eventKey, eventDoc);
        }

        public void initialize(string venueKey, string eventKey, XmlDocument eventDoc)
        {
            doc = eventDoc;
            venuekey = venueKey;
            eventkey = eventKey;
        }

        public MVCEvents GetEvent()
        {
            if (doc != null && venuekey != null && eventkey != null && venuekey.Trim().Length > 0 && eventkey.Trim().Length > 0)
            {
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[VenueKey=\"{0}\" and EventKey=\"{1}\"]", venuekey, eventkey));
                if (node != null)
                    return new MVCEvents(node);
            }
            return new MVCEvents();
        }
    }
    public class SongData
    {
        private XmlDocument _doc = null;
        private string _command = "";
        private string _artistkey = "";
        private string _titlekey = "";
        private string _diskkey = "";
        public XmlDocument doc { get { return _doc; } set { _doc = value; } }
        public string command { get { return _command; } set { _command = value; } }
        public string artistkey { get { return _artistkey; } set { _artistkey = value; } }
        public string titlekey { get { return _titlekey; } set { _titlekey = value; } }
        public string diskkey { get { return _diskkey; } set { _diskkey = value; } }
        public SongData() { }
        public SongData(string command, string artistKey, string titleKey, string diskKey, XmlDocument songDoc)
        {
            initialize(command, artistKey, titleKey, diskKey, songDoc);
        }

        public void initialize(string Command, string artistKey, string titleKey, string diskKey, XmlDocument songDoc)
        {
            doc = songDoc;
            command = Command;
            artistkey = artistKey;
            titlekey = titleKey;
            diskkey = diskKey;
        }

        public MVCSongs GetSong()
        {
            if (doc != null && 
                (
                (artistkey != null && artistkey.Trim().Length > 0 && artistkey.Trim().Length > 0) ||
                (titlekey != null && titlekey.Trim().Length > 0 && titlekey.Trim().Length > 0) ||
                (diskkey != null && diskkey.Trim().Length > 0 && diskkey.Trim().Length > 0)
                )
            )
            {
                XmlNode node = doc.SelectSingleNode(
                    string.Format("/Root/Data[Artisk=\"{0}\" and Title=\"{1}\" and Disk=\"{2}\"]", Utility.NotNullString(artistkey), Utility.NotNullString(titlekey), Utility.NotNullString(diskkey))
                );
                if (node != null)
                    return new MVCSongs(node);
            }
            return new MVCSongs();
        }
    }
    public class SingerData
    {
        private XmlDocument _doc = null;
        private string _command = "";
        private string _singerkey = "";
        public XmlDocument doc { get { return _doc; } set { _doc = value; } }
        public string command { get { return _command; } set { _command = value; } }
        public string singerkey { get { return _singerkey; } set { _singerkey = value; } }
        public SingerData() { }
        public SingerData(string command, string singerKey, XmlDocument singerDoc)
        {
            initialize(command, singerKey, singerDoc);
        }

        public void initialize(string Command, string singerKey, XmlDocument singerDoc)
        {
            doc = singerDoc;
            command = Command;
            singerkey = singerKey;
        }

        public MVCSingers GetSinger()
        {
            if (doc != null && singerkey != null && singerkey.Trim().Length > 0 && singerkey.Trim().Length > 0)
            {
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[SingerKey=\"{0}\"]", singerkey));
                if (node != null)
                    return new MVCSingers(node);
            }
            return new MVCSingers();
        }
    }
    public class VenueData
    {
        private XmlDocument _doc = null;
        private string _command = "";
        private string _venuekey = "";
        public XmlDocument doc { get { return _doc; } set { _doc = value; } }
        public string command { get { return _command; } set { _command = value; } }
        public string venuekey { get { return _venuekey; } set { _venuekey = value; } }
        public VenueData() { }
        public VenueData(string command, string venueKey, XmlDocument venueDoc)
        {
            initialize(command, venueKey, venueDoc);
        }

        public void initialize(string Command, string venueKey, XmlDocument venueDoc)
        {
            doc = venueDoc;
            command = Command;
            venuekey = venueKey;
        }

        public MVCVenues GetVenue()
        {
            if (doc != null && venuekey != null && venuekey.Trim().Length > 0 && venuekey.Trim().Length > 0)
            {
                XmlNode node = doc.SelectSingleNode(string.Format("/Root/Data[VenueKey=\"{0}\"]", venuekey));
                if (node != null)
                    return new MVCVenues(node);
            }
            return new MVCVenues();
        }
    }

    public class InitData
    {
        public string VenuesXml { get; set; }
        public string EventsXml { get; set; }
        public string SingersXml { get; set; }
        public string RoundXml { get; set; }
        public string SongsXml { get; set; }
    }
    public class InitHelper
    {
        public void InitialLoad()
        {
            InitData d = new InitData();
            SingingClubClient client = new SingingClubClient();
            client.Open();
            d.VenuesXml = client.GeneralStore("TSCVenues", "GET", (new TSCVenues()).GetDataXml());
            d.EventsXml = client.GeneralStore("TSCEvents", "GET", (new TSCEvents()).GetDataXml());
            d.SingersXml = client.GeneralStore("TSCSingers", "GET", (new TSCSingers()).GetDataXml());
            client.Close();
            string eventvenueKey = GetLatestEventKey(d);
            InitialLoad(d, eventvenueKey);
        }

        public void InitialLoad(InitData d, string eventvenueKey)
        {
            SingingClubClient client = new SingingClubClient();
            client.Open();
            d.RoundXml = "";
            string eventkey = "";
            string venuekey = "";
            if (eventvenueKey.Trim().Length > 0)
            {
                string[] keys = eventvenueKey.Split('\t');
                if (keys.Length == 2)
                {
                    eventkey = keys[0].Trim();
                    venuekey = keys[1].Trim();
                }
                if (eventkey.Length > 0 && venuekey.Length > 0)
                {
                    SessionBag.Current.EventKey = eventkey;
                    SessionBag.Current.VenueKey = venuekey;
                    TSCQueue q = new TSCQueue();
                    q.EventKey = eventkey;
                    q.QueueRound = -1;
                    d.RoundXml = client.GeneralStore("TSCQueue", "GET", q.GetDataXml());
                }
            }
            client.Close();
            XmlDocument VenuesXml = new XmlDocument();
            XmlDocument EventsXml = new XmlDocument();
            XmlDocument SingersXml = new XmlDocument();
            XmlDocument RoundXml = new XmlDocument();
            XmlDocument VenuesXmlReference = new XmlDocument();
            XmlDocument EventsXmlReference = new XmlDocument();
            XmlDocument SingersXmlReference = new XmlDocument();
            XmlDocument RoundXmlReference = new XmlDocument();

            if (d.VenuesXml != null && d.VenuesXml.Trim().Length > 0)
                VenuesXml.LoadXml(d.VenuesXml);
            if (d.EventsXml != null && d.EventsXml.Trim().Length > 0)
                EventsXml.LoadXml(d.EventsXml);
            if (d.SingersXml != null && d.SingersXml.Trim().Length > 0)
                SingersXml.LoadXml(d.SingersXml);
            if (d.RoundXml != null && d.RoundXml.Trim().Length > 0)
                RoundXml.LoadXml(d.RoundXml);
            if (d.VenuesXml != null && d.VenuesXml.Trim().Length > 0)
                VenuesXmlReference.LoadXml(d.VenuesXml);
            if (d.EventsXml != null && d.EventsXml.Trim().Length > 0)
                EventsXmlReference.LoadXml(d.EventsXml);
            if (d.SingersXml != null && d.SingersXml.Trim().Length > 0)
                SingersXmlReference.LoadXml(d.SingersXml);
            if (d.RoundXml != null && d.RoundXml.Trim().Length > 0)
                RoundXmlReference.LoadXml(d.RoundXml);

            SessionBag.Current.VenuesXml = VenuesXml;
            SessionBag.Current.EventsXml = EventsXml;
            SessionBag.Current.SingersXml = SingersXml;
            SessionBag.Current.RoundXml = RoundXml;
            SessionBag.Current.VenuesXmlReference = VenuesXmlReference;
            SessionBag.Current.EventsXmlReference = EventsXmlReference;
            SessionBag.Current.SingersXmlReference = SingersXmlReference;
            SessionBag.Current.RoundXmlReference = RoundXmlReference;
        }
        private string GetLatestEventKey(InitData d)
        {
            try
            {
                string xml = d.EventsXml;
                if (xml != null && xml.Trim().Length > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList nodes = doc.SelectNodes("/Root/Data");
                    string eventkey = "";
                    string venuekey = "";
                    DateTime maxdt = DateTime.MinValue;
                    foreach (XmlNode node in nodes)
                    {
                        DateTime dt = Utility.GetXmlDateTime(node, "EventDate");
                        if (dt > maxdt)
                        {
                            string evt = Utility.GetXmlString(node, "EventKey");
                            string vnu = Utility.GetXmlString(node, "VenueKey");
                            if (evt.Trim().Length > 0)
                            {
                                maxdt = dt;
                                eventkey = evt.Trim();
                                venuekey = vnu.Trim();
                            }
                        }
                    }
                    if (maxdt > DateTime.MinValue)
                        return eventkey + '\t' + venuekey;
                }
            }
            catch { }
            return "";
        }
    }
    public class LaunchForm
    {
        private const string KBPlayer = "kbplayer.exe";

        public string _path = "";

        public LaunchForm(string path)
        {
            string userpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (path.Trim().Length > 2 && (path.Trim().ToLower().Substring(path.Length - 3) == "cdg" || path.Trim().ToLower().Substring(path.Length - 3) == "bin"))
            {
                path = path.Replace('/', '\\');
                int ix = path.ToLower().IndexOf(@"\skydrive");
                if (ix < 0)
                    ix = path.ToLower().IndexOf(@"\onedrive");
                if (ix > 0)
                {
                    path = userpath + path.Substring(ix);
                }
            }
            _path = path;
        }

        public void Launch()
        {
            if (_path.Contains('\\') && _path.Trim().Length > 2 && (_path.Trim().ToLower().Substring(_path.Length - 3) == "cdg" || _path.Trim().ToLower().Substring(_path.Length - 3) == "bin"))
            {
                KillPlayer(KBPlayer);
            }
            if (_path.Trim().Length > 0)
                Process.Start(_path);
        }

        private void KillPlayer(string processName)
        {
            Process playerProcess = null;
            Process[] list = Process.GetProcessesByName(processName);
            foreach (Process process in list)
            {
                if (process.ProcessName == processName)
                {
                    playerProcess = process;
                    break;
                }
            }
            if (playerProcess != null)
            {
                playerProcess.Kill();
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    public class OnDeck : IComparable
    {
        public string SingerKey { get; set; }
        public int QueueRound { get; set; }
        public int QueueOrder { get; set; }

        public OnDeck() : this("", 0, 0){}

        public OnDeck(string key, int round, int order)
        {
            SingerKey = key;
            QueueRound = round;
            QueueOrder = order;
        }

        int IComparable.CompareTo(Object obj)
        {
            int result = 0;
            OnDeck a = obj as OnDeck;
            if (a != null)
            {
                result = a.QueueRound.CompareTo(this.QueueRound);
                if (result == 0)
                    result = a.QueueOrder.CompareTo(this.QueueOrder);
            }
            return result;
        }

        public static bool operator ==(OnDeck a, OnDeck b)
        {
            if (System.Object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return FieldsEqual(a, b);
        }

        public static bool operator !=(OnDeck a, OnDeck b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            OnDeck a = obj as OnDeck;
            if (a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public bool Equals(OnDeck a)
        {
            if ((object)a == null)
                return false;
            return FieldsEqual(this, a);
        }

        public static bool FieldsEqual(OnDeck a, OnDeck b)
        {
            return (
                (a.SingerKey == b.SingerKey) &&
                (a.QueueRound == b.QueueRound) &&
                (a.QueueOrder == b.QueueOrder) &&
                (true)
                );
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool KeyEquals(OnDeck a)
        {
            if ((object)a == null)
                return false;
            return (
            (a.SingerKey.Trim().ToLower() == SingerKey.Trim().ToLower())
            );
        }
    }
    public class clash
    {
        private readonly string PasswordHash = "F0rAll!TheW0rl9";
        private readonly string SaltKey = "Fant0zyf00tb0ll";
        private readonly string VIKey = "@1B2c3D4e5F6g7H8";
        private readonly string myKeyGuest = "w1W3XC4gDYazedwM43+1ZQ==";
        private readonly string myKeyAdmin = "WGNTII+JKtMJEwVh5ueCCg==";
        private bool _HasCredentials = false;
        private bool _IsKj = false;

        public bool HasCredentials { get { return _HasCredentials; } }
        public bool IsKj { get { return _IsKj; } }

        public clash(string plainText)
        {
            Encrypt(plainText);
        }
        
        public void Encrypt(string plainText)
        {
            if (plainText == null || plainText.Trim().Length == 0)
                return;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            string key = Convert.ToBase64String(cipherTextBytes);
            if (key == myKeyAdmin)
            {
                _HasCredentials = true;
                _IsKj = true;
            }
            if (key == myKeyGuest)
            {
                _HasCredentials = true;
            }
        }
        /*
        public string TestEncrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
        */
        public string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}