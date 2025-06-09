namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Sub;

public record Sub_A_HL() : OpCode(
    0x96,
    "SUB A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Sub(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
