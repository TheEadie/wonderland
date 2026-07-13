namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record RETI() : OpCode(
    0xD9,
    "RETI",
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
        (r, _, i) =>
            {
                r.PC = Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB);
                i.EnableInterrupts();
                return false;
            },
        (_, _, _) => true
    ]);
