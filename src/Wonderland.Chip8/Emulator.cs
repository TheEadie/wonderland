using System.Diagnostics;

namespace Wonderland.Chip8;

public class Emulator
{
    private readonly byte[] _memory;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;
    private readonly ConsoleScreen _screen;
    private int _fps;
    private int _actual1KHz;

    public Emulator()
    {
        _memory = new byte[4096];
        _gpu = new Gpu();
        _cpu = new Cpu(_memory, _gpu);
        _screen = new ConsoleScreen(_gpu);
    }

    public void Load(string pathToRom)
    {
        var font = new byte[]
        {
            0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
            0x20, 0x60, 0x20, 0x20, 0x70, // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
            0x90, 0x90, 0xF0, 0x10, 0x10, // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
            0xF0, 0x10, 0x20, 0x40, 0x40, // 7
            0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90, // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
            0xF0, 0x80, 0x80, 0x80, 0xF0, // C
            0xE0, 0x90, 0x90, 0x90, 0xE0, // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
            0xF0, 0x80, 0xF0, 0x80, 0x80  // F
        };

        font.CopyTo(_memory, 0x50);

        var rom = File.ReadAllBytes(pathToRom);
        rom.CopyTo(_memory, 0x200);
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        _screen.Init();
        await Task.WhenAll(
            Run1Hz(cancellationToken),
            Run60Hz(cancellationToken),
            Run1KHz(cancellationToken));
    }

    private async Task Run1Hz(CancellationToken cancellationToken)
    {
        var ticker1Hz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond);
        var timer = new PeriodicTimer(ticker1Hz);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            _screen.UpdateStats(_fps, _actual1KHz);
            _fps = 0;
            _actual1KHz = 0;
        }
    }

    private async Task Run60Hz(CancellationToken cancellationToken)
    {
        var ticks60Hz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        var timer = new PeriodicTimer(ticks60Hz);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            _screen.DrawFrame();
            _fps++;
        }
    }

    private async Task Run1KHz(CancellationToken cancellationToken)
    {
        var ticks1KHz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 1000);
        var timer = new PeriodicTimer(ticks1KHz);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            _cpu.Step();
            _actual1KHz++;
        }
    }
}