namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record Add_With_Carry_A_L() : OpCode(
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
