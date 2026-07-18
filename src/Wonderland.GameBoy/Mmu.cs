using u8 = byte;
using u16 = ushort;
using s8 = sbyte;

namespace Wonderland.GameBoy;

public class Mmu(Stream serialOutput, InterruptManager interruptManager, Timer timer)
{
    private readonly u8[] _memory = new u8[65_536];

    public void LoadCart(string filePath)
    {
        var rom = File.ReadAllBytes(filePath);
        rom[..Math.Min(rom.Length, 0x8000)].CopyTo(_memory, 0);
    }

    public u8 GetMemory(u16 location) =>
        location switch
        {
            0xFFFF => interruptManager.IE,
            0xFF0F => interruptManager.IF,
            _ => _memory[location]
        };

    public s8 GetSignedMemory(u16 location) =>
        location switch
        {
            0xFFFF => unchecked((s8)interruptManager.IE),
            0xFF0F => unchecked((s8)interruptManager.IF),
            _ => unchecked((s8)_memory[location])
        };

    public void WriteMemory(u16 location, u8 value)
    {
        switch (location)
        {
            case 0xFFFF:
                interruptManager.IE = value;
                return;
            case 0xFF0F:
                interruptManager.IF = value;
                return;
            case 0xFF07:
                timer.TAC = value;
                return;
            case 0xFF06:
                timer.TMA = value;
                return;
            case 0xFF05:
                timer.TIMA = value;
                return;
            case 0xFF04:
                timer.DIV = value;
                return;
            case 0xFF02 when value == 0x81:
                serialOutput.WriteByte(GetMemory(0xFF01));
                return;
            default:
                _memory[location] = value;
                return;
        }
    }
}
