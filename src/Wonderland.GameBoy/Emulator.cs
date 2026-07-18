namespace Wonderland.GameBoy;

public class Emulator
{
    public Registers Registers => _cpu.Registers;

    private readonly InterruptManager _interruptManager;
    private readonly Timer _timer;
    private readonly Mmu _mmu;
    private readonly Cpu _cpu;

    public Emulator(Stream? serialOutput = null)
    {
        _interruptManager = new InterruptManager();
        _timer = new Timer(_interruptManager);
        _mmu = new Mmu(serialOutput ?? new MemoryStream(), _interruptManager, _timer);
        _cpu = new Cpu(_mmu, _interruptManager);
    }

    public void Load(string romPath) => _mmu.LoadCart(romPath);

    public void Step()
    {
        _timer.Step();
        _cpu.Step();
    }
}
