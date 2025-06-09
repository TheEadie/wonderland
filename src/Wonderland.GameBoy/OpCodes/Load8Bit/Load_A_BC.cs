namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_BC() : OpCode(
    0x0A,
    "LD A, (BC)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.BC);
                return false;
            },
        (_, _, _) => true
    ]);
