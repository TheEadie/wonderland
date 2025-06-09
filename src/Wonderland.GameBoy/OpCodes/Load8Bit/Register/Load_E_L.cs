namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_E_L() : OpCode(
    0x5D,
    "LD E, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.L;
                return true;
            }
    ]);
