namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_E() : OpCode(
    0x83,
    "ADD A, E",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.E);
                return true;
            }
    ]);
