using SFML.Graphics;

namespace Wonderland.Chip8.IO.Text;

public static class TextFactory
{
    private static readonly SFML.Graphics.Text Text;

    static TextFactory()
    {
        Text = new SFML.Graphics.Text();
        Text.Font = new Font("resources/JetBrainsMonoNL-Regular.ttf");
        Text.CharacterSize = 14;
    }

    public static SFML.Graphics.Text Create()
    {
        return Text;
    }
}