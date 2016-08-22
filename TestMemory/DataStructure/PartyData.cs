using Commonm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMemory.Helper;

namespace TestMemory.DataStructure
{
    public class PartyData
    {
        public static PartyDataStructure ResolvePartyFromBytes(byte[] source)
        {
            var entry = new PartyDataStructure();

            try
            {                
                entry.X = BitConverter.ToSingle(source, 0x0);
                entry.Z = BitConverter.ToSingle(source, 0x4);
                entry.Y = BitConverter.ToSingle(source, 0x8);
                //entry.Coordinate = new Coordinate(entry.X, entry.Z, entry.Z);
                entry.ID = BitConverter.ToUInt32(source, 0x10);
                entry.Name = MemoryLib.GetStringFromBytes(source, 0x20);
                entry.Job = (Enumes.Job)source[0x61];
                entry.Level = source[0x63];
                entry.HPCurrent = BitConverter.ToInt32(source, 0x68);
                entry.HPMax = BitConverter.ToInt32(source, 0x6C);
                entry.MPCurrent = BitConverter.ToInt16(source, 0x70);
                entry.MPMax = BitConverter.ToInt16(source, 0x72);
            }
            catch(Exception e)
            {

            }
            return entry;

        }
    }
}
