using Advanced_Combat_Tracker;
using System;

namespace MultiProject
{
    public static class BasePlugin
    {
        public static SettingsSerializer xmlSettings;
        public static bool combatOn = false;

        private static DateTime time = new DateTime();//開始時間維持
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
