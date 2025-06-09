namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Or;

public record Or_A_HL() : OpCode(
    0xB6,
    "OR A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Or(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
