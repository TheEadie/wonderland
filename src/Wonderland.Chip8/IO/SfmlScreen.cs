using System.Text;

namespace Wonderland.Chip8.IO;

public class SfmlScreen : IScreen
{
    private SFML.Graphics.RenderWindow? _window;
    private readonly Gpu _gpu;
    private readonly Cpu _cpu;

    private int _fps;
    private int _instructionsPerSecond;

    public SfmlScreen(Gpu gpu, Cpu cpu)
    {
        _gpu = gpu;
        _cpu = cpu;
    }

    public void Init()
    {
        _window = new SFML.Graphics.RenderWindow(new SFML.Window.VideoMode(800, 320), "Wonderland");
        _window.Clear();
    }

    public void DrawFrame()
    {
        _window.Clear();
        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var sfmlArray = new SFML.Graphics.Color[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                sfmlArray[x, y] = vRam[x, y] ? SFML.Graphics.Color.Green : SFML.Graphics.Color.Black;
            }
        }

        var image = new SFML.Graphics.Image(sfmlArray);
        var texture = new SFML.Graphics.Texture(image);
        var sprite = new SFML.Graphics.Sprite(texture);
        sprite.Scale = new SFML.System.Vector2f(10, 10);
        _window.Draw(sprite);

        var stringBuilder = new StringBuilder();

        stringBuilder.Append("FPS: ").Append(_fps).AppendLine();
        stringBuilder.Append("IPS: ").Append(_instructionsPerSecond).AppendLine();
        stringBuilder.AppendLine();
        stringBuilder.Append("PC: ").AppendLine(_cpu.PC.ToString("x4"));
        stringBuilder.Append("I: ").AppendLine(_cpu.I.ToString("x3"));
        stringBuilder.Append("DT: ").AppendLine(_cpu.DelayTimer.ToString("x2"));
        stringBuilder.Append("ST: ").AppendLine(_cpu.SoundTimer.ToString("x2"));

        var text = new SFML.Graphics.Text();
        text.Font = new SFML.Graphics.Font("/home/eadie/code/font.ttf");
        text.DisplayedString = stringBuilder.ToString();
        text.CharacterSize = 18;
        text.FillColor = SFML.Graphics.Color.Green;
        text.Position = new SFML.System.Vector2f(640, 0);

        _window.Draw(text);

        _window.Display();
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}
