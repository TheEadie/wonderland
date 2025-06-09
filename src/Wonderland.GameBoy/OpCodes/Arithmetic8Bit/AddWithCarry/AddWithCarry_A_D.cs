namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record AddWithCarry_A_D() : OpCode(
    0x8A,
    "ADC A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.D);
                return true;
            }
    ]);
