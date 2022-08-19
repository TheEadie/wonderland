using SFML.Graphics;

namespace Wonderland.Chip8.IO;

public static class TextFactory
{
    private static readonly Text Text;

    static TextFactory()
    {
        Text = new Text();
        Text.Font = new Font("resources/JetBrainsMonoNL-Regular.ttf");
        Text.CharacterSize = 14;    
    }

    public static Text Create()
    {
        return Text;
    }
}