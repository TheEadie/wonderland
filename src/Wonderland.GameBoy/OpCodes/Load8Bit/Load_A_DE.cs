namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_DE() : OpCode(
    0x1A,
    "LD A, (DE)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.DE);
                return false;
            },
        (_, _, _) => true
    ]);
