namespace Wonderland.Chip8;

public interface IScreen
{
    Queue<EmulatorAction> Actions { get; }

    void Init();
    void DrawFrame();
    void UpdateStatus(int fps, int instructionsPerSecond);
    void UpdateButtonStates(bool pause);
    bool IsOpen();
}
