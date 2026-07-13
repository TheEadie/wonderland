namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record RET() : OpCode(
    0xC9,
    "RET",
    1,
    16,
    [
        (r, m, _) =>
            {
                r.SubOp_LSB = m.GetMemory(r.SP++);
                return false;
            },
        (r, m, _) =>
            {
                r.SubOp_MSB = m.GetMemory(r.SP++);
                return false;
            },
        (r, _, _) =>
            {
                r.PC = Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB);
                return false;
            },
        (_, _, _) => true
    ]);
