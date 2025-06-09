namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_H_D() : OpCode(
    0x62,
    "LD H, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.D;
                return true;
            }
    ]);
