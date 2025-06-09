namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_u16_A() : OpCode(
    0xEA,
    "LD (u16), A",
    3,
    16,
    [
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                r.SubOp_MSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB), r.A);
                return false;
            },
        (_, _, _) => true
    ]);
