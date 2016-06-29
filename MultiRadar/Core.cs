
using ACT.Radardata;
using ACT.RadarForm;
using ACT.RadarViewOrder;
using Advanced_Combat_Tracker;
using MultiProject;
using MultiProject.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace MultiRadar
{
    public partial class RadarSettingControl
    {
        protected RadarForm radarForm;


        partial void oFormActMain_OnCombatStart(bool isImport, CombatToggleEventArgs encount)
        {
            //encount.encounter.StartTime();
            BasePlugin.BattleTimerStart();
            BasePlugin.combatOn = true;
        }
        partial void oFormActMain_OnCombatEnd(bool isImport, CombatToggleEventArgs encount)
        {
            BasePlugin.combatOn = false;
            BasePlugin.BattleTimerEnd();
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
            if (ckRadarVisible.Checked)
            {

                radarForm = new RadarForm();
                radarForm.Left = int.Parse(textRadarXpos.Text);
                radarForm.Top = int.Parse(textRadarYpos.Text);
                radarForm.CallbackSaveSetting = SaveSettings;

                //rbRederModeFull

                //controllForm.CallbackResize = RadarWindowResize;
                RadardataInstance.SetRadarData(textRederDataPath.SelectedText + "RadarData.xml");

                //xmlSettings.AddControlSetting(rbRederModeFull.Name, rbRederModeFull);
                //xmlSettings.AddControlSetting(rbRederModeSelect.Name, rbRederModeSelect);
                ReSetComboRederZoneItem(false);


                RadarViewOrder.AllRadarMode = rbRederModeFull.Checked;



                radarForm.Show();
            }


        }
        partial void CloseWindow()
        {
            if (radarForm != null)
            {
                radarForm.Hide();
                radarForm.Dispose();
            }

        }


    }
}
