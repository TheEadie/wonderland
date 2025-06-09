namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_H_A() : OpCode(
    0x67,
    "LD H, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.A;
                return true;
            }
    ]);
