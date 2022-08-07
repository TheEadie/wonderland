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
        switch (instruction.Type)
        {
            // Clear Screen
            case 0x0:
                // TODO: Check that its actually 0x00E0
                _gpu.Clear();
                break;
            // Jump
            case 0x1:
                PC = instruction.NNN;
                break;
            // Set
            case 0x6:
                V[instruction.X] = instruction.NN;
                break;
            // Add
            case 0x7:
                V[instruction.X] += instruction.NN;
                break;
            // Update I
            case 0xA:
                I = instruction.NNN;
                break;
            // Draw sprite
            case 0xD:
                var end = I + instruction.N;
                var sprite = _memory[I..end];
                var x = V[instruction.X];
                var y = V[instruction.Y];
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