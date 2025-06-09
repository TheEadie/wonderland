namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_H_E() : OpCode(
    0x63,
    "LD H, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.E;
                return true;
            }
    ]);
