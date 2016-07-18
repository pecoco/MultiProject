

using System.Collections.Generic;

namespace TimeLineXML
{
    class TimeLineXML
    {

        List<Alertall> alertalls;
        List<TimeLine> timeLines;
    }



    public class Alertall{
        bool commentOut;
        int id;
        string title;
        int before;
        string acter;
        string messege;
    }

    public class TimeLine
    {
        bool commentOut;
        int id;
        int sec;
        string title;
        string syncString;
        int duration;
        int window; 
    }

}
