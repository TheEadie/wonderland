namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_D_H() : OpCode(
    0x54,
    "LD D, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.H;
                return true;
            }
    ]);
