namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_D_L() : OpCode(
    0x55,
    "LD D, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.L;
                return true;
            }
    ]);
