namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_H() : OpCode(
    0x24,
    "INC H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.H & 0b_0000_1111) == 0b_0000_1111;
                r.H++;
                r.FlagZ = r.H == 0;
                r.FlagN = false;
                return true;
            }
    ]);
