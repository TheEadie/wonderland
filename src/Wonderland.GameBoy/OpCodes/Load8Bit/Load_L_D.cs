namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_L_D() : OpCode(
    0x6A,
    "LD L, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.D;
                return true;
            }
    ]);

