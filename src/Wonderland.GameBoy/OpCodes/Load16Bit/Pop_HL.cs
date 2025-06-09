namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Pop_HL() : OpCode(
    0xE1,
    "POP HL",
    1,
    12,
    [
        (r, m, _) =>
            {
                r.L = m.GetMemory(r.SP++);
                return false;
            },
        (r, m, _) =>
            {
                r.H = m.GetMemory(r.SP++);
                return false;
            },
        (_, _, _) => true
    ]);
