namespace Wonderland.GameBoy.OpCodes.RotateShift;

public record RRCA() : OpCode(
    0x0F,
    "RRCA",
    1,
    4,
    [
        (r, _, _) =>
            {
                var carry = (r.A & 0b_0000_0001) != 0;
                r.A = (byte)((r.A >> 1) | (carry ? 0b_1000_0000 : 0));
                r.FlagZ = false;
                r.FlagN = false;
                r.FlagH = false;
                r.FlagC = carry;
                return true;
            }
    ]);
