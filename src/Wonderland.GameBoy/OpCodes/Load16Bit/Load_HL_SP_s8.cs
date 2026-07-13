namespace Wonderland.GameBoy.OpCodes.Load16Bit;

public record Load_HL_SP_s8() : OpCode(
    0xF8,
    "LD HL, SP+s8",
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
                r.HL = OpCodeHandler.AddSignedByteToSp(r);
                return false;
            },
        (_, _, _) => true
    ]);
