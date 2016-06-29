
using MultiRadar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Radardata
{
    public static class RadardataInstance
    {
        //マスターデータのオリジナルが維持されているデーター
        public static RadarData radarData;
        public static void SetRadarData(string path)
        {
            radarData = new RadarData(path);
        }

        static string zone;
        public static string Zone
        {
            set { zone = value.Trim(); }
            get { return zone; }
        }
        public static List<string> ZoneList
        {
            get { return radarData.ZoneList(); }
        }
    }
}
