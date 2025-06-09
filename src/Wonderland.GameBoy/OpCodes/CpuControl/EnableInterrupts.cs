namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record EnableInterrupts() : OpCode(
    0xFB,
    "EI",
    1,
    4,
    [
        (_, _, i) =>
            {
                i.EnableInterruptsWithDelay();
                return true;
            }
    ]);
