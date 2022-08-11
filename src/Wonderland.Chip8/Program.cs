using System.CommandLine;
using Wonderland.Chip8;

var romOption = new Option<string>(
    name: "--rom",
    description: "The path to the ROM to use in the emulator."
) {IsRequired = true};

var clockSpeedOption = new Option<int>(
    name: "--clock-speed",
    description: "The target clock speed to be used in Hz.",
    getDefaultValue: () => 1000);

var rootCommand = new RootCommand("Wonderland Chip-8 emulator");
rootCommand.AddOption(romOption);
rootCommand.AddOption(clockSpeedOption);

rootCommand.SetHandler(async (file, clockSpeed) => 
    { 
        await RunEmulator(file, clockSpeed); 
    },
    romOption,
    clockSpeedOption);

return await rootCommand.InvokeAsync(args);


async Task RunEmulator(string romPath, int clockSpeed)
{
    var emulator = new Emulator(clockSpeed);
    emulator.Load(romPath);
    await emulator.Run(CancellationToken.None);
}

