using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Wonderland.Chip8.IO.Tabs;

public class Tab
{
    private readonly Vector2f _start;
    private readonly Vector2f _end;
    private readonly Vector2f _size;
    private readonly Action<Tab> _onClick;
    private readonly ITabContent _content;
    private bool _hover;
    private bool _clicked;
    public string Name { get; }
    public bool Active { get; set; }

    public Tab(Vector2f position, string name, Action<Tab> onClick, Window window, ITabContent content)
    {
        _start = position;
        _size = new Vector2f(name.Length * 10 + 28, 42);
        _end = position + _size;
        Name = name;
        _onClick = onClick;
        _content = content;
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
        var border = new RectangleShape(_size);
        border.OutlineThickness = 0;
        border.FillColor = Active || _hover || _clicked ? Colours.BackgroundDark : Colours.Inactive;
        border.Position = _start;
        parent.Draw(border);

        var text = TextFactory.Create();
        text.DisplayedString = Name;
        text.FillColor = Colours.TextColour;
        text.CharacterSize = 16;
        text.Position = _start + new Vector2f(14, 10);
        parent.Draw(text);
    
        text.CharacterSize = 14;

        if (Active)
        {
            _content.Draw();
        }
    }
    
    private static bool MouseIsInArea(Vector2f mouse, Vector2f start, Vector2f end)
    {
        return (start.X < mouse.X  && mouse.X < end.X &&
                start.Y < mouse.Y && mouse.Y < end.Y);
    }
}