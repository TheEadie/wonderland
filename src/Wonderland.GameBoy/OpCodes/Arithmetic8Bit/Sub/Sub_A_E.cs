namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_E() : OpCode(
    0x93,
    "SUB A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.E);
                return true;
            }
    ]);
