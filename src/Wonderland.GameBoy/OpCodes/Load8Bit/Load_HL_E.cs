namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_HL_E() : OpCode(
    0x73,
    "LD (HL), E",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.E);
                return false;
            },
        (_, _, _) => true
    ]);
