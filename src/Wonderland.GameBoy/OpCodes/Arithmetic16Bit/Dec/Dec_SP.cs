namespace Wonderland.GameBoy.OpCodes.Arithmetic16Bit.Dec;

public record Dec_SP() : OpCode(
    0x3B,
    "DEC SP",
    1,
    8,
    [
        (r, _, _) =>
            {
                r.SP--;
                return false;
            },
        (_, _, _) => true
    ]);
