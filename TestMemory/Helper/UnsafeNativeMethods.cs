using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TestMemory.Helper;
using static MemoryUtil.ProcessModel;

namespace Memory.Helper
{
    public static class UnsafeNativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, [In] [Out] IntPtr lpBuffer, IntPtr regionSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, [In] [Out] Byte[] lpBuffer, IntPtr regionSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process(
        [In] IntPtr hProcess,
        [Out] out bool wow64Process
        );
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref IntPtr lpBuffer, IntPtr dwSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);



        [DllImport("ntdll.dll")]//
        public static extern int NtQueryInformationProcess(IntPtr ProcessHandle, int ProcessInformationClass, ref IntPtr ProcessInformation, int ProcessInformationLength, IntPtr ReturnLength);
        // for 32-bit process in a 64-bit OS only
        [DllImport("ntdll.dll")]
        public static extern int NtWow64ReadVirtualMemory64(IntPtr hProcess, long lpBaseAddress, ref long lpBuffer, long dwSize, IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]//
        public static extern bool CloseHandle(IntPtr hObject);

    }
    //Heap

    public static class UnsafeNativeHeap {
        public const int INVALID_HANDLE_VALUE = -1;

        public const uint TH32CS_SNAPHEAPLIST = 0x00000001;
        public const uint TH32CS_SNAPMODULE = 0x00000008;

        public const uint TH32CS_SNAPMODULE32 = 0x00000010;

        public struct HEAPLIST32
        {
            public uint dwSize;
            public uint th32ProcessID;
            public uint th32HeapID;
            public uint dwFlags;
        }
        public struct HEAPLIST64
        {
            public UInt64 dwSize;
            public UInt32 th32ProcessID;
            public UInt64 th32HeapID;
            public UInt32 dwFlags;
        }

        public struct HEAPENTRY32
        {
            public uint dwSize;
            public IntPtr hHandle;
            public uint dwAddress;
            public uint dwBlockSize;
            public uint dwFlags;
            public uint dwLockCount;
            public uint dwResvd;
            public uint th32ProcessID;
            public uint th32HeapID;
        }

        public struct HEAPENTRY64
        {
            public UInt64 dwSize;
            public UInt32 hHandle;
            public UInt64 dwAddress;
            public UInt64 dwBlockSize;
            public UInt32 dwFlags;
            public UInt32 dwLockCount;
            public UInt32 dwResvd;
            public UInt32 th32ProcessID;
            public UInt64 th32HeapID;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct STRUCT_BLOCK
        {
            public IntPtr hMem;
            public uint dwReserved1_1;
            public uint dwReserved1_2;
            public uint dwReserved1_3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct STRUCT_REGION
        {
            public uint dwCommittedSize;
            public uint dwUnCommittedSize;
            public IntPtr lpFirstBlock;
            public IntPtr lpLastBlock;
        }
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct UNION_BLOCK
        {
            [FieldOffset(0)]
            public STRUCT_BLOCK Block;

            [FieldOffset(0)]
            public STRUCT_REGION Region;
        }

        [StructLayout(LayoutKind.Explicit, Size = 28) ]
        public struct PROCESS_HEAP_ENTRY64
        {
            [FieldOffset(0)]
            public IntPtr lpData;//4
            [FieldOffset(4)]
            public UInt32 cbData;//4
            [FieldOffset(8)]
            public UInt16 cbOverhead;//2
            [FieldOffset(10)]
            public UInt16 iRegionIndex;//2
            [FieldOffset(12)]
            public UInt16 wFlags;//2
            [FieldOffset(14)]
            public UNION_BLOCK UnionBlock;
        }
/*
[DllImport("toolhelp.dll", SetLastError = true)]
internal static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
[DllImport("toolhelp.dll", SetLastError = true)]
internal static extern int CloseToolhelp32Snapshot(IntPtr handle);


[DllImport("toolhelp.dll")]
internal static extern bool Heap32ListFirst(IntPtr hSnapshot, ref HEAPLIST32 lphl);
[DllImport("toolhelp.dll")]
internal static extern bool Heap32ListNext(IntPtr hSnapshot, ref HEAPLIST32 lphl);
[DllImport("toolhelp.dll")]
internal static extern bool Heap32First(IntPtr hSnapshot, ref HEAPENTRY32 lphe, uint th32ProcessID, uint th32HeapID);
[DllImport("toolhelp.dll")]
internal static extern bool Heap32Next(IntPtr hSnapshot, ref HEAPENTRY32 lphe);
*/

[DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int CloseHandle(IntPtr handle);


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Heap32ListFirst(IntPtr hSnapshot, ref HEAPLIST32 lphl);
        [DllImport("kernel32.dll")]
        internal static extern bool Heap32ListNext(IntPtr hSnapshot, ref HEAPLIST32 lphl);


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Heap32ListFirst(IntPtr hSnapshot, ref HEAPLIST64 lphl);
        [DllImport("kernel32.dll")]
        internal static extern bool Heap32ListNext(IntPtr hSnapshot, ref HEAPLIST64 lphl);

            
        [DllImport("kernel32.dll")]
        internal static extern bool Heap32First(ref HEAPENTRY32 lphe, uint th32ProcessID, uint th32HeapID);
        [DllImport("kernel32.dll")]
        internal static extern bool Heap32Next(ref HEAPENTRY32 lphe);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Heap32First( ref HEAPENTRY64 lphe, uint th32ProcessID, UInt64 th32HeapID);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool Heap32Next( ref HEAPENTRY64 lphe);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool HeapWalk(IntPtr hHeap, ref PROCESS_HEAP_ENTRY64 lpEntry);


    }
}
