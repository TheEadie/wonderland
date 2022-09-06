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

    public Cpu()
    {
        _registers = new Registers();
        _mmu = new Mmu();
        _interruptManager = new InterruptManager();

        _opCodeHandler = new OpCodeHandler();
        _currentOpCode = new OpCode(0x00, "NULL", 0, 0, Array.Empty<Func<Registers, Mmu, InterruptManager, bool>>());
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

    private OpCode FetchAndDecode() => _opCodeHandler.Lookup(_mmu.GetMemory(_registers.PC++));
}