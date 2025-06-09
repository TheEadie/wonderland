namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_D_E() : OpCode(
    0x53,
    "LD D, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.E;
                return true;
            }
    ]);
