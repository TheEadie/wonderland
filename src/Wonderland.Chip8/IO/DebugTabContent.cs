using System.Text;
using SFML.Graphics;
using SFML.System;

namespace Wonderland.Chip8.IO;

public class DebugTabContent : ITabContent
{
    private readonly Vector2f _position;
    private readonly RenderTarget _window;
    private readonly Cpu _cpu;
    private readonly Gpu _gpu;

    public DebugTabContent(Vector2f position, RenderTarget window, Cpu cpu, Gpu gpu)
    {
        _position = position;
        _window = window;
        _cpu = cpu;
        _gpu = gpu;
    }
    
    public void Draw()
    {
        DrawSection(_position + new Vector2f(1, 0), new Vector2f(320, 356), "Instructions");
        DrawDebugInstructions(_position+ new Vector2f(1, 0));
        
        DrawSection(_position + new Vector2f(321, 0), new Vector2f(159, 356), "CPU");
        DrawDebugRegisters(_position + new Vector2f(321, 0));
        
        DrawSection(_position + new Vector2f(481, 0), new Vector2f(160, 356), "Graphics");
        DrawDebugGraphics(_position + new Vector2f(481, 0));
    }
    
    private void DrawSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 26));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = Colours.BorderInternal;
        headerSection.FillColor = Colours.BackgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);

        var text = TextFactory.Create();
        text.DisplayedString = title;
        text.FillColor = Colours.TextColour;
        text.Position = position + new Vector2f(12, 4);

        _window.Draw(text);

        var sectionBody = new RectangleShape(size);
        sectionBody.OutlineThickness = 1;
        sectionBody.OutlineColor = Colours.BorderInternal;
        sectionBody.FillColor = Colours.Background;
        sectionBody.Position = position + new Vector2f(0, 26);
        _window.Draw(sectionBody);
    }
    
    private void DrawDebugRegisters(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"DT: {_cpu.DelayTimer:x2}  ST:  {_cpu.SoundTimer:x2}";
        text.FillColor = Colours.TextColour;
        text.Position = position + new Vector2f(14, 34);
        _window.Draw(text);

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

            text.DisplayedString = stringBuilder.ToString();
            text.FillColor = (i % 2) == 0 ? Colours.TextColour : Colours.TextAlt;
            text.Position = position + new Vector2f(14, 70 + (i * 18));
            _window.Draw(text);
        }
    }

    private void DrawDebugGraphics(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"I: {_cpu.I:x3}";
        text.FillColor = Colours.TextColour;
        text.Position = position + new Vector2f(14, 34);
        _window.Draw(text);

        var memory = _cpu.GetGraphicsMemory().ToList();
        for (var i = 0; i < memory.Count; i++)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append((_cpu.I + (i - 4)).ToString("x3")).Append(':');

            text.DisplayedString = stringBuilder.ToString();
            text.FillColor = (i - 4 == 0) ? Colours.TextHighlight : (i % 2) == 0 ? Colours.TextColour : Colours.TextAlt;
            text.Position = position + new Vector2f(14, 70 + (i * 18));
            _window.Draw(text);
        }

        var graphicsBorder = new RectangleShape(new Vector2f(88, 18 * memory.Count + 8));
        graphicsBorder.OutlineThickness = 1;
        graphicsBorder.OutlineColor = Colours.BorderInternal;
        graphicsBorder.FillColor = Colours.Background;
        graphicsBorder.Position = position + new Vector2f(55, 66);
        _window.Draw(graphicsBorder);

        var sfmlArray = new Color[8, memory.Count];

        for (var y = 0; y < memory.Count; y++)
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
        line.FillColor = Colours.TextHighlight;
        line.OutlineThickness = 0;
        line.Position = position + new Vector2f(55, 70 + 18 * 4);
        _window.Draw(line);
    }

    private void DrawDebugInstructions(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"PC: {_cpu.PC:x3}";
        text.FillColor = Colours.TextColour;
        text.Position = position + new Vector2f(14, 34);
        _window.Draw(text);

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

            text.DisplayedString = stringBuilder.ToString();
            text.FillColor = (i == 4) ? Colours.TextHighlight : (i % 2) == 0 ? Colours.TextColour : Colours.TextAlt;
            text.Position = position + new Vector2f(14, 70 + (i * 18));
            _window.Draw(text);
            i++;
        }
    }
}