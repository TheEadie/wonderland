namespace Wonderland.Chip8;

public class Gpu
{
    private readonly bool[,] vRam;

    public Gpu()
    {
        vRam = new bool[64, 32];
    }

    public bool[,] GetVRam()
    {
        return vRam;
    }

    public void Clear()
    {
        for (var x = 0; x < vRam.GetLength(0); x++)
        {
            for (var y = 0; y < vRam.GetLength(1); y++)
            {
                vRam[x, y] = false;
            }
        }
    }

    public void Draw(int x, int yStart, byte[] bytes)
    {
        x %= 64;
        yStart %= 32;
        for (var y = 0; y < bytes.Length; y++)
        {
            vRam[x, yStart + y] ^= (bytes[y] >> 7 & 0b0000001) == 1;
            vRam[x + 1, yStart + y] ^= (bytes[y] >> 6 & 0b00000001) == 1;
            vRam[x + 2, yStart + y] ^= (bytes[y] >> 5 & 0b00000001) == 1;
            vRam[x + 3, yStart + y] ^= (bytes[y] >> 4 & 0b00000001) == 1;
            vRam[x + 4, yStart + y] ^= (bytes[y] >> 3 & 0b00000001) == 1;
            vRam[x + 5, yStart + y] ^= (bytes[y] >> 2 & 0b00000001) == 1;
            vRam[x + 6, yStart + y] ^= (bytes[y] >> 1 & 0b00000001) == 1;
            vRam[x + 7, yStart + y] ^= (bytes[y] & 0b00000001) == 1;
        }
    }

    public void PrintDebug()
    {
        int width = vRam.GetLength(0);
        int height = vRam.GetLength(1);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                Console.Write(vRam[x, y] || y == 0 ? 'X' : ' ');
            }
            Console.Write(Environment.NewLine);
        }
    }

}