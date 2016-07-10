using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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

        private bool antiPersonnelChecked;
        public bool AntiPersonnelChecked
        {
            get { return antiPersonnelChecked; }
            set
            {
                if (antiPersonnelChecked != value)
                {
                    antiPersonnelChecked = value;
                    RaisePropertyChanged("antiPersonnelChecked");
                }
            }
        }





    }
}
