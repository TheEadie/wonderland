namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Dec;

public record Dec_HL() : OpCode(
    0x35,
    "DEC (HL)",
    1,
    12,
    [
        (r, m, _) =>
            {
                var value = m.GetMemory(r.HL);
                r.FlagH = (value & 0b_0000_1111) == 0b_0000_0000;
                value--;
                r.FlagZ = value == 0;
                r.FlagN = true;
                m.WriteMemory(r.HL, value);
                return true;
            }
    ]);
