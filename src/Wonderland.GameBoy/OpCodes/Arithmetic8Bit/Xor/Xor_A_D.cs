namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_D() : OpCode(
    0xAA,
    "XOR A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.D);
                return true;
            }
    ]);
