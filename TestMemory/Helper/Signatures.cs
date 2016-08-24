using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Helper
{
    public class Signatures
    {
        public static IEnumerable<Signature> Resolve(bool IsWin64)
        {
            var signatures = new List<Signature>();

            if (IsWin64)
            {
                signatures.Add(new Signature
                {
                    Key = "PLAYERINFO",
                    Offset = 24791840
                });
                signatures.Add(new Signature
                {
                    Key = "PARTY",
                    Offset = 22787280
                });
                signatures.Add(new Signature
                {
                    Key = "PARTYCOUNT",
                    //Value = "5F50617274794C69737400",
                    Offset = //0x10A5E6C
                             0x10A8EBC,
                    Heap = true
                });
            }
            else
            {
                signatures.Add(new Signature
                {
                    Key = "PLAYERINFO",
                    Offset = 24791840 
                });
                signatures.Add(new Signature
                {
                    Key = "PARTY",
                    Offset = 22787280
                });
                signatures.Add(new Signature
                {
                    Key = "PARTYCOUNT",
                    //Value = "5F50617274794C69737400",
                    Offset = //0x10A5E6C
                             0x10A8EBC
                });
            }
            return signatures;
        }
    }
}
