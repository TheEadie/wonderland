namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_C() : OpCode(
    0x41,
    "LD B, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.C;
                return true;
            }
    ]);
