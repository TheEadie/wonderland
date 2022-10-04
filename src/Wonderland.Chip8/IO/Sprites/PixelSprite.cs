using SFML.Graphics;
using SFML.System;

namespace Wonderland.Chip8.IO.Sprites;

public class PixelSprite
{
    public Color[,] Pixels { get; }

    private readonly RenderTarget _parent;
    private readonly int _width;
    private readonly int _height;
    private readonly byte[] _spriteArray;

    public PixelSprite(RenderTarget parent, int width, int height)
    {
        _parent = parent;
        _width = width;
        _height = height;
        Pixels = new Color[width, height];
        _spriteArray = new byte[width * height * 4];
    }

    public void Draw(Vector2f position)
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _spriteArray[(y * _width + x) * 4] = Pixels[x, y].R;
                _spriteArray[(y * _width + x) * 4 + 1] = Pixels[x, y].G;
                _spriteArray[(y * _width + x) * 4 + 2] = Pixels[x, y].B;
                _spriteArray[(y * _width + x) * 4 + 3] = 255;
            }
        }

        var image = new Image((uint)_width, (uint)_height, _spriteArray);
        var texture = new Texture(image);
        var sprite = new SFML.Graphics.Sprite(texture);
        sprite.Position = position;
        _parent.Draw(sprite);
        image.Dispose();
    }
}
