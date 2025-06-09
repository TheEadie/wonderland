namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_A() : OpCode(
    0x87,
    "ADD A, A",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.A);
                return true;
            }
    ]);
