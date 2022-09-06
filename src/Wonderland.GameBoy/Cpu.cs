using Wonderland.GameBoy.OpCodes;
using u8 = System.Byte;
using u16 = System.UInt16;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Wonderland.GameBoy;

public class Cpu
{
    private readonly Registers _registers;
    private readonly Mmu _mmu;

    private readonly OpCodeHandler _opCodeHandler;
    private OpCode _currentOpCode;
    private int _currentOpCodeMachineCycle;

    public Cpu()
    {
        _registers = new Registers();
        _mmu = new Mmu();

        _opCodeHandler = new OpCodeHandler();
        _currentOpCode = new OpCode(0x00, "NULL", 0, 0, Array.Empty<Action<Registers, Mmu>>());
    }

    public void Step()
    {
        if (_currentOpCodeMachineCycle == _currentOpCode.MachineCycles)
        {
            _currentOpCode = FetchAndDecode();
            _currentOpCodeMachineCycle = 0;
        }

        _currentOpCode.Steps[_currentOpCodeMachineCycle](_registers, _mmu);
        _currentOpCodeMachineCycle++;
    }

    private OpCode FetchAndDecode()
    {
        return _opCodeHandler.Lookup(_mmu.GetMemory(_registers.PC));
    }
}