using SFML.Graphics;
using SFML.System;

namespace Wonderland.Chip8.IO.Tabs;

public class TabControl
{
    private readonly Vector2f _position;
    private readonly RenderWindow _window;
    private readonly ICollection<Tab> _tabs;
    private string _activeTab;

    public TabControl(Vector2f position, RenderWindow window)
    {
        _position = position;
        _window = window;
        _tabs = new List<Tab>();
        _activeTab = string.Empty;
    }

    public void Add(string name, ITabContent content)
    {
        var xPosition = _tabs.Sum(x => x.Name.Length) * 10 + _tabs.Count * 2 + _tabs.Count * 28;
        _tabs.Add(new Tab(_position + new Vector2f(xPosition, 0), name, tab => { _activeTab = tab.Name; }, _window, content));
        if (_activeTab == string.Empty) _activeTab = name;
    }

    public void Draw()
    {
        foreach (var tab in _tabs)
        {
            tab.Active = _activeTab == tab.Name;
            tab.Draw(_window);
        }
    }
}
