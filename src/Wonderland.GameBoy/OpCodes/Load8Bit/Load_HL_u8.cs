namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_HL_u8() : OpCode(
    0x36,
    "LD (HL), u8",
    2,
    12,
    [
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(r.HL, r.SubOp_LSB);
                return false;
            },
        (_, _, _) => true
    ]);
