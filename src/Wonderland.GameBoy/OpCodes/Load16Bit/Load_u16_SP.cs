using u8 = byte;
using u16 = ushort;

namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Load_u16_SP() : OpCode(
    0x08,
    "LD (u16), SP",
    3,
    20,
    [
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                r.SubOp_MSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB), (u8)(r.SP & 0b_0000_0000_1111_1111));
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(
                    (u16)(Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB) + 1),
                    (u8)(r.SP & (0b_1111_1111_0000_0000 >> 8)));
                return false;
            },
        (_, _, _) => true
    ]);
