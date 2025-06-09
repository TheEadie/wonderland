namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record AddWithCarry_A_HL() : OpCode(
    0x8E,
    "ADC A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.AddWithCarry(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
