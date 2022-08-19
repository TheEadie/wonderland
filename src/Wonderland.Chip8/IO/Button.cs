using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Wonderland.Chip8.IO;

public class Button
{
    private readonly Vector2f _start;
    private readonly Vector2f _end;
    private readonly Vector2f _size;
    private readonly string _content;
    private readonly Action<Button> _onClick;
    private bool _hover;
    private bool _clicked;
    public bool Active { get; set; }

    public Button(Vector2f position, string content, Action<Button> onClick, Window window)
    {
        _start = position;
        _size = new Vector2f(42, 42);
        _end = position + _size;
        _content = content;
        _onClick = onClick;
        window.MouseButtonPressed += OnMouseButtonPressed;
        window.MouseButtonReleased += OnMouseButtonReleased;
        window.MouseMoved += OnMouseMoved;
    }

    private void OnMouseMoved(object? sender, MouseMoveEventArgs e)
    {
        var mouse = new Vector2f(e.X, e.Y);
        _hover = MouseIsInArea(mouse, _start, _end);
    }

    private void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e)
    {
        var mouse = new Vector2f(e.X, e.Y);
        if (MouseIsInArea(mouse, _start, _end))
        {
            _clicked = false;
            _onClick(this);
        }
    }

    private void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e)
    {
        var mouse = new Vector2f(e.X, e.Y);
        if (MouseIsInArea(mouse, _start, _end))
        {
            _clicked = true;
        }
    }

    public void Draw(RenderTarget parent, Vector2f mouse)
    {
        var border = new RectangleShape(_size);
        border.OutlineThickness = 1;
        border.OutlineColor = Colours.TextColour;
        border.FillColor = Active || _hover || _clicked ? Colours.BackgroundDark : Colours.Inactive;
        border.Position = _start;
        parent.Draw(border);

        var text = TextFactory.Create();
        text.DisplayedString = _content;
        text.FillColor = Colours.TextColour;
        text.CharacterSize = 16;
        text.Position = _start + new Vector2f(12, 10);
        parent.Draw(text);
    
        text.CharacterSize = 14;
    }
    
    private static bool MouseIsInArea(Vector2f mouse, Vector2f start, Vector2f end)
    {
        return (start.X < mouse.X  && mouse.X < end.X &&
                start.Y < mouse.Y && mouse.Y < end.Y);
    }
}