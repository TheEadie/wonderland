namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_C_D() : OpCode(
    0x4A,
    "LD C, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.D;
                return true;
            }
    ]);
