namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_B() : OpCode(
    0x80,
    "ADD A, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.B);
                return true;
            }
    ]);
