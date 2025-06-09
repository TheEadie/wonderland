namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_C_A() : OpCode(
    0x4F,
    "LD C, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.A;
                return true;
            }
    ]);
