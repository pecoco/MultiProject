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
            ReadPartyCount();

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
            if (Locations.ContainsKey(key))
            {
                MemoryPointer = Locations[key];
            }else
            {
                MemoryPointer = IntPtr.Zero;
            }
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
            PartyInfo.ResolvePartyFromBytes(source);
        }

        //private static IntPtr PartyCount { get; set; }
        public void ReadPartyCount()
        {
            IntPtr PartyCountAddress;
            PartyCountAddress = GetLocations("PARTYCOUNT");
            PartyInfo.PartyCount = MemoryLib.GetByte(PartyCountAddress);
        }

        //Search HeapMemory
        private void SearchPartyCountMemory()
        {

        }

        private void SystemSearch()
        {

            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            UnsafeNativeMethods.GetSystemInfo(out sys_info);
            MEMORY_BASIC_INFORMATION64 mem_basic_info = new MEMORY_BASIC_INFORMATION64();
            List<MEMORY_BASIC_INFORMATION64> MemReg = new List<MEMORY_BASIC_INFORMATION64>();
            long bip = ProcessModel.GetProcessBaseAddress(process);

            foreach (Signature sig in Signatures.Resolve(true))
            {
                if (sig.Heap)
                {
                    IntPtr hModuleSnapshot = new IntPtr();
                    
                    hModuleSnapshot = UnsafeNativeHeap.CreateToolhelp32Snapshot(UnsafeNativeHeap.TH32CS_SNAPHEAPLIST | UnsafeNativeHeap.TH32CS_SNAPMODULE32, (uint)process.Id);

                    if((int)hModuleSnapshot == UnsafeNativeHeap.INVALID_HANDLE_VALUE)
                    {
                        System.Windows.Forms.MessageBox.Show("Not Heap Access. "+Marshal.GetLastWin32Error().ToString());
                        return;
                    }
                    UnsafeNativeHeap.HEAPENTRY64 pe64 =new UnsafeNativeHeap.HEAPENTRY64();
                    UnsafeNativeHeap.HEAPLIST64 heaplist = new UnsafeNativeHeap.HEAPLIST64();
                    heaplist.dwSize = (uint)Marshal.SizeOf(heaplist);
                    UnsafeNativeHeap.PROCESS_HEAP_ENTRY64 phe64 = new UnsafeNativeHeap.PROCESS_HEAP_ENTRY64();

                    System.Threading.Thread.Sleep(10);
                    bool r = UnsafeNativeHeap.Heap32ListFirst(hModuleSnapshot, ref heaplist);
                    r = UnsafeNativeHeap.Heap32ListNext(hModuleSnapshot, ref heaplist);
                    bool r2;
                    while (r)
                    {
                        phe64.lpData = IntPtr.Zero;

                        int a = Marshal.SizeOf(phe64);//28 (32)
                        a = Marshal.SizeOf(phe64.lpData);//4
                        a = Marshal.SizeOf(phe64.cbData);//4
                        a = Marshal.SizeOf(phe64.cbOverhead);//2
                        a = Marshal.SizeOf(phe64.iRegionIndex);//2
                        a = Marshal.SizeOf(phe64.wFlags);//2


                        r2 = UnsafeNativeHeap.HeapWalk((IntPtr)heaplist.th32HeapID, ref phe64);
                        System.Windows.Forms.MessageBox.Show("Not Heap Access. " + Marshal.GetLastWin32Error().ToString());
                        int cc = 0;

                        //System.Windows.Forms.MessageBox.Show("Not Heap Access. " + Marshal.GetLastWin32Error().ToString());
                        while (r2 && cc<100)
                        {
                            
                            if (pe64.dwBlockSize == 883)
                            {
                                int t = 1;
                            }
                            r2 = UnsafeNativeHeap.Heap32Next(ref pe64);
                            cc++;
                        }

                        r = UnsafeNativeHeap.Heap32ListNext(hModuleSnapshot, ref heaplist);
                    }


                    System.Windows.Forms.MessageBox.Show(Marshal.GetLastWin32Error().ToString());
                    UnsafeNativeHeap.CloseHandle(hModuleSnapshot);
                    continue;

                    //この方法もダメ
                    sig.BaseAddress = (long)MemoryLib.ReadPointer((IntPtr)sig.PointerAddress);
                    Locations[sig.Key] = sig;
                    continue;

                    ulong proc_min_address = (ulong)sys_info.minimumApplicationAddress;
                    ulong proc_max_address = (ulong)sys_info.maximumApplicationAddress;
                    // saving the values as long ints so I won't have to do a lot of casts later
                    ulong proc_min_address_l = (ulong)proc_min_address;
                    ulong proc_max_address_l = (ulong)proc_max_address;
                    bool exitFg = true;
                    while (proc_min_address_l < proc_max_address_l && exitFg)
                    {

                        VirtualQueryEx(processHandle, (IntPtr)proc_min_address, out mem_basic_info, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION64)));
                        // if this memory chunk is accessible
                        if (mem_basic_info.Protect == MemoryLib.PAGE_READWRITE && mem_basic_info.State == MemoryLib.MEM_COMMIT)
                        {


/*                            

                                if (sig.Key == "PARTYCOUNT")
                                {
                                    //check Mmeory PalyerNmae
                                    ReadPlayerInfo();
                                    byte[] pattan = new byte[PlayerInfo.d.Name.Length];
                                    byte[] pattan0 = new byte[PlayerInfo.d.Name.Length+1];

                                    pattan = System.Text.Encoding.ASCII.GetBytes(PlayerInfo.d.Name);
                                    pattan.CopyTo(pattan0,1);
                                    pattan0[0] = 1;
                                    // read everything in the buffer above
                                    int hp = (int)processHandle;
                                    int readSize = (int)mem_basic_info.RegionSize;// < 64*1024 ? (int)mem_basic_info.RegionSize : 64 * 1024;
                                    byte[] buffer = new byte[readSize];
                                    int bytesRead = 0;
                                    UnsafeNativeMethods.ReadProcessMemory(hp, (Int64)mem_basic_info.BaseAddress, buffer, readSize, ref bytesRead);
                                    int pickup = MemoryLib.FindSuperSig(buffer, pattan0);
                                    if (pickup == -1)
                                    {
                                        proc_min_address_l += mem_basic_info.RegionSize;
                                        proc_min_address = proc_min_address_l;
                                        continue;
                                    }else
                                    {
                                        sig.BaseAddress = (Int64)mem_basic_info.BaseAddress;
                                        sig.Offset = pickup - (0x10 * 24) - 1;
                                        Locations[sig.Key] = sig;
                                        exitFg = false;
                                    }
                                }
*/
                            
                        }
                        proc_min_address_l += mem_basic_info.RegionSize;
                        proc_min_address = proc_min_address_l;
                    }
                }
                else
                {
                    if (sig.Value == "")
                    {
                        sig.BaseAddress = bip;
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
/*
 *                     byte[] pattan;
                    if (sig.Key == "PARTYCOUNT")
                    {
                        //pattan = new byte["/autofacetarget.".Length];
                        //byte[] pattan0 = new byte[] { 1 };
                        //pattan0.CopyTo(pattan, 0);
                        //(System.Text.Encoding.ASCII.GetBytes("Shinon")).CopyTo(pattan,1);
                        //pattan = System.Text.Encoding.ASCII.GetBytes("/autofacetarget.");
                        //000020834000 - 00002083400F: 10h(16)Byte[Windows ANSI]
                        //0000'20834000 : 00 00 00 00 00 00 00 00 32 C3 93 67 E5 CC AA 15
                        pattan = new byte[] { 00, 00, 00, 00, 00, 00, 00, 00, 0x32, 0xC3, 0x93, 0x67 };
                    }
                    else
                    {
                        pattan = MemoryLib.SigToByte(sig.Value, (byte)'*');
                    }


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

                            if (mem_basic_info.RegionSize != 0x348000) {
                                proc_min_address_l += mem_basic_info.RegionSize;
                                proc_min_address = proc_min_address_l;
                                continue;
                            }
                           //00,00,00,00,00,00,00,00,9b,ac,db,e1,d4,14,0b,10
                            // read everything in the buffer above
                            int hp = (int)processHandle;
                            int readSize = (int)mem_basic_info.RegionSize;// < 64*1024 ? (int)mem_basic_info.RegionSize : 64 * 1024;
                            byte[] buffer = new byte[readSize];
                            UnsafeNativeMethods.ReadProcessMemory(hp, (Int64)mem_basic_info.BaseAddress, buffer, readSize, ref bytesRead);

                            int a = buffer[0x22dbd8];

                            /*
                            if (mem_basic_info.BaseAddress < 0x1F0FC0B0 && mem_basic_info.BaseAddress + mem_basic_info .RegionSize >= 0x1F0FC0B0)
                            {
                                MemReg.Add(mem_basic_info);
                                int partyCount = buffer[1335472];
                            }
                            

int match = MemoryLib.FindSuperSig(buffer, pattan);
                            if (match > -1)
                            {
                                /*
                                int partyAddress = match - (16 * 18) - 1;
                                int partyCount = buffer[partyAddress];
                                if (partyCount <= 8)
                                {
                                    sig.BaseAddress = (Int64)mem_basic_info.BaseAddress;
                                    sig.Offset = partyAddress;

                                    Locations[sig.Key] = sig;



                                    break;
                                }
                                
                            }
                            
                         }
                        proc_min_address_l += mem_basic_info.RegionSize;
                        proc_min_address = proc_min_address_l;
                    }
                    */