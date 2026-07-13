namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Dec;

public record Dec_BC() : OpCode(
    0x0B,
    "DEC BC",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.BC--;
                return false;
            },
        (_, _, _) => true
    ]);
