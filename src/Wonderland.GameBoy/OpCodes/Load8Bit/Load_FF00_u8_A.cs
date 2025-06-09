namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_FF00_u8_A() : OpCode(
    0xE0,
    "LD (FF00+u8), A",
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
                m.WriteMemory(Bits.CreateU16(0xFF, r.SubOp_LSB), r.A);
                return false;
            },
        (_, _, _) => true
    ]);
