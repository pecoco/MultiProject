using Memory.Helper;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace MemoryUtil
{
    static class ProcessModel
    {


        public enum PROCESSINFOCLASS : int
        {
            ProcessBasicInformation = 0, // 0, q: PROCESS_BASIC_INFORMATION, PROCESS_EXTENDED_BASIC_INFORMATION
            ProcessWow64Information = 26, // q: ULONG_PTR
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_BASIC_INFORMATION
        {
            public IntPtr Reserved1;
            public IntPtr PebBaseAddress;
            public IntPtr Reserved2_0;
            public IntPtr Reserved2_1;
            public IntPtr UniqueProcessId;
            public IntPtr Reserved3;
        }

        // for 32-bit process in a 64-bit OS only
        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_BASIC_INFORMATION_WOW64
        {
            public long Reserved1;
            public long PebBaseAddress;
            public long Reserved2_0;
            public long Reserved2_1;
            public long UniqueProcessId;
            public long Reserved3;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct PEB_INTERNAL
        {
            public byte InheritedAddressSpace;
            public byte BeingDebugged;
            public byte Spare;
            public IntPtr Mutant;
            public IntPtr ImageBaseAddress;
            public static readonly int len;
            static PEB_INTERNAL()
            {
                len = Marshal.SizeOf(typeof(PEB_INTERNAL));
            }
        };

        [DllImport("ntdll.dll")]//
        public static extern int NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, ref PROCESS_BASIC_INFORMATION ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);
        [DllImport("ntdll.dll")]//
        public static extern int NtWow64QueryInformationProcess64(IntPtr ProcessHandle, int ProcessInformationClass, ref PROCESS_BASIC_INFORMATION_WOW64 ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);


        //public static readonly bool Is64BitProcess = IntPtr.Size > 4; このアプリを調べても無意味

        private const int PROCESS_QUERY_INFORMATION = 0x400;
        private const int PROCESS_VM_READ = 0x10;
        private static System.Diagnostics.Process[] ps;
        public static Process GetProcessId(string processName)
        {
            //System.Diagnostics.Process[] 
            ps =System.Diagnostics.Process.GetProcessesByName(processName);
            return ps[0];
        }
 
        public static IntPtr OpenProcessHandle(Process process)
        {
            return UnsafeNativeMethods.OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, process.Id);
        }
        public static void CloseProcessHandle(IntPtr handle)
        {
            UnsafeNativeMethods.CloseHandle(handle);
        }
        public static Process process { get; set; }
        private static IntPtr _handle;
        public static IntPtr handle { get { return _handle; } }

        public static Int64 GetProcessBaseAddress(Process _process)
        {
            process = _process;
            int processId = process.Id;
            _handle = OpenProcessHandle(process);
            if (handle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            bool Is64BitOperatingSystem = Is64BitChecker.isWindows64bit();
           // bool IsWow64Process = Is64BitChecker.InternalCheckIsWow64(handle);
            bool IsTargetWow64Process = Is64BitChecker.GetProcessIsWow64(handle);
            bool IsTarget64BitProcess = Is64BitOperatingSystem && !IsTargetWow64Process;

            long processParametersOffset = IsTarget64BitProcess ? 0x10 : 0x8;//オリジナルはox20 ox10
            Int64 pebAddress = 0;
            try
            {
                int hr; 
                if (IsTargetWow64Process) // OS : 64Bit, Cur : 32 or 64, Tar: 32bit
                {
                    IntPtr peb32 = new IntPtr();
                    hr = UnsafeNativeMethods.NtQueryInformationProcess(handle, (int)PROCESSINFOCLASS.ProcessWow64Information, ref peb32, IntPtr.Size, IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = peb32.ToInt64();
                    IntPtr pp = new IntPtr();
                    if (!UnsafeNativeMethods.ReadProcessMemory(handle, new IntPtr(pebAddress + processParametersOffset), ref pp, new IntPtr(Marshal.SizeOf(pp)), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    return pp.ToInt64();
                }
                else if (IsTargetWow64Process)//Os : 64Bit, Cur 32, Tar 64
                {
                    PROCESS_BASIC_INFORMATION_WOW64 pbi = new PROCESS_BASIC_INFORMATION_WOW64();
                    hr = NtWow64QueryInformationProcess64(handle, (int)PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = pbi.PebBaseAddress;
                    long pp = 0;
                    hr = UnsafeNativeMethods.NtWow64ReadVirtualMemory64(handle, pebAddress + processParametersOffset, ref pp, Marshal.SizeOf(pp), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    return pp;
                }
                else// Os,Cur,Tar : 64 or 32
                {
                    PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
                    hr = NtQueryInformationProcess(handle, (int)PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = pbi.PebBaseAddress.ToInt64();
                    IntPtr pp = new IntPtr();
                    if (!UnsafeNativeMethods.ReadProcessMemory(handle, new IntPtr(pebAddress + processParametersOffset), ref pp, new IntPtr(Marshal.SizeOf(pp)), IntPtr.Zero))
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    return pp.ToInt64();
                }
            }
            catch(Exception e)
            {

            }
            finally
            {
                UnsafeNativeMethods.CloseHandle(handle);
            }
            //Return Base Image Address.
            return pebAddress;
        }
    }

    public class Is64BitChecker
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );
        public static bool isWindows64bit()
        {
            return Environment.Is64BitOperatingSystem;
        }
        public static bool GetProcessIsWow64(IntPtr hProcess)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                bool retVal;
                if (!IsWow64Process(hProcess, out retVal))
                {
                    return false;
                }
                return retVal;
            }
            else
            {
                return false;
            }
        }
    }
}
