namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_A() : OpCode(
    0x9F,
    "SBC A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.SubWithCarry(r, r.A);
                return true;
            }
    ]);
