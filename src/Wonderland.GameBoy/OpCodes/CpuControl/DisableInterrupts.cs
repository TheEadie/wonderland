namespace Wonderland.GameBoy.OpCodes.CpuControl;

public record DisableInterrupts() : OpCode(
    0xF3,
    "DI",
    1,
    4,
    [
        (_, _, i) =>
            {
                i.DisableInterrupts();
                return true;
            }
    ]);
