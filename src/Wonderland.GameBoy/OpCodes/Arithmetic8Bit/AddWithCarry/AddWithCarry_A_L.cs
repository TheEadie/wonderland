namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record AddWithCarry_A_L() : OpCode(
    0x8D,
    "ADC A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.L);
                return true;
            }
    ]);
