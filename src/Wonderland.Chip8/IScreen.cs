namespace Wonderland.Chip8;

public interface IScreen
{
    void Init();
    void DrawFrame();
    void UpdateStats(int fps, int instructionsPerSecond);
}