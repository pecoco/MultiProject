using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory.Helper
{
    class Signatures
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
            }
            return signatures;
        }
    }
}
