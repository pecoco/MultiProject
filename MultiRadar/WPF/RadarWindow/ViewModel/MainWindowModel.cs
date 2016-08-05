using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace Wpf.RadarWindow
{
    internal sealed class RadarMainWindowViewModel :  INotifyPropertyChanged
    {
        #region Property Bindings

        private static RadarMainWindowViewModel _instance;

        public static RadarMainWindowViewModel Instance
        {
            get { return _instance ?? (_instance = new RadarMainWindowViewModel()); }
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        #region
        private double windowLift;
        public double WindowLeft
        {
            get { return windowLift; }
            set {
                if (windowLift != value)
                {
                    windowLift = value;
                    RaisePropertyChanged("WindowLeft");
                }
            }
        }

        private double windowTop;
        public double WindowTop
        {
            get {return windowTop; }
            set
            {
                if (windowTop != value)
                {
                    windowTop = value;
                    RaisePropertyChanged("WindowTop");
                }
            }
        }

        private double windowWidth;
        public double WindowWidth
        {
            get { return windowWidth; }
            set
            {
                if (windowWidth != value)
                {
                    windowWidth = value;
                    RaisePropertyChanged("WindowWidth");
                }
            }
        }

        private double windowHeight;
        public double WindowHeight
        {
            get { return windowHeight; }
            set
            {
                if (windowHeight != value)
                {
                    windowHeight = value;
                    RaisePropertyChanged("WindowHeight");
                }
            }
        }
        #endregion

        //All or Limit Select Mob
        private bool selectChecked;
        public bool SelectChecked
        {
            get { return selectChecked; }
            set
            {
                if (selectChecked != value)
                {
                    selectChecked = value;
                    RaisePropertyChanged("SelectChecked");
                }
            }
        }

        private bool antiPersonalChecked;
        public bool AntiPersonalChecked
        {
            get { return antiPersonalChecked; }
            set
            {
                if (antiPersonalChecked != value)
                {
                    antiPersonalChecked = value;
                    RaisePropertyChanged("AntiPersonalChecked");
                }
            }
        }
        private bool idModeCheckrd;
        public bool IdModeCheckrd
        {
            get { return idModeCheckrd; }
            set
            {
                if (idModeCheckrd != value)
                {
                    idModeCheckrd = value;
                    RaisePropertyChanged("IdModeCheckrd");
                }
            }
        }

        private float windowOpacity;
        public float WindowOpacity
        {
            get { return windowOpacity; }
            set
            {
                if (windowOpacity != value)
                {
                    windowOpacity = value;
                    RaisePropertyChanged("WindowOpacity");
                }
            }
        }

        private bool viewAreaCheckrd;
        public bool ViewAreaCheckrd
        {
            get { return viewAreaCheckrd; }
            set
            {
                if (viewAreaCheckrd != value)
                {
                    viewAreaCheckrd = value;
                    RaisePropertyChanged("ViewAreaCheckrd");
                }
            }
        }
        



        /*
        private Margin myIconMargin;
        public double MyIconX
        {
            set
            {
                if (myIconMargin != value)
                {
                    myIconMargin = value;
                    RaisePropertyChanged("MyIconX");
                }
            }
        }
        */


    }
    public class ThicknessMultiConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double left = System.Convert.ToDouble(values[0]);
            double top = System.Convert.ToDouble(values[1]);
            double right = System.Convert.ToDouble(values[2]);
            double bottom = System.Convert.ToDouble(values[3]);
            return new Thickness(left, top, right, bottom);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            Thickness thickness = (Thickness)value;
            return new object[]
            {
            thickness.Left,
            thickness.Top,
            thickness.Right,
            thickness.Bottom
            };
        }

        #endregion
    }
}
