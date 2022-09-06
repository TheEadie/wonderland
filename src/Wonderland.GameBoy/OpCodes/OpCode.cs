namespace Wonderland.GameBoy.OpCodes;

public record OpCode(byte Value, string Description, int Length, int ClockCycles, Action<Registers, Mmu>[] Steps)
{
    public int MachineCycles => ClockCycles / 4;
}