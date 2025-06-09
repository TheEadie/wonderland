namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_FF00_u8() : OpCode(
    0xF0,
    "LD A, (FF00+u8)",
    2,
    12,
    [
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                r.A = m.GetMemory(Bits.CreateU16(0xFF, r.SubOp_LSB));
                return false;
            },
        (_, _, _) => true
    ]);
