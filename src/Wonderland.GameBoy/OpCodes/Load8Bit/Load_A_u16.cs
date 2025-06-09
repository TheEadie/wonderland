namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_A_u16() : OpCode(
    0xFA,
    "LD A, (u16)",
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
                r.A = m.GetMemory(Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB));
                return false;
            },
        (_, _, _) => true
    ]);
