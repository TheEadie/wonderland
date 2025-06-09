namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_C_L() : OpCode(
    0x4D,
    "LD C, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.L;
                return true;
            }
    ]);
