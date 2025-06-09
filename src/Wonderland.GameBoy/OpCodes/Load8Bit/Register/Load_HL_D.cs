namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_HL_D() : OpCode(
    0x72,
    "LD (HL), D",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.D);
                return false;
            },
        (_, _, _) => true
    ]);
