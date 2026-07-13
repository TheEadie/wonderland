namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record RET_Z() : OpCode(
    0xC8,
    "RET Z",
    1,
    20,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                if (!r.FlagZ)
                {
                    return true;
                }

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
