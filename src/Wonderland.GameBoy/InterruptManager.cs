namespace Wonderland.GameBoy;

public class InterruptManager
{
    public bool InterruptsEnabled { get; private set; }

    private int _imeEnableDelay;

    public void DisableInterrupts()
    {
        InterruptsEnabled = false;
        _imeEnableDelay = 0;
    }

    public void EnableInterruptsWithDelay()
    {
        _imeEnableDelay = 2;
    }

    public void EnableInterrupts()
    {
        InterruptsEnabled = true;
        _imeEnableDelay = 0;
    }

    public void OnInstructionComplete()
    {
        if (_imeEnableDelay > 0)
        {
            _imeEnableDelay--;
            if (_imeEnableDelay == 0)
            {
                InterruptsEnabled = true;
            }
        }
    }
}
