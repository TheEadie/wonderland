using SFML.Window;

namespace Wonderland.Chip8.IO;

public class SfmlInput : IInputOutput
{
    public bool[] Keys { get; } = new bool[16];

    private readonly bool[] _keysReleased = new bool[16];
    public bool Pause { get; private set; }
    public bool StepForward { get; private set; }

    private readonly Dictionary<Keyboard.Key, byte> _lookup;
    private int _cooldown;

    public SfmlInput()
    {
        _lookup = new Dictionary<Keyboard.Key, byte>
        {
            {Keyboard.Key.Num1, 0x1},
            {Keyboard.Key.Num2, 0x2},
            {Keyboard.Key.Num3, 0x3},
            {Keyboard.Key.Num4, 0xC},
            {Keyboard.Key.Q, 0x4},
            {Keyboard.Key.W, 0x5},
            {Keyboard.Key.E, 0x6},
            {Keyboard.Key.R, 0xD},
            {Keyboard.Key.A, 0x7},
            {Keyboard.Key.S, 0x8},
            {Keyboard.Key.D, 0x9},
            {Keyboard.Key.F, 0xE},
            {Keyboard.Key.Z, 0xA},
            {Keyboard.Key.X, 0x0},
            {Keyboard.Key.C, 0xB},
            {Keyboard.Key.V, 0xF}
        };
    }

    public byte? GetReleasedKey()
    {
        var keyReleased = _keysReleased.Select((b, i) => new { Index = i, Value = b })
            .Where(o => o.Value)
            .Select(o => o.Index)
            .ToList();

        var keyReleasedCount = keyReleased.Count;
        var keyReleasedFirst = keyReleased.FirstOrDefault();
        return (keyReleasedCount > 0) ? (byte)keyReleasedFirst : null;
    }

    public void Beep()
    {
        // TODO
    }

    public void Step()
    {
        if (_cooldown > 0) _cooldown--;
        var pPressed = Keyboard.IsKeyPressed(Keyboard.Key.P);
        Pause = pPressed && _cooldown == 0;
        var nPressed = Keyboard.IsKeyPressed(Keyboard.Key.N);
        StepForward = nPressed && _cooldown == 0;
        foreach (var key in _lookup.Keys)
        {
            var isKeyPressed = Keyboard.IsKeyPressed(key);
            var mappedKey = _lookup[key];

            _keysReleased[mappedKey] = Keys[mappedKey] && !isKeyPressed;
            Keys[mappedKey] = isKeyPressed;
        }

        if ((pPressed || nPressed) && _cooldown == 0)
        {
            _cooldown = 10;
        }
    }
}
