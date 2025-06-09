namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_FF00_C() : OpCode(
    0xF2,
    "LD A, (FF00+C)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.A = m.GetMemory(Bits.CreateU16(0xFF, r.C));
                return false;
            },
        (_, _, _) => true
    ]);
