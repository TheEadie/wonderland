namespace Wonderland.GameBoy.OpCodes.Load8Bit;

public record Load_DE_A() : OpCode(
    0x12,
    "LD (DE), A",
    1,
    8,
    [
        (r, m, _) =>
            {
                m.WriteMemory(r.DE, r.A);
                return false;
            },
        (_, _, _) => true
    ]);
