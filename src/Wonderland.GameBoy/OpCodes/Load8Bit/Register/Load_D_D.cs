namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_D_D() : OpCode(
    0x52,
    "LD D, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.D;
                return true;
            }
    ]);
