using ACT.Radardata;
using Advanced_Combat_Tracker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiProject
{
    public class AnalyzeBase
    {
        /*
        int enochianTime;
        public int EnochianTime
        {
            get { return enochianTime; }
        }

        StringBuilder zone = new StringBuilder();
        public string Zone
        {
            get { return zone.ToString(); }
            set { zone.Length = 0; zone.Append(value); }
        }


        StringBuilder flag = new StringBuilder();
        public string Flag
        {
            get { return flag.ToString(); }
            set { flag.Length = 0; flag.Append(value); }
        }

        private string playerName = "";

        public string PlayerName
        {
            set { playerName = value; }
            get { return playerName; }
        }

        public List<SkillTimer> abilityTimer = new List<SkillTimer>();
        public List<SkillTimer> reCastTimer = new List<SkillTimer>();

        */
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

        static string Seconds = "Seconds.";
        static int SecondsCount = "Seconds.".Length;
        static string gainsTheEffectOf = " gains the effect of ";
        static int gainsTheEffectOfCount = gainsTheEffectOf.Length;

        static string EffectOffFirst = "の「";
        static int EffectOffFirstCount = EffectOffFirst.Length;

        static char[] SecondsBuff = new char[SecondsCount];

        StringBuilder code = new StringBuilder();





        private bool isString(string logLine, StringBuilder chkText, ref int hitIndex)
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

        virtual protected void SetAbilityTable(int myJobId)
        {
            //NametoIdTable = AbilityData.GetaAbilityIdfromName(myJobId);
        }


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

            /*
            StringBuilder text = new StringBuilder("Remaining");
            int MaxLength = logLine.Length;
            int hitIndex = 0;
            string v;
            if (isString(logLine, text, ref hitIndex))
            {
                text.Length = 0;
                text.Append("Enochian. ");
                if (isString(logLine, text, ref hitIndex))
                {
                    v = logLine.Substring(hitIndex + "Enochian. ".Length, MaxLength - (hitIndex + "Enochian. ".Length));
                    int.TryParse(v, out enochianTime);
                }
                chenge = ChengeParameter.enochianTime;
                return true;
            }
            */
            int nCode;

            //16進コードの変換に注意
            if(int.TryParse(logLine.Substring(15, 2), out nCode))
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


            if (isString(logLine, text, ref hitIndex))
            {
                RadardataInstance.Zone = logLine.Substring(hitIndex + "Changed Zone to ".Length, MaxLength - (hitIndex + "Changed Zone to ".Length));
                chenge = ChengeParameter.changedZone;
                return true;
            }




            /*


            text = new StringBuilder();
            hitIndex = 0;


            if (playerName != "")
            {
                text.Length = 0;
                text.Append("03:Added new combatant ");
                if (isString(logLine, text, ref hitIndex))
                {
                    if (logLine.Substring(hitIndex + "03:Added new combatant ".Length, playerName.Length) == playerName)
                    {
                        myJobId = int.Parse(logLine.Substring(39 + playerName.Length + 7, 2).Trim());
                        chenge = ChengeParameter.chengeJob;
                        SetAbilityTable(myJobId);
                        return true;
                    }


                    string mobName = logLine.Substring(hitIndex + "03:Added new combatant ".Length);

                    foreach (string zoneList in RadardataInstance.radarData.ZoneList())
                    {
                        if (zoneList == RadardataInstance.Zone)
                        {

                            RadardataInstance.radarData.getMobList(zoneList);

                            ACT.MultiViewer.ZoneMobData rm = RadardataInstance.radarData.getMobList(zoneList);
                            if (rm != null)
                            {

                                foreach (string mob in rm.s)
                                {
                                    if (mobName.IndexOf(mob) > -1)
                                    {
                                        RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "s"));
                                        break;
                                    }
                                }
                                foreach (string mob in rm.a)
                                {
                                    if (mobName.IndexOf(mob) > -1)
                                    {
                                        RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "a"));
                                    }
                                }
                                foreach (string mob in rm.b)
                                {
                                    if (mobName.IndexOf(mob) > -1)
                                    {
                                        RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "b"));
                                    }
                                }
                                foreach (string mob in rm.etc)
                                {
                                    if (mobName.IndexOf(mob) > -1)
                                    {
                                        RadarViewOrder.AddHitMob(new RadarViewOrder.HitMobdata(mob, "e"));
                                    }
                                }
                            }
                        }
                    }
                    return false;
                }
            }
            */
            /*
            text.Length = 0;
            text.Append("」が切れた。");
            if (isString(logLine, text, ref hitIndex))
            {
                if (logLine.Substring(23, playerName.Length) == playerName)
                {
                    text.Length = 0;
                    text.Append(logLine.Substring(23+playerName.Length + EffectOffFirstCount, hitIndex - (23 + playerName.Length + EffectOffFirstCount)));

                    if (text.ToString() == "忠義の剣")
                    {
                        chenge = ChengeParameter.SwordOathOff;
                        return true;
                    }
                    if (text.ToString() == "忠義の盾")
                    {
                        chenge = ChengeParameter.ShieldOathOff;
                        return true;
                    }
                    if (text.ToString()== "ディフェンダー")
                    {
                        chenge = ChengeParameter.rathOff;
                        checkNeedAbility(330);
                        return true;
                    }
                    if (text.ToString() == "デストロイヤー")
                    {
                        chenge = ChengeParameter.dethtoroyerOff;
                        return true;
                    }
                }
                return false;
            }
            */

            //[07:39:12.437] 1E:Shinon Lu loses the effect of アストラルファイア from Shinon Lu.



            /*


            if (code.ToString() == "15")
            {
                if (logLine.Substring(27, playerName.Length) == playerName)
                {
                    text.Length = 0;
                    int aId = 0;

                    text.Append(logLine.Substring(31 + playerName.Length, 64).Split(':')[0]);
                    if (text.Length == 0)
                    {
                        text.Append(logLine.Substring(32 + playerName.Length, 64).Split(':')[0]);
                    }
                    if (NametoIdTable.ContainsKey(text.ToString()))
                    {
                        aId = (int)NametoIdTable[text.ToString()];
                    }
                    else
                    {
                        return false;
                    }
                    foreach (KeyValuePair<int, Ability> ab in AbilityData.abilities)
                    {

                        if (ab.Key == aId)
                        {
                            AddSkillTimer(ab.Key, 1, ab.Value.GetMaxReCast(MyJobId), ab.Value.abilityNameJp);
                            return false;
                        }
                    }
                }
            }
            */
            /*
            if (code.ToString() == "1E")
            {
                if (logLine.Substring(18, playerName.Length) == playerName)
                {

                    if (logLine.Substring(18 + playerName.Length + 1, "loses the effect of".Length) == "loses the effect of")
                    {
                        int aId = 0;
                        int fiNdex = 18 + playerName.Length + 2 + "loses the effect of".Length;
                        if (logLine.Substring(fiNdex).IndexOf(playerName) == -1)
                        {
                            return false;
                        }
                        if (NametoIdTable.ContainsKey(logLine.Substring(fiNdex, logLine.Length - fiNdex - playerName.Length - 3 - "from".Length)))
                        {
                            aId = (int)NametoIdTable[logLine.Substring(fiNdex, logLine.Length - fiNdex - playerName.Length - 3 - "from".Length)];
                        }
                        else
                        {
                            return false;
                        }
                        foreach (KeyValuePair<int, Ability> ab in AbilityData.abilities)
                        {

                            if (ab.Key == aId)
                            {
                                StopSkillTimer(ab.Key);
                                switch (aId)
                                {
                                    case 130:// "忠義の剣"
                                        chenge = ChengeParameter.SwordOathOff;
                                        return true;
                                    case 140:// "忠義の盾"
                                        chenge = ChengeParameter.SwordOathOff;
                                        return true;
                                    case 330:// "ディフェンダー"
                                        chenge = ChengeParameter.rathOff;
                                        return true;
                                    case 352:// "デストロイヤー"
                                        chenge = ChengeParameter.dethtoroyerOff;
                                        return true;
                                }
                                //chenge = ChengeParameter.;
                                return false;
                            }
                        }
                    }
                }
            }
            */
            /*
            //text.Length - SecondsCount, SecondsBuff, SecondsCount
            if (0 == string.Compare(Seconds, logLine.Substring(logLine.Length - SecondsCount, SecondsCount)))
            {

                if (code.ToString() == "1A")
                {
                    if (logLine.Substring(18, playerName.Length) == playerName)
                    {

                        int sec = chkNum(logLine.Substring(logLine.Length - SecondsCount - 7, 7));
                        if (sec == 0) { return false; }

                        text.Length = 0;
                        text.Append("from");
                        if (isString(logLine.Substring(18 + gainsTheEffectOfCount + playerName.Length), text, ref hitIndex))
                        {
                            int aId = 0;
                            if (NametoIdTable.ContainsKey(logLine.Substring(18 + gainsTheEffectOfCount + playerName.Length, hitIndex).TrimEnd()))
                            {
                                aId = (int)NametoIdTable[(logLine.Substring(18 + gainsTheEffectOfCount + playerName.Length, hitIndex).TrimEnd())];
                            }
                            else
                            {
                                return false;
                            }

                            foreach (KeyValuePair<int, Ability> ab in AbilityData.abilities)
                            {

                                // if (ab.Value.abilityNameJp == (logLine.Substring(18 + gainsTheEffectOfCount + playerName.Length, hitIndex).TrimEnd())){
                                if (ab.Key == aId)
                                {


                                    //忠義やラースなどのモードを設定
                                    switch (ab.Key)
                                    {
                                        case 130://忠義の剣
                                            chenge = ChengeParameter.SwordOathOn;
                                            return true;
                                        case 140://忠義の盾
                                            chenge = ChengeParameter.ShieldOathOn;
                                            return true;
                                        case 330://ラース　ON
                                            chenge = ChengeParameter.rathOn;
                                            return true;
                                        case 352://デストロイヤー　ON
                                            checkNeedAbility(330);
                                            chenge = ChengeParameter.dethtoroyerOn;
                                            return true;

                                    }

                                    AddSkillTimer(ab.Key, sec, ab.Value.GetMaxReCast(MyJobId), ab.Value.abilityNameJp);
                                    //chenge = ChengeParameter.;
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        //対象に与える[23:39:35.367] 1A:木人 gains the effect of ホルムギャング from Shinon Lu for 6.00 Seconds.
                        text.Length = 0;
                        text.Append(playerName);
                        if (isString(logLine, text, ref hitIndex))
                        {
                            int sec = chkNum(logLine.Substring(hitIndex + playerName.Length + " for".Length, 6));
                            if (sec == 0) { return false; }

                            int kp = hitIndex - " from ".Length;
                            text.Length = 0;
                            text.Append(gainsTheEffectOf);
                            if (isString(logLine, text, ref hitIndex))
                            {
                                text.Length = 0;
                                text.Append(logLine.Substring(hitIndex + gainsTheEffectOfCount, kp - (hitIndex + gainsTheEffectOfCount)));

                                foreach (KeyValuePair<int, Ability> ab in AbilityData.abilities)
                                {
                                    if (ab.Value.abilityNameJp == text.ToString())
                                    {
                                        AddSkillTimer(ab.Key, sec, ab.Value.GetMaxReCast(MyJobId), ab.Value.abilityNameJp);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        */
            /*
                if (endChk(logLine) == false)
                {

                    //Flag
                    if (isFlag(logLine, ref hitIndex))
                    {
                        chenge = ChengeParameter.flag;
                        Flag = ExString.MidB(logLine, hitIndex, 255) + ">>";
                        return true;
                    }
                }
                */
            return false;
        }
        /*
        private void checkNeedAbility(int abilityId)
        {
            foreach (SkillTimer key in abilityTimer)
            {
                if (key.abilityId != 0)
                {
                    if (AbilityData.abilities[key.abilityId].needAbility == abilityId)
                    {
                        key.sec = 0;
                    }
                }
            }

        }


        private int chkNum(string s)
        {
            float v = 0;
            if (float.TryParse(s, out v))
            {
                return (int)v;
            }
            if (s.Length > 1)
            {
                v = chkNum(s.Substring(1));
            }
            return (int)v;
        }


        private bool endChk(string logLine)
        {
            if (logLine.Substring(logLine.Length - 2) == ">>")
            {
                return true;
            }
            return false;
        }

        private bool isFlag(string logLine, ref int hitIndex)
        {
            int Max = logLine.Length;
            if (logLine.IndexOf(" )") >= 0)
            {
                int n = logLine.IndexOf("");
                hitIndex = n;
                if (n > -1)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }


        }

        private void AddSkillTimer(int _abilityId, int _sec, int _reCastSec, string _skillName)
        {
            for (int i = 0; i < abilityTimer.Count; i++)
            {
                if (!abilityTimer[i].isMove())
                {
                    abilityTimer[i].Start(_abilityId, _sec, _skillName);
                    AddReCastTimer(_abilityId, _reCastSec, _skillName);
                    return;
                }
            }
            abilityTimer.Add(new SkillTimer(_abilityId, _sec, _skillName));
            AddReCastTimer(_abilityId, _reCastSec, _skillName);
        }
        private void AddReCastTimer(int _abilityId, int _sec, string _skillName)
        {
            for (int i = 0; i < reCastTimer.Count; i++)
            {
                if (!reCastTimer[i].isMove())
                {
                    reCastTimer[i].Start(_abilityId, _sec, _skillName);
                    return;
                }
            }
            reCastTimer.Add(new SkillTimer(_abilityId, _sec, _skillName));
        }
        private void StopSkillTimer(int _abilityId)
        {
            for (int i = 0; i < abilityTimer.Count; i++)
            {

                if (abilityTimer[i].abilityId == _abilityId)
                {
                    abilityTimer[i].Stop();
                    return;
                }
            }
        }



    }

    public class SkillTimer
    {
        public int sec;
        public string skillName;
        public int abilityId;
        private System.Timers.Timer t;
        public SkillTimer(int _abilityId, int _sec, string _skillName)
        {

            t = new System.Timers.Timer();
            t.Elapsed += new System.Timers.ElapsedEventHandler(MyClock);
            t.Interval = 1000;
            Start(_abilityId, _sec, _skillName);
        }
        public void Start(int _abilityId, int _sec, string _skillName)
        {
            abilityId = _abilityId;
            sec = _sec;
            skillName = _skillName;
            t.Start();
        }
        public void Stop()
        {
            abilityId = 0;
            t.Stop();
            skillName = "";
            sec = 0;
        }
        private void MyClock(object sender, EventArgs e)
        {
            sec--;
            if (sec <= 0)
            {
                sec = 0;
                t.Stop();
            }
        }
        public bool isMove()
        {
            if (sec == 0)
            {
                t.Stop();
                return false;
            }
            return true;
        }
    }
    */

}
}
