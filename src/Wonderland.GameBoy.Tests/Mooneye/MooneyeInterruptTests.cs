namespace Wonderland.GameBoy.Tests.Mooneye;

public class MooneyeInterruptTests
{
    private static readonly string[] SubTests =
    [
        "ei_sequence",
        "ei_timing",
        "halt_ime0_ei",
        "halt_ime0_nointr_timing",
        "halt_ime1_timing",
        "if_ie_registers",
        "intr_timing",
        "rapid_di_ei",
        "reti_intr_timing",
        "reti_timing"
    ];

    [TestCaseSource(nameof(SubTests))]
    public void RunInterruptAcceptanceTest(string subTest) => MooneyeRunner.Run("acceptance", $"{subTest}.gb");

    [Test]
    public void IePush() => MooneyeRunner.Run("acceptance", "interrupts", "ie_push.gb");
}
