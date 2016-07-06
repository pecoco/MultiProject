using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        public MainWindow()
        {
            InitializeComponent();
            //this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
        }

        private Point mousePoint;
 



        private void btVer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                Point mousePointNow = e.GetPosition(btVer);
                this.Left += mousePointNow.X - mousePoint.X;
                this.Top += mousePointNow.Y - mousePoint.Y;

            }
        }

        private void btVer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //this.FormBorderStyle = FormBorderStyle.None;
                mousePoint = e.GetPosition(btVer);
            }
        }

        private void btVer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
