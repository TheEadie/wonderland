namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.And;

public record And_A_u8() : OpCode(
    0xE6,
    "AND A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.And(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
