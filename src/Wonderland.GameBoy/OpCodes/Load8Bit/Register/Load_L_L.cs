namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_L_L() : OpCode(
    0x6D,
    "LD L, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.L;
                return true;
            }
    ]);
