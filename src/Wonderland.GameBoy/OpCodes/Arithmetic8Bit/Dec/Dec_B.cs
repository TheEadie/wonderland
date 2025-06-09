namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_B() : OpCode(
    0x05,
    "DEC B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.B & 0b_0000_1111) == 0b_0000_0000;
                r.B--;
                r.FlagZ = r.B == 0;
                r.FlagN = true;
                return true;
            }
    ]);
