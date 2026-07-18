namespace Wonderland.GameBoy;

public class Emulator
{
    public Stream SerialOutput { get; }

    private readonly InterruptManager _interruptManager;
    private readonly Timer _timer;
    private readonly Mmu _mmu;
    private readonly Cpu _cpu;

    public Emulator(Stream? serialOutput = null)
    {
        SerialOutput = serialOutput ?? new MemoryStream();
        _interruptManager = new InterruptManager();
        _timer = new Timer(_interruptManager);
        _mmu = new Mmu(SerialOutput, _interruptManager, _timer);
        _cpu = new Cpu(_mmu, _interruptManager);
    }

    public void Load(string romPath) => _mmu.LoadCart(romPath);

    public void Step()
    {
        _timer.Step();
        _cpu.Step();
    }
}
