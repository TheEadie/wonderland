namespace Wonderland.Chip8;

public class Cpu
{
    private readonly byte[] _memory;

    private readonly Gpu _gpu;
    public int PC { get; private set; }
    public int I { get; private set; }
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
        switch (instruction.One)
        {
            // Clear Screen
            case 0x0:
                // TODO: Check that its actually 0x00E0
                _gpu.Clear();
                break;
            // Jump
            case 0x1:
                PC = GetNNN(instruction);
                break;
            // Set
            case 0x6:
                V[GetX(instruction)] = GetNN(instruction);
                break;
            // Add
            case 0x7:
                V[GetX(instruction)] += GetNN(instruction);
                break;
            // Update I
            case 0xA:
                I = GetNNN(instruction);
                break;
            case 0xD:
                var end = I + GetN(instruction);
                var sprite = _memory[I..end];
                var x = V[GetX(instruction)];
                var y = V[GetY(instruction)];
                _gpu.Draw(x, y, sprite);
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
            (byte)((opCode >> 12) % 16),
            (byte)((opCode >> 8) % 16),
            (byte)((opCode >> 4) % 16),
            (byte)(opCode % 16));
        PC += 2;
        return nextMemory;
    }

    private byte GetX(Instruction instruction)
    {
        return (byte)((instruction.OpCode >> 8) & 0x000F);
    }

    private byte GetY(Instruction instruction)
    {
        return (byte)((instruction.OpCode >> 4) & 0x000F);
    }

    private ushort GetNNN(Instruction instruction)
    {
        return (ushort)(instruction.OpCode & 0x0FFF);
    }

    private byte GetNN(Instruction instruction)
    {
        return (byte)(instruction.OpCode & 0x00FF);
    }

    private byte GetN(Instruction instruction)
    {
        return (byte)(instruction.OpCode & 0x000F);
    }

    public void PrintDebug()
    {
        Console.WriteLine($"PC: {PC:x3}, I: {I:x3}");
        Console.WriteLine(string.Join(',', V.Select((x, i) => $"V{i:x}: {x:x}")));
    }
}

internal record struct Instruction(ushort OpCode, byte One, byte Two, byte Three, byte Four);