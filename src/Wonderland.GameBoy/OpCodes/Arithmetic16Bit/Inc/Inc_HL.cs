namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Inc;

public record Inc_HL() : OpCode(
    0x23,
    "INC HL",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.HL++;
                return false;
            },
        (_, _, _) => true
    ]);
