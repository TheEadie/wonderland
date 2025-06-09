namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Pop_BC() : OpCode(
    0xC1,
    "POP BC",
    1,
    12,
    [
        (r, m, _) =>
            {
                r.C = m.GetMemory(r.SP++);
                return false;
            },
        (r, m, _) =>
            {
                r.B = m.GetMemory(r.SP++);
                return false;
            },
        (_, _, _) => true
    ]);
