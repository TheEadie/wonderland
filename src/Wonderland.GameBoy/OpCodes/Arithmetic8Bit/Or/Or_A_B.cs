namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_B() : OpCode(
    0xB0,
    "OR A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.B);
                return true;
            }
    ]);
