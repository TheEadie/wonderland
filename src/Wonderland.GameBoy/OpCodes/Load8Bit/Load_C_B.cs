namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_C_B() : OpCode(
    0x48,
    "LD C, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.B;
                return true;
            }
    ]);
