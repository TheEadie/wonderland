namespace Wonderland.Chip8;

public class Cpu
{
    private readonly byte[] _memory;

    private readonly Gpu _gpu;
    public ushort PC { get; private set; }
    public ushort I { get; private set; }
    public byte[] V { get; }
    public Stack<ushort> Stack { get; }

    public Cpu(byte[] memory, Gpu gpu)
    {
        _memory = memory;
        _gpu = gpu;
        PC = 512;
        I = 0;
        V = new byte[16];
        Stack = new Stack<ushort>();
    }

    public void Step()
    {
        var instruction = Fetch();
        switch (instruction.Type)
        {
            case 0x0:
                switch (instruction.NN)
                {
                    // Clear Screen
                    case 0xE0:
                        _gpu.Clear();
                        break;
                    // Return from sub-routine
                    case 0xEE:
                        PC = Stack.Pop();
                        break;
                    // Execute machine language routine (unused)
                    default:
                        break;
                }
                break;
            // Jump
            case 0x1:
                PC = instruction.NNN;
                break;
            // Call Sub-routine
            case 0x2:
                Stack.Push(PC);
                PC = instruction.NNN;
                break;
            // Equal
            case 0x3:
                if (V[instruction.X] == instruction.NN)
                {
                    PC += 2;
                }
                break;
            // Not equal
            case 0x4:
                if (V[instruction.X] != instruction.NN)
                {
                    PC += 2;
                }
                break;
            // Equal
            case 0x5:
                if (V[instruction.X] == V[instruction.Y])
                {
                    PC += 2;
                }
                break;
            // Set
            case 0x6:
                V[instruction.X] = instruction.NN;
                break;
            // Add
            case 0x7:
                V[instruction.X] += instruction.NN;
                break;
            // Logic and arithmetic
            case 0x8:
                switch (instruction.N)
                {
                    // Set
                    case 0x0:
                        V[instruction.X] = V[instruction.Y];
                        break;
                    // OR
                    case 0x1:
                        V[instruction.X] |= V[instruction.Y];
                        break;
                    // AND
                    case 0x2:
                        V[instruction.X] &= V[instruction.Y];
                        break;
                    // XOR
                    case 0x3:
                        V[instruction.X] ^= V[instruction.Y];
                        break;
                    // Add
                    case 0x4:
                        var add = V[instruction.X] + V[instruction.Y];
                        V[instruction.X] = (byte)add;
                        if (add > 255)
                        {
                            V[0xF] = 1;
                        }
                        else
                        {
                            V[0xF] = 0;
                        }
                        break;
                    // Subtract
                    case 0x5:
                        var subXY = V[instruction.X] - V[instruction.Y];
                        V[instruction.X] = (byte)subXY;
                        if (subXY > 0)
                        {
                            V[0xF] = 1;
                        }
                        else
                        {
                            V[0xF] = 0;
                        }
                        break;
                    // Subtract
                    case 0x7:
                        var subYX = V[instruction.Y] - V[instruction.X];
                        V[instruction.X] = (byte)subYX;
                        if (subYX > 0)
                        {
                            V[0xF] = 1;
                        }
                        else
                        {
                            V[0xF] = 0;
                        }
                        break;
                    // Shift Right
                    case 0x6:
                        V[0xF] = (byte)(V[instruction.X] & 1);
                        V[instruction.X] >>= 1;
                        break;
                    // Shift Left
                    case 0xE:
                        V[0xF] = (byte)((V[instruction.X] >> 7) & 1);
                        V[instruction.X] <<= 1;
                        break;

                }
                break;
            // Not equal
            case 0x9:
                if (V[instruction.X] != V[instruction.Y])
                {
                    PC += 2;
                }
                break;
            // Update I
            case 0xA:
                I = instruction.NNN;
                break;
            // Jump
            case 0xB:
                PC = (ushort)(V[0x0] + instruction.NNN);
                break;
            // Random
            case 0xC:
                var random = new Random();
                var num = random.Next(256);
                V[instruction.X] = (byte)(num & instruction.NN);
                break;
            // Draw sprite
            case 0xD:
                var end = I + instruction.N;
                var sprite = _memory[I..end];
                var x = V[instruction.X];
                var y = V[instruction.Y];
                _gpu.Draw(x, y, sprite);
                // TODO - set VF if any pixel are turned off
                break;
            case 0xF:
                switch (instruction.NN)
                {
                    // Font character
                    case 0x29:
                        I = (ushort)(V[instruction.X] + 0x50);
                        break;
                    // Binary coded decimal conversion
                    case 0x33:
                        var value = V[instruction.X];
                        _memory[I] = (byte)(value / 100);
                        _memory[I + 1] = (byte)(value / 10 % 10);
                        _memory[I + 2] = (byte)(value % 10);
                        break;
                    // Store memory
                    case 0x55:
                        V[0..(instruction.X + 1)].CopyTo(_memory, I);
                        break;
                    // Load memory
                    case 0x65:
                        _memory[I..(I + instruction.X + 1)].CopyTo(V, 0);
                        break;
                    // Add to index
                    case 0x1E:
                        I += V[instruction.X];
                        break;

                    default:
                        throw new NotImplementedException($"Unknown Op code: {instruction.OpCode:x}");
                }
                break;
            default:
                throw new NotImplementedException($"Unknown Op code: {instruction.OpCode:x}");
        }
    }

    private Instruction Fetch()
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
        return nextMemory;
    }

    public void PrintDebug()
    {
        Console.WriteLine($"PC: {PC:x3}, I: {I:x3}");
        Console.WriteLine(string.Join(',', V.Select((x, i) => $"V{i:x}: {x:x}")));
    }
}

internal record struct Instruction(ushort OpCode, byte Type, byte X, byte Y, byte N, byte NN, ushort NNN);