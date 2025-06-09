namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Xor;

public record Xor_A_u8() : OpCode(
    0xEE,
    "XOR A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Xor(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
