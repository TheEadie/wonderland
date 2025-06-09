namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_A_A() : OpCode(
    0x7F,
    "LD A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.A;
                return true;
            }
    ]);
