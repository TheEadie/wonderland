using u8 = byte;

namespace Wonderland.GameBoy.OpCodes.SingleBit;

public record BIT(u8 Value, int Bit, string Target, Func<Registers, u8> Get) : OpCode(
    Value,
    $"BIT {Bit}, {Target}",
    2,
    8,
    [
        (_, _, _) => false,
        (r, _, _) =>
            {
                OpCodeHandler.TestBit(r, Bit, Get(r));
                return true;
            }
    ]);
