namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Load_SP_HL() : OpCode(
    0xF9,
    "LD SP, HL",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.SP = r.HL;
                return false;
            },
        (_, _, _) => true
    ]);
