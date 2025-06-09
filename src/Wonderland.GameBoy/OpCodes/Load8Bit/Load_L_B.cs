namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_L_B() : OpCode(
    0x68,
    "LD L, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.B;
                return true;
            }
    ]);

