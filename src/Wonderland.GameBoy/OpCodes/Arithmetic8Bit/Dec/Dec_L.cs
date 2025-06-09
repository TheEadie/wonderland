namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_L() : OpCode(
    0x2D,
    "DEC L",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.L & 0b_0000_1111) == 0b_0000_0000;
                r.L--;
                r.FlagZ = r.L == 0;
                r.FlagN = true;
                return true;
            }
    ]);
