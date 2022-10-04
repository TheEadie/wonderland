using System.Text;
using SFML.Graphics;
using SFML.System;
using Wonderland.Chip8.IO.Tabs;
using Wonderland.Chip8.IO.Text;

namespace Wonderland.Chip8.IO;

public class DebugTabContent : ITabContent
{
    private readonly Vector2f _position;
    private readonly RenderTarget _window;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;

    private const int Scale = 10;
    private readonly Color[,] _sfmlArray = new Color[8 * Scale, 16 * Scale];
    private Image _image;
    private readonly Sprite _sprite;

    public DebugTabContent(Vector2f position, RenderTarget window, Cpu cpu, Gpu gpu)
    {
        _image = new Image(_sfmlArray);
        var texture = new Texture(_image);
        _sprite = new Sprite(texture);

        _position = position;
        _window = window;
        _cpu = cpu;
        _gpu = gpu;
    }

    public void Draw()
    {
        DrawSection(_position + new Vector2f(0, 0), new Vector2f(162, 356), "Registers");
        DrawDebugRegisters(_position + new Vector2f(0, 0));

        DrawSection(_position + new Vector2f(162, 0), new Vector2f(210, 356), "Memory");
        DrawDebugGraphics(_position + new Vector2f(162, 0));

        DrawSection(_position + new Vector2f(162 + 210, 0), new Vector2f(320, 356), "Instructions");
        DrawDebugInstructions(_position + new Vector2f(162 + 210, 0));
    }

    private void DrawSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 36));
        headerSection.FillColor = Colours.BackgroundHeader;
        headerSection.Position = position;
        _window.Draw(headerSection);

        var text = TextFactory.Create();
        text.DisplayedString = title;
        text.FillColor = Colours.TextPrimary;
        text.CharacterSize = 16;
        text.Position = position + new Vector2f(24, 8);

        _window.Draw(text);

        var sectionBody = new RectangleShape(size);
        sectionBody.FillColor = Colours.BackgroundLevel2;
        sectionBody.Position = position + new Vector2f(0, 36);
        _window.Draw(sectionBody);

        text.CharacterSize = 14;
    }

    private void DrawDebugRegisters(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"DT: {_cpu.DelayTimer:X2}  ST:  {_cpu.SoundTimer:X2}";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(24, 36 + 12);
        _window.Draw(text);

        for (var i = 0; i < 16; i++)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("V")
                .Append(i.ToString("X"))
                .Append(": ")
                .Append(_cpu.V[i].ToString("X2"));

            if (_cpu.Stack.Count - 1 >= i)
            {
                stringBuilder.Append("  S")
                    .Append(i.ToString("X"))
                    .Append(": ")
                    .Append(_cpu.Stack.ElementAt(i).ToString("X3"));
            }

            text.DisplayedString = stringBuilder.ToString();
            text.FillColor = (i % 2) == 0 ? Colours.TextPrimary : Colours.TextSecondary;
            text.Position = position + new Vector2f(24, 48 + 36 + (i * 18));
            _window.Draw(text);
        }
    }

    private void DrawDebugGraphics(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"I: {_cpu.I:x3}";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(24, 36 + 12);
        _window.Draw(text);

        var memory = _cpu.GetGraphicsMemory().ToList();
        for (var i = 0; i < memory.Count; i++)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append((_cpu.I + i).ToString("X3"))
                .Append(":  ")
                .Append((memory[i]).ToString("X2"));

            text.DisplayedString = stringBuilder.ToString();
            text.FillColor = (i == 0) ? Colours.TextHighlight :
                (i % 2) == 0 ? Colours.TextPrimary : Colours.TextSecondary;
            text.Position = position + new Vector2f(24, 48 + 36 + (i * 18));
            _window.Draw(text);
        }

        text.DisplayedString = $"8xN";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(147, 55);
        _window.Draw(text);


        for (var y = 0; y < memory.Count; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                for (var i = 0; i < Scale; i++)
                {
                    for (var j = 0; j < Scale; j++)
                    {
                        if (i is Scale - 1 || j is Scale - 1)
                        {
                            _sfmlArray[x * Scale + i, y * Scale + j] = Colours.BackgroundLevel2;
                        }
                        else
                        {
                            _sfmlArray[x * Scale + i, y * Scale + j] = (memory.ElementAt(y) >> (7 - x) & 0b0000001) == 1
                                ? Color.White
                                : Color.Black;
                        }
                    }
                }
            }
        }

        _image = new Image(_sfmlArray);
        _sprite.Texture = new Texture(_image);
        _sprite.Position = position + new Vector2f(120, 48 + 36);
        _window.Draw(_sprite);
    }

    private void DrawDebugInstructions(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"PC: {_cpu.PC:X3}";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(24, 36 + 12);
        _window.Draw(text);

        var i = 0;
        var scope = 0;
        foreach (var (instruction, opCode) in _cpu.Peek())
        {
            var stringBuilder = new StringBuilder();

            var opCodeDescription = opCode.Description(instruction);
            stringBuilder.Append((_cpu.PC + 2 * (i - 4)).ToString("X3"))
                .Append(":  ")
                .Append(instruction.OpCode.ToString("X4"))
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

            text.DisplayedString = stringBuilder.ToString();
            text.FillColor = (i == 4) ? Colours.TextHighlight :
                (i % 2) == 0 ? Colours.TextPrimary : Colours.TextSecondary;
            text.Position = position + new Vector2f(24, 48 + 36 + (i * 18));
            _window.Draw(text);
            i++;
        }
    }
}
