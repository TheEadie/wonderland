namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_u8() : OpCode(
    0xFE,
    "CP A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Compare(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
