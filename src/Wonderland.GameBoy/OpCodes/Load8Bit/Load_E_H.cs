namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_E_H() : OpCode(
    0x5C,
    "LD E, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.H;
                return true;
            }
    ]);
