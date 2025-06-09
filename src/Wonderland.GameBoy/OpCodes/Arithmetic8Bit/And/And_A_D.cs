namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_D() : OpCode(
    0xA2,
    "AND A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.D);
                return true;
            }
    ]);
