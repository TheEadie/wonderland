namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_E() : OpCode(
    0x43,
    "LD B, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.E;
                return true;
            }
    ]);
