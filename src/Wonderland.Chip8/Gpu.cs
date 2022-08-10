namespace Wonderland.Chip8;

public class Gpu
{
    private readonly bool[,] _vRam;

    public Gpu()
    {
        _vRam = new bool[64, 32];
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

    public void Draw(int x, int yStart, byte[] bytes)
    {
        x %= 64;
        yStart %= 32;
        for (var y = 0; y < bytes.Length; y++)
        {
            SetRam(x, yStart + y, (bytes[y] >> 7 & 0b0000001) == 1);
            SetRam(x + 1, yStart + y, (bytes[y] >> 6 & 0b00000001) == 1);
            SetRam(x + 2, yStart + y, (bytes[y] >> 5 & 0b00000001) == 1);
            SetRam(x + 3, yStart + y, (bytes[y] >> 4 & 0b00000001) == 1);
            SetRam(x + 4, yStart + y, (bytes[y] >> 3 & 0b00000001) == 1);
            SetRam(x + 5, yStart + y, (bytes[y] >> 2 & 0b00000001) == 1);
            SetRam(x + 6, yStart + y, (bytes[y] >> 1 & 0b00000001) == 1);
            SetRam(x + 7, yStart + y, (bytes[y] & 0b00000001) == 1);
        }
    }

    private void SetRam(int x, int y, bool value)
    {
        if (x < 0 || y < 0 ||
            x >= _vRam.GetLength(0) ||
            y >= _vRam.GetLength(1))
            return;
        _vRam[x, y] ^= value;
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