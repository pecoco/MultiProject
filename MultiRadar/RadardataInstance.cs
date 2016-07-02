using MultiRadar;
using System.Collections.Generic;
using System.Text;
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

        //現在のゾーン
        static StringBuilder zone;
        public static string Zone
        {
            set {
                zone = zone ?? new StringBuilder();
                zone.Length = 0;
                zone.Append(value.Trim());    
                 }
            get {
                zone = zone ?? new StringBuilder();
                return zone.ToString();
            }
        }
        public static List<string> ZoneList
        {
            get { return radarData.ZoneList(); }
        }
        public static void Initializer()
        {
            zone = new StringBuilder();
        }
    }
}
