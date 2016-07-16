using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

using System.Windows.Threading;

using System.Windows.Media.Media3D;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;
using System.Reflection;

using System.Timers;
using System.Text.RegularExpressions;
using ACT.RadarViewOrder;
using MultiProject.Common;
using ACT.Radardata;
using MultiProject;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;

namespace Wpf.RadarWindow
{
    public partial class MainWindow : Window
    {
        const int CLOSE_WINDOW_SET_HEIGHT = 40;
        Action callbackSaveSetting = null;
        public Action CallbackSaveSetting
        {
            set { callbackSaveSetting = value; }
        }
        private readonly DispatcherTimer mTimer = new DispatcherTimer(DispatcherPriority.Render);
        
        private RadarMainWindowViewModel model;

        
        public bool isRadarSelect{
            get{ return model.SelectChecked;}
            set { model.SelectChecked = value; }
        }
        public bool isRadarAntiParsonal
        {
            get { return model.AntiPersonalChecked;}
            set { model.AntiPersonalChecked = value; }
        }

        SolidColorBrush windowRectBrush = new SolidColorBrush();

        MainWindow instance = null;
        public MainWindow()
        {

            instance = this;
            InitializeComponent();
            
            windowRectBrush.Color = Color.FromArgb(1, 80, 80, 80);

            Loaded += (o, e) =>
            {
                var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                source.AddHook(new HwndSourceHook(WndProc));
                mTimer.Interval = TimeSpan.FromSeconds(0.05);//50ミリ秒間隔に設定
                mTimer.Tick += new EventHandler(TickTimer);
                mTimer.Start();
            };

            //-
            model = new RadarMainWindowViewModel();
            String propertyName = "";
            model.PropertyChanged += new PropertyChangedEventHandler((s, e) => { propertyName = e.PropertyName; });
            DataContext = model;
            //-
            rtClipBar.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
        }

        ~MainWindow()
        {
        }
        #region Property
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
        #endregion

        #region
        private void CreateMyIcon()
        {
            //BitmapImage myImage;
            //Rect myImageRect;
            /* 回転がある為今回は使えないが、リソースはこの方法がベスト
            using (MemoryStream memory = new MemoryStream())
            {
                var bm = MultiRadar.Properties.Resources.Arrow;
                bm.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                myImage = new BitmapImage();
                myImage.BeginInit();
                myImage.StreamSource = memory;
                myImage.CacheOption = BitmapCacheOption.OnLoad;
                myImage.EndInit();
                
            }
            */
        }
        #endregion
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        const int WM_MOUSEACTIVATE = 0x21;
        const int MA_NOACTIVATE = 3;
        const int MA_NOACTIVATEANDEAT = 4;
        const int WM_SYSKEYDOWN = 0x0104;
        const int VK_F4 = 0x73;
        const int VM_LBUTTON = 0x0201;
        static Int32 MES;


        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
 
            if ((msg == WM_SYSKEYDOWN) &&
                (wParam.ToInt32() == VK_F4))
            {
                handled = true;
            }
            MES = msg;

            if(MES== WM_MOUSEACTIVATE)
            {
                handled = true;
                return new IntPtr(MA_NOACTIVATE);
            }
            return IntPtr.Zero;
        }

        public bool isView
        {
            get
            {
                if (instance != null) { return true; } else { return false; }
            }

        }

        public void TickTimer(object sender, EventArgs e)
        {
            if (isView)
            {
                Render();
            }
        }

