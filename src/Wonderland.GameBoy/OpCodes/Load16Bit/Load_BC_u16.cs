namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Load_BC_u16() : OpCode(
    0x01,
    "LD BC, u16",
    3,
    12,
    [
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.PC++);
                return false;
            },
        (r, m, _) =>
            {
                r.SubOp_MSB = m.GetMemory(r.PC++);
                r.BC = Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB);
                return false;
            },
        (_, _, _) => true
    ]);
