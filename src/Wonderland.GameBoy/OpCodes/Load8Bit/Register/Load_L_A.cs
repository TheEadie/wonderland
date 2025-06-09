namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_L_A() : OpCode(
    0x6F,
    "LD L, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.L = r.A;
                return true;
            }
    ]);
