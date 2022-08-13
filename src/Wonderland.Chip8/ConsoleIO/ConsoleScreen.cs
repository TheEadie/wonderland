namespace Wonderland.Chip8.ConsoleIO;

public class ConsoleScreen : IScreen
{
    private readonly Gpu _gpu;
    private readonly Cpu _cpu;
    private readonly IInputOutput _io;
    private int _fps;
    private int _instructionsPerSecond;

    public ConsoleScreen(Gpu gpu, Cpu cpu, IInputOutput io)
    {
        _gpu = gpu;
        _cpu = cpu;
        _io = io;

        _fps = 0;
        _instructionsPerSecond = 0;
    }

    public void Init()
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.ForegroundColor = ConsoleColor.Green;
    }

    public void DrawFrame()
    {
        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var startX = (Console.WindowWidth / 2) - (width / 2) - 10;
        var startY = (Console.WindowHeight / 2) - (height / 2);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.SetCursorPosition(startX + x, startY + y);
                Console.Write(vRam[x, y] ? '■' : ' ');
            }
        }

        Console.SetCursorPosition(startX + width + 2, startY);
        Console.WriteLine($"FPS: {_fps}  ");
        Console.SetCursorPosition(startX + width + 2, startY + 1);
        Console.WriteLine($"Instructions: {_instructionsPerSecond}  ");

        Console.SetCursorPosition(startX + width + 2, startY + 3);
        Console.WriteLine($"PC: {_cpu.PC:x4} - {_cpu.GetOpCode()}");
        Console.SetCursorPosition(startX + width + 2, startY + 4);
        Console.WriteLine($"I: {_cpu.I:x3}");

        Console.SetCursorPosition(startX + width + 2, startY + 5);
        Console.WriteLine($"DT: {_cpu.DelayTimer:x2}");

        Console.SetCursorPosition(startX + width + 2, startY + 6);
        Console.WriteLine($"ST: {_cpu.SoundTimer:x2}");

        for (var i = 0; i < 16; i++)
        {
            Console.SetCursorPosition(startX + width + 2, startY + 8 + i);
            Console.WriteLine($"V{i:x}: {_cpu.V[i]:x2}");
        }

        var s = 0;
        foreach (var stackVal in _cpu.Stack)
        {
            Console.SetCursorPosition(startX + width + 12, startY + 8 + s);
            Console.WriteLine($"S{s:x}: {stackVal:x2}");
            s++;
        }

        var keyPressed = _io.GetPressedKey();
        var keyPressedDisplay = (keyPressed != null) ? keyPressed.Value.ToString("x") : " ";
        Console.SetCursorPosition(startX + width + 2, startY + 25);
        Console.WriteLine($"Key: {keyPressedDisplay}");

    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}
