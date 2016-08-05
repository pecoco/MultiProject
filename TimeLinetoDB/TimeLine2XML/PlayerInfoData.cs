using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TimeLinetoDB.PlayerStructure;

namespace TimeLinetoDB
{

        internal static class PlayerInfoData
        {
            public static PlayerStructure ResolvePlayerFromBytes(byte[] source)
            {
                var entry = new PlayerStructure();
                try
                {
                    //entry.Name = GetStringFromBytes(source, 1);


                            entry.JobID = source[0x66];
                            //entry.Job = (Actor.Job)entry.JobID;

                            #region Job Levels

                            var step = 2;
                            var i = 0x68 - step;
              
                
                            entry.PGL = source[i += step];
                            entry.GLD = source[i += step];
                            entry.MRD = source[i += step];
                            entry.ARC = source[i += step];
                            entry.LNC = source[i += step];
                            entry.THM = source[i += step];
                            entry.CNJ = source[i += step];

                            entry.CPT = source[i += step];
                            entry.BSM = source[i += step];
                            entry.ARM = source[i += step];
                            entry.GSM = source[i += step];
                            entry.LTW = source[i += step];
                            entry.WVR = source[i += step];
                            entry.ALC = source[i += step];
                            entry.CUL = source[i += step];

                            entry.MIN = source[i += step];
                            entry.BTN = source[i += step];
                            entry.FSH = source[i += step];

                            entry.ACN = source[i += step];
                            entry.ROG = source[i += step];

                            entry.MCH = source[i += step];
                            entry.DRK = source[i += step];
                            entry.AST = source[i += step];

                            #endregion

                            #region Current Experience

                            step = 4;
                            i = 0x98 - step;

                            entry.PGL_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.GLD_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.MRD_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.ARC_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.LNC_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.THM_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.CNJ_CurrentEXP = BitConverter.ToInt32(source, i += step);

                            entry.CPT_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.BSM_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.ARM_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.GSM_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.LTW_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.WVR_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.ALC_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.CUL_CurrentEXP = BitConverter.ToInt32(source, i += step);

                            entry.MIN_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.BTN_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.FSH_CurrentEXP = BitConverter.ToInt32(source, i += step);

                            entry.ACN_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.ROG_CurrentEXP = BitConverter.ToInt32(source, i += step);

                            entry.MCH_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.DRK_CurrentEXP = BitConverter.ToInt32(source, i += step);
                            entry.AST_CurrentEXP = BitConverter.ToInt32(source, i += step);

                            #endregion

                            #region Base Stats

                            step = 4;
                            i = 0x100 - step;

                            entry.BaseStrength = BitConverter.ToInt16(source, i += step);
                            entry.BaseDexterity = BitConverter.ToInt16(source, i += step);
                            entry.BaseVitality = BitConverter.ToInt16(source, i += step);
                            entry.BaseIntelligence = BitConverter.ToInt16(source, i += step);
                            entry.BaseMind = BitConverter.ToInt16(source, i += step);
                            entry.BasePiety = BitConverter.ToInt16(source, i += step);

                            #endregion

                            #region Base Stats (base+gear+bonus)

                            step = 4;
                            i = 0x11C - step;

                            entry.Strength = BitConverter.ToInt16(source, i += step);
                            entry.Dexterity = BitConverter.ToInt16(source, i += step);
                            entry.Vitality = BitConverter.ToInt16(source, i += step);
                            entry.Intelligence = BitConverter.ToInt16(source, i += step);
                            entry.Mind = BitConverter.ToInt16(source, i += step);
                            entry.Piety = BitConverter.ToInt16(source, i += step);

                            #endregion

                            #region Basic Info

                            step = 4;
                            i = 0x138 - step;

                            entry.HPMax = BitConverter.ToInt16(source, i += step);
                            entry.MPMax = BitConverter.ToInt16(source, i += step);
                            entry.TPMax = BitConverter.ToInt16(source, i += step);
                            entry.GPMax = BitConverter.ToInt16(source, i += step);
                            entry.CPMax = BitConverter.ToInt16(source, i += step);

                            #endregion

                            #region Offensive Properties

                            entry.Accuracy = BitConverter.ToInt16(source, 0x170);
                            entry.CriticalHitRate = BitConverter.ToInt16(source, 0x184);
                            entry.Determination = BitConverter.ToInt16(source, 0x1C8);

                            #endregion

                            #region Defensive Properties

                            entry.Parry = BitConverter.ToInt16(source, 0x164);
                            entry.Defense = BitConverter.ToInt16(source, 0x16C);
                            entry.MagicDefense = BitConverter.ToInt16(source, 0x178);

                            #endregion

                            #region Phyiscal Properties

                            entry.AttackPower = BitConverter.ToInt16(source, 0x168);
                            entry.SkillSpeed = BitConverter.ToInt16(source, 0x1CC);

                            #endregion

                            #region Mental Properties

                            entry.SpellSpeed = BitConverter.ToInt16(source, 0x174);
                            entry.AttackMagicPotency = BitConverter.ToInt16(source, 0x19C);
                            entry.HealingMagicPotency = BitConverter.ToInt16(source, 0x1A0);

                            #endregion

                            #region Status Resistances

                            //entry.SlowResistance = BitConverter.ToInt16(source, 0x1C8);
                            //entry.SilenceResistance = BitConverter.ToInt16(source, 0x1CC);
                            //entry.BindResistance = BitConverter.ToInt16(source, 0x1D0);
                            //entry.PoisionResistance = BitConverter.ToInt16(source, 0x1D4);
                            //entry.StunResistance = BitConverter.ToInt16(source, 0x1D8);
                            //entry.SleepResistance = BitConverter.ToInt16(source, 0x1DC);
                            //entry.BindResistance = BitConverter.ToInt16(source, 0x1E0);
                            //entry.HeavyResistance = BitConverter.ToInt16(source, 0x1E4);

                            #endregion

                            #region Elemental Resistances

                            step = 4;
                            i = 0x1AC - step;

                            entry.FireResistance = BitConverter.ToInt16(source, i += step);
                            entry.IceResistance = BitConverter.ToInt16(source, i += step);
                            entry.WindResistance = BitConverter.ToInt16(source, i += step);
                            entry.EarthResistance = BitConverter.ToInt16(source, i += step);
                            entry.LightningResistance = BitConverter.ToInt16(source, i += step);
                            entry.WaterResistance = BitConverter.ToInt16(source, i += step);

                            #endregion

                            #region Physical Resistances

                            step = 4;
                            i = 0x18C - step;

                            entry.SlashingResistance = BitConverter.ToInt16(source, i += step);
                            entry.PiercingResistance = BitConverter.ToInt16(source, i += step);
                            entry.BluntResistance = BitConverter.ToInt16(source, i += step);

                            #endregion

                            #region Crafting

                            entry.Craftmanship = BitConverter.ToInt16(source, 0x230);
                            entry.Control = BitConverter.ToInt16(source, 0x234);

                            #endregion

                            #region Gathering

                            entry.Gathering = BitConverter.ToInt16(source, 0x238);
                            entry.Perception = BitConverter.ToInt16(source, 0x23C);

                            #endregion


                }
                catch (Exception ex)
                {
                }
                return entry;
            }
        }
    }

