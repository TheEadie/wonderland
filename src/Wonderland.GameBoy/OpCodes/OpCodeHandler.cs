using Wonderland.GameBoy.OpCodes.Load16Bit;
using Wonderland.GameBoy.OpCodes.Load8Bit;
using u8 = byte;
using s8 = sbyte;

namespace Wonderland.GameBoy.OpCodes;

public class OpCodeHandler
{
    private readonly Dictionary<u8, OpCode> _opCodes;

    // Yuck
    private u8 _lsb;
    private u8 _msb;
    private s8 _signed8Bit;

    public OpCodeHandler()
    {
        _opCodes = new Dictionary<u8, OpCode>
        {
            // 8-bit Load Instructions

            #region 8-bit load
            #region r=r
            { 0x7F, new Load_A_A() },
            { 0x78, new Load_A_B() },
            { 0x79, new Load_A_C() },
            { 0x7A, new Load_A_D() },
            { 0x7B, new Load_A_E() },
            { 0x7C, new Load_A_H() },
            { 0x7D, new Load_A_L() },
            { 0x47, new Load_B_A() },
            { 0x40, new Load_B_B() },
            { 0x41, new Load_B_C() },
            { 0x42, new Load_B_D() },
            { 0x43, new Load_B_E() },
            { 0x44, new Load_B_H() },
            { 0x45, new Load_B_L() },
            { 0x4F, new Load_C_A() },
            { 0x48, new Load_C_B() },
            { 0x49, new Load_C_C() },
            { 0x4A, new Load_C_D() },
            { 0x4B, new Load_C_E() },
            { 0x4C, new Load_C_H() },
            { 0x4D, new Load_C_L() },
            { 0x57, new Load_D_A() },
            { 0x50, new Load_D_B() },
            { 0x51, new Load_D_C() },
            { 0x52, new Load_D_D() },
            { 0x53, new Load_D_E() },
            { 0x54, new Load_D_H() },
            { 0x55, new Load_D_L() },
            { 0x5F, new Load_E_A() },
            { 0x58, new Load_E_B() },
            { 0x59, new Load_E_C() },
            { 0x5A, new Load_E_D() },
            { 0x5B, new Load_E_E() },
            { 0x5C, new Load_E_H() },
            { 0x5D, new Load_E_L() },
            { 0x67, new Load_H_A() },
            { 0x60, new Load_H_B() },
            { 0x61, new Load_H_C() },
            { 0x62, new Load_H_D() },
            { 0x63, new Load_H_E() },
            { 0x64, new Load_H_H() },
            { 0x65, new Load_H_L() },
            { 0x6F, new Load_L_A() },
            { 0x68, new Load_L_B() },
            { 0x69, new Load_L_C() },
            { 0x6A, new Load_L_D() },
            { 0x6B, new Load_L_E() },
            { 0x6C, new Load_L_H() },
            { 0x6D, new Load_L_L() },
            #endregion

            #region r=n
            { 0x3E, new Load_A_u8() },
            { 0x06, new Load_B_u8() },
            { 0x0E, new Load_C_u8() },
            { 0x16, new Load_D_u8() },
            { 0x1E, new Load_E_u8() },
            { 0x26, new Load_H_u8() },
            { 0x2E, new Load_L_u8() },
            #endregion

            #region r=(HL)
            { 0x7E, new Load_A_HL() },
            { 0x46, new Load_B_HL() },
            { 0x4E, new Load_C_HL() },
            { 0x56, new Load_D_HL() },
            { 0x5E, new Load_E_HL() },
            { 0x66, new Load_H_HL() },
            { 0x6E, new Load_L_HL() },
            #endregion

            #region (HL)=r
            { 0x77, new Load_HL_A() },
            { 0x70, new Load_HL_B() },
            { 0x71, new Load_HL_C() },
            { 0x72, new Load_HL_D() },
            { 0x73, new Load_HL_E() },
            { 0x74, new Load_HL_H() },
            { 0x75, new Load_HL_L() },
            #endregion

            #region other
            { 0x36, new Load_HL_u8() },
            { 0x0A, new Load_A_BC() },
            { 0x1A, new Load_A_DE() },
            { 0xFA, new Load_A_u16() },
            { 0x02, new Load_BC_A() },
            { 0x12, new Load_DE_A() },
            { 0xEA, new Load_u16_A() },
            { 0xF0, new Load_A_FF00_u8() },
            { 0xE0, new Load_FF00_u8_A() },
            { 0xF2, new Load_A_FF00_C() },
            { 0xE2, new Load_FF00_C_A() },
            { 0x22, new Load_HL_INC_A() },
            { 0x2A, new Load_A_HL_INC() },
            { 0x32, new Load_HL_DEC_A() },
            { 0x3A, new Load_A_HL_DEC() },
            #endregion
            #endregion

            // 16-bit Load Instructions

            #region 16-bit load
            { 0x01, new Load_BC_u16() },
            { 0x11, new Load_DE_u16() },
            { 0x21, new Load_HL_u16() },
            { 0x31, new Load_SP_u16() },
            { 0x08, new Load_u16_SP() },
            { 0xF9, new Load_SP_HL() },
            { 0xC5, new Push_BC() },
            { 0xD5, new Push_DE() },
            { 0xE5, new Push_HL() },
            { 0xF5, new Push_AF() },
            { 0xC1, new Pop_BC() },
            { 0xD1, new Pop_DE() },
            { 0xE1, new Pop_HL() },
            { 0xF1, new Pop_AF() },
            #endregion

            // CPU Control Instructions

            #region CPU Control
            {
                0xCF, new OpCode(
                    0xCF,
                    "CCF",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagC = !r.FlagC;
                                r.FlagH = false;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0xC7, new OpCode(
                    0xC7,
                    "SCF",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagC = true;
                                r.FlagH = false;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            { 0x00, new OpCode(0x00, "NOP", 1, 4, [(_, _, _) => true]) },
            { 0x76, new OpCode(0x76, "HALT", 1, 4, [(_, _, _) => throw new NotImplementedException()]) },
            { 0x10, new OpCode(0x10, "STOP", 1, 4, [(_, _, _) => throw new NotImplementedException()]) },
            {
                0xF3, new OpCode(
                    0xF3,
                    "DI",
                    1,
                    4,
                    [
                        (_, _, i) =>
                            {
                                i.DisableInterrupts();
                                return true;
                            }
                    ])
            },
            {
                0xFB, new OpCode(
                    0xFB,
                    "EI",
                    1,
                    4,
                    [
                        (_, _, i) =>
                            {
                                i.EnableInterruptsWithDelay();
                                return true;
                            }
                    ])
            },
            #endregion

            // 8-bit Arithmetic / Logic Instructions

            #region 8-bit Arithmetic/Logic
            #region ADD
            {
                0x87, new OpCode(
                    0x87,
                    "ADD A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0x80, new OpCode(
                    0x80,
                    "ADD A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0x81, new OpCode(
                    0x81,
                    "ADD A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0x82, new OpCode(
                    0x82,
                    "ADD A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0x83, new OpCode(
                    0x83,
                    "ADD A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0x84, new OpCode(
                    0x84,
                    "ADD A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0x85, new OpCode(
                    0x85,
                    "ADD A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Add(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0x86, new OpCode(
                    0x86,
                    "ADD A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Add(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xC6, new OpCode(
                    0xC6,
                    "ADD A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Add(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region ADD with Carry
            {
                0x8F, new OpCode(
                    0x8F,
                    "ADC A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0x88, new OpCode(
                    0x88,
                    "ADC A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0x89, new OpCode(
                    0x89,
                    "ADC A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0x8A, new OpCode(
                    0x8A,
                    "ADC A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0x8B, new OpCode(
                    0x8B,
                    "ADC A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0x8C, new OpCode(
                    0x8C,
                    "ADC A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0x8D, new OpCode(
                    0x8D,
                    "ADC A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                AddWithCarry(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0x8E, new OpCode(
                    0x8E,
                    "ADC A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                AddWithCarry(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xCE, new OpCode(
                    0xCE,
                    "ADC A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                AddWithCarry(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region SUB
            {
                0x97, new OpCode(
                    0x97,
                    "SUB A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0x90, new OpCode(
                    0x90,
                    "SUB A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0x91, new OpCode(
                    0x91,
                    "SUB A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0x92, new OpCode(
                    0x92,
                    "SUB A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0x93, new OpCode(
                    0x93,
                    "SUB A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0x94, new OpCode(
                    0x94,
                    "SUB A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0x95, new OpCode(
                    0x95,
                    "SUB A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Sub(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0x96, new OpCode(
                    0x96,
                    "SUB A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Sub(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xD6, new OpCode(
                    0xD6,
                    "SUB A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Sub(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region SUB with Carry
            {
                0x9F, new OpCode(
                    0x9F,
                    "SBC A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0x98, new OpCode(
                    0x98,
                    "SBC A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0x99, new OpCode(
                    0x99,
                    "SBC A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0x9A, new OpCode(
                    0x9A,
                    "SBC A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0x9B, new OpCode(
                    0x9B,
                    "SBC A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0x9C, new OpCode(
                    0x9C,
                    "SBC A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0x9D, new OpCode(
                    0x9D,
                    "SBC A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                SubWithCarry(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0x9E, new OpCode(
                    0x9E,
                    "SBC A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                SubWithCarry(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xDE, new OpCode(
                    0xDE,
                    "SBC A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                SubWithCarry(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region AND
            {
                0xA7, new OpCode(
                    0xA7,
                    "AND A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0xA0, new OpCode(
                    0xA0,
                    "AND A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0xA1, new OpCode(
                    0xA1,
                    "AND A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0xA2, new OpCode(
                    0xA2,
                    "AND A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0xA3, new OpCode(
                    0xA3,
                    "AND A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0xA4, new OpCode(
                    0xA4,
                    "AND A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0xA5, new OpCode(
                    0xA5,
                    "AND A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                And(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0xA6, new OpCode(
                    0xA6,
                    "AND A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                And(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xE6, new OpCode(
                    0xE6,
                    "AND A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                And(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region XOR
            {
                0xAF, new OpCode(
                    0xAF,
                    "XOR A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0xA8, new OpCode(
                    0xA8,
                    "XOR A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0xA9, new OpCode(
                    0xA9,
                    "XOR A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0xAA, new OpCode(
                    0xAA,
                    "XOR A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0xAB, new OpCode(
                    0xAB,
                    "XOR A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0xAC, new OpCode(
                    0xAC,
                    "XOR A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0xAD, new OpCode(
                    0xAD,
                    "XOR A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Xor(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0xAE, new OpCode(
                    0xAE,
                    "XOR A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Xor(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xEE, new OpCode(
                    0xDE,
                    "XOR A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Xor(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region OR
            {
                0xB7, new OpCode(
                    0xB7,
                    "OR A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0xB0, new OpCode(
                    0xB0,
                    "OR A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0xB1, new OpCode(
                    0xB1,
                    "OR A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0xB2, new OpCode(
                    0xB2,
                    "OR A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0xB3, new OpCode(
                    0xB3,
                    "OR A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0xB4, new OpCode(
                    0xB4,
                    "OR A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0xB5, new OpCode(
                    0xB5,
                    "OR A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Or(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0xB6, new OpCode(
                    0xB6,
                    "OR A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Or(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xF6, new OpCode(
                    0xF6,
                    "OR A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Or(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region CP
            {
                0xBF, new OpCode(
                    0xBF,
                    "CP A, A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.A);
                                return true;
                            }
                    ])
            },
            {
                0xB8, new OpCode(
                    0xB8,
                    "CP A, B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.B);
                                return true;
                            }
                    ])
            },
            {
                0xB9, new OpCode(
                    0xB9,
                    "CP A, C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.C);
                                return true;
                            }
                    ])
            },
            {
                0xBA, new OpCode(
                    0xBA,
                    "CP A, D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.D);
                                return true;
                            }
                    ])
            },
            {
                0xBB, new OpCode(
                    0xBB,
                    "CP A, E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.E);
                                return true;
                            }
                    ])
            },
            {
                0xBC, new OpCode(
                    0xBC,
                    "CP A, H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.H);
                                return true;
                            }
                    ])
            },
            {
                0xBD, new OpCode(
                    0xBD,
                    "CP A, L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                Compare(r, r.L);
                                return true;
                            }
                    ])
            },
            {
                0xBE, new OpCode(
                    0xBE,
                    "CP A, (HL)",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Compare(r, m.GetMemory(r.HL));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0xFE, new OpCode(
                    0xFE,
                    "CP A, u8",
                    1,
                    8,
                    [
                        (r, m, _) =>
                            {
                                Compare(r, m.GetMemory(r.PC++));
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region INC
            {
                0x3C, new OpCode(
                    0x3C,
                    "INC A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.A & 0b_0000_1111) == 0b_0000_1111;
                                r.A++;
                                r.FlagZ = r.A == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x04, new OpCode(
                    0x04,
                    "INC B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.B & 0b_0000_1111) == 0b_0000_1111;
                                r.B++;
                                r.FlagZ = r.B == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x0C, new OpCode(
                    0x0C,
                    "INC C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.C & 0b_0000_1111) == 0b_0000_1111;
                                r.C++;
                                r.FlagZ = r.C == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x14, new OpCode(
                    0x14,
                    "INC D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.D & 0b_0000_1111) == 0b_0000_1111;
                                r.D++;
                                r.FlagZ = r.D == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x1C, new OpCode(
                    0x1C,
                    "INC E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.E & 0b_0000_1111) == 0b_0000_1111;
                                r.E++;
                                r.FlagZ = r.E == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x24, new OpCode(
                    0x24,
                    "INC H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.H & 0b_0000_1111) == 0b_0000_1111;
                                r.H++;
                                r.FlagZ = r.H == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x2C, new OpCode(
                    0x2C,
                    "INC L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.L & 0b_0000_1111) == 0b_0000_1111;
                                r.L++;
                                r.FlagZ = r.L == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x34, new OpCode(
                    0x34,
                    "INC (HL)",
                    1,
                    12,
                    [
                        (r, m, _) =>
                            {
                                _lsb = m.GetMemory(r.HL);
                                return false;
                            },
                        (r, m, _) =>
                            {
                                r.FlagH = (_lsb & 0b_0000_1111) == 0b_0000_1111;
                                _lsb++;
                                r.FlagZ = _lsb == 0;
                                r.FlagN = false;
                                m.WriteMemory(r.HL, _lsb);
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            #region DEC
            {
                0x3D, new OpCode(
                    0x3D,
                    "DEC A",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.A & 0b_0000_1111) == 0b_0000_0000;
                                r.A--;
                                r.FlagZ = r.A == 0;
                                r.FlagN = true;
                                return true;
                            }
                    ])
            },
            {
                0x05, new OpCode(
                    0x05,
                    "DEC B",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.B & 0b_0000_1111) == 0b_0000_0000;
                                r.B--;
                                r.FlagZ = r.B == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x0D, new OpCode(
                    0x0D,
                    "DEC C",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.C & 0b_0000_1111) == 0b_0000_0000;
                                r.C--;
                                r.FlagZ = r.C == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x15, new OpCode(
                    0x15,
                    "DEC D",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.D & 0b_0000_1111) == 0b_0000_0000;
                                r.D--;
                                r.FlagZ = r.D == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x1D, new OpCode(
                    0x1D,
                    "DEC E",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.E & 0b_0000_1111) == 0b_0000_0000;
                                r.E--;
                                r.FlagZ = r.E == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x25, new OpCode(
                    0x25,
                    "DEC H",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.H & 0b_0000_1111) == 0b_0000_0000;
                                r.H--;
                                r.FlagZ = r.H == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x2D, new OpCode(
                    0x2D,
                    "DEC L",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.FlagH = (r.L & 0b_0000_1111) == 0b_0000_0000;
                                r.L--;
                                r.FlagZ = r.L == 0;
                                r.FlagN = false;
                                return true;
                            }
                    ])
            },
            {
                0x35, new OpCode(
                    0x35,
                    "DEC (HL)",
                    1,
                    12,
                    [
                        (r, m, _) =>
                            {
                                _lsb = m.GetMemory(r.HL);
                                return false;
                            },
                        (r, m, _) =>
                            {
                                r.FlagH = (_lsb & 0b_0000_1111) == 0b_0000_0000;
                                _lsb--;
                                r.FlagZ = _lsb == 0;
                                r.FlagN = false;
                                m.WriteMemory(r.HL, _lsb);
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            #endregion

            {
                0x27, new OpCode(
                    0x27,
                    "DAA",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                if (r.FlagN)
                                {
                                    if (r.FlagC)
                                    {
                                        r.A -= 0x60;
                                    }

                                    if (r.FlagH)
                                    {
                                        r.A -= 0x6;
                                    }
                                }
                                else
                                {
                                    if (r.FlagC || r.A > 0x99)
                                    {
                                        r.A += 0x60;
                                        r.FlagC = true;
                                    }

                                    if (r.FlagH || (r.A & 0xF) > 0x9)
                                    {
                                        r.A += 0x6;
                                    }
                                }

                                r.FlagZ = r.A == 0;
                                r.FlagH = false;
                                return true;
                            }
                    ])
            },
            {
                0x2F, new OpCode(
                    0x2F,
                    "CPL",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.A = (u8)(r.A ^ 0xFF);
                                r.FlagN = true;
                                r.FlagH = true;
                                return true;
                            }
                    ])
            },
            #endregion

            {
                0x03, new OpCode(
                    0x03,
                    "INC BC",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.BC++;
                                return true;
                            }
                    ])
            },
            {
                0x13, new OpCode(
                    0x03,
                    "INC DE",
                    1,
                    4,
                    [
                        (r, _, _) =>
                            {
                                r.DE++;
                                return true;
                            }
                    ])
            },
            {
                0xC3, new OpCode(
                    0xC3,
                    "JP u16",
                    3,
                    16,
                    [
                        (r, m, _) =>
                            {
                                _lsb = m.GetMemory(r.PC++);
                                return false;
                            },
                        (r, m, _) =>
                            {
                                _msb = m.GetMemory(r.PC++);
                                return false;
                            },
                        (r, _, _) =>
                            {
                                r.PC = Bits.CreateU16(_msb, _lsb);
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0x20, new OpCode(
                    0x20,
                    "JP NZ s8",
                    2,
                    12,
                    [
                        (r, m, _) =>
                            {
                                _signed8Bit = m.GetSignedMemory(r.PC++);
                                return false;
                            },
                        (r, _, _) =>
                            {
                                if (r.FlagZ)
                                {
                                    return true;
                                }

                                r.PC = Convert.ToUInt16(r.PC + _signed8Bit);
                                return false;
                            },
                        (_, _, _) => true
                    ])
            },
            {
                0x30, new OpCode(
                    0x30,
                    "JP NC s8",
                    2,
                    12,
                    [
                        (r, m, _) =>
                            {
                                _signed8Bit = m.GetSignedMemory(r.PC++);
                                return false;
                            },
                        (r, _, _) =>
                            {
                                if (r.FlagC)
                                {
                                    return true;
                                }

                                r.PC = Convert.ToUInt16(r.PC + _signed8Bit);
                                return false;
                            },
                        (_, _, _) => true
                    ])
            }
        };
    }

    private static void And(Registers r, u8 value)
    {
        var result = r.A & value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = true;
        r.FlagC = false;
        r.A = (u8)result;
    }

    private static void Xor(Registers r, u8 value)
    {
        var result = r.A ^ value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8)result;
    }

    private static void Or(Registers r, u8 value)
    {
        var result = r.A | value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8)result;
    }

    private static void Compare(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
    }

    private static void Sub(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8)result;
    }

    private static void SubWithCarry(Registers r, u8 value)
    {
        var c = r.FlagC ? 1 : 0;
        var result = r.A - value - c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) + c > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8)result;
    }

    private static void Add(Registers r, u8 value)
    {
        var result = r.A + value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8)result;
    }

    private static void AddWithCarry(Registers r, u8 value)
    {
        var c = r.FlagC ? 1 : 0;
        var result = r.A + value + c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) + c > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8)result;
    }

    public OpCode Lookup(u8 value)
    {
        if (!_opCodes.ContainsKey(value))
        {
            throw new NotImplementedException($"Unknown Opcode: 0x{value:X2}");
        }

        return _opCodes[value];
    }
}
