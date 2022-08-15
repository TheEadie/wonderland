using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Wonderland.Chip8.IO;

public class SfmlScreen : IScreen
{
    private RenderWindow? _window;
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
        _window = new RenderWindow(new SFML.Window.VideoMode(800, 350), "Wonderland");
        _window.Clear();
    }

    public void DrawFrame()
    {
        _window.Clear();
        
        DrawSection(new Vector2f(2,2), new Vector2f(640, 320), "Game");

        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var sfmlArray = new Color[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                sfmlArray[x, y] = vRam[x, y] ? Color.Green : Color.Black;
            }
        }

        var image = new Image(sfmlArray);
        var texture = new Texture(image);
        var sprite = new Sprite(texture);
        sprite.Position = new Vector2f(3, 27);
        sprite.Scale = new Vector2f(10, 10);
        _window.Draw(sprite);
        
        DrawSection(new Vector2f(644,2), new Vector2f(100, 320), "Debug");

        var stringBuilder = new StringBuilder();

        stringBuilder.Append("FPS: ").Append(_fps).AppendLine();
        stringBuilder.Append("IPS: ").Append(_instructionsPerSecond).AppendLine();
        stringBuilder.AppendLine();
        stringBuilder.Append("PC: ").AppendLine(_cpu.PC.ToString("x4"));
        stringBuilder.Append("I: ").AppendLine(_cpu.I.ToString("x3"));
        stringBuilder.Append("DT: ").AppendLine(_cpu.DelayTimer.ToString("x2"));
        stringBuilder.Append("ST: ").AppendLine(_cpu.SoundTimer.ToString("x2"));

        var text = new Text();
        text.Font = new Font("resources/JetBrainsMonoNL-Regular.ttf");
        text.DisplayedString = stringBuilder.ToString();
        text.CharacterSize = 14;
        text.FillColor = Color.Green;
        text.Position = new Vector2f(648, 27);

        _window.Draw(text);

        _window.Display();
    }

    private void DrawSection(Vector2f position, Vector2f size, string title)
    {
        var gameAreaHeader = new RectangleShape(new Vector2f(size.X, 24));
        gameAreaHeader.OutlineThickness = 2;
        gameAreaHeader.FillColor = Color.Black;
        gameAreaHeader.Position = position;
        _window.Draw(gameAreaHeader);

        var gameHeaderText = new Text();
        gameHeaderText.Font = new Font("resources/JetBrainsMonoNL-Regular.ttf");
        gameHeaderText.DisplayedString = title;
        gameHeaderText.CharacterSize = 14;
        gameHeaderText.FillColor = Color.Green;
        gameHeaderText.Position = position + new Vector2f(8, 4);

        _window.Draw(gameHeaderText);

        var gameArea = new RectangleShape(size);
        gameArea.OutlineThickness = 2;
        gameArea.FillColor = Color.Black;
        gameArea.Position = position + new Vector2f(0, 26);
        _window.Draw(gameArea);
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}
