namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_E_HL() : OpCode(
    0x5E,
    "LD E, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.E = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
