using u8 = System.Byte;
using u16 = System.UInt16;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Wonderland.GameBoy;

public class Cpu
{
    public Registers Registers { get; }

    private Dictionary<u8, OpCode> _opCodes;
    private readonly u8[] _memory;

    // Temp state for OpCodes
    private u8 _lsb;
    private u8 _msb;

    public Cpu()
    {
        Registers = new Registers();
        _memory = new u8[65_535];
        _opCodes = new Dictionary<u8, OpCode>
        {
            {
                0x00, new OpCode(0x00, "NOP", 1, 4,
                    Array.Empty<Action>())
            },
            {
                0x01, new OpCode(0x01, "LD BC, ${0:X4}", 3, 12,
                    new Action[]
                    {
                        () => { _lsb = GetMemory(Registers.PC++); },
                        () =>
                        {
                            _msb = GetMemory(Registers.PC++);
                            Registers.BC = Bits.CreateU16(_msb, _lsb);
                        },
                    })
            }
        };
    }

    public u8 GetMemory(u16 location)
    {
        return _memory[location];
    }

    public void WriteMemory(u16 location, u8 value)
    {
        _memory[location] = value;
    }
}