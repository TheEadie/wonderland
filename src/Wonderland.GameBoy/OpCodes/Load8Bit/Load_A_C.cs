namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_C() : OpCode(
    0x79,
    "LD A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.C;
                return true;
            }
    ]);
