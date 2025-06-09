namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_E_A() : OpCode(
    0x5F,
    "LD E, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.A;
                return true;
            }
    ]);

