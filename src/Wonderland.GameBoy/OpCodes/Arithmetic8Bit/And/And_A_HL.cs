namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_HL() : OpCode(
    0xA6,
    "AND A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.And(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
