namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Add;

public record Add_SP_s8() : OpCode(
    0xE8,
    "ADD SP, s8",
    2,
    16,
    [
        (r, m, _) =>
            {
                r.SubOp_SignedByte = m.GetSignedMemory(r.PC++);
                return false;
            },
        (_, _, _) => false,
        (r, _, _) =>
            {
                r.SP = OpCodeHandler.AddSignedByteToSp(r);
                return false;
            },
        (_, _, _) => true
    ]);
