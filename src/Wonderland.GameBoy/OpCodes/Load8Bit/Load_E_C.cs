namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_E_C() : OpCode(
    0x59,
    "LD E, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.C;
                return true;
            }
    ]);
