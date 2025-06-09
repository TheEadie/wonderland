namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record AddWithCarry_A_H() : OpCode(
    0x8C,
    "ADC A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.H);
                return true;
            }
    ]);
