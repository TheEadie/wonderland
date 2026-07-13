namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record RET_NC() : OpCode(
    0xD0,
    "RET NC",
    1,
    20,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                if (r.FlagC)
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
