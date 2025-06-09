namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_L_E() : OpCode(
    0x6B,
    "LD L, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.E;
                return true;
            }
    ]);

