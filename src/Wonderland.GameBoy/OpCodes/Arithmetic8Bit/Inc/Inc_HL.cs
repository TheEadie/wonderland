namespace Wonderland.GameBoy.OpCodes.Arithmetic8Bit.Inc;

public record Inc_HL() : OpCode(
    0x34,
    "INC (HL)",
    1,
    12,
    [
        (r, m, _) =>
            {
                var value = m.GetMemory(r.HL);
                r.FlagH = (value & 0b_0000_1111) == 0b_0000_1111;
                value++;
                r.FlagZ = value == 0;
                r.FlagN = false;
                m.WriteMemory(r.HL, value);
                return true;
            }
    ]);
