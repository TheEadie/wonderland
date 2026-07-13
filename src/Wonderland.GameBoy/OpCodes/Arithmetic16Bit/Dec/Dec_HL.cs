namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Dec;

public record Dec_HL() : OpCode(
    0x2B,
    "DEC HL",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.HL--;
                return false;
            },
        (_, _, _) => true
    ]);
