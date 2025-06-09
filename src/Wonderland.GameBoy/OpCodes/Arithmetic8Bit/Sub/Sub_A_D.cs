namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_D() : OpCode(
    0x92,
    "SUB A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.D);
                return true;
            }
    ]);
