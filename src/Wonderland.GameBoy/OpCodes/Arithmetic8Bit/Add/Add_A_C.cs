namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_C() : OpCode(
    0x81,
    "ADD A, C",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.C);
                return true;
            }
    ]);
