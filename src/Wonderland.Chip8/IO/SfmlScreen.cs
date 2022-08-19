using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Wonderland.Chip8.IO;

public class SfmlScreen : IScreen
{
    private readonly RenderWindow _window;
    private readonly Gpu _gpu;
    private readonly Cpu _cpu;

    private int _fps;
    private int _instructionsPerSecond;

    private bool _paused;
    private readonly Button _pauseButton;
    private readonly Button _stepButton;
    private readonly Tab _infoTab;
    private readonly Tab _debugTab;
    private readonly ITabContent _debugTabContents;
    private readonly Tab _settingsTab;

    public Queue<EmulatorAction> Actions { get; }


    public SfmlScreen(Gpu gpu, Cpu cpu)
    {
        _gpu = gpu;
        _cpu = cpu;

        Actions = new Queue<EmulatorAction>();

        _window = new RenderWindow(new VideoMode(690, 900), "Wonderland", Styles.Close);
        _window.Closed += (_, _) => { _window.Close(); };
        _window.KeyPressed +=
            (sender, e) =>
            {
                var window = (Window)sender!;
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }
            };

        _pauseButton = new Button(new Vector2f(690 - 25 - 42, 400), "||", 
            (obj) => { 
                obj.Active = !obj.Active;
                Actions.Enqueue(EmulatorAction.Pause);
                },
            _window);
        _stepButton = new Button(new Vector2f(690 - 25 - 42 - 5 - 42, 400), "->",
            (_) =>
            {
                Actions.Enqueue(EmulatorAction.Step);
            },
            _window);

        _infoTab = new Tab(new Vector2f(25, 408), "Info", (_) => { }, _window);
        _debugTab = new Tab(new Vector2f(25 + 68 + 2, 408), "Debug", (_) => { }, _window);
        _debugTabContents = new DebugTabContent(new Vector2f(25, 450), _window, _cpu, _gpu);
        _settingsTab = new Tab(new Vector2f(25 + 68 + 78 + 4, 408), "Settings", (_) => { }, _window);
    }

    public bool IsOpen() => _window.IsOpen;

    public void Init()
    {
        _window.Clear();
    }

    public void DrawFrame()
    {
        _window.DispatchEvents();
        _window.Clear();

        DrawMainSection(new Vector2f(1, 1), new Vector2f(690, 870), "Wonderland CHIP-8");
        DrawGame(new Vector2f(25,  62));

        _debugTab.Active = true;
        _infoTab.Draw(_window);
        _debugTab.Draw(_window);
        _settingsTab.Draw(_window);

        _pauseButton.Active = _paused;
        _pauseButton.Draw(_window);
        _stepButton.Draw(_window);

        _debugTabContents.Draw();

        DrawFooter(new Vector2f(1, 900-27), new Vector2f(690, 0));

        _window.Display();
    }
    
    private void DrawGame(Vector2f position)
    {
        var border = new RectangleShape(new Vector2f(640, 320));
        border.OutlineThickness = 1;
        border.OutlineColor = Colours.BorderInternal;
        border.FillColor = Colours.Background;
        border.Position = position + new Vector2f(0, 0);
        _window.Draw(border);
        
        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        var sfmlArray = new Color[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                sfmlArray[x, y] = vRam[x, y] ? Color.White : Color.Black;
            }
        }

        var image = new Image(sfmlArray);
        var texture = new Texture(image);
        var sprite = new Sprite(texture);
        sprite.Position = position;
        sprite.Scale = new Vector2f(10, 10);
        _window.Draw(sprite);
    }

    private void DrawMainSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 36));
        headerSection.OutlineThickness = 1;
        headerSection.OutlineColor = Colours.BorderInternal;
        headerSection.FillColor = Colours.BackgroundDark;
        headerSection.Position = position;
        _window.Draw(headerSection);

        var text = TextFactory.Create();
        text.DisplayedString = title;
        text.FillColor = Colours.TextColour;
        text.CharacterSize = 16;
        text.Position = position + new Vector2f(12, 8);

        _window.Draw(text);

        var sectionBody = new RectangleShape(size);
        sectionBody.OutlineThickness = 1;
        sectionBody.OutlineColor = Colours.BorderInternal;
        sectionBody.FillColor = Colours.Background;
        sectionBody.Position = position + new Vector2f(0, 36);
        _window.Draw(sectionBody);
        
        text.CharacterSize = 14;
    }

    private void DrawFooter(Vector2f position, Vector2f size)
    {
        var section = new RectangleShape(new Vector2f(size.X, 26));
        section.OutlineThickness = 1;
        section.OutlineColor = Colours.BorderInternal;
        section.FillColor = Colours.BackgroundDark;
        section.Position = position;
        _window.Draw(section);

        var text = TextFactory.Create();
        text.DisplayedString = $"IPS: {_instructionsPerSecond}    FPS: {_fps}";
        text.FillColor = Colours.TextAlt;
        text.Position = position + size - new Vector2f(text.DisplayedString.Length * 8 + 14, -4);

        _window.Draw(text);
    }

    public void UpdateStatus(int fps, int instructionsPerSecond)
    {
        _fps = fps;
        _instructionsPerSecond = instructionsPerSecond;
        
    }

    public void UpdateButtonStates(bool pause)
    {
        _paused = pause;
    }
}
