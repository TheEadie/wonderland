namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_A() : OpCode(
    0x97,
    "SUB A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Sub(r, r.A);
                return true;
            }
    ]);
