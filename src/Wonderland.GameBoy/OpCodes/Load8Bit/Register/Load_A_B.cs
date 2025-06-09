namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_A_B() : OpCode(
    0x78,
    "LD A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.B;
                return true;
            }
    ]);
