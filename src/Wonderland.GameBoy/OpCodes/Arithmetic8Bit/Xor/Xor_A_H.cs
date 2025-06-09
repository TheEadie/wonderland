namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_H() : OpCode(
    0xAC,
    "XOR A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Xor(r, r.H);
                return true;
            }
    ]);
