namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record Stop() : OpCode(0x10, "STOP", 1, 4, [(_, _, _) => throw new NotImplementedException()]);
