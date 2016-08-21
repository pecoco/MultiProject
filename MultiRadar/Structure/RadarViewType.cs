using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiRadar.Structure
{
    public class RadarViewType
    {
        public int defaultZoomoutValue = -200;
        public ViewOption mob = new ViewOption();
        public ViewOption hum = new ViewOption();
        public ViewOption id = new ViewOption();

    }

    public class ViewOption
    {
        public bool name;
        public bool positon;
        public bool hp;
        public bool job;
        public bool link;
    }
}
/*
 * <?xml version="1.0" encoding="utf-8"?>
<ViewOptions xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Mob>
   <Name>true</Name>
   <Positon>true</Positon>
   <Hp>true</Hp>
   <Job>false</Job>
  </Mob>
  <AntiHum>
   <Name>false</Name>
   <Positon>false</Positon>
   <Hp>false</Hp>
   <Job>true</Job>
  </AntiHum>
  <id> 
   <Name>true</Name>
   <Positon>false</Positon>
   <Hp>false</Hp>
   <Job>true</Job>
  </id> 
</ViewOptions>
*/