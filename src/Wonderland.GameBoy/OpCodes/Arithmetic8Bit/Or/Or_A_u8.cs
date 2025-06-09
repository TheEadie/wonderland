namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_u8() : OpCode(
    0xF6,
    "OR A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Or(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
