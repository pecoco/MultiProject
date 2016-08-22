using Memory.Helper;
using MemoryUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestMemory.Helper
{
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



    public static class MemoryLib
    {
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
        public const int MEM_COMMIT = 0x00001000;
        public const int PAGE_READWRITE = 0x04;
        public const int PROCESS_WM_READ = 0x0010;



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

        public static IntPtr SearchMemory(string signature)
        {

            byte WildCardChar = (byte)'*';
            byte[] sigString = SigToByte(signature, WildCardChar);




            return IntPtr.Zero;
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

        private static byte WildCardChar = (byte)'*';
        public static Signature FindExtendedSignatures( Signature signature)
        {
            const int bufferSize = 0x1000;
            var moduleMemorySize = ProcessModel.process.MainModule.ModuleMemorySize;
            var baseAddress = ProcessModel.process.MainModule.BaseAddress;
            var searchEnd = IntPtr.Add(baseAddress, moduleMemorySize);
            var searchStart = baseAddress;
            var lpBuffer = new byte[bufferSize];
            //var notFound = new List<Signature>(signatures);
            //var temp = new List<Signature>();
            var regionCount = 0;
            Signature returnSig = new Signature();
            while (searchStart.ToInt64() < searchEnd.ToInt64())
            {
                
                try
                {
                    IntPtr lpNumberOfBytesRead;
                    var regionSize = new IntPtr(bufferSize);
                    if (IntPtr.Add(searchStart, bufferSize)
                              .ToInt64() > searchEnd.ToInt64())
                    {
                        regionSize = (IntPtr)(searchEnd.ToInt64() - searchStart.ToInt64());
                    }
                    if (UnsafeNativeMethods.ReadProcessMemory(ProcessModel.handle, searchStart, lpBuffer, regionSize, out lpNumberOfBytesRead))
                    {

                       
                        byte[] sigString = SigToByte(signature.Value, WildCardChar);


                        var idx = FindSuperSig(lpBuffer, SigToByte(signature.Value, WildCardChar));
                        if (idx < 0)
                        {
                           
                        }
                        else
                        {
                            returnSig.BaseAddress = searchStart.ToInt64() + idx;
                            return returnSig;
                        }

                        /*
                        foreach (var signature in notFound)
                        {
                            var idx = FindSuperSig(lpBuffer, SigToByte(signature.Value, WildCardChar));
                            if (idx < 0)
                            {
                                temp.Add(signature);
                                continue;
                            }
                            var baseResult = new IntPtr((long)(baseAddress + (regionCount * bufferSize)));
                            var searchResult = IntPtr.Add(baseResult, idx + signature.Offset);
                            signature.SigScanAddress = new IntPtr(searchResult.ToInt64());
                            Locations.Add(signature.Key, signature);
                        }
                        
                        notFound = new List<Signature>(temp);
                        temp.Clear();
                        */
                    }
                    regionCount++;
                    searchStart = IntPtr.Add(searchStart, bufferSize);
                }
                catch (Exception ex)
                {
                }
            }
            return returnSig;
        }

        private static int FindSuperSig(byte[] buffer, byte[] pattern)
        {
            var result = -1;
            if (buffer.Length <= 0 || pattern.Length <= 0 || buffer.Length < pattern.Length)
            {
                return result;
            }
            for (var i = 0; i <= buffer.Length - pattern.Length; i++)
            {
                if (buffer[i] != pattern[0])
                {
                    continue;
                }
                if (buffer.Length > 1)
                {
                    var matched = true;
                    for (var y = 1; y <= pattern.Length - 1; y++)
                    {
                        if (buffer[i + y] == pattern[y] || pattern[y] == WildCardChar)
                        {
                            continue;
                        }
                        matched = false;
                        break;
                    }
                    if (!matched)
                    {
                        continue;
                    }
                    result = i;
                    break;
                }
                result = i;
                break;
            }
            return result;
        }
    }
}
