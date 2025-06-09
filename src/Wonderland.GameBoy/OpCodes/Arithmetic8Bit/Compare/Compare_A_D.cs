namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_D() : OpCode(
    0xBA,
    "CP A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.D);
                return true;
            }
    ]);
