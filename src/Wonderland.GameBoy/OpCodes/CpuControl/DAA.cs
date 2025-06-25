namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record DAA() : OpCode(
    0x27,
    "DAA",
    1,
    4,
    [
        (r, _, _) =>
            {
                if (r.FlagN)
                {
                    if (r.FlagC)
                    {
                        r.A -= 0x60;
                    }

                    if (r.FlagH)
                    {
                        r.A -= 0x6;
                    }
                }
                else
                {
                    if (r.FlagC || r.A > 0x99)
                    {
                        r.A += 0x60;
                        r.FlagC = true;
                    }

                    if (r.FlagH || (r.A & 0xF) > 0x9)
                    {
                        r.A += 0x6;
                    }
                }

                r.FlagZ = r.A == 0;
                r.FlagH = false;
                return true;
            }
    ]);
