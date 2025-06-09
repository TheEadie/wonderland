namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_H() : OpCode(
    0x25,
    "DEC H",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagH = (r.H & 0b_0000_1111) == 0b_0000_0000;
                r.H--;
                r.FlagZ = r.H == 0;
                r.FlagN = true;
                return true;
            }
    ]);
