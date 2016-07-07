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

namespace RadarWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            Window wnd = new MainWindow();
            Application app = new Application();
            app.Run(wnd);

        }

        const int WM_SYSKEYDOWN = 0x0104;
        const int VK_F4 = 0x73;
        private DispatcherTimer mTimer;
     



        public MainWindow()
        {
            InitializeComponent();

            mTimer = new DispatcherTimer(DispatcherPriority.Normal);
            mTimer.Interval = TimeSpan.FromSeconds(1);//100ミリ秒間隔に設定
            mTimer.Tick += new EventHandler(TickTimer);
            mTimer.Start();

            isView = true;
            Loaded += (o, e) =>
            {
                var source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
                source.AddHook(new HwndSourceHook(WndProc));
            };
        }


        //WndProc
        static Int32 YY;
        static Int32 MES;
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == WM_SYSKEYDOWN) &&
                (wParam.ToInt32() == VK_F4))
            {
                handled = true;
            }
            MES = msg;

                YY = ((int)lParam>>16);
                if (MES == 512)
                {
                   // if (YY > 16) { handled = true; }
                }
            
            return IntPtr.Zero;
        }
        private static Action EmptyDelegate = delegate () { };
        void TickTimer(object sender, EventArgs e)
        {
            /*            DispatcherFrame frame = new DispatcherFrame();
                        var callback = new DispatcherOperationCallback(obj =>
                        {
                            ((DispatcherFrame)obj).Continue = false;
                            return null;
                        });
                        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
                        Dispatcher.PushFrame(frame);
                        */
            //Dispatcher.BeginInvoke(new Action(() => { }), DispatcherPriority.Render);
            using (var dc = dg.Open())
            {
                // 四角形
                dc.DrawRectangle(Brushes.SkyBlue, null, new Rect(0, 0, 10, 100));

                // 画像
                //var image = BitmapFrame.Create(new Uri("Image.bmp", UriKind.Relative));
                //dc.DrawImage(image, new Rect(10, 5, 180, 90));

                // 円
                dc.DrawEllipse(null, new Pen(Brushes.Red, 1), new Point(100, 50), 80, 40);

                // テキスト
                dc.DrawText(new FormattedText(MES.ToString()+"Drawing sample!" +YY.ToString(),
                System.Globalization.CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface("Verdana"),
                16, Brushes.Black), new Point(40, 40));

                // 線
                dc.DrawLine(new Pen(Brushes.Green, 2), new Point(5, 5), new Point(195, 95));
            }
        }



  //      protected override void OnRender(DrawingContext drawingContext)
  //      {
   //         base.OnRender(drawingContext);

            //Pen pen = new Pen(Brushes.Black, 10);
            //Point pt1 = new Point(10, 10);
            //Point pt2 = new Point(10, 100);
            //drawingContext.DrawLine(pen, pt1, pt2);

/*
            drawingContext.DrawEllipse(Brushes.LightPink, null, new Point(150, 150), 3, 3);
            drawingContext.DrawText(
   new FormattedText("Click Me!"+YY,
      CultureInfo.GetCultureInfo("en-us"),
      FlowDirection.LeftToRight,
      new Typeface("Verdana"),
      36, System.Windows.Media.Brushes.Black),
      new System.Windows.Point(200, 116));
*/
    //    }




        //function Button
        private Point mousePoint;
        private bool isView;



        private void rtClipBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                mousePoint = e.GetPosition(rtClipBar);
            }
        }

        private void rtClipBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                Point mousePointNow = e.GetPosition(rtClipBar);
                this.Left += mousePointNow.X - mousePoint.X;
                this.Top += mousePointNow.Y - mousePoint.Y;
            }
        }

        private void btResie_Click(object sender, RoutedEventArgs e)
        {
            if (this.ResizeMode == ResizeMode.CanResizeWithGrip)
            {
                this.ResizeMode = ResizeMode.NoResize;
            }
            else {
                this.ResizeMode = ResizeMode.CanResizeWithGrip;
            }
           


        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged && isView==false)
            {
                if (this.Width < 100) { this.Width = 100; }

            }
        }


        private double keepWidth;
        private double keepHeight;
        private void btSwitch_Click(object sender, RoutedEventArgs e)
        {
            //#FF9FFF9A
            if (isView)
            {
                keepWidth = this.Width;
                keepHeight = this.Height;
                this.Width = btSwitch.Width+ btSwitch.Margin.Left+16 ;
                this.Height = btSwitch.Height+ btSwitch.Margin.Bottom;
                btSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
            }
            else {
                this.Width = keepWidth;
                this.Height = keepHeight;
                btSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 200, 255, 200));
            }

            isView = !isView;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
