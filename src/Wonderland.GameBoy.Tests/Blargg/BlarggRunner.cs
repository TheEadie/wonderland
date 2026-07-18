using System.Text;

namespace Wonderland.GameBoy.Tests.Blargg;

internal static class BlarggRunner
{
    private const int StepLimit = 50_000_000;

    public static void Run(params string[] romSegments)
    {
        var romPath = RepoPaths.GameBoyRomPath(romSegments);
        var romRelativePath = string.Join('/', romSegments);

        if (romPath is null || !File.Exists(romPath))
        {
            Assert.Ignore($"ROM file not found: roms/gameboy/{romRelativePath}");
            return;
        }

        var serial = new MemoryStream();
        var emulator = new Emulator(serial);
        emulator.Load(romPath);

        var passed = false;
        string? failure = null;
        var lastLength = 0L;
        try
        {
            for (var step = 0; step < StepLimit; step++)
            {
                emulator.Step();
                if (serial.Length == lastLength)
                {
                    continue;
                }

                lastLength = serial.Length;
                var output = Encoding.ASCII.GetString(serial.ToArray());
                if (output.Contains("Passed", StringComparison.Ordinal))
                {
                    passed = true;
                    break;
                }

                if (output.Contains("Failed", StringComparison.Ordinal))
                {
                    failure = $"ROM reported failure.{Environment.NewLine}{output}";
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            failure = $"Exception while running '{romRelativePath}': {ex}{Environment.NewLine}"
                + $"Serial so far:{Environment.NewLine}{Encoding.ASCII.GetString(serial.ToArray())}";
        }

        if (passed)
        {
            return;
        }

        Assert.Fail(failure
            ?? $"Timed out after {StepLimit} steps without Passed/Failed.{Environment.NewLine}"
                + $"Serial so far:{Environment.NewLine}{Encoding.ASCII.GetString(serial.ToArray())}");
    }
}
