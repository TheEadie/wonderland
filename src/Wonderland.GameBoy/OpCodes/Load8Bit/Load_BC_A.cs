namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_BC_A() : OpCode(
    0x02,
    "LD (BC), A",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.BC, r.A);
                return false;
            },
        (_, _, _) => true
    ]);
