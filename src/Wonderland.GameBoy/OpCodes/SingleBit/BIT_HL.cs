using u8 = byte;

namespace Wonderland.GameBoy.OpCodes.SingleBit;

public record BIT_HL(u8 Value, int Bit) : OpCode(
    Value,
    $"BIT {Bit}, (HL)",
    2,
    12,
    [
        (_, _, _) => false,
        (r, m, _) =>
            {
                OpCodeHandler.TestBit(r, Bit, m.GetMemory(r.HL));
                return false;
            },
        (_, _, _) => true
    ]);
