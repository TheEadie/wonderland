using System.Diagnostics;

namespace Wonderland.Chip8;

public class Emulator
{
    private readonly byte[] _memory;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;
    private readonly ConsoleScreen _screen;
    private int _fps;
    private int _stepsPerSecond;

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
        var timer = Stopwatch.StartNew();
        var prevtime1Hz = TimeSpan.Zero;
        var prevtime60Hz = TimeSpan.Zero;
        _screen.Init();

        while (!cancellationToken.IsCancellationRequested)
        {
            prevtime1Hz = RunOnTimer(timer.Elapsed, prevtime1Hz,
                TimeSpan.FromSeconds(1),
                () =>
                {
                    _screen.UpdateStats(_fps, _stepsPerSecond);
                    _fps = 0;
                    _stepsPerSecond = 0;
                });

            prevtime60Hz = RunOnTimer(timer.Elapsed, prevtime60Hz,
                TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60),
                () =>
                {
                    _screen.DrawFrame();
                    _fps++;

                    for (var i = 0; i < 10; i++)
                    {
                        _cpu.Step();
                        _stepsPerSecond++;
                    }
                });
        }
    }

    private static TimeSpan RunOnTimer(TimeSpan now, TimeSpan lastRun, TimeSpan interval, Action toRun)
    {
        var elapsed = now - lastRun;
        while (elapsed >= interval)
        {
            toRun();

            lastRun += interval;
            elapsed -= interval;
        }

        return lastRun;
    }
}