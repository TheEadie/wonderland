namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record AddWithCarry_A_E() : OpCode(
    0x8B,
    "ADC A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.E);
                return true;
            }
    ]);
