namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_HL_INC_A() : OpCode(
    0x22,
    "LD (HL+), A",
    2,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.A);
                return false;
            },
        (r, _, _) =>
            {
                r.HL++;
                return true;
            }
    ]);
