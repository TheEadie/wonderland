using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;
using Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;
using Wonderland.GameBoy.OpCodes.CpuControl;
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
            { 0xCF, new CCF() },
            { 0xC7, new SCF() },
            { 0x00, new NOP() },
            { 0x76, new Halt() },
            { 0x10, new Stop() },
            { 0xF3, new DisableInterrupts() },
            { 0xFB, new EnableInterrupts() },
            #endregion

            // 8-bit Arithmetic / Logic Instructions

            #region 8-bit Arithmetic/Logic
            #region ADD
            { 0x87, new Add_A_A() },
            { 0x80, new Add_A_B() },
            { 0x81, new Add_A_C() },
            { 0x82, new Add_A_D() },
            { 0x83, new Add_A_E() },
            { 0x84, new Add_A_H() },
            { 0x85, new Add_A_L() },
            { 0x86, new Add_A_HL() },
            { 0xC6, new Add_A_u8() },
            #endregion

            #region ADD with Carry
            { 0x8F, new AddWithCarry_A_A() },
            { 0x88, new AddWithCarry_A_B() },
            { 0x89, new AddWithCarry_A_C() },
            { 0x8A, new AddWithCarry_A_D() },
            { 0x8B, new AddWithCarry_A_E() },
            { 0x8C, new AddWithCarry_A_H() },
            { 0x8D, new AddWithCarry_A_L() },
            { 0x8E, new AddWithCarry_A_HL() },
            { 0xCE, new AddWithCarry_A_u8() },
            #endregion

            #region SUB
            { 0x97, new Sub_A_A() },
            { 0x90, new Sub_A_B() },
            { 0x91, new Sub_A_C() },
            { 0x92, new Sub_A_D() },
            { 0x93, new Sub_A_E() },
            { 0x94, new Sub_A_H() },
            { 0x95, new Sub_A_L() },
            { 0x96, new Sub_A_HL() },
            { 0xD6, new Sub_A_u8() },
            #endregion

            #region SUB with Carry
            { 0x9F, new SubWithCarry_A_A() },
            { 0x98, new SubWithCarry_A_B() },
            { 0x99, new SubWithCarry_A_C() },
            { 0x9A, new SubWithCarry_A_D() },
            { 0x9B, new SubWithCarry_A_E() },
            { 0x9C, new SubWithCarry_A_H() },
            { 0x9D, new SubWithCarry_A_L() },
            { 0x9E, new SubWithCarry_A_HL() },
            { 0xDE, new SubWithCarry_A_u8() },
            #endregion

            #region AND
            { 0xA7, new And_A_A() },
            { 0xA0, new And_A_B() },
            { 0xA1, new And_A_C() },
            { 0xA2, new And_A_D() },
            { 0xA3, new And_A_E() },
            { 0xA4, new And_A_H() },
            { 0xA5, new And_A_L() },
            { 0xA6, new And_A_HL() },
            { 0xE6, new And_A_u8() },
            #endregion

            #region XOR
            { 0xAF, new Xor_A_A() },
            { 0xA8, new Xor_A_B() },
            { 0xA9, new Xor_A_C() },
            { 0xAA, new Xor_A_D() },
            { 0xAB, new Xor_A_E() },
            { 0xAC, new Xor_A_H() },
            { 0xAD, new Xor_A_L() },
            { 0xAE, new Xor_A_HL() },
            { 0xEE, new Xor_A_u8() },
            #endregion

            #region OR
            { 0xB7, new Or_A_A() },
            { 0xB0, new Or_A_B() },
            { 0xB1, new Or_A_C() },
            { 0xB2, new Or_A_D() },
            { 0xB3, new Or_A_E() },
            { 0xB4, new Or_A_H() },
            { 0xB5, new Or_A_L() },
            { 0xB6, new Or_A_HL() },
            { 0xF6, new Or_A_u8() },
            #endregion

            #region CP
            { 0xBF, new Compare_A_A() },
            { 0xB8, new Compare_A_B() },
            { 0xB9, new Compare_A_C() },
            { 0xBA, new Compare_A_D() },
            { 0xBB, new Compare_A_E() },
            { 0xBC, new Compare_A_H() },
            { 0xBD, new Compare_A_L() },
            { 0xBE, new Compare_A_HL() },
            { 0xFE, new Compare_A_u8() },
            #endregion

            #region INC
            { 0x3C, new Inc_A() },
            { 0x04, new Inc_B() },
            { 0x0C, new Inc_C() },
            { 0x14, new Inc_D() },
            { 0x1C, new Inc_E() },
            { 0x24, new Inc_H() },
            { 0x2C, new Inc_L() },
            { 0x34, new Inc_HL() },
            #endregion

            #region DEC
            { 0x3D, new Dec_A() },
            { 0x05, new Dec_B() },
            { 0x0D, new Dec_C() },
            { 0x15, new Dec_D() },
            { 0x1D, new Dec_E() },
            { 0x25, new Dec_H() },
            { 0x2D, new Dec_L() },
            { 0x35, new Dec_HL() },
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

    internal static void And(Registers r, u8 value)
    {
        var result = r.A & value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = true;
        r.FlagC = false;
        r.A = (u8)result;
    }

    internal static void Xor(Registers r, u8 value)
    {
        var result = r.A ^ value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8)result;
    }

    internal static void Or(Registers r, u8 value)
    {
        var result = r.A | value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8)result;
    }

    internal static void Compare(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
    }

    internal static void Sub(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8)result;
    }

    internal static void SubWithCarry(Registers r, u8 value)
    {
        var c = r.FlagC ? 1 : 0;
        var result = r.A - value - c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) + c > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8)result;
    }

    internal static void Add(Registers r, u8 value)
    {
        var result = r.A + value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8)result;
    }

    internal static void AddWithCarry(Registers r, u8 value)
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
