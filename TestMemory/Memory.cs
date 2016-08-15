using Memory.DataStructure;
using Memory.Helper;
using MemoryUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMemory.DataStructure;
using TestMemory.Helper;

namespace Memory
{
    
    public class Memory
    {
        static Process process;
        private static Dictionary<string, Signature> _locations;
        public static Dictionary<string, Signature> Locations
        {
            get { return _locations ?? (_locations = new Dictionary<string, Signature>()); }
            private set
            {
                if (_locations == null)
                {
                    _locations = new Dictionary<string, Signature>();
                }
                _locations = value;
            }
        }

        IntPtr handle = IntPtr.Zero;
        public Memory()
        {
            process = ProcessModel.GetProcessId("ffxiv_dx11");
            //process = ProcessModel.GetProcessId("notepad");
            long bip = ProcessModel.GetProcessBaseAddress(process);

            handle = ProcessModel.OpenProcessHandle(process);
            MemoryLib.SetHandle(handle);

           


            foreach (Signature sig in Signatures.Resolve(true))
            {
                sig.BaseAddress = bip;
                if (sig.Value == "")
                {
                    Locations[sig.Key] = sig;
                    continue;
                }else
                {
                    Signature retrnSig =MemoryLib.FindExtendedSignatures(sig);                    //toDo Active Search...
                    sig.BaseAddress = retrnSig.BaseAddress;
                    Locations[sig.Key] = sig;
                }
            }


            ReadPlayerInfo();
            ReadParty();

        }
        ~Memory()
        {
            if (handle != IntPtr.Zero)
            {
                ProcessModel.CloseProcessHandle(handle);
            }
        }


        private static IntPtr MemoryPointer { get; set; }

        
        private static IntPtr GetLocations(string key)
        {
            MemoryPointer = Locations[key];
            return MemoryPointer;
        }
        //この関数でデーターを読み込み
        private static IntPtr PlayerInfoMap { get; set; }

        public void ReadPlayerInfo()
        {
            PlayerInfoMap = GetLocations("PLAYERINFO");
            var source = MemoryLib.GetByteArray(PlayerInfoMap, 0x256);
            PlayerInfo.ResolvePlayerFromBytes(source);

        }
        private static IntPtr PartyMap { get; set; }
        public void ReadParty()
        {
            PartyMap = GetLocations("PARTY");
            var source = MemoryLib.GetByteArray(PartyMap, 0x220);
            PartyData.ResolvePartyFromBytes(source);

        }

 

        
    }
}
