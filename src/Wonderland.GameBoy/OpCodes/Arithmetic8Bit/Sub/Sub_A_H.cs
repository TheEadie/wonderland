namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_H() : OpCode(
    0x94,
    "SUB A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.H);
                return true;
            }
    ]);
