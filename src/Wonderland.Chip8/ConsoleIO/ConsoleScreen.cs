using Spectre.Console;

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

        var canvas = new Canvas(width, height);
        canvas.PixelWidth = 2;
        canvas.Scale = false;
        var startY = (Console.WindowHeight / 2) - (height / 2);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                canvas.SetPixel(x, y, vRam[x, y] ? Color.Green : Color.Black);
            }
        }

        var game = new Panel(canvas);

        var cpu = new Table().HideHeaders().Centered();
        cpu.AddColumn("");
        cpu.AddColumn("").Alignment(Justify.Right);

        cpu.AddRow("FPS", $"{_fps}");
        cpu.AddRow("IPS", $"{_instructionsPerSecond}");
        cpu.AddEmptyRow();
        cpu.AddRow("PC", $"{_cpu.PC:x4}");
        cpu.AddRow("I", $"{_cpu.I:x3}");
        cpu.AddRow("DT", $"{_cpu.DelayTimer:x2}");
        cpu.AddRow("ST", $"{_cpu.SoundTimer:x2}");

        var reg = new Table().HideHeaders().Centered();
        reg.AddColumn("");
        reg.AddColumn("");
        for (var i = 0; i < 16; i++)
        {
            reg.AddRow($"V{i:x}", $"{_cpu.V[i]:x2}");
        }

        var stack = new Table().HideHeaders().Centered();
        stack.AddColumn("");
        stack.AddColumn("");
        var s = 0;
        foreach (var stackVal in _cpu.Stack)
        {
            stack.AddRow($"S{s:x}", $"{stackVal:x3}");
            s++;
        }
        for (var i = s; i < 16; i++)
        {
            stack.AddRow($"S{i:x}", "   ");
        }

        cpu.Columns[1].Alignment(Justify.Right);
        reg.Columns[1].Alignment(Justify.Right);
        stack.Columns[1].Alignment(Justify.Right);

        var debug = new Table().HideHeaders().Centered().Border(TableBorder.None);
        debug.AddColumn(new TableColumn("CPU"));
        debug.AddColumn(new TableColumn("Reg"));
        debug.AddColumn(new TableColumn("Stack"));
        debug.AddRow(cpu, reg, stack);

        var table = new Table().Centered().HideHeaders().Border(TableBorder.None);
        table.AddColumn(new TableColumn("Wonderland - CHIP-8"));
        table.AddColumn(new TableColumn("Debug"));
        table.AddRow(game, debug);

        AnsiConsole.Cursor.SetPosition(0, 0);
        AnsiConsole.Write(table);
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}
