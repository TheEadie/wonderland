namespace Wonderland.GameBoy.OpCodes.Load8Bit.Register;

public record Load_E_B() : OpCode(
    0x58,
    "LD E, B",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.E = r.B;
                return true;
            }
    ]);
