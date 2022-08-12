using System.Diagnostics;
using Wonderland.Chip8.ConsoleIO;

namespace Wonderland.Chip8;

public class Emulator
{
    private readonly byte[] _memory;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;
    private readonly IScreen _screen;
    private readonly IInputOutput _io;

    private readonly int _targetClockSpeed;
    private readonly int _targetFps;
    private int _actualClockSpeed;
    private int _actualFps;
    private readonly double _stepsPerFrame;
    private int _debugStepsSinceLastStep60Hz;

    private bool _pause;

    public Emulator(int clockSpeed = 1000)
    {
        _memory = new byte[4096];
        _io = new ConsoleInput();
        _gpu = new Gpu();
        _cpu = new Cpu(_memory, _gpu, _io);
        _screen = new ConsoleScreen(_gpu, _cpu, _io);
        _targetClockSpeed = clockSpeed;
        _targetFps = 60;
        _stepsPerFrame = (double)_targetClockSpeed / _targetFps;

        _pause = false;
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

    public void Run(CancellationToken cancellationToken)
    {
        var timer = Stopwatch.StartNew();
        var prevtime1Hz = TimeSpan.Zero;
        var prevtime60Hz = TimeSpan.Zero;
        _screen.Init();

        while (!cancellationToken.IsCancellationRequested)
        {
            void EverySecond()
            {
                _screen.UpdateStats(_actualFps, _actualClockSpeed);
                _actualFps = 0;
                _actualClockSpeed = 0;
            }

            void EveryFrame()
            {
                ProcessInput();
                _screen.DrawFrame();
                _actualFps++;

                if (_pause) return;

                _cpu.Step60Hz();
                for (var i = 0; i < _stepsPerFrame; i++)
                {
                    _cpu.Step();
                    _actualClockSpeed++;
                }
            }

            prevtime1Hz = RunOnTimer(timer.Elapsed, prevtime1Hz,
                TimeSpan.FromSeconds(1),
                EverySecond);

            prevtime60Hz = RunOnTimer(timer.Elapsed, prevtime60Hz,
                TimeSpan.FromTicks(TimeSpan.TicksPerSecond / _targetFps),
                EveryFrame);
        }
    }

    private void ProcessInput()
    {
        _io.Step();

        if (_cpu.SoundTimer > 0)
        {
            _io.Beep();
        }

        if (_io.Pause)
        {
            _pause = !_pause;
        }
        if (_io.StepForward)
        {
            if (_debugStepsSinceLastStep60Hz >= _stepsPerFrame)
            {
                _cpu.Step60Hz();
                _debugStepsSinceLastStep60Hz = 0;
            }
            
            _cpu.Step();
            _debugStepsSinceLastStep60Hz++;
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