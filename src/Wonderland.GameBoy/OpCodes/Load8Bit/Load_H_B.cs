namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_H_B() : OpCode(
    0x60,
    "LD H, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.B;
                return true;
            }
    ]);
