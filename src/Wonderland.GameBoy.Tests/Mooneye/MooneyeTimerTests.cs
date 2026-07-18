namespace Wonderland.GameBoy.Tests.Mooneye;

public class MooneyeTimerTests
{
    private static readonly string[] SubTests =
    [
        "div_write",
        "rapid_toggle",
        "tim00",
        "tim00_div_trigger",
        "tim01",
        "tim01_div_trigger",
        "tim10",
        "tim10_div_trigger",
        "tim11",
        "tim11_div_trigger",
        "tima_reload",
        "tima_write_reloading",
        "tma_write_reloading"
    ];

    [TestCaseSource(nameof(SubTests))]
    public void RunTimerAcceptanceTest(string subTest) => MooneyeRunner.Run("acceptance", "timer", $"{subTest}.gb");
}
