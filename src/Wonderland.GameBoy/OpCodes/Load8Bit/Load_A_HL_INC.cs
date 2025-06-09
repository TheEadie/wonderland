namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_HL_INC() : OpCode(
    0x2A,
    "LD A, (HL+)",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.HL);
                return false;
            },
        (r, _, _) =>
            {
                r.HL++;
                return true;
            }
    ]);
