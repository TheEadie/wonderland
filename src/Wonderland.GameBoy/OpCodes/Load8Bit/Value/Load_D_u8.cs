namespace Wonderland.GameBoy.OpCodes.Load8Bit.Value;

public record Load_D_u8() : OpCode(
    0x16,
    "LD D, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.D = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
