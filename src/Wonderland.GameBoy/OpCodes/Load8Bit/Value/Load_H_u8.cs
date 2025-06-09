namespace Wonderland.GameBoy.OpCodes.Load8Bit.Value;

public record Load_H_u8() : OpCode(
    0x26,
    "LD H, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.H = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
