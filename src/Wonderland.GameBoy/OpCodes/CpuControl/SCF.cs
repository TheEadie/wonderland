namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record SCF() : OpCode(
    0xC7,
    "SCF",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.FlagC = true;
                r.FlagH = false;
                r.FlagN = false;
                return true;
            }
    ]);
