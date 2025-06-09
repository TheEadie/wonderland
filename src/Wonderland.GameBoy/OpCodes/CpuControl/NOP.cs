namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record NOP() : OpCode(0x00, "NOP", 1, 4, [(_, _, _) => true]);
