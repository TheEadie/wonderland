using Wonderland.GameBoy.OpCodes;

namespace Wonderland.GameBoy;

public class Cpu
{
    public Registers Registers { get; }

    private readonly InterruptManager _interruptManager;
    private readonly Mmu _mmu;

    private readonly OpCodeHandler _opCodeHandler;
    private OpCode _currentOpCode;
    private int _currentOpCodeMachineCycle;

    public Cpu(Mmu mmu, InterruptManager  interruptManager)
    {
        _mmu = mmu;

        Registers = new Registers();
        _interruptManager = interruptManager;
        _opCodeHandler = new OpCodeHandler();
        _currentOpCode = new OpCode(0x00, "NULL", 1, 4, [(_, _, _) => true]);

        Registers.PC = 0x100;
        Registers.A = 0x01;
        Registers.F = 0x00;
        Registers.FlagZ = true;
        Registers.B = 0x00;
        Registers.C = 0x13;
        Registers.D = 0x00;
        Registers.E = 0xD8;
        Registers.H = 0x01;
        Registers.L = 0x4D;
        Registers.SP = 0xFFFE;
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
        _currentOpCode.Steps[_currentOpCodeMachineCycle](Registers, _mmu, _interruptManager);

    private OpCode FetchAndDecode()
    {
        var memory = Registers.PC;
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
            Registers.PC += (ushort)(opCode == 0xCB ? 2 : 1);
        }

        return decoded;
    }
}
