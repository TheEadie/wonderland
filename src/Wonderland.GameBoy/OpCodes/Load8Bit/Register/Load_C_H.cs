namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_C_H() : OpCode(
    0x4C,
    "LD C, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.C = r.H;
                return true;
            }
    ]);
