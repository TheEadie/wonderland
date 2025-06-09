namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_C() : OpCode(
    0x0C,
    "INC C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.C & 0b_0000_1111) == 0b_0000_1111;
                r.C++;
                r.FlagZ = r.C == 0;
                r.FlagN = false;
                return true;
            }
    ]);
