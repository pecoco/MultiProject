using ACT.RadarViewOrder;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace Wpf.RadarWindow
{
    public partial class MainWindow
    {
        private void SaveAction()
        {
            if (callbackSaveSetting != null)
            {
                callbackSaveSetting();
            }
        }


        private void RadarWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            img.Width = RadarWindow.Width; img.Height = RadarWindow.Height;
        }
        private void btZoomIn_Click(object sender, RoutedEventArgs e)
        {
            RadarViewOrder.ZoomIn();
        }
        #region Flag On
        private void btFlagOn_Click(object sender, RoutedEventArgs e)
        {
            FlagKeepOn = true;
        }
        #endregion

        private void btZoomOut_Click(object sender, RoutedEventArgs e)
        {
            RadarViewOrder.ZoomOut();
        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            if (callbackSaveSetting != null)
            {
                callbackSaveSetting();
            }
        }

        private void btAntiPersonal_Click(object sender, RoutedEventArgs e)
        {
            if (callbackSaveSetting != null)
            {
                callbackSaveSetting();
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
            SaveAction();
        }

        bool isOpen = true;
        private double keepWidth;
        private double keepHeight;
        private void btSwitch_Click(object sender, RoutedEventArgs e)
        {
            //#FF9FFF9A
            if (isOpen)
            {
                keepWidth = model.WindowWidth;
                keepHeight = model.WindowHeight;

                if (this.ResizeMode == ResizeMode.CanResizeWithGrip)
                {
                    btResie_Click(sender, e);
                    btResize.IsChecked = false;
                }

                model.WindowWidth = btSwitch.Width + btSwitch.Margin.Left + 16;
                model.WindowHeight = 32;
                btSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 70, 70, 70));
                myIcon.Width = 0;

            }
            else
            {
                model.WindowWidth = keepWidth;
                model.WindowHeight = keepHeight;
                btSwitch.Background = new SolidColorBrush(Color.FromArgb(255, 200, 255, 200));
                myIcon.Width = 8;

            }

            isOpen = !isOpen;
        }

    }
}
