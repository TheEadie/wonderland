namespace Wonderland.Chip8.IO;

public class SfmlScreen : IScreen
{
    private SFML.Graphics.RenderWindow? _window;
    private readonly Gpu _gpu;

    public SfmlScreen(Gpu gpu)
    {
        _gpu = gpu;
    }

    public void Init()
    {
        _window = new SFML.Graphics.RenderWindow(new SFML.Window.VideoMode(640, 320), "Wonderland");
        _window.Clear();
    }

    public void DrawFrame()
    {
        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var sfmlArray = new SFML.Graphics.Color[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                sfmlArray[x, y] = vRam[x, y] ? SFML.Graphics.Color.Green : SFML.Graphics.Color.Black;
            }
        }

        var image = new SFML.Graphics.Image(sfmlArray);
        var texture = new SFML.Graphics.Texture(image);
        var sprite = new SFML.Graphics.Sprite(texture);
        sprite.Scale = new SFML.System.Vector2f(10, 10);
        _window.Draw(sprite);
        _window.Display();
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {

    }
}
