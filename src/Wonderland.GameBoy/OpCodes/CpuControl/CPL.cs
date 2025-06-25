namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record CPL() : OpCode(
    0x2F,
    "CPL",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.A = (byte)(r.A ^ 0xFF);
                r.FlagN = true;
                r.FlagH = true;
                return true;
            }
    ]);
