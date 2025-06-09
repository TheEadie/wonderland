namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_D_B() : OpCode(
    0x50,
    "LD D, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.B;
                return true;
            }
    ]);
