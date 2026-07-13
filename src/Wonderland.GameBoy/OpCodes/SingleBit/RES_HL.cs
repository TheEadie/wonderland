using u8 = byte;

namespace Wonderland.GameBoy.OpCodes.SingleBit;

public record RES_HL(u8 Value, int Bit) : OpCode(
    Value,
    $"RES {Bit}, (HL)",
    2,
    16,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.HL);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, (u8)(r.SubOp_LSB & ~(1 << Bit)));
                return false;
            },
        (_, _, _) => true
    ]);
