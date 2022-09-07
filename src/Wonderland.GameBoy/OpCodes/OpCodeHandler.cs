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
            // 8-bit Load Instructions

            #region 8-bit load

            #region r=r

            {
                0x7F, new OpCode(0x7F, "LD A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.A;
                            return true;
                        }
                    })
            },
            {
                0x78, new OpCode(0x40, "LD A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.B;
                            return true;
                        }
                    })
            },
            {
                0x79, new OpCode(0x79, "LD A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.C;
                            return true;
                        }
                    })
            },
            {
                0x7A, new OpCode(0x7A, "LD A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.D;
                            return true;
                        }
                    })
            },
            {
                0x7B, new OpCode(0x7B, "LD A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.E;
                            return true;
                        }
                    })
            },
            {
                0x7C, new OpCode(0x7C, "LD A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.H;
                            return true;
                        }
                    })
            },
            {
                0x7D, new OpCode(0x7D, "LD A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.A = r.L;
                            return true;
                        }
                    })
            },
            {
                0x47, new OpCode(0x47, "LD B, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.A;
                            return true;
                        }
                    })
            },
            {
                0x40, new OpCode(0x40, "LD B, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.B;
                            return true;
                        }
                    })
            },
            {
                0x41, new OpCode(0x41, "LD B, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.C;
                            return true;
                        }
                    })
            },
            {
                0x42, new OpCode(0x42, "LD B, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.D;
                            return true;
                        }
                    })
            },
            {
                0x43, new OpCode(0x43, "LD B, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.E;
                            return true;
                        }
                    })
            },
            {
                0x44, new OpCode(0x43, "LD B, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.H;
                            return true;
                        }
                    })
            },
            {
                0x45, new OpCode(0x45, "LD B, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.L;
                            return true;
                        }
                    })
            },
            {
                0x4F, new OpCode(0x4F, "LD C, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.C = r.A;
                            return true;
                        }
                    })
            },
            {
                0x48, new OpCode(0x48, "LD C, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.C = r.B;
                            return true;
                        }
                    })
            },
            {
                0x49, new OpCode(0x49, "LD C, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.C = r.C;
                            return true;
                        }
                    })
            },
            {
                0x4A, new OpCode(0x4A, "LD C, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.C = r.D;
                            return true;
                        }
                    })
            },
            {
                0x4B, new OpCode(0x4B, "LD C, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.B = r.E;
                            return true;
                        }
                    })
            },
            {
                0x4C, new OpCode(0x4C, "LD C, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.C = r.H;
                            return true;
                        }
                    })
            },
            {
                0x4D, new OpCode(0x4D, "LD C, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.C = r.L;
                            return true;
                        }
                    })
            },
            {
                0x57, new OpCode(0x57, "LD D, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.A;
                            return true;
                        }
                    })
            },
            {
                0x50, new OpCode(0x50, "LD D, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.B;
                            return true;
                        }
                    })
            },
            {
                0x51, new OpCode(0x51, "LD D, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.C;
                            return true;
                        }
                    })
            },
            {
                0x52, new OpCode(0x52, "LD D, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.D;
                            return true;
                        }
                    })
            },
            {
                0x53, new OpCode(0x53, "LD D, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.E;
                            return true;
                        }
                    })
            },
            {
                0x54, new OpCode(0x54, "LD D, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.H;
                            return true;
                        }
                    })
            },
            {
                0x55, new OpCode(0x55, "LD D, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.D = r.L;
                            return true;
                        }
                    })
            },
            {
                0x5F, new OpCode(0x5F, "LD E, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.A;
                            return true;
                        }
                    })
            },
            {
                0x58, new OpCode(0x58, "LD E, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.B;
                            return true;
                        }
                    })
            },
            {
                0x59, new OpCode(0x59, "LD E, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.C;
                            return true;
                        }
                    })
            },
            {
                0x5A, new OpCode(0x5A, "LD E, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.D;
                            return true;
                        }
                    })
            },
            {
                0x5B, new OpCode(0x5B, "LD E, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.E;
                            return true;
                        }
                    })
            },
            {
                0x5C, new OpCode(0x5C, "LD E, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.H;
                            return true;
                        }
                    })
            },
            {
                0x5D, new OpCode(0x5D, "LD E, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.E = r.L;
                            return true;
                        }
                    })
            },
            {
                0x67, new OpCode(0x67, "LD H, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.A;
                            return true;
                        }
                    })
            },
            {
                0x60, new OpCode(0x60, "LD H, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.B;
                            return true;
                        }
                    })
            },
            {
                0x61, new OpCode(0x61, "LD H, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.C;
                            return true;
                        }
                    })
            },
            {
                0x62, new OpCode(0x62, "LD H, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.D;
                            return true;
                        }
                    })
            },
            {
                0x63, new OpCode(0x63, "LD H, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.E;
                            return true;
                        }
                    })
            },
            {
                0x64, new OpCode(0x64, "LD H, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.H;
                            return true;
                        }
                    })
            },
            {
                0x65, new OpCode(0x65, "LD H, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.H = r.L;
                            return true;
                        }
                    })
            },
            {
                0x6F, new OpCode(0x6F, "LD L, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.A;
                            return true;
                        }
                    })
            },
            {
                0x68, new OpCode(0x68, "LD L, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.B;
                            return true;
                        }
                    })
            },
            {
                0x69, new OpCode(0x69, "LD L, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.C;
                            return true;
                        }
                    })
            },
            {
                0x6A, new OpCode(0x6A, "LD L, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.D;
                            return true;
                        }
                    })
            },
            {
                0x6B, new OpCode(0x6B, "LD L, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.E;
                            return true;
                        }
                    })
            },
            {
                0x6C, new OpCode(0x6C, "LD L, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.H;
                            return true;
                        }
                    })
            },
            {
                0x6D, new OpCode(0x6D, "LD L, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.L = r.L;
                            return true;
                        }
                    })
            },

            #endregion

            #region r=n

            {
                0x3E, new OpCode(0x3E, "LD A, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x06, new OpCode(0x06, "LD B, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.B = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x0E, new OpCode(0x0E, "LD C, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.C = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x16, new OpCode(0x16, "LD D, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.D = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x1E, new OpCode(0x1E, "LD E, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.E = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x26, new OpCode(0x26, "LD H, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.H = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x2E, new OpCode(0x2E, "LD L, u8", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.L = m.GetMemory(r.PC++);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region r=(HL)

            {
                0x7E, new OpCode(0x7E, "LD A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x46, new OpCode(0x46, "LD B, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.B = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x4E, new OpCode(0x4E, "LD C, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.C = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x56, new OpCode(0x56, "LD D, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.D = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x5E, new OpCode(0x5E, "LD E, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.E = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x66, new OpCode(0x66, "LD H, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.H = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x6E, new OpCode(0x6E, "LD L, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.L = m.GetMemory(r.HL);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region (HL)=r

            {
                0x77, new OpCode(0x77, "LD (HL), A", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.A);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x70, new OpCode(0x70, "LD (HL), B", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.B);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x71, new OpCode(0x71, "LD (HL), C", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.C);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x72, new OpCode(0x72, "LD (HL), D", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.D);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x73, new OpCode(0x73, "LD (HL), E", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.E);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x74, new OpCode(0x74, "LD (HL), H", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.H);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x75, new OpCode(0x75, "LD (HL), L", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.L);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #endregion

            // 16-bit Load Instructions

            #region 16-bit load

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

            #endregion

            // CPU Control Instructions

            #region CPU Control

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
                        (_, _, i) =>
                        {
                            i.EnableInterruptsWithDelay();
                            return true;
                        }
                    })
            },

            #endregion

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