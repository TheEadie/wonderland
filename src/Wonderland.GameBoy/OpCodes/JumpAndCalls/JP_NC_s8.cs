namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record JP_NC_s8() : OpCode(
    0x30,
    "JP NC s8",
    2,
    12,
    [
        (r, m, _) =>
            {
                r.SubOp_SignedByte = m.GetSignedMemory(r.PC++);
                return false;
            },
        (r, _, _) =>
            {
                if (r.FlagC)
                {
                    return true;
                }

                r.PC = Convert.ToUInt16(r.PC + r.SubOp_SignedByte);
                return false;
            },
        (_, _, _) => true
    ]);
