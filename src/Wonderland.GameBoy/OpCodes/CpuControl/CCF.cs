namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record CCF() : OpCode(
    0xCF,
    "CCF",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagC = !r.FlagC;
                r.FlagH = false;
                r.FlagN = false;
                return true;
            }
    ]);
