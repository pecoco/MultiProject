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

        partial void LoadSettings()
        {
            //Radar(Map)(Contrall Window)
            BasePlugin.xmlSettings.AddControlSetting(textRadarXpos.Name, textRadarXpos);
            BasePlugin.xmlSettings.AddControlSetting(textRadarYpos.Name, textRadarYpos);
            BasePlugin.xmlSettings.AddControlSetting(ckRadarVisible.Name, ckRadarVisible);
            BasePlugin.xmlSettings.AddControlSetting(rbRederModeSelect.Name, rbRederModeSelect);
            BasePlugin.xmlSettings.AddControlSetting(rbRadarTaegetPlayer.Name, rbRadarTaegetPlayer);

            BasePlugin.xmlSettings.AddControlSetting(textAlertXpos.Name, textAlertXpos);
            BasePlugin.xmlSettings.AddControlSetting(textAlertYpos.Name, textAlertYpos);

            BasePlugin.xmlSettings.AddControlSetting(textRadarWidth.Name, textRadarWidth);
            BasePlugin.xmlSettings.AddControlSetting(textRadarHeight.Name, textRadarHeight);

            BasePlugin.xmlSettings.AddControlSetting(numZoom.Name, numZoom);
            //SE
            BasePlugin.xmlSettings.AddControlSetting(ckRadarSE.Name, ckRadarSE);
            //Path
            BasePlugin.xmlSettings.AddControlSetting(textRadarDataPath.Name, textRadarDataPath);
            //SePath
            BasePlugin.xmlSettings.AddControlSetting(textSePath.Name, textSePath);
            //Fontsize
            BasePlugin.xmlSettings.AddControlSetting(numFontSize.Name, numFontSize);
            //Opacity
            BasePlugin.xmlSettings.AddControlSetting(numOpacity.Name, numOpacity);

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
            if (textRadarDataPath.Text == "")
            {
                textRadarDataPath.Text = Application.StartupPath + "\\MultiProjectResources\\"; ;
            }
            if (textSePath.Text == "")
            {
                textSePath.Text = Application.StartupPath + "\\MultiProjectResources\\se\\";
            }
            if (Directory.Exists(textSePath.Text))
            {
                RadarViewOrder.SePathName = textSePath.Text;
            }else
            {
                MessageBox.Show("SE Pathが無効です。("+ textSePath.Text + ") 設定パネルでSEのパスを確認、再設定が必要です。");
            }
        }
        partial void SaveSettings()
        {
            SaveSettings(false);
        }
        void SaveSettings(bool skipSetWindowPos = false)
        {
            if (skipSetWindowPos == false)
            {
                if (radarForm != null)
                {
                    textRadarXpos.Text = ((int)radarForm.Left).ToString();
                    textRadarYpos.Text = ((int)radarForm.Top).ToString();
                    textRadarWidth.Text = ((int)radarForm.Width).ToString();
                    if (radarForm.Height > 300)
                    {
                        textRadarHeight.Text = ((int)radarForm.Height).ToString();
                    }
                    numZoom.Value = 21 - RadarViewOrder.radarZoom;

                    rbRederModeSelect.Checked = radarForm.isRadarSelect;
                    rbRadarTaegetPlayer.Checked = radarForm.isRadarAntiParsonal;
                }
                if (alertForm != null)
                {
                    textAlertXpos.Text = ((int)alertForm.Left).ToString();
                    textAlertYpos.Text = ((int)alertForm.Top).ToString();
                }
            }

            if (textSePath.Text.Length > 0) { 
                if (textSePath.Text[textSePath.Text.Length - 1] != '\\')
                {
                    textSePath.Text = textSePath.Text + "\\";
                }
            }
            if (textRadarDataPath.Text.Length > 0)
            {
                if (textRadarDataPath.Text[textRadarDataPath.Text.Length - 1] != '\\')
                {
                    textRadarDataPath.Text = textRadarDataPath.Text + "\\";
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
