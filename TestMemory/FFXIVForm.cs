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
            listBoxA.Items.Add("ACN:"+PlayerInfo.d.ACN);
            listBoxA.Items.Add("ACN_EXP:" + PlayerInfo.d.ACN_CurrentEXP);

            //entry = memory.ReadPlayerInfo();

        }
    }
}
