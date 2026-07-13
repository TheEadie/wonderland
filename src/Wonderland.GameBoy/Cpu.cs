using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Wonderland.GameBoy.OpCodes;

namespace Wonderland.GameBoy;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class Cpu
{
    private const ushort InterruptEnableRegister = 0xFFFF;
    private const ushort InterruptFlagRegister = 0xFF0F;

    private readonly InterruptManager _interruptManager;
    private readonly Mmu _mmu;

    private readonly OpCodeHandler _opCodeHandler;
    private readonly Registers _registers;
    private readonly bool _trace;
    private OpCode _currentOpCode;
    private int _currentOpCodeMachineCycle;

    public Cpu(Mmu mmu, bool trace = true)
    {
        _mmu = mmu;
        _trace = trace;

        _registers = new Registers();
        _interruptManager = new InterruptManager();
        _opCodeHandler = new OpCodeHandler();
        _currentOpCode = new OpCode(0x00, "NULL", 1, 4, [(_, _, _) => true]);

        _registers.PC = 0x100;
        _registers.A = 0x01;
        _registers.F = 0x00;
        _registers.FlagZ = true;
        _registers.B = 0x00;
        _registers.C = 0x13;
        _registers.D = 0x00;
        _registers.E = 0xD8;
        _registers.H = 0x01;
        _registers.L = 0x4D;
        _registers.SP = 0xFFFE;
    }

    public ushort PC => _registers.PC;
    public ushort SP => _registers.SP;

    public void Step()
    {
        var opCodeComplete = RunNextSubInstruction();
        if (opCodeComplete)
        {
            _interruptManager.OnInstructionComplete();
            var pending = PendingInterrupts();
            if (_interruptManager.InterruptsEnabled && pending != 0)
            {
                var interruptBit = BitOperations.TrailingZeroCount(pending);
                _currentOpCode = BuildInterruptDispatch(interruptBit);
            }
            else
            {
                _currentOpCode = FetchAndDecode();
            }

            _currentOpCodeMachineCycle = 0;
        }
        else
        {
            _currentOpCodeMachineCycle++;
        }
    }

    private bool RunNextSubInstruction() =>
        _currentOpCode.Steps[_currentOpCodeMachineCycle](_registers, _mmu, _interruptManager);

    private byte PendingInterrupts() =>
        (byte)(_mmu.GetMemory(InterruptEnableRegister) & _mmu.GetMemory(InterruptFlagRegister) & 0x1F);

    private static OpCode BuildInterruptDispatch(int interruptBit)
    {
        var vector = (ushort)(0x40 + (interruptBit * 8));
        return new OpCode(0x00, "INT", 1, 20,
        [
            (_, _, i) =>
                {
                    i.DisableInterrupts();
                    return false;
                },
            (_, _, _) => false,
            (r, m, _) =>
                {
                    m.WriteMemory(--r.SP, (byte)(r.PC >> 8));
                    return false;
                },
            (r, m, _) =>
                {
                    m.WriteMemory(--r.SP, (byte)r.PC);
                    return false;
                },
            (r, m, _) =>
                {
                    m.WriteMemory(InterruptFlagRegister,
                        (byte)(m.GetMemory(InterruptFlagRegister) & ~(1 << interruptBit)));
                    r.PC = vector;
                    return true;
                }
        ]);
    }

    private OpCode FetchAndDecode()
    {
        var memory = _registers.PC;
        var opCode = _mmu.GetMemory(memory);
        var decoded = opCode == 0xCB
            ? _opCodeHandler.LookupCb(_mmu.GetMemory((ushort)(memory + 1)))
            : _opCodeHandler.Lookup(opCode);

        if (_trace)
        {
            Console.WriteLine(
                $"{memory:X4} - {opCode:X2}: {decoded.Description,-20}{_registers} : "
                + $"{_mmu.GetMemory((ushort)(_registers.PC + 1)):X2} {_mmu.GetMemory((ushort)(_registers.PC + 2)):X2}");
        }

        _registers.PC += (ushort)(opCode == 0xCB ? 2 : 1);
        return decoded;
    }
}
