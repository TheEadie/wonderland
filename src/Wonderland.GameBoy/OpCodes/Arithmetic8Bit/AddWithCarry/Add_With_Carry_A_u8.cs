namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.AddWithCarry;

public record Add_With_Carry_A_u8() : OpCode(
    0xCE,
    "ADC A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.AddWithCarry(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
