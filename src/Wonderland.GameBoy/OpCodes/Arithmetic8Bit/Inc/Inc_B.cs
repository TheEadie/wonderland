namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_B() : OpCode(
    0x04,
    "INC B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.B & 0b_0000_1111) == 0b_0000_1111;
                r.B++;
                r.FlagZ = r.B == 0;
                r.FlagN = false;
                return true;
            }
    ]);
