namespace Wonderland.Chip8.ConsoleIO;

public class ConsoleInput : IInputOutput
{
    public bool[] Keys { get; } = new bool[16];
    public bool Pause { get; private set; }
    public bool StepForward { get; private set; }

    private readonly Dictionary<ConsoleKey, byte> _lookup;

    private int _cooldown;

    public ConsoleInput()
    {
        _lookup = new Dictionary<ConsoleKey, byte> {
            {ConsoleKey.D1, 0x1},
            {ConsoleKey.D2, 0x2},
            {ConsoleKey.D3, 0x3},
            {ConsoleKey.D4, 0xC},
            {ConsoleKey.Q, 0x4},
            {ConsoleKey.W, 0x5},
            {ConsoleKey.E, 0x6},
            {ConsoleKey.R, 0xD},
            {ConsoleKey.A, 0x7},
            {ConsoleKey.S, 0x8},
            {ConsoleKey.D, 0x9},
            {ConsoleKey.F, 0xE},
            {ConsoleKey.Z, 0xA},
            {ConsoleKey.X, 0x0},
            {ConsoleKey.C, 0xB},
            {ConsoleKey.V, 0xF}
        };
    }

    public byte? GetPressedKey()
    {
        var keyPressed = Keys.Select((b, i) => new {Index = i, Value = b})
            .Where(o => o.Value)
            .Select(o => o.Index)
            .ToList();

        var keyPressedCount = keyPressed.Count;
        var keyPressedFirst = keyPressed.FirstOrDefault();
        return (keyPressedCount > 0) ? (byte)keyPressedFirst : null;
    }

    public void Beep()
    {
        System.Console.Beep();
    }

    public void Step()
    {
        if (!System.Console.KeyAvailable)
        {
            if (_cooldown <= 0)
            {
                Pause = false;
                StepForward = false;

                for (var i = 0; i < 16; i++)
                {
                    Keys[i] = false;
                }
            }
            else
            {
                _cooldown--;
            }

            return;
        }

        var key = System.Console.ReadKey(true).Key;

        switch (key)
        {
            case ConsoleKey.P:
                Pause = true;
                _cooldown = 0;
                break;
            case ConsoleKey.N:
                StepForward = true;
                _cooldown = 0;
                break;
            default:
                if (_lookup.ContainsKey(key))
                {
                    Keys[_lookup[key]] = true;
                }
                _cooldown = 10;
                break;
        }

        
    }
}