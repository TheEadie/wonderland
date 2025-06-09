namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_HL() : OpCode(
    0x7E,
    "LD A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
