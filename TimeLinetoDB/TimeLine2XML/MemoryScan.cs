using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;


namespace TimeLinetoDB
{
    public class MemoryScan
    {
        // REQUIRED CONSTS

        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;


        // REQUIRED METHODS

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("ntdll.dll")]
        private static extern int NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, ref PROCESS_BASIC_INFORMATION ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);
        [DllImport("ntdll.dll")]
        private static extern int NtWow64QueryInformationProcess64(IntPtr ProcessHandle, int ProcessInformationClass, ref PROCESS_BASIC_INFORMATION_WOW64 ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);
        [DllImport("ntdll.dll")]
        private static extern int NtWow64ReadVirtualMemory64(IntPtr hProcess, long lpBaseAddress, ref long lpBuffer, long dwSize, IntPtr lpNumberOfBytesRead);

        [DllImport("ntdll.dll")]
        private static extern int NtWow64ReadVirtualMemory64(IntPtr hProcess, long lpBaseAddress, ref UNICODE_STRING_WOW64 lpBuffer, long dwSize, IntPtr lpNumberOfBytesRead);

        [DllImport("ntdll.dll")]
        private static extern int NtWow64ReadVirtualMemory64(IntPtr hProcess, long lpBaseAddress, [MarshalAs(UnmanagedType.LPWStr)] string lpBuffer, long dwSize, IntPtr lpNumberOfBytesRead);



        // for 32-bit process in a 64-bit OS only
        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_BASIC_INFORMATION_WOW64
        {
            public long Reserved1;
            public long PebBaseAddress;
            public long Reserved2_0;
            public long Reserved2_1;
            public long UniqueProcessId;
            public long Reserved3;
        }

        // for 32-bit process in a 64-bit OS only
        [StructLayout(LayoutKind.Sequential)]
        private struct UNICODE_STRING_WOW64
        {
            public short Length;
            public short MaximumLength;
            public long Buffer;
        }



        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_BASIC_INFORMATION
        {
            public IntPtr Reserved1;
            public IntPtr PebBaseAddress;
            public IntPtr Reserved2_0;
            public IntPtr Reserved2_1;
            public IntPtr UniqueProcessId;
            public IntPtr Reserved3;
        }
        // REQUIRED STRUCTS
        public struct MEMORY_BASIC_INFORMATION
        {
            public int BaseAddress;
            public int AllocationBase;
            public int AllocationProtect;
            public int RegionSize;
            public int State;
            public int Protect;
            public int lType;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }
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

        private static PROCESS_BASIC_INFORMATION_WOW64 Pbi64(IntPtr handle)
        {
            PROCESS_BASIC_INFORMATION_WOW64 pbi = new PROCESS_BASIC_INFORMATION_WOW64();
            int hr = NtWow64QueryInformationProcess64(handle, 0, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
            if (hr != 0)
                throw new Win32Exception(hr);
            return pbi;
        }
        private static PROCESS_BASIC_INFORMATION Pbi32(IntPtr handle)
        {
            PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
            int hr = NtQueryInformationProcess(handle, 0, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
            if (hr != 0)
                throw new Win32Exception(hr);
            return pbi;
        }



        // finally...
        public static void Go(Process process, IntPtr processHandle)
        {

            if (processHandle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            int processParametersOffset = Environment.Is64BitOperatingSystem ? 0x20 : 0x10;

            try
            {
                if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess) // are we running in WOW?
                {
                    Pbi64(processHandle);
                }
                else // we are running with the same bitness as the OS, 32 or 64
                {
                    Pbi32(processHandle);
                }
            }
            catch
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }











            // getting minimum & maximum address

            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            IntPtr proc_min_address = sys_info.minimumApplicationAddress;
            IntPtr proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            long proc_min_address_l = (long)proc_min_address;
            long proc_max_address_l = (long)proc_max_address;

            // notepad better be runnin'

            // opening the process with desired access level
            //IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);

            //StreamWriter sw = new StreamWriter("dump.txt");

            // this will store any information we get from VirtualQueryEx()
            MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();
            List<MEMORY_BASIC_INFORMATION> MemReg = new List<MEMORY_BASIC_INFORMATION>();
            int bytesRead = 0;  // number of bytes read with ReadProcessMemory

           /* while (proc_min_address_l < proc_max_address_l)
            {
                // 28 = sizeof(MEMORY_BASIC_INFORMATION)
                VirtualQueryEx(processHandle, proc_min_address, out mem_basic_info, 28);

                // if this memory chunk is accessible
                if (mem_basic_info.Protect == PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
                {
                    MemReg.Add(mem_basic_info);
                    byte[] buffer = new byte[mem_basic_info.RegionSize];
                    // read everything in the buffer above
                    ReadProcessMemory((int)processHandle, mem_basic_info.BaseAddress, buffer, mem_basic_info.RegionSize, ref bytesRead);
                 }
                proc_min_address_l += mem_basic_info.RegionSize;
                proc_min_address = new IntPtr(proc_min_address_l);
            }*/

            foreach (Signature sig in Signatures.Resolve(true)) {

                if (sig.Value == "")
                {
                    Locations[sig.Key] = sig;
                    continue;
                }
                /*
                foreach (MEMORY_BASIC_INFORMATION mem in MemReg)
                {



                    byte[] buffer = new byte[mem.RegionSize];
                    ReadProcessMemory((int)processHandle, mem.BaseAddress, buffer, mem.RegionSize, ref bytesRead);

                    if (buffer[0] > 0)
                    {
                        sig.Value = sig.Value.Replace("*", "?");
                        byte[] data = SigToByte(sig.Value, WildCardChar);

                        for (int i= 0; i < mem.RegionSize - sig.Value.Length; i++)
                        {
                            //if(check(buffer, data, i, sig.Value.Length))
                            //{

                            //}
                        }
                                           

                    }
                }
                */
            }


        }
        private const int WildCardChar = 63;
        static bool check(byte[]s,byte[]e,int si,int size)
        {
            for(int i= 0;i<size; i++)
            {
                if (s[si+i] == e[i] || s[si + i]== WildCardChar) { continue; }else { return false; }
            }

            return true;
        }

        private static byte[] SigToByte(string signature, byte wildcard)
        {
            var pattern = new byte[signature.Length / 2];
            var hexTable = new[]
            {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
            };
            try
            {
                for (int x = 0, i = 0; i < signature.Length; i += 2, x += 1)
                {
                    if (signature[i] == wildcard)
                    {
                        pattern[x] = wildcard;
                    }
                    else
                    {
                        pattern[x] = (byte)(hexTable[Char.ToUpper(signature[i]) - '0'] << 4 | hexTable[Char.ToUpper(signature[i + 1]) - '0']);
                    }
                }
                return pattern;
            }
            catch
            {
                return null;
            }
        }

        private static IntPtr MemoryPointer { get; set; }
        public static IntPtr GetLocations(string key)
        {
            MemoryPointer = Locations["PLAYERINFO"/*key*/];
            return MemoryPointer;
        }


        //MessageBox.Show("end");
    }
}
