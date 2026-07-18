namespace Wonderland.GameBoy.Tests.Mooneye;

/// <summary>
/// Runs mooneye-test-suite ROMs. These ROMs signal pass/fail by loading a fixed
/// pattern into B/C/D/E/H/L right before executing the "ld b,b" magic breakpoint
/// opcode: 3,5,8,13,21,34 on success, 0x42 repeated on failure. See quit.s at
/// https://github.com/Gekkio/mooneye-test-suite/blob/main/common/lib/quit.s
/// </summary>
internal static class MooneyeRunner
{
    private const int StepLimit = 50_000_000;

    private static readonly byte[] Success = [3, 5, 8, 13, 21, 34];
    private static readonly byte[] Failure = [0x42, 0x42, 0x42, 0x42, 0x42, 0x42];

    public static void Run(params string[] romSegments)
    {
        var romPath = RepoPaths.GameBoyRomPath(["mooneye", .. romSegments]);
        var romRelativePath = string.Join('/', romSegments);

        if (romPath is null || !File.Exists(romPath))
        {
            Assert.Ignore($"ROM file not found: roms/gameboy/mooneye/{romRelativePath}");
            return;
        }

        var emulator = new Emulator();
        emulator.Load(romPath);

        for (var step = 0; step < StepLimit; step++)
        {
            emulator.Step();

            var registers = emulator.Registers;
            byte[] current = [registers.B, registers.C, registers.D, registers.E, registers.H, registers.L];

            if (current.SequenceEqual(Success))
            {
                return;
            }

            if (current.SequenceEqual(Failure))
            {
                Assert.Fail($"ROM reported failure via magic breakpoint registers: {registers}");
                return;
            }
        }

        Assert.Fail($"Timed out after {StepLimit} steps without observing pass/fail registers.");
    }
}
