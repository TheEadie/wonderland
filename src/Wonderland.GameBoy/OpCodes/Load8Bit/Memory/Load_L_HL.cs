namespace Wonderland.GameBoy.OpCodes.Load8Bit.Memory;

public record Load_L_HL() : OpCode(
    0x6E,
    "LD L, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.L = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
