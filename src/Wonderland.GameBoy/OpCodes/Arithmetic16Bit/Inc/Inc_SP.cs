namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Inc;

public record Inc_SP() : OpCode(
    0x33,
    "INC SP",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.SP++;
                return false;
            },
        (_, _, _) => true
    ]);
