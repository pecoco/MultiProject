using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiProject
{
    public static class BasePlugin
    {
        public static SettingsSerializer xmlSettings;
        public static bool combatOn = false;

        private static DateTime time = new DateTime();//開始時間維持
        //private static System.Timers.Timer t =new System.Timers.Timer();
        public static void BattleTimerStart()
        {
            time = DateTime.Now;
           
            combatOn = true;
        }
        public static void BattleTimerEnd()
        {
           
            combatOn = false;
        }

    }
}
