namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Pop_AF() : OpCode(
    0xF1,
    "POP AF",
    1,
    12,
    [
        (r, m, _) =>
            {
                r.F = m.GetMemory(r.SP++);
                return false;
            },
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.SP++);
                return false;
            },
        (_, _, _) => true
    ]);
