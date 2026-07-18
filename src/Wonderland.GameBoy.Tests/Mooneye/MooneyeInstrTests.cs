namespace Wonderland.GameBoy.Tests.Mooneye;

public class MooneyeInstrTests
{
    [Test]
    public void Daa() => MooneyeRunner.Run("acceptance", "instr", "daa.gb");

    [Test]
    public void RegF() => MooneyeRunner.Run("acceptance", "bits", "reg_f.gb");
}
