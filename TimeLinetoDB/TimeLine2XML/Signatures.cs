using System.Collections.Generic;


namespace TimeLinetoDB
{
    public static class Signatures
    {
        public static IEnumerable<Signature> Resolve(bool IsWin64, string gameLanguage = "English")
        {
            var signatures = new List<Signature>();

            switch (gameLanguage)
            {
                // Copied from 6eae40ef (Patch 2.5)
                case "Korean":
                    break;
                // copied from a4cab8e327 (patch 3.07)
                case "Chinese":
                    break;
                default:
                    if (IsWin64)
                    {
                        // can still use old style entry of signatures
                        signatures.Add(new Signature
                        {
                            Key = "GAMEMAIN",
                            Value = "47616D654D61696E000000",
                            Offset = 1344 // is this even used anymore?
                        });
                        // or a combination of signature and offset from that signature
                        signatures.Add(new Signature
                        {
                            Key = "TARGET",
                            Value = "0f84a005000048896808488978104c8960184c8968e8488d0d", // 25 digits,
                            ASMSignature = true,
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT offsets
                                144L
                                    // values above are "Target" from ACT. Adjust to what ffxivapp expects:
                                + 32L
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "CHATLOG",
                            Value = "e8********85c0740e488b0d********33D2E8********488b0d",
                            ASMSignature = true,
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "ChatLogLenStart" offsets
                                0L,
                                48L,
                                1048L
                                    // values above are "ChatLogLenStart" from ACT. Adjust to what ffxivapp expects:
                                - 0x24
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "CHARMAP",
                            Value = "48c1e8033dffff0000742b3da80100007324488d0d",
                            ASMSignature = true,
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "MobArray" offsets
                                0L
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "PARTYMAP",
                            Value = "85d27508b0014883c4205bc3488d0d",
                            ASMSignature = true,
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "PartyList" offsets
                                0L
                                    // values above are "PartyList" from ACT. Adjust to what ffxivapp expects:
                                + 0x10
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "MAP",
                            Value = "b83d020000488bac24a00000004883c4705f5e5bc38b0d",
                            ASMSignature = true,
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "ZoneID" offsets
                                0L
                            }
                        });
                        // or just pure offsets from base address
                        signatures.Add(new Signature
                        {
                            Key = "PLAYERINFO",
                            PointerPath = new List<long>
                            {
                                0x7FF785DA4B20
                                //0x103F518
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "AGRO",
                            PointerPath = new List<long>
                            {
                                0x103EBF4
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "AGRO_COUNT",
                            PointerPath = new List<long>
                            {
                                0x103EBF4 + 0x900
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "ENMITYMAP",
                            PointerPath = new List<long>
                            {
                                0x103E2EC
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "PARTYCOUNT",
                            PointerPath = new List<long>
                            {
                                0x10A5E6C
                            }
                        });
                        // TODO: Need to do all 64 bit values still
                        //signatures.Add(new Signature
                        //{
                        //    Key = "GAMEMAIN",
                        //    Value = "47616D654D61696E000000",
                        //    Offset = 1672
                        //});
                        //signatures.Add(new Signature
                        //{
                        //    Key = "CHARMAP",
                        //    Value = "DB0FC940AAAA26416E30763FDB0FC93FDB0F49416F12833A000000000000000000000000????0000????0000FFFFFFFF",
                        //    Offset = 60
                        //});
                        //signatures.Add(new Signature
                        //{
                        //    Key = "ENMITYMAP",
                        //    Value = "FFFFFFFF00000000000000000000000000000000000000000000000000000000000000000000????????????????????????????????????????????DB0FC940AAAA26416E30763FDB0FC93FDB0F49416F12833AFFFFFFFF",
                        //    Offset = 96 // pre 3.0 2.4
                        //});
                        //signatures.Add(new Signature
                        //{
                        //    Key = "PARTYMAP",
                        //    Value = "FFFFFFFF00000000000000000000000000000000000000000000000000000000000000000000000000000000DB0FC940AAAA26416E30763FDB0FC93FDB0F49416F12833AFFFFFFFF",
                        //    Offset = -188764
                        //});
                        //signatures.Add(new Signature
                        //{
                        //    Key = "PARTYCOUNT",
                        //    Value = "5F50617274794C69737400",
                        //    Offset = 2416
                        //});
                        //signatures.Add(new Signature
                        //{
                        //    Key = "MAP",
                        //    Value = "F783843E????????????????????????FFFFFFFFDB0FC940AAAA26416E30763FDB0FC93FDB0F49416F12833A",
                        //    Offset = 3092
                        //});
                        //signatures.Add(new Signature
                        //{
                        //    Key = "TARGET",
                        //    Value = "DB0F49416F12833AFFFFFFFF0000000000000000000000000000000000000000????????00000000DB0FC940AAAA26416E30763FDB0FC93FDB0F49416F12833A0000000000000000",
                        //    Offset = 472
                        //});
                        //MemoryHandler.Instance.PointerPaths["PLAYERINFO"] = new List<long>()
                        //{
                        //    0x1679030
                        //};
                        //MemoryHandler.Instance.PointerPaths["AGRO"] = new List<long>()
                        //{
                        //    0x1678708 + 8
                        //};
                        //MemoryHandler.Instance.PointerPaths["AGRO_COUNT"] = new List<long>()
                        //{
                        //    0x1679010
                        //};
                    }
                    else // 32 bit
                    {
                        // can still use old style entry of signatures
                        signatures.Add(new Signature
                        {
                            Key = "GAMEMAIN",
                            Value = "47616D654D61696E000000",
                            Offset = 1344 // is this even used anymore?
                        });
                        // or a combination of signature and offset from that signature
                        signatures.Add(new Signature
                        {
                            Key = "TARGET",
                            Value = "750e85d2750ab9", // 7 digits
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT offsets
                                92L
                                    // values above are "Target" from ACT. Adjust to what ffxivapp expects:
                                + 16L
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "CHATLOG",
                            Value = "8b55fc83e2f983ca098b4d08a1********515250E8********a1",
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "ChatLogLenStart" offsets
                                0L,
                                24L,
                                736L
                                    // values above are "ChatLogLenStart" from ACT. Adjust to what ffxivapp expects:
                                - 0x24
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "CHARMAP",
                            Value = "81feffff0000743581fe58010000732d8b3cb5",
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "MobArray" offsets
                                0L
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "PARTYMAP",
                            Value = "85c074178b407450b9",
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "PartyList" offsets
                                0L
                                    // values above are "PartyList" from ACT. Adjust to what ffxivapp expects:
                                + 0x10
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "MAP",
                            Value = "8b0d********85c975068b0d",
                            PointerPath = new List<long>
                            {
                                0L, // ACT assumes the first entry after the signature is the pointer. Manually do a zero offset to replicate.
                                // Start ACT "ZoneID" offsets
                                0L
                            }
                        });

                        // 02292318
                        // or just pure offsets from base address
                        signatures.Add(new Signature
                        {
                            Key = "PLAYERINFO",
                            PointerPath = new List<long>
                            {
                                0x1092318
                                //0x1042580
                                
                            }
                        });

                        signatures.Add(new Signature
                        {
                            Key = "AGRO",
                            PointerPath = new List<long>
                            {
                                0x10919F4
                                //0x1041BF0
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "AGRO_COUNT",
                            PointerPath = new List<long>
                            {
                                0x10919F4 + 0x900
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "ENMITYMAP",
                            PointerPath = new List<long>
                            {
                                0x10412EC
                                //0x103E2EC
                            }
                        });
                        signatures.Add(new Signature
                        {
                            Key = "PARTYCOUNT",
                            PointerPath = new List<long>
                            {
                                0x10A8EBC
                            }
                        });
                        // TODO: Update the following for patch 3.1
                        //signatures.Add(new Signature
                        //{
                        //    Key = "INVENTORY",
                        //    Value = "0000??00000000000000DB0FC940AAAA26416D30763FDB0FC93FDB0F49416F12833AFFFFFFFF00000000??00??00??00??00??????00??00????0000????????????",
                        //    Offset = 106
                        //});
                    }
                    break;
            }

            return signatures;
        }
    }
}
