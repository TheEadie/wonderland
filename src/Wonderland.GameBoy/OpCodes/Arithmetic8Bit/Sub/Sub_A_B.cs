namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_B() : OpCode(
    0x90,
    "SUB A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.B);
                return true;
            }
    ]);
