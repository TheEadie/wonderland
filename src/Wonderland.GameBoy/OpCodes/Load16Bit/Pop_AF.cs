namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Pop_AF() : OpCode(
    0xF1,
    "POP AF",
    1,
    12,
    [
        (r, m, _) =>
            {
                r.F = (byte)(m.GetMemory(r.SP++) & 0xF0);
                return false;
            },
        (r, m, _) =>
            {
                r.A = m.GetMemory(r.SP++);
                return false;
            },
        (_, _, _) => true
    ]);
