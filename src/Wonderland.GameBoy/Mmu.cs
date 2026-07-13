using u8 = byte;
using u16 = ushort;
using s8 = sbyte;

namespace Wonderland.GameBoy;

public class Mmu
{
    private readonly u8[] _memory = new u8[65_536];
    private readonly Stream _serialOutput;

    public Mmu(Stream? serialOutput = null)
    {
        _serialOutput = serialOutput ?? new MemoryStream();
    }

    public void LoadCart(string filePath)
    {
        var rom = File.ReadAllBytes(filePath);
        rom[..Math.Min(rom.Length, 0x8000)].CopyTo(_memory, 0);
    }

    public u8 GetMemory(u16 location) => _memory[location];

    public s8 GetSignedMemory(u16 location) => unchecked((s8)_memory[location]);

    public void WriteMemory(u16 location, u8 value)
    {
        if (location == 0xFF02 && value == 0x81)
        {
            _serialOutput.WriteByte(GetMemory(0xFF01));
        }

        _memory[location] = value;
    }
}
