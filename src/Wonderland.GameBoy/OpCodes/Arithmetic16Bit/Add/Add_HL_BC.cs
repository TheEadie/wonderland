namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Add;

public record Add_HL_BC() : OpCode(
    0x09,
    "ADD HL, BC",
    1,
    8,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddToHl(r, r.BC);
                return false;
            },
        (_, _, _) => true
    ]);
