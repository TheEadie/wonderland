namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_L() : OpCode(
    0x9D,
    "SBC A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.SubWithCarry(r, r.L);
                return true;
            }
    ]);
