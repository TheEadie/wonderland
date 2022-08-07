using System.Diagnostics;

namespace Wonderland.Chip8;

public class ConsoleScreen
{
    private readonly Gpu _gpu;

    public ConsoleScreen(Gpu gpu)
    {
        _gpu = gpu;
    }

    public async Task Draw(CancellationToken cancellationToken)
    {
        Console.Clear();
        Console.CursorVisible = false;

        while (!cancellationToken.IsCancellationRequested)
        {
            var vRam = _gpu.GetVRam();

            int width = vRam.GetLength(0);
            int height = vRam.GetLength(1);

            var startX = (Console.WindowWidth / 2) - (width / 2);
            var startY = (Console.WindowHeight / 2) - (height / 2);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(startX + x, startY + y);
                    Console.Write(vRam[x, y] ? '■' : ' ');
                }
            }

            await Task.Delay(16, cancellationToken);
        }
    }
}