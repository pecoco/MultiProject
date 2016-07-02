using ACT.Radardata;
using ACT.RadarViewOrder;
using MultiProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        override protected bool AnalyzeProc03(string logLine)
        {
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
                                RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "s"));
                                return false;
                            }
                        }
                        foreach (string mob in rm.a)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "a"));
                                return false;
                            }
                        }
                        foreach (string mob in rm.b)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "b"));
                                return false;
                            }
                        }
                        foreach (string mob in rm.etc)
                        {
                            if (mobName.ToString().IndexOf(mob) > -1)
                            {
                                RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "e"));
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
