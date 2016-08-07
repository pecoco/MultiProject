using System;
using System.Collections.Generic;
using System.Drawing;

namespace ACT.RadarViewOrder
{
    using MultiProject;
    using System.IO;
    using System.Windows;
    public static class RadarViewOrder
    {
        static bool playerView = false;
        private static int fontSize;
        public static int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        public static int opacity;
        public static int Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }


        public static bool PlayerView
        {
            get { return playerView; }
            set { playerView = value; }
        }

        public static int bX = 0;
        public static int bY = 0;
        public static int bW = 0;
        public static int bH = 0;
        public static int infoX = 0;
        public static int infoY = 0;
        public static int scale = 16;
        static float scaleX = 0;
        static float scaleY = 0;

        private static bool soundEnable = true;
        public static bool SoundEnable
        {
            get {return soundEnable; }
            set { soundEnable = value; }
        }

        public static PointF GetBasePosition(int addX, int addY)
        {
            return new PointF(bX + addX, bY + addY);
        }

        public static double myRadian;
        public static Rect oldPlayerRect;
        public static Rect PlayerRect()
        {
            oldPlayerRect = new Rect((int)(scaleX * bW / bW) - 2, (int)(scaleY * bH / bH) - 2, 4, 4);
            return oldPlayerRect;
        }
        public static int keepWindowHeight;



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
            scaleX = bW / 2f;
            scaleY = bH / 2f;

        }

        public static Rect MobRect(float myX, float myY, float mobX, float mobY)
        {
            float x = (mobX) - myX + (scale * radarZoom);//0-2000
            float y = (mobY) - myY + (scale * radarZoom);

            x = (x * bW) / (scale * radarZoom * 2);// (scale * 2);//  400;
            y = (y * bH) / (scale * radarZoom * 2);// (scale * 2);//400;

            return new Rect((int)x-5, (int)y, 3, 3);
        }

        public static Point AreaPos()
        {
            float x = (145);//0-2000
            float y = (145);

            x = (x * bW) / (scale * radarZoom);// (scale * 2);//  400;
            y = (y * bH) / (scale * radarZoom);// (scale * 2);//400;

            return new Point((int)x , (int)y);
        }

        public static Rect AreaRect(int value)
        {
            float x = (value);//0-2000
            float y = (value);

            x = (x * bW) / (scale * radarZoom);// (scale * 2);//  400;
            y = (y * bH) / (scale * radarZoom);// (scale * 2);//400;

            return new Rect((int)x, (int)y, 0, 0);
        }





        public static int radarZoom = 10;

        public static void ZoomIn()
        {
            radarZoom = radarZoom > 10 ? radarZoom - 1 : 10;
        }
        public static void ZoomOut()
        {
            radarZoom = radarZoom < 20 ? radarZoom + 1 : 20;
        }

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
        private static System.Media.SoundPlayer SeE = null;
        private static System.Media.SoundPlayer SeS = null;

        private static bool SeEnable = true;


        private static string sePathName;
        public static string SePathName
        {
            set {
                if (Directory.Exists(value))
                {
                    sePathName = value;
                    SeEnable = true; ;
                }
                else
                {
                    SeEnable = false;
                }
            }
        }


        public static void PlaySeS()
        {
            if (SeEnable != true) { return; }
            SeS = SeS ?? new System.Media.SoundPlayer(sePathName + "/s.wav");
            //SeS.PlaySync();
            SeS.Play();
        }
        public static void PlaySeA()
        {
            if (SeEnable != true) { return; }
            SeA = SeA ?? new System.Media.SoundPlayer(sePathName + "/a.wav");
            SeA.Play();
        }
        public static void PlaySeB()
        {
            if (SeEnable != true) { return; }
            SeB = SeB ?? new System.Media.SoundPlayer(sePathName + "/b.wav");
            SeB.Play();
        }
        public static void PlaySeE()
        {
            if (SeEnable != true) { return; }
            SeE = SeE ?? new System.Media.SoundPlayer(sePathName + "/e.wav");
            SeE.Play();
        }

        public static bool windowsStatus;
        public static List<HitMobdata> hitMobdatasFromLog;
        public static void AddHitMobfromLog(HitMobdata mobdata)
        {
            hitMobdatasFromLog = hitMobdatasFromLog ?? new List<HitMobdata>();
            hitMobdatasFromLog.Add(mobdata);
        }

        public class HitMobdata
        {
            public string mobName;
            public string rank;
            public HitMobdata(string _mobName, string _rank)
            {
                mobName = _mobName;
                rank = _rank;              
            }
            public void RemoveAt(int Index)
            {
                if (hitMobdatasFromLog.Count > Index && Index >-1)
                {
                    hitMobdatasFromLog.RemoveAt(Index);
                }
            }
        }
    }
}
