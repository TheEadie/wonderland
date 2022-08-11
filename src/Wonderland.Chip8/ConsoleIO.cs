namespace Wonderland.Chip8;

public class ConsoleIO
{
    public bool Pause { get; private set; }
    public bool StepForward { get; private set; }

    public void Step()
    {
        if (!Console.KeyAvailable)
        {
            Pause = false;
            StepForward = false;
            return;
        }

        var key = Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.P:
                Pause = true;
                break;
            case ConsoleKey.N:
                StepForward = true;
                break;
        }
    }
}