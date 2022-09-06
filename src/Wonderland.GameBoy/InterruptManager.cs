namespace Wonderland.GameBoy;

public class InterruptManager
{
    public bool InterruptsEnabled { get; private set; }

    private bool _enableOnNextCycle;

    public void DisableInterrupts()
    {
        InterruptsEnabled = false;
        _enableOnNextCycle = false;
    }

    public void EnableInterruptsWithDelay()
    {
        _enableOnNextCycle = true;
    }

    public void Step()
    {
        if (_enableOnNextCycle)
        {
            InterruptsEnabled = true;
            _enableOnNextCycle = false;
        }
    }
}