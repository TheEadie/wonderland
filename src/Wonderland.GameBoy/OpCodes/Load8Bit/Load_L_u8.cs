namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_L_u8() : OpCode(
    0x2E,
    "LD L, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.L = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
