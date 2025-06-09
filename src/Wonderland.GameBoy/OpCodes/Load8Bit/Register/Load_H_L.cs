namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_H_L() : OpCode(
    0x65,
    "LD H, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.L;
                return true;
            }
    ]);
