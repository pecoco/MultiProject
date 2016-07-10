using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using System.Windows.Media.Media3D;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;
using System.Reflection;

using System.Timers;
using System.Text.RegularExpressions;


namespace Wpf.RadarWindow
{
    public partial class MainWindow : Window
    {
       
        [STAThread]
        public static void Main()
        {
            Window mainWnd = new MainWindow();
            Application app = new Application();
            app.Run(mainWnd);
        }
       
        private DispatcherTimer mTimer;
        private RadarMainWindowViewModel model;

        MainWindow instance = null;
        public MainWindow()
        {

            instance = this;
            InitializeComponent();
            
            Loaded += (o, e) =>
            {
                var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                source.AddHook(new HwndSourceHook(WndProc));


            };
            
            Hookproc();

            //-
            model = new RadarMainWindowViewModel();
            String propertyName = "";
            model.PropertyChanged += new PropertyChangedEventHandler((s, e) => { propertyName = e.PropertyName; });

            model.WindowLeft = 500;
            model.WindowTop = 300;

            model.WindowWidth = 600;
            model.WindowHeight = 400;

            model.SelectChecked = true;
            DataContext = model;

            //-

            rtClipBar.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };

            mTimer = new DispatcherTimer(DispatcherPriority.Normal);
            mTimer.Interval = TimeSpan.FromSeconds(0.05);//50ミリ秒間隔に設定
            mTimer.Tick += new EventHandler(TickTimer);
            mTimer.Start();
        }

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
        static Int32 YY;

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == WM_SYSKEYDOWN) &&
                (wParam.ToInt32() == VK_F4))
            {
                handled = true;
            }
            MES = msg;

            YY = ((int)lParam >> 16) & 0xFFFF;
            if (MES == VM_LBUTTON)
            {
                if (YY > 80) {
                    handled = true;
                }
            }
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

        void TickTimer(object sender, EventArgs e)
        {
            if (isView)
            {

                if (CalculateCommand.CanExecute)
                {
                    CalculateCommand.Execute();
                }
                this.BackgroundInvoke();
            }
        }

        private List<Point> po = new List<Point>();
        private Livet.Commands.ViewModelCommand _CalculateCommand;
        public Livet.Commands.ViewModelCommand CalculateCommand
        {
            get
            {
                if (_CalculateCommand == null)
                {
                    _CalculateCommand = new Livet.Commands.ViewModelCommand(DataCreate, CanDataCreate);
                }
                return _CalculateCommand;
            }
        }

        private void DataCreate()
        {
            po.Clear();
            Random r = new Random();

            for (int i = 0; i < 100; i++)
            {
                po.Add(new Point(r.Next(1, 500), r.Next(1, 500)));
            }
        }
        private bool CanDataCreate()
        {
            var ret = true;
            this.BackgroundInvoke();
            return ret;
        }
        private void BackgroundInvoke()
        {

            this.Dispatcher.BeginInvoke(
                new Action(() => { Refresh(); }));
        }

        public void Refresh()
        {
            Render();
        }

        private void Render()
        {
            using (var dc = dg.Open())
            {
                // 四角形
                dc.DrawRectangle(Brushes.SkyBlue, null, new Rect(0, 0, 10, 100));

                // 画像
                //var image = BitmapFrame.Create(new Uri("Image.bmp", UriKind.Relative));
                //dc.DrawImage(image, new Rect(10, 5, 180, 90));

                for (int i = 0; i < po.Count; i++)
                {
                    if (po[i].X < 30) { continue; }
                    if (po[i].X > 260) { continue; }
                    if (po[i].Y < 30) { continue; }
                    if (po[i].Y > 260) { continue; }

                    dc.DrawEllipse(null, new Pen(Brushes.Red, 1), po[i], 5, 5);
                }

                dc.DrawRectangle(null, new Pen(Brushes.Red, 1), new Rect(0, 0, this.Width, this.Height));
               

                // dc.DrawRectangle(null, new Pen(Brushes.Yellow, 1), new Rect(0, 0, vbox.Width - 1, vbox.Height - 1));

                dc.DrawRectangle(null, new Pen(Brushes.Green, 1), new Rect(0, 0, img.Width - 1, img.Height - 1));

                //dc.DrawRectangle(null, new Pen(Brushes.Purple, 1), new Rect(0, 0, RadarWindow.Width - 1, RadarWindow.Height - 1));


                    // テキスト
                    dc.DrawText(new FormattedText(MES.ToString() + "width:" + Width.ToString() + " deW:" + img.Width.ToString(),
                    System.Globalization.CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight, new Typeface("Verdana"),
                    16, Brushes.White), new Point(0, 80));

                


                // テキスト
                dc.DrawText(new FormattedText(MES.ToString() + "Drawing sample!" + YY.ToString(),
                System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface("Verdana"),
                16, Brushes.Yellow), new Point(40, 40));

                // 線
                dc.DrawLine(new Pen(Brushes.Green, 2), new Point(5, 5), new Point(195, 95));
            }
        }



        private void btResie_Click(object sender, RoutedEventArgs e)
        {
            if (this.ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                this.ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }

            //model.WindowWidth = 500;

        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged && isView == false)
            {
                if (this.Width < 100) { this.Width = 100; }

            }
        }

        bool isOpen = true;
        private double keepWidth;
        private double keepHeight;
        private void btSwitch_Click(object sender, RoutedEventArgs e)
        {
            //#FF9FFF9A
            if (isOpen)
            {
                keepWidth = this.Width;
                keepHeight = this.Height;
                this.Width = btSwitch.Width + btSwitch.Margin.Left + 16;
                this.Height = btSwitch.Height + btSwitch.Margin.Bottom;
                btSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
            }
            else
            {
                this.Width = keepWidth;
                this.Height = keepHeight;
                btSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 200, 255, 200));
            }

            isOpen = !isOpen;
        }
        private void RadarWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           // cvs.Width = RadarWindow.Width-1; cvs.Height = RadarWindow.Height-1;
            img.Width = RadarWindow.Width; img.Height = RadarWindow.Height;
            //this.Width = RadarWindow.Width-1; this.Height = RadarWindow.Height-1;


        }
        //--------------------------------------
        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        int WH_MOUSE_LL = 0x14;

        private static int hHook = 0;
        private void Hookproc()
        {
            

            // IntPtr handle = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
            // hHook = SetWindowsHookEx(WH_MOUSE_LL, new HookProc(MouseHookProc), handle, 0);
            HookWidgetTopMost();
        }
        ~MainWindow(){
            if (hHook != 0)
            {
                bool ret = UnhookWindowsHookEx(hHook);
                if (ret == false)
                {
                    return;
                }
                hHook = 0;
            }
        }

        private static int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
   /*         if (Form1.ActiveForm != null)
            {
                KeyBoardLLHookStruct MyHookStruct = (KeyBoardLLHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardLLHookStruct));
                if (nCode == 0)
                {
                    // 91 : 左Windowsキー  92 : 右Windowsキー
                    if ((MyHookStruct.vkCode == 91) || (MyHookStruct.vkCode == 92))
                    {
                        // 0以外を返すと無効
                        return 1;
                    }
                }
            }*/
            // 対象のキー以外
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static Timer SetWindowTimer { get; set; }
        public string DesignHeight { get; private set; }

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        public const uint EVENT_SYSTEM_FOREGROUND = 3;
        private static WinEventDelegate _delegate;
        private static IntPtr _mainHandleHook;
        public const uint WINEVENT_OUTOFCONTEXT = 0;
        public static void HookWidgetTopMost()
        {
            try
            {
                _delegate = BringWidgetsIntoFocus;
                _mainHandleHook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _delegate, 0, 0, WINEVENT_OUTOFCONTEXT);
            }
            catch (Exception e)
            {
            }

        }
        private static void BringWidgetsIntoFocus(IntPtr hwineventhook, uint eventtype, IntPtr hwnd, int idobject, int idchild, uint dweventthread, uint dwmseventtime)
        {
            BringWidgetsIntoFocus(hwnd);
        }

        private static void BringWidgetsIntoFocus(IntPtr hwnd)
        {
            try
            {
                var handle = GetForegroundWindow();
                var activeTitle = GetActiveWindowTitle();
                if (activeTitle.IndexOf("FainalFunta") < 0)
                {
                  
                }
                else
                {

                }

                //FF14以外がアクティブになったら閉じる機能
             }
            catch (Exception ex)
            {
            }
        }

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            var handle = IntPtr.Zero;
            var Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            return GetWindowText(handle, Buff, nChars) > 0 ? Buff.ToString() : "";


        }
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return null;
        }

        #region  Click Not Hit
        //Spcial Spell Timer を参考にしました！　ありがとう
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SetWindowLong(helper.Handle, GWL_EXSTYLE, GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }
        #endregion

        private void btZoomIn_Click(object sender, RoutedEventArgs e)
        {


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
