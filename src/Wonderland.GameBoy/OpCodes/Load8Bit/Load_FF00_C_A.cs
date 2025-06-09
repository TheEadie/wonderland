namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_FF00_C_A() : OpCode(
    0xE2,
    "LD (FF00+C), A",
    2,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(Bits.CreateU16(0xFF, r.C), r.A);
                return false;
            },
        (_, _, _) => true
    ]);
