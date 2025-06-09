namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_L() : OpCode(
    0xA5,
    "AND A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.L);
                return true;
            }
    ]);
