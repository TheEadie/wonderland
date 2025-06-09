namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_A_D() : OpCode(
    0x7A,
    "LD A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.D;
                return true;
            }
    ]);
