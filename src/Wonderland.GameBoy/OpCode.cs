namespace Wonderland.GameBoy;

public record OpCode(byte Value, string Description, int Length, int ClockCycles, IEnumerable<Action> Steps)
{
    public int MachineCycles => ClockCycles / 4;
}