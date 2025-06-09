namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_D_C() : OpCode(
    0x51,
    "LD D, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.C;
                return true;
            }
    ]);
