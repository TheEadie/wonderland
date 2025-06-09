namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_H() : OpCode(
    0x9C,
    "SBC A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.SubWithCarry(r, r.H);
                return true;
            }
    ]);
