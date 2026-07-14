using System.Numerics;
using Wonderland.GameBoy.OpCodes;
using Wonderland.GameBoy.OpCodes.Interrupts;

namespace Wonderland.GameBoy;

public class InterruptManager
{
    public bool InterruptsEnabled { get; private set; }
    public byte IE { get; internal set; } = 0x00;
    public byte IF { get => (byte)(field | 0xE0); internal set => field = (byte)((value & 0x1F) | 0xE0); } = 0xE0;

    private int _enableDelay;


    public void DisableInterrupts()
    {
        InterruptsEnabled = false;
        _enableDelay = 0;
    }

    public void EnableInterruptsWithDelay()
    {
        _enableDelay = 2;
    }

    public void EnableInterrupts()
    {
        InterruptsEnabled = true;
        _enableDelay = 0;
    }

    public void RequestInterrupt(InterruptSource source)
    {
        IF |= (byte)(1 << (int)source);
    }

    public void ClearInterrupt(InterruptSource source)
    {
        IF &= (byte)~(1 << (int)source);
    }

    public OpCode? CheckAndDispatch()
    {
        if (_enableDelay > 0)
        {
            _enableDelay--;
            if (_enableDelay == 0)
            {
                InterruptsEnabled = true;
            }
        }

        var (pending, source) = PendingInterrupt();

        return pending ? new INT(source) : null;
    }

    private (bool, InterruptSource) PendingInterrupt()
    {
        var pending = (byte)(IE & IF & 0x1F);
        var anyPending = InterruptsEnabled && pending != 0;
        var highestPriority = (InterruptSource)BitOperations.TrailingZeroCount(pending);
        return (anyPending, highestPriority);
    }
}
