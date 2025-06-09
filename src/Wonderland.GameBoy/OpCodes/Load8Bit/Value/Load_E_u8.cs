namespace Wonderland.GameBoy.OpCodes.Load8Bit.Value;

public record Load_E_u8() : OpCode(
    0x1E,
    "LD E, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.E = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
