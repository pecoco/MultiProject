using Memory.DataStructure;
using Memory.Helper;
using MemoryUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestMemory.DataStructure;
using TestMemory.Helper;


namespace Memory
{
    
    
        
    public class Memory
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength);

        Process process;
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
            process = ProcessModel.GetProcessId("ffxiv_dx11");
            //process = ProcessModel.GetProcessId("notepad");
           

            processHandle = ProcessModel.OpenProcessHandle(process);
            MemoryLib.SetHandle(processHandle);
            SystemSearch();



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

            int PROCESS_QUERY_INFORMATION = 0x0400;
            int PROCESS_WM_READ = 0x0010;
            MEMORY_BASIC_INFORMATION64 mem_basic_info = new MEMORY_BASIC_INFORMATION64();
            List<MEMORY_BASIC_INFORMATION64> MemReg = new List<MEMORY_BASIC_INFORMATION64>();
            int bytesRead = 0;  // number of bytes read with ReadProcessMemory
                                //process = ProcessModel.GetProcessId("ffxiv_dx11");
                                //IntPtr processHandle2 = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id); ;
            long bip = ProcessModel.GetProcessBaseAddress(process);

            foreach (Signature sig in Signatures.Resolve(true))
            {
                if (sig.Heap)
                {
                    ulong proc_min_address = (ulong)sys_info.minimumApplicationAddress;
                    ulong proc_max_address = (ulong)sys_info.maximumApplicationAddress;
                    bip = 0;
                    // saving the values as long ints so I won't have to do a lot of casts later
                    ulong proc_min_address_l = (ulong)proc_min_address;
                    ulong proc_max_address_l = (ulong)proc_max_address;

                    while (proc_min_address_l < proc_max_address_l)
                    {

                        // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                        VirtualQueryEx(processHandle, (IntPtr)proc_min_address, out mem_basic_info, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION64)));
                        // if this memory chunk is accessible
                        if (mem_basic_info.Protect == MemoryLib.PAGE_READWRITE && mem_basic_info.State == MemoryLib.MEM_COMMIT)
                        {
                            MemReg.Add(mem_basic_info);
                            byte[] buffer = new byte[mem_basic_info.RegionSize];
                            // read everything in the buffer above
                            //        public static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, ref byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

                            UnsafeNativeMethods.ReadProcessMemory((IntPtr) processHandle, (IntPtr)mem_basic_info.BaseAddress, ref buffer, (long)mem_basic_info.RegionSize, (IntPtr) bytesRead);
                        }
                        proc_min_address_l += mem_basic_info.RegionSize;
                        proc_min_address = proc_min_address_l;
                    }
                }
                else
                {

                    sig.BaseAddress = bip;
                    if (sig.Value == "")
                    {
                        Locations[sig.Key] = sig;
                        continue;
                    }
                    else
                    {
                        Signature retrnSig = MemoryLib.FindExtendedSignatures(sig);                    //toDo Active Search...
                        sig.BaseAddress = retrnSig.BaseAddress;
                        Locations[sig.Key] = sig;
                    }
                }
            }

        }


    }
}
