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

    private readonly Color _background = new(50, 50, 50);
    private readonly Color _backgroundDark = new(80, 100, 80);
    private readonly Color _inactive = new(59, 68, 60);
    private readonly Color _textColour = new(255, 255, 255);
    private readonly Color _textAlt = new(204, 204, 204);
    private readonly Color _textHighlight = new(253, 184, 51);
    private readonly Color _borderInternal = new(170, 170, 170);

    public SfmlScreen(Gpu gpu, Cpu cpu)
    {
        _gpu = gpu;
        _cpu = cpu;

        _text = new Text();

        _window = new RenderWindow(new VideoMode(690, 900), "Wonderland", Styles.Close);
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

        DrawMainSection(new Vector2f(1, 1), new Vector2f(690, 870), "Wonderland CHIP-8");
        DrawGame(new Vector2f(25,  62));

        DrawTab(new Vector2f(25, 408), "Info", false);
        DrawTab(new Vector2f(25 + 68 + 2, 408), "Debug", true);
        DrawTab(new Vector2f(25 + 68 + 78 + 4, 408), "Settings", false);
        
        DrawButton(new Vector2f(690 - 25 - 42, 400), "||", false);
        DrawButton(new Vector2f(690 - 25 - 42 - 5 - 42, 400), "->", false);

        const int tabSectionStart = 450;
        DrawSection(new Vector2f(25 + 321, tabSectionStart), new Vector2f(159, 356), "CPU");
        DrawDebugRegisters(new Vector2f(25 + 321, tabSectionStart));
        
        DrawSection(new Vector2f(25 + 481, tabSectionStart), new Vector2f(160, 356), "Graphics");
        DrawDebugGraphics(new Vector2f(25 + 481, tabSectionStart));
        
        DrawSection(new Vector2f(25 + 1, tabSectionStart), new Vector2f(320, 356), "Instructions");
        DrawDebugInstructions(new Vector2f(25 + 1, tabSectionStart));
        
        DrawFooter(new Vector2f(1, 900-27), new Vector2f(690, 0));

        _window.Display();
    }

    private void DrawTab(Vector2f position, string name, bool active)
    {
        var border = new RectangleShape(new Vector2f(name.Length * 10 + 28, 42));
        border.FillColor = active ? _backgroundDark : _inactive;
        border.Position = position + new Vector2f(1, 0);
        _window.Draw(border);
        
        _text.DisplayedString = name;
        _text.FillColor = _textColour;
        _text.CharacterSize = 16;
        _text.Position = position + new Vector2f(14, 10);
        _window.Draw(_text);
        
        _text.CharacterSize = 14;
    }
    
    private void DrawButton(Vector2f position, string content, bool active)
    {
        var border = new RectangleShape(new Vector2f(42, 42));
        border.OutlineThickness = 1;
        border.OutlineColor = _textColour;
        border.FillColor = active ? _backgroundDark : _inactive;
        border.Position = position + new Vector2f(0, 0);
        _window.Draw(border);
        
        _text.DisplayedString = content;
        _text.FillColor = _textColour;
        _text.CharacterSize = 16;
        _text.Position = position + new Vector2f(12, 10);
        _window.Draw(_text);
        
        _text.CharacterSize = 14;
    }

    private void DrawGame(Vector2f position)
    {
        var border = new RectangleShape(new Vector2f(640, 320));
        border.OutlineThickness = 1;
        border.OutlineColor = _borderInternal;
        border.FillColor = _background;
        border.Position = position + new Vector2f(0, 0);
        _window.Draw(border);
        
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
        sprite.Position = position;
        sprite.Scale = new Vector2f(10, 10);
        _window.Draw(sprite);
    }

    private void DrawDebugRegisters(Vector2f position)
    {
        _text.DisplayedString = $"DT: {_cpu.DelayTimer:x2}  ST:  {_cpu.SoundTimer:x2}";
        _text.FillColor = _textColour;
        _text.Position = position + new Vector2f(14, 34);
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
            _text.FillColor = (i % 2) == 0 ? _textColour : _textAlt;
            _text.Position = position + new Vector2f(14, 70 + (i * 18));
            _window.Draw(_text);
        }
    }

    private void DrawDebugGraphics(Vector2f position)
    {
        _text.DisplayedString = $"I: {_cpu.I:x3}";
        _text.FillColor = _textColour;
        _text.Position = position + new Vector2f(14, 34);
        _window.Draw(_text);

        var memory = _cpu.GetGraphicsMemory();
        for (var i = 0; i < memory.Count(); i++)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append((_cpu.I + (i - 4)).ToString("x3")).Append(':');

            _text.DisplayedString = stringBuilder.ToString();
            _text.FillColor = (i - 4 == 0) ? _textHighlight : (i % 2) == 0 ? _textColour : _textAlt;
            _text.Position = position + new Vector2f(14, 70 + (i * 18));
            _window.Draw(_text);
        }

        var graphicsBorder = new RectangleShape(new Vector2f(88, 18 * memory.Count() + 8));
        graphicsBorder.OutlineThickness = 1;
        graphicsBorder.OutlineColor = _borderInternal;
        graphicsBorder.FillColor = _background;
        graphicsBorder.Position = position + new Vector2f(55, 66);
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
        sprite.Position = position + new Vector2f(59, 70);
        sprite.Scale = new Vector2f(10, 18);
        _window.Draw(sprite);

        var line = new RectangleShape(new Vector2f(88, 1));
        line.FillColor = _textHighlight;
        line.OutlineThickness = 0;
        line.Position = position + new Vector2f(55, 70 + 18 * 4);
        _window.Draw(line);
    }

    private void DrawDebugInstructions(Vector2f position)
    {
        _text.DisplayedString = $"PC: {_cpu.PC:x3}";
        _text.FillColor = _textColour;
        _text.Position = position + new Vector2f(14, 34);
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
            _text.FillColor = (i == 4) ? _textHighlight : (i % 2) == 0 ? _textColour : _textAlt;
            _text.Position = position + new Vector2f(14, 70 + (i * 18));
            _window.Draw(_text);
            i++;
        }
    }
    
    private void DrawMainSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 36));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = _borderInternal;
        headerSection.FillColor = _backgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);

        _text.DisplayedString = title;
        _text.FillColor = _textColour;
        _text.CharacterSize = 16;
        _text.Position = position + new Vector2f(12, 8);

        _window.Draw(_text);

        var sectionBody = new RectangleShape(size);
        sectionBody.OutlineThickness = 1;
        sectionBody.OutlineColor = _borderInternal;
        sectionBody.FillColor = _background;
        sectionBody.Position = position + new Vector2f(0, 36);
        _window.Draw(sectionBody);
        
        _text.CharacterSize = 14;
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
        _text.FillColor = _textColour;
        _text.Position = position + new Vector2f(12, 4);

        _window.Draw(_text);

        var sectionBody = new RectangleShape(size);
        sectionBody.OutlineThickness = 1;
        sectionBody.OutlineColor = _borderInternal;
        sectionBody.FillColor = _background;
        sectionBody.Position = position + new Vector2f(0, 26);
        _window.Draw(sectionBody);
    }

    private void DrawFooter(Vector2f position, Vector2f size)
    {
        var section = new RectangleShape(new Vector2f(size.X, 26));
        section.OutlineThickness = 1;
        section.OutlineColor = _borderInternal;
        section.FillColor = _backgroundDark;
        section.Position = position;
        _window.Draw(section);

        _text.DisplayedString = $"IPS: {_instructionsPerSecond}    FPS: {_fps}";
        _text.FillColor = _textAlt;
        _text.Position = position + size - new Vector2f(_text.DisplayedString.Length * 8 + 14, -4);

        _window.Draw(_text);
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
    }
}
