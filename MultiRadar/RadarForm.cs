using System.Windows.Forms;
using ACT.Radardata;
using System;
using System.Drawing;

namespace ACT.RadarForm
{
    using ACT.RadarViewOrder;
    using MultiProject;
    using MultiProject.Common;
    using System.Collections.Generic;
    using System.Text;
    public partial class RadarForm : Form
    {
        public RadarForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Selectable, false);
            this.TransparencyKey = this.BackColor;
            RadarViewOrder.keepWindowSize(this.Width, this.Height);
            RadarViewOrder.windowsStatus = true;
            btAllModeSwitch.Text = RadarViewOrder.ChengeRadarMode(true);

        }
        ~RadarForm()
        {
            solidBrush.Dispose();
            backBrush.Dispose();
            font.Dispose();
            font2.Dispose();
            font7.Dispose();
        }
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        public void AddMob(string zoneName, MobType mobtype, string mobName, string viewName)
        {
            if (mobName.Length > 0)
            {
                RadardataInstance.radarData.AddMob(zoneName, mobtype, mobName, viewName);
            }
            else
            {
                RadardataInstance.radarData.AddZone(zoneName, viewName);
            }
        }
        public void RemoveMob(string zoneName, MobType mobtype, string mobName)
        {
            if (mobName.Length > 0)
            {
                RadardataInstance.radarData.RemoveMob(zoneName, mobtype, mobName);

            }
        }
        public ZoneMobData getMobList(string zomeName)
        {
            return RadardataInstance.radarData.getMobList(zomeName);
        }

        public void SaveRadarData()
        {
            RadardataInstance.radarData.SaveAreaData();
        }
        SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 80, 210, 255));
        SolidBrush backBrush = new SolidBrush(Color.FromArgb(255, 30, 30, 40));
        Font font = new Font(FontFamily.GenericSerif, 8, FontStyle.Bold, GraphicsUnit.Point);
        Font font2 = new Font("Georgia", 18, FontStyle.Regular, GraphicsUnit.Point);
        Font font7 = new Font(FontFamily.GenericMonospace, 7, FontStyle.Regular, GraphicsUnit.Point);
        const int CLOSE_WINDOW_SET_HEIGHT = 70;
        const float VIEW_OPACITY = 0.7f;
        const float HIDE_OPACITY = 0.3f;

        private bool saveCall = false;
        bool view = true;
        int keepWidth = 0;
        int keepheight = 0;

        Action callbackSaveSetting = null;
        public Action CallbackSaveSetting
        {
            set { callbackSaveSetting = value; }

        }

        private Point mousePoint;
        private void RadarForm_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void RadarForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
                RadarViewOrder.SetBasePosition(this.Left, this.Top, this.Width, this.Height);
                saveCall = true;
            }
        }
        bool swResize = false;
        private void btResize_Click(object sender, EventArgs e)
        {

            if (swResize==false)
            {
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                RadarViewOrder.SetBasePosition(this.Left, this.Top, this.Width, this.Height);
                swResize = true;
            }else
            {
                saveResize();
                swResize = false;
            }
        }

        private void btInterpersonal_Click(object sender, EventArgs e)
        {
            RadarViewOrder.PlayerView = !RadarViewOrder.PlayerView;
            if (RadarViewOrder.PlayerView)
            {
                btInterpersonal.Text = "P";
            }
            else
            {
                btInterpersonal.Text = "M";
            }
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            saveResize();
        }
        private void saveResize()
        {
            if (saveCall)
            {
                saveCall = false;
                if (callbackSaveSetting != null)
                {
                    RadarViewOrder.SetBasePosition(this.Left, this.Top, this.Width, this.Height);
                    callbackSaveSetting();
                    this.FormBorderStyle = FormBorderStyle.None;
                }
                saveCall = true;
            }
        }



        private void btAllModeSwitch_Click(object sender, EventArgs e)
        {
            btAllModeSwitch.Text = RadarViewOrder.ChengeRadarMode();
        }

        private void btZoomPlus_Click(object sender, EventArgs e)
        {
            if (RadarViewOrder.radarZoom > 1)
            {
                RadarViewOrder.radarZoom--;
            }
        }

        private void btZoomMainus_Click(object sender, EventArgs e)
        {
            RadarViewOrder.radarZoom++;
        }

        private void lbSwitch_Click(object sender, EventArgs e)
        {
            if (view)
            {
                view = false;
                keepWidth = this.Width;
                keepheight = this.Height;
                this.Width = 24;
                lbSwitch.ForeColor = Color.Gray;
            }
            else
            {
                this.Width = keepWidth;
                this.Height = keepheight;
                view = true;
                lbSwitch.ForeColor = Color.White;

            }
        }

        private void DrowMyCharacter(Graphics g)
        {
            Rectangle rect = RadarViewOrder.PlayerRect();//

            g.FillEllipse(Brushes.Black, rect);

            float sf = (180f * (float)RadarViewOrder.myRadian) / (float)3.1415;
            float ef = 18.0f;//sf;

            sf = sf - 9 < -180 ? sf - 9 + 180 : sf - 9;
            g.DrawPie(Pens.Aqua, rect, sf, ef); ;
        }

        private delegate void safeCall(object sender, PaintEventArgs e);
        private void RadarForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.InvokeRequired)
            {
                //デリゲートからInvoke呼び出し
                safeCall d = new safeCall(radar_Paint);
                try
                {
                    this.Invoke(d, new object[] { sender, e });
                }
                catch (Exception error)
                {
                }
            }
            else
            {
                radar_Paint(sender, e);
            }
        }

        private void radar_Paint(object sender, PaintEventArgs e)
        {
            if (ActData.AllCharactor == null) { return; }

            if (ActData.AllCharactor.Count == 0) { return; }
            if (!view) { return; }

            RadarViewOrder.SetBasePosition(this.Left, this.Top, this.Width, this.Height);
            RadarViewOrder.myData = ActData.AllCharactor[0];

            //場所
            if (RadardataInstance.Zone != "")
            {
                e.Graphics.DrawString(RadardataInstance.Zone, font2, Brushes.WhiteSmoke, 0, 20);
            }

            lock (ActData.AllCharactor)
            {
                List<Combatant> searchObjects = new List<Combatant>();
                if (!RadarViewOrder.AllRadarMode)
                {
                    foreach (Combatant mob in ActData.AllCharactor)
                    {
                        if (RadardataInstance.radarData.Search(RadardataInstance.Zone, mob.Name) > 0)
                        {
                            searchObjects.Add(mob);
                        }
                    }
                }
                else
                {
                    selectAll(ref searchObjects);
                }

                if (searchObjects.Count > 0)
                {
                    if (this.Height == CLOSE_WINDOW_SET_HEIGHT)
                    {
                        this.Height = RadarViewOrder.getKeepWindowHeightSize();
                        this.Opacity = VIEW_OPACITY;
                    }
                    if (RadarViewOrder.windowsStatus == false)
                    {
                        RadarViewOrder.windowsStatus = true;
                    }
                    namePlate(searchObjects, e);
                }else
                {
                    this.Height = CLOSE_WINDOW_SET_HEIGHT;
                    this.Opacity = HIDE_OPACITY;
                    RadarViewOrder.windowsStatus = false;
                }

                if (RadarViewOrder.windowsStatus)
                {
                    DrowMyCharacter(e.Graphics);
                }

            }


        }
        private List<uint> FlagIDs = new List<uint>();
        private void selectAll(ref List<Combatant> searchObjects)
        {
            if (FlagKeepOn)
            {
                FlagIDs.Clear();

                foreach (Combatant charcter in ActData.AllCharactor)
                {
                    FlagIDs.Add(charcter.ID);
                }

                FlagKeepOn = false;
            }
            foreach (Combatant charcter in ActData.AllCharactor)
            {

                if (charcter.Name == "ガルーダ・エギ")
                {
                    continue;
                }
                if (charcter.Name == "イフリート・エギ")
                {
                    continue;
                }
                if (charcter.Name == "フェアリー・エオス")
                {
                    continue;
                }
                if (charcter.Name == "フェアリー・セレネ")
                {
                    continue;
                }
                if (charcter.Name == "オートタレット・ルーク")
                {
                    continue;
                }
                if (charcter.Name == "オートタレット・ビショップ")
                {
                    continue;
                }
                if (charcter.Name == "木人")
                {
                    continue;
                }

                if (!RadarViewOrder.PlayerView)
                {
                    if (charcter.Name.IndexOf("宝箱") > -0)
                    {
                        searchObjects.Add(charcter);
                    }
                    if (charcter.type == 2)
                    {
                        searchObjects.Add(charcter);
                    }
                }else
                {
                    if (charcter.type != 2)
                    {
                        searchObjects.Add(charcter);
                    }
                }
            }
        }


        public void upDate()
        {
            this.Invalidate();
        }

        private void namePlate(List<Combatant> searchObjects, PaintEventArgs e)
        {
            foreach (Combatant mob in searchObjects)
            {
                bool flag = isFlag(mob.ID);
                int hpPar = (mob.CurrentHP * 100 / mob.MaxHP);
                bool shortName = false;
                if (mob.type == 1) { shortName = true; }

                Rectangle rect = RadarViewOrder.MobRect(RadarViewOrder.myData.PosX, RadarViewOrder.myData.PosY, mob.PosX, mob.PosY);

                e.Graphics.FillEllipse(getBrush(hpPar, flag), rect);

                this.TextOut(e, mob.Name, Brushes.LightGray, rect.X, rect.Y - 14, flag, shortName);
                if (RadarViewOrder.PlayerView)
                {
                    this.JobTextOut(e, mob.Job, rect, mob.IsCasting);
                    if(mob.CastTargetID == RadarViewOrder.myData.ID)
                    {
                        this.TextOut(e, mob.Name, Brushes.Red, rect.X, rect.Y - 14, flag, shortName);
                    }
                }
            }
        }

        private Brush getBrush(int hpPar, bool flag)
        {
            if (hpPar == 0)
            {
                return Brushes.Gray;
            }
            else if (hpPar < 10)
            {
                return (Brushes.Red);
            }
            else if (hpPar < 40)
            {
                return (Brushes.Yellow);
            }
            else
            {
                if (flag)
                {
                    return (Brushes.Lime);
                }
                else
                {
                    return (Brushes.Violet);
                }
            }
        }
           

        private bool FlagKeepOn = false;
        private void btFlagOn_Click(object sender, EventArgs e)
        {
            FlagKeepOn = true;
        }

        StringBuilder name = new StringBuilder();
        private void TextOut(PaintEventArgs e, string charcterName, Brush color, int left, int top, bool flag, bool ShortName)
        {
            name.Length = 0;
            if (ShortName)
            {
                string[] ss = charcterName.Split(' ');
                name.Append(ss[0].Substring(0, 1));
                if (ss.Length > 1)
                {
                    name.Append('.' + ss[1]);
                }
            }else
            {
                name.Append(charcterName);
            }

            if (flag)
            {
                e.Graphics.DrawString("▶", font, Brushes.Aqua, left, top);
                e.Graphics.DrawString(name.ToString(), font, color, left + 8, top);
                return;
            }
            e.Graphics.DrawString(name.ToString(), font, color, left, top);
        }
        private void JobTextOut(PaintEventArgs e, int JobId, Rectangle rect, bool IsCasting)
        {
            //Jobtype
            switch (JobId)
            {
                case 19:
                case 21:
                case 32:
                    e.Graphics.DrawString("TANK", font7, Brushes.DodgerBlue, rect.Left + 5, rect.Top - 2); break;
                case 20:
                case 22:
                case 30:
                    e.Graphics.DrawString("MELE", font7, Brushes.Red, rect.Left + 5, rect.Top - 2); break;
                case 23:
                case 31:
                    e.Graphics.DrawString("RANG", font7, Brushes.Orange, rect.Left + 5, rect.Top - 2); break;
                case 24:
                case 28:
                case 33:
                    if (IsCasting)
                    {
                        e.Graphics.DrawString("HEAL◎", font7, Brushes.LightGreen, rect.Left + 5, rect.Top - 2); break;
                    }
                    else
                    {
                        e.Graphics.DrawString("HEAL", font7, Brushes.LightGreen, rect.Left + 5, rect.Top - 2); break;
                    }
                case 25:
                case 27:
                    if (IsCasting)
                    {
                        e.Graphics.DrawString("CAS◎", font7, Brushes.Yellow, rect.Left + 5, rect.Top - 2); break;
                    }
                    else
                    {
                        e.Graphics.DrawString("CAS", font7, Brushes.Yellow, rect.Left + 5, rect.Top - 2); break;
                    }
                default:
                    break;
            }
        }

        private bool isFlag(uint id)
        {
            foreach (uint flagId in FlagIDs)
            {
                if (flagId == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
