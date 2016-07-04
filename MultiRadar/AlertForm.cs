using ACT.RadarViewOrder;
using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace MultiRadar
{
    public partial class AlertForm : Form
    {
        Action callbackSaveSetting = null;
        public Action CallbackSaveSetting
        {
            set { callbackSaveSetting = value; }

        }

        private static System.Timers.Timer t;
        public AlertForm()
        {
            InitializeComponent();
            this.TransparencyKey = this.BackColor;
            this.Opacity = transparent;

            t = new System.Timers.Timer(3000);

            t.Elapsed += OnTimedEvent;
            viewCount = 8;
            t.Enabled = true;

        }
        private int viewCount = 0;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (RadarViewOrder.hitMobdatasFromLog != null)
            {
                if (RadarViewOrder.hitMobdatasFromLog.Count > 0)
                {
                    RadarViewOrder.HitMobdata mob = RadarViewOrder.hitMobdatasFromLog[RadarViewOrder.hitMobdatasFromLog.Count - 1];
                    mob.RemoveAt(RadarViewOrder.hitMobdatasFromLog.Count - 1);
                    lbMessage.Text = mob.rank.ToUpper() + ":" + mob.mobName;
                    viewCount = 3;
                    if (RadarViewOrder.SoundEnable)
                    {
                        if (mob.rank == "s" && viewCount == 3) { RadarViewOrder.PlaySeS(); }
                        if (mob.rank == "a" && viewCount == 3) { RadarViewOrder.PlaySeA(); }
                        if (mob.rank == "b" && viewCount == 3) { RadarViewOrder.PlaySeB(); }
                        if (mob.rank == "e" && viewCount == 3) { RadarViewOrder.PlaySeB(); }
                    }
                }
            }else
            {
                switch (viewCount)
                {
                    case 8: lbMessage.Text= "it isn't hard to find."; break;
                    case 7: lbMessage.Text = "You can have the love you need to live."; break;
                    case 6: lbMessage.Text = "But if you look for truthfulness"; break;
                    case 5: lbMessage.Text = "You might just as well be blind."; break;
                    case 4: lbMessage.Text = "It always seems to be so hard to give."; break;
                    case 3: lbMessage.Text = "Honesty is such a lonely word."; break;
                    case 2: lbMessage.Text = "Good Luck!"; break;
                }
            }
            switch (viewCount)
            {
                case 5: lbMessage.ForeColor = Color.AliceBlue; break;
                case 4: lbMessage.ForeColor = Color.Yellow; break;
                case 3: lbMessage.ForeColor = Color.Lime; break;
                case 2: lbMessage.ForeColor = Color.White; break;
                case 1: lbMessage.ForeColor = Color.White; break;
            }
            viewCount -= 1;
            if (viewCount > 0)
            {
                this.Visible = true;
            }
            else
            {
                viewCount = 0;
                this.Visible = false;
            }
        }

        float transparent = 0.7f;

        private Point mousePoint;
        private bool saveCall = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
                saveCall = true;
            }

        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            if (saveCall)
            {
                saveCall = false;
                if (callbackSaveSetting != null)
                {
                    callbackSaveSetting();
                }
            }
        }
    }
}
