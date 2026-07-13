namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Add;

public record Add_HL_HL() : OpCode(
    0x29,
    "ADD HL, HL",
    1,
    8,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddToHl(r, r.HL);
                return false;
            },
        (_, _, _) => true
    ]);
