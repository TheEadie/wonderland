namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_B_HL() : OpCode(
    0x46,
    "LD B, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.B = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
