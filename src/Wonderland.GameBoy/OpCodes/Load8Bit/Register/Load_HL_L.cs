namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_HL_L() : OpCode(
    0x75,
    "LD (HL), L",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.L);
                return false;
            },
        (_, _, _) => true
    ]);
