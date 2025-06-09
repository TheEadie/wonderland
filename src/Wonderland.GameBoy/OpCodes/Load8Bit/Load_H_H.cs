namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_H_H() : OpCode(
    0x64,
    "LD H, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.H = r.H;
                return true;
            }
    ]);

