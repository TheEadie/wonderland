using SFML.Graphics;
using SFML.System;

namespace Wonderland.Chip8.IO;

public class SettingsTabContents : ITabContent
{
    private readonly Vector2f _position;
    private readonly RenderTarget _window;

    public SettingsTabContents(Vector2f position, RenderTarget window)
    {
        _position = position;
        _window = window;
    }
    
    public void Draw()
    {
        DrawSection(_position + new Vector2f(1, 0), new Vector2f(320, 356), "Quirks");
        DrawContent(_position+ new Vector2f(1, 0));
        
        DrawSection(_position + new Vector2f(321, 0), new Vector2f(320, 356), "Colours");
        DrawContent(_position+ new Vector2f(1, 0));
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
    
    private void DrawContent(Vector2f position)
    {
        var text = TextFactory.Create();
        text.DisplayedString = $"";
        text.FillColor = Colours.TextColour;
        text.Position = position + new Vector2f(14, 34);
        _window.Draw(text);
    }
}