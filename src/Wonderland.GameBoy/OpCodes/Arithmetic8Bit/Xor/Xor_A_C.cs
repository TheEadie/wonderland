namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_C() : OpCode(
    0xA9,
    "XOR A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.C);
                return true;
            }
    ]);
