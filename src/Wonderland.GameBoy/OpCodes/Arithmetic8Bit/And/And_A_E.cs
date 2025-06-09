namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_E() : OpCode(
    0xA3,
    "AND A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.E);
                return true;
            }
    ]);
