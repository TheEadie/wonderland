namespace Wonderland.GameBoy.Tests;

public class InterruptDispatchTests
{
    private const ushort InterruptEnableRegister = 0xFFFF;
    private const ushort InterruptFlagRegister = 0xFF0F;

    private static void Step(Cpu cpu, int times)
    {
        for (var i = 0; i < times; i++)
        {
            cpu.Step();
        }
    }

    [Test]
    public void SingleInterruptIsDispatched()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        // EI; NOP at 0x100
        mmu.WriteMemory(0x100, 0xFB);
        mmu.WriteMemory(0x101, 0x00);

        mmu.WriteMemory(InterruptEnableRegister, 0x01);
        mmu.WriteMemory(InterruptFlagRegister, 0x01);

        // NOP at the VBlank vector so the overlapped post-dispatch fetch (see below) is deterministic.
        mmu.WriteMemory(0x0040, 0x00);

        Step(cpu, 8);

        // Step() folds the fetch of the next opcode into the same call that completes
        // the previous one (existing fetch/execute overlap). So by the time Step 8
        // returns, the CPU has already fetched the (zero-initialised, i.e. NOP) byte
        // at the vector and advanced PC by one further. The vector jump itself is
        // observed via SP / the pushed return address / the cleared IF bit.
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0041));
        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFC));
        Assert.That(mmu.GetMemory(0xFFFC), Is.EqualTo(0x02));
        Assert.That(mmu.GetMemory(0xFFFD), Is.EqualTo(0x01));
        Assert.That(mmu.GetMemory(InterruptFlagRegister), Is.EqualTo(0x00));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void EachInterruptBitDispatchesToItsOwnVector(int bit)
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        mmu.WriteMemory(0x100, 0xFB); // EI
        mmu.WriteMemory(0x101, 0x00); // NOP

        var mask = (byte)(1 << bit);
        mmu.WriteMemory(InterruptEnableRegister, mask);
        mmu.WriteMemory(InterruptFlagRegister, mask);

        var vector = (ushort)(0x40 + (bit * 8));
        mmu.WriteMemory(vector, 0x00); // NOP, keeps the post-dispatch fetch deterministic

        Step(cpu, 8);

        Assert.That(cpu.PC, Is.EqualTo((ushort)(vector + 1)));
        Assert.That(mmu.GetMemory(InterruptFlagRegister), Is.EqualTo(0x00));
    }

    [Test]
    public void LowestBitIsServicedFirstAndOthersStayPending()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        mmu.WriteMemory(0x100, 0xFB); // EI
        mmu.WriteMemory(0x101, 0x00); // NOP

        mmu.WriteMemory(InterruptEnableRegister, 0x03);
        mmu.WriteMemory(InterruptFlagRegister, 0x03);

        mmu.WriteMemory(0x0040, 0x00); // NOP at VBlank vector

        Step(cpu, 8);

        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0041));
        Assert.That(mmu.GetMemory(InterruptFlagRegister), Is.EqualTo(0x02));
    }

    [Test]
    public void UpperThreeBitsAreIgnoredWhenCheckingForPendingInterrupts()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        mmu.WriteMemory(0x100, 0xFB); // EI
        mmu.WriteMemory(0x101, 0x00); // NOP
        mmu.WriteMemory(0x102, 0x00); // NOP (normal execution continues past here)

        mmu.WriteMemory(InterruptEnableRegister, 0xE0);
        mmu.WriteMemory(InterruptFlagRegister, 0xE0);

        // Step1: NULL completes -> fetch EI (PC->0x101).
        // Step2: EI completes (delay armed 2->1) -> fetch NOP@0x101 (PC->0x102).
        // Step3: NOP completes (delay 1->0, IME becomes true; no dispatch since bits 5-7 ignored)
        //        -> fetch NOP@0x102 (PC->0x103).
        Step(cpu, 3);

        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFE));
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0103));
    }

    [Test]
    public void PendingInterruptIsNotServicedWhileImeIsDisabled()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        // No EI executed anywhere: IME stays false throughout.
        mmu.WriteMemory(0x100, 0x00); // NOP
        mmu.WriteMemory(0x101, 0x00); // NOP

        mmu.WriteMemory(InterruptEnableRegister, 0x01);
        mmu.WriteMemory(InterruptFlagRegister, 0x01);

        Step(cpu, 3);

        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFE));
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0103));
    }

    [Test]
    public void EiDelaySpansTheWholeFollowingInstructionRegardlessOfItsMachineCycleCount()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        mmu.WriteMemory(0x100, 0xFB); // EI
        mmu.WriteMemory(0x101, 0x06); // LD B, u8 (2 M-cycles)
        mmu.WriteMemory(0x102, 0x42); // immediate operand

        mmu.WriteMemory(InterruptEnableRegister, 0x01);
        mmu.WriteMemory(InterruptFlagRegister, 0x01);

        mmu.WriteMemory(0x0040, 0x00); // NOP at VBlank vector

        // Step1: NULL completes -> fetch EI (PC->0x101).
        // Step2: EI completes (delay 2->1) -> fetch LD B,u8 (PC->0x102).
        // Step3: LD B,u8 M1 runs (reads operand, PC->0x103) -> not complete yet, delay still 1.
        Step(cpu, 3);

        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFE), "interrupt must not be serviced mid-instruction");
        Assert.That(cpu.PC, Is.Not.EqualTo((ushort)0x0040));

        // Step4: LD B,u8 M2 completes -> delay 1->0, IME becomes true, dispatch is loaded (cycle 0).
        // Steps5-9: dispatch M1-M5 run; M5 also folds in the fetch at the vector (see SingleInterruptIsDispatched).
        Step(cpu, 6);

        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFC), "interrupt must be dispatched once the following instruction completes");
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0041));
        Assert.That(mmu.GetMemory(InterruptFlagRegister), Is.EqualTo(0x00));
    }

    [Test]
    public void DiImmediatelyAfterEiCancelsThePendingEnable()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        mmu.WriteMemory(0x100, 0xFB); // EI
        mmu.WriteMemory(0x101, 0xF3); // DI
        mmu.WriteMemory(0x102, 0x00); // NOP

        mmu.WriteMemory(InterruptEnableRegister, 0x01);
        mmu.WriteMemory(InterruptFlagRegister, 0x01);

        // Step1: NULL completes -> fetch EI (PC->0x101).
        // Step2: EI completes (delay armed 2->1) -> fetch DI (PC->0x102).
        // Step3: DI completes (IME=false, delay cancelled to 0) -> fetch NOP (PC->0x103).
        // Step4: NOP completes -> fetch next byte (PC->0x104).
        Step(cpu, 4);

        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFE));
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0104));
    }

    [Test]
    public void RetiResumesServicingSoALaterPendingInterruptDispatches()
    {
        var mmu = new Mmu();
        var cpu = new Cpu(mmu, trace: false);

        mmu.WriteMemory(0x100, 0xFB); // EI
        mmu.WriteMemory(0x101, 0x00); // NOP

        mmu.WriteMemory(InterruptEnableRegister, 0x01);
        mmu.WriteMemory(InterruptFlagRegister, 0x01);

        mmu.WriteMemory(0x0040, 0xD9); // RETI at the VBlank vector (acts as the ISR)

        // Steps 1-8: same as SingleInterruptIsDispatched - dispatch pushes return PC (0x0102),
        // clears IME and IF, jumps to 0x0040, and (via fetch overlap) fetches RETI there.
        Step(cpu, 8);
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0041));
        Assert.That(mmu.GetMemory(InterruptFlagRegister), Is.EqualTo(0x00));

        // A fresh interrupt becomes pending while the "ISR" (just RETI) runs.
        mmu.WriteMemory(InterruptFlagRegister, 0x01);

        // Steps 9-12: RETI pops SP back to 0xFFFE, restores PC to 0x0102, and re-enables
        // IME immediately (no delay). On RETI's completing step, the pending interrupt is
        // recognised straight away since IME is already true (unlike EI's delayed enable).
        Step(cpu, 4);

        // Steps 13-17: the pending interrupt dispatches again - pushes 0x0102 back onto the
        // stack, clears IME and IF, and jumps to the vector once more.
        Step(cpu, 5);

        Assert.That(cpu.SP, Is.EqualTo((ushort)0xFFFC));
        Assert.That(mmu.GetMemory(0xFFFC), Is.EqualTo(0x02));
        Assert.That(mmu.GetMemory(0xFFFD), Is.EqualTo(0x01));
        Assert.That(mmu.GetMemory(InterruptFlagRegister), Is.EqualTo(0x00));
        Assert.That(cpu.PC, Is.EqualTo((ushort)0x0041));
    }
}
