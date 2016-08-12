using Memory.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMemory.Helper
{
    public static class MemoryLib
    {
        static IntPtr handle = IntPtr.Zero;
        public static void SetHandle(IntPtr _handle)
        {
            handle = _handle;
        }
        //Memory Lib
        private static bool Peek(IntPtr address, byte[] buffer)
        {
            if (handle == IntPtr.Zero) { return false; }
            IntPtr lpNumberOfBytesRead;
            return UnsafeNativeMethods.ReadProcessMemory(handle, address, buffer, new IntPtr(buffer.Length), out lpNumberOfBytesRead);
        }
        public static byte[] GetByteArray(IntPtr address, int length)
        {
            var data = new byte[length];
            Peek(address, data);
            return data;
        }

        public static string GetStringFromBytes(byte[] source, int offset = 0, int size = 256)
        {
            var bytes = new byte[size];
            Array.Copy(source, offset, bytes, 0, size);
            var realSize = 0;
            for (var i = 0; i < size; i++)
            {
                if (bytes[i] != 0)
                {
                    continue;
                }
                realSize = i;
                break;
            }
            Array.Resize(ref bytes, realSize);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
