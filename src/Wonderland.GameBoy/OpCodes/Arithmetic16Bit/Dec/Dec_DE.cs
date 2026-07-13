namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Dec;

public record Dec_DE() : OpCode(
    0x1B,
    "DEC DE",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.DE--;
                return false;
            },
        (_, _, _) => true
    ]);
