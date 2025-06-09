namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_HL() : OpCode(
    0xAE,
    "XOR A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Xor(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
