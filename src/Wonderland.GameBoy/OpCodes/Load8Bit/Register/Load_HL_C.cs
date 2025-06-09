namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_HL_C() : OpCode(
    0x71,
    "LD (HL), C",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.C);
                return false;
            },
        (_, _, _) => true
    ]);
