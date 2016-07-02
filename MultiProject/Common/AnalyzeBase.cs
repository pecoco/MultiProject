using ACT.Radardata;
using System.Collections;
using System.Globalization;
using System.Text;

namespace MultiProject
{
    public class AnalyzeBase
    {
        private string playerName = "";

        public string PlayerName
        {
            set { playerName = value; }
            get { return playerName; }
        }

        public enum ChengeParameter : int
        {
            enochianTime,
            changedZone,
            flag,
            chengeJob,
            SwordOathOn,
            ShieldOathOn,
            SwordOathOff,
            ShieldOathOff,
            rathOn,
            rathOff,
            dethtoroyerOn,
            dethtoroyerOff,
        }

        private int myJobId;
        public int MyJobId
        {
            get { return myJobId; }

        }
        private Hashtable NametoIdTable = new Hashtable();
        public ChengeParameter chenge;

        private bool compareString(string logLine, StringBuilder chkText, ref int hitIndex)
        {
            int n = logLine.IndexOf(chkText.ToString());
            hitIndex = n;
            if (n > -1)
            {
                return true;
            }
            return false;
        }

        protected StringBuilder text = new StringBuilder();
        protected int hitIndex = 0;
        protected int MaxLength = 0;

        virtual protected bool AnalyzeProc00(string logLine)
        {
            return false;
        }
        virtual protected bool AnalyzeProc03(string logLine)
        {
            return false;
        }
        virtual protected bool AnalyzeProc15(string logLine)
        {
            return false;
        }
        virtual protected bool AnalyzeProc1A(string logLine)
        {
            return false;
        }
        virtual protected bool AnalyzeProc1E(string logLine)
        {
            return false;
        }

        virtual public bool AnalyzeLogLine(string logLine)
        {

            int nCode;
            CultureInfo provider = new CultureInfo("en-US");
            //16進コードの変換に注意
            if (int.TryParse(logLine.Substring(15, 2), System.Globalization.NumberStyles.HexNumber, provider, out nCode))
            {
                switch(nCode){
                    case 0: return AnalyzeProc00(logLine);
                    case 3: return AnalyzeProc03(logLine);
                    case 0x15: return AnalyzeProc15(logLine);
                    case 0x1A: return AnalyzeProc1A(logLine);
                    case 0x1E: return AnalyzeProc1E(logLine);
                }
            }

            text.Length = 0;
            text.Append("Changed Zone to ");
            MaxLength = logLine.Length;

            if (compareString(logLine, text, ref hitIndex))
            {
                RadardataInstance.Zone = logLine.Substring(hitIndex + "Changed Zone to ".Length, MaxLength - (hitIndex + "Changed Zone to ".Length));
                chenge = ChengeParameter.changedZone;
                return true;
            }
            return false;
        }
    }
}
