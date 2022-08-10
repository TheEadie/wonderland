using System.Diagnostics;

namespace Wonderland.Chip8;

public class Emulator
{
    private readonly byte[] _memory;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;
    private readonly ConsoleScreen _screen;
    private readonly List<TimeSpan> _actual60HzTimes;
    private readonly List<TimeSpan> _actual1KHzTimes;

    public Emulator()
    {
        _memory = new byte[4096];
        _gpu = new Gpu();
        _cpu = new Cpu(_memory, _gpu);
        _screen = new ConsoleScreen(_gpu);
        _actual60HzTimes = new List<TimeSpan>();
        _actual1KHzTimes = new List<TimeSpan>();
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
            var avgFrame = _actual60HzTimes.Average(x => x.TotalMilliseconds);
            var avgCpuTick = _actual1KHzTimes.Average(x => x.TotalMilliseconds);
            _screen.UpdateStats((int)(1000 / avgFrame), (int)(1000 / avgCpuTick));
            _actual60HzTimes.Clear();
            _actual1KHzTimes.Clear();
        }
    }

    private async Task Run60Hz(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var ticks60Hz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);
        var timer = new PeriodicTimer(ticks60Hz);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            _screen.DrawFrame();
            _actual60HzTimes.Add(stopwatch.Elapsed);
            stopwatch.Restart();
        }
    }
    
    private async Task Run1KHz(CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var ticks1KHz = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 1000);
        var timer = new PeriodicTimer(ticks1KHz);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            _cpu.Step();
            _actual1KHzTimes.Add(stopwatch.Elapsed);
            stopwatch.Restart();
        }
    }
}