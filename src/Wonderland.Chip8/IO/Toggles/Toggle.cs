using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Wonderland.Chip8.IO.Text;

namespace Wonderland.Chip8.IO.Toggles;

public class Toggle
{
    private readonly Vector2f _start;
    private readonly Vector2f _end;
    private readonly Vector2f _size;
    private readonly Action<Toggle> _onClick;
    private bool _hover;
    private bool _clicked;
    public bool Value { get; set; }

    public Toggle(Vector2f position, bool value, Action<Toggle> onClick, Window window)
    {
        _start = position;
        _size = new Vector2f(100, 24);
        _end = position + _size;
        Value = value;
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

    public void Draw(RenderTarget parent)
    {
        var middle = _size.X / 2;
        var border = new RectangleShape(new Vector2f(middle, _size.Y));
        border.OutlineThickness = 0;
        if (_hover || _clicked)
        {
            border.FillColor = Value ? Colours.HoverActive : Colours.Hover;
        }
        else
        {
            border.FillColor = Value ? Colours.Active : Colours.Inactive;
        }

        border.Position = _start;
        parent.Draw(border);

        var text = TextFactory.Create();
        text.DisplayedString = "On";
        text.FillColor = Colours.TextPrimary;
        text.CharacterSize = 16;
        text.Position = _start + new Vector2f(16, 2);
        parent.Draw(text);

        if (_hover || _clicked)
        {
            border.FillColor = !Value ? Colours.HoverActive : Colours.Hover;
        }
        else
        {
            border.FillColor = !Value ? Colours.Active : Colours.Inactive;
        }

        border.Position = _start + new Vector2f(middle, 0);
        parent.Draw(border);

        text.DisplayedString = "Off";
        text.Position = _start + new Vector2f(middle + 10, 2);
        parent.Draw(text);

        text.CharacterSize = 14;
    }

    private static bool MouseIsInArea(Vector2f mouse, Vector2f start, Vector2f end)
    {
        return (start.X < mouse.X && mouse.X < end.X &&
                start.Y < mouse.Y && mouse.Y < end.Y);
    }
}
