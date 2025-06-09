namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_HL() : OpCode(
    0x9E,
    "SBC A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.SubWithCarry(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
