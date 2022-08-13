namespace Wonderland.Chip8;

public interface IScreen
{
    void Init();
    void DrawFrame();
    void UpdateStatus(int fps, int instructionsPerSecond);
}
