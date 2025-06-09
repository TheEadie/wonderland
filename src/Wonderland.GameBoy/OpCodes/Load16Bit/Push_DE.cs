namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Push_DE() : OpCode(
    0xD5,
    "PUSH DE",
    1,
    16,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.D);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.E);
                return false;
            },
        (_, _, _) => true
    ]);
