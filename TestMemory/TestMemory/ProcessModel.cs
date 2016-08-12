using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryUtil
{
    static class ProcessModel
    {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
        [In] IntPtr hProcess,
        [Out] out bool wow64Process
        );

        [DllImport("kernel32.dll", SetLastError = true)]//
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("ntdll.dll")]//
        private static extern int NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, ref PROCESS_BASIC_INFORMATION ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);
        [DllImport("ntdll.dll")]//
        private static extern int NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, ref IntPtr ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);

        // for 32-bit process in a 64-bit OS only
        [DllImport("ntdll.dll")]//
        private static extern int NtWow64QueryInformationProcess64(IntPtr ProcessHandle, int ProcessInformationClass, ref PROCESS_BASIC_INFORMATION_WOW64 ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);

        [DllImport("kernel32.dll")]//
        private static extern bool CloseHandle(IntPtr hObject);

        public enum PROCESSINFOCLASS : int
        {
            ProcessBasicInformation = 0, // 0, q: PROCESS_BASIC_INFORMATION, PROCESS_EXTENDED_BASIC_INFORMATION
            ProcessWow64Information = 26, // q: ULONG_PTR
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


        public static readonly bool Is64BitProcess = IntPtr.Size > 4;
        public static readonly bool Is64BitOperatingSystem = Is64BitProcess || Is64BitChecker.InternalCheckIsWow64();

        private const int PROCESS_QUERY_INFORMATION = 0x400;
        private const int PROCESS_VM_READ = 0x10;

        public static int GetProcessId(string processName)
        {
            System.Diagnostics.Process[] ps =System.Diagnostics.Process.GetProcessesByName(processName);
            return ps[0].Id;
        }
        public struct ProcessBaseAddress{
            public IntPtr BaseAddress32;
            public long BaseAddress64;      
       }


        public static long GetProcessBaseAddress(int processId)
        {
            IntPtr handle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);
            if (handle == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            bool IsWow64Process = Is64BitChecker.InternalCheckIsWow64();
            bool IsTargetWow64Process = Is64BitChecker.GetProcessIsWow64(handle);
            bool IsTarget64BitProcess = Is64BitOperatingSystem && !IsTargetWow64Process;


            long pebAddress = 0;
            try
            {
               
                if (IsTargetWow64Process) // OS : 64Bit, Cur : 32 or 64, Tar: 32bit
                {
                    IntPtr peb32 = new IntPtr();
                    int hr = NtQueryInformationProcess(handle, (int)PROCESSINFOCLASS.ProcessWow64Information, ref peb32, IntPtr.Size, IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = peb32.ToInt64();
                   
                }
                else if (IsWow64Process)//Os : 64Bit, Cur 32, Tar 64
                {
                    PROCESS_BASIC_INFORMATION_WOW64 pbi = new PROCESS_BASIC_INFORMATION_WOW64();
                    int hr = NtWow64QueryInformationProcess64(handle, (int)PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = pbi.PebBaseAddress;
                }
                else// Os,Cur,Tar : 64 or 32
                {
                    PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
                    int hr = NtQueryInformationProcess(handle, (int)PROCESSINFOCLASS.ProcessBasicInformation, ref pbi, Marshal.SizeOf(pbi), IntPtr.Zero);
                    if (hr != 0) throw new Win32Exception(hr);
                    pebAddress = pbi.PebBaseAddress.ToInt64();
                }
            }
            catch
            {

            }
            finally
            {
                CloseHandle(handle);
            }
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

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }
    }

}
