using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MultiProjectTools
{
    using System.Timers;
    public partial class ClockTimer
    {
        private static ClockTimer instance;

        public Timer watchTimer;

        public static ClockTimer Default
        {
            get
            {
                ClockTimer.Initialize();
                return instance;
            }
        }

        public static void Initialize()
        {
            if (instance == null)
            {
                instance = new ClockTimer()
                {
                    //PreviousMP = -1
                };

                instance.watchTimer = new Timer()
                {
                    Interval = 1000,//Settings.Default.ParameterRefreshRate,
                    Enabled = false
                };
            }
        }
        public static void Deinitialize()
        {
            if (instance != null)
            {
   
                if (instance.watchTimer != null)
                {
                    instance.watchTimer.Stop();
                    instance.watchTimer.Dispose();
                    instance.watchTimer = null;
                }
                instance = null;
            }
        }

    }

}
