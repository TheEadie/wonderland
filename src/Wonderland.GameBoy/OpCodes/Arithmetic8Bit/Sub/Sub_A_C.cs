namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_C() : OpCode(
    0x91,
    "SUB A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.C);
                return true;
            }
    ]);
