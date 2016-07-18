using ACT.Radardata;

using ACT.RadarViewOrder;
using Advanced_Combat_Tracker;
using MultiProject;
using MultiProject.Common;
using System.Drawing;
using System.Timers;
using System.Windows;
using Wpf.RadarWindow;

namespace MultiRadar
{
    public partial class RadarSettingControl
    {
        protected MainWindow radarForm;
        protected AlertForm alertForm;
        Analyze analyze = new Analyze();

        partial void oFormActMain_OnCombatStart(bool isImport, CombatToggleEventArgs encount)
        {
           
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
        }

        partial void oFormActMain_BeforeLogLineRead(bool isImport, LogLineEventArgs actionInfo)
        {
            if (analyze.AnalyzeLogLine(actionInfo.logLine))
            {
                switch (analyze.change)
                {
                    case AnalyzeBase.ChangeParameter.ChangedZone:
                        break;
                }
            }
        }

        partial void oClock_Action(object sender, ElapsedEventArgs e)
        {
            ActData.AllCharactor = ActHelper.GetCombatantList();
            if (ActData.AllCharactor.Count > 0)
            {

            }
 
        }
        partial void ViewWindow()
        {
            if (ckRadarVisible.Checked)
            {
                
                radarForm = new MainWindow();

                radarForm.isRadarSelect = rbRederModeSelect.Checked;
                radarForm.isRadarAntiParsonal = rbRadarTaegetPlayer.Checked;

                radarForm.Show();
                radarForm.SetWindowRect( new Rect(int.Parse(textRadarXpos.Text), int.Parse(textRadarYpos.Text), 300,300));
                /*radarForm.Left = int.Parse(textRadarXpos.Text);
                radarForm.Top = int.Parse(textRadarYpos.Text);*/
                radarForm.CallbackSaveSetting = SaveSettings;
                
                RadarViewOrder.SoundEnable = ckRadarSE.Checked;
                alertForm = new AlertForm();
                alertForm.CallbackSaveSetting = SaveSettings;
                alertForm.Left = int.Parse(textAlertXpos.Text);
                alertForm.Top = int.Parse(textAlertYpos.Text);

                alertForm.Show();

                RadardataInstance.SetRadarData(textRadarDataPath.Text + "\\RadarData.xml");
                ReSetComboRadarZoneItem(false);
            }
        }
        partial void CloseWindow()
        {
            
            if (radarForm != null)
            {
                radarForm.Hide();
            }            
            if (alertForm != null)
            {
                alertForm.Hide();
                alertForm.Dispose();
            }
        }
    }
}
