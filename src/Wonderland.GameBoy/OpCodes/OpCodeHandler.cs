using u8 = System.Byte;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Wonderland.GameBoy.OpCodes;

public class OpCodeHandler
{
    private readonly Dictionary<u8, OpCode> _opCodes;

    // Yuck
    private u8 _tempState;

    public OpCodeHandler()
    {
        _opCodes = new Dictionary<u8, OpCode>
        {
            {
                0x00, new OpCode(0x00, "NOP", 1, 4,
                    Array.Empty<Action<Registers, Mmu>>())
            },
            {
                0x01, new OpCode(0x01, "LD BC, ${0:X4}", 3, 12,
                    new Action<Registers, Mmu>[]
                    {
                        (r, m) => { _tempState = m.GetMemory(r.PC++); },
                        (r, m) =>
                        {
                            var msb = m.GetMemory(r.PC++);
                            r.BC = Bits.CreateU16(msb, _tempState);
                        },
                    })
            }
        };
    }

    public OpCode Lookup(u8 value)
    {
        return _opCodes[value];
    }
}