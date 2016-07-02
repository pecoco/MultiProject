
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.RadarViewOrder
{
    using MultiProject;
    public static class RadarViewOrder
    {
        static bool playerView = false;

        public static bool PlayerView
        {
            get { return playerView; }
            set { playerView = value; }
        }
        //static int bW = this.Width;//radarBaseWidth;
        //static int bH = this.Height;//radarBaseHeight;
        public static int bX = 0;
        public static int bY = 0;
        public static int bW = 0;
        public static int bH = 0;
        public static int infoX = 0;
        public static int infoY = 0;
        static float scaleX = 0;
        static float scaleY = 0;


 


        public static PointF GetBasePosition(int addX, int addY)
        {
            return new PointF(bX + addX, bY + addY);
        }

        public static double myRadian;
        public static Rectangle oldPlayerRect;
        public static Rectangle PlayerRect()
        {

            oldPlayerRect = new Rectangle((int)(scaleX * bW / bW) - 2, (int)(scaleY * bH / bH) - 2, 8, 8);
            return oldPlayerRect;
        }

        private static double getRadian(double x, double y, double x2, double y2)
        {
            double radian = Math.Atan2(y2 - y, x2 - x);
            return radian;
        }

        public static void SetBasePosition(int posX, int posY, int width, int height)
        {
            bX = posX;
            bY = posY;
            bH = height;
            bW = width;
            infoX = 0;// bX;
            infoY = 54;
            scaleX = bW / 2;
            scaleY = bH / 2;

        }

        public static Rectangle MobRect(float myX, float myY, float mobX, float mobY)
        {
            float x = (mobX) - myX + (64 * radarZoom);//0-2000
            float y = (mobY) - myY + (64 * radarZoom);

            x = (x * bW) / (64 * radarZoom * 2);// (scale * 2);//  400;
            y = (y * bH) / (64 * radarZoom * 2);// (scale * 2);//400;

            return new Rectangle((int)x, (int)y, 5, 5);
        }

        public static int radarZoom = 3;

        public static Combatant oldMyData;
        private static Combatant newMyData;
        public static Combatant myData
        {
            get { return newMyData; }
            set
            {
                newMyData = newMyData ?? value;
                if (newMyData.PosX != value.PosX || newMyData.PosY != value.PosY)
                {
                    oldMyData = newMyData ?? value;
                    newMyData = value;
                    myRadian = getRadian(oldMyData.PosX, oldMyData.PosY, newMyData.PosX, newMyData.PosY);
                }
                else
                {
                    return;
                }


            }
        }
        private static System.Media.SoundPlayer SeA = null;
        private static System.Media.SoundPlayer SeB = null;
        private static System.Media.SoundPlayer SeS = null;

        public static string SePathName;

        public static void PlaySeS()
        {
            SeS = SeS ?? new System.Media.SoundPlayer(SePathName + "/s.wav");
            //SeS.PlaySync();
            SeS.Play();
        }
        public static void PlaySeA()
        {
            SeA = SeA ?? new System.Media.SoundPlayer(SePathName + "/a.wav");
            SeA.Play();
        }
        public static void PlaySeB()
        {
            SeB = SeB ?? new System.Media.SoundPlayer(SePathName + "/b.wav");
            SeB.Play();
        }
        public static void PlaySeE()
        {
            SeB = SeB ?? new System.Media.SoundPlayer(SePathName + "/b.wav");
            SeB.Play();
        }

        private static bool allRadarMode;
        public static bool AllRadarMode
        {
            set { allRadarMode = value; }
            get { return allRadarMode; }
        }
        public static string ChengeRadarMode(bool notDataChange = false)
        {
            if (notDataChange == false)
            {
                AllRadarMode = !allRadarMode;
            }
            return allRadarMode ? "A" : "O";
        }
        private static Point keepPoint;

        public static void keepWindowSize(int width, int height)
        {
            keepPoint = new Point(width, height);
        }
        public static int getKeepWindowHeightSize()
        {
            return keepPoint.Y;
        }
        public static bool windowsStatus;

        public static List<HitMobdata> hitMobdatas;
        public static void AddHitMob(HitMobdata mobdata)
        {
            hitMobdatas = hitMobdatas ?? new List<HitMobdata>();
            hitMobdatas.Add(mobdata);
        }

        public class HitMobdata
        {
            public string mobName;
            public string rank;
            float posY;
            bool alert;
            public HitMobdata(string _mobName, string _rank)
            {
                mobName = _mobName;
                rank = _rank;
                alert = false;
            }
            public void RemoveAt(int Index)
            {
                if (hitMobdatas.Count > Index)
                {
                    hitMobdatas.RemoveAt(Index);
                }
            }
        }

    }
}
