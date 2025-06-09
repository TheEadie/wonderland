namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_HL_DEC_A() : OpCode(
    0x32,
    "LD (HL-), A",
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
                r.HL--;
                return true;
            }
    ]);
