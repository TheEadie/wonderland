namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_B_A() : OpCode(
    0x47,
    "LD B, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.B = r.A;
                return true;
            }
    ]);
