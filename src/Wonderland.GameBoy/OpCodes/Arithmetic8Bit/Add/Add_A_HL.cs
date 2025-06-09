namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Add;

public record Add_A_HL() : OpCode(
    0x86,
    "ADD A, (HL)",
    1,
    8,
    [
        (r, m, _) =>
            {
                OpCodeHandler.Add(r, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
