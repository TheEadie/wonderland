using SFML.Graphics;

namespace Wonderland.Chip8.IO;

public static class Colours
{
    public static readonly Color BackgroundLevel1 = new(50, 50, 50);
    public static readonly Color BackgroundLevel2 = new(68, 68, 68);
    public static readonly Color BackgroundHeader = new(80, 100, 80);

    public static readonly Color TextPrimary = new(255, 255, 255);
    public static readonly Color TextSecondary = new(204, 204, 204);
    public static readonly Color TextHighlight = new(253, 184, 51);

    public static readonly Color Inactive = new(80, 80, 80);
    public static readonly Color Active = new(80, 100, 80);
    public static readonly Color Hover = new(76, 86, 76);
    public static readonly Color HoverActive = new(82, 115, 86);
}