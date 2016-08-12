using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Helper
{
    class UnsafeNativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, [In] [Out] IntPtr lpBuffer, IntPtr regionSize, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr processHandle, IntPtr lpBaseAddress, [In] [Out] Byte[] lpBuffer, IntPtr regionSize, out IntPtr lpNumberOfBytesRead);

    }
}
