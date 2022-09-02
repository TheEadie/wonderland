using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Wonderland.Chip8.IO.Buttons;
using Wonderland.Chip8.IO.Tabs;
using Wonderland.Chip8.IO.Text;

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

    private readonly TabControl _tabControl;

    public Queue<EmulatorAction> Actions { get; }

    public SfmlScreen(Gpu gpu, Cpu cpu)
    {
        _gpu = gpu;
        _cpu = cpu;

        Actions = new Queue<EmulatorAction>();

        _window = new RenderWindow(new VideoMode(738, 1026), "Wonderland", Styles.Close);
        _window.Closed += (_, _) => { _window.Close(); };
        _window.KeyPressed +=
            (sender, e) =>
            {
                var window = (Window) sender!;
                if (e.Code == Keyboard.Key.Escape)
                {
                    window.Close();
                }
            };

        _pauseButton = new Button(new Vector2f(318, 454), "||",
            (obj) =>
            {
                obj.Active = !obj.Active;
                Actions.Enqueue(EmulatorAction.Pause);
            },
            _window);
        _stepButton = new Button(new Vector2f(372, 454), "->",
            (_) => { Actions.Enqueue(EmulatorAction.Step); },
            _window);


        _tabControl = new TabControl(new Vector2f(36, 533), _window);
        _tabControl.Add("Info", new InfoTabContents(new Vector2f(24, 575), _window));
        _tabControl.Add("Debug", new DebugTabContent(new Vector2f(24, 575), _window, _cpu, _gpu));
        _tabControl.Add("Settings", new SettingsTabContents(new Vector2f(24, 575), _window, _cpu));
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

        DrawMainSection(new Vector2f(0, 0), new Vector2f(738, 1000), "Wonderland CHIP-8");
        DrawGame(new Vector2f(24, 36 + 24));

        _tabControl.Draw();

        _pauseButton.Active = _paused;
        _pauseButton.Draw(_window);
        _stepButton.Draw(_window);

        DrawFooter(new Vector2f(0, 1000), new Vector2f(738, 0));

        _window.Display();
    }

    private void DrawGame(Vector2f position)
    {
        var border = new RectangleShape(new Vector2f(640 + 48, 320 + 48));
        border.FillColor = Colours.BackgroundLevel2;
        border.Position = position + new Vector2f(0, 0);
        _window.Draw(border);

        var vRam = _gpu.GetVRam();
        var width = vRam.GetLength(0);
        var height = vRam.GetLength(1);

        const int scale = 5;

        var sfmlArray = new Color[(width) * scale, (height) * scale];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                for (var i = 0; i < scale; i++)
                {
                    for (var j = 0; j < scale; j++)
                    {
                        if (i is scale - 1 || j is scale - 1)
                        {
                            sfmlArray[x * scale + i, y * scale + j] = Colours.BackgroundLevel2;
                        }
                        else
                        {
                            sfmlArray[x * scale + i, y * scale + j] = vRam[x, y] ? Color.White : Color.Black;
                        }
                    }
                }
            }
        }

        var image = new Image(sfmlArray);
        var texture = new Texture(image);
        var sprite = new Sprite(texture);
        sprite.Position = position + new Vector2f(24, 24);
        _window.Draw(sprite);
    }

    private void DrawMainSection(Vector2f position, Vector2f size, string title)
    {
        var headerSection = new RectangleShape(new Vector2f(size.X, 36));
        headerSection.FillColor = Colours.BackgroundHeader;
        headerSection.Position = position;
        _window.Draw(headerSection);

        var text = TextFactory.Create();
        text.DisplayedString = title;
        text.FillColor = Colours.TextPrimary;
        text.CharacterSize = 16;
        text.Position = position + new Vector2f(12, 8);

        _window.Draw(text);

        var sectionBody = new RectangleShape(size);
        sectionBody.FillColor = Colours.BackgroundLevel1;
        sectionBody.Position = position + new Vector2f(0, 36);
        _window.Draw(sectionBody);

        text.CharacterSize = 14;
    }

    private void DrawFooter(Vector2f position, Vector2f size)
    {
        var section = new RectangleShape(new Vector2f(size.X, 26));
        section.FillColor = Colours.BackgroundHeader;
        section.Position = position;
        _window.Draw(section);

        var text = TextFactory.Create();
        text.DisplayedString = $"IPS: {_instructionsPerSecond}    FPS: {_fps}";
        text.FillColor = Colours.TextSecondary;
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