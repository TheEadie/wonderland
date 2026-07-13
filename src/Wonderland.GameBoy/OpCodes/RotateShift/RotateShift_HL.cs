using u8 = byte;

namespace Wonderland.GameBoy.OpCodes.RotateShift;

public record RotateShift_HL(u8 Value, string Mnemonic, Func<Registers, u8, u8> Transform) : OpCode(
    Value,
    $"{Mnemonic} (HL)",
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
                m.WriteMemory(r.HL, Transform(r, r.SubOp_LSB));
                return false;
            },
        (_, _, _) => true
    ]);
