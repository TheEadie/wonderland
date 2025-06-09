namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_A() : OpCode(
    0x3C,
    "INC A",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.A & 0b_0000_1111) == 0b_0000_1111;
                r.A++;
                r.FlagZ = r.A == 0;
                r.FlagN = false;
                return true;
            }
    ]);
