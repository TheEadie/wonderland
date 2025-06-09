namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_L() : OpCode(
    0xB5,
    "OR A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.L);
                return true;
            }
    ]);
