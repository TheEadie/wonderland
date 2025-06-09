namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_L() : OpCode(
    0x2C,
    "INC L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.L & 0b_0000_1111) == 0b_0000_1111;
                r.L++;
                r.FlagZ = r.L == 0;
                r.FlagN = false;
                return true;
            }
    ]);
