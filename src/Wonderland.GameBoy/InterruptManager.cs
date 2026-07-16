using System.Numerics;
using Wonderland.GameBoy.OpCodes;
using Wonderland.GameBoy.OpCodes.Interrupts;

namespace Wonderland.GameBoy;

public class InterruptManager
{
    public bool HaltCpu { get; set; }
    public bool HaltBug { get; set; }
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

        return InterruptsEnabled && AnyPending() ? new INT(HighestPrioritySource()) : null;
    }

    public void RequestHalt()
    {
        if (!InterruptsEnabled && AnyPending())
        {
            HaltBug = true;
        }
        else
        {
            HaltCpu = true;
        }
    }

    public bool AnyPending() => (byte)(IE & IF & 0x1F) != 0;

    private InterruptSource HighestPrioritySource() => (InterruptSource)BitOperations.TrailingZeroCount((byte)(IE & IF & 0x1F));
}
