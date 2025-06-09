namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_u8() : OpCode(
    0x3E,
    "LD A, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
