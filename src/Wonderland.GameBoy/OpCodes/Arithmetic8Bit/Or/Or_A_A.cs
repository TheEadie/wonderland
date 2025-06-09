namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_A() : OpCode(
    0xB7,
    "OR A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Or(r, r.A);
                return true;
            }
    ]);
