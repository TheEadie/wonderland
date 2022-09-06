namespace Wonderland.GameBoy.OpCodes;

public record OpCode(byte Value, string Description, int Length, int ClockCycles,
    Func<Registers, Mmu, InterruptManager, bool>[] Steps)
{
    public int MachineCycles => ClockCycles / 4;
}