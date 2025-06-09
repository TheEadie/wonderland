namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Push_HL() : OpCode(
    0xE5,
    "PUSH HL",
    1,
    16,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.H);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.L);
                return false;
            },
        (_, _, _) => true
    ]);
