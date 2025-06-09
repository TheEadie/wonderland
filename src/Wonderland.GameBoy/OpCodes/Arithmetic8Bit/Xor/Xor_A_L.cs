namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_L() : OpCode(
    0xAD,
    "XOR A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.L);
                return true;
            }
    ]);
