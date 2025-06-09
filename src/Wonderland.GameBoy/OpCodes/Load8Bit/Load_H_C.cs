namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_H_C() : OpCode(
    0x61,
    "LD H, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.C;
                return true;
            }
    ]);

