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

            #region other

            {
                0x36, new OpCode(0x36, "LD (HL), u8", 2, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x0A, new OpCode(0x0A, "LD A, (BC)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.BC);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0x1A, new OpCode(0x1A, "LD A, (DE)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.DE);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xFA, new OpCode(0xFA, "LD A, (u16)", 3, 16,
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
                            r.A = m.GetMemory(Bits.CreateU16(_msb, _lsb));
                            return false;
                        },
                        (_, _, _) => true
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
                0x12, new OpCode(0x12, "LD (DE), A", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.DE, r.A);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xEA, new OpCode(0xEA, "LD (u16), A", 3, 16,
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
                            m.WriteMemory(Bits.CreateU16(_msb, _lsb), r.A);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xF0, new OpCode(0xF0, "LD A, (FF00+u8)", 2, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(Bits.CreateU16(0xFF, _lsb));
                            return false;
                        },
                        (_, _, _) => true,
                    })
            },
            {
                0xE0, new OpCode(0xE0, "LD (FF00+u8), A", 2, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.PC++);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            m.WriteMemory(Bits.CreateU16(0xFF, _lsb), r.A);
                            return false;
                        },
                        (_, _, _) => true,
                    })
            },
            {
                0xF2, new OpCode(0xF2, "LD A, (FF00+C)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(Bits.CreateU16(0xFF, r.C));
                            return false;
                        },
                        (_, _, _) => true,
                    })
            },
            {
                0xE2, new OpCode(0xE2, "LD (FF00+C), A", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(Bits.CreateU16(0xFF, r.C), r.A);
                            return false;
                        },
                        (_, _, _) => true,
                    })
            },
            {
                0x22, new OpCode(0x22, "LD (HL+), A", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.A);
                            return false;
                        },
                        (r, _, _) =>
                        {
                            r.HL++;
                            return true;
                        },
                    })
            },
            {
                0x2A, new OpCode(0x2A, "LD A, (HL+)", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.HL);
                            return false;
                        },
                        (r, _, _) =>
                        {
                            r.HL++;
                            return true;
                        },
                    })
            },
            {
                0x32, new OpCode(0x32, "LD (HL-), A", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            m.WriteMemory(r.HL, r.A);
                            return false;
                        },
                        (r, _, _) =>
                        {
                            r.HL--;
                            return true;
                        },
                    })
            },
            {
                0x3A, new OpCode(0x3A, "LD A, (HL-)", 2, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            r.A = m.GetMemory(r.HL);
                            return false;
                        },
                        (r, _, _) =>
                        {
                            r.HL--;
                            return true;
                        },
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


            // 8-bit Arithmetic / Logic Instructions

            #region 8-bit Arithmetic/Logic

            #region ADD

            {
                0x87, new OpCode(0x87, "ADD A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0x80, new OpCode(0x80, "ADD A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0x81, new OpCode(0x81, "ADD A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0x82, new OpCode(0x82, "ADD A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0x83, new OpCode(0x83, "ADD A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0x84, new OpCode(0x84, "ADD A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0x85, new OpCode(0x85, "ADD A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Add(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0x86, new OpCode(0x86, "ADD A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Add(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xC6, new OpCode(0xC6, "ADD A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Add(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region ADD with Carry

            {
                0x8F, new OpCode(0x8F, "ADC A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0x88, new OpCode(0x88, "ADC A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0x89, new OpCode(0x89, "ADC A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0x8A, new OpCode(0x8A, "ADC A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0x8B, new OpCode(0x8B, "ADC A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0x8C, new OpCode(0x8C, "ADC A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0x8D, new OpCode(0x8D, "ADC A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            AddWithCarry(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0x8E, new OpCode(0x8E, "ADC A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            AddWithCarry(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xCE, new OpCode(0xCE, "ADC A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            AddWithCarry(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region SUB

            {
                0x97, new OpCode(0x97, "SUB A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0x90, new OpCode(0x90, "SUB A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0x91, new OpCode(0x91, "SUB A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0x92, new OpCode(0x92, "SUB A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0x93, new OpCode(0x93, "SUB A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0x94, new OpCode(0x94, "SUB A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0x95, new OpCode(0x95, "SUB A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Sub(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0x96, new OpCode(0x96, "SUB A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Sub(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xD6, new OpCode(0xD6, "SUB A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Sub(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region SUB with Carry

            {
                0x9F, new OpCode(0x9F, "SBC A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0x98, new OpCode(0x98, "SBC A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0x99, new OpCode(0x99, "SBC A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0x9A, new OpCode(0x9A, "SBC A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0x9B, new OpCode(0x9B, "SBC A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0x9C, new OpCode(0x9C, "SBC A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0x9D, new OpCode(0x9D, "SBC A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            SubWithCarry(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0x9E, new OpCode(0x9E, "SBC A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            SubWithCarry(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xDE, new OpCode(0xDE, "SBC A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            SubWithCarry(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region AND

            {
                0xA7, new OpCode(0xA7, "AND A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0xA0, new OpCode(0xA0, "AND A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0xA1, new OpCode(0xA1, "AND A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0xA2, new OpCode(0xA2, "AND A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0xA3, new OpCode(0xA3, "AND A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0xA4, new OpCode(0xA4, "AND A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0xA5, new OpCode(0xA5, "AND A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            And(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0xA6, new OpCode(0xA6, "AND A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            And(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xE6, new OpCode(0xE6, "AND A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            And(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region XOR

            {
                0xAF, new OpCode(0xAF, "XOR A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0xA8, new OpCode(0xA8, "XOR A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0xA9, new OpCode(0xA9, "XOR A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0xAA, new OpCode(0xAA, "XOR A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0xAB, new OpCode(0xAB, "XOR A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0xAC, new OpCode(0xAC, "XOR A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0xAD, new OpCode(0xAD, "XOR A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Xor(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0xAE, new OpCode(0xAE, "XOR A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Xor(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xEE, new OpCode(0xDE, "XOR A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Xor(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region OR

            {
                0xB7, new OpCode(0xB7, "OR A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0xB0, new OpCode(0xB0, "OR A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0xB1, new OpCode(0xB1, "OR A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0xB2, new OpCode(0xB2, "OR A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0xB3, new OpCode(0xB3, "OR A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0xB4, new OpCode(0xB4, "OR A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0xB5, new OpCode(0xB5, "OR A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Or(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0xB6, new OpCode(0xB6, "OR A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Or(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xF6, new OpCode(0xF6, "OR A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Or(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region CP

            {
                0xBF, new OpCode(0xBF, "CP A, A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.A);
                            return true;
                        }
                    })
            },
            {
                0xB8, new OpCode(0xB8, "CP A, B", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.B);
                            return true;
                        }
                    })
            },
            {
                0xB9, new OpCode(0xB9, "CP A, C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.C);
                            return true;
                        }
                    })
            },
            {
                0xBA, new OpCode(0xBA, "CP A, D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.D);
                            return true;
                        }
                    })
            },
            {
                0xBB, new OpCode(0xBB, "CP A, E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.E);
                            return true;
                        }
                    })
            },
            {
                0xBC, new OpCode(0xBC, "CP A, H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.H);
                            return true;
                        }
                    })
            },
            {
                0xBD, new OpCode(0xBD, "CP A, L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            Compare(r, r.L);
                            return true;
                        }
                    })
            },
            {
                0xBE, new OpCode(0xBE, "CP A, (HL)", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Compare(r, m.GetMemory(r.HL));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },
            {
                0xFE, new OpCode(0xFE, "CP A, u8", 1, 8,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            Compare(r, m.GetMemory(r.PC++));
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #region INC

            {
                0x3C, new OpCode(0x3C, "INC A", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.A & 0b_0000_1111) == 0b_0000_1111;
                            r.A++;
                            r.FlagZ = r.A == 0;
                            r.FlagN = false;
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
                            r.FlagH = (r.B & 0b_0000_1111) == 0b_0000_1111;
                            r.B++;
                            r.FlagZ = r.B == 0;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x0C, new OpCode(0x0C, "INC C", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.C & 0b_0000_1111) == 0b_0000_1111;
                            r.C++;
                            r.FlagZ = r.C == 0;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x14, new OpCode(0x14, "INC D", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.D & 0b_0000_1111) == 0b_0000_1111;
                            r.D++;
                            r.FlagZ = r.D == 0;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x1C, new OpCode(0x1C, "INC E", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.E & 0b_0000_1111) == 0b_0000_1111;
                            r.E++;
                            r.FlagZ = r.E == 0;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x24, new OpCode(0x24, "INC H", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.H & 0b_0000_1111) == 0b_0000_1111;
                            r.H++;
                            r.FlagZ = r.H == 0;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x2C, new OpCode(0x2C, "INC L", 1, 4,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, _, _) =>
                        {
                            r.FlagH = (r.L & 0b_0000_1111) == 0b_0000_1111;
                            r.L++;
                            r.FlagZ = r.L == 0;
                            r.FlagN = false;
                            return true;
                        }
                    })
            },
            {
                0x44, new OpCode(0x24, "INC (HL)", 1, 12,
                    new Func<Registers, Mmu, InterruptManager, bool>[]
                    {
                        (r, m, _) =>
                        {
                            _lsb = m.GetMemory(r.HL);
                            return false;
                        },
                        (r, m, _) =>
                        {
                            r.FlagH = (_lsb & 0b_0000_1111) == 0b_0000_1111;
                            _lsb++;
                            r.FlagZ = _lsb == 0;
                            r.FlagN = false;
                            m.WriteMemory(r.HL, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            },

            #endregion

            #endregion

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
                0xC3, new OpCode(0xC3, "JP u16", 3, 16,
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
                        (r, _, _) =>
                        {
                            r.PC = Bits.CreateU16(_msb, _lsb);
                            return false;
                        },
                        (_, _, _) => true
                    })
            }
        };
    }

    private static void And(Registers r, u8 value)
    {
        var result = r.A & value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = true;
        r.FlagC = false;
        r.A = (u8) result;
    }

    private static void Xor(Registers r, u8 value)
    {
        var result = r.A ^ value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8) result;
    }

    private static void Or(Registers r, u8 value)
    {
        var result = r.A | value;
        r.FlagZ = result == 0;
        r.FlagN = false;
        r.FlagH = false;
        r.FlagC = false;
        r.A = (u8) result;
    }

    private static void Compare(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
    }

    private static void Sub(Registers r, u8 value)
    {
        var result = r.A - value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8) (result);
    }

    private static void SubWithCarry(Registers r, u8 value)
    {
        var c = (r.FlagC ? 1 : 0);
        var result = r.A - value - c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = true;
        r.FlagH = (value & 0b_0000_1111) + c > (r.A & 0b_0000_1111);
        r.FlagC = result < 0;
        r.A = (u8) (result);
    }

    private static void Add(Registers r, u8 value)
    {
        var result = r.A + value;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8) (result);
    }

    private static void AddWithCarry(Registers r, u8 value)
    {
        var c = (r.FlagC ? 1 : 0);
        var result = r.A + value + c;
        r.FlagZ = (result & 0b_1111_1111) == 0;
        r.FlagN = false;
        r.FlagH = (r.A & 0b_0000_1111) + (value & 0b_0000_1111) + c > 0b_0000_1111;
        r.FlagC = result > 0b_1111_1111;
        r.A = (u8) (result);
    }

    public OpCode Lookup(u8 value)
    {
        if (!_opCodes.ContainsKey(value))
            throw new NotImplementedException($"Unknown Opcode: {value:X2}");
        return _opCodes[value];
    }
}