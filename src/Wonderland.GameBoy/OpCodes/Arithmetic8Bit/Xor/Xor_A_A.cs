namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_A() : OpCode(
    0xAF,
    "XOR A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.A);
                return true;
            }
    ]);
