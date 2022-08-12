namespace Wonderland.Chip8;

public class ConsoleScreen
{
    private readonly Gpu _gpu;
    private readonly Cpu _cpu;
    private readonly ConsoleIO _io;
    private int _fps;
    private int _instructionsPerSecond;

    public ConsoleScreen(Gpu gpu, Cpu cpu, ConsoleIO io)
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

        for (var i = 0; i < 16; i++)
        {
            Console.SetCursorPosition(startX + width + 2, startY + 6 + i);
            Console.WriteLine($"V{i:x}: {_cpu.V[i]:x2}");
        }

        var s = 0;
        foreach (var stackVal in _cpu.Stack)
        {
            Console.SetCursorPosition(startX + width + 12, startY + 6 + s);
            Console.WriteLine($"S{s:x}: {stackVal:x2}");
            s++;
        }

        var keyPressed = _io.Keys.Select((b, i) => new {Index = i, Value = b})
            .Where(o => o.Value)
            .Select(o => o.Index)
            .ToList();

        var keyPressedCount = keyPressed.Count;
        var keyPressedFirst = keyPressed.FirstOrDefault();
        var keyPressedDisplay = (keyPressedCount > 0) ? keyPressedFirst.ToString("x") : " ";
        Console.SetCursorPosition(startX + width + 22, startY + 6);
        Console.WriteLine($"Key: {keyPressedDisplay}");
    }

    public void UpdateStats(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}