namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_B() : OpCode(
    0xB8,
    "CP A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.B);
                return true;
            }
    ]);
