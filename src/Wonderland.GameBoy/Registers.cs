using System.Diagnostics.CodeAnalysis;
using u8 = System.Byte;
using u16 = System.UInt16;

namespace Wonderland.GameBoy;

[SuppressMessage("ReSharper", "BuiltInTypeReferenceStyle")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class Registers
{
    public u8 A { get; set; }
    public u8 B { get; set; }
    public u8 C { get; set; }
    public u8 D { get; set; }
    public u8 E { get; set; }
    public u8 F { get; set; }
    public u8 H { get; set; }
    public u8 L { get; set; }

    public u16 PC { get; set; }
    public u16 SP { get; set; }

    public u16 AF
    {
        get => (u16) ((A << 8) | F);

        set
        {
            A = (u8) ((value & 0b_11111111_00000000) >> 8);
            F = (u8) (value & 0b_00000000_11111111);
        }
    }

    public u16 BC
    {
        get => (u16) ((B << 8) | C);

        set
        {
            B = (u8) ((value & 0b_11111111_00000000) >> 8);
            C = (u8) (value & 0b_00000000_11111111);
        }
    }

    public u16 DE
    {
        get => (u16) ((D << 8) | E);

        set
        {
            D = (u8) ((value & 0b_11111111_00000000) >> 8);
            E = (u8) (value & 0b_00000000_11111111);
        }
    }

    public u16 HL
    {
        get => (u16) ((H << 8) | L);

        set
        {
            H = (u8) ((value & 0b_11111111_00000000) >> 8);
            L = (u8) (value & 0b_00000000_11111111);
        }
    }

    public bool FlagZ
    {
        get => (F & 0b_10000000) != 0;
        set => F = (u8) (F & 0b_01111111 | (value ? 0b10000000 : 0b00000000));
    }

    public bool FlagN
    {
        get => (F & 0b_01000000) != 0;
        set => F = (u8) (F & 0b_10111111 | (value ? 0b01000000 : 0b00000000));
    }

    public bool FlagH
    {
        get => (F & 0b_00100000) != 0;
        set => F = (u8) (F & 0b_11011111 | (value ? 0b00100000 : 0b00000000));
    }

    public bool FlagC
    {
        get => (F & 0b_00010000) != 0;
        set => F = (u8) (F & 0b_11101111 | (value ? 0b00010000 : 0b00000000));
    }
}