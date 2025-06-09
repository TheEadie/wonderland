namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record Add_With_Carry_A_A() : OpCode(
    0x8F,
    "ADC A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddWithCarry(r, r.A);
                return true;
            }
    ]);
