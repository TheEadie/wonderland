namespace Wonderland.GameBoy.OpCodes.JumpAndCalls;

public record JP_HL() : OpCode(
    0xE9,
    "JP HL",
    1,
    4,
    [
        (r, _, _) =>
            {
                r.PC = r.HL;
                return true;
            }
    ]);
