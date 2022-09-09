using System.CommandLine;
using Wonderland.GameBoy;

var romArg = new Argument<string>(
    name: "rom-path",
    description: "The path to the ROM to use in the emulator."
);

var rootCommand = new RootCommand("Wonderland GameBoy emulator");
rootCommand.AddArgument(romArg);

rootCommand.SetHandler(RunEmulator, romArg);

return await rootCommand.InvokeAsync(args);


void RunEmulator(string romPath)
{
    var mmu = new Mmu();
    mmu.LoadCart(romPath);
    var cpu = new Cpu(mmu);

    while (true)
    {
        cpu.Step();
    }
}