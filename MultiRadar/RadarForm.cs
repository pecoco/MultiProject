using System.Windows.Forms;
using ACT.Radardata;
using System;
using System.Drawing;

namespace ACT.RadarForm
{
    using ACT.RadarViewOrder;
    public partial class RadarForm : Form
    {
        public RadarForm()
        {
            InitializeComponent();
        }
        ~RadarForm()
        {
            solidBrush.Dispose();
            backBrush.Dispose();
            font.Dispose();
            font2.Dispose();
            font7.Dispose();
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

        private bool saveCall = false;

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
    }
}
