namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_D_A() : OpCode(
    0x57,
    "LD D, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.D = r.A;
                return true;
            }
    ]);
