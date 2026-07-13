using u8 = byte;

namespace Wonderland.GameBoy.OpCodes.SingleBit;

public record SET(u8 Value, int Bit, string Target, Func<Registers, u8> Get, Action<Registers, u8> Set) : OpCode(
    Value,
    $"SET {Bit}, {Target}",
    2,
    8,
    [
        (_, _, _) => false,
        (r, _, _) =>
            {
                Set(r, (u8)(Get(r) | (1 << Bit)));
                return true;
            }
    ]);
