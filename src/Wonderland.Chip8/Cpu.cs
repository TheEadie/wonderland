namespace Wonderland.Chip8;

public class Cpu
{
    private readonly byte[] _memory;

    private readonly Gpu _gpu;
    private readonly IInputOutput _io;

    public ushort PC { get; private set; }
    public ushort I { get; private set; }
    public byte[] V { get; }
    public Stack<ushort> Stack { get; }

    public byte DelayTimer { get; private set; }
    public byte SoundTimer { get; private set; }

    private readonly Dictionary<byte, Action<Instruction>> _opCodes;
    private readonly Dictionary<byte, Action> _opCodes0;
    private readonly Dictionary<byte, Action<Instruction>> _opCodes8;
    private readonly Dictionary<byte, Action<Instruction>> _opCodesE;
    private readonly Dictionary<byte, Action<Instruction>> _opCodesF;

    public Cpu(byte[] memory, Gpu gpu, IInputOutput io)
    {
        _memory = memory;
        _gpu = gpu;
        _io = io;

        PC = 512;
        I = 0;
        V = new byte[16];
        Stack = new Stack<ushort>();

        _opCodes = new Dictionary<byte, Action<Instruction>>
        {
            {0x0, X0},
            {0x1, X1NNN_Jump},
            {0x2, X2NNN_CallSubroutine},
            {0x3, X3xNN_Equal},
            {0x4, X4xNN_NotEqual},
            {0x5, X5xyN_Equal},
            {0x6, X6xNN_Set},
            {0x7, X7xNN_Add},
            {0x8, X8},
            {0x9, X9xyN_NotEqual},
            {0xA, XANNN_SetI},
            {0xB, XBNNN_Jump},
            {0xC, XCxNN_Random},
            {0xD, XDxyN_DrawSprite},
            {0xE, XE},
            {0xF, XF},
        };

        _opCodes0 = new Dictionary<byte, Action>
        {
            {0xE0, X00E0_ClearScreen},
            {0XEE, X00EE_ReturnFromSubroutine}
        };

        _opCodes8 = new Dictionary<byte, Action<Instruction>>
        {
            {0x0, X8xy0_Set},
            {0x1, X8xy1_Or},
            {0x2, X8xy2_And},
            {0x3, X8xy3_Xor},
            {0x4, X8xy4_Add},
            {0x5, X8xy5_Subtract},
            {0x7, X8xy7_Subtract},
            {0x6, X8xy6_ShiftRight},
            {0xE, X8xyE_ShiftLeft}
        };

        _opCodesE = new Dictionary<byte, Action<Instruction>>
        {
            {0x9E, XEx9E_CheckForKey},
            {0xA1, XExA1_CheckForNotKey}
        };

        _opCodesF = new Dictionary<byte, Action<Instruction>>
        {
            {0x55, XFx55_StoreMemory},
            {0x65, XFx65_LoadMemory},
            {0x07, XFx07_GetDelayTimer},
            {0x15, XFx15_SetDelayTimer},
            {0x18, XFx18_SetSoundTimer},
            {0x0A, XFx0A_WaitForInput},
            {0x1E, XFx1E_AddToI},
            {0x33, XFx33_BinaryCodedDecimalConversion},
            {0x29, XFx29_SetIToFontChar}
        };
    }

    public void Step()
    {
        var opCode = _memory[PC] << 8;
        opCode += _memory[PC + 1];

        var nextMemory = new Instruction(
            (ushort)opCode,
            (byte)((opCode >> 12) & 0x000F),
            (byte)((opCode >> 8) & 0x000F),
            (byte)((opCode >> 4) & 0x000F),
            (byte)(opCode & 0x000F),
            (byte)(opCode & 0x00FF),
            (ushort)(opCode & 0x0FFF));
        PC += 2;

        if (!_opCodes.ContainsKey(nextMemory.Type))
            throw new NotImplementedException($"Unknown OpCode: {nextMemory.OpCode:x4}");
        _opCodes[nextMemory.Type](nextMemory);
    }

    public void Step60Hz()
    {
        if (DelayTimer > 0) DelayTimer--;
        if (SoundTimer > 0) SoundTimer--;
    }

    private void X0(Instruction instruction)
    {
        if (!_opCodes0.ContainsKey(instruction.NN))
            throw new NotImplementedException($"Unknown OpCode: {instruction.OpCode:x4}");
        _opCodes0[instruction.NN]();
    }

    private void X00E0_ClearScreen()
    {
        _gpu.Clear();
    }

    private void X00EE_ReturnFromSubroutine()
    {
        PC = Stack.Pop();
    }

    private void X1NNN_Jump(Instruction instruction)
    {
        PC = instruction.NNN;
    }

    private void X2NNN_CallSubroutine(Instruction instruction)
    {
        Stack.Push(PC);
        PC = instruction.NNN;
    }

    private void X3xNN_Equal(Instruction instruction)
    {
        if (V[instruction.X] == instruction.NN)
        {
            PC += 2;
        }
    }

    private void X4xNN_NotEqual(Instruction instruction)
    {
        if (V[instruction.X] != instruction.NN)
        {
            PC += 2;
        }
    }

    private void X5xyN_Equal(Instruction instruction)
    {
        if (V[instruction.X] == V[instruction.Y])
        {
            PC += 2;
        }
    }

    private void X6xNN_Set(Instruction instruction)
    {
        V[instruction.X] = instruction.NN;
    }

    private void X7xNN_Add(Instruction instruction)
    {
        V[instruction.X] += instruction.NN;
    }

