namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_D() : OpCode(
    0x9A,
    "SBC A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.SubWithCarry(r, r.D);
                return true;
            }
    ]);
