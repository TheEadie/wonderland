namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_L() : OpCode(
    0x95,
    "SUB A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.L);
                return true;
            }
    ]);
