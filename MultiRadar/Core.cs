using Advanced_Combat_Tracker;
using MultiProject;
using MultiProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MultiRadar
{
    public partial class RadarSettingControl
    {
        partial void oFormActMain_OnCombatStart(bool isImport, CombatToggleEventArgs encount)
        {
            //encount.encounter.StartTime();
            BasePrugin.BattleTimerStart();
            BasePrugin.combatOn = true;
        }
        partial void oFormActMain_OnCombatEnd(bool isImport, CombatToggleEventArgs encount)
        {
            BasePrugin.combatOn = false;
            BasePrugin.BattleTimerEnd();
        }
        partial void oFormActMain_AfterCombatAction(bool isImport, CombatActionEventArgs actionInfo)
        {
            //logform.addLog("CA:" + actionInfo.combatAction.Attacker + ":" + actionInfo.combatAction.AttackType);
        }

        partial void oFormActMain_BeforeLogLineRead(bool isImport, LogLineEventArgs actionInfo)
        {

        }

        partial void oClock_Action(object sender, ElapsedEventArgs e)
        {
            ActData.AllCharactor = ActHelper.GetCombatantList();
            if (ActData.AllCharactor.Count > 0)
            {

                //CallSetLine();
            }
        }
        partial void ViewWindow()
        {



        }
        partial void CloseWindow()
        {


        }


    }
}
