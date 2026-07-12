using System.CommandLine;
using Wonderland.GameBoy;

var romArg = new Argument<string>("rom-path")
{
    Description = "The path to the ROM to use in the emulator."
};

var rootCommand = new RootCommand("Wonderland GameBoy emulator");
rootCommand.Arguments.Add(romArg);

rootCommand.SetAction(parseResult => RunEmulator(parseResult.GetValue(romArg)!));

return await rootCommand.Parse(args).InvokeAsync();


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