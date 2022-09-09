﻿using Wonderland.GameBoy.OpCodes;
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