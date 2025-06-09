namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_E() : OpCode(
    0x1C,
    "INC E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.E & 0b_0000_1111) == 0b_0000_1111;
                r.E++;
                r.FlagZ = r.E == 0;
                r.FlagN = false;
                return true;
            }
    ]);
