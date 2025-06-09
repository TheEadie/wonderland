namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_D() : OpCode(
    0xB2,
    "OR A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.D);
                return true;
            }
    ]);
