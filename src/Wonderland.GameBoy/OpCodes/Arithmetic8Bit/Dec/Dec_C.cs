namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_C() : OpCode(
    0x0D,
    "DEC C",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.C & 0b_0000_1111) == 0b_0000_0000;
                r.C--;
                r.FlagZ = r.C == 0;
                r.FlagN = true;
                return true;
            }
    ]);
