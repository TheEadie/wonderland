namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record AddWithCarry_A_B() : OpCode(
    0x88,
    "ADC A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.B);
                return true;
            }
    ]);
