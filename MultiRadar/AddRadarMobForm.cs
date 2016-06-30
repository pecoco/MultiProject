using ACT.Radardata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MultiRadar
{
    public partial class AddRadarMobForm : Form
    {
        public AddRadarMobForm()
        {
            InitializeComponent();
            zoneList = RadardataInstance.ZoneList;
        }

        public List<string> zoneList;


        private void BtCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void rbSelectArea_CheckedChanged(object sender, EventArgs e)
        {
            cbZone.Show();
            textZone.Hide();
        }

        private void rbNewArea_CheckedChanged(object sender, EventArgs e)
        {
            cbZone.Hide();
            textZone.Show();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (rbNewArea.Checked)
            {
                textZone.Text = textZone.Text.Trim();
                if (textZone.Text.Length == 0)
                {
                    btOk.DialogResult = DialogResult.None;
                    return;
                }
            }
            btOk.DialogResult = DialogResult.OK;
        }

        public string ZoneName
        {
            get
            {
                if (rbNewArea.Checked)
                {
                    return textZone.Text;
                }
                else
                {
                    return cbZone.Text;
                }
            }
            set
            {
                if (rbNewArea.Checked)
                {
                }
                else
                {
                    cbZone.Text = value;
                }
            }
        }

        public string ZoneNameJp
        {
            get { return textZoneJp.Text; }
            set { textZoneJp.Text = value; }
        }

        public MobType SelectMobtype
        {
            get
            {
                switch (cbMobType.Text)
                {
                    case "S": return MobType.S;
                    case "A": return MobType.A;
                    case "B": return MobType.B;
                    case "ETC": return MobType.ETC;
                    default: return MobType.ETC;
                }
            }
        }
        public string MobName
        {
            get
            {
                return textMob.Text;
            }
        }



        private void AddRadarMobForm_VisibleChanged(object sender, EventArgs e)
        {
            if (zoneList == null)
            {
                rbNewArea.Checked = true;
            }
            else
            {
                foreach (string zone in zoneList)
                {
                    cbZone.Items.Add(zone);
                }
            }
        }

        private void cbZone_ValueMemberChanged(object sender, EventArgs e)
        {
            if (cbZone.Text.Length>0)
            {
                ZoneMobData radarData = RadardataInstance.radarData.getMobList(cbZone.Text);
                if (radarData != null)
                {
                    textZoneJp.Text = radarData.nameJp;
                }
            }
        }

        private void cbZone_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbZone.Text.Length > 0)
            {
                ZoneMobData radarData = RadardataInstance.radarData.getMobList(cbZone.Text);
                if (radarData != null)
                {
                    textZoneJp.Text = radarData.nameJp;
                }
            }
        }
    }
}
