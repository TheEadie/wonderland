namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_L_H() : OpCode(
    0x6C,
    "LD L, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.H;
                return true;
            }
    ]);
