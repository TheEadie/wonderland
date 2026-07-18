namespace Wonderland.GameBoy.Tests.Blargg;

public class BlarggCpuInstrsTests
{
    private static readonly string[] SubTests =
    [
        "01-special",
        "02-interrupts",
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
    public void RunCpuInstrsSubTest(string subTest) =>
        BlarggRunner.Run("cpu_instrs", "individual", $"{subTest}.gb");
}
