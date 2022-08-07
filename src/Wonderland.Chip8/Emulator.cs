namespace Wonderland.Chip8;

public class Emulator
{
    private readonly byte[] _memory;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;
    private readonly ConsoleScreen _screen;

    public Emulator()
    {
        _memory = new byte[4096];
        _gpu = new Gpu();
        _cpu = new Cpu(_memory, _gpu);
        _screen = new ConsoleScreen(_gpu);

        _memory[0x50] = 0xF0;
    }

    public void Load(string pathToRom)
    {
        var rom = File.ReadAllBytes(pathToRom);
        rom.CopyTo(_memory, 512);
    }

    public void Run()
    {
        Console.WriteLine("Running...");
        _ = _screen.Draw(CancellationToken.None);
        while (true)
        {
            _cpu.Step();
            //_cpu.PrintDebug();
            //_gpu.PrintDebug();

            //Console.ReadKey();
        }
    }
}