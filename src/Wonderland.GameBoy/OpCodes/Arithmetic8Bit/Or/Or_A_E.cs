namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_E() : OpCode(
    0xB3,
    "OR A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.E);
                return true;
            }
    ]);
