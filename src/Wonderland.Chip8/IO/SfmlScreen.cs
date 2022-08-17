using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Wonderland.Chip8.IO;

public class SfmlScreen : IScreen
{
    private readonly RenderWindow _window;
    private readonly Gpu _gpu;
    private readonly Cpu _cpu;

    private int _fps;
    private int _instructionsPerSecond;

    private readonly Text _text;

    private readonly Color _background = new(114, 131, 116);
    private readonly Color _backgroundDark = new(83, 97, 84);
    private readonly Color _textColour = new(255, 255, 255);
    private readonly Color _textHeading = new(204, 204, 204);
    private readonly Color _textHighlight = new(253, 184, 51);
    private readonly Color _borderInternal = new(170, 170, 170);

    public SfmlScreen(Gpu gpu, Cpu cpu)
    {
        _gpu = gpu;
        _cpu = cpu;

        _text = new Text();

        _window = new RenderWindow(new VideoMode(642, 758), "Wonderland", Styles.Close);
        _window.Closed += (obj, e) => { _window.Close(); };
        _window.KeyPressed +=
            (sender, e) =>
            {
                var window = (Window)sender!;
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }
            };
    }

    public bool IsOpen() => _window.IsOpen;

    public void Init()
    {
        _window.Clear();

        _text.Font = new Font("resources/JetBrainsMonoNL-Regular.ttf");
        _text.CharacterSize = 14;
    }

    public void DrawFrame()
    {
        _window.DispatchEvents();
        _window.Clear();

        DrawSection(new Vector2f(1, 1), new Vector2f(640, 320), "Wonderland CHIP-8");
        DrawSection(new Vector2f(1, 348), new Vector2f(159, 356), "CPU");
        DrawSection(new Vector2f(161, 348), new Vector2f(159, 356), "Graphics");
        DrawSection(new Vector2f(321, 348), new Vector2f(320, 356), "Instructions");
        DrawFooterIPS(new Vector2f(1, 731), new Vector2f(159, 0));
        DrawFooterFPS(new Vector2f(161, 731), new Vector2f(159, 0));
        DrawFooterButtons(new Vector2f(321, 731), new Vector2f(320, 0));

        DrawGame();

        DrawDebugRegisters();
        DrawDebugGraphics();
        DrawDebugInstructions();

        _window.Display();
    }

    private void DrawGame()
    {
        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var sfmlArray = new Color[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                sfmlArray[x, y] = vRam[x, y] ? Color.White : Color.Black;
            }
        }

        var image = new Image(sfmlArray);
        var texture = new Texture(image);
        var sprite = new Sprite(texture);
        sprite.Position = new Vector2f(1, 27);
        sprite.Scale = new Vector2f(10, 10);
        _window.Draw(sprite);
    }

    private void DrawDebugRegisters()
    {
        _text.DisplayedString = $"DT: {_cpu.DelayTimer:x2}  ST:  {_cpu.SoundTimer:x2}";
        _text.FillColor = _textColour;
        _text.Position = new Vector2f(14, 382);
        _window.Draw(_text);

        for (var i = 0; i < 16; i++)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("V")
                .Append(i.ToString("x"))
                .Append(": ")
                .Append(_cpu.V[i].ToString("x2"));

            if (_cpu.Stack.Count - 1 >= i)
            {
                stringBuilder.Append("  S")
                    .Append(i.ToString("x"))
                    .Append(": ")
                    .Append(_cpu.Stack.ElementAt(i).ToString("x3"));
            }

            _text.DisplayedString = stringBuilder.ToString();
            _text.FillColor = (i % 2) == 0 ? _textColour : _textHeading;
            _text.Position = new Vector2f(14, 382 + 36 + (i * 18));
            _window.Draw(_text);
        }
    }

    private void DrawDebugGraphics()
    {
        _text.DisplayedString = $"I: {_cpu.I:x3}";
        _text.FillColor = _textColour;
        _text.Position = new Vector2f(161 + 14, 382);
        _window.Draw(_text);

        var memory = _cpu.GetGraphicsMemory();
        for (var i = 0; i < memory.Count(); i++)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append((_cpu.I + (i - 4)).ToString("x3")).Append(':');

            _text.DisplayedString = stringBuilder.ToString();
            _text.FillColor = (i - 4 == 0) ? _textHighlight : (i % 2) == 0 ? _textColour : _textHeading;
            _text.Position = new Vector2f(161 + 14, 418 + (i * 18));
            _window.Draw(_text);
        }

        var graphicsBorder = new RectangleShape(new Vector2f(88, 18 * memory.Count() + 8));
        graphicsBorder.OutlineThickness = 1;
        graphicsBorder.OutlineColor = _borderInternal;
        graphicsBorder.FillColor = _background;
        graphicsBorder.Position = new Vector2f(216, 414);
        _window.Draw(graphicsBorder);

        var sfmlArray = new Color[8, memory.Count()];

        for (var y = 0; y < memory.Count(); y++)
        {
            for (var x = 0; x < 8; x++)
            {
                sfmlArray[x, y] = (memory.ElementAt(y) >> (7 - x) & 0b0000001) == 1 ? Color.White : Color.Black;
            }
        }

        var image = new Image(sfmlArray);
        var texture = new Texture(image);
        var sprite = new Sprite(texture);
        sprite.Position = new Vector2f(220, 418);
        sprite.Scale = new Vector2f(10, 18);
        _window.Draw(sprite);

        var line = new RectangleShape(new Vector2f(88, 1));
        line.FillColor = _textHighlight;
        line.OutlineThickness = 0;
        line.Position = new Vector2f(216, 414 + 18 * 4 + 3);
        _window.Draw(line);
    }

    private void DrawDebugInstructions()
    {
        _text.DisplayedString = $"PC: {_cpu.PC:x3}";
        _text.FillColor = _textColour;
        _text.Position = new Vector2f(321 + 14, 382);
        _window.Draw(_text);

        var i = 0;
        var scope = 0;
        foreach (var (instruction, opCode) in _cpu.Peek())
        {
            var stringBuilder = new StringBuilder();
            
            var opCodeDescription = opCode.Description(instruction);
            stringBuilder.Append((_cpu.PC + 2 * (i - 4)).ToString("x3"))
                .Append(":  ")
                .Append(instruction.OpCode.ToString("x4"))
                .Append("    ")
                .Append(scope > 0 ? "  " : "")
                .Append(opCodeDescription);

            if (opCodeDescription.StartsWith("IF "))
            {
                scope++;
            }
            else
            {
                if (scope > 0) scope--;
            }

            _text.DisplayedString = stringBuilder.ToString();
            _text.FillColor = (i == 4) ? _textHighlight : (i % 2) == 0 ? _textColour : _textHeading;
            _text.Position = new Vector2f(321 + 14, 418 + (i * 18));
            _window.Draw(_text);
            i++;
        }
    }

    private void DrawSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 26));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = _borderInternal;
        headerSection.FillColor = _backgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);

        _text.DisplayedString = title;
        _text.FillColor = _textHeading;
        _text.Position = position + new Vector2f(12, 4);

        _window.Draw(_text);

        var sectionBody = new RectangleShape(size);
        sectionBody.OutlineThickness = 1;
        sectionBody.OutlineColor = _borderInternal;
        sectionBody.FillColor = _background;
        sectionBody.Position = position + new Vector2f(0, 26);
        _window.Draw(sectionBody);
    }

    private void DrawFooterIPS(Vector2f position, Vector2f size)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 26));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = _borderInternal;
        headerSection.FillColor = _backgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);

        _text.DisplayedString = $"IPS: {_instructionsPerSecond}";
        _text.FillColor = _textHeading;
        _text.Position = position + new Vector2f(12, 4);

        _window.Draw(_text);
    }

    private void DrawFooterFPS(Vector2f position, Vector2f size)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 26));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = _borderInternal;
        headerSection.FillColor = _backgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);

        _text.DisplayedString = $"FPS: {_fps}";
        _text.FillColor = _textHeading;
        _text.Position = position + new Vector2f(12, 4);

        _window.Draw(_text);
    }

    private void DrawFooterButtons(Vector2f position, Vector2f size)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 26));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = _borderInternal;
        headerSection.FillColor = _backgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}
