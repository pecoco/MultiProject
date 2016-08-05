using System;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using System.IO;
using MultiProjectTools;
using System.Timers;
using MultiProject;

namespace MultiRadar
{
    public partial class RadarSettingControl : UserControl, IActPluginV1
    {
        string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\PluginMultiRadar.config.xml");
        private Label lbStatus;

        private ClockTimer clock;

        partial void oFormActMain_BeforeLogLineRead(bool isImport, LogLineEventArgs actionInfo);
        partial void oFormActMain_AfterCombatAction(bool isImport, CombatActionEventArgs actionInfo);
        partial void oFormActMain_OnCombatStart(bool isImport, CombatToggleEventArgs encount);
        partial void oFormActMain_OnCombatEnd(bool isImport, CombatToggleEventArgs encount);

        partial void oClock_Action(object sender, ElapsedEventArgs e);

        partial void ViewWindow();
        partial void CloseWindow();

        partial void LoadSettings();
        partial void SaveSettings();

        public RadarSettingControl()
        {
            InitializeComponent();
            ActHelper.Initialize();
            clock = new ClockTimer();
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            lbStatus = pluginStatusText;   // Hand the status label's reference to our local var
            pluginScreenSpace.Controls.Add(this);   // Add this UserControl to the tab ACT provides
            this.Dock = DockStyle.Fill; // Expand the UserControl to fill the tab's client space
            MultiProject.BasePlugin.xmlSettings = new SettingsSerializer(this); // Create a new settings serializer and pass it this instance

            LoadSettings();
            ActGlobals.oFormActMain.AfterCombatAction += new CombatActionDelegate(oFormActMain_AfterCombatAction);
            ActGlobals.oFormActMain.BeforeLogLineRead += new LogLineEventDelegate(oFormActMain_BeforeLogLineRead);
            ActGlobals.oFormActMain.OnCombatStart += new CombatToggleEventDelegate(oFormActMain_OnCombatStart);
            ActGlobals.oFormActMain.OnCombatEnd += new CombatToggleEventDelegate(oFormActMain_OnCombatEnd);
            ViewWindow();

            clock.watchTimer = new System.Timers.Timer()
            {
                Interval = 200,//Settings.Default.ParameterRefreshRate,
                Enabled = false
            };

            clock.watchTimer.Elapsed += oClock_Action;
            clock.watchTimer.Start();

            lbStatus.Text = "Plugin Started. (#^^#).";
        }

        public void DeInitPlugin()
        {
            ActGlobals.oFormActMain.AfterCombatAction -= oFormActMain_AfterCombatAction;
            ActGlobals.oFormActMain.BeforeLogLineRead -= oFormActMain_BeforeLogLineRead;
            ActGlobals.oFormActMain.OnCombatStart -= oFormActMain_OnCombatStart;
            ActGlobals.oFormActMain.OnCombatEnd -= oFormActMain_OnCombatEnd;
           
            SaveSettings();

            if (clock.watchTimer != null)
            {
                clock.watchTimer.Elapsed -= oClock_Action;
                clock.watchTimer.Stop();
                clock.watchTimer.Dispose();
                clock.watchTimer = null;
            }
            CloseWindow();
            lbStatus.Text = "Plugin Exited";
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        
            dynamic list = ActHelper.ff14PluginScancombat.GetCombatantList();
            foreach (dynamic item in list.ToArray())
            {
                if (item == null)
                {
                    continue;
                }

                var combatant = new Combatant();

                combatant.Name = (string)item.Name;
                combatant.ID = (uint)item.ID;
                combatant.Job = (int)item.Job;
                combatant.CurrentMP = (int)item.CurrentMP;
                combatant.MaxMP = (int)item.MaxMP;
                combatant.CastTargetID = (int)item.CastTargetID;
                combatant.IsCasting = (bool)item.IsCasting;
                combatant.OwnerID = (uint)item.OwnerID;
                combatant.Name = (string)item.Name;
                combatant.type = (byte)item.type;
                combatant.Level = (int)item.Level;
                combatant.CurrentHP = (int)item.CurrentHP;
                combatant.MaxHP = (int)item.MaxHP;
                combatant.CurrentTP = (int)item.CurrentTP;
                combatant.PosX = (float)item.PosX;
                combatant.PosY = (float)item.PosY;
            }
            
        }
    }
}

