using u8 = byte;
using u16 = ushort;

namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record RST(u8 Value, u16 Vector) : OpCode(
    Value,
    $"RST 0x{Vector:X2}",
    1,
    16,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                m.WriteMemory(--r.SP, (u8)(r.PC >> 8));
                return false;
            },
        (r, m, _) =>
            {
                m.WriteMemory(--r.SP, (u8)r.PC);
                return false;
            },
        (r, _, _) =>
            {
                r.PC = Vector;
                return true;
            }
    ]);
