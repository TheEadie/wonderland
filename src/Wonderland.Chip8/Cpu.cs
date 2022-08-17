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

    private readonly Dictionary<byte, OpCode> _opCodes;
    private readonly Dictionary<byte, OpCode> _opCodes0;
    private readonly Dictionary<byte, OpCode> _opCodes8;
    private readonly Dictionary<byte, OpCode> _opCodesE;
    private readonly Dictionary<byte, OpCode> _opCodesF;

    public Cpu(byte[] memory, Gpu gpu, IInputOutput io)
    {
        _memory = memory;
        _gpu = gpu;
        _io = io;

        PC = 512;
        I = 0;
        V = new byte[16];
        Stack = new Stack<ushort>();

        _opCodes = new Dictionary<byte, OpCode>
        {
            {0x1, new OpCode(i => $"GOTO {i.NNN:x3}", i => PC = i.NNN)},
            {0x2, new OpCode(i => $"CALL {i.NNN:x3}", i =>
            {
                Stack.Push(PC);
                PC = i.NNN;
            })},
            {0x3, new OpCode(i => $"IF V{i.X:x} != {i.NN:x2}", i =>
            {
                if (V[i.X] == i.NN)
                {
                    PC += 2;
                }
            })},
            {0x4, new OpCode(i => $"IF V{i.X:x} == {i.NN:x2}", i =>
            {
                if (V[i.X] != i.NN)
                {
                    PC += 2;
                }
            })},
            {0x5, new OpCode(i => $"IF V{i.X:x} != V{i.Y:x}", i =>
            {
                if (V[i.X] == V[i.Y])
                {
                    PC += 2;
                }
            })},
            {0x6, new OpCode(i => $"V{i.X:x} = {i.NN:x2}", i => V[i.X] = i.NN)},
            {0x7, new OpCode(i => $"V{i.X:x} += {i.NN:x2}", i => V[i.X] += i.NN)},
            {0x9, new OpCode(i => $"IF V{i.X:x} == V{i.Y:X}", i =>
            {
                if (V[i.X] != V[i.Y])
                {
                    PC += 2;
                }
            })},
            {0xA, new OpCode(i => $"I = {i.NNN:x3}", i => I = i.NNN)},
            {0xB, new OpCode(i => $"GOTO V0 + {i.NNN:x2}", i => PC = (ushort)(V[0x0] + i.NNN))},
            {0xC, new OpCode(i => $"V{i.X} = RANDOM(0 - {i.NN:x2})", i =>
            {
                var random = new Random();
                var num = random.Next(256);
                V[i.X] = (byte)(num & i.NN);
            })},
            {0xD, new OpCode(i => $"DRAW ({i.X}, {i.Y}) x{i.N}", i =>
            {
                var end = I + i.N;
                var sprite = _memory[I..end];
                var x = V[i.X];
                var y = V[i.Y];
                var turnedOff = _gpu.Draw(x, y, sprite);
                V[0xF] = turnedOff ? (byte)1 : (byte)0;
            })},
        };

        _opCodes0 = new Dictionary<byte, OpCode>
        {
            {0xE0, new OpCode(_ => "CLEAR", _ => _gpu.Clear())},
            {0XEE, new OpCode(_ => "RETURN", _ => PC = Stack.Pop())}
        };

        _opCodes8 = new Dictionary<byte, OpCode>
        {
            {0x0, new OpCode(i => $"V{i.X:x} = V{i.Y:x}", i => V[i.X] = V[i.Y])},
            {0x1, new OpCode(i => $"V{i.X:x} |= V{i.Y:x}", i => V[i.X] |= V[i.Y])},
            {0x2, new OpCode(i => $"V{i.X:x} &= V{i.Y:x}", i => V[i.X] &= V[i.Y])},
            {0x3, new OpCode(i => $"V{i.X:x} ^= V{i.Y:x}", i => V[i.X] ^= V[i.Y])},
            {0x4, new OpCode(i => $"V{i.X:x} += V{i.Y:x}; Vf = CARRY", i =>
            {
                var add = V[i.X] + V[i.Y];
                V[i.X] = (byte)add;
                V[0xF] = (byte)(add > 255 ? 1 : 0);
            })},
            {0x5, new OpCode(i => $"V{i.X:x} -= V{i.Y:x}; Vf = CARRY", i =>
            {
                var sub = V[i.X] - V[i.Y];
                V[i.X] = (byte)sub;
                V[0xF] = (byte)(sub > 0 ? 1 : 0);
            })},
            {0x7, new OpCode(i => $"V{i.X:x} = V{i.Y:x} - V{i.Y:x}; Vf = CARRY", i =>
            {
                var sub = V[i.Y] - V[i.X];
                V[i.X] = (byte)sub;
                V[0xF] = (byte)(sub > 0 ? 1 : 0);
            })},
            {0x6, new OpCode(i => $"V{i.X:x} /= 2; VF = LSB", i =>
            {
                V[0xF] = (byte)(V[i.X] & 1);
                V[i.X] >>= 1;
            })},
            {0xE, new OpCode(i => $"V{i.X:x} *= 2; VF = MSB", i =>
            {
                V[0xF] = (byte)((V[i.X] >> 7) & 1);
                V[i.X] <<= 1;
            })}
        };

        _opCodesE = new Dictionary<byte, OpCode>
        {
            {0x9E, new OpCode(i => $"IF !KEY V{i.X:x} PRESSED", i =>
            {
                if (_io.Keys[V[i.X]])
                {
                    PC += 2;
                }
            })},
            {0xA1, new OpCode(i => $"IF KEY V{i.X:x} PRESSED", i =>
            {
                if (!_io.Keys[V[i.X]])
                {
                    PC += 2;
                }
            })}
        };

        _opCodesF = new Dictionary<byte, OpCode>
        {
            {0x55, new OpCode(i => $"SAVE V0-V{i.X:x} -> I", i => V[0..(i.X + 1)].CopyTo(_memory, I))},
            {0x65, new OpCode(i => $"LOAD I -> V0-V{i.X:x}", i => _memory[I..(I + i.X + 1)].CopyTo(V, 0))},
            {0x07, new OpCode(i => $"V{i.X:x} = DT", i => V[i.X] = DelayTimer)},
            {0x15, new OpCode(i => $"DT = V{i.X:x}", i => DelayTimer = V[i.X])},
            {0x18, new OpCode(i => $"ST = V{i.X:x}", i => SoundTimer = V[i.X])},
            {0x0A, new OpCode(i => $"WAIT FOR KEY; V{i.X:x} = KEY", i =>
            {
                var pressedKey = _io.GetPressedKey();
                if (pressedKey is not null)
                {
                    V[i.X] = pressedKey.Value;
                }
                else
                {
                    PC -= 2;
                }
            })},
            {0x1E, new OpCode(i => $"I = V{i.X:x}", i => I += V[i.X])},
            {0x33, new OpCode(i => $"I..I+2 = BCD V{i.X:x}", i =>
            {
                var value = V[i.X];
                _memory[I] = (byte)(value / 100);
                _memory[I + 1] = (byte)(value / 10 % 10);
                _memory[I + 2] = (byte)(value % 10);
            })},
            {0x29, new OpCode(i => $"I = FONT {i.X:x}", i => I = (ushort)(V[i.X] + 0x50))}
        };
    }

    public void Step()
    {
        var instruction = Fetch(PC);
        PC += 2;
        var opCode = Decode(instruction);
        opCode.Run(instruction);
    }

    private Instruction Fetch(ushort pc)
    {
        var opCode = _memory[pc] << 8;
        opCode += _memory[pc + 1];

        return new Instruction(
            (ushort)opCode,
            (byte)((opCode >> 12) & 0x000F),
            (byte)((opCode >> 8) & 0x000F),
            (byte)((opCode >> 4) & 0x000F),
            (byte)(opCode & 0x000F),
            (byte)(opCode & 0x00FF),
            (ushort)(opCode & 0x0FFF));
    }

    private OpCode Decode(Instruction instruction)
    {
        var unknownOpCode = new OpCode(_ => "",
            i => throw new NotImplementedException($"Unknown OpCode: {i.OpCode:x4}"));

        return instruction.Type switch
        {
            0x0 => !_opCodes0.ContainsKey(instruction.NN) ? unknownOpCode : _opCodes0[instruction.NN],
            0x8 => !_opCodes8.ContainsKey(instruction.N) ? unknownOpCode : _opCodes8[instruction.N],
            0xE => !_opCodesE.ContainsKey(instruction.NN) ? unknownOpCode : _opCodesE[instruction.NN],
            0xF => !_opCodesF.ContainsKey(instruction.NN) ? unknownOpCode : _opCodesF[instruction.NN],
            _ => !_opCodes.ContainsKey(instruction.Type) ? unknownOpCode : _opCodes[instruction.Type]
        };
    }

    public void Step60Hz()
    {
        if (DelayTimer > 0) DelayTimer--;
        if (SoundTimer > 0) SoundTimer--;
    }

    internal IEnumerable<(Instruction, OpCode)> Peek()
    {
        for (var i = -4; i < 12; i++)
        {
            var instruction = Fetch((ushort)(PC + 2 * i));
            var opCode = Decode(instruction);
            yield return (instruction, opCode);
        }
    }

    internal IEnumerable<byte> GetGraphicsMemory()
    {
        for (var i = -4; i < 12; i++)
        {
            if ((I + i) < 0 || (I + i) > 0xFFF) continue;
            yield return _memory[I + i];
        }
    }

    internal record OpCode(Func<Instruction, string> Description, Action<Instruction> Run);

    public void PrintDebug()
    {
        Console.WriteLine($"PC: {PC:x3}, I: {I:x3}");
        Console.WriteLine(string.Join(',', V.Select((x, i) => $"V{i:x}: {x:x}")));
    }
}

internal record struct Instruction(ushort OpCode, byte Type, byte X, byte Y, byte N, byte NN, ushort NNN);
