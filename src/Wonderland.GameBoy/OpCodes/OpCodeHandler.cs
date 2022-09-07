using u8 = System.Byte;
using u16 = System.UInt16;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Wonderland.GameBoy.OpCodes;

public class OpCodeHandler
{
    private readonly Dictionary<u8, OpCode> _opCodes;

    // Yuck
    private u8 _lsb;
    private u8 _msb;

    public OpCodeHandler()
    {
        _opCodes = new Dictionary<u8, OpCode>
        {
            // 16-bit Load Instructions
            {
                0x01, new OpCode(0x01, "LD BC, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            _msb = m.GetMemory(r.PC++);
                            r.BC = Bits.CreateU16(_msb, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x11, new OpCode(0x11, "LD DE, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            _msb = m.GetMemory(r.PC++);
                            r.DE = Bits.CreateU16(_msb, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x21, new OpCode(0x21, "LD HL, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            _msb = m.GetMemory(r.PC++);
                            r.HL = Bits.CreateU16(_msb, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x31, new OpCode(0x31, "LD SP, u16", 3, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            _msb = m.GetMemory(r.PC++);
                            r.SP = Bits.CreateU16(_msb, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x08, new OpCode(0x08, "LD (u16), SP", 3, 20,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            _msb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(Bits.CreateU16(_msb, _lsb), (u8) (r.SP & 0b_0000_0000_1111_1111));
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory((u16) (Bits.CreateU16(_msb, _lsb) + 1),
                                (u8) (r.SP & 0b_1111_1111_0000_0000 >> 8));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xF9, new OpCode(0xF9, "LD SP, HL", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.SP = r.HL;
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xC5, new OpCode(0xC5, "PUSH BC", 1, 16,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => false,
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.B);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.C);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xD5, new OpCode(0xD5, "PUSH DE", 1, 16,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => false,
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.D);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.E);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xE5, new OpCode(0xE5, "PUSH HL", 1, 16,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => false,
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.H);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.L);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xF5, new OpCode(0xF5, "PUSH AF", 1, 16,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (_, _, _) => false,
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.A);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.SP--, r.F);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xC1, new OpCode(0xC1, "POP BC", 1, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.C = m.GetMemory(r.SP++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            r.B = m.GetMemory(r.SP++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xD1, new OpCode(0xD1, "POP DE", 1, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.E = m.GetMemory(r.SP++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            r.D = m.GetMemory(r.SP++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xE1, new OpCode(0xE1, "POP HL", 1, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.L = m.GetMemory(r.SP++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            r.H = m.GetMemory(r.SP++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xF1, new OpCode(0xF1, "POP AF", 1, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.F = m.GetMemory(r.SP++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.SP++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

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
                0x02, new OpCode(0x02, "LD (BC), A", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.BC, r.A);
                            return false;
                        },
                        (_, _, _) => true
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