namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_B() : OpCode(
    0xA8,
    "XOR A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.B);
                return true;
            }
    ]);
