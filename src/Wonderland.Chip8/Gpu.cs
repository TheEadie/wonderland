namespace Wonderland.Chip8;

public class Gpu
{
    private readonly bool[,] _vRam;

    public bool HighResolutionMode { get; set; }
    public int Width { get; }
    public int Height { get; }

    public Gpu()
    {
        Width = 128;
        Height = 64;
        _vRam = new bool[Width, Height];
        HighResolutionMode = false;
    }

    public bool[,] GetVRam()
    {
        return _vRam;
    }

    public void Clear()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public void ScrollRight()
    {
        const int pixels = 4;

        for (var x = Width - 1; x >= pixels; x--)
        {
            for (var y = 0; y < Height; y++)
            {
                _vRam[x, y] = _vRam[x - pixels, y];
            }
        }

        for (var x = pixels - 1; x >= 0; x--)
        {
            for (var y = 0; y < Height; y++)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public void ScrollLeft()
    {
        const int pixels = 4;

        for (var x = 0; x < Width - pixels; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _vRam[x, y] = _vRam[x + pixels, y];
            }
        }

        for (var x = Width - pixels; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public void ScrollDown(int pixels)
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = Height - 1; y >= pixels; y--)
            {
                _vRam[x, y] = _vRam[x, y - pixels];
            }
        }

        for (var x = 0; x < Width; x++)
        {
            for (var y = pixels - 1; y >= 0; y--)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public bool DrawSprite(int xStart, int yStart, byte[] bytes, int width, bool wrap)
    {
        if (HighResolutionMode)
        {
            xStart %= Width;
            yStart %= Height;
        }
        else
        {
            xStart %= Width / 2;
            yStart %= Height / 2;
        }

        var spriteHeight = bytes.Length / width;
        var spriteWidth = 8 * width;

        var index = -1;
        var turnedOff = false;

        for (var y = 0; y < spriteHeight; y++)
        {
            for (var x = 0; x < spriteWidth; x++)
            {
                if (x % 8 == 0) { index++; }
                turnedOff |= SetRam(xStart + x, yStart + y, (bytes[index] >> (7 - x % 8) & 0b0000001) == 1, wrap);
            }
        }

        return turnedOff;
    }

    private bool SetRam(int x, int y, bool value, bool wrap)
    {
        if (!HighResolutionMode)
        {
            x *= 2;
            y *= 2;
        }

        if (wrap)
        {
            x %= Width;
            y %= Height;
        }

        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return false;
        }

        var original = _vRam[x, y];
        _vRam[x, y] ^= value;

        if (!HighResolutionMode)
        {
            _vRam[x, y + 1] ^= value;
            _vRam[x + 1, y] ^= value;
            _vRam[x + 1, y + 1] ^= value;
        }

        return original & !_vRam[x, y];
    }

    public void PrintDebug()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                Console.Write(_vRam[x, y] || y == 0 ? 'X' : ' ');
            }

            Console.Write(Environment.NewLine);
        }
    }
}
