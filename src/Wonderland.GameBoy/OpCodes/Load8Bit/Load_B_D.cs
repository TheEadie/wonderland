namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_D() : OpCode(
    0x42,
    "LD B, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.D;
                return true;
            }
    ]);
