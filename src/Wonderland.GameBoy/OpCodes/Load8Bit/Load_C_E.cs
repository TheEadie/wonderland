namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_C_E() : OpCode(
    0x4B,
    "LD C, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.E;
                return true;
            }
    ]);
