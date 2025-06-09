namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_L_C() : OpCode(
    0x69,
    "LD L, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.C;
                return true;
            }
    ]);

