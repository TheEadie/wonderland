using u8 = byte;

namespace Wonderland.GameBoy.OpCodes.RotateShift;

public record RotateShift_r(
    u8 Value,
    string Mnemonic,
    string Target,
    Func<Registers, u8> Get,
    Action<Registers, u8> Set,
    Func<Registers, u8, u8> Transform) : OpCode(
    Value,
    $"{Mnemonic} {Target}",
    2,
    8,
    [
        (_, _, _) => false,
        (r, _, _) =>
            {
                Set(r, Transform(r, Get(r)));
                return true;
            }
    ]);
