namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_L() : OpCode(
    0x7D,
    "LD A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.L;
                return true;
            }
    ]);
