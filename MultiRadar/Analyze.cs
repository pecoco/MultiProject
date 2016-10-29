using ACT.Radardata;
using ACT.RadarViewOrder;
using MultiProject;
using System.Text;

namespace MultiRadar
{
    class Analyze:AnalyzeBase
    {
        StringBuilder mobName = new StringBuilder();
        override public bool AnalyzeLogLine(string logLine)
        {
            if (base.AnalyzeLogLine(logLine))
            {
                return true;
            }
            //それ以外の解析
            return false;
        }
        override protected string GetZoneName(string logLine)
        {
            RadardataInstance.Zone= base.GetZoneName(logLine);
            return RadardataInstance.Zone;
        }
        override protected bool AnalyzeProc00(string logLine)
        {
//            if (logLine.IndexOf("魔土器") > 0)
//            {

//            }
            return false;
        }


        override protected bool AnalyzeProc03(string logLine)
        {
            mobName.Length = 0;
            mobName.Append(logLine.Substring(hitIndex + "03:Added new combatant ".Length));

            foreach (string zoneList in RadardataInstance.radarData.ZoneList())
            {
                if (zoneList == RadardataInstance.Zone)
                {
                    RadardataInstance.radarData.getMobList(zoneList);
                    ZoneMobData rm = RadardataInstance.radarData.getMobList(zoneList);
                    if (rm != null)
                    {
                        foreach (string mob in rm.s)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMobfromLog(new RadarViewOrder.HitMobdata(mob, "s"));
                                RadarViewOrder.LuckUpS = true;
                                return false;
                            }
                        }
                        foreach (string mob in rm.a)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMobfromLog(new RadarViewOrder.HitMobdata(mob, "a"));
                                return false;
                            }
                        }
                        foreach (string mob in rm.b)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMobfromLog(new RadarViewOrder.HitMobdata(mob, "b"));
                                return false;
                            }
                        }
                        foreach (string mob in rm.etc)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMobfromLog(new RadarViewOrder.HitMobdata(mob, "e"));
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
         
        }

    }
}
