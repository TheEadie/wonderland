namespace Wonderland.GameBoy.OpCodes.Interrupts;

public record INT(InterruptSource Source) : OpCode(
    0x00,
    $"INT {Source.ToString()}",
    1,
    20,
    [
        (_, _, i) =>
            {
                i.DisableInterrupts();
                return false;
            },
        (_, _, _) => false,
        (r, m, _) =>
            {
                m.WriteMemory(--r.SP, (byte)(r.PC >> 8));
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(--r.SP, (byte)r.PC);
                return false;
            },
        (r, _, i) =>
            {
                i.ClearInterrupt(Source);
                r.PC = (ushort)(0x40 + (int)Source * 8);
                return true;
            }
    ]);
