namespace Wonderland.GameBoy.OpCodes.Load8Bit.Value;

public record Load_C_u8() : OpCode(
    0x0E,
    "LD C, u8",
    2,
    8,
    [
        (r, m, _) =>
            {
                r.C = m.GetMemory(r.PC++);
                return false;
            },
        (_, _, _) => true
    ]);
