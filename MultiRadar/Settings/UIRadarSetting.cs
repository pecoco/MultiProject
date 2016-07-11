using ACT.Radardata;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace MultiRadar
{
   
    partial class RadarSettingControl
    {
        AddRadarMobForm addRadarDataForm;
        private void btOpenRadar_Click(object sender, EventArgs e)
        {
            if (ofdRederPath.ShowDialog() == DialogResult.OK)
            {
                textRadarDataPath.Text = ofdRederPath.SelectedPath;
            }
        }

        //地名追加ボタン
        private void btAddAction_Click(object sender, EventArgs e)
        {

            int keepIndex = ComboRadarZone.SelectedIndex;
            addRadarDataForm = new AddRadarMobForm();
            Point btnClientLocation = btAddAction.ClientRectangle.Location;
            Point btnScreenLocation = btAddAction.PointToScreen(btnClientLocation);
            addRadarDataForm.Location = btnScreenLocation;
            addRadarDataForm.ZoneName = ComboRadarZone.Text;
            addRadarDataForm.ZoneNameJp = textAreaJp.Text;

            if (addRadarDataForm.ShowDialog() == DialogResult.OK)
            {
                /*
                radarForm.AddMob(addRadarDataForm.ZoneName, addRadarDataForm.SelectMobtype, addRadarDataForm.MobName, addRadarDataForm.ZoneNameJp);
                radarForm.SaveRadarData();
                */
                ReSetComboRadarZoneItem();
                ComboRadarZone.SelectedIndex = keepIndex;
            }
        }

        //プラグイン　コンボボックス地名リスト追加
        private void ReSetComboRadarZoneItem(bool saveAction = true)
        {
            ComboRadarZone.Items.Clear();
            foreach (string zone in RadardataInstance.ZoneList)
            {
                ComboRadarZone.Items.Add(zone);
            }
            if (saveAction)
            {
                //radarForm.SaveRadarData();
            }
            //表示処理
            setSettingFormRederData();
        }

        private void setSettingFormRederData()
        {
            if (ComboRadarZone.Text != "")
            {
                /*
                ZoneMobData zone = radarForm.getMobList(ComboRadarZone.Text);

                textAreaJp.Text = zone.nameJp;

                listMobSS.Items.Clear();
                listMobA.Items.Clear();
                listMobB.Items.Clear();
                listMobETC.Items.Clear();
                for (int i = 0; i < zone.s.Count; i++)
                {
                    listMobSS.Items.Add(zone.s[i]);
                }
                for (int i = 0; i < zone.a.Count; i++)
                {
                    listMobA.Items.Add(zone.a[i]);
                }
                for (int i = 0; i < zone.b.Count; i++)
                {
                    listMobB.Items.Add(zone.b[i]);
                }
                for (int i = 0; i < zone.etc.Count; i++)
                {
                    listMobETC.Items.Add(zone.etc[i]);
                }
                */
            }
        }

        private void ComboRederZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //表示処理
            setSettingFormRederData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            switch (tabControlMob.SelectedIndex)
            {
                
                case 0:
                    radarForm.RemoveMob(ComboRadarZone.Text, MobType.S, listMobSS.SelectedItem.ToString());
                    listMobSS.Items.Clear();
                    break;
                case 1:
                    radarForm.RemoveMob(ComboRadarZone.Text, MobType.A, listMobA.SelectedItem.ToString());
                    listMobA.Items.Clear();
                    break;
                case 2:
                    radarForm.RemoveMob(ComboRadarZone.Text, MobType.B, listMobB.SelectedItem.ToString());
                    listMobB.Items.Clear();
                    break;
                case 3:
                    radarForm.RemoveMob(ComboRadarZone.Text, MobType.ETC, listMobETC.SelectedItem.ToString());
                    listMobETC.Items.Clear();
                    break;
            }*/
            ReSetComboRadarZoneItem();
        }
    }
}
