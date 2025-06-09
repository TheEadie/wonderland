namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_u8() : OpCode(
    0xC6,
    "ADD A, u8",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Add(r, m.GetMemory(r.PC++));
                return false;
            },
        (_, _, _) => true
    ]);
