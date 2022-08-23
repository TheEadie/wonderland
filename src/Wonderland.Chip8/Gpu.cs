namespace Wonderland.Chip8;

public class Gpu
{
    private readonly bool[,] _vRam;

    public bool HighResolutionMode { get; set; }

    public Gpu()
    {
        _vRam = new bool[128, 64];
        HighResolutionMode = false;
    }

    public bool[,] GetVRam()
    {
        return _vRam;
    }

    public void Clear()
    {
        for (var x = 0; x < _vRam.GetLength(0); x++)
        {
            for (var y = 0; y < _vRam.GetLength(1); y++)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public void ScrollRight()
    {
        const int pixels = 4;
        var width = _vRam.GetLength(0);

        for (var x = width - 1; x >= pixels; x--)
        {
            for (var y = 0; y < _vRam.GetLength(1); y++)
            {
                _vRam[x, y] = _vRam[x - pixels, y];
            }
        }

        for (var x = pixels - 1; x >= 0; x--)
        {
            for (var y = 0; y < _vRam.GetLength(1); y++)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public void ScrollLeft()
    {
        const int pixels = 4;
        var width = _vRam.GetLength(0);

        for (var x = 0; x < width - pixels; x++)
        {
            for (var y = 0; y < _vRam.GetLength(1); y++)
            {
                _vRam[x, y] = _vRam[x + pixels, y];
            }
        }

        for (var x = width - pixels; x < width; x++)
        {
            for (var y = 0; y < _vRam.GetLength(1); y++)
            {
                _vRam[x, y] = false;
            }
        }
    }

    public void ScrollDown(int pixels)
    {
        var height = _vRam.GetLength(1);

        for (var x = 0; x < _vRam.GetLength(0); x++)
        {
            for (var y = height - 1; y >= pixels; y--)
            {
                _vRam[x, y] = _vRam[x, y - pixels];
            }
        }

        for (var x = 0; x < _vRam.GetLength(0); x++)
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
            xStart %= _vRam.GetLength(0);
            yStart %= _vRam.GetLength(1);
        }
        else
        {
            xStart %= _vRam.GetLength(0) / 2;
            yStart %= _vRam.GetLength(1) / 2;
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
        var width = _vRam.GetLength(0);
        var height = _vRam.GetLength(1);

        if (!HighResolutionMode)
        {
            x *= 2;
            y *= 2;
        }

        if (wrap)
        {
            x %= width;
            y %= height;
        }

        if (x < 0 || y < 0 || x >= width || y >= height)
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
        int width = _vRam.GetLength(0);
        int height = _vRam.GetLength(1);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(_vRam[x, y] || y == 0 ? 'X' : ' ');
            }

            Console.Write(Environment.NewLine);
        }
    }
}