namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_A_H() : OpCode(
    0x7C,
    "LD A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.H;
                return true;
            }
    ]);
