using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using System.ComponentModel;
using System.Timers;
using ACT.RadarViewOrder;
using MultiProject.Common;
using ACT.Radardata;
using MultiProject;


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

        private Pen areaPen;
        private Pen myAreaPen;
        private Pen circlePen;
        private Pen circleOffPen;

        public MainWindow()
        {

            instance = this;
            InitializeComponent();
            
            windowRectBrush.Color = Color.FromArgb(1, 80, 80, 80);
            model = new RadarMainWindowViewModel();
   
            Loaded += (o, e) =>
            {
                model.WindowOpacity = (float)RadarViewOrder.Opacity / 100;
                var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                source.AddHook(new HwndSourceHook(WndProc));
                areaPen = new Pen(Brushes.LawnGreen, 1);
                myAreaPen = new Pen(Brushes.LightCyan, 1);
                circlePen = new Pen(Brushes.Lime, 1);
                circleOffPen = new Pen(windowRectBrush, 1);

                SelectZoomSelect();

                mTimer.Interval = TimeSpan.FromSeconds(0.05);//50ミリ秒間隔に設定
                mTimer.Tick += new EventHandler(TickTimer);
                mTimer.Start();
            };

            string propertyName = "";
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

                if (RadarViewOrder.LuckUpS)
                {

                }
                RadarViewOrder.SetBasePosition((int)this.Left, (int)this.Top, (int)img.Width-1, (int)img.Height-1);
                RadarViewOrder.myData = ActData.AllCharactor[0];

                //Zoom
                dc.DrawText(new FormattedText((RadarViewOrder.RadarZoom).ToString(),System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface("Verdana"),7, Brushes.LightGray), new Point(136, 10));

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

                        if (OpenWindowAnimetion(dc, RadarViewOrder.keepWindowHeight))
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
                        DrowMyCharacter(searchObjects[0],dc);
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
            if (openAnimationY+32 < maxY)
            {
                openAnimationY+=32;
                this.Height = openAnimationY;
                dc.DrawLine(new Pen(Brushes.LightSkyBlue, 1), new Point(0, openAnimationY-4), new Point(this.Width, openAnimationY-4));
                dc.DrawLine(new Pen(Brushes.Green, 1), new Point(0, openAnimationY), new Point(this.Width, openAnimationY));
                RadarViewOrder.isRadarWindowAnimation = true;
                return false;
            }else
            {
                if (openAnimationY != RadarViewOrder.keepWindowHeight)
                {
                    openAnimationY = RadarViewOrder.keepWindowHeight;
                    this.Height = openAnimationY;
                }
            }
            RadarViewOrder.isRadarWindowAnimation = false;
            return true;
        }

        private void DrowMyCharacter(Combatant myData,DrawingContext dc)
        {
            Rect rect = RadarViewOrder.PlayerRect();//
          
            dc.DrawEllipse(Brushes.Black, null, new Point(rect.Left+2.5, rect.Top+2.5), (double)rect.Width, (double)rect.Height);

            Point areaPos = RadarViewOrder.AreaPos();
            

            if (RadardataInstance.viewOptionData.IsLinkView(RadarViewOrder.radarZoomSelect))
            {
                dc.DrawEllipse(null, circlePen, new Point(rect.Left , rect.Top + 2), (double)areaPos.X/2, (double)areaPos.Y/2);
            }else
            {
                dc.DrawEllipse(null, circleOffPen, new Point(rect.Left, rect.Top + 2), (double)areaPos.X / 2, (double)areaPos.Y / 2);
            }

            if (model.ViewAreaCheckrd)
            {
                switch (myData.Job)
                {
                    case 19://knight
                    case 32://暗黒
                        DrawingArea(dc, rect, 15, 5, myAreaPen, Color.FromArgb(100, 106, 48, 80)); break;//Flash
                    case 21://戦士
                        DrawingArea(dc, rect, 15, 0, myAreaPen, Color.FromArgb(100, 106, 48, 80)); break;//Flash
                    case 23://詩人
                        DrawingArea(dc, rect, 25, 20, myAreaPen, Color.FromArgb(100, 106, 148, 80)); break;//Flash
                    case 31://機工
                        DrawingArea(dc, rect, 25, 0, myAreaPen, Color.FromArgb(100, 106, 48, 80)); break;//Flash
                    case 25://黒
                    case 27://召喚
                        DrawingArea(dc, rect, 25, 0, myAreaPen, Color.FromArgb(100, 106, 48, 80)); break;//30
                    case 24://白
                    case 28://学
                    case 33://占
                            //ケアル、メディカラ
                        DrawingArea(dc, rect, 30, 15, areaPen, Color.FromArgb(100, 40, 148, 80)); break;//30 15
                }
            }
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
                    if (rect.X > img.Width - (40 + (RadarViewOrder.FontSize*5))) { continue; }
                    if (rect.Y > img.Height ) { continue; }

                    if (model.IdModeCheckrd && mob.type == 1)
                    {
                        dc.DrawEllipse(getBrush(hpPar, true), null, new Point(rect.Left, rect.Top), (double)rect.Width, (double)rect.Height);
                    }
                    else
                    {
                        if (model.IdModeCheckrd && mob.Name == "トラップ")
                        {
                            dc.DrawRectangle( Brushes.Yellow, null,rect);
                        }
                        else {
                            dc.DrawEllipse(getBrush(hpPar, flag), null, new Point(rect.Left, rect.Top), (double)rect.Width, (double)rect.Height);
                        }
                    }
                }

                if (RadardataInstance.viewOptionData.IsNameView(RadarViewOrder.radarZoomSelect))
                {
                    this.TextOut(dc, mob.Name, Brushes.LightGray, rect.X - 4, rect.Y + RadarViewOrder.fontTop, flag, shortName);
                }
                float vX = 0;
                float vY = 0;

                if (mob.PosX < 0)
                {
                    vX = 22- 22*((mob.PosX * -1)/1100);
                }else
                {
                    vX = 22 + 22 * ((mob.PosX ) / 1100);
                }
                if (mob.PosY < 0)
                {
                    vY = 22 - 22 * ((mob.PosY * -1) / 1100);
                }
                else
                {
                    vY = 22 + 22 * ((mob.PosY) / 1100);
                }


                if (RadardataInstance.viewOptionData.IsPositionView(RadarViewOrder.radarZoomSelect))
                {
                    dc.DrawText(new FormattedText(((int)(vX)).ToString() + "," + ((int)(vY)).ToString(),
                    System.Globalization.CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight, new Typeface("Verdana"),
                    RadarViewOrder.FontSize + 2, Brushes.Red), new Point(rect.X - 4, rect.Y + 4));
                }



                if (model.IdModeCheckrd)
                {
                    if (model.ViewAreaCheckrd && RadarViewOrder.myData.ID != mob.ID)
                    {
                        switch (mob.Job)
                        {
                            case 24:
                            case 28:
                            case 33: DrawingArea(dc, rect, 30, 15,areaPen, Color.FromArgb(100, 40, 148, 80)); break;//ケアル、メディカラ
                        }
                    }
                    continue;
                }

                if (RadardataInstance.viewOptionData.IsJobView(RadarViewOrder.radarZoomSelect))
                {
                    jobTextLayout job = GetJobTextLayout(mob.Job, rect, mob.IsCasting);
                    // JOBテキスト

                    //HP表示と重なるときは、Job表示をずらす
                    int plusX = 2;
                    if (RadardataInstance.viewOptionData.IsHpView(RadarViewOrder.radarZoomSelect))
                    {
                        plusX = -28;
                    }

                    dc.DrawText(new FormattedText(job.job,
                    System.Globalization.CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight, new Typeface("Verdana"),
                    RadarViewOrder.FontSize, job.brush), new Point(job.left + plusX, rect.Y - 4));

                }
                if (RadardataInstance.viewOptionData.IsHpView(RadarViewOrder.radarZoomSelect))
                {
                    dc.DrawText(new FormattedText(mob.MaxHP.ToString(),
                    System.Globalization.CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight, new Typeface("Verdana"),
                    RadarViewOrder.FontSize, Brushes.Aqua), new Point(rect.X + 4, rect.Y-4));
                }
            }
        }

        private void DrawingArea(DrawingContext dc,Rect rect, int r1Value, int r2Value ,Pen pen, Color color)
        {
            Rect area;
            if (r1Value > 0)
            {
                area = RadarViewOrder.AreaRect(r1Value);
                dc.DrawEllipse(null, pen, new Point(rect.Left, rect.Top + 2), (double)area.X / 2, (double)area.Y / 2);
            }
            if (r2Value > 0)
            {
                area = RadarViewOrder.AreaRect(r2Value);
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = color;
                dc.DrawEllipse(mySolidColorBrush, null, new Point(rect.Left, rect.Top + 2), (double)area.X / 2, (double)area.Y / 2);
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
                        job.job = "HEA●"; job.brush = Brushes.LightGreen; job.left = rect.Left + 5; break;
                    }
                    else
                    {
                        job.job = "HEAL"; job.brush = Brushes.LightGreen; job.left = rect.Left + 5; break;
                    }
                case 25:
                case 27:
                    if (IsCasting)
                    {
                        job.job = "CAS●"; job.brush = Brushes.Yellow; job.left = rect.Left + 5; break;
                    }
                    else
                    {
                        job.job = "CAS"; job.brush = Brushes.Yellow; job.left = rect.Left + 5; break;
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
                        name.Append('.' + ss[1].Substring(0, 6));
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
                        name.Append(ss[0].Substring(0, 6));
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
                RadarViewOrder.FontSize+2, Brushes.Aqua), new Point(left, top));

                dc.DrawText(new FormattedText(name.ToString(),
                System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface("Verdana"),
                RadarViewOrder.FontSize+2, Brushes.WhiteSmoke), new Point(left + 8, top));

                return;
            }
            dc.DrawText(new FormattedText(name.ToString(),
            System.Globalization.CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight, new Typeface("Verdana"),
            RadarViewOrder.FontSize+2, Brushes.WhiteSmoke), new Point(left, top));

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
                if (charcter.Name == "カーバンクル")
                {
                    continue;
                }
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


                if (model.IdModeCheckrd)//ID Mode
                {
                    searchObjects.Add(charcter);
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
