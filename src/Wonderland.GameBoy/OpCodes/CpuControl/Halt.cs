namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record Halt() : OpCode(0x76, "HALT", 1, 4, [(_, _, _) => throw new NotImplementedException()]);
