namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_E_D() : OpCode(
    0x5A,
    "LD E, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.D;
                return true;
            }
    ]);

