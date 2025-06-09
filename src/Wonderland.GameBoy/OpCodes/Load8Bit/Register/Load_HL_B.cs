namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_HL_B() : OpCode(
    0x70,
    "LD (HL), B",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.B);
                return false;
            },
        (_, _, _) => true
    ]);
