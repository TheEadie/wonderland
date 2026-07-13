namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record CALL_Z_u16() : OpCode(
    0xCC,
    "CALL Z u16",
    3,
    24,
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
        (r, _, _) => !r.FlagZ,
        (r, m, _) =>
            {
                m.WriteMemory(--r.SP, (byte)(r.PC >> 8));
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(--r.SP, (byte)r.PC);
                return false;
            },
        (r, _, _) =>
            {
                r.PC = Bits.CreateU16(r.SubOp_MSB, r.SubOp_LSB);
                return true;
            }
    ]);
