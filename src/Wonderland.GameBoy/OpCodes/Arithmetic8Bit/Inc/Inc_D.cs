namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_D() : OpCode(
    0x14,
    "INC D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.D & 0b_0000_1111) == 0b_0000_1111;
                r.D++;
                r.FlagZ = r.D == 0;
                r.FlagN = false;
                return true;
            }
    ]);
