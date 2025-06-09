namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_B() : OpCode(
    0xA0,
    "AND A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.B);
                return true;
            }
    ]);
