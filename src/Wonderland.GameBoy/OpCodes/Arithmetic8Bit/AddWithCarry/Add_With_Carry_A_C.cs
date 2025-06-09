namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record Add_With_Carry_A_C() : OpCode(
    0x89,
    "ADC A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.C);
                return true;
            }
    ]);
