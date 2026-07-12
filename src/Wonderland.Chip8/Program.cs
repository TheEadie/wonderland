using System.CommandLine;
using Wonderland.Chip8;

var romArg = new Argument<string>("rom-path")
{
    Description = "The path to the ROM to use in the emulator."
};

var clockSpeedOption = new Option<int>("--clock-speed")
{
    Description = "The target clock speed to be used in Hz.",
    DefaultValueFactory = _ => 1000
};

var rootCommand = new RootCommand("Wonderland Chip-8 emulator");
rootCommand.Arguments.Add(romArg);
rootCommand.Options.Add(clockSpeedOption);

rootCommand.SetAction(parseResult => RunEmulator(parseResult.GetValue(romArg)!, parseResult.GetValue(clockSpeedOption)));

return await rootCommand.Parse(args).InvokeAsync();


void RunEmulator(string romPath, int clockSpeed)
{
    var emulator = new Emulator(clockSpeed);
    emulator.Load(romPath);
    emulator.Run(CancellationToken.None);
}

