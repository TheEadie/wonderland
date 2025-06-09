namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_H() : OpCode(
    0x84,
    "ADD A, H",
    1,
    4,
    [
        (r, _, _) =>
            {
                OpCodeHandler.Add(r, r.H);
                return true;
            }
    ]);
