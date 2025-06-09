namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_E() : OpCode(
    0xBB,
    "CP A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.E);
                return true;
            }
    ]);
