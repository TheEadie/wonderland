namespace Wonderland.GameBoy.OpCodes.Load8Bit.Memory;

public record Load_C_HL() : OpCode(
    0x4E,
    "LD C, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.C = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
