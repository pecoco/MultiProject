using Memory.DataStructure;
using MemoryUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestMemory.DataStructure;

namespace Memory
{
    public partial class Form1 : Form
    {
        Memory memory;
        public Form1()
        {
            InitializeComponent();

            memory = new Memory();

        }

        private void btStart_Click(object sender, EventArgs e)
        {
            listBoxA.Items.Clear();
            memory.ReadPlayerInfo();
            listBoxA.Items.Add("ACN:" + PlayerInfo.d.ACN);
            listBoxA.Items.Add("ACN_EXP:" + PlayerInfo.d.ACN_CurrentEXP);

            listBoxA.Items.Add("錬金術師 ALC:" + PlayerInfo.d.ALC);
            listBoxA.Items.Add("ALC_EXP:" + PlayerInfo.d.ALC_CurrentEXP);

            listBoxA.Items.Add("ARC:" + PlayerInfo.d.ARC);
            listBoxA.Items.Add("ARC_EXP:" + PlayerInfo.d.ARC_CurrentEXP);

            listBoxA.Items.Add("甲冑師 ARM:" + PlayerInfo.d.ARM);
            listBoxA.Items.Add("ARM_EXP:" + PlayerInfo.d.ARM_CurrentEXP);

            listBoxA.Items.Add("AST:" + PlayerInfo.d.AST);
            listBoxA.Items.Add("AST_EXP:" + PlayerInfo.d.AST_CurrentEXP);

            listBoxA.Items.Add("黒魔導士 BSM:" + PlayerInfo.d.BSM);
            listBoxA.Items.Add("BSM_EXP:" + PlayerInfo.d.BSM_CurrentEXP);

            listBoxA.Items.Add("園芸士 BTN:" + PlayerInfo.d.BTN);
            listBoxA.Items.Add("BTN_EXP:" + PlayerInfo.d.BTN_CurrentEXP);

            listBoxA.Items.Add("幻術士 CNJ:" + PlayerInfo.d.CNJ);
            listBoxA.Items.Add("CNJ_EXP:" + PlayerInfo.d.CNJ_CurrentEXP);

            listBoxA.Items.Add("CPT:" + PlayerInfo.d.CPT);
            listBoxA.Items.Add("CPT_EXP:" + PlayerInfo.d.CPT_CurrentEXP);

            listBoxA.Items.Add("調理師 CUL:" + PlayerInfo.d.CUL);
            listBoxA.Items.Add("CUL_EXP:" + PlayerInfo.d.CUL_CurrentEXP);

            listBoxA.Items.Add("暗黒騎士 DRK:" + PlayerInfo.d.DRK);
            listBoxA.Items.Add("DRK_EXP:" + PlayerInfo.d.DRK_CurrentEXP);

            listBoxA.Items.Add("漁師 FSH:" + PlayerInfo.d.FSH);
            listBoxA.Items.Add("FSH_EXP:" + PlayerInfo.d.FSH_CurrentEXP);

            listBoxA.Items.Add("GLD:" + PlayerInfo.d.GLD);
            listBoxA.Items.Add("GLD_EXP:" + PlayerInfo.d.GLD_CurrentEXP);

            listBoxA.Items.Add("彫金師 GSM:" + PlayerInfo.d.GSM);
            listBoxA.Items.Add("GSM_EXP:" + PlayerInfo.d.GSM_CurrentEXP);

            listBoxA.Items.Add("槍術士 LNC:" + PlayerInfo.d.LNC);
            listBoxA.Items.Add("LNC_EXP:" + PlayerInfo.d.LNC_CurrentEXP);

            listBoxA.Items.Add("革細工師 LTW:" + PlayerInfo.d.LTW);
            listBoxA.Items.Add("LTW_EXP:" + PlayerInfo.d.LTW_CurrentEXP);

            listBoxA.Items.Add("MCH:" + PlayerInfo.d.MCH);
            listBoxA.Items.Add("MCH_EXP:" + PlayerInfo.d.MCH_CurrentEXP);

            listBoxA.Items.Add("採掘師 MIN:" + PlayerInfo.d.MIN);
            listBoxA.Items.Add("MIN_EXP:" + PlayerInfo.d.MIN_CurrentEXP);

            listBoxA.Items.Add("斧術士 MRD:" + PlayerInfo.d.MRD);
            listBoxA.Items.Add("MRD_EXP:" + PlayerInfo.d.MRD_CurrentEXP);

            listBoxA.Items.Add("格闘士 PGL:" + PlayerInfo.d.PGL);
            listBoxA.Items.Add("PGL_EXP:" + PlayerInfo.d.PGL_CurrentEXP);

            listBoxA.Items.Add("ROG:" + PlayerInfo.d.ROG);
            listBoxA.Items.Add("ROG_EXP:" + PlayerInfo.d.ROG_CurrentEXP);

            listBoxA.Items.Add("呪術士 THM:" + PlayerInfo.d.THM);
            listBoxA.Items.Add("THM_EXP:" + PlayerInfo.d.THM_CurrentEXP);

            listBoxA.Items.Add("白魔道士 WVR:" + PlayerInfo.d.WVR);
            listBoxA.Items.Add("WVR_EXP:" + PlayerInfo.d.WVR_CurrentEXP);


            lbName.Text = PlayerInfo.d.Name;

            memory.ReadPartyCount();
            lbPartyCount.Text = PartyInfo.PartyCount.ToString();

            //entry = memory.ReadPlayerInfo();

        }
    }
}
