namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_A() : OpCode(
    0xA7,
    "AND A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.A);
                return true;
            }
    ]);
