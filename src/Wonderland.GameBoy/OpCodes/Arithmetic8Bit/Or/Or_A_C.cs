namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_C() : OpCode(
    0xB1,
    "OR A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.C);
                return true;
            }
    ]);
