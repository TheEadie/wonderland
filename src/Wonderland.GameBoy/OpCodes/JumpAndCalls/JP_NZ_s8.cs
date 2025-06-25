namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record JP_NZ_s8() : OpCode(
    0x20,
    "JP NZ s8",
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
                if (r.FlagZ)
                {
                    return true;
                }

                r.PC = Convert.ToUInt16(r.PC + r.SubOp_SignedByte);
                return false;
            },
        (_, _, _) => true
    ]);
