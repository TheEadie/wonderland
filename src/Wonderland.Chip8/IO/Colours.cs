using SFML.Graphics;

namespace Wonderland.Chip8.IO;

public static class Colours
{
    public static readonly Color Background = new(50, 50, 50);
    public static readonly Color BackgroundDark = new(80, 100, 80);
    public static readonly Color Inactive = new(59, 68, 60);
    public static readonly Color TextColour = new(255, 255, 255);
    public static readonly Color TextAlt = new(204, 204, 204);
    public static readonly Color TextHighlight = new(253, 184, 51);
    public static readonly Color BorderInternal = new(170, 170, 170);
}
