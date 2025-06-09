namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_D() : OpCode(
    0x15,
    "DEC D",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.D & 0b_0000_1111) == 0b_0000_0000;
                r.D--;
                r.FlagZ = r.D == 0;
                r.FlagN = true;
                return true;
            }
    ]);
