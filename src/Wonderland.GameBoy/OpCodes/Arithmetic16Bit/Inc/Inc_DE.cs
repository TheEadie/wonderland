namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Inc;

public record Inc_DE() : OpCode(
    0x13,
    "INC DE",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.DE++;
                return true;
            }
    ]);
