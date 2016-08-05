using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//("ffxiv_dx11"
namespace TimeLinetoDB
{

    public class MemoryManeger
    {
        // notepad better be runnin'
        Process process { get; set; }
        public bool IsWin64 { get; set; }
        public MemoryManeger(string processName)
        {
            if(Instance == null)
            {
                //_instance = new MemoryManeger(processName);
                _instance = this;
                process = Process.GetProcessesByName(processName)[0];

                try
                {
                    ProcessHandle = UnsafeNativeMethods.OpenProcess(UnsafeNativeMethods.ProcessAccessFlags.PROCESS_VM_ALL, false, (uint)ProcessID);
                                 //IntPtr processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ, false, process.Id);
                }
                catch (Exception ex)
                {
                    ProcessHandle = process.Handle;
                }
                IsWin64 = true;//WoW32;

                MemoryScan.Go(process, ProcessHandle);
            }
        }


        ~MemoryManeger()
        {
        }
        public IntPtr ProcessHandle { get; set; }

        private static MemoryManeger _instance;
        public static MemoryManeger Instance
        {
            get { return _instance; }// ?? (_instance = new MemoryManeger(null)); }
        }
        private static IntPtr PlayerInfoMap { get; set; }
        public void ReadMemory(string key)
        {
            PlayerInfoMap = MemoryScan.GetLocations(key);
            var source = GetByteArray(PlayerInfoMap, 0x256);
            PlayerInfoData.ResolvePlayerFromBytes(source);
            
        }



        public int ProcessID
        {
            get { return process != null ? process.Id : -1; }
        }

        //func

        public IntPtr ResolvePointerPath(IEnumerable<long> path, IntPtr baseAddress, bool ASMSignature = false)
        {
            var nextAddress = baseAddress;
            foreach (var offset in path)
            {
                try
                {
                    baseAddress = new IntPtr(nextAddress.ToInt64() + offset);
                    if (baseAddress == IntPtr.Zero)
                    {
                        return IntPtr.Zero;
                    }

                    if (ASMSignature)
                    {
                        nextAddress = baseAddress + Instance.GetInt32(new IntPtr(baseAddress.ToInt64())) + 4;
                        ASMSignature = false;
                    }
                    else
                    {
                        nextAddress = Instance.ReadPointer(baseAddress);
                    }
                }
                catch
                {
                    return IntPtr.Zero;
                }
            }
            return baseAddress;
        }

        public IntPtr GetStaticAddress(long offset)
        {
            return new IntPtr(Instance.process.MainModule.BaseAddress.ToInt64() + offset);
        }

        public IntPtr ReadPointer(IntPtr address, long offset = 0)
        {
            if (IsWin64)
            {
                var win64 = new byte[8];
                Peek(new IntPtr(address.ToInt64() + offset), win64);
                return IntPtr.Add(IntPtr.Zero, (int)BitConverter.ToInt64(win64, 0));
            }
            var win32 = new byte[4];
            Peek(new IntPtr(address.ToInt64() + offset), win32);
            return IntPtr.Add(IntPtr.Zero, BitConverter.ToInt32(win32, 0));
        }

        private bool Peek(IntPtr address, byte[] buffer)
        {
            IntPtr lpNumberOfBytesRead;
            return UnsafeNativeMethods.ReadProcessMemory(Instance.ProcessHandle, address, buffer, new IntPtr(buffer.Length), out lpNumberOfBytesRead);
        }
        public byte[] GetByteArray(IntPtr address, int length)
        {
            var data = new byte[length];
            Peek(address, data);
            return data;
        }
        public int GetInt32(IntPtr address, long offset = 0)
        {
            var value = new byte[4];
            Peek(new IntPtr(address.ToInt64() + offset), value);
            return BitConverter.ToInt32(value, 0);
        }
    }
}
