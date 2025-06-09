namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_A_E() : OpCode(
    0x7B,
    "LD A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = r.E;
                return true;
            }
    ]);
