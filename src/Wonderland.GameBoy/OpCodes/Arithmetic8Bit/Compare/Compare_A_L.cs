namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_L() : OpCode(
    0xBD,
    "CP A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.L);
                return true;
            }
    ]);
