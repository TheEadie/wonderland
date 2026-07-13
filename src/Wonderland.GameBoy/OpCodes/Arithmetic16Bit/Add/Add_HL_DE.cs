namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Add;

public record Add_HL_DE() : OpCode(
    0x19,
    "ADD HL, DE",
    1,
    8,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddToHl(r, r.DE);
                return false;
            },
        (_, _, _) => true
    ]);
