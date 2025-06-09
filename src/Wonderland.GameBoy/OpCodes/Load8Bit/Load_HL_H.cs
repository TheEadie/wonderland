namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_HL_H() : OpCode(
    0x74,
    "LD (HL), H",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.H);
                return false;
            },
        (_, _, _) => true
    ]);