        private void Render()
        {
            using (var dc = dg.Open())
            {
                dc.DrawRectangle(null, new Pen(windowRectBrush, 1), new Rect(0, 0, img.Width - 1, img.Height - 1));

                if (ActData.AllCharactor == null) { return; }
                if (ActData.AllCharactor.Count == 0) { return; }
                if (!isOpen) { return; }

                RadarViewOrder.SetBasePosition((int)this.Left, (int)this.Top, (int)img.Width-1, (int)img.Height-1);
                RadarViewOrder.myData = ActData.AllCharactor[0];

                lock (ActData.AllCharactor)
                {
                    List<Combatant> searchObjects = new List<Combatant>();
                    if (model.SelectChecked)
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
                        SelectAll(ref searchObjects);
                    }

                    if (searchObjects.Count > 0)
                    {
                        if (this.Height == CLOSE_WINDOW_SET_HEIGHT)
                        {
                            
                            FirstOpenWindowAnimetion();
                        }

                        if (OpenWindowAnimetion(dc,300))
                        {
                            //場所
                            if (RadardataInstance.Zone != "")
                            {
                                dc.DrawText(new FormattedText(RadardataInstance.Zone,
                                System.Globalization.CultureInfo.CurrentUICulture,
                                FlowDirection.LeftToRight, new Typeface("Times New Roman"),
                                14, Brushes.White), new Point(2, 22));

                            }
                            if (RadarViewOrder.windowsStatus == false)
                            {
                                RadarViewOrder.windowsStatus = true;
                            }
                            namePlate(searchObjects, dc);
                        }
                    }
                    else
                    {
                        this.Height = CLOSE_WINDOW_SET_HEIGHT;
                        //this.Opacity = HIDE_OPACITY;
                        RadarViewOrder.windowsStatus = false;
                        keepWidth = model.WindowWidth;
                        keepHeight = model.WindowHeight;
                    }

                    if (RadarViewOrder.windowsStatus)
                    {
                        DrowMyCharacter(dc);
                    }
                }

            }
        }

        int openAnimationY;
        private void FirstOpenWindowAnimetion()
        {
            openAnimationY = CLOSE_WINDOW_SET_HEIGHT + 1;
            this.Height = openAnimationY;
        }
        private bool OpenWindowAnimetion(DrawingContext dc,int maxY)
        {
            if (openAnimationY < maxY)
            {
                openAnimationY+=32;
                this.Height = openAnimationY;
                dc.DrawLine(new Pen(Brushes.LightSkyBlue, 1), new Point(0, openAnimationY-4), new Point(this.Width, openAnimationY-4));
                dc.DrawLine(new Pen(Brushes.Green, 1), new Point(0, openAnimationY), new Point(this.Width, openAnimationY));
                return false;
            }
            return true;
        }

        private void DrowMyCharacter(DrawingContext dc)
        {
            Rect rect = RadarViewOrder.PlayerRect();//
          
            dc.DrawEllipse(Brushes.Black, null, new Point(rect.Left, rect.Top), (double)rect.Width, (double)rect.Height);

            dc.DrawText(new FormattedText(rect.Left.ToString()+","+ rect.Top.ToString(),
            System.Globalization.CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight, new Typeface("Verdana"),
            6, Brushes.Red), new Point(rect.X-10, rect.Y+6 ));

            float sf = (180f * (float)RadarViewOrder.myRadian) / (float)3.1415;

            RotateTransform rt = new RotateTransform(sf+90);
            myIcon.LayoutTransform = rt;

        }


        private void namePlate(List<Combatant> searchObjects, DrawingContext dc )
        {
            foreach (Combatant mob in searchObjects)
            {
                bool flag = isFlag(mob.ID);
                int hpPar = (mob.CurrentHP * 100 / mob.MaxHP);
                bool shortName = true;
                if (model.SelectChecked) { shortName = false; }

                Rect rect = RadarViewOrder.MobRect(RadarViewOrder.myData.PosX, RadarViewOrder.myData.PosY, mob.PosX, mob.PosY);

                if (mob.ID != RadarViewOrder.myData.ID)
                {
                    if (rect.X < 20) { continue; }
                    if (rect.Y < 20) { continue; }
                    if (rect.X > img.Width - 40) { continue; }
                    if (rect.Y > img.Height - 20) { continue; }

                    dc.DrawEllipse(getBrush(hpPar, flag), null, new Point(rect.Left, rect.Top), (double)rect.Width, (double)rect.Height);
                }

                this.TextOut(dc, mob.Name, Brushes.LightGray, rect.X-4, rect.Y - 14, flag, shortName);
                if (model.AntiPersonalChecked)
                {
                    jobTextLayout job = GetJobTextLayout(mob.Job, rect, mob.IsCasting);
                    // テキスト
                    dc.DrawText(new FormattedText(job.job,
                    System.Globalization.CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight, new Typeface("Verdana"),
                    6, job.brush), new Point(job.left + 2, rect.Y -4));
                    if (mob.CastTargetID == RadarViewOrder.myData.ID)
                    {
                       
                        dc.DrawText(new FormattedText(job.job,
                        System.Globalization.CultureInfo.CurrentUICulture,
                        FlowDirection.LeftToRight, new Typeface("Verdana"),
                        6, Brushes.Red), new Point(rect.X + 2, rect.Y -4));
                    }
                }
            }
        }

        private struct jobTextLayout
        {
            public string job;
            public Brush brush;
            public double left;
        };

        private jobTextLayout GetJobTextLayout(int JobId ,Rect rect,bool IsCasting)
        {
            jobTextLayout job = new jobTextLayout();
            switch (JobId)
            {
                case 19:
                case 21:
                case 32:
                    job.job = "TANK"; job.brush = Brushes.DodgerBlue;  job.left = rect.Left + 5; break;
                case 20:
                case 22:
                case 30:
                    job.job = "MELE"; job.brush = Brushes.Orange; job.left = rect.Left + 5; break;
                case 23:
                case 31:
                    job.job = "RENG"; job.brush = Brushes.Green; job.left = rect.Left + 5; break;
                case 24:
                case 28:
                case 33:
                    if (IsCasting)
                    {
                        job.job = "HEAL◎"; job.brush = Brushes.LightGreen; job.left = rect.Left + 5; break;
                    }
                    else
                    {
                        job.job = "HEAL"; job.brush = Brushes.LightGreen; job.left = rect.Left + 5; break;
                    }
                case 25:
                case 27:
                    if (IsCasting)
                    {
                        job.job = "CAS@"; job.brush = Brushes.Yellow; job.left = rect.Left + 5; break;
                    }
                    else
                    {
                        job.job = "CAS@"; job.brush = Brushes.Yellow; job.left = rect.Left + 5; break;
                    }
                default:
                    job.job = ""; job.brush = Brushes.Black; job.left = rect.Left + 5; break;
                   
            }
            return job;

        }

        StringBuilder name = new StringBuilder();
        private void TextOut(DrawingContext dc, string charcterName, Brush color,  double left,  double top, bool flag, bool ShortName)
        {
            name.Length = 0;
            if (ShortName)
            {
                string[] ss = charcterName.Split(' ');
                
                if (ss.Length > 1)
                {
                    name.Append(ss[0].Substring(0, 1));
                    if (ss[1].Length>6)
                    {
                        name.Append('.' + ss[1].Substring(1, 6));
                    }
                    else
                    {
                        name.Append('.' + ss[1]);
                    }
                }
                else
                {
                    if (ss[0].Length > 6)
                    {
                        name.Append(ss[0].Substring(1, 6));
                    }else
                    {
                        name.Append(ss[0]);
                    }
                }
            }
            else
            {
                name.Append(charcterName);
            }

            if (flag)
            {
                dc.DrawText(new FormattedText("▶",
                System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface("Verdana"),
                8, Brushes.Aqua), new Point(left, top));

                dc.DrawText(new FormattedText(name.ToString(),
                System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface("Verdana"),
                8, Brushes.WhiteSmoke), new Point(left + 8, top));

                return;
            }
            dc.DrawText(new FormattedText(name.ToString(),
            System.Globalization.CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight, new Typeface("Verdana"),
            8, Brushes.WhiteSmoke), new Point(left, top));

        }

        private List<uint> FlagIDs = new List<uint>();
        private void SelectAll(ref List<Combatant> searchObjects)
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
                if (charcter.Name == "タイタン・エギ")
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

                if (!model.AntiPersonalChecked)
                {
                    if (charcter.Name.IndexOf("宝箱") > -0)
                    {
                        searchObjects.Add(charcter);
                    }
                    if (charcter.type == 2)
                    {
                        searchObjects.Add(charcter);
                    }
                }
                else
                {
                    if (charcter.type != 2)
                    {
                        searchObjects.Add(charcter);
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

        public void SetWindowRect(Rect rect)
        {
            model.WindowLeft = rect.Left;
            model.WindowTop = rect.Top;
            model.WindowWidth = rect.Width;
            model.WindowHeight = rect.Height;
        }



        private static Timer SetWindowTimer { get; set; }
        public string DesignHeight { get; private set; }
        public bool FlagKeepOn { get; private set; }










        #region  Click Not Hit
        //Spcial Spell Timer を参考にしました！　ありがとう
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            WindowInteropHelper helper = new WindowInteropHelper(this);
            WindowsApi32.SetWindowLong(helper.Handle, GWL_EXSTYLE, WindowsApi32.GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }



        #endregion
        private void rtClipBar_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveAction();
        }
    }


    /*
    EVENT_SYSTEM_SOUND = 0x1
    EVENT_SYSTEM_ALERT = 0x2
    EVENT_SYSTEM_FOREGROUND = 0x3
    EVENT_SYSTEM_MENUSTART = 0x4 
    EVENT_SYSTEM_MENUEND = 0x5
    EVENT_SYSTEM_MENUPOPUPSTART = 0x6
    EVENT_SYSTEM_MENUPOPUPEND = 0x7
    EVENT_SYSTEM_CAPTURESTART = 0x8
    EVENT_SYSTEM_CAPTUREEND = 0x9
    EVENT_SYSTEM_MOVESIZESTART = 0xa
    EVENT_SYSTEM_MOVESIZEEND = 0xb
    EVENT_SYSTEM_CONTEXTHELPSTART = 0xc
    EVENT_SYSTEM_CONTEXTHELPEND = 0xd
    EVENT_SYSTEM_DRAGDROPSTART = 0xe
    EVENT_SYSTEM_DRAGDROPEND = 0xf
    EVENT_SYSTEM_DIALOGSTART = 0x10
    EVENT_SYSTEM_DIALOGEND = 0x11
    EVENT_SYSTEM_SCROLLINGSTART = 0x12
    EVENT_SYSTEM_SCROLLINGEND = 0x13
    EVENT_SYSTEM_SWITCHSTART = 0x14
    EVENT_SYSTEM_SWITCHEND = 0x15
    EVENT_SYSTEM_MINIMIZESTART = 0x16
    EVENT_SYSTEM_MINIMIZEEND = 0x17
    */

}
