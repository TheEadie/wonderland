namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_E_E() : OpCode(
    0x5B,
    "LD E, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.E;
                return true;
            }
    ]);
