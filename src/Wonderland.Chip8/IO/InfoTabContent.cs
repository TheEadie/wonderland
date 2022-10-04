using SFML.Graphics;
using SFML.System;
using Wonderland.Chip8.IO.Tabs;
using Wonderland.Chip8.IO.Text;

namespace Wonderland.Chip8.IO;

public class InfoTabContents : ITabContent
{
    private readonly Vector2f _position;
    private readonly RenderTarget _window;

    public InfoTabContents(Vector2f position, RenderTarget window)
    {
        _position = position;
        _window = window;
    }

    public void Draw()
    {
        DrawSection(_position + new Vector2f(0, 0), new Vector2f(690, 382 - 36), "Game Info");
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

    private void DrawContent(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"";
        text.FillColor = Colours.TextPrimary;
        text.Position = position + new Vector2f(24, 36 + 24);
        _window.Draw(text);
    }
}
