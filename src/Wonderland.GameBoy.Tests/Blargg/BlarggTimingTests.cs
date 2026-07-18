namespace Wonderland.GameBoy.Tests.Blargg;

public class BlarggTimingTests
{
    [Test]
    public void InstrTiming() => BlarggRunner.Run("instr_timing", "instr_timing.gb");

    [Test]
    public void HaltBug() => BlarggRunner.Run("halt_bug.gb");

    [TestCaseSource(nameof(MemTimingSubTests))]
    public void MemTiming(string subTest) => BlarggRunner.Run("mem_timing", "individual", $"{subTest}.gb");

    private static readonly string[] MemTimingSubTests =
    [
        "01-read_timing",
        "02-write_timing",
        "03-modify_timing"
    ];
}
