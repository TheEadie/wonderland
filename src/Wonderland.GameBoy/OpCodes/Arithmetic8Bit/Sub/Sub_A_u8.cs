namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_u8() : OpCode(
    0xD6,
    "SUB A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Sub(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
