namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_B() : OpCode(
    0x98,
    "SBC A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.SubWithCarry(r, r.B);
                return true;
            }
    ]);
