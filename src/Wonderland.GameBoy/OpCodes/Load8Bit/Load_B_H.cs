namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_H() : OpCode(
    0x44,
    "LD B, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.H;
                return true;
            }
    ]);
