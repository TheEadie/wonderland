using SFML.Graphics;
using SFML.System;
using Wonderland.Chip8.IO.Tabs;
using Wonderland.Chip8.IO.Text;
using Wonderland.Chip8.IO.Toggles;

namespace Wonderland.Chip8.IO;

public class SettingsTabContents : ITabContent
{
    private readonly Vector2f _position;
    private readonly RenderWindow _window;
    private readonly Cpu _cpu;

    private readonly Toggle _shiftQuirkToggle;
    private readonly Toggle _loadStoreQuirksToggle;
    private readonly Toggle _jumpQuirkToggle;
    private readonly Toggle _logicQuirkToggle;
    private readonly Toggle _displayWaitToggle;
    private readonly Toggle _displayWrapToggle;

    public SettingsTabContents(Vector2f position, RenderWindow window, Cpu cpu)
    {
        _position = position;
        _window = window;
        _cpu = cpu;

        _shiftQuirkToggle = new Toggle(position + new Vector2f(240, 58), _cpu.QuirkShifting,
            _ => _cpu.QuirkShifting = !_cpu.QuirkShifting,
            window);
        _loadStoreQuirksToggle = new Toggle(position + new Vector2f(240, 58 + 36), _cpu.QuirkMemory,
            _ => _cpu.QuirkMemory = !_cpu.QuirkMemory,
            window);
        _jumpQuirkToggle = new Toggle(position + new Vector2f(240, 58 + (36 * 2)), _cpu.QuirkJumping,
            _ => _cpu.QuirkJumping = !_cpu.QuirkJumping,
            window);
        _logicQuirkToggle = new Toggle(position + new Vector2f(240, 58 + (36 * 3)), _cpu.QuirkVfReset,
            _ => _cpu.QuirkVfReset = !_cpu.QuirkVfReset,
            window);
        _displayWaitToggle = new Toggle(position + new Vector2f(240, 58 + (36 * 4)), _cpu.QuirkDisplayWait,
            _ => _cpu.QuirkDisplayWait = !_cpu.QuirkDisplayWait,
            window);
        _displayWrapToggle = new Toggle(position + new Vector2f(240, 58 + (36 * 5)), _cpu.QuirkWrapOverflow,
            _ => _cpu.QuirkWrapOverflow = !_cpu.QuirkWrapOverflow,
            window);
    }

    public void Draw()
    {
        DrawSection(_position + new Vector2f(0, 0), new Vector2f(345, 356), "Quirks");
        DrawQuirks(_position + new Vector2f(0, 0));

        DrawSection(_position + new Vector2f(345, 0), new Vector2f(345, 356), "Colours");
        DrawContent(_position + new Vector2f(0, 0));
    }

    private void DrawSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 36));
        headerSection.FillColor = Colours.BackgroundHeader;
        headerSection.Position = position;
        _window.Draw(headerSection);

        var text = TextFactory.Create();
        text.DisplayedString = title;
        text.CharacterSize = 14;
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(24, 8);

        _window.Draw(text);

        var sectionBody = new RectangleShape(size);
        sectionBody.FillColor = Colours.BackgroundLevel2;
        sectionBody.Position = position + new Vector2f(0, 36);
        _window.Draw(sectionBody);
    }

    private void DrawQuirks(Vector2f position)
    {
        var text = TextFactory.Create();
        text.CharacterSize = 14;
        text.DisplayedString =
            $"Shift Quirk\n\n" +
            $"Load Store Quirk\n\n" +
            $"Jump Quirk\n\n" +
            $"Logic Quirks\n\n" +
            $"Display Wait\n\n" +
            $"Display Wrap Overflow\n\n";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(24, 36 + 24);
        _window.Draw(text);

        _shiftQuirkToggle.Value = _cpu.QuirkShifting;
        _shiftQuirkToggle.Draw(_window);
        _loadStoreQuirksToggle.Value = _cpu.QuirkMemory;
        _loadStoreQuirksToggle.Draw(_window);
        _jumpQuirkToggle.Value = _cpu.QuirkJumping;
        _jumpQuirkToggle.Draw(_window);
        _logicQuirkToggle.Value = _cpu.QuirkVfReset;
        _logicQuirkToggle.Draw(_window);
        _displayWaitToggle.Value = _cpu.QuirkDisplayWait;
        _displayWaitToggle.Draw(_window);
        _displayWrapToggle.Value = _cpu.QuirkWrapOverflow;
        _displayWrapToggle.Draw(_window);
    }

    private void DrawContent(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(14, 34);
        _window.Draw(text);
    }
}