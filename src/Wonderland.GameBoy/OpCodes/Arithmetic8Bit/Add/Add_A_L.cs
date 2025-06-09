namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_L() : OpCode(
    0x85,
    "ADD A, L",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.L);
                return true;
            }
    ]);
