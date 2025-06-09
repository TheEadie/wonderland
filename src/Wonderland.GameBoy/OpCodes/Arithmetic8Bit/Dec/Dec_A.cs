namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_A() : OpCode(
    0x3D,
    "DEC A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.A & 0b_0000_1111) == 0b_0000_0000;
                r.A--;
                r.FlagZ = r.A == 0;
                r.FlagN = true;
                return true;
            }
    ]);
