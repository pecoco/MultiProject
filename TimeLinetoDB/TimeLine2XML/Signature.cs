using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TimeLinetoDB
{
    public class Signature
    {
        private int _Offset;
        private bool offsetSet;

        public Signature()
        {
            Key = "";
            Value = "";
            RegularExpress = null;
            SigScanAddress = IntPtr.Zero;
            PointerPath = null;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public Regex RegularExpress { get; set; }
        public IntPtr SigScanAddress { get; set; }
        public bool ASMSignature { get; set; }

        public int Offset
        {
            get
            {
                if (!offsetSet)
                {
                    _Offset = Value.Length / 2;
                }
                return _Offset;
            }
            set
            {
                offsetSet = true;
                _Offset = value;
            }
        }
        
        public List<long> PointerPath { get; set; }
        
        public IntPtr GetAddress()
        {
            
            var baseAddress = IntPtr.Zero;
            var FirstIsOffsetAddress = false;
            
            if (SigScanAddress != IntPtr.Zero)
            {
                baseAddress = SigScanAddress; // Scanner should have already applied the base offset
                if (MemoryManeger.Instance.IsWin64 && ASMSignature)
                {
                    FirstIsOffsetAddress = true;
                }
            }
            else
            {
                if (PointerPath == null || PointerPath.Count == 0)
                {
                    return IntPtr.Zero;
                }
                baseAddress = MemoryManeger.Instance.GetStaticAddress(0);
            }
            if (PointerPath == null || PointerPath.Count == 0)
            {
                return baseAddress;
            }
            return MemoryManeger.Instance.ResolvePointerPath(PointerPath, baseAddress, FirstIsOffsetAddress);
            
           
        }

        // convenience conversion for less code breakage. 
        // FIXME: convert all calling functions to handle IntPtr properly someday, and stop using long for addresses
        public static implicit operator IntPtr(Signature value)
        {
            return value.GetAddress();
        }

    }


}

