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
            // CPU Control Instructions
            {
                0xCF, new OpCode(0xCF, "CCF", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagC = !r.FlagC;
                            r.FlagH = false;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0xC7, new OpCode(0xC7, "SCF", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagC = true;
                            r.FlagH = false;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x00, new OpCode(0x00, "NOP", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => true
                    })
            },
            {
                0x76, new OpCode(0x76, "HALT", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => throw new NotImplementedException()
                    })
            },
            {
                0x10, new OpCode(0x10, "STOP", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => throw new NotImplementedException()
                    })
            },
            {
                0xF3, new OpCode(0xF3, "DI", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, i) =>
                        {
                            i.DisableInterrupts();
                            return true;
                        }
                    })
            },
            {
                0xFB, new OpCode(0xFB, "EI", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, i) =>
                        {
                            i.EnableInterruptsWithDelay();
                            return true;
                        }
                    })
            },


            {
                0x01, new OpCode(0x01, "LD BC, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _tempState = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            var msb = m.GetMemory(r.PC++);
                            r.BC = Bits.CreateU16(msb, _tempState);
                            return true;
                        },
                    })
            },
            {
                0x11, new OpCode(0x11, "LD DE, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _tempState = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            var msb = m.GetMemory(r.PC++);
                            r.DE = Bits.CreateU16(msb, _tempState);
                            return true;
                        },
                    })
            },
            {
                0x21, new OpCode(0x21, "LD HL, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _tempState = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            var msb = m.GetMemory(r.PC++);
                            r.HL = Bits.CreateU16(msb, _tempState);
                            return true;
                        },
                    })
            },
            {
                0x31, new OpCode(0x31, "LD SP, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _tempState = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            var msb = m.GetMemory(r.PC++);
                            r.SP = Bits.CreateU16(msb, _tempState);
                            return true;
                        },
                    })
            },
            {
                0x02, new OpCode(0x02, "LD (BC), A", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.BC, r.A);
                            return true;
                        }
                    })
            },
            {
                0x03, new OpCode(0x03, "INC BC", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.BC++;
                            return true;
                        }
                    })
            },
            {
                0x04, new OpCode(0x04, "INC B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.BC & 0b_0000_1111) == 0b_0000_1111;
                            r.BC++;
                            r.FlagZ = r.BC == 0;
                            r.FlagN = false;
                            return true;
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