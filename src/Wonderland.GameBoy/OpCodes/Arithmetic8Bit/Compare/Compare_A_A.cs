namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_A() : OpCode(
    0xBF,
    "CP A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.A);
                return true;
            }
    ]);
