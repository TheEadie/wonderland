namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record JP_u16() : OpCode(
    0xC3,
    "JP u16",
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
        (r, _, _) =>
            {
                r.PC = Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB);
                return false;
            },
        (_, _, _) => true
    ]);
