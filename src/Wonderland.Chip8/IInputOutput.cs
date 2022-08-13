namespace Wonderland.Chip8;

public interface IInputOutput
{
    bool[] Keys { get; }
    bool Pause { get; }
    bool StepForward { get; }
    byte? GetPressedKey();
    void Beep();
    void Step();
}
