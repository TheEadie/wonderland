namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Pop_DE() : OpCode(
    0xD1,
    "POP DE",
    1,
    12,
    [
        (r, m, _) =>
            {
                r.E = m.GetMemory(r.SP++);
                return false;
            },
        (r, m, _) =>
            {
                r.D = m.GetMemory(r.SP++);
                return false;
            },
        (_, _, _) => true
    ]);
