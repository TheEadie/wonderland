namespace Wonderland.GameBoy.OpCodes.RotateShift;

public record RRA() : OpCode(
    0x1F,
    "RRA",
    1,
    4,
    [
        (r, _, _) =>
            {
                var carry = (r.A & 0b_0000_0001) != 0;
                r.A = (byte)((r.A >> 1) | (r.FlagC ? 0b_1000_0000 : 0));
                r.FlagZ = false;
                r.FlagN = false;
                r.FlagH = false;
                r.FlagC = carry;
                return true;
            }
    ]);
