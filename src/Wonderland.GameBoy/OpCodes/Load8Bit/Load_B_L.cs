namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_L() : OpCode(
    0x45,
    "LD B, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.L;
                return true;
            }
    ]);
