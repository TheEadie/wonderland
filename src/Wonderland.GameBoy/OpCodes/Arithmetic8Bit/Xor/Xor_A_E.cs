namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_E() : OpCode(
    0xAB,
    "XOR A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.E);
                return true;
            }
    ]);
