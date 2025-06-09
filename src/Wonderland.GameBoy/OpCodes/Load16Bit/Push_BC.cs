namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Push_BC() : OpCode(
    0xC5,
    "PUSH BC",
    1,
    16,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.B);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.C);
                return false;
            },
        (_, _, _) => true
    ]);
