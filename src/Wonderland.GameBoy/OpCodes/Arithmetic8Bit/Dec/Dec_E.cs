namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_E() : OpCode(
    0x1D,
    "DEC E",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.E & 0b_0000_1111) == 0b_0000_0000;
                r.E--;
                r.FlagZ = r.E == 0;
                r.FlagN = true;
                return true;
            }
    ]);
