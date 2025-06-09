namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_C_C() : OpCode(
    0x49,
    "LD C, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.C;
                return true;
            }
    ]);