    private void X8(Instruction instruction)
    {
        if (!_opCodes8.ContainsKey(instruction.N))
            throw new NotImplementedException($"Unknown OpCode: {instruction.OpCode:x4}");
        _opCodes8[instruction.N](instruction);
    }

    private void X8xy0_Set(Instruction instruction)
    {
        V[instruction.X] = V[instruction.Y];
    }

    private void X8xy1_Or(Instruction instruction)
    {
        V[instruction.X] |= V[instruction.Y];
    }

    private void X8xy2_And(Instruction instruction)
    {
        V[instruction.X] &= V[instruction.Y];
    }

    private void X8xy3_Xor(Instruction instruction)
    {
        V[instruction.X] ^= V[instruction.Y];
    }

    private void X8xy4_Add(Instruction instruction)
    {
        var add = V[instruction.X] + V[instruction.Y];
        V[instruction.X] = (byte)add;
        V[0xF] = (byte)(add > 255 ? 1 : 0);
    }

    private void X8xy5_Subtract(Instruction instruction)
    {
        var sub = V[instruction.X] - V[instruction.Y];
        V[instruction.X] = (byte)sub;
        V[0xF] = (byte)(sub > 0 ? 1 : 0);
    }

    private void X8xy7_Subtract(Instruction instruction)
    {
        var sub = V[instruction.Y] - V[instruction.X];
        V[instruction.X] = (byte)sub;
        V[0xF] = (byte)(sub > 0 ? 1 : 0);
    }

    private void X8xy6_ShiftRight(Instruction instruction)
    {
        V[0xF] = (byte)(V[instruction.X] & 1);
        V[instruction.X] >>= 1;
    }

    private void X8xyE_ShiftLeft(Instruction instruction)
    {
        V[0xF] = (byte)((V[instruction.X] >> 7) & 1);
        V[instruction.X] <<= 1;
    }

    private void X9xyN_NotEqual(Instruction instruction)
    {
        if (V[instruction.X] != V[instruction.Y])
        {
            PC += 2;
        }
    }

    private void XANNN_SetI(Instruction instruction)
    {
        I = instruction.NNN;
    }

    private void XBNNN_Jump(Instruction instruction)
    {
        PC = (ushort)(V[0x0] + instruction.NNN);
    }

    private void XCxNN_Random(Instruction instruction)
    {
        var random = new Random();
        var num = random.Next(256);
        V[instruction.X] = (byte)(num & instruction.NN);
    }

    private void XDxyN_DrawSprite(Instruction instruction)
    {
        var end = I + instruction.N;
        var sprite = _memory[I..end];
        var x = V[instruction.X];
        var y = V[instruction.Y];
        var turnedOff = _gpu.Draw(x, y, sprite);
        V[0xF] = turnedOff ? (byte)1 : (byte)0;
    }

    private void XE(Instruction instruction)
    {
        if (!_opCodesE.ContainsKey(instruction.NN))
            throw new NotImplementedException($"Unknown OpCode: {instruction.OpCode:x4}");
        _opCodesE[instruction.NN](instruction);
    }

    private void XEx9E_CheckForKey(Instruction instruction)
    {
        if (_io.Keys[V[instruction.X]])
        {
            PC += 2;
        }
    }

    private void XExA1_CheckForNotKey(Instruction instruction)
    {
        if (!_io.Keys[V[instruction.X]])
        {
            PC += 2;
        }
    }

    private void XF(Instruction instruction)
    {
        if (!_opCodesF.ContainsKey(instruction.NN))
            throw new NotImplementedException($"Unknown OpCode: {instruction.OpCode:x4}");
        _opCodesF[instruction.NN](instruction);
    }

    private void XFx29_SetIToFontChar(Instruction instruction)
    {
        I = (ushort)(V[instruction.X] + 0x50);
    }

    private void XFx33_BinaryCodedDecimalConversion(Instruction instruction)
    {
        var value = V[instruction.X];
        _memory[I] = (byte)(value / 100);
        _memory[I + 1] = (byte)(value / 10 % 10);
        _memory[I + 2] = (byte)(value % 10);
    }

    private void XFx55_StoreMemory(Instruction instruction)
    {
        V[0..(instruction.X + 1)].CopyTo(_memory, I);
    }

    private void XFx65_LoadMemory(Instruction instruction)
    {
        _memory[I..(I + instruction.X + 1)].CopyTo(V, 0);
    }

    private void XFx1E_AddToI(Instruction instruction)
    {
        I += V[instruction.X];
    }

    private void XFx07_GetDelayTimer(Instruction instruction)
    {
        V[instruction.X] = DelayTimer;
    }

    private void XFx15_SetDelayTimer(Instruction instruction)
    {
        DelayTimer = V[instruction.X];
    }

    private void XFx18_SetSoundTimer(Instruction instruction)
    {
        SoundTimer = V[instruction.X];
    }

    private void XFx0A_WaitForInput(Instruction instruction)
    {
        var pressedKey = _io.GetPressedKey();
        if (pressedKey is not null)
        {
            V[instruction.X] = pressedKey.Value;
        }
        else
        {
            PC -= 2;
        }
    }

    public void PrintDebug()
    {
        Console.WriteLine($"PC: {PC:x3}, I: {I:x3}");
        Console.WriteLine(string.Join(',', V.Select((x, i) => $"V{i:x}: {x:x}")));
    }

    public string GetOpCode()
    {
        var opCode = _memory[PC] << 8;
        opCode += _memory[PC + 1];
        return $"{opCode:x4}";
    }
}

internal record struct Instruction(ushort OpCode, byte Type, byte X, byte Y, byte N, byte NN, ushort NNN);
