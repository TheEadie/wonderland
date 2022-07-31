namespace Wonderland.Chip8;

public class Emulator
{
    private readonly byte[] _memory;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;
    
    public Emulator()
    {
        _memory = new byte[4096];
        _gpu = new Gpu();
        _cpu = new Cpu(_memory, _gpu);
    }

    public void Load(string pathToRom)
    {
        var rom = File.ReadAllBytes(pathToRom);
        rom.CopyTo(_memory, 512);
    }

    public void Run()
    {
        Console.WriteLine("Running...");
        _cpu.Step();
        _cpu.Step();
        _cpu.Step();
        _cpu.Step();
        _cpu.Step();
    }
}