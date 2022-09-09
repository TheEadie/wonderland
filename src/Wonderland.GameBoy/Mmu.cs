using u8 = System.Byte;
using u16 = System.UInt16;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Wonderland.GameBoy;

public class Mmu
{
    private readonly u8[] _memory;

    public Mmu()
    {
        _memory = new u8[65_535];
    }

    public void LoadCart(string filePath)
    {
        var rom = File.ReadAllBytes(filePath);
        rom[..0x3FFF].CopyTo(_memory, 0);
    }

    public u8 GetMemory(u16 location)
    {
        return _memory[location];
    }

    public void WriteMemory(u16 location, u8 value)
    {
        if (location == 0xFF01)
        {
            Console.WriteLine(value.ToString("x2"));
        }

        _memory[location] = value;
    }
}