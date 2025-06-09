namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_HL_A() : OpCode(
    0x77,
    "LD (HL), A",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.A);
                return false;
            },
        (_, _, _) => true
    ]);
