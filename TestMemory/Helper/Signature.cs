using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Memory.Helper
{
    public class Signature
    {
  
        public Signature()
        {
            Key = "";
            Value = "";
            Offset = 0;
            Heap = false;
        }
        public string Key { get; set; }
        public string Value { get; set; }
        public long Offset { get; set; }
        public bool Heap { get; set; }
        private Int64 baseAddress;
        public Int64 BaseAddress
        {
            set { baseAddress = value; }
            get { return baseAddress; }
        }


       

        public IntPtr GetAddress()
        {
            return (IntPtr)( baseAddress + Offset );
        }

        // convenience conversion for less code breakage. 
        // FIXME: convert all calling functions to handle IntPtr properly someday, and stop using long for addresses
        public static implicit operator IntPtr(Signature value)
        {
            return value.GetAddress();
        }

    }

}

