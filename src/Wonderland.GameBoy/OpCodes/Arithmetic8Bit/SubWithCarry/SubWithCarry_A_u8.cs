namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.SubWithCarry;

public record SubWithCarry_A_u8() : OpCode(
    0xDE,
    "SBC A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.SubWithCarry(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
