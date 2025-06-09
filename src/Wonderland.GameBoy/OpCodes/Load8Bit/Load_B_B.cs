namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_B() : OpCode(
    0x40,
    "LD B, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.B;
                return true;
            }
    ]);
