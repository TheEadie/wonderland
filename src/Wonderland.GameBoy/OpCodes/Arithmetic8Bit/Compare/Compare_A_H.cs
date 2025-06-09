namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Compare;

public record Compare_A_H() : OpCode(
    0xBC,
    "CP A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Compare(r, r.H);
                return true;
            }
    ]);
