using ACT.RadarViewOrder;
using MultiProject;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MultiRadar
{
    public partial class RadarSettingControl
    {
        private void onInputCheck(ref KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        partial void LoadSettings()
        {
            //Radar(Map)(Contrall Window)
            BasePlugin.xmlSettings.AddControlSetting(textRadarXpos.Name, textRadarXpos);
            BasePlugin.xmlSettings.AddControlSetting(textRadarYpos.Name, textRadarYpos);
            BasePlugin.xmlSettings.AddControlSetting(ckRadarVisible.Name, ckRadarVisible);

            BasePlugin.xmlSettings.AddControlSetting(rbRederModeFull.Name, rbRederModeFull);
            BasePlugin.xmlSettings.AddControlSetting(rbRederModeSelect.Name, rbRederModeSelect);

            BasePlugin.xmlSettings.AddControlSetting(textAlertXpos.Name, textAlertXpos);
            BasePlugin.xmlSettings.AddControlSetting(textAlertYpos.Name, textAlertYpos);
            //SE
            BasePlugin.xmlSettings.AddControlSetting(ckRadarSE.Name, ckRadarSE);

            if (textRadarDataPath.Text == "")
            {
                textRadarDataPath.Text = Application.StartupPath + "/MultiViewerResources/"; ;
            }
            if (textSePath.Text == "")
            {
                textSePath.Text = Application.StartupPath + "/MultiViewerResources/se/";
            }
            RadarViewOrder.SePathName = textSePath.Text;
            if (System.IO.File.Exists(settingsFile))
            {
                FileStream fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlTextReader xReader = new XmlTextReader(fs);

                try
                {
                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Element)
                        {
                            if (xReader.LocalName == "SettingsSerializer")
                            {
                                BasePlugin.xmlSettings.ImportFromXml(xReader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lbStatus.Text = "Error loading settings: " + ex.Message;
                }
                xReader.Close();
            }

        }
        partial void SaveSettings()
        {
            SaveSettings(false);
        }
        void SaveSettings(bool skipWindowPos = false)
        {
            if (skipWindowPos == false)
            {
                if (radarForm != null)
                {
                    textRadarXpos.Text = radarForm.Left.ToString();
                    textRadarYpos.Text = radarForm.Top.ToString();
                }
                if (alertForm != null)
                {
                    textAlertXpos.Text = alertForm.Left.ToString();
                    textAlertYpos.Text = alertForm.Top.ToString();
                }
            }


            FileStream fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter xWriter = new XmlTextWriter(fs, Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.Indentation = 1;
            xWriter.IndentChar = '\t';
            xWriter.WriteStartDocument(true);
            xWriter.WriteStartElement("Config");    // <Config>
            xWriter.WriteStartElement("SettingsSerializer");    // <Config><SettingsSerializer>
            BasePlugin.xmlSettings.ExportToXml(xWriter);   // Fill the SettingsSerializer XML
            xWriter.WriteEndElement();  // </SettingsSerializer>
            xWriter.WriteEndElement();  // </Config>
            xWriter.WriteEndDocument(); // Tie up loose ends (shouldn't be any)
            xWriter.Flush();    // Flush the file buffer to disk
            xWriter.Close();
        }
    }
}
