using Wonderland.GameBoy.OpCodes;

namespace Wonderland.GameBoy;

public class Cpu
{
    private readonly InterruptManager _interruptManager;
    private readonly Mmu _mmu;

    private readonly OpCodeHandler _opCodeHandler;
    private readonly Registers _registers;
    private OpCode _currentOpCode;
    private int _currentOpCodeMachineCycle;

    public Cpu(Mmu mmu, InterruptManager  interruptManager)
    {
        _mmu = mmu;

        _registers = new Registers();
        _interruptManager = interruptManager;
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

    public void Step()
    {
        if (_interruptManager.HaltCpu)
        {
            if (!_interruptManager.AnyPending())
            {
                return;
            }
            _interruptManager.HaltCpu  = false;
            _currentOpCode = _interruptManager.CheckAndDispatch() ?? FetchAndDecode();
            _currentOpCodeMachineCycle = 0;
            return;
        }

        var opCodeComplete = RunNextSubInstruction();

        if (opCodeComplete)
        {
            _currentOpCode = _interruptManager.CheckAndDispatch() ?? FetchAndDecode();
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
        var memory = _registers.PC;
        var opCode = _mmu.GetMemory(memory);
        var decoded = opCode == 0xCB
            ? _opCodeHandler.LookupCb(_mmu.GetMemory((ushort)(memory + 1)))
            : _opCodeHandler.Lookup(opCode);

        if (_interruptManager.HaltBug)
        {
            _interruptManager.HaltBug = false;
        }
        else
        {
            _registers.PC += (ushort)(opCode == 0xCB ? 2 : 1);
        }

        return decoded;
    }
}
