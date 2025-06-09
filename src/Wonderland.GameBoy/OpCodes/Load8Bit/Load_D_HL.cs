namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_D_HL() : OpCode(
    0x56,
    "LD D, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.D = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
