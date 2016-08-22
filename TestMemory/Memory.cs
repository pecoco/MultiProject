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

        IntPtr processHandle = IntPtr.Zero;
        public Memory()
        {
            //process = ProcessModel.GetProcessId("ffxiv_dx11");
            process = ProcessModel.GetProcessId("notepad");
            long bip = ProcessModel.GetProcessBaseAddress(process);

            processHandle = ProcessModel.OpenProcessHandle(process);
            MemoryLib.SetHandle(processHandle);

            SystemSearch();

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
            string playerName = PlayerInfo.d.Name;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(playerName);

             

            //ReadParty();
            //ReadPartyCount();

            //SearchMem();
        }
        ~Memory()
        {
            if (processHandle != IntPtr.Zero)
            {
                ProcessModel.CloseProcessHandle(processHandle);
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

        private static IntPtr PartyCount { get; set; }
        public void ReadPartyCount()
        {
            PartyCount = GetLocations("PARTYCOUNT");
            var source = MemoryLib.GetByteArray(PartyCount, 0x220);
            PartyData.ResolvePartyFromBytes(source);

        }

        //Search HeapMemory
        private void SearchPartyCountMemory()
        {

        }

        private void SystemSearch()
        {

            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            UnsafeNativeMethods.GetSystemInfo(out sys_info);

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;


            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();
            List<MEMORY_BASIC_INFORMATION> MemReg = new List<MEMORY_BASIC_INFORMATION>();
            int bytesRead = 0;  // number of bytes read with ReadProcessMemory

            IntPtr processHandle2 = UnsafeNativeMethods.OpenProcess(MemoryLib.PROCESS_QUERY_INFORMATION | MemoryLib.PROCESS_WM_READ, false, process.Id);

            while (proc_min_address_l < proc_max_address_l)
            {

                // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                UnsafeNativeMethods.VirtualQueryEx(processHandle2, proc_min_address, out mem_basic_info, 28);
                // if this memory chunk is accessible
                if (mem_basic_info.Protect == MemoryLib.PAGE_READWRITE && mem_basic_info.State == MemoryLib.MEM_COMMIT)
                {
                    MemReg.Add(mem_basic_info);
                    byte[] buffer = new byte[mem_basic_info.RegionSize];
                    // read everything in the buffer above
                    UnsafeNativeMethods.ReadProcessMemory((int)processHandle, mem_basic_info.BaseAddress, buffer, mem_basic_info.RegionSize, ref bytesRead);
                }
                proc_min_address_l += mem_basic_info.RegionSize;
                proc_min_address = new IntPtr(proc_min_address_l);
            }



        }


    }
}
