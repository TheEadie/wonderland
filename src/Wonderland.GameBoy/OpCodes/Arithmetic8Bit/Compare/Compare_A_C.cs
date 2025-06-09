namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_C() : OpCode(
    0xB9,
    "CP A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.C);
                return true;
            }
    ]);
