namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_H() : OpCode(
    0xA4,
    "AND A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.And(r, r.H);
                return true;
            }
    ]);
