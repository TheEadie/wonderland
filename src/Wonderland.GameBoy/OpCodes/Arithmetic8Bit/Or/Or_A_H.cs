namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_H() : OpCode(
    0xB4,
    "OR A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.H);
                return true;
            }
    ]);
