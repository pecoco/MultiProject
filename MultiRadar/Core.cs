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

                RadarViewOrder.FontSize = (int)numFontSize.Value > 5 ? (int)numFontSize.Value : 6;
                RadarViewOrder.Opacity = (int)numOpacity.Value> 40 ? (int)numOpacity.Value : 100;

                int valueX = 0;
                int valueY = 0;

                int.TryParse(textRadarXpos.Text, out valueX);
                int.TryParse(textRadarYpos.Text, out valueY);

                radarForm.SetWindowRect( new Rect(valueX, valueY, 460, 460));
                radarForm.CallbackSaveSetting = SaveSettings;
                radarForm.Show();

                RadarViewOrder.SoundEnable = ckRadarSE.Checked;
                alertForm = new AlertForm();
                alertForm.CallbackSaveSetting = SaveSettings;

                int.TryParse(textAlertXpos.Text, out valueX);
                int.TryParse(textAlertYpos.Text, out valueY);
                alertForm.Left = valueX;
                alertForm.Top = valueY;

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
