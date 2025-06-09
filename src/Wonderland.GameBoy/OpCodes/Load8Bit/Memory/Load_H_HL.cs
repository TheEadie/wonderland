namespace Wonderland.GameBoy.OpCodes.Load8Bit.Memory;

public record Load_H_HL() : OpCode(
    0x66,
    "LD H, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.H = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
