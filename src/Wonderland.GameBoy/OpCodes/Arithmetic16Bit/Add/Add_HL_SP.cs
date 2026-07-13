namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Add;

public record Add_HL_SP() : OpCode(
    0x39,
    "ADD HL, SP",
    1,
    8,
    [
        (r, _, _) =>
            {
                OpCodeHandler.AddToHl(r, r.SP);
                return false;
            },
        (_, _, _) => true
    ]);
