namespace Wonderland.GameBoy.OpCodes.Load8Bit.Value;

public record Load_B_u8() : OpCode(
    0x06,
    "LD B, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.B = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
