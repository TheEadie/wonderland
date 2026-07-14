using System.Text;

namespace Wonderland.GameBoy.Tests;

public class CpuInstrsTests
{
    private const int StepLimit = 50_000_000;

    private static readonly string[] SubTests =
    [
        "01-special",
        "03-op sp,hl",
        "04-op r,imm",
        "05-op rp",
        "06-ld r,r",
        "07-jr,jp,call,ret,rst",
        "08-misc instrs",
        "09-op r,r",
        "10-bit ops",
        "11-op a,(hl)"
    ];

    [TestCaseSource(nameof(SubTests))]
    public void RunCpuInstrsSubTest(string subTest)
    {
        var repoRoot = FindRepoRoot();
        var romPath = repoRoot is null
            ? null
            : Path.Combine(repoRoot, "roms", "gameboy", "cpu_instrs", "individual", $"{subTest}.gb");

        if (romPath is null || !File.Exists(romPath))
        {
            Assert.Ignore($"ROM file not found: roms/gameboy/cpu_instrs/individual/{subTest}.gb");
            return;
        }

        var serial = new MemoryStream();
        var interruptManager = new InterruptManager();
        var mmu = new Mmu(serial, interruptManager);
        mmu.LoadCart(romPath);

        var cpu = new Cpu(mmu, interruptManager);

        var passed = false;
        string? failure = null;
        var lastLength = 0L;
        try
        {
            for (var step = 0; step < StepLimit; step++)
            {
                cpu.Step();
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
            failure = $"Exception while running '{subTest}': {ex}{Environment.NewLine}"
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

    private static string? FindRepoRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "wonderland.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }
}
