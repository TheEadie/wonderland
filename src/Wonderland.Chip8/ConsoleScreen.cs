namespace Wonderland.Chip8;

public class ConsoleScreen
{
    private readonly Gpu _gpu;
    private int _fps;
    private int _instructionsPerSecond;

    public ConsoleScreen(Gpu gpu)
    {
        _gpu = gpu;
        _fps = 0;
        _instructionsPerSecond = 0;
    }

    public void Init()
    {
        Console.Clear();
        Console.CursorVisible = false;
    }

    public void DrawFrame()
    {
        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var startX = (Console.WindowWidth / 2) - (width / 2);
        var startY = (Console.WindowHeight / 2) - (height / 2) - 4;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.SetCursorPosition(startX + x, startY + y);
                Console.Write(vRam[x, y] ? '■' : ' ');
            }
        }

        Console.SetCursorPosition(startX, startY + height + 2);
        Console.WriteLine($"FPS: {_fps} Instructions: {_instructionsPerSecond}  ");
    }

    public void UpdateStats(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}