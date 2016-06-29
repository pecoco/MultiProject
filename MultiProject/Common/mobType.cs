using System.Collections.Generic;




    public enum MobType
    {
        S,
        A,
        B,
        ETC
    };

    public class Zones
    {
        public List<ZoneMobData> zones = new List<ZoneMobData>();
    }

    public class ZoneMobData
    {
        public string name;
        public string nameJp;
        public List<string> s = new List<string>();
        public List<string> a = new List<string>();
        public List<string> b = new List<string>();
        public List<string> etc = new List<string>();
    }
