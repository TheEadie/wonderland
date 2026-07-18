namespace Wonderland.GameBoy;

public class Timer(InterruptManager interruptManager)
{
    public byte DIV
    {
        get => (byte)(_systemCounter >> 8);
        set
        {
            _systemCounter = 0;
            CheckForEdge();
        }
    }

    public byte TIMA
    {
        get => _tima;
        set
        {
            if (_overflowPending)
            {
                _overflowPending = false;
                _tima = value;
                return;
            }

            if (_reloading)
            {
                return;
            }

            _tima = value;
        }
    }

    public byte TMA
    {
        get;
        set
        {
            field = value;
            if (_reloading)
            {
                _tima = value;
            }
        }
    }

    public byte TAC
    {
        get;
        set
        {
            var oldWatch = CurrentWatchValue();
            field = (byte)(value & 0x07);
            var newWatch = CurrentWatchValue();
            if (oldWatch && !newWatch)
            {
                IncrementTIMA();
            }
            _lastWatchValue = newWatch;
        }
    }

    public bool TimerEnabled => (TAC & 0x04) != 0;
    public TimerFrequency TimerFrequency => (TimerFrequency)(TAC & 0x03);

    private readonly int[] _watchBit =
    [
        9,
        3,
        5,
        7
    ];

    private ushort _systemCounter;
    private bool _lastWatchValue;
    private bool _overflowPending;
    private bool _reloading;
    private byte _tima;

    public void Step()
    {
        _reloading = false;

        if (_overflowPending)
        {
            _tima = TMA;
            _overflowPending = false;
            _reloading = true;
            interruptManager.RequestInterrupt(InterruptSource.Timer);
        }
        _systemCounter += 4;
        CheckForEdge();
    }

    private void CheckForEdge()
    {
        var current = CurrentWatchValue();

        if (_lastWatchValue && !current)
        {
            IncrementTIMA();
        }

        _lastWatchValue = current;
    }

    private bool CurrentWatchValue()
    {
        var watch = _watchBit[(int)TimerFrequency];
        var value = ((_systemCounter >> watch) & 1) != 0;
        return value && TimerEnabled;
    }

    private void IncrementTIMA()
    {
        if (_tima == 0xFF)
        {
            _tima = 0x00;
            _overflowPending = true;
        }
        else
        {
            _tima++;
        }
    }
}

public enum TimerFrequency
{
    MCycles256 = 0,
    MCycles4 = 1,
    MCycles16 = 2,
    MCycles64 = 3
}
