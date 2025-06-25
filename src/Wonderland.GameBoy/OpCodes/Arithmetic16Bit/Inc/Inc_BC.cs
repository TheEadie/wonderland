namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Inc;

public record Inc_BC() : OpCode(
    0x03,
    "INC BC",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.BC++;
                return true;
            }
    ]);
