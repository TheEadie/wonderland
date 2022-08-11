using System.CommandLine;
using Wonderland.Chip8;

var romArg = new Argument<string>(
    name: "rom-path",
    description: "The path to the ROM to use in the emulator."
);

var clockSpeedOption = new Option<int>(
    name: "--clock-speed",
    description: "The target clock speed to be used in Hz.",
    getDefaultValue: () => 1000);

var rootCommand = new RootCommand("Wonderland Chip-8 emulator");
rootCommand.AddArgument(romArg);
rootCommand.AddOption(clockSpeedOption);

rootCommand.SetHandler(RunEmulator, romArg, clockSpeedOption);

return await rootCommand.InvokeAsync(args);


void RunEmulator(string romPath, int clockSpeed)
{
    var emulator = new Emulator(clockSpeed);
    emulator.Load(romPath);
    emulator.Run(CancellationToken.None);
}

