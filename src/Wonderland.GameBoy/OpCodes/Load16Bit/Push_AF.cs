namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Push_AF() : OpCode(
    0xF5,
    "PUSH AF",
    1,
    16,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.A);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(r.SP--, r.F);
                return false;
            },
        (_, _, _) => true
    ]);
