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
            viewCount = 9;
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
                    case 9: lbMessage.Text = "M-u-l-t-i Radar Act Plug-in for Final Fantasy XIV"; break;
                    case 8: lbMessage.Text = "Alert Window And Sound. Catch to log"; break;
                    case 7: lbMessage.Text = "---Panel---"; break;
                    case 6: lbMessage.Text = "■ Green Button Player or Mob."; break;
                    case 5: lbMessage.Text = "▶ Marking (Front Line Used.) +/- Zoom."; break;
                    case 4: lbMessage.Text = "■ Blue Button Select or All."; break;
                    case 3: lbMessage.Text = "■ Yellow Button ID mode."; break;
                    case 2: lbMessage.Text = "Good Luck!"; break;
                }
            }
            switch (viewCount)
            {
                case 6: lbMessage.ForeColor = Color.Lime; break;
                case 5: lbMessage.ForeColor = Color.LightCyan; break;
                case 4: lbMessage.ForeColor = Color.LightSkyBlue; break;
                case 3: lbMessage.ForeColor = Color.Yellow; break;
                case 2: lbMessage.ForeColor = Color.White; break;
            }
            viewCount -= 1;
            if (viewCount > 0)
            {
                if (this != null) { this.Visible = true; }                
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
