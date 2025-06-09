namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_C() : OpCode(
    0xA1,
    "AND A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.C);
                return true;
            }
    ]);
