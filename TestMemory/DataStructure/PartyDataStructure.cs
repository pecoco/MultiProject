using Commonm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMemory.DataStructure
{
    public class PartyDataStructure
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public double X { get; set; }
        public double Z { get; set; }
        public double Y { get; set; }
        public Enumes.Job Job { get; set; }
        public byte Level { get; set; }
        public int HPCurrent { get; set; }
        public int HPMax { get; set; }
        public int MPCurrent { get; set; }
        public int MPMax { get; set; }
        //List<StatusEntry> StatusEntries { get; set; }
    }
}
