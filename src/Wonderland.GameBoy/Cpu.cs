using Wonderland.GameBoy.OpCodes;
using u8 = System.Byte;
using u16 = System.UInt16;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Wonderland.GameBoy;

public class Cpu
{
    private readonly Registers _registers;
    private readonly Mmu _mmu;
    private readonly InterruptManager _interruptManager;

    private readonly OpCodeHandler _opCodeHandler;
    private OpCode _currentOpCode;
    private int _currentOpCodeMachineCycle;

    public Cpu(Mmu mmu)
    {
        _mmu = mmu;

        _registers = new Registers();
        _interruptManager = new InterruptManager();
        _opCodeHandler = new OpCodeHandler();
        _currentOpCode = new OpCode(0x00, "NULL", 1, 4,
            new Func<Registers, Mmu, InterruptManager, bool>[]
            {
                (_, _, _) => true
            });

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

    public void Step()
    {
        _interruptManager.Step();

        var opCodeComplete = RunNextSubInstruction();
        if (opCodeComplete)
        {
            _currentOpCode = FetchAndDecode();
            _currentOpCodeMachineCycle = 0;
        }
        else
        {
            _currentOpCodeMachineCycle++;
        }
    }

    private bool RunNextSubInstruction() =>
        _currentOpCode.Steps[_currentOpCodeMachineCycle](_registers, _mmu, _interruptManager);

    private OpCode FetchAndDecode()
    {
        var memory = ++_registers.PC;
        var opCode = _mmu.GetMemory(memory);
        var decoded = _opCodeHandler.Lookup(opCode);

        Console.WriteLine(
            $"{memory:X4} - {opCode:X2}: {decoded.Description.PadRight(20)}{_registers} : {_mmu.GetMemory((ushort) (_registers.PC + 1)):X2} {_mmu.GetMemory((ushort) (_registers.PC + 2)):X2}");

        return decoded;
    }
}