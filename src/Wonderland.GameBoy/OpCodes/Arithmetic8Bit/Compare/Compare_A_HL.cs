namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_HL() : OpCode(
    0xBE,
    "CP A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Compare(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
