namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_D() : OpCode(
    0x82,
    "ADD A, D",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.D);
                return true;
            }
    ]);
