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
                    new Action<Registers, Mmu>[]
                    {
                        (_, _) => { }
                    })
            },
            {
                0x01, new OpCode(0x01, "LD BC, u16", 3, 12,
                    new Action<Registers, Mmu>[]
                    {
                        (r, m) => { _tempState = m.GetMemory(r.PC++); },
                        (r, m) =>
                        {
                            var msb = m.GetMemory(r.PC++);
                            r.BC = Bits.CreateU16(msb, _tempState);
                        },
                    })
            },
            {
                0x02, new OpCode(0x02, "LD (BC), A", 1, 8,
                    new Action<Registers, Mmu>[]
                    {
                        (r, m) => { m.WriteMemory(r.BC, r.A); }
                    })
            },
            {
                0x03, new OpCode(0x03, "INC BC", 1, 4,
                    new Action<Registers, Mmu>[]
                    {
                        (r, _) => { r.BC++; }
                    })
            },
            {
                0x04, new OpCode(0x04, "INC B", 1, 4,
                    new Action<Registers, Mmu>[]
                    {
                        (r, _) =>
                        {
                            r.FlagH = (r.BC & 0b_0000_1111) == 0b_0000_1111;
                            r.BC++;
                            r.FlagZ = r.BC == 0;
                            r.FlagN = false;
                        }
                    })
            }
        };
    }

    public OpCode Lookup(u8 value)
    {
        return _opCodes[value];
    }
}